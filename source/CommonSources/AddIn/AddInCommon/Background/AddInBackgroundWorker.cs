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
using System.ComponentModel;
using AddInCommon.Util;
using EnvDTE80;

namespace AddInCommon.Background
{
    /// <summary>
    /// アドイン用バックグラウンド処理クラス
    /// </summary>
    public class AddInBackgroundWorker : BackgroundWorker
    {
        /// <summary>
        /// 百分率最大値
        /// </summary>
        private const int MAX_PERCENTAGE = 100;

        /// <summary>
        /// VisualStudio操作オブジェクト
        /// </summary>
        private readonly DTE2 _applicationObject;

        /// <summary>
        /// 進捗名
        /// </summary>
        private string _progressName;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="applicationObject"></param>
        public AddInBackgroundWorker(DTE2 applicationObject)
        {
            if (applicationObject == null) throw new ArgumentNullException("applicationObject");
            _applicationObject = applicationObject;
            WorkerReportsProgress = true;
            ProgressChanged += AddInBackgroundWorker_ProgressChanged;
        }

        /// <summary>
        /// 進捗更新
        /// </summary>
        /// <param name="progressName"></param>
        /// <param name="amount"></param>
        /// <param name="total"></param>
        public void ReportProgress(string progressName, int amount, int total)
        {
            _progressName = progressName;
            //  進捗バー設定用なので精度にはこだわらない
            int percentage = (total == 0 ? 0 : (amount*100)/total);
            ReportProgress(percentage);
        }

        /// <summary>
        /// 表示内容のクリア
        /// </summary>
        public void ResetDisplay()
        {
            StatusBarUtils.Clear(_applicationObject);
        }

        /// <summary>
        /// 進捗更新イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddInBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            _applicationObject.StatusBar.Progress(
                true, _progressName, e.ProgressPercentage, MAX_PERCENTAGE);
        }
    }
}
