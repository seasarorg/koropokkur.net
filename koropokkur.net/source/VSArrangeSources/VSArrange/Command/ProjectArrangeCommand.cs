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

using System;
using System.Collections.Generic;
using AddInCommon.Command;
using AddInCommon.Util;
using EnvDTE;
using VSArrange.Config;
using VSArrange.Util;

namespace VSArrange.Command
{
    /// <summary>
    /// プロジェクト整理処理クラス
    /// </summary>
    public class ProjectArrangeCommand : IDTCExecCommand
    {
        #region IDTCExecCommand メンバ

        public string CommandName
        {
            get { return "ProjectArrange"; }
        }

        public string DisplayName
        {
            get { return "プロジェクト要素の整理"; }
        }

        public string ToolTipText
        {
            get { return "現在選択しているプロジェクト要素と実際のファイル構造との同期を取ります。"; }
        }

        public EnvDTE.vsCommandStatus GetCommandStatus(EnvDTE80.DTE2 applicationObject, EnvDTE.AddIn addInInstance, ref object commandText)
        {
            return VSCommandUtils.GetDefaultStatus();
        }

        public bool Execute(EnvDTE80.DTE2 applicationObject, EnvDTE.AddIn addInInstance, ref object varIn, ref object varOut)
        {
            var refreshedProjects = new Dictionary<string, Project>();
            var items = applicationObject.SelectedItems;

            try
            {
                //  設定読み込み
                //  設定が変更された時点で予め非同期で読んでおく方がより良いが
                //  パフォーマンス的に整理処理直前に読んでも問題がないと思われるため
                //  実装を単純にする＋漏れをなくすためここで呼び出し
                var configInfo = ConfigFileManager.ReadConfig(PathUtils.GetConfigPath());

                //  選択されている要素は実質一つだけのはずだが
                //  コレクションの形でしか取得できないためforeachでまわす
                foreach (SelectedItem selectedItem in items)
                {
                    var currentProject = selectedItem.Project;

                    if (refreshedProjects.ContainsKey(currentProject.FullName))
                    {
                        //  更新済のプロジェクトは無視
                        continue;
                    }
                    var reporter = ArrangeUtils.CreateAddInReporter(currentProject, configInfo, applicationObject);
                    var arranger = ArrangeUtils.CreateArrangerAsync(configInfo, reporter);
                    arranger.ArrangeProject(currentProject);
                    refreshedProjects[currentProject.FullName] = currentProject;
                }
                return true;
            }
            catch (System.Exception ex)
            {
                MessageUtils.ShowErrorMessage(ex.Message + Environment.NewLine + ex.StackTrace);
                return false;
            }
            finally
            {
                refreshedProjects.Clear();
            }
        }

        #endregion
    }
}
