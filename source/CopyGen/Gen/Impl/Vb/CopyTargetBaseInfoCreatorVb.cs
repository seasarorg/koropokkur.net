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

namespace CopyGen.Gen.Impl.Vb
{
    /// <summary>
    /// コピー処理生成対象情報生成ロジック（VB.NET)
    /// </summary>
    public class CopyTargetBaseInfoCreatorVb : AbstractCopyTargetBaseInfoCreator
    {
        protected override CopyTargetBaseInfo CreateTargetBaseInfo(string editingFilePath, string sourceTypeName, string destTypeName)
        {
            return new CopyTargetBaseInfo(sourceTypeName, destTypeName);
        }
    }
}
