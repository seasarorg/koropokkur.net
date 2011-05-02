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

using AddInCommon.Message;

namespace AddInCommon.Exception
{
    /// <summary>
    /// Koropokkur.NETがサポートしていないファイルを指定されたときの例外
    /// </summary>
    public class NotSupportedFileException : KoropokkurException
    {
        private readonly string _addInName;
        private readonly string _fileName;
        private readonly string _enables;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="addInName">アドイン名</param>
        /// <param name="fileName">ファイル名</param>
        /// <param name="enables">使用可能な形式</param>
        public NotSupportedFileException(string addInName, string fileName, string enables)
        {
            _addInName = addInName;
            _fileName = fileName;
            _enables = enables;
        }

        public override string Message
        {
            get
            {
                return KMessage.GetNotSupportExtension(_addInName, _fileName, _enables);
            }
        }

        public override string ToString()
        {
            return string.Format("{0}\n{1}", Message, StackTrace);
        }
    }
}
