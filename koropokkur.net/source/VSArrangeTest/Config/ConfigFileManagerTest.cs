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
using NUnit.Framework;
using VSArrange.Config;

namespace VSArrangeTest.Config
{
    [TestFixture]
    public class ConfigFileManagerTest
    {
        [Test]
        public void TestWriteConfig_Full()
        {
            //  ## Arrange ##
            ConfigInfoFilter fileFilter1_1 = new ConfigInfoFilter();
            fileFilter1_1.Name = "Subversion用フィルタ";
            fileFilter1_1.IsEnable = true;
            fileFilter1_1.FilterString = ".svn";

            ConfigInfoFilter fileFilter1_2 = new ConfigInfoFilter();
            fileFilter1_2.Name = "VisualStudioファイル用フィルタ";
            fileFilter1_2.IsEnable = false;
            fileFilter1_2.FilterString = ".csproj";

            IList<ConfigInfoFilter> filterList1 = new List<ConfigInfoFilter>();
            filterList1.Add(fileFilter1_1);
            filterList1.Add(fileFilter1_2);

            ConfigInfoFilter fileFilter2_1 = new ConfigInfoFilter();
            fileFilter2_1.Name = "Re#用フィルタ";
            fileFilter2_1.IsEnable = false;
            fileFilter2_1.FilterString = "ReSharper";

            ConfigInfoFilter fileFilter2_2 = new ConfigInfoFilter();
            fileFilter2_2.Name = "VisualStudioフォルダ用フィルタ";
            fileFilter2_2.IsEnable = true;
            fileFilter2_2.FilterString = "(bin|obj)";

            IList<ConfigInfoFilter> filterList2 = new List<ConfigInfoFilter>();
            filterList2.Add(fileFilter2_1);
            filterList2.Add(fileFilter2_2);

            ConfigInfo configInfo = ConfigInfo.GetInstance();
            configInfo.FilterFileStringList = filterList1;
            configInfo.FilterFolderStringList = filterList2;

            //  ## Act ##
            ConfigFileManager.WriteConfig("c:\\temp\\VSArrange.config", configInfo);
        }
    }
}
