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
using System.IO;
using AddInCommon.Exception;
using AddInCommon.Invoke;
using AddInCommon.Util;
using AddInCommon.Wrapper;
using EnvDTE;
using VSArrange.Config;
using VSArrange.Filter;
using VSArrange.Message;
using VSArrange.Report.Appender;
using VSArrange.Util;

namespace VSArrange.Report
{
    /// <summary>
    /// プロジェクト要素整理クラス
    /// </summary>
    public class ProjectArranger
    {
        /// <summary>
        /// 設定情報
        /// </summary>
        private readonly ConfigInfo _configInfo;

        /// <summary>
        /// 処理状況出力
        /// </summary>
        private readonly IOutputReport _reporter;

        /// <summary>
        /// プロジェクト追加要否判定フィルタ
        /// </summary>
        private readonly ProjectIncludeFilter _filter;

        private readonly ProjectEx _project;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="configInfo"></param>
        /// <param name="worker"></param>
        public ProjectArranger(ConfigInfo configInfo, IOutputReport reporter)
        {
            _reporter = reporter;
            _configInfo = configInfo;
            _filter = new ProjectIncludeFilter(configInfo);
            _project = new ProjectEx();
        }

        #region リフレッシュ処理

        /// <summary>
        /// プロジェクトのリフレッシュ
        /// </summary>
        /// <param name="project"></param>
        public virtual void ArrangeProject(Project project)
        {
            _project.SetProject(project);
            var projectName = _project.FullName;
            if (string.IsNullOrEmpty(projectName))
            {
                //  プロジェクト名が入っていない要素は無視
                return;
            }
            
            //  C#.NET,VB.NET以外はプロジェクトの構造が違うため
            //  現バージョンでは処理の対象外
            if(ArrangeUtils.IsSupportLanguage(projectName))
            {
                _reporter.ReportError(VSArrangeMessage.GetNotSupported(projectName));
                return;
            }

            var resultManager = new OutputResultManager();
            resultManager.Initialize(_project.Name, _configInfo);
            Execute(_project, resultManager);
            _reporter.ReportResult(resultManager.GetResultMessage());
        }

        /// <summary>
        /// バックグラウンド処理開始イベント
        /// </summary>
        /// <param name="project"></param>
        /// <param name="resultManager"></param>
        private void Execute(Project project, OutputResultManager resultManager)
        {
            try
            {
                //  プロジェクト要素属性設定フィルターを設定
                var buildActionArranger = new BuildActionArranger(_configInfo, resultManager);
                var copyToOutputDirectoryArranger = new CopyToOutputDirectoryArranger(_configInfo, resultManager);

                var projectDirPath = Path.GetDirectoryName(project.FullName);
                var projectItems = project.ProjectItems;

                _reporter.Report(VSArrangeMessage.GetArrangeNow());
                ArrangeDirectories(projectDirPath, projectItems, resultManager);

                if (_configInfo.IsSetOption)
                {
                    _reporter.Report(VSArrangeMessage.GetSetAttributeNow());
                    //  整理し終わったプロジェクト要素に対して属性設定
                    ProjectItemUtils.AccessAllProjectItems(
                        projectItems, new IProjectItemAccessor[]
                                          {
                                              buildActionArranger, copyToOutputDirectoryArranger
                                          });
                }
            }
            catch (System.Exception ex)
            {
                throw new KoropokkurException(VSArrangeMessage.GetOutputResultFailure(project.Name), ex);
            }
        }

        /// <summary>
        /// ディレクトリの整理
        /// </summary>
        /// <param name="dirPath"></param>
        /// <param name="projectItems"></param>
        /// <param name="resultManager"></param>
        private void ArrangeDirectories(string dirPath, ProjectItems projectItems, OutputResultManager resultManager)
        {
            //  進捗単位数
            const int PROGRESS_PARTS_COUNT = 4;

            var projectItemsEx = new ProjectItemsEx();
            projectItemsEx.SetProjectItems(projectItems);
            _reporter.Report(VSArrangeMessage.GetStartExecute(dirPath));

            var fileItems = new Dictionary<string, ProjectItem>();
            var folderItems = new Dictionary<string, ProjectItem>();
            var deleteTarget = new List<ProjectItem>();

            var basePath = dirPath + Path.DirectorySeparatorChar;
            
            _reporter.ReportProgress("追加・除外する要素を抽出中", 1, PROGRESS_PARTS_COUNT);
            var totalCount = projectItemsEx.Count;

            int current = 1;
            //  ファイル、フォルダ、削除対象に振り分ける
            foreach (ProjectItem projectItem in projectItemsEx)
            {
                var projectItemEx = new ProjectItemEx();
                projectItemEx.SetProjectItem(projectItem);

                _reporter.ReportSubProgress("プロジェクト要素を振分け中", current, totalCount);
                string currentPath = basePath + projectItemEx.Name;
                if (_filter.IsRemove(currentPath))
                {
                    deleteTarget.Add(projectItemEx);
                }
                else
                {
                    if (Path.HasExtension(currentPath))
                    {
                        fileItems[currentPath] = projectItemEx;
                    }
                    else
                    {
                        folderItems[currentPath] = projectItemEx;
                    }
                }
                current++;
            }

            _reporter.ReportProgress("プロジェクト要素追加中(フォルダ)", 2, PROGRESS_PARTS_COUNT);
            //  ディレクトリ追加
            DirectoryAppender directoryAppender = new DirectoryAppender(
                dirPath, _filter.FilterFolder, projectItemsEx, folderItems, resultManager);
            directoryAppender.Execute();

            _reporter.ReportProgress("プロジェクト要素追加中(ファイル)", 3, PROGRESS_PARTS_COUNT);
            //  ファイル追加
            FileAppender fileAppender = new FileAppender(
                dirPath, _filter.FilterFile, projectItemsEx, fileItems, resultManager);
            fileAppender.Execute();

            _reporter.ReportProgress("不要なプロジェクト要素を削除中", 4, PROGRESS_PARTS_COUNT);
            //  不要な要素は削除
            ProjectItemRemover projectItemRemover = new ProjectItemRemover(
                deleteTarget, resultManager);
            projectItemRemover.Execute();

            //  残ったフォルダに対して同様の処理を再帰的に実行
            foreach (var projectDirPath in folderItems.Keys)
            {
                ArrangeDirectories(projectDirPath, folderItems[projectDirPath].ProjectItems, resultManager);
            }
        }

        #endregion


    }
}
