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
using System.ComponentModel;
using System.IO;
using System.Text;
using AddInCommon.Background;
using AddInCommon.Invoke;
using AddInCommon.Util;
using EnvDTE;
using VSArrange.Arrange.Appender;
using VSArrange.Config;
using VSArrange.Filter;

namespace VSArrange.Arrange
{
    /// <summary>
    /// プロジェクト要素整理クラス
    /// </summary>
    public class ProjectArranger : IDisposable
    {
        /// <summary>
        /// バックグラウンド処理
        /// </summary>
        private readonly AddInBackgroundWorker _worker;

        /// <summary>
        /// プロジェクト要素整理処理中のプロジェクト名
        /// </summary>
        private string _projectName;

        /// <summary>
        /// 処理中に発生した例外
        /// </summary>
        private Exception _exception;

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
        /// <param name="worker"></param>
        public ProjectArranger(ConfigInfo configInfo, AddInBackgroundWorker worker)
        {
            Initialize(configInfo);
            _worker = worker;
            SetWorker(worker);
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
            
            //  C#.NET,VB.NET以外はプロジェクトの構造が違うため
            //  現バージョンでは処理の対象外
            if(!project.FullName.EndsWith(".csproj") &&
                !project.FullName.EndsWith(".vbproj"))
            {
                MessageUtils.ShowWarnMessage(
                    "「プロジェクト整理」はC#,VB.NETプロジェクトにのみ対応しています。\n当プロジェクト({0})では使用することはできません。", 
                    project.FullName);
                return;
            }

            ////  他のプロジェクトでプロジェクト要素整理を実行中の場合は
            ////  終わるまで待つ
            //while(_worker.IsBusy)
            //{
            //    System.Threading.Thread.Sleep(1000);
            //    Application.DoEvents();
            //}
            //  バックグラウンドでプロジェクト要素整理の開始
            _worker.RunWorkerAsync(project);
        }

        /// <summary>
        /// ディレクトリの整理
        /// </summary>
        /// <param name="dirPath"></param>
        /// <param name="projectItems"></param>
        protected virtual void ArrangeDirectories(string dirPath, ProjectItems projectItems)
        {
            //  進捗単位数
            const int PROGRESS_PARTS_COUNT = 4;

            IDictionary<string, ProjectItem> fileItems = new Dictionary<string, ProjectItem>();
            IDictionary<string, ProjectItem> folderItems = new Dictionary<string, ProjectItem>();
            IList<ProjectItem> deleteTarget = new List<ProjectItem>();

            string basePath = dirPath + Path.DirectorySeparatorChar;
            
            _worker.ReportProgress("追加・除外する要素抽出中", 0, PROGRESS_PARTS_COUNT);
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

            _worker.ReportProgress("プロジェクト要素を追加中（フォルダ）", 1, PROGRESS_PARTS_COUNT);
            //  ディレクトリ追加
            //  ラストで行っている再起処理にも関係するので
            //  ディレクトリ追加は新規スレッドでの実行は行わない
            DirectoryAppender directoryAppender = new DirectoryAppender(
                dirPath, _filterFolder, projectItems, folderItems);
            directoryAppender.Execute();

            _worker.ReportProgress("プロジェクト要素を追加中（ファイル）", 2, PROGRESS_PARTS_COUNT);
            //  ファイル追加
            FileAppender fileAppender = new FileAppender(
                dirPath, _filterFile, projectItems, fileItems);
            fileAppender.Execute();

            _worker.ReportProgress("不要なプロジェクト要素を削除中", 3, PROGRESS_PARTS_COUNT);
            //  不要な要素は削除
            ProjectItemRemover projectItemRemover = new ProjectItemRemover(deleteTarget);
            projectItemRemover.Execute();

            _worker.ReportProgress("フォルダ内プロジェクト要素の整理中", 0, PROGRESS_PARTS_COUNT);
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

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="configInfo"></param>
        private void Initialize(ConfigInfo configInfo)
        {
            if (configInfo == null) throw new ArgumentNullException("configInfo");

            AddFilters(FilterFile, configInfo.FilterFileStringList);
            AddFilters(FilterFolder, configInfo.FilterFolderStringList);

            //  プロジェクト要素属性設定フィルターを設定
            _buildActionArranger = new BuildActionArranger(configInfo);
            _copyToOutputDirectoryArranger = new CopyToOutputDirectoryArranger(configInfo);
        }

        #region Background

        /// <summary>
        /// バックグラウンド処理設定
        /// </summary>
        /// <param name="worker"></param>
        private void SetWorker(BackgroundWorker worker)
        {
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
        }

        /// <summary>
        /// バックグラウンド処理開始イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            _exception = null;
            AddInBackgroundWorker worker = (AddInBackgroundWorker)sender;
            try
            {
                Project project = (Project) e.Argument;
                _projectName = Path.GetFileNameWithoutExtension(project.FileName);

                string projectDirPath = Path.GetDirectoryName(project.FullName);
                ProjectItems projectItems = project.ProjectItems;

                worker.ReportProgress("プロジェクト要素の整理中", 0, 2);
                ArrangeDirectories(projectDirPath, projectItems);

                worker.ReportProgress("プロジェクト要素の属性を設定中", 1, 2);
                //  整理し終わったプロジェクト要素に対して属性設定
                ProjectItemUtils.AccessAllProjectItems(
                    projectItems, new IProjectItemAccessor[]
                                      {
                                          _buildActionArranger, _copyToOutputDirectoryArranger
                                      });
                worker.ReportProgress("プロジェクト要素の属性を設定中", 2, 2);
            }
            catch(Exception ex)
            {
                _exception = ex;
            }
            finally
            {
                worker.ResetDisplay();
            }
        }

        /// <summary>
        /// バックグラウンド処理終了イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_exception == null)
            {
                MessageUtils.ShowInfoMessage("[{0}]プロジェクト要素の整理が終了しました。",
                    _projectName);
            }
            else
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine(string.Format(
                    "[{0}]プロジェクト要素の整理に失敗しました。", _projectName));
                builder.AppendLine(_exception.Message);
                builder.AppendLine(_exception.StackTrace);
                MessageUtils.ShowErrorMessage(builder.ToString());
            }
        }

        #endregion

        #region IDisposable メンバ

        public void Dispose()
        {
            if(_worker != null)
            {
                _worker.Dispose();
            }
        }

        #endregion
    }
}
