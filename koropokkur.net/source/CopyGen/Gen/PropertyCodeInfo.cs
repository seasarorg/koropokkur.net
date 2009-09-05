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

using System;
using System.Collections.Generic;
using System.Text;

namespace CopyGen.Gen
{
    /// <summary>
    /// コピー処理コード生成に必要なプロパティ情報を保持するクラス
    /// </summary>
    public class PropertyCodeInfo
    {
        private IList<string> _sourcePropertyNames;
        public IList<string> SourcePropertyNames
        {
            get { return _sourcePropertyNames; }
            set { _sourcePropertyNames = value; }
        }

        private IList<string> _targetPropertyNames;
        public IList<string> TargetPropertyNames
        {
            get { return _targetPropertyNames; }
            set { _targetPropertyNames = value; }
        }

        private string _sourceTypeName;
        /// <summary>
        /// コピー元の型名
        /// </summary>
        public string SourceTypeName
        {
            get { return _sourceTypeName; }
            set { _sourceTypeName = value; }
        }

        private string _targetTypeName;
        /// <summary>
        /// コピー先の型名
        /// </summary>
        public string TargetTypeName
        {
            get { return _targetTypeName; }
            set { _targetTypeName = value; }
        }

        /// <summary>
        /// コード生成可能か？
        /// </summary>
        public bool CanCodeGenerate
        {
            get
            {
                return (!string.IsNullOrEmpty(SourceTypeName) 
                    && !string.IsNullOrEmpty(TargetTypeName));
            }
        }
    }
}
