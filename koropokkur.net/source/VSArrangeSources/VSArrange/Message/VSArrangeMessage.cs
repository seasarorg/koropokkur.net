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
using AddInCommon.Message;
using VSArrange.Const;

namespace VSArrange.Message
{
    /// <summary>
    /// VSArrange固有のメッセージ取得クラス
    /// </summary>
    public class VSArrangeMessage
    {
        /// <summary>
        /// VSArrangeではサポートしていない旨を知らせるメッセージの取得
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetNotSupported(string fileName)
        {
            return KMessage.GetNotSupportExtension(VSArrangeConst.ADDIN_NAME, fileName, VSArrangeConst.SUPPORTED_EXT_CSPROJ);
        }

        /// <summary>
        /// フォルダ追加中メッセージの取得
        /// </summary>
        /// <returns></returns>
        public static string GetAddFolderNow()
        {
            return "プロジェクト要素追加中(フォルダ)";
        }

        /// <summary>
        /// ファイル追加中メッセージの取得
        /// </summary>
        /// <returns></returns>
        public static string GetAddFileNow()
        {
            return "プロジェクト要素追加中(ファイル)";
        }

        /// <summary>
        /// 削除中メッセージの取得
        /// </summary>
        /// <returns></returns>
        public static string GetRemoveNow()
        {
            return "不要なプロジェクト要素を削除中";
        }

        /// <summary>
        /// 処理結果出力失敗メッセージの取得
        /// </summary>
        /// <param name="projectName"></param>
        /// <returns></returns>
        public static string GetOutputResultFailure(string projectName)
        {
            return string.Format("[{0}]処理結果の出力に失敗しました。", projectName);
        }

        /// <summary>
        /// 整理中通知メッセージの取得
        /// </summary>
        /// <returns></returns>
        public static string GetArrangeNow()
        {
            return "プロジェクト要素の整理中";
        }

        /// <summary>
        /// 属性設定中通知メッセージの取得
        /// </summary>
        /// <returns></returns>
        public static string GetSetAttributeNow()
        {
            return "プロジェクト要素の属性を設定中";
        }

        /// <summary>
        /// 処理開始メッセージの取得
        /// </summary>
        /// <param name="dirPath">処理対象ディレクトリパス</param>
        /// <returns></returns>
        public static string GetStartExecute(string dirPath)
        {
            return string.Format("{0}の整理を開始", dirPath);
        }

    }
}
