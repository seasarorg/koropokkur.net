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
using VSArrange.Arrange;
using VSArrange.Config;

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
            ProjectArranger arranger = CreateArranger();
            try
            {
                foreach (Project project in solution.Projects)
                {
                    arranger.ArrangeProject(project);
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

            try
            {
                ProjectArranger arranger = CreateArranger();
                //  �I������Ă���v�f�͎���������̂͂�����
                //  �R���N�V�����̌`�ł����擾�ł��Ȃ�����foreach�ł܂킷
                foreach (SelectedItem selectedItem in items)
                {
                    Project currentProject = selectedItem.Project;

                    if (refreshedProjects.ContainsKey(currentProject.FullName))
                    {
                        //  �X�V�ς̃v���W�F�N�g�͖���
                        continue;
                    }
                    arranger.ArrangeProject(currentProject);

                    _applicationObject.StatusBar.Text = string.Format(
                        "{0}�̐������I�����܂����B", currentProject.Name);
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

        /// <summary>
        /// �ݒ���ēǂݍ���
        /// </summary>
        /// <remarks>
        //  �ݒ肪�ύX���ꂽ���_�ŗ\�ߔ񓯊��œǂ�ł����������ǂ���
        //  �p�t�H�[�}���X�I�ɐ����������O�ɓǂ�ł���肪�Ȃ��Ǝv���邽��
        //  ������P���ɂ���{�R����Ȃ������߂����ŌĂяo��
        /// </remarks>
        private ProjectArranger CreateArranger()
        {
            //  �ݒ�ǂݍ���
            ConfigInfo configInfo = ConfigFileManager.ReadConfig(PathUtils.GetConfigPath());
            return new ProjectArranger(configInfo);
        }
    }
}