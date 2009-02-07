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
using VSArrange.Filter;

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

            filterFile.FilterName = "ファイル";
            filterFile.TestExecuted += filter_TestExecuted;
            filterFolder.FilterName = "フォルダ";
            filterFolder.TestExecuted += filter_TestExecuted;
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
                configInfo.FilterFileStringList = CreateConfigInfo(
                    filterFile.GetFilterDefinitions());
                configInfo.FilterFolderStringList = CreateConfigInfo(
                    filterFolder.GetFilterDefinitions());
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

        /// <summary>
        /// フィルターテスト実行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="inputText"></param>
        /// <returns></returns>
        private static bool filter_TestExecuted(FilterList sender, string inputText)
        {
            return IsFilterHit(inputText, sender.GetFilterDefinitions());
        }


        #endregion

        #region 補助メソッド

        /// <summary>
        /// 入力文字列がフィルターを通過するか判定
        /// </summary>
        /// <param name="inputText"></param>
        /// <param name="filterDefinitions"></param>
        /// <returns></returns>
        private static bool IsFilterHit(string inputText, DataGridViewRowCollection filterDefinitions)
        {
            if (string.IsNullOrEmpty(inputText))
            {
                return false;
            }

            ItemAttachmentFilter filter = new ItemAttachmentFilter();
            filter.AddFilters(CreateConfigInfo(filterDefinitions));
            return !filter.IsPassFilter(inputText);
        }

        /// <summary>
        /// DataGridViewへの入力から設定情報を生成する
        /// </summary>
        /// <param name="rows"></param>
        /// <returns></returns>
        private static IList<ConfigInfoFilter> CreateConfigInfo(DataGridViewRowCollection rows)
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

            SetDataGridView(configInfo.FilterFileStringList, filterFile);
            SetDataGridView(configInfo.FilterFolderStringList, filterFolder);
        }

        /// <summary>
        /// 設定情報をDataGridViewに反映させる
        /// </summary>
        /// <param name="configInfoFilters"></param>
        /// <param name="filterList"></param>
        private static void SetDataGridView(IEnumerable<ConfigInfoFilter> configInfoFilters, FilterList filterList)
        {
            if(configInfoFilters == null)
            {
                throw new ArgumentNullException("configInfoFilters");
            }

            foreach (ConfigInfoFilter filter in configInfoFilters)
            {
                filterList.SetFilterDefinitions(
                    new object[] { filter.IsEnable, filter.Name, filter.FilterString });
            }
        }

        #endregion
    }
}
