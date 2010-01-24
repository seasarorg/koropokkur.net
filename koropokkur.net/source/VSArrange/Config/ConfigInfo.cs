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

        private IList<ConfigInfoDetail> _filterFileStringList;

        /// <summary>
        /// ファイル名用フィルター文字列リスト
        /// </summary>
        public IList<ConfigInfoDetail> FilterFileStringList
        {
            set { _filterFileStringList = value; }
            get { return _filterFileStringList; }
        }

        private IList<ConfigInfoDetail> _filterFolderStringList;

        /// <summary>
        /// フォルダ名用フィルター文字列リスト
        /// </summary>
        public IList<ConfigInfoDetail> FilterFolderStringList
        {
            set { _filterFolderStringList = value; }
            get { return _filterFolderStringList; }
        }

        private IList<ConfigInfoDetail> _filterCompileStringList;

        /// <summary>
        /// ビルドアクション「コンパイル」用フィルター文字列リスト
        /// </summary>
        public IList<ConfigInfoDetail> FilterCompileStringList
        {
            set { _filterCompileStringList = value; }
            get { return _filterCompileStringList; }
        }

        private IList<ConfigInfoDetail> _filterResourceStringList;

        /// <summary>
        /// ビルドアクション「埋め込みリソース」名用フィルター文字列リスト
        /// </summary>
        public IList<ConfigInfoDetail> FilterResourceStringList
        {
            set { _filterResourceStringList = value; }
            get { return _filterResourceStringList; }
        }

        private IList<ConfigInfoDetail> _filterContentsStringList;

        /// <summary>
        /// ビルドアクション「コンテンツ」用フィルター文字列リスト
        /// </summary>
        public IList<ConfigInfoDetail> FilterContentsStringList
        {
            set { _filterContentsStringList = value; }
            get { return _filterContentsStringList; }
        }

        private IList<ConfigInfoDetail> _filterNoActionStringList;

        /// <summary>
        /// ビルドアクション「なし」用フィルター文字列リスト
        /// </summary>
        public IList<ConfigInfoDetail> FilterNoActionStringList
        {
            set { _filterNoActionStringList = value; }
            get { return _filterNoActionStringList; }
        }

        private IList<ConfigInfoDetail> _filterNoCopyStringList;

        /// <summary>
        /// ビルド後「コピーしない」用フィルター文字列リスト
        /// </summary>
        public IList<ConfigInfoDetail> FilterNoCopyStringList
        {
            set { _filterNoCopyStringList = value; }
            get { return _filterNoCopyStringList; }
        }

        private IList<ConfigInfoDetail> _filterEverytimeCopyStringList;

        /// <summary>
        /// ビルド後「常にコピー」用フィルター文字列リスト
        /// </summary>
        public IList<ConfigInfoDetail> FilterEverytimeCopyStringList
        {
            set { _filterEverytimeCopyStringList = value; }
            get { return _filterEverytimeCopyStringList; }
        }

        private IList<ConfigInfoDetail> _filterCopyIfNewStringList;

        /// <summary>
        /// ビルド後「新しい場合はコピー」用フィルター文字列リスト
        /// </summary>
        public IList<ConfigInfoDetail> FilterCopyIfNewStringList
        {
            set { _filterCopyIfNewStringList = value; }
            get { return _filterCopyIfNewStringList; }
        }

        private ConfigInfoDetail _outputResultWindow;
        /// <summary>
        /// ウィンドウへの結果出力設定
        /// </summary>
        public ConfigInfoDetail OutputResultWindow
        {
            set { _outputResultWindow = value; }
            get { return _outputResultWindow; }
        }

        private ConfigInfoDetail _outputResultFile;
        /// <summary>
        /// ファイルへの結果出力設定
        /// </summary>
        public ConfigInfoDetail OutputResultFile
        {
            set { _outputResultFile = value; }
            get { return _outputResultFile; }
        }

        /// <summary>
        /// 属性設定実行フラグ
        /// </summary>
        public bool IsSetOption
        {
            get
            {
                return (FilterCompileStringList.Count > 0 ||
                        FilterContentsStringList.Count > 0 ||
                        FilterCopyIfNewStringList.Count > 0 ||
                        FilterEverytimeCopyStringList.Count > 0 ||
                        FilterNoActionStringList.Count > 0 ||
                        FilterNoCopyStringList.Count > 0 ||
                        FilterResourceStringList.Count > 0);
            }
        }

        /// <summary>
        /// 処理結果出力フラグ
        /// </summary>
        public bool IsOutputResult
        {
            get
            {
                return (OutputResultWindow.IsEnable || OutputResultFile.IsEnable);
            }
        }
    }
}
