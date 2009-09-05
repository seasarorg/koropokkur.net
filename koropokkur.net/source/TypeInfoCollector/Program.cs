#region Copyright
/*
 * Copyright 2005-2009 the Seasar Foundation and the Others.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
 * either express or implied. See the License for the specific language
 * governing permissions and limitations under the License.
 */
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace TypeInfoCollector
{
    class Program
    {
        /// <summary>
        /// 指定したアセンブリのプロパティ名をファイルに書き出します（CopyGen用)
        /// </summary>
        /// <remarks>
        /// ・アドイン内でアセンブリを読み込むとプロセスが終了するまでファイルをつかみ続けるためコンパイルできなくなる
        /// ・AppDomainを使う場合はアセンブリの厳密名（PublicKeyTokenなどが含まれているもの)を渡す必要がある
        /// 以上の問題を解決するための苦肉の策として完全に別プロセスで
        /// プロパティ情報抽出処理を行っています。
        /// </remarks>
        /// <param name="args">
        /// [0]:コピー元プロパティ一覧の出力先
        /// [1]:コピー先プロパティ一覧の出力先
        /// [2]:読み込むアセンブリのパス
        /// [3]:プロパティ情報を取り出したいコピー元の型
        /// [4]:プロパティ情報を取り出したいコピー先の型
        /// [5]:ログ出力パス
        /// </param>
        static void Main(string[] args)
        {
            using (var errorWriter = new StreamWriter("c:\\TypeInfoCollector.error.log", true))
            {
                foreach (var s in args)
                {
                    errorWriter.WriteLine(s);
                }
            }

            string sourcePropOutputPath = args[0];
            string targetPropOutputPath = args[1];
            string assemblyPathSource = args[2];
            string sourceTypeName = args[3];
            string targetTypeName = args[4];
            string logFilePath = args[5];

            try
            {
                //  古いプロパティ情報は消しておく
                DeleteFile(sourcePropOutputPath);
                DeleteFile(targetPropOutputPath);

                string[] assemblyPaths = assemblyPathSource.Split(',');
                string[] sourceTypeNames = sourceTypeName.Split(',');
                string[] targetTypeNames = targetTypeName.Split(',');
                Type sourceType = null;
                Type targetType = null;
                bool isSameType = (sourceTypeName == targetTypeName);
                //  型情報が見つかるまで各アセンブリ情報を調べる
                foreach (string assemblyPath in assemblyPaths)
                {
                    if(!File.Exists(assemblyPath))
                    {
                        continue;
                    }

                    Assembly assembly = Assembly.LoadFrom(assemblyPath);
                    if (sourceType == null)
                    {
                        sourceType = GetType(assembly, sourceTypeNames);
                        if(isSameType)
                        {
                            targetType = sourceType;
                        }
                    }
                    //  コピー元とコピー先が違う場合のみ
                    if(targetType == null && !isSameType)
                    {
                        targetType = GetType(assembly, targetTypeNames);
                    }

                    //  コピー先、コピー元の両方の型情報が取得できたらループを抜ける
                    if(sourceType != null && targetType != null)
                    {
                        break;
                    }
                }
                
                //  型情報が見つからなかった場合
                if(sourceType == null || targetType == null)
                {
                    OutputNotFoundTypeWarning(sourceType, targetType, sourceTypeName, 
                        targetTypeName, assemblyPathSource, logFilePath);
                    return;
                }

                OutputPropertyInfos(sourceType, sourcePropOutputPath);
                OutputPropertyInfos(targetType, targetPropOutputPath);
            }
            catch (Exception ex)
            {
                using (var errorWriter = new StreamWriter(logFilePath, true))
                {
                    errorWriter.WriteLine("{0} {1}\n{2}", DateTime.Now, ex.Message, ex.StackTrace);
                }
            }
        }

        /// <summary>
        /// プロパティ情報の出力
        /// </summary>
        /// <param name="type"></param>
        /// <param name="outputPath"></param>
        private static void OutputPropertyInfos(Type type, string outputPath)
        {
            PropertyInfo[] propertyInfos = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                writer.WriteLine(type.FullName);
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    if (propertyInfo.CanRead && propertyInfo.CanWrite
                        && propertyInfo.GetGetMethod() != null
                        && propertyInfo.GetSetMethod() != null)
                    {
                        writer.WriteLine(propertyInfo.Name);
                    }
                }
            }
        }

        /// <summary>
        /// 型情報が見つからなかったことをしらせるメッセージを出力
        /// </summary>
        /// <param name="sourceType"></param>
        /// <param name="targetType"></param>
        /// <param name="sourceTypeName"></param>
        /// <param name="targetTypeName"></param>
        /// <param name="assemblyPathSource"></param>
        /// <param name="logFilePath"></param>
        private static void OutputNotFoundTypeWarning(Type sourceType, Type targetType,
            string sourceTypeName, string targetTypeName, string assemblyPathSource,
            string logFilePath)
        {
            using (StreamWriter writer = new StreamWriter(string.Format(logFilePath, assemblyPathSource), true))
            {
                if (sourceType == null)
                {
                    writer.WriteLine("{0} [{1}] is not found in [{2}]",
                                     DateTime.Now, sourceTypeName, assemblyPathSource);
                }

                if (targetType == null)
                {
                    writer.WriteLine("{0} [{1}] is not found in [{2}]",
                                     DateTime.Now, targetTypeName, assemblyPathSource);
                }
            }
        }

        /// <summary>
        /// 型情報の取得
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="typeNames"></param>
        /// <returns></returns>
        private static Type GetType(Assembly assembly, IEnumerable<string> typeNames)
        {
            Type retType = null;
            IDictionary<string, Type> typeMap = new Dictionary<string, Type>();
            foreach (Type type in assembly.GetTypes())
            {
                typeMap[type.Name] = type;
            }

            foreach (string typeName in typeNames)
            {
                if(typeMap.ContainsKey(typeName))
                {
                    retType = typeMap[typeName];
                    break;
                }
            }
            return retType;
        }

        /// <summary>
        /// ファイルを削除する
        /// </summary>
        /// <param name="path"></param>
        private static void DeleteFile(string path)
        {
            if(File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
