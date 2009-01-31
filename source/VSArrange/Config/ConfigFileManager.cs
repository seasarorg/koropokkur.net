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
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace VSArrange.Config
{
    /// <summary>
    /// 設定ファイルを読み書きするクラス
    /// </summary>
    public class ConfigFileManager
    {
        private const string SECTION_FILTERS = "filters";
        private const string SECTION_FILTER = "filter";
        private const string SUB_SECTION_FILE = "file";
        private const string SUB_SECTION_FOLDER = "folder";
        private const string ATTRIBUTE_FILTER_NAME = "name";
        private const string ATTRIBUTE_FILTER_ENABLE = "enable";

        /// <summary>
        /// 設定ファイル読み込み
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static ConfigInfo ReadConfig(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("path");
            }
            //  初回起動時
            if(!File.Exists(path))
            {
                return new ConfigInfo();
            }
    
            XmlDocument configDocument = new XmlDocument();
            configDocument.Load(path);
            ConfigInfo configInfo = new ConfigInfo();
            
            //  フィルター情報
            XmlElement filtersElement = configDocument[SECTION_FILTERS];
            if (filtersElement != null)
            {
                XmlElement fileFilterElement = filtersElement[SUB_SECTION_FILE];
                if(fileFilterElement != null)
                {
                    configInfo.FilterFileStringList = ReadConfigFilter(fileFilterElement);
                }

                XmlElement folderFilterElement = filtersElement[SUB_SECTION_FOLDER];
                if (folderFilterElement != null)
                {
                    configInfo.FilterFolderStringList = ReadConfigFilter(folderFilterElement);
                }
            }

            return configInfo;
        }

        /// <summary>
        /// 設定ファイルからフィルター情報を読み込む
        /// </summary>
        /// <param name="filterElement"></param>
        /// <returns></returns>
        private static IList<ConfigInfoFilter> ReadConfigFilter(XmlElement filterElement)
        {
            IList<ConfigInfoFilter> returnFilterList = new List<ConfigInfoFilter>();
            XmlNodeList filterNodeList = filterElement.GetElementsByTagName(SECTION_FILTER);
            foreach (XmlNode filterNode in filterNodeList)
            {
                ConfigInfoFilter newFilter = new ConfigInfoFilter();
                XmlAttribute nameAttr = filterNode.Attributes[ATTRIBUTE_FILTER_NAME];
                if (nameAttr != null)
                {
                    newFilter.Name = nameAttr.Value;
                }

                XmlAttribute enableAttr = filterNode.Attributes[ATTRIBUTE_FILTER_ENABLE];
                bool isEnable = false;
                if (enableAttr != null && bool.TryParse(enableAttr.Value, out isEnable))
                {
                    newFilter.IsEnable = isEnable;
                }

                newFilter.FilterString = filterNode.InnerText;
                returnFilterList.Add(newFilter);
            }
            return returnFilterList;
        }

        /// <summary>
        /// 設定情報書き込み
        /// </summary>
        /// <param name="path"></param>
        /// <param name="configInfo"></param>
        public static void WriteConfig(string path, ConfigInfo configInfo)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("path");
            }

            if(configInfo == null)
            {
                return;
            }

            XmlDocument configDocument = new XmlDocument();
            XmlElement filtersElement = configDocument.CreateElement(SECTION_FILTERS);

            //  ファイル用フィルター
            XmlElement fileFiltersElement = CreateFilterSectionElement(
                SUB_SECTION_FILE, configDocument, configInfo.FilterFileStringList);
            if (fileFiltersElement != null)
            {
                filtersElement.AppendChild(fileFiltersElement);
            }

            //  フォルダ用フィルター
            XmlElement folderFiltersElement = CreateFilterSectionElement(
                SUB_SECTION_FOLDER, configDocument, configInfo.FilterFolderStringList);
            if (folderFiltersElement != null)
            {
                filtersElement.AppendChild(folderFiltersElement);
            }

            configDocument.AppendChild(filtersElement);
            configDocument.Save(path);
        }

        /// <summary>
        /// フィルターセクションに書き込む内容を作成する
        /// </summary>
        /// <param name="subSectionName"></param>
        /// <param name="configDocument"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        private static XmlElement CreateFilterSectionElement(
            string subSectionName, XmlDocument configDocument, IEnumerable<ConfigInfoFilter> filters)
        {
            if(filters == null)
            {
                return null;
            }

            if(string.IsNullOrEmpty(subSectionName))
            {
                throw new ArgumentNullException("subSectionName");
            }

            if(configDocument == null)
            {
                throw new ArgumentNullException("configDocument");
            }

            XmlElement filtersElement = configDocument.CreateElement(subSectionName);
            foreach (ConfigInfoFilter filter in filters)
            {
                XmlElement filterElement = configDocument.CreateElement(SECTION_FILTER);

                XmlAttribute filterNameAttr = configDocument.CreateAttribute(ATTRIBUTE_FILTER_NAME);
                filterNameAttr.Value = filter.Name;
                filterElement.Attributes.Append(filterNameAttr);

                XmlAttribute filterEnableAttr = configDocument.CreateAttribute(ATTRIBUTE_FILTER_ENABLE);
                filterEnableAttr.Value = filter.IsEnable.ToString();
                filterElement.Attributes.Append(filterEnableAttr);

                filterElement.InnerText = filter.FilterString;
                filtersElement.AppendChild(filterElement);
            }

            return filtersElement;
        }
    }
}
