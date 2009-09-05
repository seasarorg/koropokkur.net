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
using System.Text;
using AddInCommon.Util;

namespace CopyGen.Gen.Impl
{
    /// <summary>
    /// コピー処理生成対象情報生成ロジック抽象クラス
    /// </summary>
    public abstract class AbstractCopyTargetBaseInfoCreator : ICopyTargetBaseInfoCreator
    {
        /// <summary>
        /// 規定区切り文字
        /// </summary>
        /// <remarks>
        /// 現状カンマで固定のつもりだが将来の変更用に
        /// </remarks>
        protected const char DEFAULT_SPRIT_CHAR = ',';

        /// <summary>
        /// コピー処理生成対象情報生成
        /// </summary>
        /// <param name="editingFilePath"></param>
        /// <param name="sourceTypeName"></param>
        /// <param name="targetTypeName"></param>
        /// <returns></returns>
        protected abstract CopyTargetBaseInfo CreateTargetBaseInfo(
            string editingFilePath, string sourceTypeName, string targetTypeName);

        #region ICopyTargetBaseInfoCreator メンバ

        public virtual CopyTargetBaseInfo Create(string editingFilePath, string targetLine)
        {
            string[] copyTypeNames = targetLine.Trim().Split(DEFAULT_SPRIT_CHAR);
            if (copyTypeNames.Length == 0)
            {
                return null;
            }

            string defaultTypeName = AssemblyUtils.GetTypeName(editingFilePath);
            string sourceTypeName = copyTypeNames[0].Trim();
            if (sourceTypeName.Length == 0)
            {
                sourceTypeName = defaultTypeName;
            }

            //  「,」で区切られていなかった場合は同じ型同士でのコピーとなる
            string targetTypeName = copyTypeNames.Length > 1 ? copyTypeNames[1].Trim() : sourceTypeName;
            if (targetTypeName.Length == 0)
            {
                targetTypeName = defaultTypeName;
            }

            return CreateTargetBaseInfo(editingFilePath, sourceTypeName, targetTypeName);
        }
        #endregion
    }
}
