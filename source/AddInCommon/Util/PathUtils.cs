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
using System.Text;
using System.Reflection;
using System.IO;

namespace AddInCommon.Util
{
    /// <summary>
    /// パスに関する共通処理を行う共通ユーティリティクラス
    /// </summary>
    public class PathUtils
    {
        /// <summary>
        /// 設定ファイルパスを返す
        /// </summary>
        /// <returns></returns>
        public static string GetConfigPath()
        {
            string assemblyFullName = Assembly.GetCallingAssembly().CodeBase;
            //  URI表記部分を削除して返す
            return Path.ChangeExtension(assemblyFullName.Replace("file:///", ""), ".config");
        }
    }
}
