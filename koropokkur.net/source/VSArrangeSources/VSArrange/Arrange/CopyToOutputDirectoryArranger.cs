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

using AddInCommon.Const;
using AddInCommon.Invoke;
using AddInCommon.Util;
using VSArrange.Config;
using VSArrange.Filter;

namespace VSArrange.Arrange
{
    /// <summary>
    /// 「出力先にコピー」設定クラス
    /// </summary>
    public class CopyToOutputDirectoryArranger : IProjectItemAccessor
    {
        private readonly OutputResultManager _outputResultManager;
        private readonly CopyToOutputDirectoryFilter _filter;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="configInfo"></param>
        /// <param name="outputResultManager"></param>
        public CopyToOutputDirectoryArranger(ConfigInfo configInfo, OutputResultManager outputResultManager)
        {
            _filter = new CopyToOutputDirectoryFilter(configInfo);
            _outputResultManager = outputResultManager;
        }

        #region IProjectItemAccessor メンバ

        public void AccessFile(EnvDTE.ProjectItem projectItem)
        {
            string fileName = ProjectItemUtils.GetFileName(projectItem);
            EnumCopyToOutputDirectory currentValue = ProjectItemUtils.GetCopyToOutputDirectory(projectItem);
            EnumCopyToOutputDirectory newValue = GetCopyToOutputDeirectory(fileName, currentValue);
            if (currentValue != newValue)
            {
                ProjectItemUtils.SetCopyToOutputDirectory(projectItem, newValue);

                _outputResultManager.RegisterdCopyToOutputDirectory(
                    ProjectItemUtils.GetFullPath(projectItem), newValue);
            }
        }

        public void AccessFolder(EnvDTE.ProjectItem projectItem)
        {
            //  フォルダに対しては何もしない
            return;
        }

        #endregion

        /// <summary>
        /// 「出力ディレクトリへコピー」設定値の取得
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        protected virtual EnumCopyToOutputDirectory GetCopyToOutputDeirectory(
            string fileName, EnumCopyToOutputDirectory defaultValue)
        {
            var copyToOutputDirectory = _filter.GetCopyToOutputDeirectory(fileName);
            if (copyToOutputDirectory == EnumCopyToOutputDirectory.Nothing)
            {
                return defaultValue;
            }
            return copyToOutputDirectory;
        }
    }
}
