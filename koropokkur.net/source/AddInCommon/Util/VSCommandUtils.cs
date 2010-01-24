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
using AddInCommon.Command;
using AddInCommon.Const;
using AddInCommon.Exception;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.CommandBars;

namespace AddInCommon.Util
{
    /// <summary>
    /// VisualStudioコマンド用ユーティリティ
    /// </summary>
    public sealed class VSCommandUtils
    {
        /// <summary>
        /// アドイン機能コマンドの生成、登録
        /// </summary>
        /// <param name="programId"></param>
        /// <param name="eventCommand"></param>
        /// <param name="commands"></param>
        /// <param name="creator"></param>
        /// <param name="commandBar"></param>
        /// <param name="isBeginGroup">コントロール上に区切り線を入れるか</param>
        public static void RegisterAddInCommand(
            string programId,
            IDTCExecCommand eventCommand,
            IDictionary<string, IDTCExecCommand> commands,
            VSCommandCreator creator,
            CommandBar commandBar, bool isBeginGroup)
        {
            string vsCommandName = GetVSCommandName(
                programId, eventCommand.CommandName);
            commands[vsCommandName] = eventCommand;
            EnvDTE.Command newCommand = creator.CreateButton(
                eventCommand.CommandName,
                eventCommand.DisplayName,
                eventCommand.ToolTipText);
            RegisterControl(newCommand, commandBar, isBeginGroup);
        }

        /// <summary>
        /// コマンドが登録済みかどうか判定
        /// </summary>
        /// <param name="applicationObject"></param>
        /// <param name="addInProgId">アドインプログラムを一意に識別する名称</param>
        /// <param name="commandName"></param>
        /// <returns></returns>
        public static bool IsRegisterCommand(DTE2 applicationObject, string addInProgId, string commandName)
        {
            string registerCommandName = GetVSCommandName(addInProgId, commandName);
            foreach (object item in applicationObject.Commands)
            {
                EnvDTE.Command command = item as EnvDTE.Command;
                if(command != null && command.Name == registerCommandName)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// CommandBarが登録済みかどうか判定
        /// </summary>
        /// <param name="applicationObject"></param>
        /// <param name="commandBarName"></param>
        /// <returns></returns>
        public static bool IsRegisterCommandBar(DTE2 applicationObject, string commandBarName)
        {
            CommandBars commandBars = (CommandBars)applicationObject.CommandBars;
            try
            {
                CommandBar commandBar = commandBars[commandBarName];
                return commandBar == null ? false : true;
            }
            catch(System.Exception)
            {

                return false;
            }
        }

        /// <summary>
        /// CommandBarの取得
        /// </summary>
        /// <param name="applicationObject"></param>
        /// <param name="commandBarName"></param>
        /// <returns></returns>
        /// <exception cref="CommandBarNotFoundException"></exception>
        public static CommandBar GetCommandBar(DTE2 applicationObject, string commandBarName)
        {
            if(IsRegisterCommandBar(applicationObject, commandBarName))
            {
                CommandBars commandBars = (CommandBars)applicationObject.CommandBars;
                return commandBars[commandBarName];
            }
            throw new CommandBarNotFoundException(commandBarName);
        }

        /// <summary>
        /// 「Koropokkurの設定」メニューの取得
        /// </summary>
        /// <param name="applicationObject"></param>
        /// <returns></returns>
        public static CommandBar GetKoropokkurMenuBar(DTE2 applicationObject)
        {
            if(IsRegisterCommandBar(applicationObject, KoropokkurConst.CONFIG_MENU_NAME))
            {
                return GetCommandBar(applicationObject, KoropokkurConst.CONFIG_MENU_NAME);
            }
            return null;
        }

        /// <summary>
        /// VisualStudioコマンド名を取得する
        /// </summary>
        /// <param name="addInProgId"></param>
        /// <param name="commandName"></param>
        /// <returns></returns>
        public static string GetVSCommandName(string addInProgId, string commandName)
        {
            return string.Format("{0}.{1}", addInProgId, commandName);
        }

        /// <summary>
        /// VisualStudioコントロールの基底設定を返す
        /// </summary>
        /// <returns></returns>
        public static vsCommandStatus GetDefaultStatus()
        {
            return (vsCommandStatus.vsCommandStatusSupported | vsCommandStatus.vsCommandStatusEnabled);
        }

        /// <summary>
        /// VisualStudioコマンドとコントロールの関連付け
        /// </summary>
        /// <param name="command"></param>
        /// <param name="control"></param>
        /// <param name="isBeginGroup">コントロール上の区切り線の有無</param>
        public static void RegisterControl(EnvDTE.Command command, CommandBar control, bool isBeginGroup)
        {
            CommandBarControl newControl =
                (CommandBarControl)command.AddControl(control, control.Controls.Count + 1);
            //  追加したコントロールの上に区切り線設定
            newControl.BeginGroup = isBeginGroup;
        }

        /// <summary>
        /// コントロールが既に含まれているか判定する
        /// </summary>
        /// <param name="controlName"></param>
        /// <param name="controls"></param>
        /// <returns></returns>
        public static bool IsExistsControl(string controlName, CommandBarControls controls)
        {
            foreach (CommandBarControl control in controls)
            {
                if(control.Caption == controlName)
                {
                    return true;
                }
            }
            return false;
        }

        #region 補助メソッド
        /// <summary>
        /// 引数に対応するMsoControlType値を取得
        /// </summary>
        /// <param name="controlType"></param>
        /// <returns></returns>
        private static MsoControlType GetMsoControlType(Type controlType)
        {
            if (controlType == typeof(CommandBarPopup))
            {
                return MsoControlType.msoControlPopup;
            }

            if (controlType == typeof(CommandBarButton))
            {
                return MsoControlType.msoControlButton;
            }
            //  TODO:暫定。例外に変更予定
            return MsoControlType.msoControlButton;
        }

        #endregion
    }
}