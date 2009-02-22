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

namespace VSArrange.Control.Window
{
    /// <summary>
    /// フィルターテスト実行イベントデリゲート
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="inputText"></param>
    /// <returns></returns>
    public delegate bool TestExecutedEventHandler(FilterList sender, string inputText);

    /// <summary>
    /// 再読み込みイベントデリゲート
    /// </summary>
    public delegate void ReloadExecutedEventHandler();

    /// <summary>
    /// フィルター設定コントロール
    /// </summary>
    public partial class FilterList : UserControl
    {
        /// <summary>
        /// フィルターテスト実行イベント
        /// </summary>
        public event TestExecutedEventHandler TestExecuted;

        /// <summary>
        /// 再読み込み実行イベント
        /// </summary>
        public event ReloadExecutedEventHandler ReloadExecuted;

        /// <summary>
        /// フィルター名称
        /// </summary>
        public string FilterName
        {
            set { grpFilter.Text = value; }
            get { return grpFilter.Text; }
        }

        public FilterList()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 表示するフィルター情報を設定
        /// </summary>
        /// <param name="values"></param>
        public void SetFilterDefinitions(object[] values)
        {
            dgFilters.Rows.Add(values);
        }

        /// <summary>
        /// 入力されたフィルター一覧を取得
        /// </summary>
        /// <returns></returns>
        public DataGridViewRowCollection GetFilterDefinitions()
        {
            return dgFilters.Rows;
        }

        /// <summary>
        /// 表示しているフィルター一覧を消去する
        /// </summary>
        public void Clear()
        {
            dgFilters.Rows.Clear();
        }

        #region イベント

        /// <summary>
        /// 元に戻すボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUndo_Click(object sender, EventArgs e)
        {
            if (ReloadExecuted != null)
            {
                ReloadExecuted();
            }
        }

        private void txtTestInput_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if(TestExecuted == null)
                {
                    return;
                }

                TextBox textBox = sender as TextBox;
                if(textBox == null)
                {
                    return;
                }

                bool isOk = TestExecuted(this, textBox.Text);
                lblOK.Visible = isOk;
                lblNG.Visible = !isOk;

                timerCloseMessage.Enabled = true;
            }
            else
            {
                CloseNoticeMessage();
            }
        }

        private void txtTestInput_LostFocus(object sender, System.EventArgs e)
        {
            CloseNoticeMessage();
        }

        private void timerCloseMessage_Tick(object sender, EventArgs e)
        {
            CloseNoticeMessage();
        }

        /// <summary>
        /// DataGridView（ファイル用）クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgFileFilters_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            RemoveRow(sender, e, "buttonRemoveFileFilter");
        }

        #endregion

        #region 補助メソッド

        /// <summary>
        /// テスト結果通知メッセージを消去
        /// </summary>
        private void CloseNoticeMessage()
        {
            lblOK.Visible = false;
            lblNG.Visible = false;
            timerCloseMessage.Enabled = false;
        }

        /// <summary>
        /// 行削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="targetControlName"></param>
        private void RemoveRow(object sender, DataGridViewCellEventArgs e, string targetControlName)
        {
            DataGridView dgv = (DataGridView)sender;
            //  最下（新規）行以外の削除ボタンがクリックされた
            if (dgv.Columns[e.ColumnIndex].Name == targetControlName
                && !dgv.Rows[e.RowIndex].IsNewRow)
            {
                dgv.Rows.RemoveAt(e.RowIndex);
            }
        }

        #endregion

        

        
    }
}
