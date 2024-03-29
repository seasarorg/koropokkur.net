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

using System.Collections.Generic;
using System.Text;

namespace CodeGeneratorCore.Impl.Cs
{
    /// <summary>
    /// １行分のコードを生成する
    /// </summary>
    public class LineGeneratorCs : ICodeGenerator
    {
        protected const string SECTION_NAME = "line";
        protected const string SECTION_PARTS_NAME = "item";

        /// <summary>
        /// 行を構成する要素
        /// </summary>
        protected readonly IList<string> _items = new List<string>();
        /// <summary>
        /// 行を構成する要素の集合
        /// 各要素はGenerateCodeメソッドで空白区切りで接続されて出力される
        /// </summary>
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

        #endregion
    }
}
