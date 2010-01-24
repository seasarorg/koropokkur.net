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

using System.Text;
using CodeGeneratorCore;
using CodeGeneratorCore.Impl;
using CodeGeneratorCore.Impl.Vb;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace CodeGeneratorCoreTest.Impl.Vb
{
    [TestFixture]
    public class GenerateCollectionTestVb
    {
        [Test]
        public void TestGenerateCode()
        {
            StringBuilder expectBuilder = new StringBuilder();
            expectBuilder.AppendLine("\t' TestComment");
            expectBuilder.AppendLine("\tDim a As Integer = Me.Hoge()");
            expectBuilder.AppendLine("\tReturn a");

            GeneratorColleciton generatorColleciton = new GeneratorColleciton();
            LineCommentGeneratorVb commentGenerator = new LineCommentGeneratorVb();
            commentGenerator.Items.Add("TestComment");
            generatorColleciton.Add(commentGenerator);

            LineGeneratorVb lineGenerator = new LineGeneratorVb();
            lineGenerator.Items.Add("Dim");
            lineGenerator.Items.Add("a");
            lineGenerator.Items.Add("As");
            lineGenerator.Items.Add("Integer");
            lineGenerator.Items.Add("=");
            lineGenerator.Items.Add("Me.Hoge()");
            generatorColleciton.Add(lineGenerator);

            LineReturnGeneratorVb returnGenerator = new LineReturnGeneratorVb();
            returnGenerator.Items.Add("a");
            generatorColleciton.Add(returnGenerator);

            string actual = generatorColleciton.GenerateCode("\t");
            Assert.That(actual, Is.EqualTo(expectBuilder.ToString()));
        }

        [Test]
        public void TestEnumerable()
        {
            string[] expectStrings = new string[] { "\t' TestComment", "\tDim a As Integer = Me.Hoge()", "\tReturn a" };

            GeneratorColleciton generatorColleciton = new GeneratorColleciton();
            LineCommentGeneratorVb commentGenerator = new LineCommentGeneratorVb();
            commentGenerator.Items.Add("TestComment");
            generatorColleciton.Add(commentGenerator);

            LineGeneratorVb lineGenerator = new LineGeneratorVb();
            lineGenerator.Items.Add("Dim");
            lineGenerator.Items.Add("a");
            lineGenerator.Items.Add("As");
            lineGenerator.Items.Add("Integer");
            lineGenerator.Items.Add("=");
            lineGenerator.Items.Add("Me.Hoge()");
            generatorColleciton.Add(lineGenerator);

            LineReturnGeneratorVb returnGenerator = new LineReturnGeneratorVb();
            returnGenerator.Items.Add("a");
            generatorColleciton.Add(returnGenerator);

            int i = 0;
            foreach (ICodeGenerator generator in generatorColleciton)
            {
                Assert.That(generator.GenerateCode("\t"), Is.EqualTo(expectStrings[i]), i.ToString());
                i++;
            }
        }
    }
}