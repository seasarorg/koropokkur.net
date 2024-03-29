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

using AddInCommon.Command;
using AddInCommon.Util;
using CopyGen.Control;

namespace CopyGen.Command
{
    /// <summary>
    /// ツールからKoropkkur.NETの設定->CopyGenで呼び出す処理クラス
    /// </summary>
    public class CopyGenMenuCommand : IDTCExecCommand
    {
        #region IDTCExecCommand メンバ

        public string CommandName
        {
            get { return "CopyGenMenu"; }
        }

        public string DisplayName
        {
            get { return "CopyGen"; }
        }

        public string ToolTipText
        {
            get { return "CopyGenによるメソッド生成内容を設定します。"; }
        }

        public EnvDTE.vsCommandStatus GetCommandStatus(EnvDTE80.DTE2 applicationObject, EnvDTE.AddIn addInInstance, ref object commandText)
        {
            return VSCommandUtils.GetDefaultStatus();
        }

        public bool Execute(EnvDTE80.DTE2 applicationObject, EnvDTE.AddIn addInInstance, ref object varIn, ref object varOut)
        {
            using (CopyConfig config = new CopyConfig())
            {
                config.ShowDialog();
            }
            return true;
        }

        #endregion
    }
}
