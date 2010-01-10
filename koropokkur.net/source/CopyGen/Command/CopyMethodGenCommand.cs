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
using AddInCommon.Command;
using AddInCommon.Util;
using CodeGeneratorCore;
using CopyGen.Control.Window;
using CopyGen.Gen;
using CopyGen.Util;
using EnvDTE;

namespace CopyGen.Command
{
    /// <summary>
    /// コピーメソッド生成処理クラス
    /// </summary>
    public class CopyMethodGenCommand : IDTCExecCommand
    {
        /// <summary>
        /// コピー情報
        /// </summary>
        private CopyInfo _copyInfo = new CopyInfo();

        #region IDTCExecCommand メンバ

        public string CommandName
        {
            get { return "CopyMethodGen"; }
        }

        public string DisplayName
        {
            get { return "コピーメソッドの生成"; }
        }

        public string ToolTipText
        {
            get { return "インスタンスのプロパティ値をコピーする処理を生成します。"; }
        }

        public EnvDTE.vsCommandStatus GetCommandStatus(EnvDTE80.DTE2 applicationObject, EnvDTE.AddIn addInInstance, ref object commandText)
        {
            return VSCommandUtils.GetDefaultStatus();
        }

        public bool Execute(EnvDTE80.DTE2 applicationObject, EnvDTE.AddIn addInInstance, ref object varIn, ref object varOut)
        {
            Document document = applicationObject.ActiveDocument;
            if (!ProgramLanguageUtils.IsEnableLanguage(document.FullName))
            {
                MessageUtils.ShowWarnMessage(
                    "[{0}]は\n未対応言語のコードファイルのため、コピー処理を生成できません。\n使用可能な言語は「C#、VB.NET」です。",
                    document.FullName);
                return false;
            }

            try
            {
                RefreshCopyInfo();
                //  毎回設定しなおす場合
                if (_copyInfo.IsEverytimeConfirm)
                {
                    using (CopyConfig config = new CopyConfig())
                    {
                        if (config.ShowDialog() == DialogResult.Cancel)
                        {
                            return false;
                        }
                    }
                    RefreshCopyInfo();
                }

                //  言語依存のロジック生成ファクトリを取得
                ICopyCodeBuildFactory factory = ProgramLanguageUtils.CreateCopyCodePartsBuilder(document.FullName);

                //  コピーする型名を取得
                TextSelection selection = (TextSelection)document.Selection;
                selection.StartOfLine(vsStartOfLineOptions.vsStartOfLineOptionsFirstColumn, false);
                selection.SelectLine();
                //  コード出力開始地点のインデントを取得
                string indent = factory.GetIndent(selection.Text);

                //  コピー生成対象の情報を生成
                ICopyTargetBaseInfoCreator destBaseInfoCreator = factory.CreateCopyTargetBaseInfoCreator();
                CopyTargetBaseInfo targetBaseInfo = destBaseInfoCreator.Create(document.FullName, selection.Text);

                //  参照先アセンブリパスを取得
                string referencePaths = AssemblyUtils.GetReferencePath(document);

                PropertyCodeInfo propertyCodeInfo =
                    CodeInfoUtils.ReadPropertyInfo(referencePaths,
                                                       targetBaseInfo.SourceTypeFullNames,
                                                       targetBaseInfo.DestTypeFullNames);

                CopyCodeGeneratorCreationFacade facade = new CopyCodeGeneratorCreationFacade(
                    factory.CreateCopyCodeGeneratorCreator(), _copyInfo, propertyCodeInfo);
                ICodeGenerator generator = facade.CreateCodeGenerator();
                if (generator == null)
                {
                    MessageUtils.ShowWarnMessage("コピー処理を生成できませんでした。\nファイル名とクラス名が一致していない、\nまたはアドインのインストール先が書き込み不可になっていないかご確認下さい。");
                    return false;
                }

                if (_copyInfo.IsOutputMethod)
                {
                    selection.StartOfLine(vsStartOfLineOptions.vsStartOfLineOptionsFirstColumn, true);
                    selection.Insert(generator.GenerateCode(indent),
                                     (int)vsInsertFlags.vsInsertFlagsCollapseToEnd);
                }
                else
                {
                    selection.Insert(generator.GenerateCode(indent),
                                     (int)vsInsertFlags.vsInsertFlagsCollapseToEnd);
                }
                //  生成コードの末尾にカーソルを合わせる
                selection.LineUp(false, 1);
                selection.EndOfLine(false);
                return true;
            }
            catch (System.Exception ex)
            {
                MessageUtils.ShowErrorMessage(ex.Message + "\n" + ex.StackTrace);
                return false;
            }
        }

        #endregion

        /// <summary>
        /// コピー設定の再読み込みを行う
        /// </summary>
        private void RefreshCopyInfo()
        {
            string configPath = PathUtils.GetConfigPath();
            if (File.Exists(configPath))
            {
                _copyInfo = CopyConfigFileManager.ReadConfig(configPath);
            }
        }
    }
}
