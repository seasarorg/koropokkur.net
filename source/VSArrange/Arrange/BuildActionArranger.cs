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
using AddInCommon.Invoke;
using AddInCommon.Util;
using VSArrange.Filter;
using VSLangProj;
using VSArrange.Config;

namespace VSArrange.Arrange
{
    /// <summary>
    /// 「ビルドアクション」設定クラス
    /// </summary>
    public class BuildActionArranger : IProjectItemAccessor
    {
        #region プロパティ

        /// <summary>
        /// プロジェクト要素に設定する属性付加の判定に使う正規表現
        /// </summary>
        private readonly ItemAttachmentFilter _filterCompile = new ItemAttachmentFilter();

        /// <summary>
        /// ビルドアクション「コンパイル」設定フィルター
        /// </summary>
        public ItemAttachmentFilter FilterCompile
        {
            get { return _filterCompile; }
        }

        /// <summary>
        /// プロジェクト要素に設定する属性付加の判定に使う正規表現
        /// </summary>
        private readonly ItemAttachmentFilter _filterResource = new ItemAttachmentFilter();

        /// <summary>
        /// ビルドアクション「埋め込みリソース」設定フィルター
        /// </summary>
        public ItemAttachmentFilter FilterResource
        {
            get { return _filterResource; }
        }

        /// <summary>
        /// プロジェクト要素に設定する属性付加の判定に使う正規表現
        /// </summary>
        private readonly ItemAttachmentFilter _filterContents = new ItemAttachmentFilter();

        /// <summary>
        /// ビルドアクション「コンテンツ」設定フィルター
        /// </summary>
        public ItemAttachmentFilter FilterContents
        {
            get { return _filterContents; }
        }

        /// <summary>
        /// プロジェクト要素に設定する属性付加の判定に使う正規表現
        /// </summary>
        private readonly ItemAttachmentFilter _filterNoAction = new ItemAttachmentFilter();

        /// <summary>
        /// ビルドアクション「なし」設定フィルター
        /// </summary>
        public ItemAttachmentFilter FilterNoAction
        {
            get { return _filterNoAction; }
        }

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="configInfo"></param>
        public BuildActionArranger(ConfigInfo configInfo)
        {
            AddFilters(FilterCompile, configInfo.FilterCompileStringList);
            AddFilters(FilterResource, configInfo.FilterResourceStringList);
            AddFilters(FilterContents, configInfo.FilterContentsStringList);
            AddFilters(FilterNoAction, configInfo.FilterNoActionStringList);
        }

        #region IProjectItemAccessor メンバ

        public void AccessFile(EnvDTE.ProjectItem projectItem)
        {
            string fileName = ProjectItemUtils.GetFileName(projectItem);
            prjBuildAction currentValue = ProjectItemUtils.GetBuildAction(projectItem);
            prjBuildAction newValue = GetBuildAction(fileName, currentValue);
            if(currentValue != newValue)
            {
                ProjectItemUtils.SetBuildAction(projectItem, newValue);
            }
        }

        public void AccessFolder(EnvDTE.ProjectItem projectItem)
        {
            //  フォルダに対しては何もしない
            return;
        }

        #endregion

        /// <summary>
        /// ビルドアクション値の取得
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        protected virtual prjBuildAction GetBuildAction(string fileName, prjBuildAction defaultValue)
        {
            if (FilterNoAction.IsHitFilter(fileName))
            {
                return prjBuildAction.prjBuildActionNone;
            }

            if (FilterCompile.IsHitFilter(fileName))
            {
                return prjBuildAction.prjBuildActionCompile;
            }

            if (FilterContents.IsHitFilter(fileName))
            {
                return prjBuildAction.prjBuildActionContent;
            }

            if (FilterResource.IsHitFilter(fileName))
            {
                return prjBuildAction.prjBuildActionEmbeddedResource;
            }

            return defaultValue;
        }

        /// <summary>
        /// フィルター設定追加
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="filterList"></param>
        protected void AddFilters(ItemAttachmentFilter filter, IList<ConfigInfoFilter> filterList)
        {
            if (filterList != null)
            {
                filter.Clear();
                filter.AddFilters(filterList);
            }
        }
    }
}
