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
using AddInCommon.Core;
using AddInCommon.Util;
using CopyGen.Command;

namespace CopyGen.Core
{
    /// <summary>
    /// CopyGenアドインメイン処理クラス
    /// </summary>
    public class CopyGenConnectCore : ConnectCoreBase
    {
        protected override void RegisterEventCommandAfterStartUp(IDictionary<string, AddInCommon.Command.IDTCExecCommand> commands)
        {
            VSCommandCreator creator = new VSCommandCreator(AddInInstance, ApplicationObject);
            string programId = AddInInstance.ProgID;

            //  エディター右クリックメニュー
            VSCommandUtils.RegisterAddInCommand(
                programId,
                new CopyMethodGenCommand(),
                commands,
                creator,
                creator.GetCommandBar(CommandBarConst.CONTEXT_EDITOR));

            //  ツールメニュー
            VSCommandUtils.RegisterAddInCommand(
                programId,
                new CopyGenMenuCommand(),
                commands,
                creator,
                creator.AddKoropokkurMenuCommandBar());
        }
    }
}
