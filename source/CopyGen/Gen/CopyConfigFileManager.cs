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
using System.Xml;
using CodeGeneratorCore.Enum;
using CopyGen.Exception;

namespace CopyGen.Gen
{
    /// <summary>
    /// 設定ファイルを読み書きするクラス
    /// </summary>
    /// <remarks>
    /// 諸事情により、
    /// ここだけは「コピー先」＝「target」として定義しています。
    /// （他は「dest」）
    /// </remarks>
    public class CopyConfigFileManager
    {
        #region セクション名定義

        private const string SECTION_ROOT = "root";

        private const string SECTION_IS_METHOD = "isMethod";
        private const string SECTION_METHOD = "methodDefinition";
        private const string SECTION_SOURCE = "source";
        private const string SECTION_TARGET = "target";
        private const string SECTION_IS_CONFIRM = "isConfirm";

        private const string SECTION_METHOD_VISIBILITY = "visibility";
        private const string SECTION_METHOD_OPTION = "option";
        private const string SECTION_METHOD_NAME = "methodName";

        private const string SECTION_SOURCE_WAY = "copySource";
        private const string SECTION_SOURCE_ARG_NAME = "argName";
        private const string SECTION_SOURCE_IS_NOT_NULL = "isNotNull";

        private const string SECTION_TARGET_WAY = "copyTarget";
        private const string SECTION_TARGET_ARG_NAME = "argName";
        private const string SECTION_TARGET_IS_NOT_NULL = "isNotNull";
        #endregion

        /// <summary>
        /// 設定ファイル読み込み
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static CopyInfo ReadConfig(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }

            XmlDocument configDocument = new XmlDocument();
            configDocument.Load(path);
            CopyInfo copyInfo = new CopyInfo();

            XmlNode rootNode = configDocument[SECTION_ROOT];
            if(rootNode == null)
            {
                throw new ConfigNotFoundException();
            }

            XmlElement isMethodElement = rootNode[SECTION_IS_METHOD];
            SetupForReadIsMethod(copyInfo, isMethodElement);

            XmlElement isConfirmElement = rootNode[SECTION_IS_CONFIRM];
            SetupForReadIsConfirm(copyInfo, isConfirmElement);

            XmlElement methodElement = rootNode[SECTION_METHOD];
            SetupForReadMethod(copyInfo, methodElement);

            XmlElement sourceElement = rootNode[SECTION_SOURCE];
            SetupForReadSource(copyInfo, sourceElement);

            XmlElement targetElement = rootNode[SECTION_TARGET];
            SetupForReadTarget(copyInfo, targetElement);

            return copyInfo;
        }

        #region 各子ノードの読み込み
        /// <summary>
        /// コピー先情報の設定
        /// </summary>
        /// <param name="copyInfo"></param>
        /// <param name="node"></param>
        private static void SetupForReadTarget(CopyInfo copyInfo, XmlNode node)
        {
            if(node == null)
            {
                return;
            }

            XmlNode copyTargetNode = node[SECTION_TARGET_WAY];
            if(copyTargetNode != null)
            {
                int copyTarget;
                if (int.TryParse(copyTargetNode.InnerText, out copyTarget))
                {
                    copyInfo.CopyDest = (EnumCopyDest) copyTarget;
                }
            }

            XmlNode argNameNode = node[SECTION_TARGET_ARG_NAME];
            if(argNameNode != null)
            {
                copyInfo.DestArgumentName = argNameNode.InnerText;
            }

            //XmlNode typeNameNode = node[SECTION_TARGET_TYPE_NAME];
            //if(typeNameNode != null)
            //{
            //    copyInfo.DestTypeName = typeNameNode.InnerText;
            //}

            XmlNode isNotNullNode = node[SECTION_TARGET_IS_NOT_NULL];
            if(isNotNullNode != null)
            {
                bool isNotNull;
                if(bool.TryParse(isNotNullNode.InnerText, out isNotNull))
                {
                    copyInfo.IsNotNullDest = isNotNull;
                }
            }
        }

