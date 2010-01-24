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
    public enum EnumCopyDest
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
        protected const string DEFAULT_COPY_DEST_NAME = "dest";

        #region プロパティ

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
        private EnumCopyDest _copyDest;
        /// <summary>
        /// コピー先の取得元
        /// </summary>
        public EnumCopyDest CopyDest
        {
            get { return _copyDest; }
            set { _copyDest = value; }
        }

        private string _copyDestName;
        /// <summary>
        /// コピー先引数名
        /// </summary>
        public string DestArgumentName
        {
            get
            {
                return string.IsNullOrEmpty(_copyDestName) ? DEFAULT_COPY_DEST_NAME : _copyDestName;
            }
            set { _copyDestName = value; }
        }

        private bool _isNotNullDest;
        /// <summary>
        /// Nullチェックを生成するか
        /// </summary>
        public bool IsNotNullDest
        {
            get { return _isNotNullDest; }
            set { _isNotNullDest = value; }
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
            DestArgumentName = "dest";
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
