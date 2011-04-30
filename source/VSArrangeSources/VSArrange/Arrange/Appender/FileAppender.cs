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
    /// ファイル追加クラス
    /// </summary>
    public class FileAppender
    {
        private readonly string _dirPath;
        private readonly ItemAttachmentFilter _filter;
        private ProjectItems _projectItems;
        private readonly IDictionary<string, ProjectItem> _fileItems;
        private readonly OutputResultManager _outputResultManager;
        private readonly IOutputReport _reporter;

        public FileAppender(
            string dirPath, ItemAttachmentFilter filter,
            ProjectItems projectItems, IDictionary<string, ProjectItem> fileItems,
            OutputResultManager outputResultManager,
            IOutputReport reporter)
        {
            _dirPath = dirPath;
            _filter = filter;
            _projectItems = projectItems;
            _fileItems = fileItems;
            _outputResultManager = outputResultManager;
            _reporter = reporter;
        }

        /// <summary>
        /// ファイル追加実行
        /// </summary>
        public void Execute()
        {
            var subFilePaths = Directory.GetFiles(_dirPath);
            var totalCount = subFilePaths.Length;
            int currentCount = 1;

            foreach (var subFilePath in subFilePaths)
            {
                _reporter.ReportSubProgress(VSArrangeMessage.GetAddFileNow(), currentCount, totalCount);

                string[] filePathParts = subFilePath.Split('\\');
                string fileName = filePathParts[filePathParts.Length - 1];
                if (_filter.IsPassFilter(fileName) &&
                    !_fileItems.ContainsKey(subFilePath))
                {
                    //  まだ追加していないもののみ追加
                    var newItemOrg = _projectItems.AddFromFile(subFilePath);
                    var newItem = new ProjectItemEx();
                    newItem.SetProjectItem(newItemOrg);
                    _fileItems.Add(subFilePath, newItem);

                    _outputResultManager.RegisterAddedFile(subFilePath);
                }
                currentCount++;
            }
        }
    }
}
