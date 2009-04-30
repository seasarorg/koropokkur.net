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

using System.Collections.Generic;
using System.Reflection;
using CodeGeneratorCore.Enum;

namespace CopyGen.Gen
{
    /// <summary>
    /// コピー情報
    /// </summary>
    public class CopyInfo
    {
        #region プロパティ

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

        private bool _isOutputMethod;
        /// <summary>
        /// メソッドとして出力するか？
        /// </summary>
        public bool IsOutputMethod
        {
            get { return _isOutputMethod; }
            set { _isOutputMethod = value; }
        }

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
        /// 出力するメソッド名
        /// </summary>
        public string MethodName
        {
            get { return _methodName; }
            set { _methodName = value; }
        }

        private bool _hasSourceArgument;
        /// <summary>
        /// コピー元を引数として渡すフラグ
        /// </summary>
        public bool HasSourceArgument
        {
            get { return _hasSourceArgument;}
            set { _hasSourceArgument = value; }
        }

        private string _copySourceName;
        /// <summary>
        /// コピー元引数名
        /// </summary>
        public string SourceArgumentName
        {
            get { return _copySourceName; }
            set { _copySourceName = value; }
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

        private bool _isReturn;
        /// <summary>
        /// コピー先のインスタンスをメソッドとして返すか？
        /// </summary>
        public bool IsReturn
        {
            get { return _isReturn; }
            set { _isReturn = value; }
        }

        private string _copyTargetName;
        /// <summary>
        /// コピー先引数名
        /// </summary>
        public string TargetArgumentName
        {
            get { return _copyTargetName; }
            set { _copyTargetName = value; }
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

        private bool _isEverytimeConfirm;
        /// <summary>
        /// 常にコピー設定を確認するか？
        /// </summary>
        public bool IsEverytimeConfirm
        {
            get { return _isEverytimeConfirm; }
            set { _isEverytimeConfirm = value; }
        }
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CopyInfo()
        {
            IsEverytimeConfirm = true;
            SourceArgumentName = "source";
            TargetArgumentName = "target";
        }

        /// <summary>
        /// プロパティをもっているか？
        /// </summary>
        /// <param name="propertyInfos"></param>
        /// <returns></returns>
        protected static bool HasProperty(PropertyInfo[] propertyInfos)
        {
            if(propertyInfos != null && propertyInfos.Length > 0)
            {
                return true;
            }
            return false;
        }
    }
}
