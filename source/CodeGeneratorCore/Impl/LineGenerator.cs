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
using System.Text;
using System.Xml;

namespace CodeGeneratorCore.Impl
{
    /// <summary>
    /// １行分のコードを生成する
    /// </summary>
    public class LineGenerator : ICodeGenerator
    {
        protected const string SECTION_NAME = "line";
        protected const string SECTION_PARTS_NAME = "item";

        /// <summary>
        /// 行を構成する要素
        /// </summary>
        protected readonly IList<string> _items = new List<string>();
        public IList<string> Items
        {
            get { return _items; }
        }

        #region ICodeGenerator メンバ

        /// <summary>
        /// 生成したコードを返す
        /// </summary>
        /// <returns></returns>
        public virtual string GenerateCode(string startIndent)
        {
            StringBuilder codeBuilder = new StringBuilder();
            if(startIndent != null)
            {
                codeBuilder.Append(startIndent);    
            }
            
            foreach (string item in Items)
            {
                codeBuilder.Append(item).Append(" ");
            }
            if(Items.Count > 0)
            {
                //  最後の空白分を末尾文字に置き換える
                codeBuilder[codeBuilder.Length - 1] = ';';
            }
            return codeBuilder.ToString();
        }

        /// <summary>
        /// 設定ファイル用XmlElementを返す
        /// </summary>
        /// <returns></returns>
        public virtual XmlElement CreateXmlElement(XmlDocument document)
        {
            XmlElement element = document.CreateElement(SECTION_NAME);
            foreach (string item in Items)
            {
                XmlNode node = document.CreateElement(SECTION_PARTS_NAME);
                node.Value = item;
                element.AppendChild(node);
            }
            return element;
        }

        #endregion
    }
}
