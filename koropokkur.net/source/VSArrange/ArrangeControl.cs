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
using System.Text.RegularExpressions;
using System.Windows.Forms;
using AddInCommon.Util;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.CommandBars;
using StatusBar = EnvDTE.StatusBar;

namespace VSArrange
{
    /// <summary>
    /// ソリューション、プロジェクト要素整理処理
    /// </summary>
    public class ArrangeControl
    {
        private const string REFRESH_BUTTON_NAME_SOLUTION = "全プロジェクト要素の整理";
        private const string REFRESH_BUTTON_NAME_PROJECT = "プロジェクト要素の整理";

        /// <summary>
        /// プロジェクト項目としないファイルを判別する正規表現
        /// </summary>
        private readonly Regex _regIsNotProjectFileItem = 
            new Regex(@"\.(csproj|sln|suo|user|exe|dll)$", RegexOptions.IgnoreCase);

        /// <summary>
        /// プロジェクト項目としないフォルダを判別する正規表現
        /// </summary>
        private readonly Regex _regIsNotProjectDirItem = 
            new Regex(@"(^\.svn$|^_ReSharper|^bin$|^obj$|^Properties$)", RegexOptions.IgnoreCase);

        private readonly DTE2 _applicationObject;

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
                CommandBarUtils.CreateButtonControl(commandBar);
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
                CommandBarUtils.CreateButtonControl(commandBar);
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

            try
            {
                foreach (Project project in solution.Projects)
                {
                    RefreshProject(project);
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

                    RefreshProject(currentProject);
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
        protected virtual void RefreshProject(Project project)
        {
            if (string.IsNullOrEmpty(project.FullName))
            {
                //  プロジェクト名が入っていない要素は無視
                return;    
            }

            string projectDirPath = Path.GetDirectoryName(project.FullName) + Path.DirectorySeparatorChar;
            ProjectItems projectItems = project.ProjectItems;

            string statusLabel = string.Format("プロジェクト[{0}]の要素を整理しています。", project.Name);
            RefreshDirectories(projectDirPath, projectItems, statusLabel);
        }

        /// <summary>
        /// ディレクトリ内のリフレッシュ
        /// </summary>
        /// <param name="dirPath"></param>
        /// <param name="projectItems"></param>
        /// <param name="statusLabel"></param>
        protected virtual void RefreshDirectories(string dirPath, ProjectItems projectItems, string statusLabel)
        {
            StatusBar bar = _applicationObject.StatusBar;

            IDictionary<string, ProjectItem> registeredItems = new Dictionary<string, ProjectItem>();
            //  削除されているファイル、フォルダをプロジェクトからアンロード
            for (int i = 1; i <= projectItems.Count; i++)
            {
                bar.Progress(true, statusLabel, i, projectItems.Count);
                
                ProjectItem projectItem = projectItems.Item(i);

                int beforeItemCount = projectItems.Count;
                string existPath = RefreshItem(dirPath, projectItem, statusLabel);
                int afterItemCount = projectItems.Count;
                //  プロジェクト要素を削除するとコレクションの数が
                //  変わるためその調整
                if (beforeItemCount > afterItemCount)
                {
                    i = i - (beforeItemCount - afterItemCount);
                }

                if (existPath != null)
                {
                    registeredItems.Add(existPath, projectItem);
                }
            }

            LoadNotRegisterFile(dirPath, registeredItems, projectItems);
            LoadNotRegisterDirectory(dirPath, registeredItems, projectItems);
        }

        /// <summary>
        /// 未登録のファイルをプロジェクトに追加する
        /// </summary>
        /// <param name="dirPath"></param>
        /// <param name="registeredItems"></param>
        /// <param name="projectItems"></param>
        protected virtual void LoadNotRegisterFile(string dirPath,
            IDictionary<string, ProjectItem> registeredItems,
            ProjectItems projectItems)
        {
            string[] filePaths = Directory.GetFiles(dirPath);
            foreach (string s in filePaths)
            {
                if (!registeredItems.ContainsKey(s) &&
                    !_regIsNotProjectFileItem.IsMatch(Path.GetExtension(s)))
                {
                    projectItems.AddFromFile(s);
                }
            }
        }

        /// <summary>
        /// 未登録のフォルダをプロジェクトに追加する
        /// </summary>
        /// <param name="dirPath"></param>
        /// <param name="registeredItems"></param>
        /// <param name="projectItems"></param>
        protected virtual void LoadNotRegisterDirectory(string dirPath,
            IDictionary<string, ProjectItem> registeredItems,
            ProjectItems projectItems)
        {
            string[] dirPaths = Directory.GetDirectories(dirPath);
            foreach (string s in dirPaths)
            {
                string[] dirNamePathParts = s.Split('\\');
                string dirName = dirNamePathParts[dirNamePathParts.Length - 1];
                if (!registeredItems.ContainsKey(s) &&
                    !_regIsNotProjectDirItem.IsMatch(dirName))
                {
                    ProjectItem addedProjectItem = projectItems.AddFromDirectory(s);
                    if (addedProjectItem != null)
                    {
                        //  冗長かもしれないが最後に追加対象外のファイル、フォルダがあれば削除
                        RemoveOutofTarget(addedProjectItem);
                    }
                }
            }
        }

        /// <summary>
        /// 対象外であるプロジェクト要素をプロジェクトから除外
        /// </summary>
        /// <param name="projectItem"></param>
        protected virtual void RemoveOutofTarget(ProjectItem projectItem)
        {
            foreach (ProjectItem item in projectItem.ProjectItems)
            {
                if(_regIsNotProjectFileItem.IsMatch(item.Name) ||
                    _regIsNotProjectDirItem.IsMatch(item.Name))
                {
                    item.Remove();
                    continue;
                }

                if(item.ProjectItems.Count > 0)
                {
                    RemoveOutofTarget(item);
                }
            }
        }

        /// <summary>
        /// 要素の更新（パスが存在しない要素をプロジェクトから削除する）
        /// </summary>
        /// <param name="dirPath"></param>
        /// <param name="item"></param>
        /// <param name="statusLabel"></param>
        protected virtual string RefreshItem(string dirPath, ProjectItem item, string statusLabel)
        {
            string targetPath = dirPath + item.Name;
            if (File.Exists(targetPath))
            {
                //  存在するファイルパスの場合は削除対象としない
                return targetPath;
            }

            //  ファイルでなければフォルダ？
            if (Directory.Exists(targetPath))
            {
                string childDirName = targetPath + Path.DirectorySeparatorChar;
                //  再帰的に子アイテムも処理
                RefreshDirectories(childDirName, item.ProjectItems, statusLabel);

                return targetPath;
            }
            //  ファイルとしてもフォルダとしても存在しないなら
            //  プロジェクトから外す
            item.Remove();
            return null;
        }

        #endregion
    }
}