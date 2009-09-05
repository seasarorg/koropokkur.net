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

using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using AddInCommon.Util;
using CopyGen.Gen;

namespace CopyGen.Util
{
    /// <summary>
    /// コード生成情報の抽出ユーティリティ
    /// </summary>
    public class CodeInfoUtils
    {
        /// <summary>
        /// 空白、タブ以外の文字列を取り出すための正規表現
        /// </summary>
        private static readonly Regex _regNotSpace = new Regex(@"[^ \t]");

        /// <summary>
        /// プロパティ情報収集プログラム名
        /// </summary>
        private const string FILE_NAME_PROPERTY_INFO_COLLECTOR = "TypeInfoCollector.exe";
        /// <summary>
        /// プロパティ情報ファイル名（コピー元）
        /// </summary>
        private const string FILE_NAME_SOURCE_PROPERTY_INFO = "source_props.txt";
        /// <summary>
        /// プロパティ情報ファイル名（コピー先）
        /// </summary>
        private const string FILE_NAME_TARGET_PROPERTY_INFO = "target_props.txt";

        /// <summary>
        /// インデントの取得
        /// </summary>
        /// <param name="input"></param>
        /// <param name="defaultIndent"></param>
        /// <returns></returns>
        public static string GetIndent(string input, string defaultIndent)
        {
            string indent = _regNotSpace.Replace(input, string.Empty);
            if (indent.Length == 0)
            {
                return defaultIndent;
            }
            return indent;
        }

        /// <summary>
        /// プロパティ情報の読み込み
        /// </summary>
        /// <param name="assemblyPaths"></param>
        /// <param name="sourceTypeNames"></param>
        /// <param name="targetTypeNames"></param>
        /// <returns></returns>
        public static PropertyCodeInfo ReadPropertyInfo(string assemblyPaths, string sourceTypeNames, string targetTypeNames)
        {
            string path = PathUtils.GetFolderPath(AssemblyUtils.GetExecutingAssemblyPath());

            string sourcePropInfoPath = string.Format("{0}{1}", path, FILE_NAME_SOURCE_PROPERTY_INFO);
            string targetPropInfoPath = string.Format("{0}{1}", path, FILE_NAME_TARGET_PROPERTY_INFO);

            //  型情報を出力
            ExtractPropertyInfo(assemblyPaths, sourcePropInfoPath, targetPropInfoPath, sourceTypeNames, targetTypeNames);

            PropertyCodeInfo propertyCodeInfo = new PropertyCodeInfo();

            string useSourceTypeName = null;
            IList<string> sourcePropList = ReadPropertyInfo(sourcePropInfoPath, ref useSourceTypeName);
            propertyCodeInfo.SourcePropertyNames = sourcePropList;
            propertyCodeInfo.SourceTypeName = useSourceTypeName;

            string useTargetTypeName = null;
            IList<string> targetPropList = ReadPropertyInfo(targetPropInfoPath, ref useTargetTypeName);
            propertyCodeInfo.TargetPropertyNames = targetPropList;
            propertyCodeInfo.TargetTypeName = useTargetTypeName;

            return propertyCodeInfo;
        }

        #region private
        /// <summary>
        /// プロパティ情報を収集＆ファイル出力
        /// </summary>
        /// <param name="targetAssemblyPath">アセンブリ候補</param>
        /// <param name="sourcePropInfoPath">コピー元プロパティ情報出力先パス</param>
        /// <param name="targetPropInfoPath">コピー先プロパティ情報出力先パス</param>
        /// <param name="proposedSourceTypeNames">コピー元型候補</param>
        /// <param name="proposedTargetTypeNames">コピー先型候補</param>
        private static void ExtractPropertyInfo(string targetAssemblyPath,
            string sourcePropInfoPath, string targetPropInfoPath,
            string proposedSourceTypeNames, string proposedTargetTypeNames )
        {
            string path = PathUtils.GetFolderPath(AssemblyUtils.GetExecutingAssemblyPath());
            string logFilePath = path + "TypeInfoCollector.log";
            ProcessUtils.StartProcessWithoutWindow(
                string.Format("{0}{1}", path, FILE_NAME_PROPERTY_INFO_COLLECTOR),
                string.Format("\"{0}\" \"{1}\" \"{2}\" \"{3}\" \"{4}\" \"{5}\"",
                    sourcePropInfoPath, targetPropInfoPath, targetAssemblyPath, 
                    proposedSourceTypeNames, proposedTargetTypeNames, logFilePath));
        }

        /// <summary>
        /// プロパティ情報を読み込む
        /// </summary>
        /// <param name="propInfoPath"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        private static IList<string> ReadPropertyInfo(string propInfoPath, ref string typeName)
        {
            List<string> propList = new List<string>();
            //  プロパティ情報の生成に失敗している場合は処理を終了
            if (!File.Exists(propInfoPath))
            {
                return null;
            }

            using (StreamReader reader = new StreamReader(propInfoPath))
            {
                if (!reader.EndOfStream)
                {
                    typeName = reader.ReadLine();
                }

                while (!reader.EndOfStream)
                {
                    propList.Add(reader.ReadLine());
                }
            }
            return propList;
        }

        #endregion
    }
}