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

using System.Windows.Forms;

namespace AddInCommon.Util
{
    /// <summary>
    /// メッセージ表示ユーティリティクラス
    /// </summary>
    public class MessageUtils
    {
        private const string HEADER = "Koropokkur.NET";

        /// <summary>
        /// お知らせメッセージの表示
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void ShowInfoMessage(string message, params object[] args)
        {
            ShowMessage(MessageBoxButtons.OK, MessageBoxIcon.Information, message, args);
        }

        /// <summary>
        /// 警告メッセージの表示
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void ShowWarnMessage(string message, params object[] args)
        {
            ShowMessage(MessageBoxButtons.OK, MessageBoxIcon.Warning, message, args);
        }

        /// <summary>
        /// エラーメッセージの表示
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void ShowErrorMessage(string message, params object[] args)
        {
            ShowMessage(MessageBoxButtons.OK, MessageBoxIcon.Error, message, args);
        }

        /// <summary>
        /// 確認メッセージの表示
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static DialogResult ShowConfirmMessage(string message, params object[] args)
        {
            return ShowMessage(MessageBoxButtons.OKCancel, MessageBoxIcon.Question, message, args);
        }
        
        /// <summary>
        /// メッセージの表示
        /// </summary>
        /// <param name="buttons"></param>
        /// <param name="icon"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static DialogResult ShowMessage(MessageBoxButtons buttons, MessageBoxIcon icon, 
            string message, params object[] args)
        {
            return MessageBox.Show(string.Format(message, args), HEADER, buttons, icon);
        }
    }
}
