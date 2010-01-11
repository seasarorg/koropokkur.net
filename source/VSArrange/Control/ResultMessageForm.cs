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
using System.Text;
using System.Windows.Forms;

namespace VSArrange.Control
{
    /// <summary>
    /// 処理結果表示画面
    /// </summary>
    public partial class ResultMessageForm : Form
    {
        private const int MIN_ROW_COUNT = 1;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ResultMessageForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 処理結果を設定する
        /// </summary>
        /// <param name="messages"></param>
        public void SetResultList(string messages)
        {
            if(messages == null)
            {
                return;
            }

            string[] messageParts = messages.Split(
                new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            if (messageParts.Length > MIN_ROW_COUNT)
            {
                //  １行ごとに空行が入るのでそれを見越して処理する

                //  開始、終了時間を表示
                lstResultList.Items.Add(messageParts[0]);
                lstResultList.Items.Add(messageParts[messageParts.Length - 1]);

                //  処理結果を表示（最初と最後以外の行）
                for (int i = 1; i < messageParts.Length - 1; i++)
                {
                    string line = messageParts[i].Trim();
                    if (line.Length > 0)
                    {
                        string[] lineParts = line.Split('\t');
                        dgvResultMessage.Rows.Add(lineParts);
                    }
                }
            }
        }

        /// <summary>
        /// 閉じるボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// リスト上でマウスボタンを離す
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstResultList_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if(lstResultList.SelectedItems == null)
            {
                return;
            }

            StringBuilder selectedTextBuilder = new StringBuilder();
            foreach (string selectedItem in lstResultList.SelectedItems)
            {
                selectedTextBuilder.AppendLine(selectedItem);
            }

            if (selectedTextBuilder.Length > 0)
            {
                Clipboard.SetText(selectedTextBuilder.ToString());
            }
        }
    }
}
