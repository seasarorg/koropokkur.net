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
using AddInCommon.Wrapper;
using EnvDTE;

namespace VSArrange.Report.Appender
{
    /// <summary>
    /// プロジェクト要素削除クラス
    /// </summary>
    public class ProjectItemRemover
    {
        private readonly IList<ProjectItem> _deleteTarget;
        private readonly OutputResultManager _outputResultManager;
        private readonly PropertiesEx _properties;
        private readonly ProjectItemEx _projectItem;
        private readonly PropertyEx _property;

        public ProjectItemRemover(IList<ProjectItem> deleteTarget, OutputResultManager outputResultManager)
        {
            _deleteTarget = deleteTarget;
            _outputResultManager = outputResultManager;
            _properties = new PropertiesEx();
            _projectItem = new ProjectItemEx();
            _property = new PropertyEx();
        }

        public void Execute()
        {
            foreach(ProjectItem projectItem in _deleteTarget)
            {
                _projectItem.SetProjectItem(projectItem);
                _properties.SetProperties(_projectItem.Properties);
                _property.SetProperty(_properties.Item("FullPath"));
                var path = (string)_property.Value;

                if (Path.HasExtension(path))
                {
                    _outputResultManager.RegisterRemovedFile(path);
                }
                else
                {
                    _outputResultManager.RegisterRemovedDirectory(path);
                }

                projectItem.Remove();
            }
        }
    }
}
