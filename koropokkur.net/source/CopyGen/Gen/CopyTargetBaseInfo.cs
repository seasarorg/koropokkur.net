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
    /// コピー処理を生成する対象情報保持クラス
    /// </summary>
    public class CopyTargetBaseInfo
    {
        private readonly string _sourceTypeFullNames;
        /// <summary>
        /// 名前空間を含むコピー元クラス名の候補
        /// </summary>
        public string SourceTypeFullNames
        {
            get { return _sourceTypeFullNames; }
        }

        private readonly string _destTypeFullNames;
        /// <summary>
        /// 名前空間を含むコピー先クラス名の候補
        /// </summary>
        public string DestTypeFullNames
        {
            get { return _destTypeFullNames; }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="sourceTypeFullNames"></param>
        /// <param name="destTypeFullNames"></param>
        public CopyTargetBaseInfo(string sourceTypeFullNames, string destTypeFullNames)
        {
            _sourceTypeFullNames = sourceTypeFullNames;
            _destTypeFullNames = destTypeFullNames;
        }  
    }
}
