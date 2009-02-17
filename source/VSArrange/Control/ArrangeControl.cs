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
using System.IO;
using System.Threading;
using System.Windows.Forms;
using AddInCommon.Util;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.CommandBars;
using VSArrange.Config;
using StatusBar = EnvDTE.StatusBar;
using VSArrange.Filter;
using System.Text;
using Thread=System.Threading.Thread;

namespace VSArrange.Control
{
    /// <summary>
    /// ソリューション、プロジェクト要素整理処理
    /// </summary>
    public class ArrangeControl
    {
        private const string REFRESH_BUTTON_NAME_SOLUTION = "全プロジェクト要素の整理";
        private const string REFRESH_BUTTON_NAME_PROJECT = "プロジェクト要素の整理";

        private readonly DTE2 _applicationObject;

        /// <summary>
        /// プロジェクト項目としないファイルを判別する正規表現
        /// </summary>
        private ItemAttachmentFilter _filterFile;

        /// <summary>
        /// ファイル登録除外フィルター
        /// </summary>
        public ItemAttachmentFilter FileterFile
        {
            set { _filterFile = value; }
            get { return _filterFile; }
        }

        /// <summary>
        /// プロジェクト項目としないフォルダを判別する正規表現
        /// </summary>
        private ItemAttachmentFilter _filterFolder;

        /// <summary>
        /// フォルダー登録除外フィルター
        /// </summary>
        public ItemAttachmentFilter FilterFolder
        {
            set { _filterFolder = value; }
            get { return _filterFolder; }
        }
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="applicationObject"></param>
        public ArrangeControl(DTE2 applicationObject)
        {
            _applicationObject = applicationObject;
        }

        /// <summary>
        /// ソリューション右クリックメニューに項目を一つ追加して返す
        /// </summary>
        /// <param name="commandBar"></param>
        /// <returns></returns>
        public virtual CommandBarControl CreateSolutionContextMenuItem(CommandBar commandBar)
        {
            CommandBarButton refreshSolutuinButton =
                CommandBarUtils.CreateCommandBarControl<CommandBarButton>(commandBar);
            refreshSolutuinButton.Caption = REFRESH_BUTTON_NAME_SOLUTION;
            refreshSolutuinButton.Click += refreshSolutuinButton_Click;
            return refreshSolutuinButton;
        }

        /// <summary>
        /// プロジェクト右クリックメニューに項目を一つ追加して返す
        /// </summary>
        /// <param name="commandBar"></param>
        /// <returns></returns>
        public virtual CommandBarControl CreateProjectContextMenuItem(CommandBar commandBar)
        {
            CommandBarButton refreshProjectButton =
                CommandBarUtils.CreateCommandBarControl<CommandBarButton>(commandBar);
            refreshProjectButton.Caption = REFRESH_BUTTON_NAME_PROJECT;
            refreshProjectButton.Click += refreshProjectButton_Click;
            return refreshProjectButton;
        }

        #region イベント

