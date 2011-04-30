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
    /// Projectを扱う処理にCOMExceptionのハンドリングを付加するクラス
    /// </summary>
    public class ProjectEx : Project
    {
        private Project _project;

        public void SetProject(Project project)
        {
            _project = project;
        }

        /// <summary>
        /// プロパティの取得処理委譲
        /// </summary>
        /// <typeparam name="R"></typeparam>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private R InvokeGetValue<R>(string propertyName)
        {
            return COMExceptionInvokeUtils.InvokeGetter<Project, R>(_project, propertyName);
        }

        /// <summary>
        /// プロパティの取得処理委譲
        /// </summary>
        /// <typeparam name="R"></typeparam>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private void InvokeSetValue(string propertyName, object value)
        {
            COMExceptionInvokeUtils.InvokeSetter<Project>(_project, propertyName, value);
        }

        #region Project Methods

        public CodeModel CodeModel
        {
            get { return InvokeGetValue<CodeModel>("CodeModel"); }
        }

        public Projects Collection
        {
            get { return InvokeGetValue<Projects>("Collection"); }
        }

        public ConfigurationManager ConfigurationManager
        {
            get { return InvokeGetValue<ConfigurationManager>("ConfigurationManager"); }
        }

        public DTE DTE
        {
            get { return InvokeGetValue<DTE>("DTE"); }
        }

        public void Delete()
        {
            COMExceptionInvokeUtils.InvokeNoRetMethod<Project>(_project, "Delete", null);
        }

        public string ExtenderCATID
        {
            get { return InvokeGetValue<string>("ExtenderCATID"); }
        }

        public object ExtenderNames
        {
            get { return InvokeGetValue<object>("ExtenderNames"); }
        }

        public string FileName
        {
            get { return InvokeGetValue<string>("FileName"); }
        }

        public string FullName
        {
            get { return InvokeGetValue<string>("FullName"); }
        }

        public Globals Globals
        {
            get { return InvokeGetValue<Globals>("Globals"); }
        }

        public bool IsDirty
        {
            get { return InvokeGetValue<bool>("IsDirty"); }
            set { InvokeSetValue("IsDirty", value); }
        }

        public string Kind
        {
            get { return InvokeGetValue<string>("Kind"); }
        }

        public string Name
        {
            get { return InvokeGetValue<string>("Name"); }
            set { InvokeSetValue("Name", value); }
        }

        public ProjectItem ParentProjectItem
        {
            get { return InvokeGetValue<ProjectItem>("ParentProjectItem"); }
        }

        public ProjectItems ProjectItems
        {
            get { return InvokeGetValue<ProjectItems>("ProjectItems"); }
        }

        public Properties Properties
        {
            get { return InvokeGetValue<Properties>("Properties"); }
        }

        public void Save(string FileName = "")
        {
            COMExceptionInvokeUtils.InvokeNoRetMethod<Project>(_project, "Save", new object[] { FileName });
        }

        public void SaveAs(string NewFileName)
        {
            COMExceptionInvokeUtils.InvokeNoRetMethod<Project>(_project, "SaveAs", new object[] { NewFileName });
        }

        public bool Saved
        {
            get { return InvokeGetValue<bool>("Saved"); }
            set { InvokeSetValue("Saved", value); }
        }

        public string UniqueName
        {
            get { return InvokeGetValue<string>("UniqueName"); }
        }

        public object get_Extender(string ExtenderName)
        {
            return COMExceptionInvokeUtils.InvokeMethod<Project, object>(_project, "get_Extender", new object[] { ExtenderName });
        }

        public object Object
        {
            get { return InvokeGetValue<object>("Object"); }
        }
        #endregion


        
    }
}
