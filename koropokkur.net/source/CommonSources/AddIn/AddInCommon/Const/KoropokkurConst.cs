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


namespace AddInCommon.Const
{
    /// <summary>
    /// Koropokkur.NET汎用定数クラス
    /// </summary>
    public sealed class KoropokkurConst
    {
        /// <summary>
        /// アドイン総称
        /// </summary>
        public const string NAME = "Koropokkur.NET";

        /// <summary>
        /// 各種設定メニュー名
        /// </summary>
        public const string CONFIG_MENU_NAME = "Koropokkurの設定";

        /// <summary>
        /// COMExceptionが発生した場合の待機時間(ms)
        /// </summary>
        public const int WAIT_TIME = 1000;

        /// <summary>
        /// COMExceptionが発生し続けた場合の最大やり直し回数
        /// </summary>
        public const int MAX_CONTINUE_TIMES = 50;
    }
}
