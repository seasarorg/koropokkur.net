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

using System;
using System.IO;
using System.Windows.Forms;
using AddInCommon.Util;
using CodeGeneratorCore.Enum;
using CopyGen.Gen;

namespace CopyGen.Control
{
    /// <summary>
    /// コピー情報設定画面
    /// </summary>
    public partial class CopyConfig : Form
    {

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CopyConfig()
        {
            InitializeComponent();
        }

        #region 補助メソッド

        /// <summary>
        /// 設定情報の取得
        /// </summary>
        /// <returns></returns>
        private CopyInfo GetConfigInfo()
        {
            try
            {
                string configPath = PathUtils.GetConfigPath();
                if (File.Exists(configPath))
                {
                    return CopyConfigFileManager.ReadConfig(configPath);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(string.Format("設定情報の読み込みに失敗しました。\n{0}", ex.Message));
            }
            return null;
        }

        /// <summary>
        /// 設定情報をロードし直す
        /// </summary>
        private void Reload()
        {
            CopyInfo copyInfo = GetConfigInfo();
            if (copyInfo == null)
            {
                return;
            }

            rdoAsMethod.Checked = copyInfo.IsOutputMethod;
            rdoAsCopyOnly.Checked = !copyInfo.IsOutputMethod;

            SetupViewVisibility(copyInfo);
            SetupViewMethodOption(copyInfo);

            txtNameInput.Text = copyInfo.MethodName;

            SetupViewCopySource(copyInfo);
            SetupViewCopyDest(copyInfo);

            chkEverytimeConfirm.Checked = copyInfo.IsEverytimeConfirm;

            ControlInput();
        }

        #region 設定を画面へ反映する

        /// <summary>
        /// アクセス修飾子設定を画面へ反映する
        /// </summary>
        /// <param name="copyInfo"></param>
        private void SetupViewVisibility(CopyInfo copyInfo)
        {
            switch (copyInfo.Visibility)
            {
                case EnumVisibility.Public:
                    rdoPublic.Checked = true;
                    rdoProtected.Checked = false;
                    rdoPrivate.Checked = false;
                    rdoInternal.Checked = false;
                    break;
                case EnumVisibility.Protected:
                    rdoPublic.Checked = false;
                    rdoProtected.Checked = true;
                    rdoPrivate.Checked = false;
                    rdoInternal.Checked = false;
                    break;
                case EnumVisibility.Private:
                    rdoPublic.Checked = false;
                    rdoProtected.Checked = false;
                    rdoPrivate.Checked = true;
                    rdoInternal.Checked = false;
                    break;
                case EnumVisibility.Internal:
                    rdoPublic.Checked = false;
                    rdoProtected.Checked = false;
                    rdoPrivate.Checked = false;
                    rdoInternal.Checked = true;
                    break;
                default:
                    rdoPublic.Checked = true;
                    rdoProtected.Checked = false;
                    rdoPrivate.Checked = false;
                    rdoInternal.Checked = false;
                    break;
            }
        }

        /// <summary>
        /// メソッド付加設定の画面への反映
        /// </summary>
        /// <param name="copyInfo"></param>
        private void SetupViewMethodOption(CopyInfo copyInfo)
        {
            switch (copyInfo.MethodOption)
            {
                case EnumMethodOption.None:
                    rdoOptionNone.Checked = true;
                    rdoOptionStatic.Checked = false;
                    rdoOptionVirtual.Checked = false;
                    rdoOptionOverride.Checked = false;
                    break;
                case EnumMethodOption.Static:
                    rdoOptionNone.Checked = false;
                    rdoOptionStatic.Checked = true;
                    rdoOptionVirtual.Checked = false;
                    rdoOptionOverride.Checked = false;
                    break;
                case EnumMethodOption.Virtual:
                    rdoOptionNone.Checked = false;
                    rdoOptionStatic.Checked = false;
                    rdoOptionVirtual.Checked = true;
                    rdoOptionOverride.Checked = false;
                    break;
                case EnumMethodOption.Override:
                    rdoOptionNone.Checked = false;
                    rdoOptionStatic.Checked = false;
                    rdoOptionVirtual.Checked = false;
                    rdoOptionOverride.Checked = true;
                    break;
                default:
                    rdoOptionNone.Checked = true;
                    rdoOptionStatic.Checked = false;
                    rdoOptionVirtual.Checked = false;
                    rdoOptionOverride.Checked = false;
                    break;
            }
        }

        /// <summary>
        /// コピー先設定を画面へ反映させる
        /// </summary>
        /// <param name="copyInfo"></param>
        private void SetupViewCopyDest(CopyInfo copyInfo)
        {
            switch (copyInfo.CopyDest)
            {
                case EnumCopyDest.Return:
                    rdoDestReturn.Checked = true;
                    rdoDestArgument.Checked = false;
                    rdoDestThis.Checked = false;
                    rdoDestProperty.Checked = false;
                    break;
                case EnumCopyDest.AsArgument:
                    rdoDestReturn.Checked = false;
                    rdoDestArgument.Checked = true;
                    rdoDestThis.Checked = false;
                    rdoDestProperty.Checked = false;
                    break;
                case EnumCopyDest.This:
                    rdoDestReturn.Checked = false;
                    rdoDestArgument.Checked = false;
                    rdoDestThis.Checked = true;
                    rdoDestProperty.Checked = false;
                    break;
                case EnumCopyDest.PropertyOnly:
                    rdoDestReturn.Checked = false;
                    rdoDestArgument.Checked = false;
                    rdoDestThis.Checked = false;
                    rdoDestProperty.Checked = true;
                    break;
                default:
                    rdoDestReturn.Checked = true;
                    rdoDestArgument.Checked = false;
                    rdoDestThis.Checked = false;
                    rdoDestProperty.Checked = false;
                    break;
            }

            txtDestArgumentName.Text = copyInfo.DestArgumentName;
            chkDestIfNullCheck.Checked = copyInfo.IsNotNullDest;
        }

        /// <summary>
        /// コピー元設定を画面へ反映する
        /// </summary>
        /// <param name="copyInfo"></param>
        private void SetupViewCopySource(CopyInfo copyInfo)
        {
            switch (copyInfo.CopySource)
            {
                case EnumCopySource.AsArgument:
                    rdoSourceHasArgument.Checked = true;
                    rdoSourceThis.Checked = false;
                    rdoSourceProperty.Checked = false;
                    break;
                case EnumCopySource.This:
                    rdoSourceHasArgument.Checked = false;
                    rdoSourceThis.Checked = true;
                    rdoSourceProperty.Checked = false;
                    break;
                case EnumCopySource.PropertyOnly:
                    rdoSourceHasArgument.Checked = false;
                    rdoSourceThis.Checked = true;
                    rdoSourceProperty.Checked = true;
                    break;
                default:
                    rdoSourceHasArgument.Checked = false;
                    rdoSourceThis.Checked = true;
                    rdoSourceProperty.Checked = false;
                    break;
            }

            txtSourceArgumentName.Text = copyInfo.SourceArgumentName;
            chkSouceIfNullCheck.Checked = copyInfo.IsNotNullSource;
        }

        #endregion

        /// <summary>
        /// 設定を保存する
        /// </summary>
        private void SaveCopyInfo()
        {
            CopyInfo copyInfo = CreateCopyInfo();
            CopyConfigFileManager.WriteConfig(PathUtils.GetConfigPath(), copyInfo);
        }

        /// <summary>
        /// DataGridViewへの入力から設定情報を生成する
        /// </summary>
        /// <returns></returns>
        private CopyInfo CreateCopyInfo()
        {
            CopyInfo copyInfo = new CopyInfo();
            copyInfo.IsOutputMethod = rdoAsMethod.Checked;

            SetupVisibility(copyInfo);
            SetupMethodOption(copyInfo);

            copyInfo.MethodName = txtNameInput.Text;

            SetupCopySource(copyInfo);
            SetupCopyDest(copyInfo);
            
            copyInfo.IsEverytimeConfirm = chkEverytimeConfirm.Checked;

            return copyInfo;
        }

        #region 入力内容をCopyInfoへ反映させる処理
        /// <summary>
        /// メソッドアクセス修飾子の設定
        /// </summary>
        /// <param name="copyInfo"></param>
        private void SetupVisibility(CopyInfo copyInfo)
        {
            if (rdoPublic.Checked)
            {
                copyInfo.Visibility = EnumVisibility.Public;
            }
            else if (rdoProtected.Checked)
            {
                copyInfo.Visibility = EnumVisibility.Protected;
            }
            else if (rdoPrivate.Checked)
            {
                copyInfo.Visibility = EnumVisibility.Private;
            }
            else if (rdoInternal.Checked)
            {
                copyInfo.Visibility = EnumVisibility.Internal;
            }
        }

        /// <summary>
        /// メソッド付加設定の設定
        /// </summary>
        /// <param name="copyInfo"></param>
        private void SetupMethodOption(CopyInfo copyInfo)
        {
            if (rdoOptionNone.Checked)
            {
                copyInfo.MethodOption = EnumMethodOption.None;
            }
            else if (rdoOptionStatic.Checked)
            {
                copyInfo.MethodOption = EnumMethodOption.Static;
            }
            else if (rdoOptionVirtual.Checked)
            {
                copyInfo.MethodOption = EnumMethodOption.Virtual;
            }
            else if (rdoOptionOverride.Checked)
            {
                copyInfo.MethodOption = EnumMethodOption.Override;
            }
        }

        /// <summary>
        /// コピー先取得方法の設定
        /// </summary>
        /// <param name="copyInfo"></param>
        private void SetupCopyDest(CopyInfo copyInfo)
        {
            if (rdoDestReturn.Checked)
            {
                copyInfo.CopyDest = EnumCopyDest.Return;
            }
            else if (rdoDestArgument.Checked)
            {
                copyInfo.CopyDest = EnumCopyDest.AsArgument;
            }
            else if (rdoDestThis.Checked)
            {
                copyInfo.CopyDest = EnumCopyDest.This;
            }
            else if (rdoDestProperty.Checked)
            {
                copyInfo.CopyDest = EnumCopyDest.PropertyOnly;
            }
            copyInfo.DestArgumentName = txtDestArgumentName.Text;
            copyInfo.IsNotNullDest = chkDestIfNullCheck.Checked;
        }

        /// <summary>
        /// コピー元取得方法の設定
        /// </summary>
        /// <param name="copyInfo"></param>
        private void SetupCopySource(CopyInfo copyInfo)
        {
            if (rdoSourceHasArgument.Checked)
            {
                copyInfo.CopySource = EnumCopySource.AsArgument;
            }
            else if (rdoSourceThis.Checked)
            {
                copyInfo.CopySource = EnumCopySource.This;
            }
            else if (rdoSourceProperty.Checked)
            {
                copyInfo.CopySource = EnumCopySource.PropertyOnly;
            }
            copyInfo.SourceArgumentName = txtSourceArgumentName.Text;
            copyInfo.IsNotNullSource = chkSouceIfNullCheck.Checked;
        }
        #endregion

        /// <summary>
        /// 設定状態に従って入力を制御する
        /// </summary>
        private void ControlInput()
        {
            if(rdoOptionStatic.Checked)
            {
                //  staticメソッドの場合は自分自身のプロパティを
                //  参照することはできないため、コピー元は必ず引数として取得
                rdoSourceHasArgument.Checked = true;

                if(rdoDestThis.Checked || rdoDestProperty.Checked)
                {
                    rdoDestArgument.Checked = true;
                }
            }

            //  コピー処理のみ出力する場合はコピー先、コピー元を特定する
            //  変数名が必要になるので引数名入力のみとなる
            if(rdoAsCopyOnly.Checked)
            {
                rdoSourceHasArgument.Checked = true;
                rdoDestArgument.Checked = true;
            }
            rdoSourceThis.Enabled = (rdoAsMethod.Checked && !rdoOptionStatic.Checked);
            rdoSourceProperty.Enabled = (rdoAsMethod.Checked && !rdoOptionStatic.Checked);
            
            rdoDestReturn.Enabled = rdoAsMethod.Checked;
            rdoDestThis.Enabled = (rdoAsMethod.Checked && !rdoOptionStatic.Checked);
            rdoDestProperty.Enabled = (rdoAsMethod.Checked && !rdoOptionStatic.Checked);

            //  引数がないときは引数名は入力しない
            txtSourceArgumentName.Enabled = rdoSourceHasArgument.Checked;
            chkSouceIfNullCheck.Enabled = (rdoSourceHasArgument.Checked && rdoAsMethod.Checked);
            txtDestArgumentName.Enabled = rdoDestArgument.Checked;
            chkDestIfNullCheck.Enabled = (rdoDestArgument.Checked && rdoAsMethod.Checked);

            //  privateのときはvirtual,overrideは使えない
            if(rdoPrivate.Checked)
            {
                if(rdoOptionVirtual.Checked || rdoOptionOverride.Checked)
                {
                    rdoOptionNone.Checked = true;
                }
            }
            rdoOptionVirtual.Enabled = !rdoPrivate.Checked;
            rdoOptionOverride.Enabled = !rdoPrivate.Checked;

            //  コピー処理のみ出力する場合はメソッド系の設定は行わない
            grpMethodConfig.Enabled = rdoAsMethod.Checked;
        }

        #endregion

        #region イベント

        /// <summary>
        /// 設定画面ロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyConfig_Load(object sender, EventArgs e)
        {
            Reload();
        }

        /// <summary>
        /// OKボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            SaveCopyInfo();
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        /// <summary>
        /// キャンセルボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// 適用ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApply_Click(object sender, EventArgs e)
        {
            SaveCopyInfo();
            Reload();
        }

        /// <summary>
        /// 再読込ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReload_Click(object sender, EventArgs e)
        {
            Reload();
        }

        /// <summary>
        /// static設定が変更されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdoOptionStatic_CheckedChanged(object sender, EventArgs e)
        {
            ControlInput();
        }

        /// <summary>
        /// アクセス修飾子(private)かどうかが変更されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdoPrivate_CheckedChanged(object sender, EventArgs e)
        {
            ControlInput();
        }

        #region コピー元参照設定変更
        /// <summary>
        /// コピー元This参照が変更されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdoSourceThis_CheckedChanged(object sender, EventArgs e)
        {
            ControlInput();
        }

        /// <summary>
        /// コピー元プロパティ参照設定が変更されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdoSourceProperty_CheckedChanged(object sender, EventArgs e)
        {
            ControlInput();
        }

        /// <summary>
        /// コピー元引数あり設定が変更されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdoSourceHasArgument_CheckedChanged(object sender, EventArgs e)
        {
            ControlInput();
        }

        #endregion

        #region コピー先参照設定変更
        /// <summary>
        /// コピー処理のみ設定が変更されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdoAsCopyOnly_CheckedChanged(object sender, EventArgs e)
        {
            ControlInput();
        }

        /// <summary>
        /// 戻り値としてコピー先を返す設定が変更されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdoTargetReturn_CheckedChanged(object sender, EventArgs e)
        {
            ControlInput();
        }

        /// <summary>
        /// コピー先This参照設定が変更されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdoTargetThis_CheckedChanged(object sender, EventArgs e)
        {
            ControlInput();
        }

        /// <summary>
        /// コピー先プロパティ参照設定が変更されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdoTargetProperty_CheckedChanged(object sender, EventArgs e)
        {
            ControlInput();
        }

        /// <summary>
        /// コピー先引数あり設定が変更されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdoTargetArgument_CheckedChanged(object sender, EventArgs e)
        {
            ControlInput();
        }
        #endregion

 

        #endregion
    }
}
