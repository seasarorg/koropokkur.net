#region Copyright
/*
 * Copyright 2005-2011 the Seasar Foundation and the Others.
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
using System.Text;
using AddInCommon.Const;
using AddInCommon.Util;
using VSArrange.Config;
using VSLangProj;

namespace VSArrange.Report
{
    /// <summary>
    /// 処理結果出力管理クラス
    /// </summary>
    public class OutputResultManager
    {
        /// <summary>
        /// 結果メッセージビルダー
        /// </summary>
        private StringBuilder _resultMessageBuilder;

        /// <summary>
        /// 設定情報
        /// </summary>
        private ConfigInfo _configInfo;

        /// <summary>
        /// ヘッダメッセージ
        /// </summary>
        private string _firstMessage;

        /// <summary>
        /// 処理対象要素の存在有無フラグ
        /// </summary>
        private bool _hasArrangedItem;

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="configInfo"></param>
        public void Initialize(string projectName, ConfigInfo configInfo)
        {
            if (projectName == null) throw new ArgumentNullException("projectName");
            if (configInfo == null) throw new ArgumentNullException("configInfo");

            _hasArrangedItem = false;
            _configInfo = configInfo;
            _firstMessage = string.Format("[{0}]プロジェクト要素整理", projectName);

            _resultMessageBuilder = new StringBuilder();
            DateTime currentTime = DateTime.Now;
            _resultMessageBuilder.AppendLine(
                    string.Format("{0}<開始>({1}.{2})", _firstMessage, currentTime, currentTime.Millisecond));
        }

        /// <summary>
        /// 結果メッセージを取得する
        /// </summary>
        /// <returns></returns>
        public string GetResultMessage()
        {
            if (_configInfo.IsOutputResult)
            {
                if (!_hasArrangedItem)
                {
                    _resultMessageBuilder.AppendLine("処理対象となるファイル、フォルダはありません。");
                }

                DateTime currentTime = DateTime.Now;
                _resultMessageBuilder.AppendLine(
                    string.Format("{0}<終了>({1}.{2})", _firstMessage, currentTime, currentTime.Millisecond));
            }
            return _resultMessageBuilder.ToString();
        }

        /// <summary>
        /// ビルドアクションが設定されたことを保持する
        /// </summary>
        /// <param name="path"></param>
        /// <param name="newValue"></param>
        public void RegisterdBuildAction(string path, prjBuildAction newValue)
        {
            RegisterAddedProjectItem(string.Format("ビルドアクション\t{0}\t",
                ProjectItemUtils.BuildActionToString(newValue)), path);
        }

        /// <summary>
        /// 「出力ディレクトリにコピー」が設定されたことを保持する
        /// </summary>
        /// <param name="path"></param>
        /// <param name="newValue"></param>
        public void RegisterdCopyToOutputDirectory(string path, EnumCopyToOutputDirectory newValue)
        {
            RegisterAddedProjectItem(string.Format("出力ﾃﾞｨﾚｸﾄﾘにｺﾋﾟｰ\t{0}\t", 
                ProjectItemUtils.CopyToOutputDirectoryToString(newValue)), path);
        }

        /// <summary>
        /// プロジェクトにフォルダが追加されたことを保持する
        /// </summary>
        /// <param name="path"></param>
        public void RegisterAddedDirectory(string path)
        {
            RegisterAddedProjectItem("フォルダ\t登録\t", path);
        }

        /// <summary>
        /// プロジェクトにファイルが追加されたことを保持する
        /// </summary>
        /// <param name="path"></param>
        public void RegisterAddedFile(string path)
        {
            RegisterAddedProjectItem("ファイル\t登録\t", path);
        }

        /// <summary>
        /// プロジェクトにフォルダが除外されたことを保持する
        /// </summary>
        /// <param name="path"></param>
        public void RegisterRemovedDirectory(string path)
        {
            RegisterAddedProjectItem("フォルダ\t除外\t", path);
        }

        /// <summary>
        /// プロジェクトにファイルが除外されたことを保持する
        /// </summary>
        /// <param name="path"></param>
        public void RegisterRemovedFile(string path)
        {
            RegisterAddedProjectItem("ファイル\t除外\t", path);
        }

        /// <summary>
        /// プロジェクトに種別不明の要素が除外されたことを保持する
        /// </summary>
        /// <param name="path"></param>
        public void RegisterRemovedUnknown(string path)
        {
            RegisterAddedProjectItem("種別不明なプロジェクト要素\t除外\t", path);
        }

        /// <summary>
        /// 追加された要素の情報を保持する
        /// </summary>
        /// <param name="addedMessage"></param>
        /// <param name="path"></param>
        private void RegisterAddedProjectItem(string addedMessage, string path)
        {
            _hasArrangedItem = true;
            if (_configInfo.IsOutputResult)
            {
                _resultMessageBuilder.Append(addedMessage).AppendLine(path);
            }
        }
    }
}
