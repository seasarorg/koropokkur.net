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

using log4net;
using VSArrange.Const;
using VSArrange.Report;

namespace VSArrangeConsole.Report.Impl
{
    /// <summary>
    /// バッチ処理状況出力クラス
    /// </summary>
    public class BatchReport : IOutputReport
    {
        /// <summary>
        /// ログ出力
        /// </summary>
        private static readonly ILog _logger = LogManager.GetLogger(VSArrangeConst.ADDIN_NAME);

        public void ReportProgress(string message, int current, int total)
        {
            if (_logger.IsDebugEnabled)
            {
                _logger.DebugFormat("{0} [{1} / {2}]", message, current, total);
            }
        }

        public void ReportResult(string message)
        {
            if (_logger.IsInfoEnabled)
            {
                _logger.Info(message);
            }
        }

        public void ReportError(string message)
        {
            if (_logger.IsErrorEnabled)
            {
                _logger.Error(message);
            }
        }
    }
}
