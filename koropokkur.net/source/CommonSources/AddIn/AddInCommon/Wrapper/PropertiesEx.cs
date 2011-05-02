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

using System.Collections;
using AddInCommon.Util;
using EnvDTE;

namespace AddInCommon.Wrapper
{
    /// <summary>
    /// Propertiesを扱う処理にCOMExceptionのハンドリングを付加するクラス
    /// </summary>
    public class PropertiesEx : Properties
    {
        private Properties _properties;

        /// <summary>
        /// COMオブジェクト:Propertiesのセット
        /// </summary>
        /// <param name="properties"></param>
        public void SetProperties(Properties properties)
        {
            _properties = properties;
        }

        /// <summary>
        /// プロパティの取得処理委譲
        /// </summary>
        /// <typeparam name="R"></typeparam>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private R InvokeGetValue<R>(string propertyName)
        {
            return COMExceptionInvokeUtils.InvokeGetter<Properties, R>(_properties, propertyName);
        }

        #region Properties Methods

        public object Application
        {
            get { return InvokeGetValue<object>("Application"); }
        }

        public int Count
        {
            get { return InvokeGetValue<int>("Count"); }
        }

        public DTE DTE
        {
            get { return InvokeGetValue<DTE>("DTE"); }
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return COMExceptionInvokeUtils.InvokeMethod<Properties, IEnumerator>(_properties, "GetEnumerator", null);
        }

        public Property Item(object index)
        {
            return COMExceptionInvokeUtils.InvokeMethod<Properties, Property>(_properties, "Item", new object[] { index });
        }

        public object Parent
        {
            get { return InvokeGetValue<object>("Parent"); }
        }

        #endregion


    }
}
