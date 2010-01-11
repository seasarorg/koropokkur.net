namespace VSArrange.Control
{
    partial class ResultMessageForm
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
            this.btnClose = new System.Windows.Forms.Button();
            this.lstResultList = new System.Windows.Forms.ListBox();
            this.dgvResultMessage = new System.Windows.Forms.DataGridView();
            this.TargetName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Action = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Path = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultMessage)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnClose.Location = new System.Drawing.Point(12, 408);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(608, 34);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "閉じる(&C)";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lstResultList
            // 
            this.lstResultList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstResultList.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lstResultList.FormattingEnabled = true;
            this.lstResultList.HorizontalScrollbar = true;
            this.lstResultList.ItemHeight = 16;
            this.lstResultList.Location = new System.Drawing.Point(12, 12);
            this.lstResultList.Name = "lstResultList";
            this.lstResultList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstResultList.Size = new System.Drawing.Size(608, 36);
            this.lstResultList.TabIndex = 1;
            this.lstResultList.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lstResultList_MouseUp);
            // 
            // dgvResultMessage
            // 
            this.dgvResultMessage.AllowUserToAddRows = false;
            this.dgvResultMessage.AllowUserToDeleteRows = false;
            this.dgvResultMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvResultMessage.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvResultMessage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResultMessage.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TargetName,
            this.Action,
            this.Path});
            this.dgvResultMessage.Location = new System.Drawing.Point(12, 54);
            this.dgvResultMessage.Name = "dgvResultMessage";
            this.dgvResultMessage.ReadOnly = true;
            this.dgvResultMessage.RowTemplate.Height = 21;
            this.dgvResultMessage.Size = new System.Drawing.Size(608, 348);
            this.dgvResultMessage.TabIndex = 3;
            // 
            // TargetName
            // 
            this.TargetName.HeaderText = "処理対象";
            this.TargetName.Name = "TargetName";
            this.TargetName.ReadOnly = true;
            this.TargetName.Width = 78;
            // 
            // Action
            // 
            this.Action.HeaderText = "処理内容";
            this.Action.Name = "Action";
            this.Action.ReadOnly = true;
            this.Action.Width = 78;
            // 
            // Path
            // 
            this.Path.HeaderText = "パス";
            this.Path.Name = "Path";
            this.Path.ReadOnly = true;
            this.Path.Width = 49;
            // 
            // ResultMessageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 455);
            this.Controls.Add(this.dgvResultMessage);
            this.Controls.Add(this.lstResultList);
            this.Controls.Add(this.btnClose);
            this.Name = "ResultMessageForm";
            this.Text = "VSArrange処理結果";
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultMessage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ListBox lstResultList;
        private System.Windows.Forms.DataGridView dgvResultMessage;
        private System.Windows.Forms.DataGridViewTextBoxColumn TargetName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Action;
        private System.Windows.Forms.DataGridViewTextBoxColumn Path;
    }
}