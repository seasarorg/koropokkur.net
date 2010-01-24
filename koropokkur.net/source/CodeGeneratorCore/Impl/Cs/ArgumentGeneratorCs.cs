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

using System.Text;
using CodeGeneratorCore.Enum;

namespace CodeGeneratorCore.Impl.Cs
{
    /// <summary>
    /// 引数情報クラス
    /// </summary>
    public class ArgumentGeneratorCs : ICodeGenerator
    {
        #region property
        protected EnumArgumentReference _reference;
        /// <summary>
        /// 引数参照方法列挙体
        /// </summary>
        public EnumArgumentReference Reference
        {
            get { return _reference; }
            set { _reference = value; }
        }

        protected string _argumentTypeName;
        /// <summary>
        /// 引数の型
        /// </summary>
        public string ArgumentTypeName
        {
            get { return _argumentTypeName; }
            set { _argumentTypeName = value; }
        }

        protected string _argumentName;
        /// <summary>
        /// 引数名
        /// </summary>
        public string ArgumentName
        {
            get { return _argumentName; }
            set { _argumentName = value; }
        }

        protected string _comment;
        /// <summary>
        /// 引数コメント
        /// </summary>
        public string Comment
        {
            get { return _comment; }
            set { _comment = value; }
        }

        private bool _isNotNull;
        /// <summary>
        /// Nullを禁止するか？
        /// </summary>
        public bool IsNotNull
        {
            get { return _isNotNull; }
            set { _isNotNull = value; }
        }

        public bool _isCreateInstance;
        /// <summary>
        /// 引数がnullだった場合インスタンスを生成するか？
        /// </summary>
        public bool IsCreateInstance
        {
            get { return _isCreateInstance; }
            set { _isCreateInstance = value; }
        }
        #endregion

        #region ICodeGenerator メンバ

        /// <summary>
        /// 引数の生成
        /// </summary>
        /// <returns></returns>
        public string GenerateCode(string startIndent)
        {
            StringBuilder builder = new StringBuilder();
            if(Reference == EnumArgumentReference.Params)
            {
                builder.Append("params object[]");
            }
            else
            { 
                if (Reference == EnumArgumentReference.Ref)
                {
                    builder.Append("ref ");
                }
                else if (Reference == EnumArgumentReference.Out)
                {
                    builder.Append("out ");
                }

                builder.Append(ArgumentTypeName);
            }
            builder.Append(" ").Append(ArgumentName);
            return builder.ToString();
        }

        #endregion
    }
}