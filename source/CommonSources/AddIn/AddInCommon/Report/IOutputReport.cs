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

namespace AddInCommon.Report
{
    /// <summary>
    /// メッセージ出力インターフェース
    /// （プロジェクト整理処理の情報を出力）
    /// </summary>
    public interface IOutputReport
    {
        /// <summary>
        /// 処理の進捗状況を通知
        /// </summary>
        /// <param name="message"></param>
        /// <param name="current">現在値</param>
        /// <param name="total">最大値</param>
        void ReportProgress(string message, int current, int total);

        /// <summary>
        /// サブ処理の進捗状況を通知
        /// </summary>
        /// <param name="message"></param>
        /// <param name="current">現在値</param>
        /// <param name="total">最大値</param>
        void ReportSubProgress(string message, int current, int total);

        /// <summary>
        /// その他メッセージを通知
        /// </summary>
        /// <param name="message"></param>
        void Report(string message);

        /// <summary>
        /// 処理結果を通知
        /// </summary>
        /// <param name="message"></param>
        void ReportResult(string message);

        /// <summary>
        /// 警告通知
        /// </summary>
        /// <param name="message"></param>
        void ReportWarning(string message);

        /// <summary>
        /// エラー通知
        /// </summary>
        /// <param name="message"></param>
        void ReportError(string message);
    }
}
