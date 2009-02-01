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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgFileFilters = new System.Windows.Forms.DataGridView();
            this.IsEnableFileFilter = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.FileFilterName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileFilterString = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonRemoveFileFilter = new System.Windows.Forms.DataGridViewButtonColumn();
            this.lblNoticeFilterInput = new System.Windows.Forms.Label();
            this.txtNotice = new System.Windows.Forms.TextBox();
            this.grpFile = new System.Windows.Forms.GroupBox();
            this.grpFolder = new System.Windows.Forms.GroupBox();
            this.dgFolderFilters = new System.Windows.Forms.DataGridView();
            this.IsEnableFolderFilter = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.FolderFilterName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FolderFilterString = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonRemoveFolderFilter = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgFileFilters)).BeginInit();
            this.grpFile.SuspendLayout();
            this.grpFolder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgFolderFilters)).BeginInit();
            this.SuspendLayout();
            // 
            // dgFileFilters
            // 
            this.dgFileFilters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgFileFilters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgFileFilters.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IsEnableFileFilter,
            this.FileFilterName,
            this.FileFilterString,
            this.buttonRemoveFileFilter});
            this.dgFileFilters.Location = new System.Drawing.Point(6, 18);
            this.dgFileFilters.Name = "dgFileFilters";
            this.dgFileFilters.RowHeadersVisible = false;
            this.dgFileFilters.RowTemplate.Height = 21;
            this.dgFileFilters.Size = new System.Drawing.Size(531, 187);
            this.dgFileFilters.TabIndex = 0;
            this.dgFileFilters.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgFileFilters_CellContentClick);
            // 
            // IsEnableFileFilter
            // 
            this.IsEnableFileFilter.HeaderText = "有効";
            this.IsEnableFileFilter.Name = "IsEnableFileFilter";
            this.IsEnableFileFilter.Width = 38;
            // 
            // FileFilterName
            // 
            this.FileFilterName.HeaderText = "フィルター名";
            this.FileFilterName.Name = "FileFilterName";
            this.FileFilterName.Width = 200;
            // 
            // FileFilterString
            // 
            this.FileFilterString.HeaderText = "フィルター";
            this.FileFilterString.Name = "FileFilterString";
            this.FileFilterString.Width = 255;
            // 
            // buttonRemoveFileFilter
            // 
            this.buttonRemoveFileFilter.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(1);
            this.buttonRemoveFileFilter.DefaultCellStyle = dataGridViewCellStyle1;
            this.buttonRemoveFileFilter.HeaderText = "削除";
            this.buttonRemoveFileFilter.Name = "buttonRemoveFileFilter";
            this.buttonRemoveFileFilter.Text = "×";
            this.buttonRemoveFileFilter.UseColumnTextForButtonValue = true;
            this.buttonRemoveFileFilter.Width = 35;
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
            this.txtNotice.Size = new System.Drawing.Size(545, 42);
            this.txtNotice.TabIndex = 4;
            this.txtNotice.TabStop = false;
            this.txtNotice.Text = "プロジェクトに追加しないファイル名、フォルダ名を\r\n正規表現で設定して下さい。\r\n（大文字小文字は区別しません）";
            // 
            // grpFile
            // 
            this.grpFile.Controls.Add(this.dgFileFilters);
            this.grpFile.Location = new System.Drawing.Point(12, 77);
            this.grpFile.Name = "grpFile";
            this.grpFile.Size = new System.Drawing.Size(543, 212);
            this.grpFile.TabIndex = 10;
            this.grpFile.TabStop = false;
            this.grpFile.Text = "ファイル";
            // 
            // grpFolder
            // 
            this.grpFolder.Controls.Add(this.dgFolderFilters);
            this.grpFolder.Location = new System.Drawing.Point(13, 304);
            this.grpFolder.Name = "grpFolder";
            this.grpFolder.Size = new System.Drawing.Size(542, 212);
            this.grpFolder.TabIndex = 11;
            this.grpFolder.TabStop = false;
            this.grpFolder.Text = "フォルダ";
            // 
            // dgFolderFilters
            // 
            this.dgFolderFilters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgFolderFilters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgFolderFilters.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IsEnableFolderFilter,
            this.FolderFilterName,
            this.FolderFilterString,
            this.buttonRemoveFolderFilter});
            this.dgFolderFilters.Location = new System.Drawing.Point(6, 18);
            this.dgFolderFilters.Name = "dgFolderFilters";
            this.dgFolderFilters.RowHeadersVisible = false;
            this.dgFolderFilters.RowTemplate.Height = 21;
            this.dgFolderFilters.Size = new System.Drawing.Size(530, 187);
            this.dgFolderFilters.TabIndex = 0;
            this.dgFolderFilters.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgFolderFilters_CellContentClick);
            // 
            // IsEnableFolderFilter
            // 
            this.IsEnableFolderFilter.HeaderText = "有効";
            this.IsEnableFolderFilter.Name = "IsEnableFolderFilter";
            this.IsEnableFolderFilter.Width = 38;
            // 
            // FolderFilterName
            // 
            this.FolderFilterName.HeaderText = "フィルター名";
            this.FolderFilterName.Name = "FolderFilterName";
            this.FolderFilterName.Width = 200;
            // 
            // FolderFilterString
            // 
            this.FolderFilterString.HeaderText = "フィルター";
            this.FolderFilterString.Name = "FolderFilterString";
            this.FolderFilterString.Width = 254;
            // 
            // buttonRemoveFolderFilter
            // 
            this.buttonRemoveFolderFilter.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(1);
            this.buttonRemoveFolderFilter.DefaultCellStyle = dataGridViewCellStyle2;
            this.buttonRemoveFolderFilter.HeaderText = "削除";
            this.buttonRemoveFolderFilter.Name = "buttonRemoveFolderFilter";
            this.buttonRemoveFolderFilter.Text = "×";
            this.buttonRemoveFolderFilter.UseColumnTextForButtonValue = true;
            this.buttonRemoveFolderFilter.Width = 35;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnExit.Location = new System.Drawing.Point(419, 522);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(136, 29);
            this.btnExit.TabIndex = 22;
            this.btnExit.Text = "保存せずに終了(&C)";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSave.Location = new System.Drawing.Point(278, 522);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(135, 29);
            this.btnSave.TabIndex = 23;
            this.btnSave.Text = "保存して終了(&S)";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ConfigForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(570, 559);
            this.ControlBox = false;
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.grpFolder);
            this.Controls.Add(this.grpFile);
            this.Controls.Add(this.txtNotice);
            this.Controls.Add(this.lblNoticeFilterInput);
            this.Name = "ConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VSArrange設定";
            this.Load += new System.EventHandler(this.ConfigForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgFileFilters)).EndInit();
            this.grpFile.ResumeLayout(false);
            this.grpFolder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgFolderFilters)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgFileFilters;
        private System.Windows.Forms.Label lblNoticeFilterInput;
        private System.Windows.Forms.TextBox txtNotice;
        private System.Windows.Forms.GroupBox grpFile;
        private System.Windows.Forms.GroupBox grpFolder;
        private System.Windows.Forms.DataGridView dgFolderFilters;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsEnableFileFilter;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileFilterName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileFilterString;
        private System.Windows.Forms.DataGridViewButtonColumn buttonRemoveFileFilter;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsEnableFolderFilter;
        private System.Windows.Forms.DataGridViewTextBoxColumn FolderFilterName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FolderFilterString;
        private System.Windows.Forms.DataGridViewButtonColumn buttonRemoveFolderFilter;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSave;
    }
}