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

using CodeGeneratorCore.Enum;
using CodeGeneratorCore.Impl.Cs;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace CodeGeneratorCoreTest.Impl.Cs
{
    [TestFixture]
    public class ArgumentGeneratorTestCs
    {
        [Test]
        public void TestGenerateCode_Params()
        {
            const string EXPECT_CODE = "params object[] hoges";

            ArgumentGeneratorCs generator = new ArgumentGeneratorCs();
            generator.Reference = EnumArgumentReference.Params;
            generator.ArgumentName = "hoges";

            string actual = generator.GenerateCode("\t");
            Assert.That(actual, Is.EqualTo(EXPECT_CODE));
        }

        [Test]
        public void TestGenerateCode_Ref()
        {
            const string EXPECT_CODE = "ref int huge";

            ArgumentGeneratorCs generator = new ArgumentGeneratorCs();
            generator.Reference = EnumArgumentReference.Ref;
            generator.ArgumentName = "huge";
            generator.ArgumentTypeName = "int";

            string actual = generator.GenerateCode("\t");
            Assert.That(actual, Is.EqualTo(EXPECT_CODE));
        }

        [Test]
        public void TestGenerateCode_Out()
        {
            const string EXPECT_CODE = "out long huga";

            ArgumentGeneratorCs generator = new ArgumentGeneratorCs();
            generator.Reference = EnumArgumentReference.Out;
            generator.ArgumentName = "huga";
            generator.ArgumentTypeName = "long";

            string actual = generator.GenerateCode("\t");
            Assert.That(actual, Is.EqualTo(EXPECT_CODE));
        }
    }
}