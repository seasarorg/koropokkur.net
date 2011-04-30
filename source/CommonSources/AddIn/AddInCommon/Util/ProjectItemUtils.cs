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
using System.IO;
using AddInCommon.Const;
using AddInCommon.Invoke;
using AddInCommon.Report;
using AddInCommon.Wrapper;
using EnvDTE;
using VSLangProj;

namespace AddInCommon.Util
{
    /// <summary>
    /// EnvDTE.ProjectItem.Propertiesユーティリティ
    /// </summary>
    /// <see cref="ProjectItem"/>
    public sealed class ProjectItemUtils
    {
        #region プロジェクト要素操作

        /// <summary>
        /// プロジェクト全体の要素に対して操作を行う
        /// </summary>
        /// <param name="projectItems"></param>
        /// <param name="accessors"></param>
        /// <param name="reporter"></param>
        public static void AccessAllProjectItems(ProjectItems projectItems, IProjectItemAccessor[] accessors,
            IOutputReport reporter)
        {
            if (projectItems == null) throw new ArgumentNullException("projectItems");
            if (accessors == null) throw new ArgumentNullException("accessors");

            var totalCount = projectItems.Count;
            var currentCount = 1;

            foreach (ProjectItem projectItemOrg in projectItems)
            {
                var projectItem = new ProjectItemEx();
                projectItem.SetProjectItem(projectItemOrg);

                var path = GetFullPath(projectItem);
                if(Directory.Exists(path))
                {
                    foreach (IProjectItemAccessor accessor in accessors)
                    {
                        reporter.ReportSubProgress(accessor.Name, currentCount, totalCount);
                        accessor.AccessFolder(projectItem);   
                    }
                }
                else if(File.Exists(path))
                {
                    foreach (IProjectItemAccessor accessor in accessors)
                    {
                        reporter.ReportSubProgress(accessor.Name, currentCount, totalCount);
                        accessor.AccessFile(projectItem);   
                    }
                }

                var childItemsOrg = projectItem.ProjectItems;
                var childItems = new ProjectItemsEx();
                childItems.SetProjectItems(childItemsOrg);
                if (childItems != null && childItems.Count > 0)
                {
                    AccessAllProjectItems(childItems, accessors, reporter);
                }

                currentCount++;
            }
        }

        /// <summary>
        /// プロジェクト全体の要素に対して操作を行う
        /// </summary>
        /// <param name="projectItems"></param>
        /// <param name="accessor"></param>
        /// <param name="reporter"></param>
        public static void AccessAllProjectItems(ProjectItems projectItems, IProjectItemAccessor accessor,
            IOutputReport reporter)
        {
            AccessAllProjectItems(projectItems, new IProjectItemAccessor[] { accessor }, reporter);
        }

        #endregion

        #region ファイル、フォルダ名

        /// <summary>
        /// ファイル、フォルダ名を取得する
        /// </summary>
        /// <param name="projectItem"></param>
        /// <returns></returns>
        public static string GetFileName(ProjectItem projectItem)
        {
            return (string) GetValue(projectItem, ProjectItemFileConst.FILE_NAME);
        }

        /// <summary>
        /// ファイル、フォルダ名を設定する
        /// </summary>
        /// <param name="projectItem"></param>
        /// <param name="value"></param>
        public static void SetFileName(ProjectItem projectItem, string value)
        {
            SetValue(projectItem, ProjectItemFileConst.FILE_NAME, value);
        }

        #endregion

        #region パス

        /// <summary>
        /// プロジェクト要素が参照しているファイル、フォルダの絶対パスを取得する
        /// </summary>
        /// <param name="projectItem"></param>
        /// <returns></returns>
        public static string GetFullPath(ProjectItem projectItem)
        {
            return (string) GetValue(projectItem, ProjectItemFileConst.FULL_PATH);
        }

        /// <summary>
        /// プロジェクト要素が参照しているファイル、フォルダの絶対パスを設定する
        /// </summary>
        /// <param name="projectItem"></param>
        /// <param name="value"></param>
        public static void SetFullPath(ProjectItem projectItem, string value)
        {
            SetValue(projectItem, ProjectItemFileConst.FULL_PATH, value);
        }

