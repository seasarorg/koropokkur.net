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

using AddInCommon.Util;
using EnvDTE;

namespace AddInCommon.Wrapper
{
    /// <summary>
    /// ProjectItemを扱う処理にCOMExceptionのハンドリングを付加するクラス
    /// </summary>
    public class ProjectItemEx : ProjectItem
    {
        private ProjectItem _projectItem;

        public void SetProjectItem(ProjectItem projectItem)
        {
            _projectItem = projectItem;
        }

        /// <summary>
        /// プロパティの取得処理委譲
        /// </summary>
        /// <typeparam name="R"></typeparam>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private R InvokeGetValue<R>(string propertyName)
        {
            return COMExceptionInvokeUtils.InvokeGetter<ProjectItem, R>(_projectItem, propertyName);
        }

        #region ProjectItem Methods

        public ProjectItems Collection
        {
            get { return InvokeGetValue<ProjectItems>("Collection"); }
        }

        public ConfigurationManager ConfigurationManager
        {
            get { return InvokeGetValue<ConfigurationManager>("ConfigurationManager"); }
        }

        public Project ContainingProject
        {
            get { return InvokeGetValue<Project>("ContainingProject"); }
        }

        public DTE DTE
        {
            get { return InvokeGetValue<DTE>("DTE"); }
        }

        public void Delete()
        {
            COMExceptionInvokeUtils.InvokeNoRetMethod<ProjectItem>(_projectItem, "Delete", null);
        }

        public Document Document
        {
            get { return InvokeGetValue<Document>("Document"); }
        }

        public void ExpandView()
        {
            COMExceptionInvokeUtils.InvokeNoRetMethod<ProjectItem>(_projectItem, "ExpandView", null);
        }

        public string ExtenderCATID
        {
            get { return InvokeGetValue<string>("ExtenderCATID"); }
        }

        public object ExtenderNames
        {
            get { return InvokeGetValue<object>("ExtenderNames"); }
        }

        public FileCodeModel FileCodeModel
        {
            get { return InvokeGetValue<FileCodeModel>("FileCodeModel"); }
        }

        public short FileCount
        {
            get { return InvokeGetValue<short>("FileCount"); }
        }

        public bool IsDirty
        {
            get { return InvokeGetValue<bool>("IsDirty"); }
            set { COMExceptionInvokeUtils.InvokeSetter<ProjectItem>(_projectItem, "IsDirty", value); }
        }

        public string Kind
        {
            get { return InvokeGetValue<string>("Kind"); }
        }

        public string Name
        {
            get { return InvokeGetValue<string>("Name"); }
            set { COMExceptionInvokeUtils.InvokeSetter<ProjectItem>(_projectItem, "Name", value); }
        }

        public object Object
        {
            get { return InvokeGetValue<object>("Object"); }
        }

        public Window Open(string ViewKind = "{00000000-0000-0000-0000-000000000000}")
        {
            return COMExceptionInvokeUtils.InvokeMethod<ProjectItem, Window>(_projectItem, "Open", new object[] { ViewKind });
        }

        public ProjectItems ProjectItems
        {
            get { return InvokeGetValue<ProjectItems>("ProjectItems"); }
        }

        public Properties Properties
        {
            get { return InvokeGetValue<Properties>("Properties"); }
        }

        public void Remove()
        {
            COMExceptionInvokeUtils.InvokeNoRetMethod<ProjectItem>(_projectItem, "Remove", null);
        }

        public void Save(string FileName = "")
        {
            COMExceptionInvokeUtils.InvokeNoRetMethod<ProjectItem>(_projectItem, "Remove", new object[] { FileName });
        }

        public bool SaveAs(string NewFileName)
        {
            return COMExceptionInvokeUtils.InvokeMethod<ProjectItem, bool>(_projectItem, "SaveAs", new object[] { NewFileName });
        }

        public bool Saved
        {
            get { return InvokeGetValue<bool>("Saved"); }
            set { COMExceptionInvokeUtils.InvokeSetter<ProjectItem>(_projectItem, "Saved", value); }
        }

        public Project SubProject
        {
            get { return InvokeGetValue<Project>("SubProject"); }
        }

        public object get_Extender(string ExtenderName)
        {
            return COMExceptionInvokeUtils.InvokeMethod<ProjectItem, bool>(_projectItem, "get_Extender", new object[] { ExtenderName });
        }

        public string get_FileNames(short index)
        {
            return COMExceptionInvokeUtils.InvokeMethod<ProjectItem, string>(_projectItem, "get_FileNames", new object[] { index });
        }

        public bool get_IsOpen(string ViewKind = "{FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF}")
        {
            return COMExceptionInvokeUtils.InvokeMethod<ProjectItem, bool>(_projectItem, "get_IsOpen", new object[] { ViewKind });
        }
        #endregion
    }
}
