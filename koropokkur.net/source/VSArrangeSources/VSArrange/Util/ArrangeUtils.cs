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

using System.IO;
using System.Text;
using AddInCommon.Report;
using EnvDTE80;
using VSArrange.Arrange;
using VSArrange.Config;
using VSArrange.Const;
using VSArrange.Report.Impl;

namespace VSArrange.Util
{
    /// <summary>
    /// プロジェクト要素整理ユーティリティ
    /// </summary>
    public sealed class ArrangeUtils
    {
        /// <summary>
        /// サポートしている言語か判定
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool IsSupportLanguage(string name)
        {
            if (name.EndsWith(VSArrangeConst.SUPPORTED_EXT_CSPROJ) ||
                name.EndsWith(VSArrangeConst.SUPPORTED_EXT_VBPROJ))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// プロジェクト整理オブジェクトの取得
        /// </summary>
        /// <param name="configInfo"></param>
        /// <param name="reporter"></param>
        /// <param name="isBackground"></param>
        public static ProjectArranger CreateArranger(ConfigInfo configInfo, IOutputReport reporter,
            bool isBackground = false)
        {
            if (isBackground)
            {
                return new BackgroundProjectArranger(configInfo, reporter);
            }
            return new ProjectArranger(configInfo, reporter);
        }

        /// <summary>
        /// 処理状況出力オブジェクトの生成
        /// </summary>
        /// <param name="configInfo"></param>
        /// <param name="applicationObject"></param>
        /// <returns></returns>
        public static IOutputReport CreateAddInReporter(ConfigInfo configInfo, DTE2 applicationObject)
        {
            return new AddInOutputReport(configInfo, applicationObject);
        }

        /// <summary>
        /// 出力先パスの取得
        /// </summary>
        /// <param name="configInfo"></param>
        /// <param name="projectName"></param>
        /// <returns></returns>
        public static string GetOutputPath(ConfigInfo configInfo, string projectName)
        {
            var outputPathBuilder = new StringBuilder();
            outputPathBuilder.Append(configInfo.OutputResultFile.Value);
            outputPathBuilder.Append(Path.DirectorySeparatorChar);
            outputPathBuilder.Append(projectName);
            outputPathBuilder.Append(".log");

            return outputPathBuilder.ToString();
        }
    }
}
