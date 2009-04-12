namespace CopyGen.Control.Window
{
    partial class CopyConfig
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpVisibility = new System.Windows.Forms.GroupBox();
            this.rdoInternal = new System.Windows.Forms.RadioButton();
            this.rdoPrivate = new System.Windows.Forms.RadioButton();
            this.rdoProtected = new System.Windows.Forms.RadioButton();
            this.rdoPublic = new System.Windows.Forms.RadioButton();
            this.ｇrpOption = new System.Windows.Forms.GroupBox();
            this.rdoOptionOverride = new System.Windows.Forms.RadioButton();
            this.rdoOptionVirtual = new System.Windows.Forms.RadioButton();
            this.rdoOptionStatic = new System.Windows.Forms.RadioButton();
            this.rdoOptionNone = new System.Windows.Forms.RadioButton();
            this.grpCopySource = new System.Windows.Forms.GroupBox();
            this.rdoSourceHasArgument = new System.Windows.Forms.RadioButton();
            this.rdoSourceNoArgument = new System.Windows.Forms.RadioButton();
            this.txtSourceArgumentName = new System.Windows.Forms.TextBox();
            this.grpCopyTarget = new System.Windows.Forms.GroupBox();
            this.rdoTargetArgument = new System.Windows.Forms.RadioButton();
            this.rdoTargetReturn = new System.Windows.Forms.RadioButton();
            this.txtTargetArgumentName = new System.Windows.Forms.TextBox();
            this.chkEverytimeConfirm = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.txtNameInput = new System.Windows.Forms.TextBox();
            this.grpMethodConfig = new System.Windows.Forms.GroupBox();
            this.grpOutputRange = new System.Windows.Forms.GroupBox();
            this.rdoAsCopyOnly = new System.Windows.Forms.RadioButton();
            this.rdoAsMethod = new System.Windows.Forms.RadioButton();
            this.btnReload = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.grpVisibility.SuspendLayout();
            this.ｇrpOption.SuspendLayout();
            this.grpCopySource.SuspendLayout();
            this.grpCopyTarget.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.grpMethodConfig.SuspendLayout();
            this.grpOutputRange.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpVisibility
            // 
            this.grpVisibility.Controls.Add(this.rdoInternal);
            this.grpVisibility.Controls.Add(this.rdoPrivate);
            this.grpVisibility.Controls.Add(this.rdoProtected);
            this.grpVisibility.Controls.Add(this.rdoPublic);
            this.grpVisibility.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.grpVisibility.Location = new System.Drawing.Point(13, 21);
            this.grpVisibility.Name = "grpVisibility";
            this.grpVisibility.Size = new System.Drawing.Size(425, 47);
            this.grpVisibility.TabIndex = 110;
            this.grpVisibility.TabStop = false;
            this.grpVisibility.Text = "アクセス修飾子(&A)";
            // 
            // rdoInternal
            // 
            this.rdoInternal.AutoSize = true;
            this.rdoInternal.Location = new System.Drawing.Point(239, 19);
            this.rdoInternal.Name = "rdoInternal";
            this.rdoInternal.Size = new System.Drawing.Size(72, 19);
            this.rdoInternal.TabIndex = 114;
            this.rdoInternal.Text = "internal";
            this.rdoInternal.UseVisualStyleBackColor = true;
            // 
            // rdoPrivate
            // 
            this.rdoPrivate.AutoSize = true;
            this.rdoPrivate.Location = new System.Drawing.Point(166, 19);
            this.rdoPrivate.Name = "rdoPrivate";
            this.rdoPrivate.Size = new System.Drawing.Size(67, 19);
            this.rdoPrivate.TabIndex = 113;
            this.rdoPrivate.Text = "private";
            this.rdoPrivate.UseVisualStyleBackColor = true;
            // 
            // rdoProtected
            // 
            this.rdoProtected.AutoSize = true;
            this.rdoProtected.Location = new System.Drawing.Point(74, 19);
            this.rdoProtected.Name = "rdoProtected";
            this.rdoProtected.Size = new System.Drawing.Size(86, 19);
            this.rdoProtected.TabIndex = 112;
            this.rdoProtected.Text = "protected";
            this.rdoProtected.UseVisualStyleBackColor = true;
            // 
            // rdoPublic
            // 
            this.rdoPublic.AutoSize = true;
            this.rdoPublic.Checked = true;
            this.rdoPublic.Location = new System.Drawing.Point(7, 19);
            this.rdoPublic.Name = "rdoPublic";
            this.rdoPublic.Size = new System.Drawing.Size(61, 19);
            this.rdoPublic.TabIndex = 111;
            this.rdoPublic.TabStop = true;
            this.rdoPublic.Text = "public";
            this.rdoPublic.UseVisualStyleBackColor = true;
            // 
            // ｇrpOption
            // 
            this.ｇrpOption.Controls.Add(this.rdoOptionOverride);
            this.ｇrpOption.Controls.Add(this.rdoOptionVirtual);
            this.ｇrpOption.Controls.Add(this.rdoOptionStatic);
            this.ｇrpOption.Controls.Add(this.rdoOptionNone);
            this.ｇrpOption.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ｇrpOption.Location = new System.Drawing.Point(13, 74);
            this.ｇrpOption.Name = "ｇrpOption";
            this.ｇrpOption.Size = new System.Drawing.Size(425, 45);
            this.ｇrpOption.TabIndex = 120;
            this.ｇrpOption.TabStop = false;
            this.ｇrpOption.Text = "メソッド追加設定(&M)";
            // 
            // rdoOptionOverride
            // 
            this.rdoOptionOverride.AutoSize = true;
            this.rdoOptionOverride.Location = new System.Drawing.Point(190, 21);
            this.rdoOptionOverride.Name = "rdoOptionOverride";
            this.rdoOptionOverride.Size = new System.Drawing.Size(76, 19);
            this.rdoOptionOverride.TabIndex = 124;
            this.rdoOptionOverride.Text = "override";
            this.rdoOptionOverride.UseVisualStyleBackColor = true;
            // 
            // rdoOptionVirtual
            // 
            this.rdoOptionVirtual.AutoSize = true;
            this.rdoOptionVirtual.Location = new System.Drawing.Point(121, 21);
            this.rdoOptionVirtual.Name = "rdoOptionVirtual";
            this.rdoOptionVirtual.Size = new System.Drawing.Size(63, 19);
            this.rdoOptionVirtual.TabIndex = 123;
            this.rdoOptionVirtual.Text = "virtual";
            this.rdoOptionVirtual.UseVisualStyleBackColor = true;
            // 
            // rdoOptionStatic
            // 
            this.rdoOptionStatic.AutoSize = true;
            this.rdoOptionStatic.Location = new System.Drawing.Point(55, 21);
            this.rdoOptionStatic.Name = "rdoOptionStatic";
            this.rdoOptionStatic.Size = new System.Drawing.Size(60, 19);
            this.rdoOptionStatic.TabIndex = 122;
            this.rdoOptionStatic.Text = "static";
            this.rdoOptionStatic.UseVisualStyleBackColor = true;
            this.rdoOptionStatic.CheckedChanged += new System.EventHandler(this.rdoOptionStatic_CheckedChanged);
            // 
            // rdoOptionNone
            // 
            this.rdoOptionNone.AutoSize = true;
            this.rdoOptionNone.Checked = true;
            this.rdoOptionNone.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.rdoOptionNone.Location = new System.Drawing.Point(7, 23);
            this.rdoOptionNone.Name = "rdoOptionNone";
            this.rdoOptionNone.Size = new System.Drawing.Size(42, 16);
            this.rdoOptionNone.TabIndex = 121;
            this.rdoOptionNone.TabStop = true;
            this.rdoOptionNone.Text = "なし";
            this.rdoOptionNone.UseVisualStyleBackColor = true;
            // 
            // grpCopySource
            // 
            this.grpCopySource.Controls.Add(this.rdoSourceHasArgument);
            this.grpCopySource.Controls.Add(this.rdoSourceNoArgument);
            this.grpCopySource.Controls.Add(this.txtSourceArgumentName);
            this.grpCopySource.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.grpCopySource.Location = new System.Drawing.Point(12, 258);
            this.grpCopySource.Name = "grpCopySource";
            this.grpCopySource.Size = new System.Drawing.Size(451, 56);
            this.grpCopySource.TabIndex = 200;
            this.grpCopySource.TabStop = false;
            this.grpCopySource.Text = "コピー元(&S)";
            // 
            // rdoSourceHasArgument
            // 
            this.rdoSourceHasArgument.AutoSize = true;
            this.rdoSourceHasArgument.Location = new System.Drawing.Point(89, 21);
            this.rdoSourceHasArgument.Name = "rdoSourceHasArgument";
            this.rdoSourceHasArgument.Size = new System.Drawing.Size(55, 19);
            this.rdoSourceHasArgument.TabIndex = 314;
            this.rdoSourceHasArgument.Text = "引数";
            this.rdoSourceHasArgument.UseVisualStyleBackColor = true;
            // 
            // rdoSourceNoArgument
            // 
            this.rdoSourceNoArgument.AutoSize = true;
            this.rdoSourceNoArgument.Checked = true;
            this.rdoSourceNoArgument.Location = new System.Drawing.Point(13, 21);
            this.rdoSourceNoArgument.Name = "rdoSourceNoArgument";
            this.rdoSourceNoArgument.Size = new System.Drawing.Size(79, 19);
            this.rdoSourceNoArgument.TabIndex = 313;
            this.rdoSourceNoArgument.TabStop = true;
            this.rdoSourceNoArgument.Text = "引数なし";
            this.rdoSourceNoArgument.UseVisualStyleBackColor = true;
            this.rdoSourceNoArgument.CheckedChanged += new System.EventHandler(this.rdoSourceNoArgument_CheckedChanged);
            // 
            // txtSourceArgumentName
            // 
            this.txtSourceArgumentName.Location = new System.Drawing.Point(162, 19);
            this.txtSourceArgumentName.Name = "txtSourceArgumentName";
            this.txtSourceArgumentName.Size = new System.Drawing.Size(263, 22);
            this.txtSourceArgumentName.TabIndex = 201;
            this.txtSourceArgumentName.Text = "source";
            // 
            // grpCopyTarget
            // 
            this.grpCopyTarget.Controls.Add(this.rdoTargetArgument);
            this.grpCopyTarget.Controls.Add(this.rdoTargetReturn);
            this.grpCopyTarget.Controls.Add(this.txtTargetArgumentName);
            this.grpCopyTarget.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.grpCopyTarget.Location = new System.Drawing.Point(12, 320);
            this.grpCopyTarget.Name = "grpCopyTarget";
            this.grpCopyTarget.Size = new System.Drawing.Size(451, 57);
            this.grpCopyTarget.TabIndex = 300;
            this.grpCopyTarget.TabStop = false;
            this.grpCopyTarget.Text = "コピー先(&T)";
            // 
            // rdoTargetArgument
            // 
            this.rdoTargetArgument.AutoSize = true;
            this.rdoTargetArgument.Location = new System.Drawing.Point(89, 21);
            this.rdoTargetArgument.Name = "rdoTargetArgument";
            this.rdoTargetArgument.Size = new System.Drawing.Size(55, 19);
            this.rdoTargetArgument.TabIndex = 317;
            this.rdoTargetArgument.Text = "引数";
            this.rdoTargetArgument.UseVisualStyleBackColor = true;
            // 
            // rdoTargetReturn
            // 
            this.rdoTargetReturn.AutoSize = true;
            this.rdoTargetReturn.Checked = true;
            this.rdoTargetReturn.Location = new System.Drawing.Point(13, 21);
            this.rdoTargetReturn.Name = "rdoTargetReturn";
            this.rdoTargetReturn.Size = new System.Drawing.Size(64, 19);
            this.rdoTargetReturn.TabIndex = 316;
            this.rdoTargetReturn.TabStop = true;
            this.rdoTargetReturn.Text = "戻り値";
            this.rdoTargetReturn.UseVisualStyleBackColor = true;
            this.rdoTargetReturn.CheckedChanged += new System.EventHandler(this.rdoTargetReturn_CheckedChanged);
            // 
            // txtTargetArgumentName
            // 
            this.txtTargetArgumentName.Location = new System.Drawing.Point(162, 19);
            this.txtTargetArgumentName.Name = "txtTargetArgumentName";
            this.txtTargetArgumentName.Size = new System.Drawing.Size(263, 22);
            this.txtTargetArgumentName.TabIndex = 315;
            this.txtTargetArgumentName.Text = "target";
            // 
            // chkEverytimeConfirm
            // 
            this.chkEverytimeConfirm.AutoSize = true;
            this.chkEverytimeConfirm.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkEverytimeConfirm.Location = new System.Drawing.Point(25, 383);
            this.chkEverytimeConfirm.Name = "chkEverytimeConfirm";
            this.chkEverytimeConfirm.Size = new System.Drawing.Size(104, 19);
            this.chkEverytimeConfirm.TabIndex = 401;
            this.chkEverytimeConfirm.Text = "毎回確認(&E)";
            this.chkEverytimeConfirm.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnCancel.Location = new System.Drawing.Point(123, 406);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(105, 32);
            this.btnCancel.TabIndex = 413;
            this.btnCancel.Text = "キャンセル(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnOK.Location = new System.Drawing.Point(12, 406);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(105, 32);
            this.btnOK.TabIndex = 412;
            this.btnOK.Text = "OK(&O)";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.txtNameInput);
            this.groupBox8.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBox8.Location = new System.Drawing.Point(13, 125);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(425, 55);
            this.groupBox8.TabIndex = 130;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "メソッド名(&N)";
            // 
            // txtNameInput
            // 
            this.txtNameInput.Location = new System.Drawing.Point(9, 21);
            this.txtNameInput.Name = "txtNameInput";
            this.txtNameInput.Size = new System.Drawing.Size(403, 22);
            this.txtNameInput.TabIndex = 131;
            this.txtNameInput.Text = "Copy";
            // 
            // grpMethodConfig
            // 
            this.grpMethodConfig.Controls.Add(this.groupBox8);
            this.grpMethodConfig.Controls.Add(this.ｇrpOption);
            this.grpMethodConfig.Controls.Add(this.grpVisibility);
            this.grpMethodConfig.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.grpMethodConfig.Location = new System.Drawing.Point(12, 62);
            this.grpMethodConfig.Name = "grpMethodConfig";
            this.grpMethodConfig.Size = new System.Drawing.Size(450, 190);
            this.grpMethodConfig.TabIndex = 100;
            this.grpMethodConfig.TabStop = false;
            this.grpMethodConfig.Text = "メソッド設定";
            // 
            // grpOutputRange
            // 
            this.grpOutputRange.Controls.Add(this.rdoAsCopyOnly);
            this.grpOutputRange.Controls.Add(this.rdoAsMethod);
            this.grpOutputRange.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.grpOutputRange.Location = new System.Drawing.Point(12, 4);
            this.grpOutputRange.Name = "grpOutputRange";
            this.grpOutputRange.Size = new System.Drawing.Size(449, 52);
            this.grpOutputRange.TabIndex = 414;
            this.grpOutputRange.TabStop = false;
            this.grpOutputRange.Text = "出力範囲(&R)";
            // 
            // rdoAsCopyOnly
            // 
            this.rdoAsCopyOnly.AutoSize = true;
            this.rdoAsCopyOnly.Location = new System.Drawing.Point(147, 21);
            this.rdoAsCopyOnly.Name = "rdoAsCopyOnly";
            this.rdoAsCopyOnly.Size = new System.Drawing.Size(143, 19);
            this.rdoAsCopyOnly.TabIndex = 1;
            this.rdoAsCopyOnly.TabStop = true;
            this.rdoAsCopyOnly.Text = "コピー処理のみ出力";
            this.rdoAsCopyOnly.UseVisualStyleBackColor = true;
            this.rdoAsCopyOnly.CheckedChanged += new System.EventHandler(this.rdoAsCopyOnly_CheckedChanged);
            // 
            // rdoAsMethod
            // 
            this.rdoAsMethod.AutoSize = true;
            this.rdoAsMethod.Checked = true;
            this.rdoAsMethod.Location = new System.Drawing.Point(13, 21);
            this.rdoAsMethod.Name = "rdoAsMethod";
            this.rdoAsMethod.Size = new System.Drawing.Size(128, 19);
            this.rdoAsMethod.TabIndex = 0;
            this.rdoAsMethod.TabStop = true;
            this.rdoAsMethod.Text = "メソッドとして出力";
            this.rdoAsMethod.UseVisualStyleBackColor = true;
            // 
            // btnReload
            // 
            this.btnReload.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnReload.Location = new System.Drawing.Point(342, 406);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(119, 32);
            this.btnReload.TabIndex = 415;
            this.btnReload.Text = "再読込(&L)";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // btnApply
            // 
            this.btnApply.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnApply.Location = new System.Drawing.Point(234, 406);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(102, 32);
            this.btnApply.TabIndex = 416;
            this.btnApply.Text = "適用(&W)";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // CopyConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 447);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnReload);
            this.Controls.Add(this.grpOutputRange);
            this.Controls.Add(this.grpMethodConfig);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.chkEverytimeConfirm);
            this.Controls.Add(this.grpCopyTarget);
            this.Controls.Add(this.grpCopySource);
            this.Name = "CopyConfig";
            this.Text = "コピーメソッド設定";
            this.Load += new System.EventHandler(this.CopyConfig_Load);
            this.grpVisibility.ResumeLayout(false);
            this.grpVisibility.PerformLayout();
            this.ｇrpOption.ResumeLayout(false);
            this.ｇrpOption.PerformLayout();
            this.grpCopySource.ResumeLayout(false);
            this.grpCopySource.PerformLayout();
            this.grpCopyTarget.ResumeLayout(false);
            this.grpCopyTarget.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.grpMethodConfig.ResumeLayout(false);
            this.grpOutputRange.ResumeLayout(false);
            this.grpOutputRange.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpVisibility;
        private System.Windows.Forms.RadioButton rdoInternal;
        private System.Windows.Forms.RadioButton rdoPrivate;
        private System.Windows.Forms.RadioButton rdoProtected;
        private System.Windows.Forms.RadioButton rdoPublic;
        private System.Windows.Forms.GroupBox ｇrpOption;
        private System.Windows.Forms.RadioButton rdoOptionStatic;
        private System.Windows.Forms.RadioButton rdoOptionNone;
        private System.Windows.Forms.RadioButton rdoOptionOverride;
        private System.Windows.Forms.GroupBox grpCopySource;
        private System.Windows.Forms.TextBox txtSourceArgumentName;
        private System.Windows.Forms.GroupBox grpCopyTarget;
        private System.Windows.Forms.CheckBox chkEverytimeConfirm;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.TextBox txtNameInput;
        private System.Windows.Forms.RadioButton rdoOptionVirtual;
        private System.Windows.Forms.GroupBox grpMethodConfig;
        private System.Windows.Forms.GroupBox grpOutputRange;
        private System.Windows.Forms.RadioButton rdoAsCopyOnly;
        private System.Windows.Forms.RadioButton rdoAsMethod;
        private System.Windows.Forms.RadioButton rdoSourceHasArgument;
        private System.Windows.Forms.RadioButton rdoSourceNoArgument;
        private System.Windows.Forms.RadioButton rdoTargetArgument;
        private System.Windows.Forms.RadioButton rdoTargetReturn;
        private System.Windows.Forms.TextBox txtTargetArgumentName;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Button btnApply;
    }
}