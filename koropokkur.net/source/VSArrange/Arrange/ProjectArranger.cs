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
using System.IO;
using System.Threading;
using AddInCommon.Invoke;
using AddInCommon.Util;
using EnvDTE;
using VSArrange.Arrange.Appender;
using VSArrange.Config;
using VSArrange.Filter;
using Thread = System.Threading.Thread;

namespace VSArrange.Arrange
{
    /// <summary>
    /// プロジェクト要素整理クラス
    /// </summary>
    public class ProjectArranger
    {
        /// <summary>
        /// 「ビルドアクション」設定
        /// </summary>
        protected BuildActionArranger _buildActionArranger;

        /// <summary>
        /// 「出力後にコピー」設定
        /// </summary>
        protected CopyToOutputDirectoryArranger _copyToOutputDirectoryArranger;

        #region プロパティ
        /// <summary>
        /// プロジェクト項目としないファイルを判別する正規表現
        /// </summary>
        private readonly ItemAttachmentFilter _filterFile = new ItemAttachmentFilter();

        /// <summary>
        /// ファイル登録除外フィルター
        /// </summary>
        public ItemAttachmentFilter FilterFile
        {
            get { return _filterFile; }
        }

        /// <summary>
        /// プロジェクト項目としないフォルダを判別する正規表現
        /// </summary>
        private readonly ItemAttachmentFilter _filterFolder = new ItemAttachmentFilter();

        /// <summary>
        /// フォルダー登録除外フィルター
        /// </summary>
        public ItemAttachmentFilter FilterFolder
        {
            get { return _filterFolder; }
        }
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="configInfo"></param>
        public ProjectArranger(ConfigInfo configInfo)
        {
            if (configInfo == null) throw new ArgumentNullException("configInfo");

            AddFilters(FilterFile, configInfo.FilterFileStringList);
            AddFilters(FilterFolder, configInfo.FilterFolderStringList);

            //  プロジェクト要素属性設定フィルターを設定
            _buildActionArranger = new BuildActionArranger(configInfo);
            _copyToOutputDirectoryArranger = new CopyToOutputDirectoryArranger(configInfo);
        }

        #region リフレッシュ処理

        /// <summary>
        /// プロジェクトのリフレッシュ
        /// </summary>
        /// <param name="project"></param>
        public virtual void ArrangeProject(Project project)
        {
            if (string.IsNullOrEmpty(project.FullName))
            {
                //  プロジェクト名が入っていない要素は無視
                return;
            }

            string projectDirPath = Path.GetDirectoryName(project.FullName);
            ProjectItems projectItems = project.ProjectItems;

            ArrangeDirectories(projectDirPath, projectItems);

            //  整理し終わったプロジェクト要素に対して属性設定
            ProjectItemUtils.AccessAllProjectItems(
                projectItems, new IProjectItemAccessor[]
                                  {
                                      _buildActionArranger, _copyToOutputDirectoryArranger
                                  });
        }

        /// <summary>
        /// ディレクトリの整理
        /// </summary>
        /// <param name="dirPath"></param>
        /// <param name="projectItems"></param>
        protected virtual void ArrangeDirectories(string dirPath, ProjectItems projectItems)
        {
            IDictionary<string, ProjectItem> fileItems = new Dictionary<string, ProjectItem>();
            IDictionary<string, ProjectItem> folderItems = new Dictionary<string, ProjectItem>();
            IList<ProjectItem> deleteTarget = new List<ProjectItem>();

            string basePath = dirPath + Path.DirectorySeparatorChar;
            //  ファイル、フォルダ、削除対象に振り分ける
            foreach (ProjectItem projectItem in projectItems)
            {
                string currentPath = basePath + projectItem.Name;
                if (Directory.Exists(currentPath))
                {
                    if (_filterFolder.IsPassFilter(projectItem.Name))
                    {
                        //  実際に存在していて且つプロジェクトにも登録済
                        if (!folderItems.ContainsKey(currentPath))
                        {
                            folderItems.Add(currentPath, projectItem);
                        }
                    }
                    else
                    {
                        deleteTarget.Add(projectItem);
                    }
                }
                else if (File.Exists(currentPath))
                {
                    if (_filterFile.IsPassFilter(projectItem.Name))
                    {
                        if (!fileItems.ContainsKey(currentPath))
                        {
                            fileItems.Add(currentPath, projectItem);
                        }
                    }
                    else
                    {
                        deleteTarget.Add(projectItem);
                    }
                }
                else
                {
                    deleteTarget.Add(projectItem);
                }
            }

            //  ディレクトリ追加
            //  ラストで行っている再起処理にも関係するので
            //  ディレクトリ追加は新規スレッドでの実行は行わない
            DirectoryAppender directoryAppender = new DirectoryAppender(
                dirPath, _filterFolder, projectItems, folderItems);
            directoryAppender.Execute();

            //  ファイル追加
            FileAppender fileAppender = new FileAppender(
                dirPath, _filterFile, projectItems, fileItems);
            System.Threading.Thread fileThread = new Thread(
                new ThreadStart(fileAppender.Execute));
            fileThread.Start();

            //  不要な要素は削除
            ProjectItemRemover projectItemRemover = new ProjectItemRemover(deleteTarget);
            System.Threading.Thread removeThread = new Thread(
                new ThreadStart(projectItemRemover.Execute));
            removeThread.Start();

            //  残ったフォルダに対して同様の処理を再帰的に実行
            foreach (string projectDirPath in folderItems.Keys)
            {
                ArrangeDirectories(projectDirPath, folderItems[projectDirPath].ProjectItems);
            }
        }

        #endregion

        /// <summary>
        /// フィルター設定追加
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="filterList"></param>
        private void AddFilters(ItemAttachmentFilter filter, IList<ConfigInfoFilter> filterList)
        {
            if (filterList != null)
            {
                filter.Clear();
                filter.AddFilters(filterList);
            }
        }
    }
}
