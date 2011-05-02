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

using System.Collections;
using AddInCommon.Util;
using EnvDTE;

namespace AddInCommon.Wrapper
{
    /// <summary>
    /// COMExceptionが発生した際に一定時間待機してから再度アクセスするクラス
    /// </summary>
    public class ProjectItemsEx : ProjectItems
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        private ProjectItems _projectItems;

        public void SetProjectItems(ProjectItems projectItems)
        {
            _projectItems = projectItems;
        }

        #region ProjectItems Methods
        public ProjectItem AddFolder(string Name, string Kind = "{6BB5F8EF-4483-11D3-8BCF-00C04F8EC28C}")
        {
            return COMExceptionInvokeUtils.InvokeMethod<ProjectItems, ProjectItem>(_projectItems, "AddFolder", new object[] { Name, Kind });
        }

        public ProjectItem AddFromDirectory(string Directory)
        {
            return COMExceptionInvokeUtils.InvokeMethod<ProjectItems, ProjectItem>(_projectItems, "AddFromDirectory", new object[] { Directory });
        }

        public ProjectItem AddFromFile(string FileName)
        {
            return COMExceptionInvokeUtils.InvokeMethod<ProjectItems, ProjectItem>(_projectItems, "AddFromFile", new object[] { FileName });
        }

        public ProjectItem AddFromFileCopy(string FilePath)
        {
            return COMExceptionInvokeUtils.InvokeMethod<ProjectItems, ProjectItem>(_projectItems, "AddFromFileCopy", new object[] { FilePath });
        }

        public ProjectItem AddFromTemplate(string FileName, string Name)
        {
            return COMExceptionInvokeUtils.InvokeMethod<ProjectItems, ProjectItem>(_projectItems, "AddFromTemplate", new object[] { FileName, Name });
        }

        public Project ContainingProject
        {
            get { return InvokeGetValue<Project>("ContainingProject"); }
        }

        public int Count
        {
            get { return InvokeGetValue<int>("Count"); }
        }

        public DTE DTE
        {
            get { return InvokeGetValue<DTE>("DTE"); }
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return COMExceptionInvokeUtils.InvokeMethod<ProjectItems, IEnumerator>(_projectItems, "GetEnumerator", null);
        }

        public ProjectItem Item(object index)
        {
            return COMExceptionInvokeUtils.InvokeMethod<ProjectItems, ProjectItem>(_projectItems, "Item", new object[] { index });
        }

        public string Kind
        {
            get { return InvokeGetValue<string>("Kind"); }
        }

        public object Parent
        {
            get { return InvokeGetValue<object>("Parent"); }
        }
        #endregion

        /// <summary>
        /// プロパティの取得処理委譲
        /// </summary>
        /// <typeparam name="R"></typeparam>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private R InvokeGetValue<R>(string propertyName)
        {
            return COMExceptionInvokeUtils.InvokeGetter<ProjectItems, R>(_projectItems, propertyName);
        }
    }
}
