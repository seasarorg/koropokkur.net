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
using System.Collections.Generic;
using System.Reflection;
using AddInCommon.Util;
using CopyGen.Gen;
using CopyGen.Util;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace CopyGenTest.Util
{
    [TestFixture]
    public class PropertyCodeInfoUtilsTest
    {
        private const string TARGET_ASSEMBLY = "CopyGenTest.dll";
        private const string TARGET_CLASS = "CopyGenTest.Gen.TestResources.TestClass";

        #region ExtractPropertyInfo
        [Test]
        public void TestExtractPropertyInfo()
        {
            PropertyCodeInfo propertyCodeInfo = CodeInfoUtils.ReadPropertyInfo(
                PathUtils.GetFolderPath(AssemblyUtils.GetExecutingAssemblyPath()) + TARGET_ASSEMBLY,
                TARGET_CLASS, TARGET_CLASS);

            Assert.That(propertyCodeInfo, Is.Not.Null);

            //  出力されているはずのプロパティ名
            List<string> expectList = new List<string>();
            expectList.Add("NormalProperty1");
            expectList.Add("NormalProperty2");

            IList<string> resultList = propertyCodeInfo.SourcePropertyNames;
            Assert.That(resultList.Count, Is.EqualTo(expectList.Count));
            foreach (string s in resultList)
            {
                Assert.That(expectList.Contains(s), Is.True, s);
                expectList.Remove(s);
            }
        }

        [Test]
        public void TestExtractPropertyInfo_NoProperty()
        {
            const string NO_PUBLIC_PROPERTY_CLASS = "CopyGenTest.Gen.TestResources.NoPublicPropertyClass";
            PropertyCodeInfo propertyCodeInfo = CodeInfoUtils.ReadPropertyInfo(
                PathUtils.GetFolderPath(AssemblyUtils.GetExecutingAssemblyPath()) + TARGET_ASSEMBLY,
                NO_PUBLIC_PROPERTY_CLASS, NO_PUBLIC_PROPERTY_CLASS);

            Assert.That(propertyCodeInfo, Is.Not.Null);

            IList<string> resultList = propertyCodeInfo.SourcePropertyNames;
            Assert.That(resultList, Is.Not.Null);
            Assert.That(resultList.Count, Is.EqualTo(0));
        }

        [Test]
        public void TestExtractPropertyInfo_NotExists()
        {
            const string NOT_EXIST_ASSEMBLY = "NotExists";
            PropertyCodeInfo propertyCodeInfo = CodeInfoUtils.ReadPropertyInfo(
                PathUtils.GetFolderPath(AssemblyUtils.GetExecutingAssemblyPath()) + NOT_EXIST_ASSEMBLY,
                TARGET_CLASS, TARGET_CLASS);

            Assert.That(propertyCodeInfo, Is.Not.Null);
            Assert.That(propertyCodeInfo.SourcePropertyNames, Is.Null);
        }
        #endregion

        [Test]
        public void TestReadVbClass()
        {
            Assembly assembly = Assembly.LoadFrom(@"C:\source\seasar\koropokkur\source\CopyGenTest\VBProjectForTest.exe");
            Type[] types = assembly.GetTypes();
            Type directType = assembly.GetType("Class2");
            Console.WriteLine(directType == null ? "null" : directType.Name);
            foreach (var type in types)
            {
                Console.WriteLine(type.Name);
            }
        }
    }
}
