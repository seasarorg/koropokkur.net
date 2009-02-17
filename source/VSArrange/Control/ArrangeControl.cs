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

using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using AddInCommon.Util;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.CommandBars;
using VSArrange.Config;
using StatusBar = EnvDTE.StatusBar;
using VSArrange.Filter;
using System.Text;
using Thread=System.Threading.Thread;

namespace VSArrange.Control
{
    /// <summary>
    /// �\�����[�V�����A�v���W�F�N�g�v�f��������
    /// </summary>
    public class ArrangeControl
    {
        private const string REFRESH_BUTTON_NAME_SOLUTION = "�S�v���W�F�N�g�v�f�̐���";
        private const string REFRESH_BUTTON_NAME_PROJECT = "�v���W�F�N�g�v�f�̐���";

        private readonly DTE2 _applicationObject;

        /// <summary>
        /// �v���W�F�N�g���ڂƂ��Ȃ��t�@�C���𔻕ʂ��鐳�K�\��
        /// </summary>
        private ItemAttachmentFilter _filterFile;

        /// <summary>
        /// �t�@�C���o�^���O�t�B���^�[
        /// </summary>
        public ItemAttachmentFilter FileterFile
        {
            set { _filterFile = value; }
            get { return _filterFile; }
        }

        /// <summary>
        /// �v���W�F�N�g���ڂƂ��Ȃ��t�H���_�𔻕ʂ��鐳�K�\��
        /// </summary>
        private ItemAttachmentFilter _filterFolder;

        /// <summary>
        /// �t�H���_�[�o�^���O�t�B���^�[
        /// </summary>
        public ItemAttachmentFilter FilterFolder
        {
            set { _filterFolder = value; }
            get { return _filterFolder; }
        }
        
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="applicationObject"></param>
        public ArrangeControl(DTE2 applicationObject)
        {
            _applicationObject = applicationObject;
        }

        /// <summary>
        /// �\�����[�V�����E�N���b�N���j���[�ɍ��ڂ���ǉ����ĕԂ�
        /// </summary>
        /// <param name="commandBar"></param>
        /// <returns></returns>
        public virtual CommandBarControl CreateSolutionContextMenuItem(CommandBar commandBar)
        {
            CommandBarButton refreshSolutuinButton =
                CommandBarUtils.CreateCommandBarControl<CommandBarButton>(commandBar);
            refreshSolutuinButton.Caption = REFRESH_BUTTON_NAME_SOLUTION;
            refreshSolutuinButton.Click += refreshSolutuinButton_Click;
            return refreshSolutuinButton;
        }

        /// <summary>
        /// �v���W�F�N�g�E�N���b�N���j���[�ɍ��ڂ���ǉ����ĕԂ�
        /// </summary>
        /// <param name="commandBar"></param>
        /// <returns></returns>
        public virtual CommandBarControl CreateProjectContextMenuItem(CommandBar commandBar)
        {
            CommandBarButton refreshProjectButton =
                CommandBarUtils.CreateCommandBarControl<CommandBarButton>(commandBar);
            refreshProjectButton.Caption = REFRESH_BUTTON_NAME_PROJECT;
            refreshProjectButton.Click += refreshProjectButton_Click;
            return refreshProjectButton;
        }

        #region �C�x���g

