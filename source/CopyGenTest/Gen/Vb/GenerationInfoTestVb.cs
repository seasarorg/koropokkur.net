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

using System.IO;
using CopyGen.Gen;
using CopyGen.Gen.Impl.Vb;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace CopyGenTest.Gen.Vb
{
    [TestFixture]
    public class GenerationInfoTestVb
    {
        [Test]
        public void TestCreate_異なるクラス名()
        {
            const string TARGET_PATH = "Gen/Vb/GenerationInfoTestVb.cs";
            Assert.That(File.Exists(TARGET_PATH), Is.True);

            ICopyTargetBaseInfoCreator creator = new CopyTargetBaseInfoCreatorVb();
            CopyTargetBaseInfo actual = creator.Create(TARGET_PATH, "Hoge,Huga");

            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.SourceTypeFullNames, Is.EqualTo("Hoge"));
            Assert.That(actual.DestTypeFullNames, Is.EqualTo("Huga"));
        }

        [Test]
        public void TestCreate_異なるクラス名三つ()
        {
            const string TARGET_PATH = "Gen/Vb/GenerationInfoTestVb.cs";
            Assert.That(File.Exists(TARGET_PATH), Is.True);

            ICopyTargetBaseInfoCreator creator = new CopyTargetBaseInfoCreatorVb();
            CopyTargetBaseInfo actual = creator.Create(TARGET_PATH, "Hoge,Huga,Wao");

            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.SourceTypeFullNames, Is.EqualTo("Hoge"));
            Assert.That(actual.DestTypeFullNames, Is.EqualTo("Huga"));
        }

        [Test]
        public void TestCreate_異なるクラス名_コピー元デフォルト()
        {
            const string TARGET_PATH = "Gen/Vb/GenerationInfoTestVb.cs";
            Assert.That(File.Exists(TARGET_PATH), Is.True);

            ICopyTargetBaseInfoCreator creator = new CopyTargetBaseInfoCreatorVb();
            CopyTargetBaseInfo actual = creator.Create(TARGET_PATH, ",Huga");

            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.SourceTypeFullNames, Is.EqualTo(
                                                        "CopyGenTest.Gen.Vb.GenerationInfoTestVb"));
            Assert.That(actual.DestTypeFullNames, Is.EqualTo("Huga"));
        }

        [Test]
        public void TestCreate_異なるクラス名_コピー先デフォルト()
        {
            const string TARGET_PATH = "Gen/Vb/GenerationInfoTestVb.cs";
            Assert.That(File.Exists(TARGET_PATH), Is.True);

            ICopyTargetBaseInfoCreator creator = new CopyTargetBaseInfoCreatorVb();
            CopyTargetBaseInfo actual = creator.Create(TARGET_PATH, "Hoge,");

            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.SourceTypeFullNames, Is.EqualTo("Hoge"));
            Assert.That(actual.DestTypeFullNames, Is.EqualTo(
                                                      "CopyGenTest.Gen.Vb.GenerationInfoTestVb"));
        }

        [Test]
        public void TestCreate_異なるクラス名_両方デフォルト()
        {
            const string TARGET_PATH = "Gen/Vb/GenerationInfoTestVb.cs";
            Assert.That(File.Exists(TARGET_PATH), Is.True);

            ICopyTargetBaseInfoCreator creator = new CopyTargetBaseInfoCreatorVb();
            CopyTargetBaseInfo actual = creator.Create(TARGET_PATH, ",");

            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.SourceTypeFullNames, Is.EqualTo(
                                                        "CopyGenTest.Gen.Vb.GenerationInfoTestVb"));
            Assert.That(actual.DestTypeFullNames, Is.EqualTo(
                                                      "CopyGenTest.Gen.Vb.GenerationInfoTestVb"));
        }

        [Test]
        public void TestCreate_異なるクラス名_クラス名指定一つ()
        {
            const string TARGET_PATH = "Gen/Vb/GenerationInfoTestVb.cs";
            Assert.That(File.Exists(TARGET_PATH), Is.True);

            ICopyTargetBaseInfoCreator creator = new CopyTargetBaseInfoCreatorVb();
            CopyTargetBaseInfo actual = creator.Create(TARGET_PATH, "Hoge");

            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.SourceTypeFullNames, Is.EqualTo("Hoge"));
            Assert.That(actual.DestTypeFullNames, Is.EqualTo("Hoge"));
        }

        [Test]
        public void TestCreate_異なるクラス名_デフォルト一つ()
        {
            const string TARGET_PATH = "Gen/Vb/GenerationInfoTestVb.cs";
            Assert.That(File.Exists(TARGET_PATH), Is.True);

            ICopyTargetBaseInfoCreator creator = new CopyTargetBaseInfoCreatorVb();
            CopyTargetBaseInfo actual = creator.Create(TARGET_PATH, " ");

            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.SourceTypeFullNames, Is.EqualTo(
                                                        "CopyGenTest.Gen.Vb.GenerationInfoTestVb"));
            Assert.That(actual.DestTypeFullNames, Is.EqualTo(
                                                      "CopyGenTest.Gen.Vb.GenerationInfoTestVb"));
        }
    }
}