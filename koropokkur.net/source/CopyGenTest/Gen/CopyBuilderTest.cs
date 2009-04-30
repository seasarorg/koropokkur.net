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
using System.IO;
using System.Reflection;
using AddInCommon.Util;
using CodeGeneratorCore;
using CodeGeneratorCore.Impl;
using CopyGen.Gen;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace CopyGenTest.Gen
{
    [TestFixture]
    public class CopyBuilderTest
    {
        private const string TARGET_ASSEMBLY = "CopyGenTest4Test.dll";
        private const string TARGET_METHOD_ExtractPropertyInfo = "ExtractPropertyInfo";
        private const string TARGET_METHOD_CreateCopyLinesGenerator = "CreateCopyLinesGenerator";
        private const string TARGET_METHOD_CreateCopyMethodGenerator = "CreateCopyMethodGenerator";
        
        private const string TARGET_CLASS = "CopyGenTest.Gen.TestResources.TestClass";

        #region ExtractPropertyInfo
        [Test]
        public void TestExtractPropertyInfo()
        {
            CopyBuilder copyBuilder = new CopyBuilder(new CopyInfo());
            MethodInfo targetMethod = copyBuilder.GetType().GetMethod(
                TARGET_METHOD_ExtractPropertyInfo, BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.That(targetMethod, Is.Not.Null);

            if(File.Exists(TARGET_ASSEMBLY))
            {
                File.Delete(TARGET_ASSEMBLY);
            }
            File.Copy("CopyGenTest.dll", TARGET_ASSEMBLY);
            object result = targetMethod.Invoke(copyBuilder, new object[]
                                                                 {
                                                                     PathUtils.GetFolderPath(
                                                                         AssemblyUtils.GetExecutingAssemblyPath())
                                                                         + TARGET_ASSEMBLY,
                                                                         TARGET_CLASS
                                                                 });
            List<string> resultList = result as List<string>;
            Assert.That(result, Is.Not.Null);

            //  出力されているはずのプロパティ名
            List<string> expectList = new List<string>();
            expectList.Add("NormalProperty1");
            expectList.Add("NormalProperty2");

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
            CopyBuilder copyBuilder = new CopyBuilder(new CopyInfo());
            MethodInfo targetMethod = copyBuilder.GetType().GetMethod(
                TARGET_METHOD_ExtractPropertyInfo, BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.That(targetMethod, Is.Not.Null);

            if (File.Exists(TARGET_ASSEMBLY))
            {
                File.Delete(TARGET_ASSEMBLY);
            }
            File.Copy("CopyGenTest.dll", TARGET_ASSEMBLY);
            object result = targetMethod.Invoke(copyBuilder, new object[]
                                                                 {
                                                                     PathUtils.GetFolderPath(
                                                                         AssemblyUtils.GetExecutingAssemblyPath())
                                                                         + TARGET_ASSEMBLY,
                                                                         NO_PUBLIC_PROPERTY_CLASS
                                                                 });
            List<string> resultList = result as List<string>;
            Assert.That(result, Is.Not.Null);
            Assert.That(resultList.Count, Is.EqualTo(0));
        }

        [Test]
        public void TestExtractPropertyInfo_NotExists()
        {
            const string NOT_EXIST_ASSEMBLY = "NotExists";
            CopyBuilder copyBuilder = new CopyBuilder(new CopyInfo());
            MethodInfo targetMethod = copyBuilder.GetType().GetMethod(
                TARGET_METHOD_ExtractPropertyInfo, BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.That(targetMethod, Is.Not.Null);

            if (File.Exists(NOT_EXIST_ASSEMBLY))
            {
                File.Delete(NOT_EXIST_ASSEMBLY);
            }

            object result = targetMethod.Invoke(copyBuilder, new object[]
                                                                 {
                                                                     PathUtils.GetFolderPath(
                                                                         AssemblyUtils.GetExecutingAssemblyPath())
                                                                         + NOT_EXIST_ASSEMBLY,
                                                                         TARGET_CLASS
                                                                 });
            Assert.That(result, Is.Null);
        }
        #endregion

        #region CreateCopyLinesGenerator

        [Test]
        public void TestCreateCopyLinesGenerator_PropertyNames_Null()
        {
            MethodInfo targetMethod = typeof(CopyBuilder).GetMethod(
                TARGET_METHOD_CreateCopyLinesGenerator, BindingFlags.Instance | BindingFlags.NonPublic);
            {
                CopyInfo copyInfo = new CopyInfo();
                Assert.That(copyInfo.SourcePropertyNames, Is.Null);
                Assert.That(copyInfo.TargetPropertyNames, Is.Null);
                CopyBuilder copyBuilder = new CopyBuilder(copyInfo);

                object result = targetMethod.Invoke(copyBuilder, new object[] { });
                Assert.That(result, Is.Null);
            }
            {
                CopyInfo copyInfo = new CopyInfo();
                copyInfo.SourcePropertyNames = new List<string>();
                Assert.That(copyInfo.TargetPropertyNames, Is.Null);
                CopyBuilder copyBuilder = new CopyBuilder(copyInfo);

                object result = targetMethod.Invoke(copyBuilder, new object[] { });
                Assert.That(result, Is.Null);
            }
            {
                CopyInfo copyInfo = new CopyInfo();
                Assert.That(copyInfo.SourcePropertyNames, Is.Null);
                copyInfo.TargetPropertyNames = new List<string>();
                CopyBuilder copyBuilder = new CopyBuilder(copyInfo);

                object result = targetMethod.Invoke(copyBuilder, new object[] { });
                Assert.That(result, Is.Null);
            }
        }

        [Test]
        public void TestCreateCopyLinesGenerator_PropertyNames_Zero()
        {
            MethodInfo targetMethod = typeof(CopyBuilder).GetMethod(
                TARGET_METHOD_CreateCopyLinesGenerator, BindingFlags.Instance | BindingFlags.NonPublic);
            {
                CopyInfo copyInfo = new CopyInfo();
                copyInfo.SourcePropertyNames = new List<string>();
                copyInfo.TargetPropertyNames = new List<string>();
                CopyBuilder copyBuilder = new CopyBuilder(copyInfo);

                object result = targetMethod.Invoke(copyBuilder, new object[] { });
                ICodeGenerator generator = result as ICodeGenerator;
                Assert.That(generator, Is.Not.Null);
                Assert.That(generator.GenerateCode("\t"), Is.EqualTo(""));
            }
            {
                CopyInfo copyInfo = new CopyInfo();
                copyInfo.SourcePropertyNames = new List<string>();
                copyInfo.SourcePropertyNames.Add("Test");
                copyInfo.TargetPropertyNames = new List<string>();
                CopyBuilder copyBuilder = new CopyBuilder(copyInfo);

                object result = targetMethod.Invoke(copyBuilder, new object[] { });
                ICodeGenerator generator = result as ICodeGenerator;
                Assert.That(generator, Is.Not.Null);
                Assert.That(generator.GenerateCode("\t"), Is.EqualTo(""));
            }
            {
                CopyInfo copyInfo = new CopyInfo();
                copyInfo.SourcePropertyNames = new List<string>();
                copyInfo.TargetPropertyNames = new List<string>();
                copyInfo.TargetPropertyNames.Add("Test");
                CopyBuilder copyBuilder = new CopyBuilder(copyInfo);

                object result = targetMethod.Invoke(copyBuilder, new object[] { });
                ICodeGenerator generator = result as ICodeGenerator;
                Assert.That(generator, Is.Not.Null);
                Assert.That(generator.GenerateCode("\t"), Is.EqualTo(""));
            }
        }

        [Test]
        public void TestCreateCopyLinesGenerator_HasSourceArgument()
        {
            const string TARGET_PROPERTY_NAME1 = "NormalProperty1";
            const string TARGET_PROPERTY_NAME2 = "NormalProperty2";
            const string DIFFERENT_PROPERTY_NAME1 = "DiffProp1";
            const string DIFFERENT_PROPERTY_NAME2 = "DiffProp2";
            MethodInfo targetMethod = typeof(CopyBuilder).GetMethod(
                TARGET_METHOD_CreateCopyLinesGenerator, BindingFlags.Instance | BindingFlags.NonPublic);
            {
                CopyInfo copyInfo = new CopyInfo();
                copyInfo.HasSourceArgument = true;
                copyInfo.SourcePropertyNames = new List<string>();
                copyInfo.SourcePropertyNames.Add(TARGET_PROPERTY_NAME1);
                copyInfo.SourcePropertyNames.Add(TARGET_PROPERTY_NAME2);
                copyInfo.SourcePropertyNames.Add(DIFFERENT_PROPERTY_NAME1);
                copyInfo.TargetPropertyNames = new List<string>();
                copyInfo.TargetPropertyNames.Add(TARGET_PROPERTY_NAME1);
                copyInfo.TargetPropertyNames.Add(TARGET_PROPERTY_NAME2);
                copyInfo.TargetPropertyNames.Add(DIFFERENT_PROPERTY_NAME2);
                CopyBuilder copyBuilder = new CopyBuilder(copyInfo);

                object result = targetMethod.Invoke(copyBuilder, new object[] { });
                ICodeGenerator generator = result as ICodeGenerator;
                Assert.That(generator, Is.Not.Null);
                Assert.That(generator.GenerateCode("\t"), Is.EqualTo(
                    string.Format("\ttarget.{0} = source.{0};{1}\ttarget.{2} = source.{2};{1}",
                        TARGET_PROPERTY_NAME1, Environment.NewLine, TARGET_PROPERTY_NAME2)));
            }
        }

        [Test]
        public void TestCreateCopyLinesGenerator_NotHasSourceArgument()
        {
            const string TARGET_PROPERTY_NAME1 = "NormalProperty1";
            const string TARGET_PROPERTY_NAME2 = "NormalProperty2";
            const string DIFFERENT_PROPERTY_NAME1 = "DiffProp1";
            const string DIFFERENT_PROPERTY_NAME2 = "DiffProp2";
            MethodInfo targetMethod = typeof(CopyBuilder).GetMethod(
                TARGET_METHOD_CreateCopyLinesGenerator, BindingFlags.Instance | BindingFlags.NonPublic);
            {
                CopyInfo copyInfo = new CopyInfo();
                copyInfo.HasSourceArgument = false;
                copyInfo.SourcePropertyNames = new List<string>();
                copyInfo.SourcePropertyNames.Add(TARGET_PROPERTY_NAME1);
                copyInfo.SourcePropertyNames.Add(TARGET_PROPERTY_NAME2);
                copyInfo.SourcePropertyNames.Add(DIFFERENT_PROPERTY_NAME1);
                copyInfo.TargetPropertyNames = new List<string>();
                copyInfo.TargetPropertyNames.Add(TARGET_PROPERTY_NAME1);
                copyInfo.TargetPropertyNames.Add(TARGET_PROPERTY_NAME2);
                copyInfo.TargetPropertyNames.Add(DIFFERENT_PROPERTY_NAME2);
                CopyBuilder copyBuilder = new CopyBuilder(copyInfo);

                object result = targetMethod.Invoke(copyBuilder, new object[] { });
                ICodeGenerator generator = result as ICodeGenerator;
                Assert.That(generator, Is.Not.Null);
                Assert.That(generator.GenerateCode("\t"), Is.EqualTo(
                    string.Format("\ttarget.{0} = this.{0};{1}\ttarget.{2} = this.{2};{1}", 
                    TARGET_PROPERTY_NAME1, Environment.NewLine, TARGET_PROPERTY_NAME2)));
            }
        }

        #endregion

        #region CreateCopyMethodGenerator

        [Test]
        public void TestCreateCopyMethodGenerator_HasSourceArgument()
        {
            MethodInfo targetMethod = typeof(CopyBuilder).GetMethod(
                TARGET_METHOD_CreateCopyMethodGenerator, BindingFlags.Instance | BindingFlags.NonPublic);
            {
                CopyInfo copyInfo = new CopyInfo();
                copyInfo.HasSourceArgument = false;
                copyInfo.IsReturn = true;
                CopyBuilder copyBuilder = new CopyBuilder(copyInfo);

                object result = targetMethod.Invoke(copyBuilder, new object[] {});
                MethodGenerator generator = result as MethodGenerator;
                Assert.That(generator, Is.Not.Null);
                Assert.That(generator.Arguments.Count, Is.EqualTo(0));
            }
            {
                CopyInfo copyInfo = new CopyInfo();
                copyInfo.HasSourceArgument = true;
                copyInfo.IsReturn = true;
                copyInfo.SourceTypeName = "int";
                copyInfo.SourceArgumentName = "source";
                CopyBuilder copyBuilder = new CopyBuilder(copyInfo);

                object result = targetMethod.Invoke(copyBuilder, new object[] { });
                MethodGenerator generator = result as MethodGenerator;
                Assert.That(generator, Is.Not.Null);
                Assert.That(generator.Arguments.Count, Is.EqualTo(1));
                ArgumentGenerator argumentGenerator = generator.Arguments[0];
                Assert.That(argumentGenerator.Comment, Is.EqualTo("コピー元"));
                Assert.That(argumentGenerator.ArgumentTypeName, Is.EqualTo(copyInfo.SourceTypeName));
                Assert.That(argumentGenerator.ArgumentName, Is.EqualTo(copyInfo.SourceArgumentName));
            }
        }

        [Test]
        public void TestCreateCopyMethodGenerator_IsReturn()
        {
            MethodInfo targetMethod = typeof(CopyBuilder).GetMethod(
                TARGET_METHOD_CreateCopyMethodGenerator, BindingFlags.Instance | BindingFlags.NonPublic);
            {
                CopyInfo copyInfo = new CopyInfo();
                copyInfo.HasSourceArgument = false;
                copyInfo.IsReturn = true;
                copyInfo.TargetTypeName = "TestClass";
                CopyBuilder copyBuilder = new CopyBuilder(copyInfo);

                object result = targetMethod.Invoke(copyBuilder, new object[] { });
                MethodGenerator generator = result as MethodGenerator;
                Assert.That(generator, Is.Not.Null);
                Assert.That(generator.Arguments.Count, Is.EqualTo(0));

                Assert.That(generator.Lines.Count, Is.EqualTo(3));
                Assert.That(generator.Lines.MoveNext(), Is.True);
                Assert.That(generator.Lines.Current.GenerateCode("\t"), Is.EqualTo(
                    "\tTestClass target = new TestClass();"));
                Assert.That(generator.Lines.MoveNext(), Is.True);
                Assert.That(generator.Lines.Current, Is.Null);
                Assert.That(generator.Lines.MoveNext(), Is.True);
                Assert.That(generator.Lines.Current.GenerateCode("\t\t"), Is.EqualTo(
                    "\t\treturn target;"));
            }
            {
                CopyInfo copyInfo = new CopyInfo();
                copyInfo.HasSourceArgument = false;
                copyInfo.IsReturn = false;
                copyInfo.TargetTypeName = "TestClass";
                copyInfo.TargetArgumentName = "target";
                CopyBuilder copyBuilder = new CopyBuilder(copyInfo);

                object result = targetMethod.Invoke(copyBuilder, new object[] { });
                MethodGenerator generator = result as MethodGenerator;
                Assert.That(generator, Is.Not.Null);
                Assert.That(generator.Arguments.Count, Is.EqualTo(1));
                Assert.That(generator.Arguments[0].ArgumentName, Is.EqualTo(copyInfo.TargetArgumentName));
                Assert.That(generator.Arguments[0].ArgumentTypeName, Is.EqualTo(copyInfo.TargetTypeName));

                Assert.That(generator.Lines.Count, Is.EqualTo(2));
                Assert.That(generator.Lines.MoveNext(), Is.True);
                Assert.That(generator.Lines.Current, Is.Null);
                Assert.That(generator.Lines.MoveNext(), Is.True);
                Assert.That(generator.Lines.Current.GenerateCode("\t\t"), Is.EqualTo("\t\treturn;"));
            }
        }

        #endregion

        #region CreateCodeGenerator

        [Test]
        public void TestCreateCodeGenerator_IsOutputMethod()
        {
            {
                CopyInfo copyInfo = new CopyInfo();
                copyInfo.IsOutputMethod = false;
                copyInfo.SourcePropertyNames = new List<string>(new string[] { "NormalProperty1", "NormalProperty2" });
                copyInfo.SourcePropertyNames = new List<string>(new string[] { "NormalProperty1", "NormalProperty2" });
                copyInfo.IsReturn = true;
                copyInfo.HasSourceArgument = false;
                copyInfo.MethodName = "Hoge";
                CopyBuilder copyBuilder = new CopyBuilder(copyInfo);

                ICodeGenerator generator = copyBuilder.CreateCodeGenerator(TARGET_ASSEMBLY, TARGET_CLASS, TARGET_CLASS);
                GeneratorColleciton generatorColleciton = generator as GeneratorColleciton;
                Assert.That(generatorColleciton, Is.Not.Null);
                Assert.That(generator.GenerateCode(null), Is.EqualTo(
                    "target.NormalProperty1 = this.NormalProperty1;" + Environment.NewLine
                    + "target.NormalProperty2 = this.NormalProperty2;" + Environment.NewLine));
            }
            {
                CopyInfo copyInfo = new CopyInfo();
                copyInfo.IsOutputMethod = true;
                copyInfo.SourcePropertyNames = new List<string>(new string[] { "NormalProperty1", "NormalProperty2" });
                copyInfo.SourcePropertyNames = new List<string>(new string[] { "NormalProperty1", "NormalProperty2" });
                copyInfo.IsReturn = false;
                copyInfo.HasSourceArgument = true;
                copyInfo.MethodName = "Hoge";
                CopyBuilder copyBuilder = new CopyBuilder(copyInfo);

                ICodeGenerator generator = copyBuilder.CreateCodeGenerator(TARGET_ASSEMBLY, TARGET_CLASS, TARGET_CLASS);
                MethodGenerator methodGenerator = generator as MethodGenerator;
                Assert.That(methodGenerator, Is.Not.Null);
                Assert.That(methodGenerator.Arguments.Count, Is.EqualTo(2));
                Assert.That(methodGenerator.Lines.Count, Is.EqualTo(2));
                Assert.That(methodGenerator.ReturnTypeName, Is.EqualTo("void"));
            }
        }

        [Test]
        public void TestCreateCodeGenerator_NotExistAssembly()
        {
            CopyBuilder copyBuilder = new CopyBuilder(new CopyInfo());
            object result = copyBuilder.CreateCodeGenerator("NotExists", "Hoge", "Huga");
            Assert.That(result, Is.Null);
        }

        [Test]
        public void TestCreateCodeGenerator_NotExistType()
        {
            CopyBuilder copyBuilder = new CopyBuilder(new CopyInfo());
            object result = copyBuilder.CreateCodeGenerator(TARGET_ASSEMBLY, "Hoge", "Huga");
            Assert.That(result, Is.Null);
        }

        #endregion
    }
}
