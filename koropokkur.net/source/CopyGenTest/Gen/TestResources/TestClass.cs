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

namespace CopyGenTest.Gen.TestResources
{
    public class TestClass
    {
        private int PrivateProperty { get; set; }
        protected int ProtectedProperty { get; set; }

        private string _setterOnlyProperty1;
        public string SetterOnlyProperty1
        {
            set { _setterOnlyProperty1 = value; }
        }

        public string SetterOnlyProperty2 { private get; set; }

        private const string _getterOnlyProperty1 = "hoge";
        public string GetterOnlyProperty1 { get { return _getterOnlyProperty1; } }

        public string GetterOnlyProperty2 { get; private set; }

        private DateTime _normalProperty1 = DateTime.Now;
        public DateTime NormalProperty1 { get { return _normalProperty1; } set { _normalProperty1 = value; } }

        public DateTime NormalProperty2 { get; set; }
    }
}
