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


namespace CopyGen.Gen
{
    /// <summary>
    /// コピー処理生成対象情報生成インターフェース
    /// </summary>
    public interface ICopyTargetBaseInfoCreator
    {
        /// <summary>
        /// コピー処理生成対象情報の生成
        /// </summary>
        /// <param name="editingFilePath">編集中のファイルパス</param>
        /// <param name="targetLine">編集中の行</param>
        /// <returns></returns>
        CopyTargetBaseInfo Create(string editingFilePath, string targetLine);
    }
}
