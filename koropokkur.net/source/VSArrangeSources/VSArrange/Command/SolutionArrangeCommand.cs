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
using AddInCommon.Command;
using AddInCommon.Util;
using EnvDTE;
using VSArrange.Config;
using VSArrange.Util;

namespace VSArrange.Command
{
    /// <summary>
    /// ソリューション整理処理クラス
    /// </summary>
    public class SolutionArrangeCommand : IDTCExecCommand
    {
        #region IDTCExecCommand メンバ

        public string CommandName
        {
            get { return "SolutionArrange"; }
        }

        public string DisplayName
        {
            get { return "ソリューション要素の整理"; }
        }

        public string ToolTipText
        {
            get { return "ソリューション内にある各プロジェクトの構成要素と実際のファイル構成の同期を取ります。"; }
        }

        public EnvDTE.vsCommandStatus GetCommandStatus(EnvDTE80.DTE2 applicationObject, EnvDTE.AddIn addInInstance, ref object commandText)
        {
            return VSCommandUtils.GetDefaultStatus();
        }

        public bool Execute(EnvDTE80.DTE2 applicationObject, EnvDTE.AddIn addInInstance, ref object varIn, ref object varOut)
        {
            var solution = applicationObject.Solution;

            try
            {
                var configInfo = ConfigFileManager.ReadConfig(PathUtils.GetConfigPath());
                var reporter = ArrangeUtils.CreateAddInReporter(configInfo, applicationObject);
                var arranger = ArrangeUtils.CreateArranger(configInfo, reporter, true);
                foreach (Project project in solution.Projects)
                {
                    //  プロジェクト追加フィルタの更新
                    arranger.ArrangeProject(project);
                }
                return true;
            }
            catch (System.Exception ex)
            {
                MessageUtils.ShowErrorMessage(ex.Message + Environment.NewLine + ex.StackTrace);
                return false;
            }
        }

        #endregion
    }
}
