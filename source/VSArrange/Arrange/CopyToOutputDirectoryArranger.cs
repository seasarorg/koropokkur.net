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
using AddInCommon.Invoke;
using AddInCommon.Util;
using VSArrange.Config;
using VSArrange.Filter;

namespace VSArrange.Arrange
{
    /// <summary>
    /// 「出力先にコピー」設定クラス
    /// </summary>
    public class CopyToOutputDirectoryArranger : IProjectItemAccessor
    {
        private readonly OutputResultManager _outputResultManager;

        #region プロパティ

        /// <summary>
        /// プロジェクト要素に設定する属性付加の判定に使う正規表現
        /// </summary>
        private readonly ItemAttachmentFilter _filterNoCopy = new ItemAttachmentFilter();

        /// <summary>
        /// ビルド後「コピーしない」設定フィルター
        /// </summary>
        public ItemAttachmentFilter FilterNoCopy
        {
            get { return _filterNoCopy; }
        }

        /// <summary>
        /// プロジェクト要素に設定する属性付加の判定に使う正規表現
        /// </summary>
        private readonly ItemAttachmentFilter _filterEverytimeCopy = new ItemAttachmentFilter();

        /// <summary>
        /// ビルド後「常にコピー」設定フィルター
        /// </summary>
        public ItemAttachmentFilter FilterEverytimeCopy
        {
            get { return _filterEverytimeCopy; }
        }

        /// <summary>
        /// プロジェクト要素に設定する属性付加の判定に使う正規表現
        /// </summary>
        private readonly ItemAttachmentFilter _filterCopyIfNew = new ItemAttachmentFilter();

        /// <summary>
        /// ビルド後「新しい場合はコピー」設定フィルター
        /// </summary>
        public ItemAttachmentFilter FilterCopyIfNew
        {
            get { return _filterCopyIfNew; }
        }

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="configInfo"></param>
        /// <param name="outputResultManager"></param>
        public CopyToOutputDirectoryArranger(ConfigInfo configInfo, OutputResultManager outputResultManager)
        {
            AddFilters(FilterNoCopy, configInfo.FilterNoCopyStringList);
            AddFilters(FilterEverytimeCopy, configInfo.FilterEverytimeCopyStringList);
            AddFilters(FilterCopyIfNew, configInfo.FilterCopyIfNewStringList);

            _outputResultManager = outputResultManager;
        }

        #region IProjectItemAccessor メンバ

        public void AccessFile(EnvDTE.ProjectItem projectItem)
        {
            string fileName = ProjectItemUtils.GetFileName(projectItem);
            EnumCopyToOutputDirectory currentValue = ProjectItemUtils.GetCopyToOutputDirectory(projectItem);
            EnumCopyToOutputDirectory newValue = GetCopyToOutputDeirectory(fileName, currentValue);
            if (currentValue != newValue)
            {
                ProjectItemUtils.SetCopyToOutputDirectory(projectItem, newValue);

                _outputResultManager.RegisterdCopyToOutputDirectory(
                    ProjectItemUtils.GetFullPath(projectItem), newValue);
            }
        }

        public void AccessFolder(EnvDTE.ProjectItem projectItem)
        {
            //  フォルダに対しては何もしない
            return;
        }

        #endregion

        /// <summary>
        /// 「出力ディレクトリへコピー」設定値の取得
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        protected virtual EnumCopyToOutputDirectory GetCopyToOutputDeirectory(
            string fileName, EnumCopyToOutputDirectory defaultValue)
        {
            if (FilterNoCopy.IsHitFilter(fileName))
            {
                return EnumCopyToOutputDirectory.NotCopy;
            }

            if (FilterEverytimeCopy.IsHitFilter(fileName))
            {
                return EnumCopyToOutputDirectory.EveryTime;
            }

            if (FilterCopyIfNew.IsHitFilter(fileName))
            {
                return EnumCopyToOutputDirectory.IfModified;
            }

            return defaultValue;
        }

        /// <summary>
        /// フィルター設定追加
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="filterList"></param>
        protected void AddFilters(ItemAttachmentFilter filter, IList<ConfigInfoDetail> filterList)
        {
            if (filterList != null)
            {
                filter.Clear();
                filter.AddFilters(filterList);
            }
        }
    }
}
