namespace VSArrange.Control
{
    partial class OutputResult
    {
        /// <summary> 
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナで生成されたコード

        /// <summary> 
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSelectOutputResultPath = new System.Windows.Forms.Button();
            this.txtOutputResultPath = new System.Windows.Forms.TextBox();
            this.chkIsOutputFile = new System.Windows.Forms.CheckBox();
            this.chkIsOuputWindow = new System.Windows.Forms.CheckBox();
            this.dlgSelectOutputResultPath = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // btnSelectOutputResultPath
            // 
            this.btnSelectOutputResultPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectOutputResultPath.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSelectOutputResultPath.Location = new System.Drawing.Point(463, 17);
            this.btnSelectOutputResultPath.Name = "btnSelectOutputResultPath";
            this.btnSelectOutputResultPath.Size = new System.Drawing.Size(35, 23);
            this.btnSelectOutputResultPath.TabIndex = 7;
            this.btnSelectOutputResultPath.Text = "...";
            this.btnSelectOutputResultPath.UseVisualStyleBackColor = true;
            this.btnSelectOutputResultPath.Click += new System.EventHandler(this.btnSelectOutputResultPath_Click);
            // 
            // txtOutputResultPath
            // 
            this.txtOutputResultPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                                                                    | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutputResultPath.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtOutputResultPath.Location = new System.Drawing.Point(79, 19);
            this.txtOutputResultPath.Name = "txtOutputResultPath";
            this.txtOutputResultPath.Size = new System.Drawing.Size(378, 19);
            this.txtOutputResultPath.TabIndex = 6;
            // 
            // chkIsOutputFile
            // 
            this.chkIsOutputFile.AutoSize = true;
            this.chkIsOutputFile.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkIsOutputFile.Location = new System.Drawing.Point(0, 22);
            this.chkIsOutputFile.Name = "chkIsOutputFile";
            this.chkIsOutputFile.Size = new System.Drawing.Size(73, 16);
            this.chkIsOutputFile.TabIndex = 5;
            this.chkIsOutputFile.Text = "ファイル(&F)";
            this.chkIsOutputFile.UseVisualStyleBackColor = true;
            this.chkIsOutputFile.CheckedChanged += new System.EventHandler(this.chkIsOutputFile_CheckedChanged);
            // 
            // chkIsOuputWindow
            // 
            this.chkIsOuputWindow.AutoSize = true;
            this.chkIsOuputWindow.Checked = true;
            this.chkIsOuputWindow.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIsOuputWindow.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkIsOuputWindow.Location = new System.Drawing.Point(0, 0);
            this.chkIsOuputWindow.Name = "chkIsOuputWindow";
            this.chkIsOuputWindow.Size = new System.Drawing.Size(84, 16);
            this.chkIsOuputWindow.TabIndex = 4;
            this.chkIsOuputWindow.Text = "ウィンドウ(&W)";
            this.chkIsOuputWindow.UseVisualStyleBackColor = true;
            // 
            // OutputResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSelectOutputResultPath);
            this.Controls.Add(this.txtOutputResultPath);
            this.Controls.Add(this.chkIsOutputFile);
            this.Controls.Add(this.chkIsOuputWindow);
            this.MaximumSize = new System.Drawing.Size(604, 44);
            this.Name = "OutputResult";
            this.Size = new System.Drawing.Size(498, 41);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelectOutputResultPath;
        private System.Windows.Forms.TextBox txtOutputResultPath;
        private System.Windows.Forms.CheckBox chkIsOutputFile;
        private System.Windows.Forms.CheckBox chkIsOuputWindow;
        private System.Windows.Forms.FolderBrowserDialog dlgSelectOutputResultPath;

    }
}