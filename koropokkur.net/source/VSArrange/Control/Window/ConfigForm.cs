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
using AddInCommon.Util;
using VSArrange.Config;
using VSArrange.Filter;
using System.Diagnostics;

namespace VSArrange.Control.Window
{
    /// <summary>
    /// 設定項目編集フォーム
    /// </summary>
    public partial class ConfigForm : Form
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ConfigForm()
        {
            InitializeComponent();
            SetEvents();
        }        

        #region イベント

        /// <summary>
        /// 画面表示イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfigForm_Load(object sender, EventArgs e)
        {
            ConfigInfo configInfo = GetConfigInfo();
            if (configInfo == null)
            {
                return;
            }

            try
            {
                SetFilterFile(configInfo);
                SetFilterFolder(configInfo);

                SetFilterCompile(configInfo);
                SetFilterResouce(configInfo);
                SetFilterContents(configInfo);
                SetFilterNoAction(configInfo);

                SetFilterNoCopy(configInfo);
                SetFilterEverytimeCopy(configInfo);
                SetFilterCopyIfNew(configInfo);

                SetOutputResult(configInfo);
            }
            catch(System.Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
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
                SetConfingInfo(configInfo);

                string configFilePath = PathUtils.GetConfigPath();
                ConfigFileManager.WriteConfig(configFilePath, configInfo);
            }
            catch (Exception ex)
            {
                MessageUtils.ShowErrorMessage("設定の書き込みに失敗しました。\n" + ex.Message);
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
        /// 直接編集
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditDirect_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("設定ファイルを直接編集する場合、現在編集中の内容は破棄されます。よろしいですか？", 
                "設定ファイル直接編集", MessageBoxButtons.OKCancel, 
                MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

            string configPath = PathUtils.GetConfigPath();
            try
            {
                Process process = Process.Start(configPath);
                if(process == null)
                {
                    throw new NullReferenceException("エディタプロセス起動失敗");
                }
                process.WaitForExit();

                ConfigInfo configInfo = GetConfigInfo();

                SetFilterFile(configInfo);
                SetFilterFolder(configInfo);

                SetFilterCompile(configInfo);
                SetFilterResouce(configInfo);
                SetFilterContents(configInfo);
                SetFilterNoAction(configInfo);

                SetFilterNoCopy(configInfo);
                SetFilterEverytimeCopy(configInfo);
                SetFilterCopyIfNew(configInfo);

                SetOutputResult(configInfo);
            }
            catch(System.Exception ex)
            {
                MessageUtils.ShowErrorMessage("エディタの起動、設定ファイルの読込に失敗しました。\n" +
                                PathUtils.GetConfigPath() + "\n" +
                                ex.Message + "\n" + ex.StackTrace);
            }
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

        #region ReloadExecuted
        /// <summary>
        /// ファイル用のフィルタ設定を再読込み
        /// </summary>
        private void filterFile_ReloadExecuted()
        {
            SetFilterFile(GetConfigInfo());
        }

        /// <summary>
        /// フォルダ用フィルタ設定を再読込み
        /// </summary>
        private void filterFolder_ReloadExecuted()
        {
            SetFilterFolder(GetConfigInfo());
        }

        /// <summary>
        /// ビルドアクション「コンパイル」用のフィルタ設定を再読込み
        /// </summary>
        private void filterCompile_ReloadExecuted()
        {
            SetFilterCompile(GetConfigInfo());
        }

        /// <summary>
        /// ビルドアクション「埋め込みリソース」用のフィルタ設定を再読込み
        /// </summary>
        private void filterResouce_ReloadExecuted()
        {
            SetFilterResouce(GetConfigInfo());
        }

        /// <summary>
        /// ビルドアクション「コンテンツ」用のフィルタ設定を再読込み
        /// </summary>
        private void filterContents_ReloadExecuted()
        {
            SetFilterContents(GetConfigInfo());
        }

        /// <summary>
        /// ビルドアクション「なし」用のフィルタ設定を再読込み
        /// </summary>
        private void filterNoAction_ReloadExecuted()
        {
            SetFilterNoAction(GetConfigInfo());
        }

        /// <summary>
        /// ビルド後「コピーしない」用のフィルタ設定を再読込み
        /// </summary>
        private void filterNoCopy_ReloadExecuted()
        {
            SetFilterNoCopy(GetConfigInfo());
        }

        /// <summary>
        /// ビルド後「常にコピー用のフィルタ設定を再読込み
        /// </summary>
        private void filterEverytimeCopy_ReloadExecuted()
        {
            SetFilterEverytimeCopy(GetConfigInfo());
        }

        /// <summary>
        /// ビルド後「新しい場合はコピー」用のフィルタ設定を再読込み
        /// </summary>
        private void filterCopyIfNew_ReloadExecuted()
        {
            SetFilterCopyIfNew(GetConfigInfo());
        }
        #endregion

        #endregion

        #region 補助メソッド

        /// <summary>
        /// イベントの設定
        /// </summary>
        private void SetEvents()
        {
            filterFile.TestExecuted += filter_TestExecuted;
            filterFile.ReloadExecuted += filterFile_ReloadExecuted;
            filterFolder.TestExecuted += filter_TestExecuted;
            filterFolder.ReloadExecuted += filterFolder_ReloadExecuted;
            filterCompile.TestExecuted += filter_TestExecuted;
            filterCompile.ReloadExecuted += filterCompile_ReloadExecuted;
            filterResource.TestExecuted += filter_TestExecuted;
            filterResource.ReloadExecuted += filterResouce_ReloadExecuted;
            filterContents.TestExecuted += filter_TestExecuted;
            filterContents.ReloadExecuted += filterContents_ReloadExecuted;
            filterNoAction.TestExecuted += filter_TestExecuted;
            filterNoAction.ReloadExecuted += filterNoAction_ReloadExecuted;
            filterNoCopy.TestExecuted += filter_TestExecuted;
            filterNoCopy.ReloadExecuted += filterNoCopy_ReloadExecuted;
            filterEverytimeCopy.TestExecuted += filter_TestExecuted;
            filterEverytimeCopy.ReloadExecuted += filterEverytimeCopy_ReloadExecuted;
            filterCopyIfNew.TestExecuted += filter_TestExecuted;
            filterCopyIfNew.ReloadExecuted += filterCopyIfNew_ReloadExecuted;
        }

        /// <summary>
        /// 入力内容を設定情報に反映させる
        /// </summary>
        /// <param name="configInfo"></param>
        private void SetConfingInfo(ConfigInfo configInfo)
        {
            configInfo.FilterFileStringList = filterFile.GetFilterDefinitions();
            configInfo.FilterFolderStringList = filterFolder.GetFilterDefinitions();

            configInfo.FilterCompileStringList = filterCompile.GetFilterDefinitions();
            configInfo.FilterResourceStringList = filterResource.GetFilterDefinitions();
            configInfo.FilterContentsStringList = filterContents.GetFilterDefinitions();
            configInfo.FilterNoActionStringList = filterNoAction.GetFilterDefinitions();

            configInfo.FilterNoCopyStringList = filterNoCopy.GetFilterDefinitions();
            configInfo.FilterEverytimeCopyStringList = filterEverytimeCopy.GetFilterDefinitions();
            configInfo.FilterCopyIfNewStringList = filterCopyIfNew.GetFilterDefinitions();

            configInfo.OutputResultWindow = outputResultConfig.GetOutputResultWindowDefinition();
            configInfo.OutputResultFile = outputResultConfig.GetOutputResultFileDefinition(); 
        }

        /// <summary>
        /// 入力文字列がフィルターを通過するか判定
        /// </summary>
        /// <param name="inputText"></param>
        /// <param name="filterDefinitions"></param>
        /// <returns></returns>
        private static bool IsFilterHit(string inputText, IList<ConfigInfoDetail> filterDefinitions)
        {
            if (string.IsNullOrEmpty(inputText))
            {
                return false;
            }

            ItemAttachmentFilter filter = new ItemAttachmentFilter();
            filter.AddFilters(filterDefinitions);
            return !filter.IsPassFilter(inputText);
        }

        /// <summary>
        /// 設定情報の取得
        /// </summary>
        /// <returns></returns>
        private ConfigInfo GetConfigInfo()
        {
            try
            {
                string configPath = PathUtils.GetConfigPath();
                return ConfigFileManager.ReadConfig(configPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("設定情報の読み込みに失敗しました。\n{0}", ex.Message));
            }
            return null;
        }

        /// <summary>
        /// 設定情報をDataGridViewに反映させる
        /// </summary>
        /// <param name="configInfoFilters"></param>
        /// <param name="filterList"></param>
        private static void SetDataGridView(IEnumerable<ConfigInfoDetail> configInfoFilters, FilterList filterList)
        {
            //  未設定と見なして画面への繁栄は行わない
            if(configInfoFilters == null)
            {
                return;
            }

            foreach (ConfigInfoDetail filter in configInfoFilters)
            {
                filterList.SetFilterDefinitions(
                    new object[] { filter.IsEnable, filter.Name, filter.Value });
            }
        }

        /// <summary>
        /// ファイル用のフィルタ設定をコントロールに反映
        /// </summary>
        /// <param name="configInfo"></param>
        private void SetFilterFile(ConfigInfo configInfo)
        {
            if (configInfo == null)
            {
                return;
            }
            filterFile.Clear();
            SetDataGridView(configInfo.FilterFileStringList, filterFile);
        }

        /// <summary>
        /// フォルダ用フィルタ設定をコントロールに反映
        /// </summary>
        /// <param name="configInfo"></param>
        private void SetFilterFolder(ConfigInfo configInfo)
        {
            if (configInfo == null)
            {
                return;
            }
            filterFolder.Clear();
            SetDataGridView(configInfo.FilterFolderStringList, filterFolder);
        }

        /// <summary>
        /// ビルドアクション「コンパイル」用のフィルタ設定をコントロールに反映
        /// </summary>
        /// <param name="configInfo"></param>
        private void SetFilterCompile(ConfigInfo configInfo)
        {
            if (configInfo == null)
            {
                return;
            }
            filterCompile.Clear();
            SetDataGridView(configInfo.FilterCompileStringList, filterCompile);
        }

        /// <summary>
        /// ビルドアクション「埋め込みリソース」用のフィルタ設定をコントロールに反映
        /// </summary>
        /// <param name="configInfo"></param>
        private void SetFilterResouce(ConfigInfo configInfo)
        {
            if (configInfo == null)
            {
                return;
            }
            filterResource.Clear();
            SetDataGridView(configInfo.FilterResourceStringList, filterResource);
        }

        /// <summary>
        /// ビルドアクション「コンテンツ」用のフィルタ設定をコントロールに反映
        /// </summary>
        /// <param name="configInfo"></param>
        private void SetFilterContents(ConfigInfo configInfo)
        {
            if (configInfo == null)
            {
                return;
            }
            filterContents.Clear();
            SetDataGridView(configInfo.FilterContentsStringList, filterContents);
        }

        /// <summary>
        /// ビルドアクション「なし」用のフィルタ設定をコントロールに反映
        /// </summary>
        /// <param name="configInfo"></param>
        private void SetFilterNoAction(ConfigInfo configInfo)
        {
            if (configInfo == null)
            {
                return;
            }
            filterNoAction.Clear();
            SetDataGridView(configInfo.FilterNoActionStringList, filterNoAction);
        }

        /// <summary>
        /// ビルド後「コピーしない」用のフィルタ設定をコントロールに反映
        /// </summary>
        /// <param name="configInfo"></param>
        private void SetFilterNoCopy(ConfigInfo configInfo)
        {
            if (configInfo == null)
            {
                return;
            }
            filterNoCopy.Clear();
            SetDataGridView(configInfo.FilterNoCopyStringList, filterNoCopy);
        }

        /// <summary>
        /// ビルド後「常にコピー用のフィルタ設定をコントロールに反映
        /// </summary>
        /// <param name="configInfo"></param>
        private void SetFilterEverytimeCopy(ConfigInfo configInfo)
        {
            if (configInfo == null)
            {
                return;
            }
            filterEverytimeCopy.Clear();
            SetDataGridView(configInfo.FilterEverytimeCopyStringList, filterEverytimeCopy);
        }

        /// <summary>
        /// ビルド後「新しい場合はコピー」用のフィルタ設定をコントロールに反映
        /// </summary>
        /// <param name="configInfo"></param>
        private void SetFilterCopyIfNew(ConfigInfo configInfo)
        {
            if (configInfo == null)
            {
                return;
            }
            filterCopyIfNew.Clear();
            SetDataGridView(configInfo.FilterCopyIfNewStringList, filterCopyIfNew);
        }

        /// <summary>
        /// 結果出力設定をコントロールに反映
        /// </summary>
        /// <param name="configInfo"></param>
        private void SetOutputResult(ConfigInfo configInfo)
        {
            if (configInfo == null)
            {
                return;
            }
            outputResultConfig.SetOutputResultDefinitions(
                configInfo.OutputResultWindow, configInfo.OutputResultFile);
        }
        #endregion


    }
}
