namespace VSArrange.Control.Window
{
    partial class ConfigForm
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
            this.lblNoticeFilterInput = new System.Windows.Forms.Label();
            this.txtNotice = new System.Windows.Forms.TextBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.filterFolder = new VSArrange.Control.Window.FilterList();
            this.filterFile = new VSArrange.Control.Window.FilterList();
            this.SuspendLayout();
            // 
            // lblNoticeFilterInput
            // 
            this.lblNoticeFilterInput.AutoSize = true;
            this.lblNoticeFilterInput.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNoticeFilterInput.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblNoticeFilterInput.Location = new System.Drawing.Point(12, 9);
            this.lblNoticeFilterInput.Name = "lblNoticeFilterInput";
            this.lblNoticeFilterInput.Size = new System.Drawing.Size(168, 17);
            this.lblNoticeFilterInput.TabIndex = 1;
            this.lblNoticeFilterInput.Text = "プロジェクト追加フィルター";
            this.lblNoticeFilterInput.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtNotice
            // 
            this.txtNotice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNotice.Location = new System.Drawing.Point(12, 29);
            this.txtNotice.Multiline = true;
            this.txtNotice.Name = "txtNotice";
            this.txtNotice.ReadOnly = true;
            this.txtNotice.Size = new System.Drawing.Size(548, 35);
            this.txtNotice.TabIndex = 4;
            this.txtNotice.TabStop = false;
            this.txtNotice.Text = "プロジェクトに追加しないファイル名、フォルダ名を設定して下さい。\r\n（正規表現も使えます。大文字小文字は区別しません）";
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnExit.Location = new System.Drawing.Point(424, 582);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(136, 29);
            this.btnExit.TabIndex = 22;
            this.btnExit.Text = "保存せずに終了(&C)";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSave.Location = new System.Drawing.Point(280, 582);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(135, 29);
            this.btnSave.TabIndex = 23;
            this.btnSave.Text = "保存して終了(&S)";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // filterFolder
            // 
            this.filterFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.filterFolder.FilterName = "フォルダ";
            this.filterFolder.Location = new System.Drawing.Point(12, 326);
            this.filterFolder.Name = "filterFolder";
            this.filterFolder.Size = new System.Drawing.Size(552, 250);
            this.filterFolder.TabIndex = 25;
            // 
            // filterFile
            // 
            this.filterFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.filterFile.FilterName = "ファイル";
            this.filterFile.Location = new System.Drawing.Point(12, 70);
            this.filterFile.Name = "filterFile";
            this.filterFile.Size = new System.Drawing.Size(552, 250);
            this.filterFile.TabIndex = 24;
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(573, 617);
            this.ControlBox = false;
            this.Controls.Add(this.filterFolder);
            this.Controls.Add(this.filterFile);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.txtNotice);
            this.Controls.Add(this.lblNoticeFilterInput);
            this.Name = "ConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VSArrange設定";
            this.Load += new System.EventHandler(this.ConfigForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNoticeFilterInput;
        private System.Windows.Forms.TextBox txtNotice;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSave;
        private FilterList filterFile;
        private FilterList filterFolder;
    }
}