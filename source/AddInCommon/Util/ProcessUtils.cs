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

using System;
using System.Diagnostics;

namespace AddInCommon.Util
{
    /// <summary>
    /// プロセスを取り扱うためのユーティリティクラス
    /// </summary>
    public class ProcessUtils
    {
        /// <summary>
        /// プロセスをウインドウを出さずに起動し、処理が終了するまで待つ
        /// </summary>
        /// <param name="exePath"></param>
        /// <param name="arguments"></param>
        public static void StartProcessWithoutWindow(string exePath, string arguments)
        {
            StartProcessWithoutWindow(exePath, arguments, true);
        }

        /// <summary>
        /// プロセスをウインドウを出さずに起動
        /// </summary>
        /// <param name="exePath"></param>
        /// <param name="arguments"></param>
        /// <param name="isWaitForExit"></param>
        public static void StartProcessWithoutWindow(string exePath, string arguments, bool isWaitForExit)
        {
            Process p = new Process();

            p.StartInfo = new ProcessStartInfo(exePath);
            p.StartInfo.Arguments = arguments;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.UseShellExecute = false;
            p.Start();
            if(isWaitForExit)
            {
                p.WaitForExit();
            }
        }
    }
}
