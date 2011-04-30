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

using VSArrange.Report;
using VSArrangeConsole.Message;

namespace VSArrangeConsole.Report.Impl
{
    /// <summary>
    /// バッチ処理状況出力クラス
    /// </summary>
    public class BatchReport : IOutputReport
    {
        public void ReportProgress(string message, int current, int total)
        {
            Log4NetUtils.DebugIfEnable(string.Format(
                "{0}\t【{1} / {2}】", message, current, total));
        }

        public void ReportSubProgress(string message, int current, int total)
        {
            Log4NetUtils.DebugIfEnable(string.Format(
                "{0}\t\t<{1} / {2}>", message, current.ToString("00"), total.ToString("00")));
        }

        public void Report(string message)
        {
            Log4NetUtils.DebugIfEnable(message);
        }

        public void ReportResult(string message)
        {
            Log4NetUtils.InfoIfEnable(string.Format("\n{0}", message));
        }

        public void ReportWarning(string message)
        {
            Log4NetUtils.WarnIfEnable(message);
        }

        public void ReportError(string message)
        {
            Log4NetUtils.ErrorIfEnable(message);
        }

    }
}