        #endregion

        #region ビルドアクション
        /// <summary>
        /// 「ビルドアクション」の取得
        /// </summary>
        /// <param name="projectItem"></param>
        /// <returns></returns>
        public static prjBuildAction GetBuildAction(ProjectItem projectItem)
        {
            return (prjBuildAction) GetValue(projectItem, ProjectItemFileConst.BUILD_ACTION);
        }

        /// <summary>
        /// 「ビルドアクション」の設定
        /// </summary>
        /// <param name="projectItem"></param>
        /// <param name="value"></param>
        public static void SetBuildAction(ProjectItem projectItem, prjBuildAction value)
        {
            SetValue(projectItem, ProjectItemFileConst.BUILD_ACTION, value);
        }

        /// <summary>
        /// 「ビルドアクション」列挙体を文字列に変換
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string BuildActionToString(prjBuildAction value)
        {
            switch (value)
            {
                case prjBuildAction.prjBuildActionCompile:
                    return "コンパイル";
                case prjBuildAction.prjBuildActionContent:
                    return "コンテンツ";
                case prjBuildAction.prjBuildActionEmbeddedResource:
                    return "埋め込まれたリソース";
                case prjBuildAction.prjBuildActionNone:
                    return "なし";
            }
            return "不明";
        }
        #endregion

        #region 出力ディレクトリへのコピー

        /// <summary>
        /// 「出力ディレクトリ」へのコピー
        /// </summary>
        /// <param name="projectItem"></param>
        /// <returns></returns>
        public static EnumCopyToOutputDirectory GetCopyToOutputDirectory(ProjectItem projectItem)
        {
            uint copyToOutputDirectoryValue = (uint) GetValue(
                                                         projectItem, ProjectItemFileConst.COPY_TO_OUTPUT_DIRECTORY);
            switch (copyToOutputDirectoryValue)
            {
                case 0:
                    return EnumCopyToOutputDirectory.NotCopy;
                case 1:
                    return EnumCopyToOutputDirectory.EveryTime;
                case 2:
                    return EnumCopyToOutputDirectory.IfModified;
                default:
                    throw new InvalidCastException("EnumCopyToOutputDirectory is [0 - 2].");
            }
        }

        /// <summary>
        /// 「出力ディレクトリ」へのコピー
        /// </summary>
        /// <param name="projectItem"></param>
        /// <param name="value"></param>
        public static void SetCopyToOutputDirectory(ProjectItem projectItem, EnumCopyToOutputDirectory value)
        {
            SetValue(projectItem, ProjectItemFileConst.COPY_TO_OUTPUT_DIRECTORY, value);
        }

        /// <summary>
        /// 「出力ディレクトリへコピー」列挙体を文字列に変換する
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string CopyToOutputDirectoryToString(EnumCopyToOutputDirectory value)
        {
            switch (value)
            {
                case EnumCopyToOutputDirectory.NotCopy:
                    return "コピーしない";
                case EnumCopyToOutputDirectory.EveryTime:
                    return "常にコピーする";
                case EnumCopyToOutputDirectory.IfModified:
                    return "新しい場合はコピー";
            }
            return "不明";
        }

        #endregion

        #region 補助メソッド

        /// <summary>
        /// プロパティ値の取得
        /// </summary>
        /// <param name="projectItem"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private static object GetValue(ProjectItem projectItem, string propertyName)
        {
            if (projectItem == null) throw new ArgumentNullException("projectItem");
            if (propertyName == null) throw new ArgumentNullException("propertyName");
            return projectItem.Properties.Item(propertyName).Value;
        }

        /// <summary>
        /// プロパティ値の設定
        /// </summary>
        /// <param name="projectItem"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        private static void SetValue(ProjectItem projectItem, string propertyName, object value)
        {
            if (projectItem == null) throw new ArgumentNullException("projectItem");
            if (propertyName == null) throw new ArgumentNullException("propertyName");
            projectItem.Properties.Item(propertyName).Value = value;
        }

        #endregion
    }
}
