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

namespace VSArrange.Const
{
    /// <summary>
    /// VSArrange定数クラス
    /// </summary>
    public static class VSArrangeConst
    {
        /// <summary>
        /// アドイン名
        /// </summary>
        public const string ADDIN_NAME = "VSArrange";

        public const string SUPPORTED_EXT_SLN = ".sln";
        public const string SUPPORTED_EXT_CSPROJ = ".csproj";
        public const string SUPPORTED_EXT_VBPROJ = ".vbproj";

        /// <summary>
        /// サポートしている拡張子一覧
        /// </summary>
        public static readonly string[] SUPPORTED_EXTENSIONS = new string[] {
            SUPPORTED_EXT_SLN,
            SUPPORTED_EXT_CSPROJ,
            SUPPORTED_EXT_VBPROJ
        };

        /// <summary>
        /// サポートしているプロジェクトファイルの拡張子
        /// </summary>
        public const string SUPPORTED_EXT_PROJ = SUPPORTED_EXT_CSPROJ + "," + SUPPORTED_EXT_VBPROJ;

        /// <summary>
        /// サポートしているファイルの拡張子
        /// </summary>
        public const string SUPPORTED_EXT = SUPPORTED_EXT_SLN + "," + SUPPORTED_EXT_PROJ;
        
    }
}
