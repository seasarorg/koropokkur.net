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

using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using AddInCommon.Util;
using CodeGeneratorCore;
using CopyGen.Control.Window;
using CopyGen.Gen;
using CopyGen.Util;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.CommandBars;

namespace CopyGen.Control
{
    /// <summary>
    /// �\�����[�V�����A�v���W�F�N�g�v�f��������
    /// </summary>
    public class CopyMethodGenControl
    {
        private const string COPY_GEN = "�R�s�[��������";
        
        
        private readonly DTE2 _applicationObject;


        /// <summary>
        /// �R�s�[���
        /// </summary>
        private CopyInfo _copyInfo = new CopyInfo();
        
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="applicationObject"></param>
        public CopyMethodGenControl(DTE2 applicationObject)
        {
            _applicationObject = applicationObject;
        }

        /// <summary>
        /// �\�����[�V�����E�N���b�N���j���[�ɍ��ڂ���ǉ����ĕԂ�
        /// </summary>
        /// <param name="commandBar"></param>
        /// <returns></returns>
        public virtual CommandBarControl CreateContextMenuItem(CommandBar commandBar)
        {
            CommandBarButton refreshSolutuinButton =
                CommandBarUtils.CreateCommandBarControl<CommandBarButton>(commandBar);
            refreshSolutuinButton.Caption = COPY_GEN;
            refreshSolutuinButton.Click += generateCode_Click;
            return refreshSolutuinButton;
        }

        /// <summary>
        /// �R�s�[�ݒ�̍ēǂݍ��݂��s��
        /// </summary>
        private void RefreshCopyInfo()
        {
            string configPath = PathUtils.GetConfigPath();
            if (File.Exists(configPath))
            {
                _copyInfo = CopyConfigFileManager.ReadConfig(configPath);
            }
        }

        #region �C�x���g

        /// <summary>
        /// �R�s�[���������{�^���N���b�N�C�x���g
        /// </summary>
        /// <param name="Ctrl"></param>
        /// <param name="CancelDefault"></param>
        private void generateCode_Click(CommandBarButton Ctrl, ref bool CancelDefault)
        {
            Document document = _applicationObject.ActiveDocument;
            if (!ProgramLanguageUtils.IsEnableLanguage(document.FullName))
            {
                MessageUtils.ShowWarnMessage(
                    "[{0}]��\n���Ή�����̃R�[�h�t�@�C���̂��߁A�R�s�[�����𐶐��ł��܂���B\n�g�p�\�Ȍ���́uC#�AVB.NET�v�ł��B", 
                    document.FullName);
                return;
            }
            
            try
            {
                RefreshCopyInfo();
                //  ����ݒ肵�Ȃ����ꍇ
                if(_copyInfo.IsEverytimeConfirm)
                {
                    using(CopyConfig config = new CopyConfig())
                    {
                        if(config.ShowDialog() == DialogResult.Cancel)
                        {
                            return;
                        }
                    }
                    RefreshCopyInfo();
                }

                //  ����ˑ��̃��W�b�N�����t�@�N�g�����擾
                ICopyCodeBuildFactory factory = ProgramLanguageUtils.CreateCopyCodePartsBuilder(document.FullName);

                //  �R�s�[����^�����擾
                TextSelection selection = (TextSelection)document.Selection;
                selection.StartOfLine(vsStartOfLineOptions.vsStartOfLineOptionsFirstColumn, false);
                selection.SelectLine();
                //  �R�[�h�o�͊J�n�n�_�̃C���f���g���擾
                string indent = factory.GetIndent(selection.Text);

                //  �R�s�[�����Ώۂ̏��𐶐�
                ICopyTargetBaseInfoCreator destBaseInfoCreator = factory.CreateCopyTargetBaseInfoCreator();
                CopyTargetBaseInfo targetBaseInfo = destBaseInfoCreator.Create(document.FullName, selection.Text);

                //  �Q�Ɛ�A�Z���u���p�X���擾
                string referencePaths = AssemblyUtils.GetReferencePath(document);

                PropertyCodeInfo propertyCodeInfo =
                    CodeInfoUtils.ReadPropertyInfo(referencePaths,
                                                       targetBaseInfo.SourceTypeFullNames,
                                                       targetBaseInfo.DestTypeFullNames);
                
                CopyCodeGeneratorCreationFacade facade = new CopyCodeGeneratorCreationFacade(
                    factory.CreateCopyCodeGeneratorCreator(), _copyInfo, propertyCodeInfo);
                ICodeGenerator generator = facade.CreateCodeGenerator();
                if(generator == null)
                {
                    MessageUtils.ShowWarnMessage("�R�s�[�����𐶐��ł��܂���ł����B\n�t�@�C�����ƃN���X������v���Ă��Ȃ��A\n�܂��̓A�h�C���̃C���X�g�[���悪�������ݕs�ɂȂ��Ă��Ȃ������m�F�������B");
                    return;
                }
                
                if (_copyInfo.IsOutputMethod)
                {
                    selection.StartOfLine(vsStartOfLineOptions.vsStartOfLineOptionsFirstColumn, true);
                    selection.Insert(generator.GenerateCode(indent),
                                     (int) vsInsertFlags.vsInsertFlagsCollapseToEnd);
                }
                else
                {
                    selection.Insert(generator.GenerateCode(indent),
                                     (int)vsInsertFlags.vsInsertFlagsCollapseToEnd);
                }
                //  �����R�[�h�̖����ɃJ�[�\�������킹��
                selection.LineUp(false, 1);
                selection.EndOfLine(false);
            }
            catch (System.Exception ex)
            {
                MessageUtils.ShowErrorMessage(ex.Message + "\n" + ex.StackTrace);
            }
            
        }
        

        #endregion
    }
}