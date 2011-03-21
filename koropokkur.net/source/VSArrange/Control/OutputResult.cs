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

using System;
using System.Windows.Forms;
using VSArrange.Config;

namespace VSArrange.Control
{
    /// <summary>
    /// 結果出力設定
    /// </summary>
    public partial class OutputResult : UserControl
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public OutputResult()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 処理結果ファイル出力の有効無効切替
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkIsOutputFile_CheckedChanged(object sender, EventArgs e)
        {
            txtOutputResultPath.Enabled = chkIsOutputFile.Checked;
            btnSelectOutputResultPath.Enabled = chkIsOutputFile.Checked;
        }

        /// <summary>
        /// 処理結果出力パス選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectOutputResultPath_Click(object sender, EventArgs e)
        {
            dlgSelectOutputResultPath.SelectedPath = txtOutputResultPath.Text;
            if (dlgSelectOutputResultPath.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtOutputResultPath.Text = dlgSelectOutputResultPath.SelectedPath;
            }
        }

        /// <summary>
        /// 画面から入力されたウィンドウへの結果出力設定を取得
        /// </summary>
        /// <returns></returns>
        public ConfigInfoDetail GetOutputResultWindowDefinition()
        {
            ConfigInfoDetail configInfoWindow = new ConfigInfoDetail();
            configInfoWindow.IsEnable = chkIsOuputWindow.Checked;
            return configInfoWindow;
        }

        /// <summary>
        /// 画面から入力されたファイルへの結果出力設定を取得
        /// </summary>
        /// <returns></returns>
        public ConfigInfoDetail GetOutputResultFileDefinition()
        {
            ConfigInfoDetail configInfoOutputFile = new ConfigInfoDetail();
            configInfoOutputFile.IsEnable = chkIsOutputFile.Checked;
            configInfoOutputFile.Value = txtOutputResultPath.Text;
            return configInfoOutputFile;
        }

        /// <summary>
        /// 処理結果出力設定をコントロールに反映させる
        /// </summary>
        /// <param name="outputResultWindow"></param>
        /// <param name="outputResultFile"></param>
        public void SetOutputResultDefinitions(
            ConfigInfoDetail outputResultWindow, ConfigInfoDetail outputResultFile)
        {
            chkIsOuputWindow.Checked = outputResultWindow.IsEnable;
            chkIsOutputFile.Checked = outputResultFile.IsEnable;
            txtOutputResultPath.Enabled = chkIsOutputFile.Checked;
            txtOutputResultPath.Text = outputResultFile.Value;
            btnSelectOutputResultPath.Enabled = chkIsOutputFile.Checked;
        }
    }
}
