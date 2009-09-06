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

using System.Collections.Generic;
using CopyGen.Gen;
using CopyGen.Gen.Impl.Vb;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace CopyGenTest.Gen.Vb
{
    [TestFixture]
    public class CopyTypeNameInfoTestVb
    {
        private const string TARGET_FILE_PATH = "Gen/Vb/CopyTypeNameInfoTestVb.cs";
        private const string EXPECT_DEFAULT_TYPE_NAME = "CopyGenTest.Gen.Vb.CopyTypeNameInfoTestVb";

        /// <summary>
        /// 空文字列を指定した場合
        /// </summary>
        [Test]
        public void TestCreate_DefaultOnly()
        {
            ICopyTargetBaseInfoCreator creator = new CopyTargetBaseInfoCreatorVb();
            CopyTargetBaseInfo actual = creator.Create(TARGET_FILE_PATH, string.Empty);
            
            Assert.That(actual.SourceTypeFullNames, Is.EqualTo(EXPECT_DEFAULT_TYPE_NAME));
            Assert.That(actual.DestTypeFullNames, Is.EqualTo(EXPECT_DEFAULT_TYPE_NAME));
        }
        
        /// <summary>
        /// カンマのみ指定した場合
        /// </summary>
        [Test]
        public void TestCreate_DefaultDefault()
        {
            ICopyTargetBaseInfoCreator creator = new CopyTargetBaseInfoCreatorVb();
            CopyTargetBaseInfo actual = creator.Create(TARGET_FILE_PATH, ",");

            Assert.That(actual.SourceTypeFullNames, Is.EqualTo(EXPECT_DEFAULT_TYPE_NAME));
            Assert.That(actual.DestTypeFullNames, Is.EqualTo(EXPECT_DEFAULT_TYPE_NAME));
        }

        /// <summary>
        /// クラス名一つだけ指定した場合(名前空間なし）
        /// </summary>
        [Test]
        public void TestCreate_OneClassName_NoNameSpace()
        {
            string targetClassName = typeof (HogeClass).Name;
            ICopyTargetBaseInfoCreator creator = new CopyTargetBaseInfoCreatorVb();
            CopyTargetBaseInfo actual = creator.Create(TARGET_FILE_PATH, targetClassName);

            List<string> expectNameList = new List<string>();
            expectNameList.Add(targetClassName);

            string expectNames = string.Join(",", expectNameList.ToArray());
            System.Console.WriteLine(expectNames);

            Assert.That(actual.SourceTypeFullNames, Is.EqualTo(expectNames));
            Assert.That(actual.DestTypeFullNames, Is.EqualTo(expectNames));
        }

        /// <summary>
        /// クラス名一つだけ指定した場合(名前空間あり）
        /// </summary>
        [Test]
        public void TestCreate_OneClassName_WithNameSpace()
        {
            string targetClassName = "CopyGenTest.Gen." + typeof(HogeClass).Name;
            ICopyTargetBaseInfoCreator creator = new CopyTargetBaseInfoCreatorVb();
            CopyTargetBaseInfo actual = creator.Create(TARGET_FILE_PATH, targetClassName);

            Assert.That(actual.SourceTypeFullNames, Is.EqualTo(targetClassName));
            Assert.That(actual.DestTypeFullNames, Is.EqualTo(targetClassName));
        }

        /// <summary>
        /// コピー元指定＋コピー先デフォルト（名前空間なし）
        /// </summary>
        [Test]
        public void TestCreate_SourceDefault_NoNameSpace()
        {
            string sourceClassName = typeof(HogeClass).Name;
            ICopyTargetBaseInfoCreator creator = new CopyTargetBaseInfoCreatorVb();
            CopyTargetBaseInfo actual = creator.Create(TARGET_FILE_PATH, sourceClassName + ",");

            List<string> expectNameList = new List<string>();
            expectNameList.Add(sourceClassName);

            string expectSourceNames = string.Join(",", expectNameList.ToArray());
            System.Console.WriteLine(expectSourceNames);

            Assert.That(actual.SourceTypeFullNames, Is.EqualTo(expectSourceNames));
            Assert.That(actual.DestTypeFullNames, Is.EqualTo(EXPECT_DEFAULT_TYPE_NAME));
        }

        /// <summary>
        /// コピー元指定＋コピー先デフォルト（名前空間あり）
        /// </summary>
        [Test]
        public void TestCreate_SourceDefault_WithNameSpace()
        {
            string sourceClassName = "CopyGenTest.Gen." + typeof(HogeClass).Name;
            ICopyTargetBaseInfoCreator creator = new CopyTargetBaseInfoCreatorVb();
            CopyTargetBaseInfo actual = creator.Create(TARGET_FILE_PATH, sourceClassName + ",");

            Assert.That(actual.SourceTypeFullNames, Is.EqualTo(sourceClassName));
            Assert.That(actual.DestTypeFullNames, Is.EqualTo(EXPECT_DEFAULT_TYPE_NAME));
        }

        /// <summary>
        /// コピー元デフォルト＋コピー先指定（名前空間なし）
        /// </summary>
        [Test]
        public void TestCreate_DefaultTarget_NoNameSpace()
        {
            string targetClassName = typeof(HogeClass).Name;
            ICopyTargetBaseInfoCreator creator = new CopyTargetBaseInfoCreatorVb();
            CopyTargetBaseInfo actual = creator.Create(TARGET_FILE_PATH, "," + targetClassName);

            List<string> expectNameList = new List<string>();
            expectNameList.Add(targetClassName);

            string expectTargetNames = string.Join(",", expectNameList.ToArray());
            System.Console.WriteLine(expectTargetNames);

            Assert.That(actual.SourceTypeFullNames, Is.EqualTo(EXPECT_DEFAULT_TYPE_NAME));
            Assert.That(actual.DestTypeFullNames, Is.EqualTo(expectTargetNames));
        }

        /// <summary>
        /// コピー元デフォルト＋コピー先指定（名前空間あり）
        /// </summary>
        [Test]
        public void TestCreate_DefaultTarget_WithNameSpace()
        {
            string targetClassName = "System.Collections.Generic." + typeof(HogeClass).Name;
            ICopyTargetBaseInfoCreator creator = new CopyTargetBaseInfoCreatorVb();
            CopyTargetBaseInfo actual = creator.Create(TARGET_FILE_PATH, "," + targetClassName);

            Assert.That(actual.SourceTypeFullNames, Is.EqualTo(EXPECT_DEFAULT_TYPE_NAME));
            Assert.That(actual.DestTypeFullNames, Is.EqualTo(targetClassName));
        }

        /// <summary>
        /// コピー元指定＋コピー先指定（名前空間なし）
        /// </summary>
        [Test]
        public void TestCreate_SourceTarget_NoNameSpace()
        {
            string sourceClassName = typeof(HugaClass).Name;
            string targetClassName = typeof(HogeClass).Name;

            ICopyTargetBaseInfoCreator creator = new CopyTargetBaseInfoCreatorVb();
            CopyTargetBaseInfo actual = creator.Create(TARGET_FILE_PATH, sourceClassName + "," + targetClassName);

            List<string> expectSourceNameList = new List<string>();
            expectSourceNameList.Add(sourceClassName);

            string expectSourceNames = string.Join(",", expectSourceNameList.ToArray());
            System.Console.WriteLine(expectSourceNames);

            List<string> expectTargetNameList = new List<string>();
            expectTargetNameList.Add(targetClassName);

            string expectTargetNames = string.Join(",", expectTargetNameList.ToArray());
            System.Console.WriteLine(expectTargetNames);

            Assert.That(actual.SourceTypeFullNames, Is.EqualTo(expectSourceNames));
            Assert.That(actual.DestTypeFullNames, Is.EqualTo(expectTargetNames));
        }

        /// <summary>
        /// コピー元指定＋コピー先指定（名前空間なし）
        /// </summary>
        [Test]
        public void TestCreate_SourceTarget_WithNameSpace()
        {
            string sourceClassName = "CopyGen.Gen." + typeof(HugaClass).Name;
            string targetClassName = "NUnit.Framework.SyntaxHelpers." + typeof(HogeClass).Name;

            ICopyTargetBaseInfoCreator creator = new CopyTargetBaseInfoCreatorVb();
            CopyTargetBaseInfo actual = creator.Create(TARGET_FILE_PATH, sourceClassName + "," + targetClassName);

            Assert.That(actual.SourceTypeFullNames, Is.EqualTo(sourceClassName));
            Assert.That(actual.DestTypeFullNames, Is.EqualTo(targetClassName));
        }

        /// <summary>
        /// コピー元指定＋コピー先指定（コピー元だけ名前空間なし）
        /// </summary>
        [Test]
        public void TestCreate_SourceTarget_NoNameSpaceSource()
        {
            string sourceClassName = typeof(HugaClass).Name;
            string targetClassName = "CopyGenTest.Gen." + typeof(HogeClass).Name;

            ICopyTargetBaseInfoCreator creator = new CopyTargetBaseInfoCreatorVb();
            CopyTargetBaseInfo actual = creator.Create(TARGET_FILE_PATH, sourceClassName + "," + targetClassName);

            List<string> expectSourceNameList = new List<string>();
            expectSourceNameList.Add(sourceClassName);

            string expectSourceNames = string.Join(",", expectSourceNameList.ToArray());
            System.Console.WriteLine(expectSourceNames);

            Assert.That(actual.SourceTypeFullNames, Is.EqualTo(expectSourceNames));
            Assert.That(actual.DestTypeFullNames, Is.EqualTo(targetClassName));
        }

        /// <summary>
        /// コピー元指定＋コピー先指定（コピー先だけ名前空間なし）
        /// </summary>
        [Test]
        public void TestCreate_SourceTarget_NoNameSpaceTarget()
        {
            string sourceClassName = "CopyGenTest.Gen." + typeof(HugaClass).Name;
            string targetClassName = typeof(HogeClass).Name;

            ICopyTargetBaseInfoCreator creator = new CopyTargetBaseInfoCreatorVb();
            CopyTargetBaseInfo actual = creator.Create(TARGET_FILE_PATH, sourceClassName + "," + targetClassName);

            List<string> expectTargetNameList = new List<string>();
            expectTargetNameList.Add(targetClassName);

            string expectTargetNames = string.Join(",", expectTargetNameList.ToArray());
            System.Console.WriteLine(expectTargetNames);

            Assert.That(actual.SourceTypeFullNames, Is.EqualTo(sourceClassName));
            Assert.That(actual.DestTypeFullNames, Is.EqualTo(expectTargetNames));
        }
    }

    /// <summary>
    /// テスト用のダミークラス
    /// </summary>
    public class HogeClass
    {
        private string _name = "Hoge";
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
    }

    /// <summary>
    /// テスト用のダミークラス2
    /// </summary>
    public class HugaClass
    {
        private string _name = "Huga";
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
    }
}