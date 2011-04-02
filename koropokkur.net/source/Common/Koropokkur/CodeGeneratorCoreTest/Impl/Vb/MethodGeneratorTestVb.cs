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

using System.Text;
using CodeGeneratorCore.Enum;
using CodeGeneratorCore.Impl.Vb;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace CodeGeneratorCoreTest.Impl.Vb
{
    [TestFixture]
    public class MethodGeneratorTestVb : MethodGeneratorVb
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
            expectBuilder.AppendLine("''' <summary>");
            expectBuilder.AppendLine("\t''' メソッドコメントのテストです。");
            expectBuilder.AppendLine("\t''' </summary>");
            expectBuilder.AppendLine("\t''' <param name=\"hoge\">引数のコメントです。</param>");
            expectBuilder.Append("\t''' <returns>戻り値のコメントです。</returns>");

            MethodComment = "メソッドコメントのテストです。";
            ReturnTypeName = "Integer";
            ReturnComment = "戻り値のコメントです。";

            ArgumentGeneratorVb argumentGenerator = new ArgumentGeneratorVb();
            argumentGenerator.ArgumentName = "hoge";
            argumentGenerator.ArgumentTypeName = "String";
            argumentGenerator.Comment = "引数のコメントです。";
            Arguments.Add(argumentGenerator);

            string actual = GenerateHeaderString("\t");
            Assert.That(actual, Is.EqualTo(expectBuilder.ToString()));
        }

        [Test]
        public void TestGenerateHeaderString_Void()
        {
            StringBuilder expectBuilder = new StringBuilder();
            expectBuilder.AppendLine("''' <summary>");
            expectBuilder.AppendLine("''' メソッドコメントのテストです。");
            expectBuilder.Append("''' </summary>");

            MethodComment = "メソッドコメントのテストです。";
            ReturnTypeName = "";
            ReturnComment = "戻り値のコメントです。";

            string actual = GenerateHeaderString(null);
            Assert.That(actual, Is.EqualTo(expectBuilder.ToString()));
        }

        [Test]
        public void TestGenerateMethodDefinition_PublicStatic()
        {
            const string EXPECT_CODE = "Public Shared Function Hoge(Optional args As Object = Nothing) As Integer";

            Visibility = EnumVisibility.Public;
            MethodOption = EnumMethodOption.Static;
            ReturnTypeName = "Integer";
            MethodName = "Hoge";

            ArgumentGeneratorVb argumentGenerator = new ArgumentGeneratorVb();
            argumentGenerator.Reference = EnumArgumentReference.Params;
            argumentGenerator.ArgumentName = "args";
            Arguments.Add(argumentGenerator);

            string actual = GenerateMethodDefinition();
            Assert.That(actual, Is.EqualTo(EXPECT_CODE));
        }

        [Test]
        public void TestGenerateMethodDefinition_IntarnalAbstract()
        {
            const string EXPECT_CODE = "Friend MustInherit Function Hoge(Optional args As Object = Nothing) As Integer";

            Visibility = EnumVisibility.Internal;
            MethodOption = EnumMethodOption.Abstract;
            ReturnTypeName = "Integer";
            MethodName = "Hoge";

            ArgumentGeneratorVb argumentGenerator = new ArgumentGeneratorVb();
            argumentGenerator.Reference = EnumArgumentReference.Params;
            argumentGenerator.ArgumentName = "args";
            Arguments.Add(argumentGenerator);

            string actual = GenerateMethodDefinition();
            Assert.That(actual, Is.EqualTo(EXPECT_CODE));
        }

        [Test]
        public void TestGenerateMethodDefinition_ProtectedOverride()
        {
            const string EXPECT_CODE = "Protected Overrides Function Hoge(Optional args As Object = Nothing) As Integer";

            Visibility = EnumVisibility.Protected;
            MethodOption = EnumMethodOption.Override;
            ReturnTypeName = "Integer";
            MethodName = "Hoge";

            ArgumentGeneratorVb argumentGenerator = new ArgumentGeneratorVb();
            argumentGenerator.Reference = EnumArgumentReference.Params;
            argumentGenerator.ArgumentName = "args";
            Arguments.Add(argumentGenerator);

            string actual = GenerateMethodDefinition();
            Assert.That(actual, Is.EqualTo(EXPECT_CODE));
        }

        [Test]
        public void TestGenerateMethodDefinition_ProtectedVirtual()
        {
            const string EXPECT_CODE = "Protected Overridable Function Hoge(s As String, Optional args As Object = Nothing) As Integer";

            Visibility = EnumVisibility.Protected;
            MethodOption = EnumMethodOption.Virtual;
            ReturnTypeName = "Integer";
            MethodName = "Hoge";

            ArgumentGeneratorVb argumentGenerator1 = new ArgumentGeneratorVb();
            argumentGenerator1.Reference = EnumArgumentReference.Normal;
            argumentGenerator1.ArgumentTypeName = "String";
            argumentGenerator1.ArgumentName = "s";
            Arguments.Add(argumentGenerator1);

            ArgumentGeneratorVb argumentGenerator2 = new ArgumentGeneratorVb();
            argumentGenerator2.Reference = EnumArgumentReference.Params;
            argumentGenerator2.ArgumentName = "args";
            Arguments.Add(argumentGenerator2);

            string actual = GenerateMethodDefinition();
            Assert.That(actual, Is.EqualTo(EXPECT_CODE));
        }

        [Test]
        public void TestGenerateMethodDefinition_PrivateNone()
        {
            const string EXPECT_CODE = "Private Function Hoge(Optional args As Object = Nothing) As Integer";

            Visibility = EnumVisibility.Private;
            MethodOption = EnumMethodOption.None;
            ReturnTypeName = "Integer";
            MethodName = "Hoge";

            ArgumentGeneratorVb argumentGenerator = new ArgumentGeneratorVb();
            argumentGenerator.Reference = EnumArgumentReference.Params;
            argumentGenerator.ArgumentName = "args";
            Arguments.Add(argumentGenerator);

            string actual = GenerateMethodDefinition();
            Assert.That(actual, Is.EqualTo(EXPECT_CODE));
        }

        [Test]
        public void TestGenerateMethodDefinition_NoArguments()
        {
            const string EXPECT_CODE = "Private Function Hoge() As Integer";

            Visibility = EnumVisibility.Private;
            MethodOption = EnumMethodOption.None;
            ReturnTypeName = "Integer";
            MethodName = "Hoge";

            string actual = GenerateMethodDefinition();
            Assert.That(actual, Is.EqualTo(EXPECT_CODE));
        }

        [Test]
        public void TestGenerateMethodDefinition_NoReturn()
        {
            const string EXPECT_CODE = "Private Sub Hoge()";

            Visibility = EnumVisibility.Private;
            MethodOption = EnumMethodOption.None;
            ReturnTypeName = null;
            MethodName = "Hoge";

            string actual = GenerateMethodDefinition();
            Assert.That(actual, Is.EqualTo(EXPECT_CODE));
        }
    }
}