        /// <summary>
        /// コピー元情報の設定
        /// </summary>
        /// <param name="copyInfo"></param>
        /// <param name="node"></param>
        private static void SetupForReadSource(CopyInfo copyInfo, XmlNode node)
        {
            if(node == null)
            {
                return;
            }

            XmlNode copySourceNode = node[SECTION_SOURCE_WAY];
            if(copySourceNode != null)
            {
                int copySource;
                if (int.TryParse(copySourceNode.InnerText, out copySource))
                {
                    copyInfo.CopySource = (EnumCopySource)copySource;
                }
            }

            XmlNode argNameNode = node[SECTION_SOURCE_ARG_NAME];
            if(argNameNode != null)
            {
                copyInfo.SourceArgumentName = argNameNode.InnerText;
            }

            //XmlNode nameNode = node[SECTION_SOURCE_TYPE_NAME];
            //if(nameNode != null)
            //{
            //    copyInfo.SourceTypeName = nameNode.InnerText;
            //}

            XmlNode isNotNullNode = node[SECTION_SOURCE_IS_NOT_NULL];
            if(isNotNullNode != null)
            {
                bool isNotNull;
                if(bool.TryParse(isNotNullNode.InnerText, out isNotNull))
                {
                    copyInfo.IsNotNullSource = isNotNull;
                }
            }
        }

        /// <summary>
        /// メソッド生成情報を設定
        /// </summary>
        /// <param name="copyInfo"></param>
        /// <param name="node"></param>
        private static void SetupForReadMethod(CopyInfo copyInfo, XmlNode node)
        {
            if (node == null)
            {
                return;
            }

            XmlNode visibilityNode = node[SECTION_METHOD_VISIBILITY];
            if (visibilityNode != null)
            {
                int visiblity;
                if (int.TryParse(visibilityNode.InnerText, out visiblity))
                {
                    copyInfo.Visibility = (EnumVisibility)visiblity;
                }
            }

            XmlNode optionNode = node[SECTION_METHOD_OPTION];
            if (optionNode != null)
            {
                int option;
                if (int.TryParse(optionNode.InnerText, out option))
                {
                    copyInfo.MethodOption = (EnumMethodOption)option;
                }
            }

            XmlNode nameNode = node[SECTION_METHOD_NAME];
            if(nameNode != null)
            {
                copyInfo.MethodName = nameNode.InnerText;
            }
        }

        /// <summary>
        /// 毎回設定の確認を行うか設定
        /// </summary>
        /// <param name="copyInfo"></param>
        /// <param name="node"></param>
        private static void SetupForReadIsConfirm(CopyInfo copyInfo, XmlNode node)
        {
            if (node == null)
            {
                return;
            }

            bool isCinfirm;
            if (bool.TryParse(node.InnerText, out isCinfirm))
            {
                copyInfo.IsEverytimeConfirm = isCinfirm;
            }
        }

        /// <summary>
        /// メソッドとして出力するか設定
        /// </summary>
        /// <param name="copyInfo"></param>
        /// <param name="node"></param>
        private static void SetupForReadIsMethod(CopyInfo copyInfo, XmlNode node)
        {
            if(node == null)
            {
                return;
            }

            bool isMethod;
            if(bool.TryParse(node.InnerText, out isMethod))
            {
                copyInfo.IsOutputMethod = isMethod;
            }
        }
        #endregion

