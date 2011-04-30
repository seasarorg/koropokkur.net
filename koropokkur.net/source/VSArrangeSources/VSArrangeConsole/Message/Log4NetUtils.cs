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
using System.Collections.Generic;
using System.Text;
using log4net;
using VSArrange.Const;

namespace VSArrangeConsole.Message
{
    /// <summary>
    /// log4net用ユーティリティクラス
    /// </summary>
    public static class Log4NetUtils
    {
        /// <summary>
        /// ログ出力
        /// </summary>
        private static readonly ILog _logger = LogManager.GetLogger(VSArrangeConst.ADDIN_NAME);

        public static void DebugIfEnable(string message)
        {
            if (_logger.IsDebugEnabled)
            {
                _logger.Debug(message);
            }
        }

        public static void InfoIfEnable(string message)
        {
            if (_logger.IsInfoEnabled)
            {
                _logger.Info(message);
            }
        }

        public static void WarnIfEnable(string message)
        {
            if (_logger.IsWarnEnabled)
            {
                _logger.Warn(message);
            }
        }

        public static void ErrorIfEnable(string message)
        {
            if (_logger.IsErrorEnabled)
            {
                _logger.Error(message);
            }
        }

    }
}
