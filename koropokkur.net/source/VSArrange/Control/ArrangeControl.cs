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
using System.Windows.Forms;
using AddInCommon.Util;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.CommandBars;
using VSArrange.Config;
using StatusBar = EnvDTE.StatusBar;
using VSArrange.Filter;

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

            string projectDirPath = Path.GetDirectoryName(project.FullName) + Path.DirectorySeparatorChar;
            ProjectItems projectItems = project.ProjectItems;

            string statusLabel = string.Format("�v���W�F�N�g[{0}]�̗v�f�𐮗����Ă��܂��B", project.Name);
            ArrangeDirectories(projectDirPath, projectItems, statusLabel);
        }

        /// <summary>
        /// �f�B���N�g�����̃��t���b�V��
        /// </summary>
        /// <param name="dirPath"></param>
        /// <param name="projectItems"></param>
        /// <param name="statusLabel"></param>
        protected virtual void ArrangeDirectories(string dirPath, ProjectItems projectItems, string statusLabel)
        {
            StatusBar bar = _applicationObject.StatusBar;

            IDictionary<string, ProjectItem> registeredItems = new Dictionary<string, ProjectItem>();
            //  �폜����Ă���t�@�C���A�t�H���_���v���W�F�N�g����A�����[�h
            for (int i = 1; i <= projectItems.Count; i++)
            {
                bar.Progress(true, statusLabel, i, projectItems.Count);
                
                ProjectItem projectItem = projectItems.Item(i);

                int beforeItemCount = projectItems.Count;
                string existPath = ArrangeItem(dirPath, projectItem, statusLabel);
                int afterItemCount = projectItems.Count;
                //  �v���W�F�N�g�v�f���폜����ƃR���N�V�����̐���
                //  �ς�邽�߂��̒���
                if (beforeItemCount > afterItemCount)
                {
                    i = i - (beforeItemCount - afterItemCount);
                }

                if (existPath != null)
                {
                    registeredItems.Add(existPath, projectItem);
                }
            }

            LoadNotRegisterFile(dirPath, registeredItems, projectItems);
            LoadNotRegisterDirectory(dirPath, registeredItems, projectItems);
        }

        /// <summary>
        /// ���o�^�̃t�@�C�����v���W�F�N�g�ɒǉ�����
        /// </summary>
        /// <param name="dirPath"></param>
        /// <param name="registeredItems"></param>
        /// <param name="projectItems"></param>
        protected virtual void LoadNotRegisterFile(string dirPath,
                                                   IDictionary<string, ProjectItem> registeredItems,
                                                   ProjectItems projectItems)
        {
            string[] filePaths = Directory.GetFiles(dirPath);
            foreach (string s in filePaths)
            {
                string[] dirNamePathParts = s.Split('\\');
                string fileName = dirNamePathParts[dirNamePathParts.Length - 1];
                if (!registeredItems.ContainsKey(s) &&
                    _filterFile.IsPassFilter(fileName))
                {
                    projectItems.AddFromFile(s);
                }
            }
        }

        /// <summary>
        /// ���o�^�̃t�H���_���v���W�F�N�g�ɒǉ�����
        /// </summary>
        /// <param name="dirPath"></param>
        /// <param name="registeredItems"></param>
        /// <param name="projectItems"></param>
        protected virtual void LoadNotRegisterDirectory(string dirPath,
                                                        IDictionary<string, ProjectItem> registeredItems,
                                                        ProjectItems projectItems)
        {
            string[] dirPaths = Directory.GetDirectories(dirPath);
            foreach (string s in dirPaths)
            {
                string[] dirNamePathParts = s.Split('\\');
                string dirName = dirNamePathParts[dirNamePathParts.Length - 1];
                if (!registeredItems.ContainsKey(s) &&
                    _filterFolder.IsPassFilter(dirName))
                {
                    ProjectItem addedProjectItem = projectItems.AddFromDirectory(s);
                    if (addedProjectItem != null)
                    {
                        //  �璷��������Ȃ����Ō�ɒǉ��ΏۊO�̃t�@�C���A�t�H���_������΍폜
                        //  (AddFromDirectory�ŏ��O�Ώۂ܂Ńv���W�F�N�g�Ɋ܂܂�Ă���
                        //  �\�������邽��)
                        RemoveOutofTarget(addedProjectItem);
                    }
                }
            }
        }

        /// <summary>
        /// �ΏۊO�ł���v���W�F�N�g�v�f���v���W�F�N�g���珜�O
        /// </summary>
        /// <param name="projectItem"></param>
        protected virtual void RemoveOutofTarget(ProjectItem projectItem)
        {
            foreach (ProjectItem item in projectItem.ProjectItems)
            {
                //  ���O�t�B���^�[�Ɉ���������ꍇ�̓v���W�F�N�g����폜
                if(!_filterFile.IsPassFilter(item.Name) ||
                   !_filterFolder.IsPassFilter(item.Name))
                {
                    item.Remove();
                    continue;
                }

                if(item.ProjectItems.Count > 0)
                {
                    RemoveOutofTarget(item);
                }
            }
        }

        /// <summary>
        /// �v�f�̍X�V�i�p�X�����݂��Ȃ��v�f���v���W�F�N�g����폜����j
        /// </summary>
        /// <param name="dirPath"></param>
        /// <param name="item"></param>
        /// <param name="statusLabel"></param>
        protected virtual string ArrangeItem(string dirPath, ProjectItem item, string statusLabel)
        {
            string targetPath = dirPath + item.Name;
            if (File.Exists(targetPath) &&
                _filterFile.IsPassFilter(Path.GetFileName(targetPath)))
            {
                //  ���݂���t�@�C���p�X�̏ꍇ�͍폜�ΏۂƂ��Ȃ�
                return targetPath;
            }

            //  �t�@�C���łȂ���΃t�H���_�H
            if (Directory.Exists(targetPath) &&
                _filterFolder.IsPassFilter(Path.GetFileName(targetPath)))
            {
                string childDirName = targetPath + Path.DirectorySeparatorChar;
                //  �ċA�I�Ɏq�A�C�e��������
                ArrangeDirectories(childDirName, item.ProjectItems, statusLabel);

                return targetPath;
            }
            //  �t�@�C���Ƃ��Ă��t�H���_�Ƃ��Ă����݂��Ȃ��Ȃ�
            //  �v���W�F�N�g����O��
            item.Remove();
            return null;
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