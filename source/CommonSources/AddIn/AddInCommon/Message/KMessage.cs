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

namespace AddInCommon.Message
{
    /// <summary>
    /// Koropokkur共通メッセージ
    /// </summary>
    public class KMessage
    {
        /// <summary>
        /// Koropokkur.NETがサポートしていない形式のファイルを指定されたときに返すメッセージ
        /// </summary>
        /// <param name="addInName">アドイン名</param>
        /// <param name="fileName">ファイル名</param>
        /// <param name="enables">使用可能な形式</param>
        /// <returns></returns>
        public static string GetNotSupportExtension(string addInName, string fileName, string enables)
        {
            return string.Format(
                "[{0}]は[{1}]で対応していない形式のファイルです。(使用可能：{2})",
                fileName, addInName, enables);
        }

        /// <summary>
        /// メンバー情報を取得できなかったことを知らせるメッセージの取得
        /// </summary>
        /// <param name="type"></param>
        /// <param name="memberName"></param>
        /// <returns></returns>
        public static string GetNoMember(Type type, string memberName)
        {
            return string.Format("{0}には「{1}」は定義されていません。", type.Name, memberName);
        }
    }
}
