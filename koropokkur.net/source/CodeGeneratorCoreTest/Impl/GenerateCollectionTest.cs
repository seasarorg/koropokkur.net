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
using CodeGeneratorCore;
using CodeGeneratorCore.Impl;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace CodeGeneratorCoreTest.Impl
{
    [TestFixture]
    public class GenerateCollectionTest
    {
        [Test]
        public void TestGenerateCode()
        {
            StringBuilder expectBuilder = new StringBuilder();
            expectBuilder.AppendLine("\t// TestComment");
            expectBuilder.AppendLine("\tint a = this.Hoge();");
            expectBuilder.AppendLine("\treturn a;");

            GeneratorColleciton generatorColleciton = new GeneratorColleciton();
            LineCommentGenerator commentGenerator = new LineCommentGenerator();
            commentGenerator.Items.Add("TestComment");
            generatorColleciton.Add(commentGenerator);

            LineGenerator lineGenerator = new LineGenerator();
            lineGenerator.Items.Add("int");
            lineGenerator.Items.Add("a");
            lineGenerator.Items.Add("=");
            lineGenerator.Items.Add("this.Hoge()");
            generatorColleciton.Add(lineGenerator);

            LineReturnGenerator returnGenerator = new LineReturnGenerator();
            returnGenerator.Items.Add("a");
            generatorColleciton.Add(returnGenerator);

            string actual = generatorColleciton.GenerateCode("\t");
            Assert.That(actual, Is.EqualTo(expectBuilder.ToString()));
        }

        [Test]
        public void TestEnumerable()
        {
            string[] expectStrings = new string[] { "\t// TestComment", "\tint a = this.Hoge();", "\treturn a;" };

            GeneratorColleciton generatorColleciton = new GeneratorColleciton();
            LineCommentGenerator commentGenerator = new LineCommentGenerator();
            commentGenerator.Items.Add("TestComment");
            generatorColleciton.Add(commentGenerator);

            LineGenerator lineGenerator = new LineGenerator();
            lineGenerator.Items.Add("int");
            lineGenerator.Items.Add("a");
            lineGenerator.Items.Add("=");
            lineGenerator.Items.Add("this.Hoge()");
            generatorColleciton.Add(lineGenerator);

            LineReturnGenerator returnGenerator = new LineReturnGenerator();
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
