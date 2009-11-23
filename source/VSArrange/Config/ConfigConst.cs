#region Copyright
/*
 * Copyright 2005-2009 the Seasar Foundation and the Others.
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


namespace VSArrange.Config
{
    /// <summary>
    /// 設定情報定数クラス
    /// </summary>
    public sealed class ConfigConst
    {
        public const string SECTION_ROOT = "koropokkur";

        public const string SECTION_FILTERS = "filters";
        public const string DETAIL_SECTION_FILTER = "filter";
        public const string SUB_SECTION_FILE = "file";
        public const string SUB_SECTION_FOLDER = "folder";
        public const string SUB_SECTION_COMPILE = "compile";
        public const string SUB_SECTION_RESOURCE = "resource";
        public const string SUB_SECTION_CONTENTS = "contents";
        public const string SUB_SECTION_NO_ACTION = "no_action";
        public const string SUB_SECTION_NO_COPY = "no_copy";
        public const string SUB_SECTION_EVERYTIME_COPY = "everytime_copy";
        public const string SUB_SECTION_COPY_IF_NEW = "copy_if_new";

        public const string SECTION_OUTPUT_RESULT = "output_result";
        public const string DETAIL_SECTION_DETAIL = "detail";
        public const string SUB_SECTION_OUTPUT_WINDOW = "window";
        public const string SUB_SECTION_OUTPUT_FILE = "file";

        public const string ATTRIBUTE_NAME = "name";
        public const string ATTRIBUTE_ENABLE = "enable";
    }
}
