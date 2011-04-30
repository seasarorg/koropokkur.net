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
using System.Runtime.InteropServices;
using AddInCommon.Util;
using EnvDTE;

namespace AddInCommon.Wrapper
{
    /// <summary>
    /// Propertyを扱う処理にCOMExceptionのハンドリングを付加するクラス
    /// </summary>
    public class PropertyEx : Property
    {
        private Property _property;

        public void SetProperty(Property property)
        {
            _property = property;
        }

        /// <summary>
        /// プロパティの取得処理委譲
        /// </summary>
        /// <typeparam name="R"></typeparam>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private R InvokeGetValue<R>(string propertyName)
        {
            return COMExceptionInvokeUtils.InvokeGetter<Property, R>(_property, propertyName);
        }

        #region Property Methods
        public object Application
        {
            get { return InvokeGetValue<object>("Application"); }
        }

        public Properties Collection
        {
            get { return InvokeGetValue<Properties>("Collection"); }
        }

        public DTE DTE
        {
            get { return InvokeGetValue<DTE>("DTE"); }
        }

        public string Name
        {
            get { return InvokeGetValue<string>("Name"); }
        }

        public short NumIndices
        {
            get { return InvokeGetValue<short>("NumIndices"); }
        }

        public object Object
        {
            get { return InvokeGetValue<object>("Object"); }
            set { COMExceptionInvokeUtils.InvokeSetter<Property>(_property, "Object", value); }
        }

        public Properties Parent
        {
            get { return InvokeGetValue<Properties>("Parent"); }
        }

        public object Value
        {
            get { return InvokeGetValue<object>("Value"); }
            set { COMExceptionInvokeUtils.InvokeSetter<Property>(_property, "Value", value); }
        }

        public object get_IndexedValue(object Index1, [System.Runtime.InteropServices.OptionalAttribute]object Index2, [System.Runtime.InteropServices.OptionalAttribute]object Index3, [System.Runtime.InteropServices.OptionalAttribute]object Index4)
        {
            return COMExceptionInvokeUtils.InvokeMethod<Property, object>(_property, "get_IndexedValue", new object[] { Index1, Index2, Index3, Index4 });
        }

        public void let_Value(object lppvReturn)
        {
            COMExceptionInvokeUtils.InvokeNoRetMethod<Property>(_property, "let_Value", new object[] { lppvReturn });
        }

        public void set_IndexedValue(object Index1, [System.Runtime.InteropServices.OptionalAttribute]object Index2, [System.Runtime.InteropServices.OptionalAttribute]object Index3, [System.Runtime.InteropServices.OptionalAttribute]object Index4, object Val)
        {
            COMExceptionInvokeUtils.InvokeNoRetMethod<Property>(_property, "set_IndexedValue", new object[] { Index1, Index2, Index3, Index4, Val });
        }
        #endregion
    }
}
