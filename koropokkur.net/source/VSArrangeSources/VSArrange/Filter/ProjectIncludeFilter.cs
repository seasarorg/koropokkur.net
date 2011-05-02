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

using System.IO;
using VSArrange.Config;

namespace VSArrange.Filter
{
    /// <summary>
    /// プロジェクト追加要否判定クラス
    /// </summary>
    public class ProjectIncludeFilter
    {
        /// <summary>
        /// ファイル用プロジェクト追加要否判定フィルタ
        /// </summary>
        private readonly ItemAttachmentFilter _filterFile = new ItemAttachmentFilter();

        /// <summary>
        /// ファイル用プロジェクト追加要否判定フィルタ
        /// </summary>
        public ItemAttachmentFilter FilterFile { get { return _filterFile; } }

        /// <summary>
        /// フォルダ用プロジェクト追加要否判定フィルタ
        /// </summary>
        private readonly ItemAttachmentFilter _filterFolder = new ItemAttachmentFilter();

        /// <summary>
        /// フォルダ用プロジェクト追加要否判定フィルタ
        /// </summary>
        public ItemAttachmentFilter FilterFolder { get { return _filterFolder; } }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="configInfo"></param>
        public ProjectIncludeFilter(ConfigInfo configInfo)
        {
            _filterFile.ReplaceFilters(configInfo.FilterFileStringList);
            _filterFolder.ReplaceFilters(configInfo.FilterFolderStringList);
        }

        /// <summary>
        /// プロジェクト除外対象か判定
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public bool IsRemove(string keyWord)
        {
            if (Directory.Exists(keyWord))
            {
                return _filterFolder.IsHitFilter(keyWord);
            }

            if (File.Exists(keyWord))
            {
                return _filterFile.IsHitFilter(keyWord);
            }
            // 存在しないパスの場合は必ずプロジェクト除外対象
            return true;
        }
    }
}
