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

using System.ComponentModel;
using AddInCommon.Report;
using EnvDTE;
using VSArrange.Arrange;
using VSArrange.Config;
using System;

namespace VSArrange.Arrange
{
    public delegate void DelegateCompleted(Project completedProject);

    /// <summary>
    /// プロジェクト整理処理をバックグラウンドで実行するクラス
    /// </summary>
    public class BackgroundProjectArranger : ProjectArranger
    {
        private readonly BackgroundWorker _worker = new BackgroundWorker();

        public event Action CompletedEvent;
        private Project _currentProject;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="configInfo"></param>
        /// <param name="reporter"></param>
        public BackgroundProjectArranger(ConfigInfo configInfo, IOutputReport reporter)
            : base(configInfo, reporter)
        {
            _worker.DoWork += Execute;
            _worker.RunWorkerCompleted += Completed;
        }

        public override void ArrangeProject(EnvDTE.Project project)
        {
            // 非同期でプロジェクト整理処理を実行する
            _worker.RunWorkerAsync(project);
        }

        /// <summary>
        /// 処理実行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Execute(object sender, DoWorkEventArgs e)
        {
            var project = (Project)e.Argument;
            _currentProject = project;
            base.ArrangeProject(project);
        }

        private void Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            CompletedEvent();
        }
    }
}
