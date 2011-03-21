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

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace VSArrange.Config
{
    /// <summary>
    /// 設定ファイルを読み書きするクラス
    /// </summary>
    public class ConfigFileManager
    {
        /// <summary>
        /// 設定ファイル読み込み
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static ConfigInfo ReadConfig(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }
            //  初回起動時
            if(!File.Exists(path))
            {
                return ConfigInfo.GetInstance();
            }
    
            XmlDocument configDocument = new XmlDocument();
            configDocument.Load(path);
            XmlElement rootElement = configDocument[ConfigConst.SECTION_ROOT];
            ConfigInfo configInfo = ConfigInfo.GetInstance();
            if(rootElement == null)
            {
                return configInfo;
            }
            
            //  フィルター情報
            XmlElement filtersElement = rootElement[ConfigConst.SECTION_FILTERS];
            if (filtersElement != null)
            {
                configInfo.FilterFileStringList = ReadConfigAsList(filtersElement, 
                    ConfigConst.SUB_SECTION_FILE, ConfigConst.DETAIL_SECTION_FILTER);
                configInfo.FilterFolderStringList = ReadConfigAsList(filtersElement,
                    ConfigConst.SUB_SECTION_FOLDER, ConfigConst.DETAIL_SECTION_FILTER);

                configInfo.FilterCompileStringList = ReadConfigAsList(filtersElement,
                    ConfigConst.SUB_SECTION_COMPILE, ConfigConst.DETAIL_SECTION_FILTER);
                configInfo.FilterResourceStringList = ReadConfigAsList(filtersElement,
                    ConfigConst.SUB_SECTION_RESOURCE, ConfigConst.DETAIL_SECTION_FILTER);
                configInfo.FilterContentsStringList = ReadConfigAsList(filtersElement,
                    ConfigConst.SUB_SECTION_CONTENTS, ConfigConst.DETAIL_SECTION_FILTER);
                configInfo.FilterNoActionStringList = ReadConfigAsList(filtersElement,
                    ConfigConst.SUB_SECTION_NO_ACTION, ConfigConst.DETAIL_SECTION_FILTER);

                configInfo.FilterNoCopyStringList = ReadConfigAsList(filtersElement,
                    ConfigConst.SUB_SECTION_NO_COPY, ConfigConst.DETAIL_SECTION_FILTER);
                configInfo.FilterEverytimeCopyStringList = ReadConfigAsList(filtersElement,
                    ConfigConst.SUB_SECTION_EVERYTIME_COPY, ConfigConst.DETAIL_SECTION_FILTER);
                configInfo.FilterCopyIfNewStringList = ReadConfigAsList(filtersElement,
                    ConfigConst.SUB_SECTION_COPY_IF_NEW, ConfigConst.DETAIL_SECTION_FILTER);
            }

            //  結果出力情報
            XmlElement outputResultElement = rootElement[ConfigConst.SECTION_OUTPUT_RESULT];
            if(outputResultElement != null)
            {
                IList<ConfigInfoDetail> outputResultWindowList = ReadConfigAsList(outputResultElement,
                    ConfigConst.SUB_SECTION_OUTPUT_WINDOW, ConfigConst.DETAIL_SECTION_DETAIL);
                if (outputResultWindowList != null && outputResultWindowList.Count > 0)
                {
                    configInfo.OutputResultWindow = outputResultWindowList[0];
                }

                IList<ConfigInfoDetail> outputResultFileList = ReadConfigAsList(outputResultElement, 
                    ConfigConst.SUB_SECTION_OUTPUT_FILE, ConfigConst.DETAIL_SECTION_DETAIL);
                if (outputResultFileList != null && outputResultFileList.Count > 0)
                {
                    configInfo.OutputResultFile = outputResultFileList[0];
                }
            }

            return configInfo;
        }

        /// <summary>
        /// 設定ファイルからフィルター情報を読み込む
        /// </summary>
        /// <param name="parentElement"></param>
        /// <param name="subSectionName"></param>
        /// <param name="detailSectionName"></param>
        /// <returns></returns>
        private static IList<ConfigInfoDetail> ReadConfigAsList(XmlNode parentElement,
            string subSectionName, string detailSectionName)
        {
            XmlElement filterElement = parentElement[subSectionName];
            if (filterElement == null)
            {
                return new List<ConfigInfoDetail>();
            }

            IList<ConfigInfoDetail> returnFilterList = new List<ConfigInfoDetail>();
            XmlNodeList filterNodeList = filterElement.GetElementsByTagName(detailSectionName);
            foreach (XmlNode node in filterNodeList)
            {
                ConfigInfoDetail newFilter = ReadConfigInfoDetail(node);
                returnFilterList.Add(newFilter);
            }
            return returnFilterList;
        }

        /// <summary>
        /// 設定ファイル内のの詳細設定を読み込む
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static ConfigInfoDetail ReadConfigInfoDetail(XmlNode node)
        {
            ConfigInfoDetail newFilter = new ConfigInfoDetail();
            XmlAttribute nameAttr = node.Attributes[ConfigConst.ATTRIBUTE_NAME];
            if (nameAttr != null)
            {
                newFilter.Name = nameAttr.Value;
            }

            XmlAttribute enableAttr = node.Attributes[ConfigConst.ATTRIBUTE_ENABLE];
            bool isEnable = false;
            if (enableAttr != null && bool.TryParse(enableAttr.Value, out isEnable))
            {
                newFilter.IsEnable = isEnable;
            }

            newFilter.Value = node.InnerText;

            return newFilter;
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
            XmlElement rootElement = configDocument.CreateElement(ConfigConst.SECTION_ROOT);
            configDocument.AppendChild(rootElement);
                
            //  フィルター設定
            XmlElement filtersElement = configDocument.CreateElement(ConfigConst.SECTION_FILTERS);
            AddToElement(filtersElement, configDocument, ConfigConst.SUB_SECTION_FILE, 
                ConfigConst.DETAIL_SECTION_FILTER, configInfo.FilterFileStringList);
            AddToElement(filtersElement, configDocument, ConfigConst.SUB_SECTION_FOLDER,
                ConfigConst.DETAIL_SECTION_FILTER, configInfo.FilterFolderStringList);
            AddToElement(filtersElement, configDocument, ConfigConst.SUB_SECTION_COMPILE,
                ConfigConst.DETAIL_SECTION_FILTER, configInfo.FilterCompileStringList);
            AddToElement(filtersElement, configDocument, ConfigConst.SUB_SECTION_RESOURCE,
                ConfigConst.DETAIL_SECTION_FILTER, configInfo.FilterResourceStringList);
            AddToElement(filtersElement, configDocument, ConfigConst.SUB_SECTION_CONTENTS,
                ConfigConst.DETAIL_SECTION_FILTER, configInfo.FilterContentsStringList);
            AddToElement(filtersElement, configDocument, ConfigConst.SUB_SECTION_NO_ACTION,
                ConfigConst.DETAIL_SECTION_FILTER, configInfo.FilterNoActionStringList);
            AddToElement(filtersElement, configDocument, ConfigConst.SUB_SECTION_NO_COPY,
                ConfigConst.DETAIL_SECTION_FILTER, configInfo.FilterNoCopyStringList);
            AddToElement(filtersElement, configDocument, ConfigConst.SUB_SECTION_EVERYTIME_COPY,
                ConfigConst.DETAIL_SECTION_FILTER, configInfo.FilterEverytimeCopyStringList);
            AddToElement(filtersElement, configDocument, ConfigConst.SUB_SECTION_COPY_IF_NEW,
                ConfigConst.DETAIL_SECTION_FILTER, configInfo.FilterCopyIfNewStringList);
            rootElement.AppendChild(filtersElement);

            //  結果出力設定
            XmlElement outputResultElement = configDocument.CreateElement(ConfigConst.SECTION_OUTPUT_RESULT);
            AddToElement(outputResultElement, configDocument,
                ConfigConst.SUB_SECTION_OUTPUT_WINDOW, ConfigConst.DETAIL_SECTION_DETAIL,
                configInfo.OutputResultWindow);
            AddToElement(outputResultElement, configDocument,
                ConfigConst.SUB_SECTION_OUTPUT_FILE, ConfigConst.DETAIL_SECTION_DETAIL,
                configInfo.OutputResultFile);
            rootElement.AppendChild(outputResultElement);
            
            configDocument.Save(path);
        }

        /// <summary>
        /// フィルター設定を追加する
        /// </summary>
        /// <param name="targetElement">追加対象のエレメント</param>
        /// <param name="configDocument"></param>
        /// <param name="subSectionName">サブセクション名</param>
        /// <param name="detailSectionName">詳細設定セクション名</param>
        /// <param name="configInfoDetails">フィルター情報リスト</param>
        private static void AddToElement(
            XmlNode targetElement, XmlDocument configDocument,
            string subSectionName, string detailSectionName,
            IEnumerable<ConfigInfoDetail> configInfoDetails)
        {
            XmlElement newChild = CreateChildElement(
                subSectionName, detailSectionName, configDocument, configInfoDetails);
            if (newChild != null)
            {
                targetElement.AppendChild(newChild);
            }
        }

        /// <summary>
        /// フィルター設定を追加する
        /// </summary>
        /// <param name="filtersElement">追加対象のエレメント</param>
        /// <param name="configDocument"></param>
        /// <param name="subSectionName">サブセクション名</param>
        /// <param name="detailSectionName">詳細設定セクション名</param>
        /// <param name="configInfoDetail">設定情報</param>
        private static void AddToElement(
            XmlNode filtersElement, XmlDocument configDocument,
            string subSectionName, string detailSectionName,
            ConfigInfoDetail configInfoDetail)
        {
            IList<ConfigInfoDetail> configInfoDetails = new List<ConfigInfoDetail>();
            configInfoDetails.Add(configInfoDetail);
            AddToElement(filtersElement, configDocument, subSectionName, 
                detailSectionName, configInfoDetails);
        }

        /// <summary>
        /// フィルターセクションに書き込む内容を作成する
        /// </summary>
        /// <param name="subSectionName"></param>
        /// <param name="detailSectionName"></param>
        /// <param name="configDocument"></param>
        /// <param name="configInfoDetails"></param>
        /// <returns></returns>
        private static XmlElement CreateChildElement(
            string subSectionName, string detailSectionName,
            XmlDocument configDocument, IEnumerable<ConfigInfoDetail> configInfoDetails)
        {
            if (subSectionName == null) throw new ArgumentNullException("subSectionName");
            if (detailSectionName == null) throw new ArgumentNullException("detailSectionName");
            if (configDocument == null) throw new ArgumentNullException("configDocument");
            if (configInfoDetails == null) throw new ArgumentNullException("configInfoDetails");

            XmlElement filtersElement = configDocument.CreateElement(subSectionName);
            foreach (ConfigInfoDetail filter in configInfoDetails)
            {
                XmlElement filterElement = configDocument.CreateElement(detailSectionName);

                XmlAttribute filterNameAttr = configDocument.CreateAttribute(ConfigConst.ATTRIBUTE_NAME);
                filterNameAttr.Value = filter.Name;
                filterElement.Attributes.Append(filterNameAttr);

                XmlAttribute filterEnableAttr = configDocument.CreateAttribute(ConfigConst.ATTRIBUTE_ENABLE);
                filterEnableAttr.Value = filter.IsEnable.ToString();
                filterElement.Attributes.Append(filterEnableAttr);

                filterElement.InnerText = filter.Value;
                filtersElement.AppendChild(filterElement);
            }

            return filtersElement;
        }
    }
}
