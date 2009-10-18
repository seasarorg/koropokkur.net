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

namespace VSArrange.Config
{
    /// <summary>
    /// 設定情報クラス
    /// </summary>
    public class ConfigInfo
    {
        #region for Singleton
        private static ConfigInfo _configInfo = null;

        public static ConfigInfo GetInstance()
        {
            if(_configInfo == null)
            {
                _configInfo = new ConfigInfo();
            }
            return _configInfo;
        }

        /// <summary>
        /// シングルトン用コンストラクタ
        /// </summary>
        private ConfigInfo()
        {
        }

        #endregion

        private IList<ConfigInfoFilter> _filterFileStringList;

        /// <summary>
        /// ファイル名用フィルター文字列リスト
        /// </summary>
        public IList<ConfigInfoFilter> FilterFileStringList
        {
            set { _filterFileStringList = value; }
            get { return _filterFileStringList; }
        }

        private IList<ConfigInfoFilter> _filterFolderStringList;

        /// <summary>
        /// フォルダ名用フィルター文字列リスト
        /// </summary>
        public IList<ConfigInfoFilter> FilterFolderStringList
        {
            set { _filterFolderStringList = value; }
            get { return _filterFolderStringList; }
        }

        private IList<ConfigInfoFilter> _filterCompileStringList;

        /// <summary>
        /// ビルドアクション「コンパイル」用フィルター文字列リスト
        /// </summary>
        public IList<ConfigInfoFilter> FilterCompileStringList
        {
            set { _filterCompileStringList = value; }
            get { return _filterCompileStringList; }
        }

        private IList<ConfigInfoFilter> _filterResourceStringList;

        /// <summary>
        /// ビルドアクション「埋め込みリソース」名用フィルター文字列リスト
        /// </summary>
        public IList<ConfigInfoFilter> FilterResourceStringList
        {
            set { _filterResourceStringList = value; }
            get { return _filterResourceStringList; }
        }

        private IList<ConfigInfoFilter> _filterContentsStringList;

        /// <summary>
        /// ビルドアクション「コンテンツ」用フィルター文字列リスト
        /// </summary>
        public IList<ConfigInfoFilter> FilterContentsStringList
        {
            set { _filterContentsStringList = value; }
            get { return _filterContentsStringList; }
        }

        private IList<ConfigInfoFilter> _filterNoActionStringList;

        /// <summary>
        /// ビルドアクション「なし」用フィルター文字列リスト
        /// </summary>
        public IList<ConfigInfoFilter> FilterNoActionStringList
        {
            set { _filterNoActionStringList = value; }
            get { return _filterNoActionStringList; }
        }

        private IList<ConfigInfoFilter> _filterNoCopyStringList;

        /// <summary>
        /// ビルド後「コピーしない」用フィルター文字列リスト
        /// </summary>
        public IList<ConfigInfoFilter> FilterNoCopyStringList
        {
            set { _filterNoCopyStringList = value; }
            get { return _filterNoCopyStringList; }
        }

        private IList<ConfigInfoFilter> _filterEverytimeCopyStringList;

        /// <summary>
        /// ビルド後「常にコピー」用フィルター文字列リスト
        /// </summary>
        public IList<ConfigInfoFilter> FilterEverytimeCopyStringList
        {
            set { _filterEverytimeCopyStringList = value; }
            get { return _filterEverytimeCopyStringList; }
        }

        private IList<ConfigInfoFilter> _filterCopyIfNewStringList;

        /// <summary>
        /// ビルド後「新しい場合はコピー」用フィルター文字列リスト
        /// </summary>
        public IList<ConfigInfoFilter> FilterCopyIfNewStringList
        {
            set { _filterCopyIfNewStringList = value; }
            get { return _filterCopyIfNewStringList; }
        }
    }
}
