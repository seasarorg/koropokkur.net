#region Copyright
/*
 * Copyright 2005-2010 the Seasar Foundation and the Others.
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

namespace CopyGen.Gen.Impl.Cs
{
    /// <summary>
    /// コピー処理生成対象情報生成ロジック（C#)
    /// </summary>
    public class CopyTargetBaseInfoCreatorCs : AbstractCopyTargetBaseInfoCreator
    {
        /// <summary>
        /// 規定区切り文字
        /// </summary>
        /// <remarks>
        /// 現状カンマで固定のつもりだが将来の変更用に
        /// </remarks>
        protected static readonly string default_SPRIT_STR = new string(new char[] { DEFAULT_SPRIT_CHAR });

        protected override CopyTargetBaseInfo CreateTargetBaseInfo(string editingFilePath, string sourceTypeName, string destTypeName)
        {
            IList<string> usingNamespaces = GetUsingNamespaces(editingFilePath);

            string sourceTypeFullNames = sourceTypeName;
            //  「.」が含まれていない＝名前空間を推測する必要あり
            if (!sourceTypeName.Contains("."))
            {
                sourceTypeFullNames = GetTypeNames(sourceTypeName, usingNamespaces);
            }

            string destTypeFullNames = destTypeName;
            if (!destTypeName.Contains("."))
            {
                destTypeFullNames = GetTypeNames(destTypeName, usingNamespaces);
            }

            return new CopyTargetBaseInfo(sourceTypeFullNames, destTypeFullNames);
        }

        /// <summary>
        /// 名前空間の候補となるusing句一覧を取得する
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        protected virtual IList<string> GetUsingNamespaces(string filePath)
        {
            if (filePath == null || !File.Exists(filePath))
            {
                //  TODO:メッセージ文字列定義方法の検討
                throw new FileNotFoundException("編集中のファイルが見つかりません。", filePath);
            }

            IList<string> usingNamespaces = new List<string>();
            string defaultNamespace = AssemblyUtils.GetNamespace(filePath);
            if (!string.IsNullOrEmpty(defaultNamespace))
            {
                usingNamespaces.Add(defaultNamespace);
            }

            using (StreamReader reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line.Contains("using"))
                    {
                        string usingNamespace = line.Replace("using", "").Replace(";", "").Trim();
                        usingNamespaces.Add(usingNamespace);
                    }
                    else if (line.Contains("namespace"))
                    {
                        //  namespace以後にusing句が出てくることはないのでファイル読み込み終了
                        break;
                    }
                }
            }
            return usingNamespaces;
        }

        /// <summary>
        /// 名前空間付き型名候補一覧の取得
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="usingNamespaces"></param>
        /// <returns></returns>
        protected virtual string GetTypeNames(string typeName, IEnumerable<string> usingNamespaces)
        {
            if (typeName == null) throw new ArgumentNullException("typeName");
            if (usingNamespaces == null) throw new ArgumentNullException("usingNamespaces");

            List<string> typeNames = new List<string>();
            //  デフォルトとして名前空間なしも含める
            typeNames.Add(typeName);
            foreach (string usingNamespace in usingNamespaces)
            {
                if (!string.IsNullOrEmpty(usingNamespace))
                {
                    typeNames.Add(string.Format("{0}.{1}", usingNamespace, typeName));
                }
            }
            return string.Join(default_SPRIT_STR, typeNames.ToArray());
        }
    }
}
