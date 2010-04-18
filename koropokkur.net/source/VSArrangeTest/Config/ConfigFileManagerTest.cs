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

using System;
using System.IO;
using System.Reflection;
using System.Xml;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using VSArrange.Config;

namespace VSArrangeTest.Config
{
    [TestFixture]
    public class ConfigFileManagerTest
    {
        [TearDown]
        public void TearDown()
        {
            ConfigInfo.GetInstance().FilterFileStringList = null;
            ConfigInfo.GetInstance().FilterFolderStringList = null;
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReadConfig_ArgumentNull()
        {
            ConfigFileManager.ReadConfig(null);
        }

        [Test]
        public void TestReadConfig_FileNotExists()
        {
            ConfigInfo configInfo = ConfigFileManager.ReadConfig("c:/not_exists");

            Assert.That(configInfo, Is.Not.Null);
            Assert.That(configInfo.FilterFileStringList, Is.Null);
            Assert.That(configInfo.FilterFolderStringList, Is.Null);
        }

        [Test]
        [ExpectedException(typeof(XmlException))]
        public void TestReadConfig_WrongFormatXml()
        {
            string targetPath = GetConfigPath("Wrong.config");
            ConfigFileManager.ReadConfig(targetPath);
        }

        [Test]
        public void TestReadConfig_Both()
        {
            string targetPath = GetConfigPath("Both.config");
            ConfigInfo configInfo = ConfigFileManager.ReadConfig(targetPath);

            Assert.That(configInfo, Is.Not.Null);
            Assert.That(configInfo.FilterFileStringList.Count, Is.GreaterThan(1));

            ConfigInfoDetail fileFilter1 = configInfo.FilterFileStringList[0];
            Assert.That(fileFilter1.Name, Is.EqualTo("hoge"));
            Assert.That(fileFilter1.IsEnable, Is.True);
            Assert.That(fileFilter1.Value, Is.EqualTo("test"));

            ConfigInfoDetail fileFilter2 = configInfo.FilterFileStringList[1];
            Assert.That(fileFilter2.Name, Is.EqualTo("ほげ"));
            Assert.That(fileFilter2.IsEnable, Is.False);
            Assert.That(fileFilter2.Value, Is.EqualTo("テストA"));


            Assert.That(configInfo.FilterFolderStringList.Count, Is.GreaterThan(1));

            ConfigInfoDetail folderFilter1 = configInfo.FilterFolderStringList[0];
            Assert.That(folderFilter1.Name, Is.EqualTo("ふが"));
            Assert.That(folderFilter1.IsEnable, Is.False);
            Assert.That(folderFilter1.Value, Is.EqualTo("test1"));

            ConfigInfoDetail folderFilter2 = configInfo.FilterFolderStringList[1];
            Assert.That(folderFilter2.Name, Is.EqualTo("huga"));
            Assert.That(folderFilter2.IsEnable, Is.True);
            Assert.That(folderFilter2.Value, Is.EqualTo("テストB"));
        }

        [Test]
        public void TestReadConfig_FileOnly()
        {
            string targetPath = GetConfigPath("FileOnly.config");
            ConfigInfo configInfo = ConfigFileManager.ReadConfig(targetPath);

            Assert.That(configInfo, Is.Not.Null);
            Assert.That(configInfo.FilterFileStringList.Count, Is.GreaterThan(1));

            ConfigInfoDetail fileFilter1 = configInfo.FilterFileStringList[0];
            Assert.That(fileFilter1.Name, Is.EqualTo("hoge"));
            Assert.That(fileFilter1.IsEnable, Is.True);
            Assert.That(fileFilter1.Value, Is.EqualTo("test"));

            ConfigInfoDetail fileFilter2 = configInfo.FilterFileStringList[1];
            Assert.That(fileFilter2.Name, Is.EqualTo("ほげ"));
            Assert.That(fileFilter2.IsEnable, Is.False);
            Assert.That(fileFilter2.Value, Is.EqualTo("テストA"));

            Assert.That(configInfo.FilterFolderStringList == null || 
                configInfo.FilterFolderStringList.Count == 0, Is.True);
        }

        [Test]
        public void TestReadConfig_FolderOnly()
        {
            string targetPath = GetConfigPath("FolderOnly.config");
            ConfigInfo configInfo = ConfigFileManager.ReadConfig(targetPath);

            Assert.That(configInfo, Is.Not.Null);
            Assert.That(configInfo.FilterFileStringList == null ||
                configInfo.FilterFileStringList.Count == 0, Is.True);

            Assert.That(configInfo.FilterFolderStringList.Count, Is.GreaterThan(1));

            ConfigInfoDetail folderFilter1 = configInfo.FilterFolderStringList[0];
            Assert.That(folderFilter1.Name, Is.EqualTo("ふが"));
            Assert.That(folderFilter1.IsEnable, Is.False);
            Assert.That(folderFilter1.Value, Is.EqualTo("test1"));

            ConfigInfoDetail folderFilter2 = configInfo.FilterFolderStringList[1];
            Assert.That(folderFilter2.Name, Is.EqualTo("huga"));
            Assert.That(folderFilter2.IsEnable, Is.True);
            Assert.That(folderFilter2.Value, Is.EqualTo("テストB"));
        }

        [Test]
        public void TestReadConfig_Nameless()
        {
            string targetPath = GetConfigPath("Nameless.config");
            ConfigInfo configInfo = ConfigFileManager.ReadConfig(targetPath);

            Assert.That(configInfo, Is.Not.Null);

            Assert.That(configInfo.FilterFileStringList[0].Name, Is.Null);
            Assert.That(configInfo.FilterFolderStringList[0].Name, Is.Null);
        }

        [Test]
        public void TestReadConfig_Flagless()
        {
            string targetPath = GetConfigPath("Flagless.config");
            ConfigInfo configInfo = ConfigFileManager.ReadConfig(targetPath);

            Assert.That(configInfo, Is.Not.Null);

            Assert.That(configInfo.FilterFileStringList[0].IsEnable, Is.False);
            Assert.That(configInfo.FilterFolderStringList[0].IsEnable, Is.False);
        }

        [Test]
        public void TestReadConfig_Valueless()
        {
            string targetPath = GetConfigPath("Valueless.config");
            ConfigInfo configInfo = ConfigFileManager.ReadConfig(targetPath);

            Assert.That(configInfo, Is.Not.Null);

            Assert.That(configInfo.FilterFileStringList[0].Value, Is.Empty);
            Assert.That(configInfo.FilterFolderStringList[0].Value, Is.Empty);
        }

        [Test]
        public void TestReadConfig_NoConfig()
        {
            string targetPath = GetConfigPath("NoConfig.config");
            ConfigInfo configInfo = ConfigFileManager.ReadConfig(targetPath);

            Assert.That(configInfo, Is.Not.Null);
            Assert.That(configInfo.FilterFileStringList.Count, Is.EqualTo(0));
            Assert.That(configInfo.FilterFolderStringList.Count, Is.EqualTo(0));
        }

        [Test]
        public void TestReadConfig_Shuffle()
        {
            string targetPath = GetConfigPath("Shuffle.config");
            ConfigInfo configInfo = ConfigFileManager.ReadConfig(targetPath);

            Assert.That(configInfo, Is.Not.Null);
            Assert.That(configInfo.FilterFileStringList, Is.Null);
            Assert.That(configInfo.FilterFolderStringList, Is.Null);
        }

        /// <summary>
        /// 設定ファイルパス取得
        /// </summary>
        /// <param name="configName"></param>
        /// <returns></returns>
        private static string GetConfigPath(string configName)
        {
            string executingAssembly = Assembly.GetExecutingAssembly().CodeBase.Replace("file:///", "");
            string executingFolder = executingAssembly.Replace(Path.GetFileName(executingAssembly), "");
            string targetPath = string.Format("{0}Config\\{1}", executingFolder, configName);
            return targetPath;
        }

        
    }
}
