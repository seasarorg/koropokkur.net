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
using AddInCommon.Command;
using AddInCommon.Const;
using AddInCommon.Helper;
using AddInCommon.Util;
using Microsoft.VisualStudio.CommandBars;
using VSArrange.Command;

namespace VSArrange.Helper
{
    /// <summary>
    /// VSArrange用Connect補助クラス
    /// </summary>
    public class VSArrangeConnectHelper : ConnectHelperBase
    {
        protected override void RegisterEventCommandAfterStartUp(IDictionary<string, IDTCExecCommand> commands)
        {
            VSCommandCreator creator = new VSCommandCreator(AddInInstance, ApplicationObject);

            //  ソリューションエクスプローラー右クリックメニュー
            CreateArrangeCommand(new ProjectArrangeCommand(), commands, creator,
                                 creator.GetCommandBar(CommandBarConst.CONTEXT_PROJECT));
            CreateArrangeCommand(new SolutionArrangeCommand(), commands, creator,
                                 creator.GetCommandBar(CommandBarConst.CONTEXT_SOLUTION));

            //  ツールメニュー
            CreateArrangeCommand(new VSArrangeMenuCommand(), commands, creator, 
                                 creator.AddKoropokkurMenuCommandBar());
        }

        /// <summary>
        /// 整理コマンドの生成、登録
        /// </summary>
        /// <param name="eventCommand"></param>
        /// <param name="commands"></param>
        /// <param name="creator"></param>
        /// <param name="control"></param>
        private void CreateArrangeCommand(
            IDTCExecCommand eventCommand,
            IDictionary<string, IDTCExecCommand> commands, 
            VSCommandCreator creator,
            CommandBar control)
        {
            string vsCommandName = VSCommandUtils.GetVSCommandName(
                AddInInstance.ProgID, eventCommand.CommandName);
            commands[vsCommandName] = eventCommand;
            EnvDTE.Command newCommand = creator.CreateButton(
                eventCommand.CommandName,
                eventCommand.DisplayName,
                eventCommand.ToolTipText);
            VSCommandUtils.RegisterControl(newCommand, control);
        }
    }
}
