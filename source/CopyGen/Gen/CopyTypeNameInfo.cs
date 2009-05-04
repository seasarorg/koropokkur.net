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
using AddInCommon.Util;

namespace CopyGen.Gen
{
    /// <summary>
    /// コード生成基礎情報クラス(入力された文字列から生成する型名候補を決める）
    /// </summary>
    public class CopyTypeNameInfo
    {
        /// <summary>
        /// 規定区切り文字
        /// </summary>
        /// <remarks>
        /// 現状カンマで固定のつもりだが将来の変更用に
        /// </remarks>
        private const char DEFAULT_SPRIT_CHAR = ',';
        /// <summary>
        /// 規定区切り文字
        /// </summary>
        /// <remarks>
        /// 現状カンマで固定のつもりだが将来の変更用に
        /// </remarks>
        private static readonly string default_SPRIT_STR = new string(new char[] { DEFAULT_SPRIT_CHAR });

        private readonly string _sourceTypeFullNames;
        /// <summary>
        /// 名前空間を含むコピー元クラス名の候補
        /// </summary>
        public string SourceTypeFullNames
        {
            get { return _sourceTypeFullNames; }
        }

        private readonly string _targetTypeFullNames;
        /// <summary>
        /// 名前空間を含むコピー先クラス名の候補
        /// </summary>
        public string TargetTypeFullNames
        {
            get { return _targetTypeFullNames; }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="sourceTypeFullNames"></param>
        /// <param name="targetTypeFullNames"></param>
        protected CopyTypeNameInfo(string sourceTypeFullNames, string targetTypeFullNames)
        {
            _sourceTypeFullNames = sourceTypeFullNames;
            _targetTypeFullNames = targetTypeFullNames;
        }

        /// <summary>
        /// コード生成情報の生成
        /// </summary>
        /// <param name="editingFilePath"></param>
        /// <param name="targetLine"></param>
        /// <returns></returns>
        public static CopyTypeNameInfo Create(string editingFilePath, string targetLine)
        {
            IList<string> usingNamespaces = GetUsingNamespaces(editingFilePath);

            string[] copyTypeNames = targetLine.Trim().Split(DEFAULT_SPRIT_CHAR);
            if(copyTypeNames.Length == 0)
            {
                return null;
            }

            string defaultTypeName = AssemblyUtils.GetTypeName(editingFilePath);
            string sourceTypeName = copyTypeNames[0].Trim();
            if(sourceTypeName.Length == 0)
            {
                sourceTypeName = defaultTypeName;
            }

            //  「,」で区切られていなかった場合は同じ型同士でのコピーとなる
            string targetTypeName = copyTypeNames.Length > 1 ? copyTypeNames[1].Trim() : sourceTypeName;
            if(targetTypeName.Length == 0)
            {
                targetTypeName = defaultTypeName;
            }

            string sourceTypeFullNames = sourceTypeName;
            //  「.」が含まれていない＝名前空間を推測する必要あり
            if(!sourceTypeName.Contains("."))
            {
                sourceTypeFullNames = GetTypeNames(sourceTypeName, usingNamespaces);
            }

            string targetTypeFullNames = targetTypeName;
            if(!targetTypeName.Contains("."))
            {
                targetTypeFullNames = GetTypeNames(targetTypeName, usingNamespaces);
            }

            return new CopyTypeNameInfo(sourceTypeFullNames, targetTypeFullNames);
        }

        /// <summary>
        /// 名前空間付き型名候補一覧の取得
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="usingNamespaces"></param>
        /// <returns></returns>
        private static string GetTypeNames(string typeName, IEnumerable<string> usingNamespaces)
        {
            if (typeName == null) throw new ArgumentNullException("typeName");
            if (usingNamespaces == null) throw new ArgumentNullException("usingNamespaces");

            List<string> typeNames = new List<string>();
            foreach (string usingNamespace in usingNamespaces)
            {
                typeNames.Add(string.Format("{0}.{1}", usingNamespace, typeName));   
            }
            return string.Join(default_SPRIT_STR, typeNames.ToArray());
        }

        /// <summary>
        /// 名前空間の候補となるusing句一覧を取得する
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static IList<string> GetUsingNamespaces(string filePath)
        {
            if(filePath == null || !File.Exists(filePath))
            {
                //  TODO:メッセージ文字列定義方法の検討
                throw new FileNotFoundException("編集中のファイルが見つかりません。", filePath);
            }

            IList<string> usingNamespaces = new List<string>();
            string defaultNamespace = AssemblyUtils.GetNamespace(filePath);
            if(!string.IsNullOrEmpty(defaultNamespace))
            {
                usingNamespaces.Add(defaultNamespace);
            }

            using(StreamReader reader = new StreamReader(filePath))
            {
                while(!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if(line.Contains("using"))
                    {
                        string usingNamespace = line.Replace("using", "").Replace(";", "").Trim();
                        usingNamespaces.Add(usingNamespace);
                    }
                    else if(line.Contains("namespace"))
                    {
                        //  namespace以後にusing句が出てくることはないのでファイル読み込み終了
                        break;
                    }
                }
            }
            return usingNamespaces;
        }
    }
}
