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
using EnvDTE;
using VSArrange.Filter;

namespace VSArrange.Report.Appender
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

        public DirectoryAppender(
            string dirPath, ItemAttachmentFilter filter,
            ProjectItems projectItems, IDictionary<string, ProjectItem> folderItems,
            OutputResultManager outputResultManager)
        {
            _dirPath = dirPath;
            _filter = filter;
            _projectItems = projectItems;
            _folderItems = folderItems;
            _outputResultManager = outputResultManager;
        }

        /// <summary>
        /// ディレクトリ追加実行
        /// </summary>
        public void Execute()
        {
            string[] subDirPaths = Directory.GetDirectories(_dirPath);
            foreach (string subDirPath in subDirPaths)
            {
                string[] dirPathParts = subDirPath.Split('\\');
                string dirName = dirPathParts[dirPathParts.Length - 1];
                if (_filter.IsPassFilter(dirName) &&
                    !_folderItems.ContainsKey(subDirPath))
                {
                    //  まだ追加していないもののみ追加
                    ProjectItem newItem = _projectItems.AddFromDirectory(subDirPath);
                    _folderItems.Add(subDirPath, newItem);

                    _outputResultManager.RegisterAddedDirectory(subDirPath);
                }
            }
        }
    }
}
