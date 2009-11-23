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
using System.Text.RegularExpressions;
using VSArrange.Config;

namespace VSArrange.Filter
{
    /// <summary>
    /// プロジェクト要素登録フィルタリングクラス
    /// </summary>
    public class ItemAttachmentFilter
    {
        /// <summary>
        /// フィルターリスト
        /// </summary>
        private readonly IList<Regex> _filterList;

        /// <summary>
        /// フィルター設定が存在するか？
        /// </summary>
        public bool HasFilter
        {
            get
            {
                if(_filterList != null && _filterList.Count > 0)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ItemAttachmentFilter()
        {
            _filterList = new List<Regex>();
        }

        /// <summary>
        /// フィルター条件となる文字列を追加
        /// </summary>
        /// <param name="fileterString"></param>
        public void AddFilter(string fileterString)
        {
            if(fileterString == null)
            {
                throw new ArgumentNullException("filterString");
            }

            _filterList.Add(new Regex(fileterString, RegexOptions.IgnoreCase));
        }

        /// <summary>
        /// フィルター条件となる文字列を追加
        /// </summary>
        /// <param name="configInfoFilters"></param>
        public void AddFilters(IList<ConfigInfoDetail> configInfoFilters)
        {
            if (configInfoFilters == null)
            {
                throw new ArgumentNullException("configInfoFilters");
            }

            foreach (ConfigInfoDetail filter in configInfoFilters)
            {
                if(filter.IsEnable &&
                    !string.IsNullOrEmpty(filter.Value))
                {
                    AddFilter(filter.Value);
                }
            }
        }

        /// <summary>
        /// フィルターを消去する
        /// </summary>
        public void Clear()
        {
            _filterList.Clear();
        }

        /// <summary>
        /// 引数の文字列がフィルターを全て通過するか判定
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool IsPassFilter(string target)
        {
            if(!HasFilter)
            {
                return true;
            }

            foreach (Regex regex in _filterList)
            {
                if(regex.IsMatch(target))
                {
                    //  一つでも引っかかったらアウト
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 引数の文字列がフィルターのいずれかに該当するか判定
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool IsHitFilter(string target)
        {
            return !IsPassFilter(target);
        }
    }
}