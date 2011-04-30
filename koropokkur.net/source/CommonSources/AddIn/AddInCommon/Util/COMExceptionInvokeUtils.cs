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
using System.Reflection;
using System.Runtime.InteropServices;
using AddInCommon.Const;
using AddInCommon.Message;

namespace AddInCommon.Util
{
    /// <summary>
    /// COMException対応委譲ユーティリティクラス
    /// </summary>
    public static class COMExceptionInvokeUtils
    {
        /// <summary>
        /// プロパティGetの委譲処理
        /// </summary>
        /// <typeparam name="T">委譲元クラス</typeparam>
        /// <typeparam name="R">戻り値の型</typeparam>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static R InvokeGetter<T, R>(object obj, string propertyName)
        {
            var type = typeof(T);
            var getter = type.GetProperty(propertyName);
            if (getter == null)
            {
                throw new ArgumentException(KMessage.GetNoMember(type, propertyName));
            }

            int continueCount = 0;
            while (true)
            {
                try
                {
                    return (R)getter.GetValue(obj, null);
                }
                catch (TargetInvocationException te)
                {
                    if(ShouldReThrow(te, continueCount))
                    {
                        throw;
                    }
                }
                continueCount++;
            }
        }

        /// <summary>
        /// プロパティSetの委譲処理
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public static void InvokeSetter<T>(object obj, string propertyName, object value)
        {
            var type = typeof(T);
            var setter = type.GetProperty(propertyName);
            if (setter == null)
            {
                throw new ArgumentException(KMessage.GetNoMember(type, propertyName));
            }

            int continueCount = 0;
            while (true)
            {
                try
                {
                    setter.SetValue(obj, value, null);
                    return;
                }
                catch (TargetInvocationException te)
                {
                    if(ShouldReThrow(te, continueCount))
                    {
                        throw;
                    }
                }
                continueCount++;
            }
        }

        /// <summary>
        /// 戻り値があるメソッドの委譲処理
        /// </summary>
        /// <typeparam name="R"></typeparam>
        /// <param name="obj"></param>
        /// <param name="methodName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static R InvokeMethod<T, R>(object obj, string methodName, object[] parameters)
        {
            var type = typeof(T);
            var method = type.GetMethod(methodName);

            if (method == null)
            {
                throw new ArgumentException(KMessage.GetNoMember(type, methodName));
            }

            int continueCount = 0;
            while (true)
            {
                try
                {
                    return (R)method.Invoke(obj, parameters);
                }
                catch (TargetInvocationException te)
                {
                    if(ShouldReThrow(te, continueCount))
                    {
                        throw;
                    }
                }
                continueCount++;
            }
        }

        /// <summary>
        /// 戻り値を返さないメソッドの委譲処理
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="methodName"></param>
        /// <param name="parameters"></param>
        public static void InvokeNoRetMethod<T>(object obj, string methodName, object[] parameters)
        {
            var type = typeof(T);
            var method = type.GetMethod(methodName);
            if (method == null)
            {
                throw new ArgumentException(KMessage.GetNoMember(type, methodName));
            }

            int continueCount = 0;
            while (true)
            {
                try
                {
                    method.Invoke(obj, parameters);
                    return;
                }
                catch (TargetInvocationException te)
                {
                    if(ShouldReThrow(te, continueCount))
                    {
                        throw;
                    }
                }
                continueCount++;
            }
        }

        /// <summary>
        /// 再実行を要求する例外か判定する
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        private static bool IsRetryLaterEx(System.Exception ex)
        {
            const string MSG_RETRY_LATER = "RPC_E_SERVERCALL_RETRYLATER";
            var currentEx = ex;
            while (currentEx != null)
            {
                if (currentEx is COMException &&
                    currentEx.Message.Contains(MSG_RETRY_LATER))
                {
                    return true;
                }
                currentEx = currentEx.InnerException;
            }
            return false;
        }

        /// <summary>
        /// 例外を返すか判定
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="continueCount">現リトライ回数</param>
        /// <returns>true:例外を返す, false:リトライ</returns>
        private static bool ShouldReThrow(System.Exception ex, int continueCount)
        {
            if (IsRetryLaterEx(ex))
            {
                if (continueCount > KoropokkurConst.MAX_CONTINUE_TIMES)
                {
                    return true;
                }
                System.Threading.Thread.Sleep(KoropokkurConst.WAIT_TIME);
                return false;
            }
            return true;
        }
    }
}
