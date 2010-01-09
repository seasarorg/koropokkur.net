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
using System.Windows.Forms;
using AddInCommon.Const;
using AddInCommon.Util;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.CommandBars;

namespace AddInCommon.Command
{
    /// <summary>
    /// VisualStudio上で動くCommandを生成するクラス
    /// </summary>
    public class VSCommandCreator
    {
        private const string CONFIG_MENU_NAME = "Koropokkurの設定";
        private readonly AddIn _addInInstance;
        private readonly DTE2 _applicationObject;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="addInInstance">(NotNull)</param>
        /// <param name="applicationObject">(NotNull)</param>
        public VSCommandCreator(AddIn addInInstance, DTE2 applicationObject)
        {
            if (addInInstance == null) throw new ArgumentNullException("addInInstance");
            if (applicationObject == null) throw new ArgumentNullException("applicationObject");

            _addInInstance = addInInstance;
            _applicationObject = applicationObject;
        }

        /// <summary>
        /// コマンドをVisualStudioアドインに登録する
        /// </summary>
        /// <param name="commandName"></param>
        /// <param name="dispayText"></param>
        /// <param name="toolTipText"></param>
        /// <param name="isUseOfficeResource"></param>
        /// <param name="officeItemId"></param>
        /// <param name="status"></param>
        /// <param name="style"></param>
        /// <param name="commandControlType">
        /// <returns>作成したコントロール</returns>
        public EnvDTE.Command Create(
            string commandName, string dispayText, string toolTipText,
            bool isUseOfficeResource, int officeItemId,
            vsCommandStatus status, vsCommandStyle style, vsCommandControlType commandControlType)
        {
            object[] contextGUIDS = new object[] {};
            Commands2 commands = (Commands2)_applicationObject.Commands;
            if (VSCommandUtils.IsRegisterCommand(_applicationObject, _addInInstance.ProgID, commandName))
            {
                EnvDTE.Command oldCommand = _applicationObject.Commands.Item(
                    VSCommandUtils.GetVSCommandName(_addInInstance.ProgID, commandName), -1);
                oldCommand.Delete();
            }

            EnvDTE.Command newCommand = null;
            try
            {
                newCommand = commands.AddNamedCommand2(_addInInstance, commandName, dispayText,
                                                       toolTipText,
                                                       isUseOfficeResource, officeItemId,
                                                       ref contextGUIDS,
                                                       (int)status, (int)style, commandControlType);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, this.GetType().Name + "#" + "Create");
                throw;
            }
 
            return newCommand;
        }

        /// <summary>
        /// コマンドをVisualStudioアドインに登録する
        /// </summary>
        /// <param name="commandName"></param>
        /// <param name="dispayText"></param>
        /// <param name="toolTipText"></param>
        public EnvDTE.Command CreateButton(
            string commandName, string dispayText, string toolTipText) 
        {
            return Create(commandName, dispayText, toolTipText, true, IconIdConst.SMILE,
                   vsCommandStatus.vsCommandStatusEnabled, vsCommandStyle.vsCommandStylePictAndText,
                   vsCommandControlType.vsCommandControlTypeButton);
        }

        /// <summary>
        /// CommandBarの取り出し
        /// </summary>
        /// <param name="commandBarName"></param>
        /// <returns></returns>
        public CommandBar GetCommandBar(string commandBarName)
        {
            CommandBars commandBars = (CommandBars)_applicationObject.CommandBars;
            return commandBars[commandBarName];
        }

        /// <summary>
        /// Koropokkur設定メニューにコントロールを取得(+未作成なら作成）
        /// </summary>
        /// <returns></returns>
        public CommandBarPopup GetKoropokkurConfigMenu()
        {
            //  Koropokkurメニューバーを追加
            CommandBar menuBarCommandBar = GetCommandBar(CommandBarConst.MENU_BAR);
            string toolsMenuName = ResourceUtils.GetResourceWord(_applicationObject, "Tools");
            //MenuBar コマンド バーで [ツール] コマンド バーを検索します:
            CommandBarControl toolsControl = menuBarCommandBar.Controls[toolsMenuName];
            CommandBarPopup toolsPopup = (CommandBarPopup)toolsControl;

            //  TODO:リソースファイルを使うようにする
            //string koroppokurMenuName = ResourceUtils.GetResourceWord(applicationObject, CONFIG_MENU_NAME);
            const string koroppokurMenuName = CONFIG_MENU_NAME;
            CommandBarPopup koropokkurPopup;
            if (VSCommandUtils.IsExistsControl(koroppokurMenuName, toolsPopup.Controls))
            {
                koropokkurPopup = (CommandBarPopup)toolsPopup.Controls[koroppokurMenuName];
            }
            else
            {
                koropokkurPopup = VSCommandUtils.CreatePopupChildControl<CommandBarPopup>(toolsPopup);
                koropokkurPopup.Caption = koroppokurMenuName;
            }

            return koropokkurPopup;
        }

    }
}