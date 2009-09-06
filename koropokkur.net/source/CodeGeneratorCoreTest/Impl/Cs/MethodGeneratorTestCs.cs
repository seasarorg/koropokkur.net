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

using System.Text;
using CodeGeneratorCore.Impl.Cs;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using CodeGeneratorCore.Enum;

namespace CodeGeneratorCoreTest.Impl.Cs
{
    [TestFixture]
    public class MethodGeneratorTestCs : MethodGeneratorCs
    {
        [SetUp]
        public void Setup()
        {
            Arguments.Clear();
            Lines.Clear();
            MethodComment = null;
            MethodName = null;
            MethodOption = EnumMethodOption.None;
            ReturnComment = null;
            ReturnTypeName = null;
            Visibility = EnumVisibility.Public;
        }

        [Test]
        public void TestGenerateHeaderString()
        {
            StringBuilder expectBuilder = new StringBuilder();
            expectBuilder.AppendLine("/// <summary>");
            expectBuilder.AppendLine("\t/// メソッドコメントのテストです。");
            expectBuilder.AppendLine("\t/// </summary>");
            expectBuilder.AppendLine("\t/// <param name=\"hoge\">引数のコメントです。</param>");
            expectBuilder.Append("\t/// <returns>戻り値のコメントです。</returns>");

            MethodComment = "メソッドコメントのテストです。";
            ReturnTypeName = "int";
            ReturnComment = "戻り値のコメントです。";

            ArgumentGeneratorCs argumentGenerator = new ArgumentGeneratorCs();
            argumentGenerator.ArgumentName = "hoge";
            argumentGenerator.ArgumentTypeName = "string";
            argumentGenerator.Comment = "引数のコメントです。";
            Arguments.Add(argumentGenerator);

            string actual = GenerateHeaderString("\t");
            Assert.That(actual, Is.EqualTo(expectBuilder.ToString()));
        }

        [Test]
        public void TestGenerateHeaderString_Void()
        {
            StringBuilder expectBuilder = new StringBuilder();
            expectBuilder.AppendLine("/// <summary>");
            expectBuilder.AppendLine("/// メソッドコメントのテストです。");
            expectBuilder.Append("/// </summary>");

            MethodComment = "メソッドコメントのテストです。";
            ReturnTypeName = "void";
            ReturnComment = "戻り値のコメントです。";

            string actual = GenerateHeaderString(null);
            Assert.That(actual, Is.EqualTo(expectBuilder.ToString()));
        }

        [Test]
        public void TestGenerateMethodDefinition_PublicStatic()
        {
            const string EXPECT_CODE = "public static int Hoge(params object[] args)";

            Visibility = EnumVisibility.Public;
            MethodOption = EnumMethodOption.Static;
            ReturnTypeName = "int";
            MethodName = "Hoge";

            ArgumentGeneratorCs argumentGenerator = new ArgumentGeneratorCs();
            argumentGenerator.Reference = EnumArgumentReference.Params;
            argumentGenerator.ArgumentName = "args";
            Arguments.Add(argumentGenerator);

            string actual = GenerateMethodDefinition();
            Assert.That(actual, Is.EqualTo(EXPECT_CODE));
        }

        [Test]
        public void TestGenerateMethodDefinition_IntarnalAbstract()
        {
            const string EXPECT_CODE = "internal abstract int Hoge(params object[] args)";

            Visibility = EnumVisibility.Internal;
            MethodOption = EnumMethodOption.Abstract;
            ReturnTypeName = "int";
            MethodName = "Hoge";

            ArgumentGeneratorCs argumentGenerator = new ArgumentGeneratorCs();
            argumentGenerator.Reference = EnumArgumentReference.Params;
            argumentGenerator.ArgumentName = "args";
            Arguments.Add(argumentGenerator);

            string actual = GenerateMethodDefinition();
            Assert.That(actual, Is.EqualTo(EXPECT_CODE));
        }

        [Test]
        public void TestGenerateMethodDefinition_ProtectedOverride()
        {
            const string EXPECT_CODE = "protected override int Hoge(params object[] args)";

            Visibility = EnumVisibility.Protected;
            MethodOption = EnumMethodOption.Override;
            ReturnTypeName = "int";
            MethodName = "Hoge";

            ArgumentGeneratorCs argumentGenerator = new ArgumentGeneratorCs();
            argumentGenerator.Reference = EnumArgumentReference.Params;
            argumentGenerator.ArgumentName = "args";
            Arguments.Add(argumentGenerator);

            string actual = GenerateMethodDefinition();
            Assert.That(actual, Is.EqualTo(EXPECT_CODE));
        }

        [Test]
        public void TestGenerateMethodDefinition_ProtectedVirtual()
        {
            const string EXPECT_CODE = "protected virtual int Hoge(string s, params object[] args)";

            Visibility = EnumVisibility.Protected;
            MethodOption = EnumMethodOption.Virtual;
            ReturnTypeName = "int";
            MethodName = "Hoge";

            ArgumentGeneratorCs argumentGenerator1 = new ArgumentGeneratorCs();
            argumentGenerator1.Reference = EnumArgumentReference.Normal;
            argumentGenerator1.ArgumentTypeName = "string";
            argumentGenerator1.ArgumentName = "s";
            Arguments.Add(argumentGenerator1);

            ArgumentGeneratorCs argumentGenerator2 = new ArgumentGeneratorCs();
            argumentGenerator2.Reference = EnumArgumentReference.Params;
            argumentGenerator2.ArgumentName = "args";
            Arguments.Add(argumentGenerator2);

            string actual = GenerateMethodDefinition();
            Assert.That(actual, Is.EqualTo(EXPECT_CODE));
        }

        [Test]
        public void TestGenerateMethodDefinition_PrivateNone()
        {
            const string EXPECT_CODE = "private int Hoge(params object[] args)";

            Visibility = EnumVisibility.Private;
            MethodOption = EnumMethodOption.None;
            ReturnTypeName = "int";
            MethodName = "Hoge";

            ArgumentGeneratorCs argumentGenerator = new ArgumentGeneratorCs();
            argumentGenerator.Reference = EnumArgumentReference.Params;
            argumentGenerator.ArgumentName = "args";
            Arguments.Add(argumentGenerator);

            string actual = GenerateMethodDefinition();
            Assert.That(actual, Is.EqualTo(EXPECT_CODE));
        }

        [Test]
        public void TestGenerateMethodDefinition_NoArguments()
        {
            const string EXPECT_CODE = "private int Hoge()";

            Visibility = EnumVisibility.Private;
            MethodOption = EnumMethodOption.None;
            ReturnTypeName = "int";
            MethodName = "Hoge";

            string actual = GenerateMethodDefinition();
            Assert.That(actual, Is.EqualTo(EXPECT_CODE));
        }
    }
}