        /// <summary>
        /// ソリューション整理ボタンクリックイベント
        /// </summary>
        /// <param name="Ctrl"></param>
        /// <param name="CancelDefault"></param>
        private void refreshSolutuinButton_Click(CommandBarButton Ctrl, ref bool CancelDefault)
        {
            Solution solution = _applicationObject.Solution;

            //  プロジェクト追加フィルタの更新
            //  設定が変更された時点で予め非同期で読んでおく方がより良いが
            //  パフォーマンス的に整理処理直前に読んでも問題がないと思われるため
            //  実装を単純にする＋漏れをなくすためここで呼び出し
            RefreshConfigInfo();
            try
            {
                foreach (Project project in solution.Projects)
                {
                    ArrangeProject(project);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
            finally
            {
                StatusBarUtils.Clear(_applicationObject);
            }
        }

        /// <summary>
        /// プロジェクト整理ボタンクリックイベント
        /// </summary>
        /// <param name="Ctrl"></param>
        /// <param name="CancelDefault"></param>
        private void refreshProjectButton_Click(CommandBarButton Ctrl, ref bool CancelDefault)
        {
            IDictionary<string, Project> refreshedProjects = new Dictionary<string, Project>();
            SelectedItems items = _applicationObject.SelectedItems;

            //  プロジェクト追加フィルタの更新
            //  設定が変更された時点で予め非同期で読んでおく方がより良いが
            //  パフォーマンス的に整理処理直前に読んでも問題がないと思われるため
            //  実装を単純にする＋漏れをなくすためここで呼び出し
            RefreshConfigInfo();
            try
            {
                foreach (SelectedItem selectedItem in items)
                {
                    Project currentProject = selectedItem.Project;
                    if (refreshedProjects.ContainsKey(currentProject.FullName))
                    {
                        //  更新済のプロジェクトは無視
                        continue;
                    }

                    ArrangeProject(currentProject);
                    refreshedProjects[currentProject.FullName] = currentProject;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
            finally
            {
                refreshedProjects.Clear();
                StatusBarUtils.Clear(_applicationObject);
            }
        }

        #endregion

        #region リフレッシュ処理

        /// <summary>
        /// プロジェクトのリフレッシュ
        /// </summary>
        /// <param name="project"></param>
        protected virtual void ArrangeProject(Project project)
        {
            if (string.IsNullOrEmpty(project.FullName))
            {
                //  プロジェクト名が入っていない要素は無視
                return;    
            }

            string projectDirPath = Path.GetDirectoryName(project.FullName);
            ProjectItems projectItems = project.ProjectItems;

            string statusLabel = string.Format("プロジェクト[{0}]の要素を整理しています。", project.Name);
            ArrangeDirectories(projectDirPath, projectItems, statusLabel);

            _applicationObject.StatusBar.Text = string.Format("{0}の整理が終了しました。", project.Name);
        }

        protected virtual void ArrangeDirectories(string dirPath, ProjectItems projectItems, string statusLabel)
        {
            _applicationObject.StatusBar.Text = string.Format("{0}を整理しています。", dirPath);
            IDictionary<string, ProjectItem> fileItems = new Dictionary<string, ProjectItem>();
            IDictionary<string, ProjectItem> folderItems = new Dictionary<string, ProjectItem>();
            IList<ProjectItem> deleteTarget = new List<ProjectItem>();

            string basePath = dirPath + Path.DirectorySeparatorChar;
            //  ファイル、フォルダ、削除対象に振り分ける
            foreach (ProjectItem projectItem in projectItems)
            {
                string currentPath = basePath + projectItem.Name;
                if(Directory.Exists(currentPath))
                {
                    if(_filterFolder.IsPassFilter(projectItem.Name))
                    {
                        //  実際に存在していて且つプロジェクトにも登録済
                        if(!folderItems.ContainsKey(currentPath))
                        {
                            folderItems.Add(currentPath, projectItem);
                        }
                    }
                    else
                    {
                        deleteTarget.Add(projectItem);
                    }
                }
                else if(File.Exists(currentPath))
                {
                    if(_filterFile.IsPassFilter(projectItem.Name))
                    {
                        if(!fileItems.ContainsKey(currentPath))
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

            ////  ディレクトリ追加
            //string[] subDirPaths = Directory.GetDirectories(dirPath);
            //foreach (string subDirPath in subDirPaths)
            //{
            //    string[] dirPathParts = subDirPath.Split('\\');
            //    string dirName = dirPathParts[dirPathParts.Length - 1];
            //    if (_filterFolder.IsPassFilter(dirName) &&
            //        !folderItems.ContainsKey(subDirPath))
            //    {
            //        //  まだ追加していないもののみ追加
            //        ProjectItem newItem = projectItems.AddFromDirectory(subDirPath);
            //        folderItems.Add(subDirPath, newItem);
            //    }
            //}
            DirectoryAppender directoryAppender = new DirectoryAppender(
                dirPath, _filterFolder, projectItems, folderItems);
            directoryAppender.Execute();
            //System.Threading.Thread dirThread = new Thread(
            //    new ThreadStart(directoryAppender.Execute));
            //dirThread.Start();

            ////  ファイル追加
            //string[] subFilePaths = Directory.GetFiles(dirPath);
            //foreach (string subFilePath in subFilePaths)
            //{
            //    string[] filePathParts = subFilePath.Split('\\');
            //    string fileName = filePathParts[filePathParts.Length - 1];
            //    if (_filterFile.IsPassFilter(fileName) &&
            //        !fileItems.ContainsKey(subFilePath))
            //    {
            //        //  まだ追加していないもののみ追加
            //        projectItems.AddFromFile(subFilePath);
            //    }
            //}
            FileAppender fileAppender = new FileAppender(
                dirPath, _filterFile, projectItems, fileItems);
            //fileAppender.Execute();
            System.Threading.Thread fileThread = new Thread(
                new ThreadStart(fileAppender.Execute));
            fileThread.Start();

            ////  不要な要素は削除
            //foreach (ProjectItem projectItem in deleteTarget)
            //{
            //    projectItem.Remove();
            //}
            ProjectItemRemover projectItemRemover = new ProjectItemRemover(deleteTarget);
            //projectItemRemover.Execute();
            System.Threading.Thread removeThread = new Thread(
                new ThreadStart(projectItemRemover.Execute));
            removeThread.Start();

            //dirThread.Join();
            //fileThread.Join();
            //removeThread.Join();

            //  残ったフォルダに対して同様の処理を再帰的に実行
            foreach (string projectDirPath in folderItems.Keys)
            {
                ProjectItem dirItem = folderItems[projectDirPath];
                //ArrangeDirectories(projectDirPath, dirItem.ProjectItems, statusLabel);

                Thread thread = new Thread(ExecuteArrange);
                thread.Start(new object[] { projectDirPath, dirItem.ProjectItems, statusLabel });
            }

        }

        private void ExecuteArrange(object parameter)
        {
            object[] parameters = parameter as object[];
            if(parameters == null)
            {
                return;
            }
            ArrangeDirectories(parameters[0] as string,
                parameters[1] as ProjectItems,
                parameters[2] as string);

            _applicationObject.StatusBar.Text = "";
        }

        private class ProjectItemRemover
        {
            private readonly IList<ProjectItem> _deleteTarget;

            public ProjectItemRemover(IList<ProjectItem> deleteTarget)
            {
                _deleteTarget = deleteTarget;
            }

            public void Execute()
            {
                foreach (ProjectItem projectItem in _deleteTarget)
                {
                    projectItem.Remove();
                }
            }
        }

        private class FileAppender
        {
            private readonly string _dirPath;
            private readonly ItemAttachmentFilter _filter;
            private readonly ProjectItems _projectItems;
            private readonly IDictionary<string, ProjectItem> _fileItems;

            public FileAppender(
                string dirPath, ItemAttachmentFilter filter, 
                ProjectItems projectItems, IDictionary<string, ProjectItem> fileItems)
            {
                _dirPath = dirPath;
                _filter = filter;
                _projectItems = projectItems;
                _fileItems = fileItems;
            }

            /// <summary>
            /// ファイル追加実行
            /// </summary>
            public void Execute()
            {
                string[] subFilePaths = Directory.GetFiles(_dirPath);
                foreach (string subFilePath in subFilePaths)
                {
                    string[] filePathParts = subFilePath.Split('\\');
                    string fileName = filePathParts[filePathParts.Length - 1];
                    if (_filter.IsPassFilter(fileName) &&
                        !_fileItems.ContainsKey(subFilePath))
                    {
                        //  まだ追加していないもののみ追加
                        _projectItems.AddFromFile(subFilePath);
                    }
                }
            }
        }
        
        /// <summary>
        /// ディレクトリ追加クラス
        /// </summary>
        private class DirectoryAppender
        {
            private readonly string _dirPath;
            private readonly ItemAttachmentFilter _filter;
            private readonly ProjectItems _projectItems;
            private readonly IDictionary<string, ProjectItem> _folderItems;

            public DirectoryAppender(
                string dirPath, ItemAttachmentFilter filter, 
                ProjectItems projectItems, IDictionary<string, ProjectItem> folderItems)
            {
                _dirPath = dirPath;
                _filter = filter;
                _projectItems = projectItems;
                _folderItems = folderItems;
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
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// 設定情報再読み込み
        /// </summary>
        private void RefreshConfigInfo()
        {
            //  設定読み込み
            ConfigInfo configInfo = ConfigFileManager.ReadConfig(PathUtils.GetConfigPath());
            if (configInfo != null)
            {
                //  プロジェクト要素追加除外フィルターを設定
                if (configInfo.FilterFileStringList != null)
                {
                    _filterFile.Clear();
                    _filterFile.AddFilters(configInfo.FilterFileStringList);
                }

                if (configInfo.FilterFolderStringList != null)
                {
                    _filterFolder.Clear();
                    _filterFolder.AddFilters(configInfo.FilterFolderStringList);
                }
            }
        }
    }
}