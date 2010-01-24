#region Copyright
/*
 * Copyright 2005-2010 the Seasar Foundation and the Others.
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
using System.Windows.Forms;
using AddInCommon.Command;
using AddInCommon.Util;
using EnvDTE;
using VSArrange.Arrange;
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
            IDictionary<string, Project> refreshedProjects = new Dictionary<string, Project>();
            SelectedItems items = applicationObject.SelectedItems;

            try
            {
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
                    ProjectArranger arranger = ArrangeUtils.CreateArranger(applicationObject);
                    arranger.ArrangeProject(currentProject);

                    applicationObject.StatusBar.Text = string.Format(
                        "{0}の整理が終了しました。", currentProject.Name);
                    refreshedProjects[currentProject.FullName] = currentProject;
                }
                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
                return false;
            }
            finally
            {
                refreshedProjects.Clear();
                StatusBarUtils.Clear(applicationObject);
            }
        }

        #endregion
    }
}
