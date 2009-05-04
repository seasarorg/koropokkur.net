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
    #region Enum
    /// <summary>
    /// コピー元取得方法列挙体
    /// </summary>
    public enum EnumCopySource
    {
        This = 0,
        PropertyOnly,
        AsArgument
    } ;

    /// <summary>
    /// コピー先取得方法列挙体
    /// </summary>
    public enum EnumCopyTarget
    {
        This = 0,
        PropertyOnly,
        AsArgument,
        Return
    } ;
    #endregion

    /// <summary>
    /// コピー情報
    /// </summary>
    public class CopyInfo
    {
        protected const string DEFAULT_COPY_SOURCE_NAME = "source";
        protected const string DEFAULT_COPY_TARGET_NAME = "target";

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

        #region コピー元情報
        private EnumCopySource _copySource;
        /// <summary>
        /// コピー元の取得元
        /// </summary>
        public EnumCopySource CopySource
        {
            get { return _copySource; }
            set { _copySource = value; }
        }

        private string _copySourceName;
        /// <summary>
        /// コピー元引数名
        /// </summary>
        public string SourceArgumentName
        {
            get
            {
                return string.IsNullOrEmpty(_copySourceName) ? DEFAULT_COPY_SOURCE_NAME : _copySourceName;
            }
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

        private bool _isNotNullSource;
        /// <summary>
        /// Nullチェックを生成するか
        /// </summary>
        public bool IsNotNullSource
        {
            get { return _isNotNullSource; }
            set { _isNotNullSource = value; }
        }
        #endregion

        #region コピー先情報
        private EnumCopyTarget _copyTarget;
        /// <summary>
        /// コピー先の取得元
        /// </summary>
        public EnumCopyTarget CopyTarget
        {
            get { return _copyTarget; }
            set { _copyTarget = value; }
        }

        private string _copyTargetName;
        /// <summary>
        /// コピー先引数名
        /// </summary>
        public string TargetArgumentName
        {
            get
            {
                return string.IsNullOrEmpty(_copyTargetName) ? DEFAULT_COPY_TARGET_NAME : _copyTargetName;
            }
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

        private bool _isNotNullTarget;
        /// <summary>
        /// Nullチェックを生成するか
        /// </summary>
        public bool IsNotNullTarget
        {
            get { return _isNotNullTarget; }
            set { _isNotNullTarget = value; }
        }
        #endregion

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
