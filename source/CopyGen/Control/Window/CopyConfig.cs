using System;
using System.IO;
using System.Windows.Forms;
using AddInCommon.Util;
using CodeGeneratorCore.Enum;
using CopyGen.Gen;

namespace CopyGen.Control.Window
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
                    return CopyInfoFileManager.ReadConfig(configPath);
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

            switch (copyInfo.Visibility)
            {
                case EnumVisibility.Public:
                    rdoPublic.Checked = true;
                    break;
                case EnumVisibility.Protected:
                    rdoProtected.Checked = true;
                    break;
                case EnumVisibility.Private:
                    rdoPrivate.Checked = true;
                    break;
                case EnumVisibility.Internal:
                    rdoInternal.Checked = true;
                    break;
            }

            switch (copyInfo.MethodOption)
            {
                case EnumMethodOption.None:
                    rdoOptionNone.Checked = true;
                    break;
                case EnumMethodOption.Static:
                    rdoOptionStatic.Checked = true;
                    break;
                case EnumMethodOption.Virtual:
                    rdoOptionVirtual.Checked = true;
                    break;
                case EnumMethodOption.Override:
                    rdoOptionOverride.Checked = true;
                    break;
            }

            txtNameInput.Text = copyInfo.MethodName;

            txtSourceArgumentName.Text = copyInfo.SourceArgumentName;
            rdoSourceHasArgument.Checked = copyInfo.HasSourceArgument;
            rdoSourceNoArgument.Checked = !copyInfo.HasSourceArgument;

            txtTargetArgumentName.Text = copyInfo.TargetArgumentName;
            rdoTargetReturn.Checked = copyInfo.IsReturn;
            rdoTargetArgument.Checked = !copyInfo.IsReturn;

            chkEverytimeConfirm.Checked = copyInfo.IsEverytimeConfirm;

            ControlInput();
        }

        /// <summary>
        /// 設定を保存する
        /// </summary>
        private void SaveCopyInfo()
        {
            CopyInfo copyInfo = CreateCopyInfo();
            CopyInfoFileManager.WriteConfig(PathUtils.GetConfigPath(), copyInfo);
        }

        /// <summary>
        /// DataGridViewへの入力から設定情報を生成する
        /// </summary>
        /// <returns></returns>
        private CopyInfo CreateCopyInfo()
        {
            CopyInfo copyInfo = new CopyInfo();
            copyInfo.IsOutputMethod = rdoAsMethod.Checked;

            if(rdoPublic.Checked)
            {
                copyInfo.Visibility = EnumVisibility.Public;
            }
            else if(rdoProtected.Checked)
            {
                copyInfo.Visibility = EnumVisibility.Protected;
            }
            else if(rdoPrivate.Checked)
            {
                copyInfo.Visibility = EnumVisibility.Private;
            }
            else if(rdoInternal.Checked)
            {
                copyInfo.Visibility = EnumVisibility.Internal;
            }

            if(rdoOptionNone.Checked)
            {
                copyInfo.MethodOption = EnumMethodOption.None;
            }
            else if(rdoOptionStatic.Checked)
            {
                copyInfo.MethodOption = EnumMethodOption.Static;
            }
            else if(rdoOptionVirtual.Checked)
            {
                copyInfo.MethodOption = EnumMethodOption.Virtual;
            }
            else if(rdoOptionOverride.Checked)
            {
                copyInfo.MethodOption = EnumMethodOption.Override;
            }

            copyInfo.MethodName = txtNameInput.Text;

            copyInfo.HasSourceArgument = rdoSourceHasArgument.Checked;
            copyInfo.SourceArgumentName = txtSourceArgumentName.Text;

            copyInfo.IsReturn = rdoTargetReturn.Checked;
            copyInfo.TargetArgumentName = txtTargetArgumentName.Text;

            copyInfo.IsEverytimeConfirm = chkEverytimeConfirm.Checked;

            return copyInfo;
        }

        /// <summary>
        /// 設定状態に従って入力を制御する
        /// </summary>
        private void ControlInput()
        {
            if(rdoOptionStatic.Checked)
            {
                //  staticメソッドの場合は必ずコピー元は引数として取得
                rdoSourceHasArgument.Checked = true;
            }

            if(rdoAsCopyOnly.Checked)
            {
                rdoSourceHasArgument.Checked = true;
                rdoTargetArgument.Checked = true;
            }
            rdoSourceNoArgument.Enabled = rdoAsMethod.Checked;
            rdoTargetReturn.Enabled = rdoAsMethod.Checked;

            //  引数がないときは引数名は入力しない
            txtSourceArgumentName.Enabled = rdoSourceHasArgument.Checked;
            txtTargetArgumentName.Enabled = rdoTargetArgument.Checked;

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

        #endregion

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
        /// コピー元引数なし設定が変更されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdoSourceNoArgument_CheckedChanged(object sender, EventArgs e)
        {
            ControlInput();
        }

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
    }
}