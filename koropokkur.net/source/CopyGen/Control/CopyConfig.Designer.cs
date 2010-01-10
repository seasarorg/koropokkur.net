namespace CopyGen.Control
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
            this.chkSouceIfNullCheck = new System.Windows.Forms.CheckBox();
            this.rdoSourceProperty = new System.Windows.Forms.RadioButton();
            this.rdoSourceHasArgument = new System.Windows.Forms.RadioButton();
            this.rdoSourceThis = new System.Windows.Forms.RadioButton();
            this.txtSourceArgumentName = new System.Windows.Forms.TextBox();
            this.grpCopyDest = new System.Windows.Forms.GroupBox();
            this.chkDestIfNullCheck = new System.Windows.Forms.CheckBox();
            this.rdoDestProperty = new System.Windows.Forms.RadioButton();
            this.rdoDestThis = new System.Windows.Forms.RadioButton();
            this.rdoDestArgument = new System.Windows.Forms.RadioButton();
            this.rdoDestReturn = new System.Windows.Forms.RadioButton();
            this.txtDestArgumentName = new System.Windows.Forms.TextBox();
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
            this.grpCopyDest.SuspendLayout();
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
            this.rdoPrivate.CheckedChanged += new System.EventHandler(this.rdoPrivate_CheckedChanged);
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
            this.grpCopySource.Controls.Add(this.chkSouceIfNullCheck);
            this.grpCopySource.Controls.Add(this.rdoSourceProperty);
            this.grpCopySource.Controls.Add(this.rdoSourceHasArgument);
            this.grpCopySource.Controls.Add(this.rdoSourceThis);
            this.grpCopySource.Controls.Add(this.txtSourceArgumentName);
            this.grpCopySource.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.grpCopySource.Location = new System.Drawing.Point(12, 258);
            this.grpCopySource.Name = "grpCopySource";
            this.grpCopySource.Size = new System.Drawing.Size(451, 78);
            this.grpCopySource.TabIndex = 200;
            this.grpCopySource.TabStop = false;
            this.grpCopySource.Text = "コピー元(&S)";
            // 
            // chkSouceIfNullCheck
            // 
            this.chkSouceIfNullCheck.AutoSize = true;
            this.chkSouceIfNullCheck.Checked = true;
            this.chkSouceIfNullCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSouceIfNullCheck.Location = new System.Drawing.Point(358, 46);
            this.chkSouceIfNullCheck.Name = "chkSouceIfNullCheck";
            this.chkSouceIfNullCheck.Size = new System.Drawing.Size(80, 19);
            this.chkSouceIfNullCheck.TabIndex = 316;
            this.chkSouceIfNullCheck.Text = "Null禁止";
            this.chkSouceIfNullCheck.UseVisualStyleBackColor = true;
            // 
            // rdoSourceProperty
            // 
            this.rdoSourceProperty.AutoSize = true;
            this.rdoSourceProperty.Location = new System.Drawing.Point(67, 21);
            this.rdoSourceProperty.Name = "rdoSourceProperty";
            this.rdoSourceProperty.Size = new System.Drawing.Size(118, 19);
            this.rdoSourceProperty.TabIndex = 315;
            this.rdoSourceProperty.Text = "プロパティ名のみ";
            this.rdoSourceProperty.UseVisualStyleBackColor = true;
            this.rdoSourceProperty.CheckedChanged += new System.EventHandler(this.rdoSourceProperty_CheckedChanged);
            // 
            // rdoSourceHasArgument
            // 
            this.rdoSourceHasArgument.AutoSize = true;
            this.rdoSourceHasArgument.Location = new System.Drawing.Point(12, 46);
            this.rdoSourceHasArgument.Name = "rdoSourceHasArgument";
            this.rdoSourceHasArgument.Size = new System.Drawing.Size(55, 19);
            this.rdoSourceHasArgument.TabIndex = 314;
            this.rdoSourceHasArgument.Text = "引数";
            this.rdoSourceHasArgument.UseVisualStyleBackColor = true;
            this.rdoSourceHasArgument.CheckedChanged += new System.EventHandler(this.rdoSourceHasArgument_CheckedChanged);
            // 
            // rdoSourceThis
            // 
            this.rdoSourceThis.AutoSize = true;
            this.rdoSourceThis.Checked = true;
            this.rdoSourceThis.Location = new System.Drawing.Point(13, 21);
            this.rdoSourceThis.Name = "rdoSourceThis";
            this.rdoSourceThis.Size = new System.Drawing.Size(48, 19);
            this.rdoSourceThis.TabIndex = 313;
            this.rdoSourceThis.TabStop = true;
            this.rdoSourceThis.Text = "this";
            this.rdoSourceThis.UseVisualStyleBackColor = true;
            this.rdoSourceThis.CheckedChanged += new System.EventHandler(this.rdoSourceThis_CheckedChanged);
            // 
            // txtSourceArgumentName
            // 
            this.txtSourceArgumentName.Location = new System.Drawing.Point(74, 46);
            this.txtSourceArgumentName.Name = "txtSourceArgumentName";
            this.txtSourceArgumentName.Size = new System.Drawing.Size(278, 22);
            this.txtSourceArgumentName.TabIndex = 201;
            this.txtSourceArgumentName.Text = "source";
            // 
            // grpCopyDest
            // 
            this.grpCopyDest.Controls.Add(this.chkDestIfNullCheck);
            this.grpCopyDest.Controls.Add(this.rdoDestProperty);
            this.grpCopyDest.Controls.Add(this.rdoDestThis);
            this.grpCopyDest.Controls.Add(this.rdoDestArgument);
            this.grpCopyDest.Controls.Add(this.rdoDestReturn);
            this.grpCopyDest.Controls.Add(this.txtDestArgumentName);
            this.grpCopyDest.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.grpCopyDest.Location = new System.Drawing.Point(11, 342);
            this.grpCopyDest.Name = "grpCopyDest";
            this.grpCopyDest.Size = new System.Drawing.Size(451, 82);
            this.grpCopyDest.TabIndex = 300;
            this.grpCopyDest.TabStop = false;
            this.grpCopyDest.Text = "コピー先(&T)";
            // 
            // chkDestIfNullCheck
            // 
            this.chkDestIfNullCheck.AutoSize = true;
            this.chkDestIfNullCheck.Checked = true;
            this.chkDestIfNullCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDestIfNullCheck.Location = new System.Drawing.Point(359, 46);
            this.chkDestIfNullCheck.Name = "chkDestIfNullCheck";
            this.chkDestIfNullCheck.Size = new System.Drawing.Size(80, 19);
            this.chkDestIfNullCheck.TabIndex = 317;
            this.chkDestIfNullCheck.Text = "Null禁止";
            this.chkDestIfNullCheck.UseVisualStyleBackColor = true;
            // 
            // rdoDestProperty
            // 
            this.rdoDestProperty.AutoSize = true;
            this.rdoDestProperty.Location = new System.Drawing.Point(148, 21);
            this.rdoDestProperty.Name = "rdoDestProperty";
            this.rdoDestProperty.Size = new System.Drawing.Size(118, 19);
            this.rdoDestProperty.TabIndex = 319;
            this.rdoDestProperty.Text = "プロパティ名のみ";
            this.rdoDestProperty.UseVisualStyleBackColor = true;
            this.rdoDestProperty.CheckedChanged += new System.EventHandler(this.rdoTargetProperty_CheckedChanged);
            // 
            // rdoDestThis
            // 
            this.rdoDestThis.AutoSize = true;
            this.rdoDestThis.Location = new System.Drawing.Point(88, 21);
            this.rdoDestThis.Name = "rdoDestThis";
            this.rdoDestThis.Size = new System.Drawing.Size(48, 19);
            this.rdoDestThis.TabIndex = 318;
            this.rdoDestThis.Text = "this";
            this.rdoDestThis.UseVisualStyleBackColor = true;
            this.rdoDestThis.CheckedChanged += new System.EventHandler(this.rdoTargetThis_CheckedChanged);
            // 
            // rdoDestArgument
            // 
            this.rdoDestArgument.AutoSize = true;
            this.rdoDestArgument.Location = new System.Drawing.Point(14, 46);
            this.rdoDestArgument.Name = "rdoDestArgument";
            this.rdoDestArgument.Size = new System.Drawing.Size(55, 19);
            this.rdoDestArgument.TabIndex = 317;
            this.rdoDestArgument.Text = "引数";
            this.rdoDestArgument.UseVisualStyleBackColor = true;
            this.rdoDestArgument.CheckedChanged += new System.EventHandler(this.rdoTargetArgument_CheckedChanged);
            // 
            // rdoDestReturn
            // 
            this.rdoDestReturn.AutoSize = true;
            this.rdoDestReturn.Checked = true;
            this.rdoDestReturn.Location = new System.Drawing.Point(13, 21);
            this.rdoDestReturn.Name = "rdoDestReturn";
            this.rdoDestReturn.Size = new System.Drawing.Size(64, 19);
            this.rdoDestReturn.TabIndex = 316;
            this.rdoDestReturn.TabStop = true;
            this.rdoDestReturn.Text = "戻り値";
            this.rdoDestReturn.UseVisualStyleBackColor = true;
            this.rdoDestReturn.CheckedChanged += new System.EventHandler(this.rdoTargetReturn_CheckedChanged);
            // 
            // txtDestArgumentName
            // 
            this.txtDestArgumentName.Location = new System.Drawing.Point(75, 45);
            this.txtDestArgumentName.Name = "txtDestArgumentName";
            this.txtDestArgumentName.Size = new System.Drawing.Size(278, 22);
            this.txtDestArgumentName.TabIndex = 315;
            this.txtDestArgumentName.Text = "target";
            // 
            // chkEverytimeConfirm
            // 
            this.chkEverytimeConfirm.AutoSize = true;
            this.chkEverytimeConfirm.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkEverytimeConfirm.Location = new System.Drawing.Point(25, 430);
            this.chkEverytimeConfirm.Name = "chkEverytimeConfirm";
            this.chkEverytimeConfirm.Size = new System.Drawing.Size(104, 19);
            this.chkEverytimeConfirm.TabIndex = 401;
            this.chkEverytimeConfirm.Text = "毎回確認(&E)";
            this.chkEverytimeConfirm.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnCancel.Location = new System.Drawing.Point(122, 455);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(113, 32);
            this.btnCancel.TabIndex = 413;
            this.btnCancel.Text = "キャンセル(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnOK.Location = new System.Drawing.Point(11, 455);
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
            this.btnReload.Location = new System.Drawing.Point(351, 455);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(111, 32);
            this.btnReload.TabIndex = 415;
            this.btnReload.Text = "再読込(&L)";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // btnApply
            // 
            this.btnApply.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnApply.Location = new System.Drawing.Point(241, 455);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(104, 32);
            this.btnApply.TabIndex = 416;
            this.btnApply.Text = "適用(&W)";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // CopyConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 497);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnReload);
            this.Controls.Add(this.grpOutputRange);
            this.Controls.Add(this.grpMethodConfig);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.chkEverytimeConfirm);
            this.Controls.Add(this.grpCopyDest);
            this.Controls.Add(this.grpCopySource);
            this.Name = "CopyConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "コピーメソッド設定";
            this.Load += new System.EventHandler(this.CopyConfig_Load);
            this.grpVisibility.ResumeLayout(false);
            this.grpVisibility.PerformLayout();
            this.ｇrpOption.ResumeLayout(false);
            this.ｇrpOption.PerformLayout();
            this.grpCopySource.ResumeLayout(false);
            this.grpCopySource.PerformLayout();
            this.grpCopyDest.ResumeLayout(false);
            this.grpCopyDest.PerformLayout();
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
        private System.Windows.Forms.GroupBox grpCopyDest;
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
        private System.Windows.Forms.RadioButton rdoSourceThis;
        private System.Windows.Forms.RadioButton rdoDestArgument;
        private System.Windows.Forms.RadioButton rdoDestReturn;
        private System.Windows.Forms.TextBox txtDestArgumentName;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.RadioButton rdoSourceProperty;
        private System.Windows.Forms.RadioButton rdoDestProperty;
        private System.Windows.Forms.RadioButton rdoDestThis;
        private System.Windows.Forms.CheckBox chkSouceIfNullCheck;
        private System.Windows.Forms.CheckBox chkDestIfNullCheck;
    }
}