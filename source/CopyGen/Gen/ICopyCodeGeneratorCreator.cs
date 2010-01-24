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

using CodeGeneratorCore;

namespace CopyGen.Gen
{
    /// <summary>
    /// コピー処理の部分部分を出力するインターフェース
    /// </summary>
    public interface ICopyCodeGeneratorCreator
    {
        /// <summary>
        /// コピーメソッド生成オブジェクトの生成
        /// </summary>
        /// <param name="copyInfo"></param>
        /// <param name="propertyCodeInfo"></param>
        /// <returns></returns>
        ICodeGenerator CreateCopyMethodGenerator(CopyInfo copyInfo, PropertyCodeInfo propertyCodeInfo);

        /// <summary>
        /// コピー処理生成オブジェクトの生成
        /// </summary>
        /// <param name="copyInfo"></param>
        /// <param name="propertyCodeInfo"></param>
        /// <returns></returns>
        ICodeGenerator CreateCopyLinesGenerator(CopyInfo copyInfo, PropertyCodeInfo propertyCodeInfo);

        /// <summary>
        /// コピー元文字列の取得
        /// </summary>
        /// <param name="copyInfo"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        string GetCopySourceString(CopyInfo copyInfo, string propertyName);

        /// <summary>
        /// コピー先文字列の取得
        /// </summary>
        /// <param name="copyInfo"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        string GetCopyDestString(CopyInfo copyInfo, string propertyName);
    }
}
