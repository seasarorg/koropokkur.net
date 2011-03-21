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
using System.Globalization;
using System.Reflection;
using System.Resources;
using EnvDTE80;

namespace AddInCommon.Util
{
    /// <summary>
    /// リソースに関する共通処理ユーティリティクラス
    /// </summary>
    public class ResourceUtils
    {
        /// <summary>
        /// ResourceManagerを利用して多言語対応しているキーワードを取得
        /// </summary>
        /// <param name="applicationObject"></param>
        /// <param name="baseName"></param>
        /// <returns></returns>
        public static string GetResourceWord(DTE2 applicationObject, string baseName)
        {
            string retWord = null;
            try
            {
                //コマンドを別のメニューに移動するには、単語 "Tools" を英語版のメニューに変換してください。 
                //  このコードはカルチャを認識して、メニュー名に追加した後、コマンドをそのメニューに追加します。
                //  ファイル内にトップレベル メニューの一覧があります。
                //  CommandBar.resx.
                string resourceName;
                ResourceManager resourceManager = new ResourceManager("AddInCommon.CommandBar", Assembly.GetExecutingAssembly());
                CultureInfo cultureInfo = new CultureInfo(applicationObject.LocaleID);

                if (cultureInfo.TwoLetterISOLanguageName == "zh")
                {
                    System.Globalization.CultureInfo parentCultureInfo = cultureInfo.Parent;
                    resourceName = String.Concat(parentCultureInfo.Name, baseName);
                }
                else
                {
                    resourceName = String.Concat(cultureInfo.TwoLetterISOLanguageName, baseName);
                }
                retWord = resourceManager.GetString(resourceName);
            }
            catch(SystemException e)
            {
                string msg = e.Message;
                //単語 'Tools' の他言語バージョンを検索しましたが、見つかりませんでした。
                //  既定は en-US の単語ですが、現在のカルチャでも機能する可能性があります。
                retWord = baseName;
            }
            return retWord;
        }
    }
}
