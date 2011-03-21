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
using CodeGeneratorCore.Enum;

namespace CodeGeneratorCore.Impl.Cs
{
    /// <summary>
    /// メソッド生成クラス
    /// </summary>
    public class MethodGeneratorCs : ICodeGenerator
    {
        protected const string HEADER_COMMENT = "/// ";
        protected const string SECTION_SUMMARY = "summary";
        protected const string SECTION_PARAM = "param";
        protected const string SECTION_RETURN = "returns";
        protected const string ATTRIBUTE_NAME = "name";

        #region プロパティ

        private string _methodComment;
        /// <summary>
        /// メソッドコメント
        /// </summary>
        public string MethodComment
        {
            get { return _methodComment; }
            set { _methodComment = value; }
        }

        private string _returnComment;
        /// <summary>
        /// 戻り値コメント
        /// </summary>
        public string ReturnComment
        {
            get { return _returnComment; }
            set { _returnComment = value; }
        }

        //  例外など他のヘッダコメントは現時点では省略

        private EnumVisibility _visibility;
        /// <summary>
        /// アクセス修飾子
        /// </summary>
        public EnumVisibility Visibility
        {
            get { return _visibility; }
            set { _visibility = value; }
        }

        private EnumMethodOption _methodOption;
        /// <summary>
        /// メソッド付加情報
        /// </summary>
        public EnumMethodOption MethodOption
        {
            get { return _methodOption; }
            set { _methodOption = value; }
        }

        private string _methodName;
        /// <summary>
        /// メソッド名
        /// </summary>
        public string MethodName
        {
            get { return _methodName; }
            set { _methodName = value; }
        }

        private string _returnTypeName;
        /// <summary>
        /// 戻り値の型
        /// </summary>
        public string ReturnTypeName
        {
            get { return _returnTypeName; }
            set { _returnTypeName = value; }
        }

        private readonly IList<ArgumentGeneratorCs> _arguments
            = new List<ArgumentGeneratorCs>();
        /// <summary>
        /// 引数
        /// </summary>
        public IList<ArgumentGeneratorCs> Arguments
        {
            get { return _arguments; }
        }

        private readonly GeneratorColleciton _lines = new GeneratorColleciton();
        /// <summary>
        /// メソッド内の行
        /// </summary>
        public GeneratorColleciton Lines
        {
            get { return _lines; }
        }
        #endregion

        #region ICodeGenerator メンバ

        /// <summary>
        /// メソッドコードの生成
        /// </summary>
        /// <returns></returns>
        public string GenerateCode(string startIndent)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(startIndent).AppendLine(GenerateHeaderString(startIndent));
            builder.Append(startIndent).AppendLine(GenerateMethodDefinition());
            builder.Append(startIndent).AppendLine("{");

            string methodLineIndent = startIndent + "\t";
            string assertNullLines = GenerateAssertNull(methodLineIndent);
            if(!string.IsNullOrEmpty(assertNullLines))
            {
                builder.AppendLine(assertNullLines);
            }
   
            foreach (ICodeGenerator codeGenerator in Lines)
            {
                if (codeGenerator != null)
                {
                    builder.AppendLine(codeGenerator.GenerateCode(methodLineIndent));
                }
            }
            builder.Append(startIndent).AppendLine("}");
            return builder.ToString();
        }

        #endregion

        /// <summary>
        /// ヘッダコメントの生成
        /// </summary>
        /// <returns></returns>
        protected virtual string GenerateHeaderString(string startIndent)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(HEADER_COMMENT).AppendLine(GetSectionStart(SECTION_SUMMARY));
            if(startIndent != null)
            {
                builder.Append(startIndent);
            }
            builder.Append(HEADER_COMMENT).AppendLine(MethodComment);
            if (startIndent != null)
            {
                builder.Append(startIndent);
            }
            builder.Append(HEADER_COMMENT).Append(GetSectionEnd(SECTION_SUMMARY));
            foreach (ArgumentGeneratorCs argument in Arguments)
            {
                builder.AppendLine();
                if(startIndent != null)
                {
                    builder.Append(startIndent);
                }
                builder.Append(HEADER_COMMENT);
                builder.Append(GetParamStart(argument.ArgumentName));
                builder.Append(argument.Comment);
                if(argument.IsNotNull)
                {
                    builder.Append("(NotNull)");
                }
                builder.Append(GetSectionEnd(SECTION_PARAM));
            }

