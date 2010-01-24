#region Copyright
/*
 * Copyright 2005-2010 the Seasar Foundation and the Others.
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

using EnvDTE;
using EnvDTE80;

namespace AddInCommon.Command
{
    /// <summary>
    /// アドインで発生したイベントを処理するインターフェース
    /// </summary>
    public interface IDTCExecCommand
    {
        /// <summary>
        /// 処理名称
        /// </summary>
        /// <remarks>他の処理名称と被らない名前にする</remarks>
        string CommandName { get; }

        /// <summary>
        /// 表示用処理名称
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// ツールチップ表示内容
        /// </summary>
        string ToolTipText { get; }

        /// <summary>
        /// イベント有効無効判定
        /// </summary>
        /// <param name="applicationObject"></param>
        /// <param name="addInInstance"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        vsCommandStatus GetCommandStatus(DTE2 applicationObject, AddIn addInInstance, ref object commandText);

        /// <summary>
        /// 処理実行
        /// </summary>
        /// <param name="applicationObject">起動中のVisualStudioを構成する情報</param>
        /// <param name="addInInstance">実行中のアドインのインスタンス</param>
        /// <param name="varIn">イベント入力値</param>
        /// <param name="varOut">イベント出力値</param>
        /// <returns>処理正常終了フラグ</returns>
        bool Execute(DTE2 applicationObject, AddIn addInInstance,
                     ref object varIn, ref object varOut);
    }
}
