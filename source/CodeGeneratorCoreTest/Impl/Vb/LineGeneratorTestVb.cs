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

using CodeGeneratorCore.Impl.Vb;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace CodeGeneratorCoreTest.Impl.Vb
{
    [TestFixture]
    public class LineGeneratorTestVb
    {
        [Test]
        public void TestGenerateCode()
        {
            const string EXPECT_CODE = "\t\tDim a As Integer = 999";

            LineGeneratorVb generator = new LineGeneratorVb();
            generator.Items.Add("Dim");
            generator.Items.Add("a");
            generator.Items.Add("As");
            generator.Items.Add("Integer");
            generator.Items.Add("=");
            generator.Items.Add("999");

            string actual = generator.GenerateCode("\t\t");
            Assert.That(actual, Is.EqualTo(EXPECT_CODE));
        }

        [Test]
        public void TestGenerateCode_NoItem()
        {
            const string EXPECT_CODE = "";

            LineGeneratorVb generator = new LineGeneratorVb();

            string actual = generator.GenerateCode(null);
            Assert.That(actual, Is.EqualTo(EXPECT_CODE));
        }
    }
}