            if(ReturnTypeName != "void")
            {
                builder.AppendLine();
                if (startIndent != null)
                {
                    builder.Append(startIndent);
                }
                builder.Append(GetOneLineSection(SECTION_RETURN, ReturnComment));
            }

            return builder.ToString();
        }

        /// <summary>
        /// メソッド定義の生成
        /// </summary>
        /// <returns></returns>
        protected virtual string GenerateMethodDefinition()
        {
            StringBuilder builder = new StringBuilder();
            switch (Visibility)
            {
                case EnumVisibility.Public:
                    builder.Append("public");
                    break;
                case EnumVisibility.Protected:
                    builder.Append("protected");
                    break;
                case EnumVisibility.Private:
                    builder.Append("private");
                    break;
                case EnumVisibility.Internal:
                    builder.Append("internal");
                    break;
            }
            builder.Append(" ");

            switch (_methodOption)
            {
                case EnumMethodOption.Static:
                    builder.Append("static").Append(" ");
                    break;
                case EnumMethodOption.Virtual:
                    builder.Append("virtual").Append(" ");
                    break;
                case EnumMethodOption.Override:
                    builder.Append("override").Append(" ");
                    break;
                case EnumMethodOption.Abstract:
                    builder.Append("abstract").Append(" ");
                    break;
            }

            builder.Append(ReturnTypeName).Append(" ");
            builder.Append(MethodName).Append("(");
            foreach (ArgumentGeneratorCs argument in Arguments)
            {
                builder.Append(argument.GenerateCode("")).Append(", ");
            }
            
            if (Arguments.Count > 0)
            {
                //  最後を引数の終わり「)」に置換
                builder.Remove(builder.Length - 1, 1);
                builder[builder.Length - 1] = ')';
            }
            else
            {
                //  引き数がないときはそのままカッコを閉じる
                builder.Append(")");
            }
            return builder.ToString();
        }
        
        /// <summary>
        /// Nullチェック処理の生成
        /// </summary>
        /// <param name="indent"></param>
        /// <returns></returns>
        protected virtual string GenerateAssertNull(string indent)
        {
            StringBuilder builder = new StringBuilder();
            foreach (ArgumentGeneratorCs argument in Arguments)
            {
                if (argument.IsNotNull)
                {
                    if(!string.IsNullOrEmpty(indent))
                    {
                        builder.Append(indent);
                    }
                    builder.Append("if(").Append(argument.ArgumentName);
                    builder.Append(" == null) { throw new System.ArgumentNullException(\"");
                    builder.Append(argument.ArgumentName).AppendLine("\"); }");
                }
            }
            return builder.ToString();
        }

        /// <summary>
        /// ヘッダコメント（引数）開始
        /// </summary>
        /// <param name="argumentName"></param>
        /// <returns></returns>
        protected virtual string GetParamStart(string argumentName)
        {
            return string.Format("<{0} {1}=\"{2}\">", SECTION_PARAM, ATTRIBUTE_NAME, argumentName);
        }

        /// <summary>
        /// ヘッダコメント開始
        /// </summary>
        /// <param name="sectionName"></param>
        /// <returns></returns>
        protected virtual string GetSectionStart(string sectionName)
        {
            return string.Format("<{0}>", sectionName);
        }

        /// <summary>
        /// ヘッダコメント終了
        /// </summary>
        /// <param name="sectionName"></param>
        /// <returns></returns>
        protected virtual string GetSectionEnd(string sectionName)
        {
            return string.Format("</{0}>", sectionName);
        }

        /// <summary>
        /// １行ヘッダコメント
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        protected virtual string GetOneLineSection(string sectionName, string comment)
        {
            return string.Format("{0}{1}{2}{3}", HEADER_COMMENT,
                                 GetSectionStart(sectionName), comment, GetSectionEnd(sectionName));
        }
    }
}
