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

using System.IO;
using CopyGen.Exception;
using CopyGen.Gen;
using CopyGen.Gen.Impl.Cs;
using CopyGen.Gen.Impl.Vb;

namespace CopyGen.Util
{
    /// <summary>
    /// 言語関係のユーティリティクラス
    /// </summary>
    public class ProgramLanguageUtils
    {
        /// <summary>
        /// C#コードのファイルか判定
        /// </summary>
        /// <param name="codeFilePath"></param>
        /// <returns></returns>
        public static bool IsCS(string codeFilePath)
        {
            return IsSameExtension(codeFilePath, EXT_CS);
        }

        /// <summary>
        /// VBのファイルか判定
        /// </summary>
        /// <param name="codeFilePath"></param>
        /// <returns></returns>
        public static bool IsVB(string codeFilePath)
        {
            return IsSameExtension(codeFilePath, EXT_VB);
        }

        /// <summary>
        /// コピー処理コード生成に対応している拡張子か判定
        /// </summary>
        /// <param name="codeFilePath"></param>
        /// <returns></returns>
        public static bool IsEnableLanguage(string codeFilePath)
        {
            return (IsCS(codeFilePath) || IsVB(codeFilePath));
        }

        /// <summary>
        /// コード生成クラスのインスタンスを生成する
        /// </summary>
        /// <param name="codeFilePath"></param>
        /// <returns></returns>
        public static ICopyCodeBuildFactory CreateCopyCodePartsBuilder(string codeFilePath)
        {
            if(IsCS(codeFilePath))
            {
                return new CopyCodeBuildFactoryCs();
            }
            
            if(IsVB(codeFilePath))
            {
                return new CopyCodeBuildFactoryVb();
            }

            throw new NotSupportLanguageException();
        }


        #region private
        /// <summary>
        /// C#コードファイルの拡張子
        /// </summary>
        private static string EXT_CS = ".cs";

        /// <summary>
        /// VBコードファイルの拡張子
        /// </summary>
        private static string EXT_VB = ".vb";

        /// <summary>
        /// 拡張子が想定しているものと同じか判定
        /// </summary>
        /// <param name="codeFilePath"></param>
        /// <param name="targetExtension"></param>
        /// <returns></returns>
        private static bool IsSameExtension(string codeFilePath, string targetExtension)
        {
            string ext = Path.GetExtension(codeFilePath);
            return (ext == targetExtension);
        }
        #endregion
    }
}