        /// <summary>
        /// 設定情報書き込み
        /// </summary>
        /// <param name="path"></param>
        /// <param name="copyInfo"></param>
        public static void WriteConfig(string path, CopyInfo copyInfo)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("path");
            }

            if (copyInfo == null)
            {
                return;
            }

            XmlDocument configDocument = new XmlDocument();
            XmlNode rootNode = configDocument.AppendChild(configDocument.CreateElement(SECTION_ROOT));

            XmlElement isMethodElement = configDocument.CreateElement(SECTION_IS_METHOD);
            SetupForWriteIsMethod(copyInfo, isMethodElement);
            rootNode.AppendChild(isMethodElement);

            XmlElement isConfirmElement = configDocument.CreateElement(SECTION_IS_CONFIRM);
            SetupForWriteIsConfirm(copyInfo, isConfirmElement);
            rootNode.AppendChild(isConfirmElement);

            XmlElement methodElement = configDocument.CreateElement(SECTION_METHOD);
            SetupForWriteMethod(configDocument, copyInfo, methodElement);
            rootNode.AppendChild(methodElement);

            XmlElement sourceElement = configDocument.CreateElement(SECTION_SOURCE);
            SetupForWriteSource(configDocument, copyInfo, sourceElement);
            rootNode.AppendChild(sourceElement);

            XmlElement targetElement = configDocument.CreateElement(SECTION_TARGET);
            SetupForWriteTarget(configDocument, copyInfo, targetElement);
            rootNode.AppendChild(targetElement);

            configDocument.Save(path);
        }

        #region 各子ノードへの書き込み設定

        /// <summary>
        /// 保存情報としてコピー先情報を設定
        /// </summary>
        /// <param name="document"></param>
        /// <param name="copyInfo"></param>
        /// <param name="node"></param>
        private static void SetupForWriteTarget(XmlDocument document, CopyInfo copyInfo, XmlNode node)
        {
            XmlElement isReturnNode = document.CreateElement(SECTION_TARGET_WAY);
            isReturnNode.InnerText = ((int) copyInfo.CopyDest).ToString();
            node.AppendChild(isReturnNode);

            XmlElement argNameNode = document.CreateElement(SECTION_TARGET_ARG_NAME);
            argNameNode.InnerText = copyInfo.DestArgumentName;
            node.AppendChild(argNameNode);

            XmlElement isNotNullNode = document.CreateElement(SECTION_TARGET_IS_NOT_NULL);
            isNotNullNode.InnerText = copyInfo.IsNotNullDest.ToString();
            node.AppendChild(isNotNullNode);
        }

        /// <summary>
        /// 保存情報としてコピー元情報を設定
        /// </summary>
        /// <param name="document"></param>
        /// <param name="copyInfo"></param>
        /// <param name="node"></param>
        private static void SetupForWriteSource(XmlDocument document, CopyInfo copyInfo, XmlNode node)
        {
            XmlElement hasArgNode = document.CreateElement(SECTION_SOURCE_WAY);
            hasArgNode.InnerText = ((int) copyInfo.CopySource).ToString();
            node.AppendChild(hasArgNode);

            XmlElement argNameNode = document.CreateElement(SECTION_SOURCE_ARG_NAME);
            argNameNode.InnerText = copyInfo.SourceArgumentName;
            node.AppendChild(argNameNode);

            XmlElement isNotNullNode = document.CreateElement(SECTION_SOURCE_IS_NOT_NULL);
            isNotNullNode.InnerText = copyInfo.IsNotNullSource.ToString();
            node.AppendChild(isNotNullNode);
        }

        /// <summary>
        /// 保存情報としてコピーメソッド定義情報を設定
        /// </summary>
        /// <param name="document"></param>
        /// <param name="copyInfo"></param>
        /// <param name="node"></param>
        private static void SetupForWriteMethod(XmlDocument document, CopyInfo copyInfo, XmlNode node)
        {
            XmlElement visibilityNode = document.CreateElement(SECTION_METHOD_VISIBILITY);
            visibilityNode.InnerText = ((int)copyInfo.Visibility).ToString();
            node.AppendChild(visibilityNode);

            XmlElement optionNode = document.CreateElement(SECTION_METHOD_OPTION);
            optionNode.InnerText = ((int)copyInfo.MethodOption).ToString();
            node.AppendChild(optionNode);

            XmlElement nameNode = document.CreateElement(SECTION_METHOD_NAME);
            nameNode.InnerText = copyInfo.MethodName;
            node.AppendChild(nameNode);
        }

        /// <summary>
        /// 保存情報として毎回設定確認をするかフラグを設定
        /// </summary>
        /// <param name="copyInfo"></param>
        /// <param name="node"></param>
        private static void SetupForWriteIsConfirm(CopyInfo copyInfo, XmlNode node)
        {
            node.InnerText = copyInfo.IsEverytimeConfirm.ToString();
        }

        /// <summary>
        /// 保存情報としてメソッド出力するかフラグを設定
        /// </summary>
        /// <param name="copyInfo"></param>
        /// <param name="node"></param>
        private static void SetupForWriteIsMethod(CopyInfo copyInfo, XmlNode node)
        {
            node.InnerText = copyInfo.IsOutputMethod.ToString();
        }

        #endregion
    }
}
