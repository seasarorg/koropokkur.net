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
using VSArrange.Const;

namespace VSArrangeConsole.Message
{
    /// <summary>
    /// VSArrangeコンソール用メッセージ
    /// </summary>
    public class VSArrangeConsoleMessage
    {
        /// <summary>
        /// 引数が指定されずにプログラムが実行されたときのメッセージ
        /// </summary>
        /// <returns></returns>
        public static string GetNoArgumentMessage()
        {
            return "引数が指定されていません。USAGE:[0](必須)=処理対象ファイルパス, [1](任意)=設定ファイルパス, [2](任意)処理対象プロジェクト名";
        }

        /// <summary>
        /// プロジェクトファイルパスが見つからないメッセージ
        /// </summary>
        /// <param name="basePath"></param>
        /// <returns></returns>
        public static string GetNotExistsProject(string basePath)
        {
            return string.Format("[{0}]下にプロジェクトファイルのパスが見つかりません。",
                basePath);
        }

        /// <summary>
        /// プログラム開始メッセージ
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string GetStartMessage(string[] args)
        {
            return string.Format("{0}：開始  (ARGS=[{1}])", VSArrangeConst.ADDIN_NAME, string.Join(",", args));
        }

        /// <summary>
        /// プログラム終了メッセージ
        /// </summary>
        /// <param name="startTime">処理開始時刻</param>
        /// <param name="endTime">処理終了時刻</param>
        /// <returns></returns>
        public static string GetEndMessage(DateTime startTime, DateTime endTime)
        {
            return string.Format("{0}：終了 （所要時間：{1})", VSArrangeConst.ADDIN_NAME, 
                (endTime - startTime).ToString().Substring(0, 11));
        }
    }
}
