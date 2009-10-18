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
using System.Windows.Forms;
using AddInCommon.Util;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.CommandBars;
using VSArrange.Arrange;
using VSArrange.Config;

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
            ProjectArranger arranger = CreateArranger();
            try
            {
                foreach (Project project in solution.Projects)
                {
                    arranger.ArrangeProject(project);
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
                ProjectArranger arranger = CreateArranger();
                //  選択されている要素は実質一つだけのはずだが
                //  コレクションの形でしか取得できないためforeachでまわす
                foreach (SelectedItem selectedItem in items)
                {
                    Project currentProject = selectedItem.Project;

                    if (refreshedProjects.ContainsKey(currentProject.FullName))
                    {
                        //  更新済のプロジェクトは無視
                        continue;
                    }
                    arranger.ArrangeProject(currentProject);

                    _applicationObject.StatusBar.Text = string.Format(
                        "{0}の整理が終了しました。", currentProject.Name);
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

        /// <summary>
        /// 設定情報再読み込み
        /// </summary>
        /// <remarks>
        //  設定が変更された時点で予め非同期で読んでおく方がより良いが
        //  パフォーマンス的に整理処理直前に読んでも問題がないと思われるため
        //  実装を単純にする＋漏れをなくすためここで呼び出し
        /// </remarks>
        private ProjectArranger CreateArranger()
        {
            //  設定読み込み
            ConfigInfo configInfo = ConfigFileManager.ReadConfig(PathUtils.GetConfigPath());
            return new ProjectArranger(configInfo);
        }
    }
}