#region Copyright
/*
 * Copyright 2005-2011 the Seasar Foundation and the Others.
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
using VSArrange.Config;
using AddInCommon.Const;

namespace VSArrange.Filter
{
    /// <summary>
    /// 「出力ディレクトリにコピー」フィルター判定クラス
    /// </summary>
    public class CopyToOutputDirectoryFilter
    {
         #region プロパティ

        /// <summary>
        /// プロジェクト要素に設定する属性付加の判定に使う正規表現
        /// </summary>
        private readonly ItemAttachmentFilter _filterNoCopy = new ItemAttachmentFilter();

        /// <summary>
        /// プロジェクト要素に設定する属性付加の判定に使う正規表現
        /// </summary>
        private readonly ItemAttachmentFilter _filterEverytimeCopy = new ItemAttachmentFilter();

        /// <summary>
        /// プロジェクト要素に設定する属性付加の判定に使う正規表現
        /// </summary>
        private readonly ItemAttachmentFilter _filterCopyIfNew = new ItemAttachmentFilter();

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="configInfo"></param>
        public CopyToOutputDirectoryFilter(ConfigInfo configInfo)
        {
            _filterNoCopy.ReplaceFilters(configInfo.FilterNoCopyStringList);
            _filterEverytimeCopy.ReplaceFilters(configInfo.FilterEverytimeCopyStringList);
            _filterCopyIfNew.ReplaceFilters(configInfo.FilterCopyIfNewStringList);
        }

        /// <summary>
        /// 「出力ディレクトリへコピー」設定値の取得
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public EnumCopyToOutputDirectory GetCopyToOutputDeirectory(string fileName)
        {
            if (_filterNoCopy.IsHitFilter(fileName))
            {
                return EnumCopyToOutputDirectory.NotCopy;
            }

            if (_filterEverytimeCopy.IsHitFilter(fileName))
            {
                return EnumCopyToOutputDirectory.EveryTime;
            }

            if (_filterCopyIfNew.IsHitFilter(fileName))
            {
                return EnumCopyToOutputDirectory.IfModified;
            }

            return EnumCopyToOutputDirectory.Nothing;
        }
    }
}
