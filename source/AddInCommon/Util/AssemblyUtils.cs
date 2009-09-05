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
using EnvDTE;
using VSLangProj;

namespace AddInCommon.Util
{
    /// <summary>
    /// アセンブリ情報を使用した処理を取り扱うユーティリティ
    /// </summary>
    public static class AssemblyUtils
    {
        /// <summary>
        /// 実行中のアセンブリパスを返す
        /// </summary>
        /// <returns></returns>
        public static string GetExecutingAssemblyPath()
        {
            return GetAssemblyPath(Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// アセンブリパスを返す
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static string GetAssemblyPath(Assembly assembly)
        {
            return assembly.CodeBase.Replace("file:///", "");
        }

        /// <summary>
        /// ドキュメント情報から型名を取得する
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static string GetTypeName(Document document)
        {
            return GetTypeName(document.FullName);
        }

        /// <summary>
        /// ドキュメント情報から型名を取得する
        /// </summary>
        /// <param name="documentPath"></param>
        /// <returns></returns>
        public static string GetTypeName(string documentPath)
        {
            string ns = GetNamespace(documentPath);
            string className = Path.GetFileNameWithoutExtension(documentPath);
            if(string.IsNullOrEmpty(ns))
            {
                return className;
            }
            return ns + "." + className;
        }

        /// <summary>
        /// ファイルから名前空間を取得
        /// </summary>
        /// <param name="documentPath"></param>
        /// <returns></returns>
        public static string GetNamespace(string documentPath)
        {
            string ns = "";
            using (StreamReader reader = new StreamReader(documentPath))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line.Contains("namespace"))
                    {
                        //  名前空間名の抽出
                        ns = line.Replace("namespace", "").Replace("{", "").Trim();
                        break;
                    }
                }
            }
            return ns;
        }

        /// <summary>
        /// ドキュメント情報からアセンブリ名を取得
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static string GetAssemblyName(Document document)
        {
            EnvDTE.Project project = document.ProjectItem.ContainingProject;
            EnvDTE.Configuration config =
               project.ConfigurationManager.ActiveConfiguration;

            // =====================================================
            //                      ビルド出力先のパスを取得して表示
            //                      ================================
            string currentAssemblyPath =
               string.Format(@"{0}{1}{2}",
                   project.Properties.Item("FullPath").Value,
                   config.Properties.Item("OutputPath").Value,
                   project.Properties.Item("OutputFileName").Value);
            return currentAssemblyPath;
        }

        /// <summary>
        /// 参照先アセンブリパス一覧を取得
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static string GetReferencePath(Document document)
        {
            EnvDTE.Project project = document.ProjectItem.ContainingProject;
            VSProject vsProject = project.Object as VSProject;
            if(vsProject == null)
            {
                return null;
            }

            List<string> referencePaths = new List<string>();
            referencePaths.Add(GetAssemblyName(document));

            foreach (Reference reference in vsProject.References)
            {
                referencePaths.Add(reference.Path);
            }

            return string.Join(",", referencePaths.ToArray());
        }

        /// <summary>
        /// コンストラクタ情報の取得
        /// </summary>
        /// <param name="type"></param>
        /// <param name="argTypes"></param>
        /// <returns></returns>
        public static ConstructorInfo GetConstructorInfo(Type type, Type[] argTypes)
        {
            Type[] types = argTypes ?? Type.EmptyTypes;
            ConstructorInfo constructor = type.GetConstructor(types);
            return constructor;
        }

        /// <summary>
        /// クラス名からType情報を取得
        /// </summary>
        /// <param name="className"></param>
        /// <param name="assemblys"></param>
        /// <returns></returns>
        public static Type ForName(string className, Assembly[] assemblys)
        {
            Type type = Type.GetType(className);
            if (type != null)
            {
                return type;
            }
            foreach (Assembly assembly in assemblys)
            {
                type = assembly.GetType(className);
                if (type != null)
                {
                    return type;
                }
            }
            return null;
        }

        /// <summary>
        /// 現在使用可能なアセンブリの中から、
        /// クラス名を使って型を取得する
        /// </summary>
        /// <param name="className">名前空間を含むクラス名</param>
        /// <returns>該当する型</returns>
        public static Type ForName(string className)
        {
            return ForName(className, AppDomain.CurrentDomain.GetAssemblies());
        }

        /// <summary>
        /// インスタンスの生成
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object NewInstance(Type type)
        {
            return Activator.CreateInstance(type);
        }

        /// <summary>
        /// クラス名とアセンブリ名からインスタンスを生成
        /// </summary>
        /// <param name="className"></param>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        public static object NewInstance(string className, string assemblyName)
        {
            Assembly[] asms = new Assembly[1] { Assembly.LoadFrom(assemblyName) };
            return NewInstance(ForName(className, asms));
        }
    }
}