        /// <summary>
        /// �\�����[�V���������{�^���N���b�N�C�x���g
        /// </summary>
        /// <param name="Ctrl"></param>
        /// <param name="CancelDefault"></param>
        private void refreshSolutuinButton_Click(CommandBarButton Ctrl, ref bool CancelDefault)
        {
            Solution solution = _applicationObject.Solution;

            //  �v���W�F�N�g�ǉ��t�B���^�̍X�V
            //  �ݒ肪�ύX���ꂽ���_�ŗ\�ߔ񓯊��œǂ�ł����������ǂ���
            //  �p�t�H�[�}���X�I�ɐ����������O�ɓǂ�ł���肪�Ȃ��Ǝv���邽��
            //  ������P���ɂ���{�R����Ȃ������߂����ŌĂяo��
            RefreshConfigInfo();
            try
            {
                foreach (Project project in solution.Projects)
                {
                    ArrangeProject(project);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
            finally
            {
                StatusBarUtils.Clear(_applicationObject);
            }
        }

        /// <summary>
        /// �v���W�F�N�g�����{�^���N���b�N�C�x���g
        /// </summary>
        /// <param name="Ctrl"></param>
        /// <param name="CancelDefault"></param>
        private void refreshProjectButton_Click(CommandBarButton Ctrl, ref bool CancelDefault)
        {
            IDictionary<string, Project> refreshedProjects = new Dictionary<string, Project>();
            SelectedItems items = _applicationObject.SelectedItems;

            //  �v���W�F�N�g�ǉ��t�B���^�̍X�V
            //  �ݒ肪�ύX���ꂽ���_�ŗ\�ߔ񓯊��œǂ�ł����������ǂ���
            //  �p�t�H�[�}���X�I�ɐ����������O�ɓǂ�ł���肪�Ȃ��Ǝv���邽��
            //  ������P���ɂ���{�R����Ȃ������߂����ŌĂяo��
            RefreshConfigInfo();
            try
            {
                foreach (SelectedItem selectedItem in items)
                {
                    Project currentProject = selectedItem.Project;
                    if (refreshedProjects.ContainsKey(currentProject.FullName))
                    {
                        //  �X�V�ς̃v���W�F�N�g�͖���
                        continue;
                    }

                    ArrangeProject(currentProject);
                    refreshedProjects[currentProject.FullName] = currentProject;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
            finally
            {
                refreshedProjects.Clear();
                StatusBarUtils.Clear(_applicationObject);
            }
        }

        #endregion

        #region ���t���b�V������

        /// <summary>
        /// �v���W�F�N�g�̃��t���b�V��
        /// </summary>
        /// <param name="project"></param>
        protected virtual void ArrangeProject(Project project)
        {
            if (string.IsNullOrEmpty(project.FullName))
            {
                //  �v���W�F�N�g���������Ă��Ȃ��v�f�͖���
                return;    
            }

            string projectDirPath = Path.GetDirectoryName(project.FullName);
            ProjectItems projectItems = project.ProjectItems;

            string statusLabel = string.Format("�v���W�F�N�g[{0}]�̗v�f�𐮗����Ă��܂��B", project.Name);
            ArrangeDirectories(projectDirPath, projectItems, statusLabel);

            _applicationObject.StatusBar.Text = string.Format("{0}�̐������I�����܂����B", project.Name);
        }

        protected virtual void ArrangeDirectories(string dirPath, ProjectItems projectItems, string statusLabel)
        {
            _applicationObject.StatusBar.Text = string.Format("{0}�𐮗����Ă��܂��B", dirPath);
            IDictionary<string, ProjectItem> fileItems = new Dictionary<string, ProjectItem>();
            IDictionary<string, ProjectItem> folderItems = new Dictionary<string, ProjectItem>();
            IList<ProjectItem> deleteTarget = new List<ProjectItem>();

            string basePath = dirPath + Path.DirectorySeparatorChar;
            //  �t�@�C���A�t�H���_�A�폜�ΏۂɐU�蕪����
            foreach (ProjectItem projectItem in projectItems)
            {
                string currentPath = basePath + projectItem.Name;
                if(Directory.Exists(currentPath))
                {
                    if(_filterFolder.IsPassFilter(projectItem.Name))
                    {
                        //  ���ۂɑ��݂��Ă��Ċ��v���W�F�N�g�ɂ��o�^��
                        if(!folderItems.ContainsKey(currentPath))
                        {
                            folderItems.Add(currentPath, projectItem);
                        }
                    }
                    else
                    {
                        deleteTarget.Add(projectItem);
                    }
                }
                else if(File.Exists(currentPath))
                {
                    if(_filterFile.IsPassFilter(projectItem.Name))
                    {
                        if(!fileItems.ContainsKey(currentPath))
                        {
                            fileItems.Add(currentPath, projectItem);
                        }
                    }
                    else
                    {
                        deleteTarget.Add(projectItem);
                    }
                }
                else
                {
                    deleteTarget.Add(projectItem);
                }
            }

            ////  �f�B���N�g���ǉ�
            //string[] subDirPaths = Directory.GetDirectories(dirPath);
            //foreach (string subDirPath in subDirPaths)
            //{
            //    string[] dirPathParts = subDirPath.Split('\\');
            //    string dirName = dirPathParts[dirPathParts.Length - 1];
            //    if (_filterFolder.IsPassFilter(dirName) &&
            //        !folderItems.ContainsKey(subDirPath))
            //    {
            //        //  �܂��ǉ����Ă��Ȃ����̂̂ݒǉ�
            //        ProjectItem newItem = projectItems.AddFromDirectory(subDirPath);
            //        folderItems.Add(subDirPath, newItem);
            //    }
            //}
            DirectoryAppender directoryAppender = new DirectoryAppender(
                dirPath, _filterFolder, projectItems, folderItems);
            directoryAppender.Execute();
            //System.Threading.Thread dirThread = new Thread(
            //    new ThreadStart(directoryAppender.Execute));
            //dirThread.Start();

            ////  �t�@�C���ǉ�
            //string[] subFilePaths = Directory.GetFiles(dirPath);
            //foreach (string subFilePath in subFilePaths)
            //{
            //    string[] filePathParts = subFilePath.Split('\\');
            //    string fileName = filePathParts[filePathParts.Length - 1];
            //    if (_filterFile.IsPassFilter(fileName) &&
            //        !fileItems.ContainsKey(subFilePath))
            //    {
            //        //  �܂��ǉ����Ă��Ȃ����̂̂ݒǉ�
            //        projectItems.AddFromFile(subFilePath);
            //    }
            //}
            FileAppender fileAppender = new FileAppender(
                dirPath, _filterFile, projectItems, fileItems);
            //fileAppender.Execute();
            System.Threading.Thread fileThread = new Thread(
                new ThreadStart(fileAppender.Execute));
            fileThread.Start();

            ////  �s�v�ȗv�f�͍폜
            //foreach (ProjectItem projectItem in deleteTarget)
            //{
            //    projectItem.Remove();
            //}
            ProjectItemRemover projectItemRemover = new ProjectItemRemover(deleteTarget);
            //projectItemRemover.Execute();
            System.Threading.Thread removeThread = new Thread(
                new ThreadStart(projectItemRemover.Execute));
            removeThread.Start();

            //dirThread.Join();
            //fileThread.Join();
            //removeThread.Join();

            //  �c�����t�H���_�ɑ΂��ē��l�̏������ċA�I�Ɏ��s
            foreach (string projectDirPath in folderItems.Keys)
            {
                ProjectItem dirItem = folderItems[projectDirPath];
                //ArrangeDirectories(projectDirPath, dirItem.ProjectItems, statusLabel);

                Thread thread = new Thread(ExecuteArrange);
                thread.Start(new object[] { projectDirPath, dirItem.ProjectItems, statusLabel });
            }

        }

        private void ExecuteArrange(object parameter)
        {
            object[] parameters = parameter as object[];
            if(parameters == null)
            {
                return;
            }
            ArrangeDirectories(parameters[0] as string,
                parameters[1] as ProjectItems,
                parameters[2] as string);

            _applicationObject.StatusBar.Text = "";
        }

        private class ProjectItemRemover
        {
            private readonly IList<ProjectItem> _deleteTarget;

            public ProjectItemRemover(IList<ProjectItem> deleteTarget)
            {
                _deleteTarget = deleteTarget;
            }

            public void Execute()
            {
                foreach (ProjectItem projectItem in _deleteTarget)
                {
                    projectItem.Remove();
                }
            }
        }

        private class FileAppender
        {
            private readonly string _dirPath;
            private readonly ItemAttachmentFilter _filter;
            private readonly ProjectItems _projectItems;
            private readonly IDictionary<string, ProjectItem> _fileItems;

            public FileAppender(
                string dirPath, ItemAttachmentFilter filter, 
                ProjectItems projectItems, IDictionary<string, ProjectItem> fileItems)
            {
                _dirPath = dirPath;
                _filter = filter;
                _projectItems = projectItems;
                _fileItems = fileItems;
            }

            /// <summary>
            /// �t�@�C���ǉ����s
            /// </summary>
            public void Execute()
            {
                string[] subFilePaths = Directory.GetFiles(_dirPath);
                foreach (string subFilePath in subFilePaths)
                {
                    string[] filePathParts = subFilePath.Split('\\');
                    string fileName = filePathParts[filePathParts.Length - 1];
                    if (_filter.IsPassFilter(fileName) &&
                        !_fileItems.ContainsKey(subFilePath))
                    {
                        //  �܂��ǉ����Ă��Ȃ����̂̂ݒǉ�
                        _projectItems.AddFromFile(subFilePath);
                    }
                }
            }
        }
        
        /// <summary>
        /// �f�B���N�g���ǉ��N���X
        /// </summary>
        private class DirectoryAppender
        {
            private readonly string _dirPath;
            private readonly ItemAttachmentFilter _filter;
            private readonly ProjectItems _projectItems;
            private readonly IDictionary<string, ProjectItem> _folderItems;

            public DirectoryAppender(
                string dirPath, ItemAttachmentFilter filter, 
                ProjectItems projectItems, IDictionary<string, ProjectItem> folderItems)
            {
                _dirPath = dirPath;
                _filter = filter;
                _projectItems = projectItems;
                _folderItems = folderItems;
            }

            /// <summary>
            /// �f�B���N�g���ǉ����s
            /// </summary>
            public void Execute()
            {
                string[] subDirPaths = Directory.GetDirectories(_dirPath);
                foreach (string subDirPath in subDirPaths)
                {
                    string[] dirPathParts = subDirPath.Split('\\');
                    string dirName = dirPathParts[dirPathParts.Length - 1];
                    if (_filter.IsPassFilter(dirName) &&
                        !_folderItems.ContainsKey(subDirPath))
                    {
                        //  �܂��ǉ����Ă��Ȃ����̂̂ݒǉ�
                        ProjectItem newItem = _projectItems.AddFromDirectory(subDirPath);
                        _folderItems.Add(subDirPath, newItem);
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// �ݒ���ēǂݍ���
        /// </summary>
        private void RefreshConfigInfo()
        {
            //  �ݒ�ǂݍ���
            ConfigInfo configInfo = ConfigFileManager.ReadConfig(PathUtils.GetConfigPath());
            if (configInfo != null)
            {
                //  �v���W�F�N�g�v�f�ǉ����O�t�B���^�[��ݒ�
                if (configInfo.FilterFileStringList != null)
                {
                    _filterFile.Clear();
                    _filterFile.AddFilters(configInfo.FilterFileStringList);
                }

                if (configInfo.FilterFolderStringList != null)
                {
                    _filterFolder.Clear();
                    _filterFolder.AddFilters(configInfo.FilterFolderStringList);
                }
            }
        }
    }
}