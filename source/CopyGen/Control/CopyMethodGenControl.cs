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
using System.Windows.Forms;
using AddInCommon.Util;
using CodeGeneratorCore;
using CopyGen.Control.Window;
using CopyGen.Gen;
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
        private const string METHOD_INDENT = "\t\t";
        

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

        #region �C�x���g

        /// <summary>
        /// �R�s�[���������{�^���N���b�N�C�x���g
        /// </summary>
        /// <param name="Ctrl"></param>
        /// <param name="CancelDefault"></param>
        private void generateCode_Click(CommandBarButton Ctrl, ref bool CancelDefault)
        {
            Document document = _applicationObject.ActiveDocument;
            string docExt = Path.GetExtension(document.FullName);
            if (docExt != ".cs")
            {
                MessageUtils.ShowWarnMessage(
                    "[{0}]��\nC#�\�[�X�R�[�h�t�@�C���ł͂Ȃ����߁A�R�s�[�����𐶐��ł��܂���B", 
                    document.FullName);
                return;
            }
            
            try
            {
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
                }

                string configPath = PathUtils.GetConfigPath();
                if(File.Exists(configPath))
                {
                    _copyInfo = CopyInfoFileManager.ReadConfig(configPath);
                }

                CopyBuilder builder = new CopyBuilder(_copyInfo);
                ICodeGenerator generator = builder.CreateCodeGenerator(
                    AssemblyUtils.GetAssemblyName(document), AssemblyUtils.GetTypeName(document));
                if(generator == null)
                {
                    MessageUtils.ShowWarnMessage("�R�s�[�����𐶐��ł��܂���ł����B\n�t�@�C�����ƃN���X������v���Ă��Ȃ��A\n�܂��̓A�h�C���̃C���X�g�[���悪�������ݕs�ɂȂ��Ă��Ȃ������m�F�������B");
                    return;
                }

                TextSelection selection = (TextSelection)document.Selection;
                if (_copyInfo.IsOutputMethod)
                {
                    selection.StartOfLine(vsStartOfLineOptions.vsStartOfLineOptionsFirstColumn, true);
                    selection.Insert(generator.GenerateCode(METHOD_INDENT),
                                     (int) vsInsertFlags.vsInsertFlagsCollapseToEnd);
                }
                else
                {
                    selection.Insert(generator.GenerateCode(string.Empty),
                                     (int)vsInsertFlags.vsInsertFlagsCollapseToEnd);
                }
            }
            catch (System.Exception ex)
            {
                MessageUtils.ShowErrorMessage(ex.Message);
            }
            
        }

        #endregion
    }
}