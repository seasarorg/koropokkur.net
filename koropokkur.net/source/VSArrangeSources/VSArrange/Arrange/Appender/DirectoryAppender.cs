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
using System.IO;
using AddInCommon.Report;
using AddInCommon.Wrapper;
using EnvDTE;
using VSArrange.Arrange;
using VSArrange.Filter;
using VSArrange.Message;

namespace VSArrange.Arrange.Appender
{
    /// <summary>
    /// ディレクトリ追加クラス
    /// </summary>
    public class DirectoryAppender
    {
        private readonly string _dirPath;
        private readonly ItemAttachmentFilter _filter;
        private readonly ProjectItems _projectItems;
        private readonly IDictionary<string, ProjectItem> _folderItems;
        private readonly OutputResultManager _outputResultManager;
        private readonly IOutputReport _reporter;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dirPath"></param>
        /// <param name="filter"></param>
        /// <param name="projectItems"></param>
        /// <param name="folderItems">追加済みフォルダ</param>
        /// <param name="outputResultManager"></param>
        /// <param name="reporter"></param>
        public DirectoryAppender(
            string dirPath, ItemAttachmentFilter filter,
            ProjectItems projectItems, IDictionary<string, ProjectItem> folderItems,
            OutputResultManager outputResultManager,
            IOutputReport reporter)
        {
            _dirPath = dirPath;
            _filter = filter;
            _projectItems = projectItems;
            _folderItems = folderItems;
            _outputResultManager = outputResultManager;
            _reporter = reporter;
        }

        /// <summary>
        /// ディレクトリ追加実行
        /// </summary>
        public void Execute()
        {
            var subDirPaths = Directory.GetDirectories(_dirPath);
            var totalCount = subDirPaths.Length;
            int currentCount = 1;

            foreach (string subDirPath in subDirPaths)
            {
                _reporter.ReportSubProgress(VSArrangeMessage.GetAddFolderNow(), currentCount, totalCount);

                string[] dirPathParts = subDirPath.Split(Path.DirectorySeparatorChar);
                string dirName = dirPathParts[dirPathParts.Length - 1];
                if (_filter.IsPassFilter(dirName) &&
                    !_folderItems.ContainsKey(subDirPath))
                {
                    //  まだ追加していないもののみ追加
                    var newItemOrg = _projectItems.AddFromDirectory(subDirPath);
                    var newItem = new ProjectItemEx();
                    newItem.SetProjectItem(newItemOrg);
                    _folderItems.Add(subDirPath, newItem);

                    _outputResultManager.RegisterAddedDirectory(subDirPath);
                }
                currentCount++;
            }
        }
    }
}
