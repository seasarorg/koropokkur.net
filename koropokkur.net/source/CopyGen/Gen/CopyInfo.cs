﻿#region Copyright
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

        #region 読み取り専用
        
        //protected Type _sourceType;
        ///// <summary>
        ///// コピー元の型（使用時にはnullではないことが前提）
        ///// </summary>
        //public Type SourceType { get { return _sourceType; } }
        
        ////protected PropertyInfo[] _sourcePropertyInfos;
        /////// <summary>
        /////// コピー元の公開プロパティ（使用時にはnullではないことが前提）
        /////// </summary>
        ////public PropertyInfo[] SourcePropertyInfos { get { return _sourcePropertyInfos; } }

        //protected Type _targetType;
        ///// <summary>
        ///// コピー先の型（使用時にはnullではないことが前提）
        ///// </summary>
        //public Type TargetType { get { return _targetType; } }

        //protected PropertyInfo[] _targetPropertyInfos;
        ///// <summary>
        ///// コピー先の公開プロパティ（使用時にはnullではないことが前提）
        ///// </summary>
        //public PropertyInfo[] TargetPropertyInfos { get { return _targetPropertyInfos; } }
        
        //protected readonly IDictionary<string, PropertyInfo> _targetPropertyMap
        //    = new Dictionary<string, PropertyInfo>();
        ///// <summary>
        ///// コピー先のプロパティマッピング
        ///// </summary>
        //public IDictionary<string, PropertyInfo> TargetPropertyMap { get { return _targetPropertyMap; } }

        #endregion

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

        private bool _isSourceTypeAuto;
        /// <summary>
        /// コピー元の型を自動設定するか？
        /// </summary>
        public bool IsSourceTypeAuto
        {
            get { return _isSourceTypeAuto; }
            set { _isSourceTypeAuto = value; }
        }

        private string _sourceTypeName;
        /// <summary>
        /// コピー元の型名
        /// </summary>
        public string SourceTypeName
        {
            get { return _sourceTypeName; }
            set
            {
                //string inputSourceTypeName = value;
                //_sourceType = AssemblyUtils.ForName(inputSourceTypeName);
                //if(_sourceType == null)
                //{
                //    throw new ClassNotFoundRuntimeException(inputSourceTypeName);
                //}

                //_sourcePropertyInfos = _sourceType.GetProperties(BindingFalgsUtils.GetPublicFlags());
                //if(!HasProperty(_sourcePropertyInfos))
                //{
                //    throw new PropertyNotFoundRutimeException(inputSourceTypeName);
                //}
                _sourceTypeName = value;
            }
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

        private bool _isTargetTypeAuto;
        /// <summary>
        /// コピー先の型を自動設定するか？
        /// </summary>
        public bool IsTargetTypeAuto
        {
            get { return _isTargetTypeAuto; }
            set { _isTargetTypeAuto = value; }
        }

        private string _targetTypeName;
        /// <summary>
        /// コピー先の型名
        /// </summary>
        public string TargetTypeName
        {
            get { return _targetTypeName; }
            set
            {
                //string inputTargetName = value;
                //_targetType = AssemblyUtils.ForName(inputTargetName);
                //if (_targetType == null)
                //{
                //    throw new ClassNotFoundRuntimeException(inputTargetName);
                //}

                //_targetPropertyInfos = _targetType.GetProperties(BindingFalgsUtils.GetPublicFlags());
                //if (!HasProperty(_targetPropertyInfos))
                //{
                //    throw new PropertyNotFoundRutimeException(inputTargetName);
                //}

                //foreach (PropertyInfo info in _targetPropertyInfos)
                //{
                //    _targetPropertyMap.Add(info.Name, info);
                //}
                _targetTypeName = value;
            }
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
            IsSourceTypeAuto = true;
            IsTargetTypeAuto = true;
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