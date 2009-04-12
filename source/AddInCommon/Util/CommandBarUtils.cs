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
using EnvDTE80;
using Microsoft.VisualStudio.CommandBars;

namespace AddInCommon.Util
{
    /// <summary>
    /// CommandBar利用ユーティリティ
    /// </summary>
    public class CommandBarUtils
    {
        private const string CONFIG_MENU_NAME = "Koropokkurの設定";

        /// <summary>
        /// ソリューション右クリックで表示されるメニューコントロールを取得
        /// </summary>
        /// <param name="applicationObject"></param>
        /// <returns></returns>
        public static CommandBar GetSolutionContextMenu(DTE2 applicationObject)
        {
            return GetCommandBar(applicationObject, "Solution");
        }

        /// <summary>
        /// プロジェクト右クリックで表示されるメニューコントロールを取得
        /// </summary>
        /// <param name="applicationObject"></param>
        /// <returns></returns>
        public static CommandBar GetProjectContextMenu(DTE2 applicationObject)
        {
            return GetCommandBar(applicationObject, "Project");
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
            if(IsExistsControl(koroppokurMenuName, toolsPopup.Controls))
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

        #region 補助メソッド

        /// <summary>
        /// コントロールが既に含まれているか判定する
        /// </summary>
        /// <param name="controlName"></param>
        /// <param name="controls"></param>
        /// <returns></returns>
        private static bool IsExistsControl(string controlName, CommandBarControls controls)
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