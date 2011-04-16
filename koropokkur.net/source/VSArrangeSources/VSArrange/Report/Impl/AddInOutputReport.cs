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

using System.IO;
using AddInCommon.Util;
using EnvDTE80;
using VSArrange.Config;
using VSArrange.Control;
using VSArrange.Util;

namespace VSArrange.Report.Impl
{
    /// <summary>
    /// アドイン用処理内容出力クラス
    /// </summary>
    public class AddInOutputReport : IOutputReport
    {
        /// <summary>
        /// 設定情報
        /// </summary>
        private readonly ConfigInfo _configInfo;

        /// <summary>
        /// VisualStudio操作オブジェクト
        /// </summary>
        private readonly DTE2 _applicationObject;

        /// <summary>
        /// プロジェクト名
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="configInfo"></param>
        public AddInOutputReport(ConfigInfo configInfo, DTE2 applicationObject)
        {
            _configInfo = configInfo;
            _applicationObject = applicationObject;
        }

        /// <summary>
        /// 進捗メッセージの消去
        /// </summary>
        public void Clear()
        {
            StatusBarUtils.Clear(_applicationObject);
        }

        #region IOutputReport

        public void ReportProgress(string message, int current, int total)
        {
            _applicationObject.StatusBar.Progress(true, message, current, total);
        }

        public void ReportResult(string message)
        {
            if (_configInfo.OutputResultWindow.IsEnable)
            {
                using (var resultMessageForm = new ResultMessageForm())
                {
                    resultMessageForm.SetResultList(message);
                    resultMessageForm.ShowDialog();
                }
            }

            if (_configInfo.OutputResultFile.IsEnable)
            {
                var outputPath = ArrangeUtils.GetOutputPath(_configInfo, ProjectName);
                using (var writer = new StreamWriter(outputPath, true))
                {
                    writer.WriteLine(message);
                    writer.Flush();
                }
            }
        }

        public void ReportError(string message)
        {
            MessageUtils.ShowErrorMessage(message);

            if (_configInfo.OutputResultFile.IsEnable)
            {
                var outputPath = ArrangeUtils.GetOutputPath(_configInfo, ProjectName);
                using (var writer = new StreamWriter(outputPath, true))
                {
                    writer.WriteLine(message);
                    writer.Flush();
                }
            }
        }

        #endregion
    }
}
