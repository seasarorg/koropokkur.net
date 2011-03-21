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
using System.Reflection;
using CodeGeneratorCore;
using CodeGeneratorCore.Impl;
using CodeGeneratorCore.Impl.Cs;
using CopyGen.Gen;
using CopyGen.Gen.Impl.Cs;
using CopyGen.Util;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace CopyGenTest.Gen.Vb
{
    [TestFixture]
    public class CopyBuilderTestVb
    {
        private const string TARGET_ASSEMBLY = "CopyGenTest.dll";
        //private const string TARGET_ASSEMBLY = "CopyGenTest4Test.dll";

        private const string TARGET_METHOD_CreateCopyLinesGenerator = "CreateCopyLinesGenerator";
        private const string TARGET_METHOD_CreateCopyMethodGenerator = "CreateCopyMethodGenerator";
        private const string TARGET_CLASS = "CopyGenTest.Gen.TestResources.TestClass";

        #region CreateCopyLinesGenerator

        [Test]
        public void TestCreateCopyLinesGenerator_PropertyNames_Null()
        {
            MethodInfo targetMethod = typeof(CopyCodeGeneratorCreatorCs).GetMethod(
                TARGET_METHOD_CreateCopyLinesGenerator, BindingFlags.Instance | BindingFlags.Public);
            {
                PropertyCodeInfo propertyCodeInfo = new PropertyCodeInfo();
                Assert.That(propertyCodeInfo.SourcePropertyNames, Is.Null);
                Assert.That(propertyCodeInfo.DestPropertyNames, Is.Null);
                ICopyCodeGeneratorCreator copyBuilder = new CopyCodeGeneratorCreatorCs();

                object result = targetMethod.Invoke(copyBuilder, new object[] { new CopyInfo(), propertyCodeInfo });
                Assert.That(result, Is.Null);
            }
            {
                PropertyCodeInfo propertyCodeInfo = new PropertyCodeInfo();
                propertyCodeInfo.SourcePropertyNames = new List<string>();
                Assert.That(propertyCodeInfo.DestPropertyNames, Is.Null);
                ICopyCodeGeneratorCreator copyBuilder = new CopyCodeGeneratorCreatorCs();

                object result = targetMethod.Invoke(copyBuilder, new object[] { new CopyInfo(), propertyCodeInfo });
                Assert.That(result, Is.Null);
            }
            {
                PropertyCodeInfo propertyCodeInfo = new PropertyCodeInfo();
                Assert.That(propertyCodeInfo.SourcePropertyNames, Is.Null);
                propertyCodeInfo.DestPropertyNames = new List<string>();
                ICopyCodeGeneratorCreator copyBuilder = new CopyCodeGeneratorCreatorCs();

                object result = targetMethod.Invoke(copyBuilder, new object[] { new CopyInfo(), propertyCodeInfo });
                Assert.That(result, Is.Null);
            }
        }

        [Test]
        public void TestCreateCopyLinesGenerator_PropertyNames_Zero()
        {
            MethodInfo targetMethod = typeof(CopyCodeGeneratorCreatorCs).GetMethod(
                TARGET_METHOD_CreateCopyLinesGenerator, BindingFlags.Instance | BindingFlags.Public);
            {
                PropertyCodeInfo propertyCodeInfo = new PropertyCodeInfo();
                propertyCodeInfo.SourcePropertyNames = new List<string>();
                propertyCodeInfo.DestPropertyNames = new List<string>();
                ICopyCodeGeneratorCreator copyBuilder = new CopyCodeGeneratorCreatorCs();

                object result = targetMethod.Invoke(copyBuilder, new object[] { new CopyInfo(), propertyCodeInfo });
                ICodeGenerator generator = result as ICodeGenerator;
                Assert.That(generator, Is.Not.Null);
                Assert.That(generator.GenerateCode("\t"), Is.EqualTo(""));
            }
            {
                PropertyCodeInfo propertyCodeInfo = new PropertyCodeInfo();
                propertyCodeInfo.SourcePropertyNames = new List<string>();
                propertyCodeInfo.SourcePropertyNames.Add("Test");
                propertyCodeInfo.DestPropertyNames = new List<string>();
                ICopyCodeGeneratorCreator copyBuilder = new CopyCodeGeneratorCreatorCs();

                object result = targetMethod.Invoke(copyBuilder, new object[] { new CopyInfo(), propertyCodeInfo });
                ICodeGenerator generator = result as ICodeGenerator;
                Assert.That(generator, Is.Not.Null);
                Assert.That(generator.GenerateCode("\t"), Is.EqualTo(""));
            }
            {
                PropertyCodeInfo propertyCodeInfo = new PropertyCodeInfo();
                propertyCodeInfo.SourcePropertyNames = new List<string>();
                propertyCodeInfo.DestPropertyNames = new List<string>();
                propertyCodeInfo.DestPropertyNames.Add("Test");
                ICopyCodeGeneratorCreator copyBuilder = new CopyCodeGeneratorCreatorCs();

                object result = targetMethod.Invoke(copyBuilder, new object[] { new CopyInfo(), propertyCodeInfo });
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

            MethodInfo targetMethod = typeof(CopyCodeGeneratorCreatorCs).GetMethod(
                TARGET_METHOD_CreateCopyLinesGenerator, BindingFlags.Instance | BindingFlags.Public);
            {
                CopyInfo copyInfo = new CopyInfo();
                copyInfo.CopySource = EnumCopySource.AsArgument;

                PropertyCodeInfo propertyCodeInfo = new PropertyCodeInfo();
                propertyCodeInfo.SourcePropertyNames = new List<string>();
                propertyCodeInfo.SourcePropertyNames.Add(TARGET_PROPERTY_NAME1);
                propertyCodeInfo.SourcePropertyNames.Add(TARGET_PROPERTY_NAME2);
                propertyCodeInfo.SourcePropertyNames.Add(DIFFERENT_PROPERTY_NAME1);
                propertyCodeInfo.DestPropertyNames = new List<string>();
                propertyCodeInfo.DestPropertyNames.Add(TARGET_PROPERTY_NAME1);
                propertyCodeInfo.DestPropertyNames.Add(TARGET_PROPERTY_NAME2);
                propertyCodeInfo.DestPropertyNames.Add(DIFFERENT_PROPERTY_NAME2);
                ICopyCodeGeneratorCreator copyBuilder = new CopyCodeGeneratorCreatorCs();

                object result = targetMethod.Invoke(copyBuilder, new object[] { copyInfo, propertyCodeInfo });
                ICodeGenerator generator = result as ICodeGenerator;
                Assert.That(generator, Is.Not.Null);
                Assert.That(generator.GenerateCode("\t"), Is.EqualTo(
                                                              string.Format("\tthis.{0} = source.{0};{1}\tthis.{2} = source.{2};{1}",
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
            MethodInfo targetMethod = typeof(CopyCodeGeneratorCreatorCs).GetMethod(
                TARGET_METHOD_CreateCopyLinesGenerator, BindingFlags.Instance | BindingFlags.Public);
            {
                CopyInfo copyInfo = new CopyInfo();
                copyInfo.CopySource = EnumCopySource.This;

                PropertyCodeInfo propertyCodeInfo = new PropertyCodeInfo();
                propertyCodeInfo.SourcePropertyNames = new List<string>();
                propertyCodeInfo.SourcePropertyNames.Add(TARGET_PROPERTY_NAME1);
                propertyCodeInfo.SourcePropertyNames.Add(TARGET_PROPERTY_NAME2);
                propertyCodeInfo.SourcePropertyNames.Add(DIFFERENT_PROPERTY_NAME1);
                propertyCodeInfo.DestPropertyNames = new List<string>();
                propertyCodeInfo.DestPropertyNames.Add(TARGET_PROPERTY_NAME1);
                propertyCodeInfo.DestPropertyNames.Add(TARGET_PROPERTY_NAME2);
                propertyCodeInfo.DestPropertyNames.Add(DIFFERENT_PROPERTY_NAME2);
                ICopyCodeGeneratorCreator copyBuilder = new CopyCodeGeneratorCreatorCs();

                object result = targetMethod.Invoke(copyBuilder, new object[] { copyInfo, propertyCodeInfo });
                ICodeGenerator generator = result as ICodeGenerator;
                Assert.That(generator, Is.Not.Null);
                Assert.That(generator.GenerateCode("\t"), Is.EqualTo(
                                                              string.Format("\tthis.{0} = this.{0};{1}\tthis.{2} = this.{2};{1}", 
                                                                            TARGET_PROPERTY_NAME1, Environment.NewLine, TARGET_PROPERTY_NAME2)));
            }
        }

        #endregion

        #region CreateCopyMethodGenerator

        [Test]
        public void TestCreateCopyMethodGenerator_HasSourceArgument()
        {
            PropertyCodeInfo propertyCodeInfo = CodeInfoUtils.ReadPropertyInfo(TARGET_ASSEMBLY,
                                                                               TARGET_CLASS, TARGET_CLASS);
            MethodInfo targetMethod = typeof(CopyCodeGeneratorCreatorCs).GetMethod(
                TARGET_METHOD_CreateCopyMethodGenerator, BindingFlags.Instance | BindingFlags.Public);
            {
                CopyInfo copyInfo = new CopyInfo();
                copyInfo.CopySource = EnumCopySource.This;
                copyInfo.CopyDest = EnumCopyDest.Return;
                ICopyCodeGeneratorCreator copyBuilder = new CopyCodeGeneratorCreatorCs();

                object result = targetMethod.Invoke(copyBuilder, new object[] { copyInfo, propertyCodeInfo });
                MethodGeneratorCs generator = result as MethodGeneratorCs;
                Assert.That(generator, Is.Not.Null);
                Assert.That(generator.Arguments.Count, Is.EqualTo(0));
            }
            {
                CopyInfo copyInfo = new CopyInfo();
                copyInfo.CopySource = EnumCopySource.AsArgument;
                copyInfo.CopyDest = EnumCopyDest.Return;
                propertyCodeInfo.SourceTypeName = "int";
                copyInfo.SourceArgumentName = "source";
                ICopyCodeGeneratorCreator copyBuilder = new CopyCodeGeneratorCreatorCs();

                object result = targetMethod.Invoke(copyBuilder, new object[] { copyInfo, propertyCodeInfo });

                MethodGeneratorCs generator = result as MethodGeneratorCs;
                Assert.That(generator, Is.Not.Null);
                Assert.That(generator.Arguments.Count, Is.EqualTo(1));
                ArgumentGeneratorCs argumentGenerator = generator.Arguments[0];
                Assert.That(argumentGenerator.Comment, Is.EqualTo("コピー元"));
                Assert.That(argumentGenerator.ArgumentTypeName, Is.EqualTo(propertyCodeInfo.SourceTypeName));
                Assert.That(argumentGenerator.ArgumentName, Is.EqualTo(copyInfo.SourceArgumentName));
            }
        }

        [Test]
        public void TestCreateCopyMethodGenerator_IsReturn()
        {
            MethodInfo targetMethod = typeof(CopyCodeGeneratorCreatorCs).GetMethod(
                TARGET_METHOD_CreateCopyMethodGenerator, BindingFlags.Instance | BindingFlags.Public);
            {
                PropertyCodeInfo propertyCodeInfo = new PropertyCodeInfo();

                CopyInfo copyInfo = new CopyInfo();
                copyInfo.CopySource = EnumCopySource.This;
                copyInfo.CopyDest = EnumCopyDest.Return;
                propertyCodeInfo.DestTypeName = "TestClass";
                ICopyCodeGeneratorCreator copyBuilder = new CopyCodeGeneratorCreatorCs();

                object result = targetMethod.Invoke(copyBuilder, new object[] { copyInfo, propertyCodeInfo });
                MethodGeneratorCs generator = result as MethodGeneratorCs;
                Assert.That(generator, Is.Not.Null);
                Assert.That(generator.Arguments.Count, Is.EqualTo(0));

                Assert.That(generator.Lines.Count, Is.EqualTo(2));
                Assert.That(generator.Lines.MoveNext(), Is.True);
                Assert.That(generator.Lines.Current.GenerateCode("\t"), Is.EqualTo(
                                                                            "\tTestClass dest = new TestClass();"));
                Assert.That(generator.Lines.MoveNext(), Is.True);
                Assert.That(generator.Lines.Current.GenerateCode("\t\t"), Is.EqualTo(
                                                                              "\t\treturn dest;"));
            }
            {
                PropertyCodeInfo propertyCodeInfo = new PropertyCodeInfo();

                CopyInfo copyInfo = new CopyInfo();
                copyInfo.CopySource = EnumCopySource.This;
                copyInfo.CopyDest = EnumCopyDest.AsArgument;
                propertyCodeInfo.DestTypeName = "TestClass";
                copyInfo.DestArgumentName = "target";
                ICopyCodeGeneratorCreator copyBuilder = new CopyCodeGeneratorCreatorCs();

                object result = targetMethod.Invoke(copyBuilder, new object[] { copyInfo, propertyCodeInfo });
                MethodGeneratorCs generator = result as MethodGeneratorCs;
                Assert.That(generator, Is.Not.Null);
                Assert.That(generator.Arguments.Count, Is.EqualTo(1));
                Assert.That(generator.Arguments[0].ArgumentName, Is.EqualTo(copyInfo.DestArgumentName));
                Assert.That(generator.Arguments[0].ArgumentTypeName, Is.EqualTo(propertyCodeInfo.DestTypeName));

                Assert.That(generator.Lines.Count, Is.EqualTo(1));
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
                PropertyCodeInfo propertyCodeInfo =
                    CodeInfoUtils.ReadPropertyInfo(TARGET_ASSEMBLY,
                                                   TARGET_CLASS, TARGET_CLASS);
                copyInfo.IsOutputMethod = false;
                propertyCodeInfo.SourcePropertyNames = new List<string>(new string[] { "NormalProperty1", "NormalProperty2" });
                propertyCodeInfo.DestPropertyNames = new List<string>(new string[] { "NormalProperty1", "NormalProperty2" });
                copyInfo.CopyDest = EnumCopyDest.Return;
                copyInfo.CopySource = EnumCopySource.This;
                copyInfo.MethodName = "Hoge";
                CopyCodeGeneratorCreationFacade copyBuilder = new CopyCodeGeneratorCreationFacade(new CopyCodeGeneratorCreatorCs(), copyInfo, propertyCodeInfo);

                ICodeGenerator generator = copyBuilder.CreateCodeGenerator();
                GeneratorColleciton generatorColleciton = generator as GeneratorColleciton;
                Assert.That(generatorColleciton, Is.Not.Null);
                Assert.That(generator.GenerateCode(null), Is.EqualTo(
                                                              "dest.NormalProperty1 = this.NormalProperty1;" + Environment.NewLine
                                                              + "dest.NormalProperty2 = this.NormalProperty2;" + Environment.NewLine));
            }
            {
                CopyInfo copyInfo = new CopyInfo();
                PropertyCodeInfo propertyCodeInfo =
                    CodeInfoUtils.ReadPropertyInfo(TARGET_ASSEMBLY,
                                                   TARGET_CLASS, TARGET_CLASS);
                copyInfo.IsOutputMethod = true;
                propertyCodeInfo.SourcePropertyNames = new List<string>(new string[] { "NormalProperty1", "NormalProperty2" });
                propertyCodeInfo.SourcePropertyNames = new List<string>(new string[] { "NormalProperty1", "NormalProperty2" });
                copyInfo.CopyDest = EnumCopyDest.AsArgument;
                copyInfo.CopySource = EnumCopySource.AsArgument;
                copyInfo.MethodName = "Hoge";
                CopyCodeGeneratorCreationFacade copyBuilder = new CopyCodeGeneratorCreationFacade(new CopyCodeGeneratorCreatorCs(), copyInfo, propertyCodeInfo);

                ICodeGenerator generator = copyBuilder.CreateCodeGenerator();
                MethodGeneratorCs methodGenerator = generator as MethodGeneratorCs;
                Assert.That(methodGenerator, Is.Not.Null);
                Assert.That(methodGenerator.Arguments.Count, Is.EqualTo(2));
                Assert.That(methodGenerator.Lines.Count, Is.EqualTo(2));
                Assert.That(methodGenerator.ReturnTypeName, Is.EqualTo("void"));
            }
        }

        [Test]
        public void TestCreateCodeGenerator_NotExistAssembly()
        {
            PropertyCodeInfo propertyCodeInfo = CodeInfoUtils.ReadPropertyInfo("NotExists", "Hoge", "Huga");
            CopyCodeGeneratorCreationFacade copyBuilder = new CopyCodeGeneratorCreationFacade(new CopyCodeGeneratorCreatorCs(), new CopyInfo(), propertyCodeInfo);
            object result = copyBuilder.CreateCodeGenerator();
            Assert.That(result, Is.Null);
        }

        [Test]
        public void TestCreateCodeGenerator_NotExistType()
        {
            PropertyCodeInfo propertyCodeInfo = CodeInfoUtils.ReadPropertyInfo(TARGET_ASSEMBLY, "Hoge", "Huga");
            CopyCodeGeneratorCreationFacade copyBuilder = new CopyCodeGeneratorCreationFacade(new CopyCodeGeneratorCreatorCs(), new CopyInfo(), propertyCodeInfo);
            object result = copyBuilder.CreateCodeGenerator();
            Assert.That(result, Is.Null);
        }

        #endregion
    }
}
