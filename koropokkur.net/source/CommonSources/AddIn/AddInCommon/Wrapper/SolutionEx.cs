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
    /// Solutionを扱う処理にCOMExceptionのハンドリングを付加するクラス
    /// </summary>
    public class SolutionEx : Solution
    {
        private Solution _solution;

        public void SetSolution(Solution solution)
        {
            _solution = solution;
        }

        /// <summary>
        /// プロパティの取得処理委譲
        /// </summary>
        /// <typeparam name="R"></typeparam>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private R InvokeGetValue<R>(string propertyName)
        {
            return COMExceptionInvokeUtils.InvokeGetter<_Solution, R>(_solution, propertyName);
        }

        /// <summary>
        /// プロパティの設定処理委譲
        /// </summary>
        /// <typeparam name="R"></typeparam>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private void InvokeSetValue(string propertyName, object value)
        {
            COMExceptionInvokeUtils.InvokeSetter<_Solution>(_solution, propertyName, value);
        }

        #region Solution Methods

        public Project AddFromFile(string FileName, bool Exclusive = false)
        {
            return COMExceptionInvokeUtils.InvokeMethod<_Solution, Project>(_solution, "AddFromFile", 
                new object[] { FileName, Exclusive });
        }

        public Project AddFromTemplate(string FileName, string Destination, string ProjectName, bool Exclusive = false)
        {
            return COMExceptionInvokeUtils.InvokeMethod<_Solution, Project>(_solution, "AddFromTemplate",
                new object[] { FileName, Destination, ProjectName,  Exclusive });
        }

        public AddIns AddIns
        {
            get { return InvokeGetValue<AddIns>("AddIns"); }
        }

        public void Close(bool SaveFirst = false)
        {
            if (_solution != null)
            {
                COMExceptionInvokeUtils.InvokeNoRetMethod<_Solution>(_solution, "Close", new object[] { SaveFirst });
            }
        }

        public int Count
        {
            get { return InvokeGetValue<int>("Count"); }
        }

        public void Create(string Destination, string Name)
        {
            COMExceptionInvokeUtils.InvokeNoRetMethod<_Solution>(_solution, "Create", new object[] { Destination, Name });
        }

        public DTE DTE
        {
            get { return InvokeGetValue<DTE>("DTE"); }
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

        public ProjectItem FindProjectItem(string FileName)
        {
            return COMExceptionInvokeUtils.InvokeMethod<_Solution, ProjectItem>(_solution, "FindProjectItem", new object[] { FileName });
        }

        public string FullName
        {
            get { return InvokeGetValue<string>("FullName"); }
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return COMExceptionInvokeUtils.InvokeMethod<_Solution, IEnumerator>(_solution, "GetEnumerator", null);
        }

        public Globals Globals
        {
            get { return InvokeGetValue<Globals>("Globals"); }
        }

        public bool IsDirty
        {
            get { return InvokeGetValue<bool>("IsDirty"); }
            set { COMExceptionInvokeUtils.InvokeSetter<_Solution>(_solution, "IsDirty", value); }
        }

        public bool IsOpen
        {
            get 
            {
                if (_solution == null)
                {
                    return false;
                }
                return InvokeGetValue<bool>("IsOpen"); 
            }
        }

        public Project Item(object index)
        {
            return COMExceptionInvokeUtils.InvokeMethod<_Solution, Project>(_solution, "Item", new object[] { index });
        }

        public void Open(string FileName)
        {
            COMExceptionInvokeUtils.InvokeNoRetMethod<_Solution>(_solution, "Open", new object[] { FileName });
        }

        public DTE Parent
        {
            get { return InvokeGetValue<DTE>("Parent"); }
        }

        public string ProjectItemsTemplatePath(string ProjectKind)
        {
            return COMExceptionInvokeUtils.InvokeMethod<_Solution, string>(_solution, "ProjectItemsTemplatePath", new object[] { ProjectKind });
        }

        public Projects Projects
        {
            get { return InvokeGetValue<Projects>("Projects"); }
        }

        public Properties Properties
        {
            get { return InvokeGetValue<Properties>("Properties"); }
        }

        public void Remove(Project proj)
        {
            COMExceptionInvokeUtils.InvokeNoRetMethod<_Solution>(_solution, "Remove", new object[] { proj });
        }

        public void SaveAs(string FileName)
        {
            COMExceptionInvokeUtils.InvokeNoRetMethod<_Solution>(_solution, "SaveAs", new object[] { FileName });
        }

        public bool Saved
        {
            get { return InvokeGetValue<bool>("Saved"); }
            set { COMExceptionInvokeUtils.InvokeSetter<_Solution>(_solution, "Saved", value); }
        }

        public SolutionBuild SolutionBuild
        {
            get { return InvokeGetValue<SolutionBuild>("SolutionBuild"); }
        }

        public object get_Extender(string ExtenderName)
        {
            return COMExceptionInvokeUtils.InvokeMethod<_Solution, object>(_solution, "get_Extender", new object[] { ExtenderName });
        }

        public string get_TemplatePath(string ProjectType)
        {
            return COMExceptionInvokeUtils.InvokeMethod<_Solution, string>(_solution, "get_TemplatePath", new object[] { ProjectType });
        }
        #endregion
    }
}
