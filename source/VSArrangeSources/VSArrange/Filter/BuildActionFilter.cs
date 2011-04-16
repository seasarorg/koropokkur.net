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

using System.Collections.Generic;
using AddInCommon.Const;
using VSArrange.Config;

namespace VSArrange.Filter
{
    /// <summary>
    /// 「ビルドアクション」フィルター判定クラス
    /// </summary>
    public class BuildActionFilter
    {
        /// <summary>
        /// プロジェクト要素に設定する属性付加の判定に使う正規表現
        /// </summary>
        private readonly ItemAttachmentFilter _filterCompile = new ItemAttachmentFilter();

        /// <summary>
        /// プロジェクト要素に設定する属性付加の判定に使う正規表現
        /// </summary>
        private readonly ItemAttachmentFilter _filterResource = new ItemAttachmentFilter();

        /// <summary>
        /// プロジェクト要素に設定する属性付加の判定に使う正規表現
        /// </summary>
        private readonly ItemAttachmentFilter _filterContents = new ItemAttachmentFilter();

        /// <summary>
        /// プロジェクト要素に設定する属性付加の判定に使う正規表現
        /// </summary>
        private readonly ItemAttachmentFilter _filterNoAction = new ItemAttachmentFilter();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="configInfo"></param>
        public BuildActionFilter(ConfigInfo configInfo)
        {
            _filterCompile.ReplaceFilters(configInfo.FilterCompileStringList);
            _filterResource.ReplaceFilters(configInfo.FilterResourceStringList);
            _filterContents.ReplaceFilters(configInfo.FilterContentsStringList);
            _filterNoAction.ReplaceFilters(configInfo.FilterNoActionStringList);
        }
        
        /// <summary>
        /// 設定する「ビルドアクション」値を取得する
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public EnumBuildAction GetBuildAction(string keyWord)
        {
            if (_filterCompile.IsHitFilter(keyWord))
            {
                return EnumBuildAction.Compile;
            }

            if (_filterResource.IsHitFilter(keyWord))
            {
                return EnumBuildAction.Resource;
            }

            if (_filterContents.IsHitFilter(keyWord))
            {
                return EnumBuildAction.Contents;
            }

            if (_filterNoAction.IsHitFilter(keyWord))
            {
                return EnumBuildAction.NoAction;
            }

            return EnumBuildAction.Nothing;
        }
    }
}
