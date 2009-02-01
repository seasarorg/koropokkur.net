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

using AddInCommon.Util;
using EnvDTE80;
using Microsoft.VisualStudio.CommandBars;
using System.Windows.Forms;
using VSArrange.Control.Window;

namespace VSArrange.Control
{
    /// <summary>
    /// 設定画面表示コントロール
    /// </summary>
    public class ConfigMenu
    {
        private const string MENU_NAME = "VSArrange";
        private readonly DTE2 _applicationObject;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="applicationObject"></param>
        public ConfigMenu(DTE2 applicationObject)
        {
            _applicationObject = applicationObject;
        }

        /// <summary>
        /// ソリューション右クリックメニューに項目を一つ追加して返す
        /// </summary>
        /// <param name="parentPopup"></param>
        /// <returns></returns>
        public virtual CommandBarControl CreateSolutionContextMenuItem(CommandBarPopup parentPopup)
        {
            CommandBarButton configMenuButton = 
                CommandBarUtils.CreatePopupChildControl<CommandBarButton>(parentPopup);
            configMenuButton.Caption = MENU_NAME;
            configMenuButton.Click += configMenuButton_Click;
            return configMenuButton;
        }

        #region イベント
        private void configMenuButton_Click(CommandBarButton Ctrl, ref bool CancelDefault)
        {
            using (ConfigForm dialog = new ConfigForm())
            {
                if(dialog.ShowDialog() == DialogResult.OK)
                {
                    
                }
            }
        }
        #endregion
    }
}
