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
using System.Windows.Forms;
using VSArrange.Config;
using AddInCommon.Util;

namespace VSArrange.Control.Window
{
    /// <summary>
    /// 設定項目編集フォーム
    /// </summary>
    public partial class ConfigForm : Form
    {
        private const int COL_NO_IS_ENABLE = 0;
        private const int COL_NO_FILTER_NAME = 1;
        private const int COL_NO_FOLDER_STRING = 2;

        public ConfigForm()
        {
            InitializeComponent();
        }

        #region イベント

        /// <summary>
        /// 画面表示イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfigForm_Load(object sender, EventArgs e)
        {
            try
            {
                string configPath = PathUtils.GetConfigPath();
                ConfigInfo configInfo = ConfigFileManager.ReadConfig(configPath);
                UpdateConfigInfo2Display(configInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("設定情報の読み込みに失敗しました。\n{0}", ex.Message));
            }
        }
        
        /// <summary>
        /// DataGridView（ファイル用）クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgFileFilters_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            RemoveRow(sender, e, "buttonRemoveFileFilter" );
        }

        /// <summary>
        /// DataGridView（フォルダ用）クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgFolderFilters_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            RemoveRow(sender, e, "buttonRemoveFolderFilter");
        }

        /// <summary>
        /// 保存して終了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            //  設定を保存する
            try
            {
                ConfigInfo configInfo = ConfigInfo.GetInstance();
                configInfo.FilterFileStringList = CreateConfigInfo(dgFileFilters.Rows);
                configInfo.FilterFolderStringList = CreateConfigInfo(dgFolderFilters.Rows);
                string configFilePath = PathUtils.GetConfigPath();
                ConfigFileManager.WriteConfig(configFilePath, configInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("設定の書き込みに失敗しました。\n" + ex.Message);
                return;
            }
            Close();
        }

        /// <summary>
        /// 画面を閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }


        #endregion

        #region 補助メソッド

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

        /// <summary>
        /// DataGridViewへの入力から設定情報を生成する
        /// </summary>
        /// <param name="rows"></param>
        /// <returns></returns>
        private IList<ConfigInfoFilter> CreateConfigInfo(DataGridViewRowCollection rows)
        {
            IList<ConfigInfoFilter> configInfoFilters = new List<ConfigInfoFilter>(rows.Count);
            foreach (DataGridViewRow row in rows)
            {
                if (row == null ||
                    row.Cells[COL_NO_FILTER_NAME].Value == null ||
                    row.Cells[COL_NO_FOLDER_STRING].Value == null)
                {
                    continue;
                }
                ConfigInfoFilter configInfoFilter = new ConfigInfoFilter();
                configInfoFilter.IsEnable = row.Cells[COL_NO_IS_ENABLE].Value == null ? false : (bool)row.Cells[COL_NO_IS_ENABLE].Value;
                configInfoFilter.Name = (string) row.Cells[COL_NO_FILTER_NAME].Value;
                configInfoFilter.FilterString = (string) row.Cells[COL_NO_FOLDER_STRING].Value;
                configInfoFilters.Add(configInfoFilter);
            }
            return configInfoFilters;
        }

        /// <summary>
        /// 設定情報を画面表示に反映させる
        /// </summary>
        /// <param name="configInfo"></param>
        private void UpdateConfigInfo2Display(ConfigInfo configInfo)
        {
            if(configInfo == null)
            {
                throw new ArgumentNullException("configInfo");
            }

            SetDataGridView(configInfo.FilterFileStringList, dgFileFilters);
            SetDataGridView(configInfo.FilterFolderStringList, dgFolderFilters);
        }

        /// <summary>
        /// 設定情報をDataGridViewに反映させる
        /// </summary>
        /// <param name="configInfoFilters"></param>
        /// <param name="dataGridView"></param>
        private void SetDataGridView(IList<ConfigInfoFilter> configInfoFilters, DataGridView dataGridView)
        {
            foreach (ConfigInfoFilter filter in configInfoFilters)
            {
                dataGridView.Rows.Add(new object[] {filter.IsEnable, filter.Name, filter.FilterString});
            }
        }

        #endregion

        

        
        

        
    }
}
