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


namespace CopyGen.Gen
{
    /// <summary>
    /// 言語依存のロジック生成ファクトリ
    /// </summary>
    public interface ICopyCodeBuildFactory
    {
        /// <summary>
        /// コピー処理コード出力クラス生成ロジックの取得
        /// </summary>
        /// <returns></returns>
        ICopyCodeGeneratorCreator CreateCopyCodeGeneratorCreator();

        /// <summary>
        /// コピー処理対象情報生成ロジックの取得
        /// </summary>
        /// <returns></returns>
        ICopyTargetBaseInfoCreator CreateCopyTargetBaseInfoCreator();

        /// <summary>
        /// コード生成の起点となるインデントの取得
        /// </summary>
        /// <param name="input">インデント位置を取得するための解析対象文字列</param>
        /// <returns></returns>
        string GetIndent(string input);
    }
}
