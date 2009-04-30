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
using System.Text.RegularExpressions;

namespace CopyGen.Control
{
    /// <summary>
    /// ソリューション、プロジェクト要素整理処理
    /// </summary>
    public class CopyMethodGenControl
    {
        private const string COPY_GEN = "コピー処理生成";
        private const string DEFAULT_METHOD_INDENT = "\t\t";
        
        private readonly DTE2 _applicationObject;
        /// <summary>
        /// 空白、タブ以外の文字列を取り出すための正規表現
        /// </summary>
        private readonly Regex _regNotSpace = new Regex(@"[^ \t]");

        /// <summary>
        /// コピー情報
        /// </summary>
        private CopyInfo _copyInfo = new CopyInfo();
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="applicationObject"></param>
        public CopyMethodGenControl(DTE2 applicationObject)
        {
            _applicationObject = applicationObject;
        }

        /// <summary>
        /// ソリューション右クリックメニューに項目を一つ追加して返す
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
        /// インデントの取得
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string GetIndent(string input)
        {
            string indent = _regNotSpace.Replace(input, string.Empty);
            if(indent.Length == 0)
            {
                return DEFAULT_METHOD_INDENT;
            }
            return indent;
        }

        #region イベント

        /// <summary>
        /// コピー処理生成ボタンクリックイベント
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
                    "[{0}]は\nC#ソースコードファイルではないため、コピー処理を生成できません。", 
                    document.FullName);
                return;
            }
            
            try
            {
                //  毎回設定しなおす場合
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

                //  コピーする型名を取得
                TextSelection selection = (TextSelection)document.Selection;
                selection.StartOfLine(vsStartOfLineOptions.vsStartOfLineOptionsFirstColumn, false);
                selection.SelectLine();
                string indent = GetIndent(selection.Text);
                
                CopyTypeNameInfo generateInfo = CopyTypeNameInfo.Create(document.FullName, selection.Text);
                //  参照先アセンブリパスを取得
                string referencePaths = AssemblyUtils.GetReferencePath(document);

                ICodeGenerator generator = builder.CreateCodeGenerator(
                    referencePaths, generateInfo.SourceTypeFullNames, generateInfo.TargetTypeFullNames);
                if(generator == null)
                {
                    MessageUtils.ShowWarnMessage("コピー処理を生成できませんでした。\nファイル名とクラス名が一致していない、\nまたはアドインのインストール先が書き込み不可になっていないかご確認下さい。");
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
                //  生成コードの末尾にカーソルを合わせる
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