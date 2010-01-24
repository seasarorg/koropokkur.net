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

using AddInCommon.Background;
using AddInCommon.Util;
using EnvDTE80;
using VSArrange.Arrange;
using VSArrange.Config;

namespace VSArrange.Util
{
    /// <summary>
    /// プロジェクト要素整理ユーティリティ
    /// </summary>
    public sealed class ArrangeUtils
    {
        /// <summary>
        /// 設定情報再読み込み
        /// </summary>
        /// <remarks>
        //  設定が変更された時点で予め非同期で読んでおく方がより良いが
        //  パフォーマンス的に整理処理直前に読んでも問題がないと思われるため
        //  実装を単純にする＋漏れをなくすためここで呼び出し
        /// </remarks>
        public static ProjectArranger CreateArranger(DTE2 applicationObject)
        {
            //  設定読み込み
            //  設定が変更された時点で予め非同期で読んでおく方がより良いが
            //  パフォーマンス的に整理処理直前に読んでも問題がないと思われるため
            //  実装を単純にする＋漏れをなくすためここで呼び出し
            ConfigInfo configInfo = ConfigFileManager.ReadConfig(PathUtils.GetConfigPath());
            return new ProjectArranger(configInfo, new AddInBackgroundWorker(applicationObject));
        }
    }
}
