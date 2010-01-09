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

using System;
using System.Collections.Generic;
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
        private const string CONFIG_MENU_NAME = "Koropokkurの設定";

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
        public static void RegisterControl(EnvDTE.Command command, CommandBar control)
        {            
            CommandBarControl newControl = 
                (CommandBarControl)command.AddControl(control, control.Controls.Count + 1);
            //  追加したコントロールの上に区切り線を入れる
            newControl.BeginGroup = true;
        }

        /// <summary>
        /// エディタ右クリックで表示されるメニューコントロールを取得
        /// </summary>
        /// <param name="applicationObject"></param>
        /// <returns></returns>
        public static CommandBar GetCodeContextMenu(DTE2 applicationObject)
        {
            return GetCommandBar(applicationObject, "Code Window");
        }

        /// <summary>
        /// メニューコントロールを取得
        /// </summary>
        /// <param name="applicationObject"></param>
        /// <returns></returns>
        public static CommandBar GetMenuBar(DTE2 applicationObject)
        {
            return GetCommandBar(applicationObject, "MenuBar");
        }

        /// <summary>
        /// Koropokkur設定メニューにコントロールを追加する
        /// </summary>
        /// <param name="applicationObject"></param>
        /// <param name="customControlContainer"></param>
        /// <returns></returns>
        public static CommandBarPopup GetKoropokkurConfigMenu(DTE2 applicationObject, IList<CommandBarControl> customControlContainer)
        {
            //  Koropokkurメニューバーを追加
            CommandBar menuBarCommandBar = GetMenuBar(applicationObject);
            string toolsMenuName = ResourceUtils.GetResourceWord(applicationObject, "Tools");
            //MenuBar コマンド バーで [ツール] コマンド バーを検索します:
            CommandBarControl toolsControl = menuBarCommandBar.Controls[toolsMenuName];
            CommandBarPopup toolsPopup = (CommandBarPopup)toolsControl;

            //  TODO:リソースファイルを使うようにする
            //string koroppokurMenuName = ResourceUtils.GetResourceWord(applicationObject, CONFIG_MENU_NAME);
            const string koroppokurMenuName = CONFIG_MENU_NAME;
            CommandBarPopup koropokkurPopup;
            if (IsExistsControl(koroppokurMenuName, toolsPopup.Controls))
            {
                koropokkurPopup = (CommandBarPopup)toolsPopup.Controls[koroppokurMenuName];
            }
            else
            {
                koropokkurPopup = CreatePopupChildControl<CommandBarPopup>(toolsPopup);
                koropokkurPopup.Caption = koroppokurMenuName;
                customControlContainer.Add(koropokkurPopup);
            }

            return koropokkurPopup;
        }

        /// <summary>
        /// 引数コントロールの一番後ろにコントロールを一つ追加する
        /// </summary>
        /// <param name="commandBar"></param>
        /// <returns></returns>
        public static CONTROL_TYPE CreateCommandBarControl<CONTROL_TYPE>(CommandBar commandBar)
        {
            return (CONTROL_TYPE)commandBar.Controls.Add(
                                         GetMsoControlType(typeof(CONTROL_TYPE)), Type.Missing,
                                         Type.Missing, Type.Missing, true);
        }

        /// <summary>
        /// ポップアップにコントロールを追加する
        /// </summary>
        /// <param name="parentPopup">親コントロール</param>
        /// <returns></returns>
        public static CONTROL_TYPE CreatePopupChildControl<CONTROL_TYPE>(CommandBarPopup parentPopup)
            where CONTROL_TYPE : CommandBarControl
        {
            return (CONTROL_TYPE)parentPopup.Controls.Add(
                                         GetMsoControlType(typeof(CONTROL_TYPE)), Type.Missing,
                                         Type.Missing, Type.Missing, true);
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
            if(controlType == typeof(CommandBarPopup))
            {
                return MsoControlType.msoControlPopup;
            }
            
            if(controlType == typeof(CommandBarButton))
            {
                return MsoControlType.msoControlButton;
            }
            //  TODO:暫定。例外に変更予定
            return MsoControlType.msoControlButton;
        }

        /// <summary>
        /// コントロール取得
        /// </summary>
        /// <param name="applicationObject"></param>
        /// <param name="commandBarName"></param>
        /// <returns></returns>
        private static CommandBar GetCommandBar(DTE2 applicationObject, string commandBarName)
        {
            return ((CommandBars)applicationObject.CommandBars)[commandBarName];
        }

        #endregion
    }
}