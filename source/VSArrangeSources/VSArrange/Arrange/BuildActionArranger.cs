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

using System.Collections.Generic;
using AddInCommon.Const;
using AddInCommon.Invoke;
using AddInCommon.Util;
using VSArrange.Config;
using VSArrange.Filter;
using VSLangProj;

namespace VSArrange.Report
{
    /// <summary>
    /// 「ビルドアクション」設定クラス
    /// </summary>
    public class BuildActionArranger : IProjectItemAccessor
    {
        /// <summary>
        /// 結果出力情報
        /// </summary>
        private readonly OutputResultManager _outputResultManager;

        /// <summary>
        /// 「ビルドアクション」設定取得フィルター
        /// </summary>
        private readonly BuildActionFilter _filter;

        /// <summary>
        /// 列挙体変換Map
        /// </summary>
        private IDictionary<EnumBuildAction, prjBuildAction> _adapter;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="configInfo"></param>
        /// <param name="outputResultManager"></param>
        public BuildActionArranger(ConfigInfo configInfo, OutputResultManager outputResultManager)
        {
            _filter = new BuildActionFilter(configInfo);
            _adapter = CreateAdapter();
            _outputResultManager = outputResultManager;
        }

        #region IProjectItemAccessor メンバ

        public void AccessFile(EnvDTE.ProjectItem projectItem)
        {
            string fileName = ProjectItemUtils.GetFileName(projectItem);
            prjBuildAction currentValue = ProjectItemUtils.GetBuildAction(projectItem);
            prjBuildAction newValue = GetBuildAction(fileName, currentValue);
            if(currentValue != newValue)
            {
                ProjectItemUtils.SetBuildAction(projectItem, newValue);

                _outputResultManager.RegisterdBuildAction(ProjectItemUtils.GetFullPath(projectItem), newValue);
            }
        }

        public void AccessFolder(EnvDTE.ProjectItem projectItem)
        {
            //  フォルダに対しては何もしない
            return;
        }

        #endregion

        /// <summary>
        /// ビルドアクション値の取得
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        private prjBuildAction GetBuildAction(string fileName, prjBuildAction defaultValue)
        {
            var buildAction = _filter.GetBuildAction(fileName);
            if (_adapter.ContainsKey(buildAction))
            {
                return _adapter[buildAction];
            }
            return defaultValue;
        }

        /// <summary>
        /// 変換Mapを生成する
        /// </summary>
        /// <returns></returns>
        private IDictionary<EnumBuildAction, prjBuildAction> CreateAdapter()
        {
            var adapter = new Dictionary<EnumBuildAction, prjBuildAction>();
            adapter[EnumBuildAction.Compile] = prjBuildAction.prjBuildActionCompile;
            adapter[EnumBuildAction.Contents] = prjBuildAction.prjBuildActionContent;
            adapter[EnumBuildAction.NoAction] = prjBuildAction.prjBuildActionNone;
            adapter[EnumBuildAction.Resource] = prjBuildAction.prjBuildActionEmbeddedResource;
            return adapter;
        }
    }
}
