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
using System.Text;
using AddInCommon.Invoke;
using AddInCommon.Util;
using EnvDTE;
using VSArrange.Report.Appender;
using VSArrange.Config;
using VSArrange.Filter;
using VSArrange.Message;
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
        }

        #region リフレッシュ処理

        /// <summary>
        /// プロジェクトのリフレッシュ
        /// </summary>
        /// <param name="project"></param>
        public virtual void ArrangeProject(Project project)
        {
            var projectName = project.FullName;
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
            resultManager.Initialize(project.Name, _configInfo);
            Execute(project, resultManager);
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

                string projectDirPath = Path.GetDirectoryName(project.FullName);
                ProjectItems projectItems = project.ProjectItems;

                _reporter.ReportProgress("プロジェクト要素の整理中", 0, 2);
                ArrangeDirectories(projectDirPath, projectItems, resultManager);

                if (_configInfo.IsSetOption)
                {
                    _reporter.ReportProgress("プロジェクト要素の属性を設定中", 1, 2);
                    //  整理し終わったプロジェクト要素に対して属性設定
                    ProjectItemUtils.AccessAllProjectItems(
                        projectItems, new IProjectItemAccessor[]
                                          {
                                              buildActionArranger, copyToOutputDirectoryArranger
                                          });
                }
                _reporter.ReportProgress("プロジェクト要素の属性を設定中", 2, 2);
            }
            catch (System.Exception ex)
            {
                var builder = new StringBuilder();
                builder.AppendLine(string.Format(
                    "[{0}]処理結果の出力に失敗しました。", project.Name));
                builder.AppendLine(ex.Message);
                builder.AppendLine(ex.StackTrace);
                _reporter.ReportError(builder.ToString());
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

            var fileItems = new Dictionary<string, ProjectItem>();
            var folderItems = new Dictionary<string, ProjectItem>();
            var deleteTarget = new List<ProjectItem>();

            var basePath = dirPath + Path.DirectorySeparatorChar;
            
            _reporter.ReportProgress("追加・除外する要素抽出中", 0, PROGRESS_PARTS_COUNT);
            var totalCount = projectItems.Count;
            var current = 0;
            //  ファイル、フォルダ、削除対象に振り分ける
            foreach (ProjectItem projectItem in projectItems)
            {
                _reporter.ReportProgress("プロジェクト要素を振分け中", current, totalCount);
                string currentPath = basePath + projectItem.Name;
                if (_filter.IsRemove(currentPath))
                {
                    deleteTarget.Add(projectItem);
                }
                else
                {
                    if (Path.HasExtension(currentPath))
                    {
                        fileItems[currentPath] = projectItem;
                    }
                    else
                    {
                        folderItems[currentPath] = projectItem;
                    }
                }
                current++;
            }

            _reporter.ReportProgress("プロジェクト要素を追加中（フォルダ）", 1, PROGRESS_PARTS_COUNT);
            //  ディレクトリ追加
            DirectoryAppender directoryAppender = new DirectoryAppender(
                dirPath, _filter.FilterFolder, projectItems, folderItems, resultManager);
            directoryAppender.Execute();

            _reporter.ReportProgress("プロジェクト要素を追加中（ファイル）", 2, PROGRESS_PARTS_COUNT);
            //  ファイル追加
            FileAppender fileAppender = new FileAppender(
                dirPath, _filter.FilterFile, projectItems, fileItems, resultManager);
            fileAppender.Execute();

            _reporter.ReportProgress("不要なプロジェクト要素を削除中", 3, PROGRESS_PARTS_COUNT);
            //  不要な要素は削除
            ProjectItemRemover projectItemRemover = new ProjectItemRemover(
                deleteTarget, resultManager);
            projectItemRemover.Execute();

            _reporter.ReportProgress("フォルダ内プロジェクト要素の整理中", 0, PROGRESS_PARTS_COUNT);
            //  残ったフォルダに対して同様の処理を再帰的に実行
            foreach (string projectDirPath in folderItems.Keys)
            {
                ArrangeDirectories(projectDirPath, folderItems[projectDirPath].ProjectItems, resultManager);
            }
        }

        #endregion


    }
}
