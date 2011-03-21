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

namespace AddInCommon.Const
{
    /// <summary>
    /// EnvDTE.ProjectItem.Properties用定数クラス
    /// </summary>
    public sealed class ProjectItemFileConst
    {
        /// <summary>
        /// 拡張子(.***)（string)
        /// </summary>
        public const string EXTENSION = "Extension";

        /// <summary>
        /// ファイル名（拡張子付）（string)
        /// </summary>
        public const string FILE_NAME = "FileName";

        /// <summary>
        /// （要調査）
        /// </summary>
        public const string CUSTOM_TOOL_OUTPUT = "CustomToolOutput";

        /// <summary>
        /// （要調査）(DateTime)
        /// </summary>
        public const string DATE_MODIFIED = "DateModified";

        /// <summary>
        /// （要調査）(bool)
        /// </summary>
        public const string IS_LINK = "IsLink";

        /// <summary>
        /// ビルドアクション(prjBuildAction列挙体)
        /// ・prjBuildActionNone＝「なし」
        /// ・prjBuildActionCompile＝「コンパイル」
        /// ・prjBuildActionContent＝「コンテンツ」
        /// ・prjBuildActionEmbeddedResource＝「埋め込まれたリソース」
        /// </summary>
        public const string BUILD_ACTION = "BuildAction";

        /// <summary>
        /// （要調査）(string?)
        /// </summary>
        public const string SUB_TYPE = "SubType";

        /// <summary>
        /// 出力ディレクトリにコピー(uint)
        /// ・0 = 「コピーしない」
        /// ・1 = 「常にコピー」
        /// ・2 = 「新しい場合はコピー」
        /// </summary>
        /// <see cref="EnumCopyToOutputDirectory"/>
        public const string COPY_TO_OUTPUT_DIRECTORY = "CopyToOutputDirectory";

        /// <summary>
        /// （要調査）（bool)
        /// </summary>
        public const string IS_SHARED_DESIGN_TIME_BUILD_INPUT = "IsSharedDesignTimeBuildInput";

        /// <summary>
        /// （要調査）
        /// </summary>
        public const string ITEM_TYPE = "ItemType";

        /// <summary>
        /// （要調査）(bool)
        /// </summary>
        public const string IS_CUSTOM_TOOL_OUTPUT = "IsCustomToolOutput";

        /// <summary>
        /// （要調査）
        /// </summary>
        public const string HTML_TITLE = "HTMLTitle";

        /// <summary>
        /// （要調査）
        /// </summary>
        public const string CUSTOM_TOOL = "CustomTool";

        /// <summary>
        /// （要調査）
        /// </summary>
        public const string URL = "URL";

        /// <summary>
        /// （要調査）
        /// </summary>
        public const string FILE_SIZE = "Filesize";

        /// <summary>
        /// （要調査）
        /// </summary>
        public const string CUSTOM_TOOL_NAMESPACE = "CustomToolNamespace";

        /// <summary>
        /// （要調査）
        /// </summary>
        public const string AUTHOR = "Author";

        /// <summary>
        /// 完全パス（string)
        /// </summary>
        public const string FULL_PATH = "FullPath";

        /// <summary>
        /// （要調査）(bool)
        /// </summary>
        public const string IS_DEPENDENT_FILe = "IsDependentFile";

        /// <summary>
        /// （要調査）(bool)
        /// </summary>
        public const string IS_DESIGN_TIME_BUILD_OUTPUT = "IsDesignTimeBuildInput";

        /// <summary>
        /// ファイル作成日（DateTime）
        /// </summary>
        public const string DATE_CREATED = "DateCreated";

        /// <summary>
        /// ローカルパス（FullPathとほぼ同じ？）(string)
        /// </summary>
        public const string LOCAL_PATH = "LocalPath";

        /// <summary>
        /// （要調査）
        /// </summary>
        public const string MODIFIED_BY = "ModifiedBy";
    }
}
