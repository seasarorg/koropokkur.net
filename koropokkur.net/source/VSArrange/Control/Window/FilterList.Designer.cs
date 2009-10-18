namespace VSArrange.Control.Window
{
    partial class FilterList
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grpFilter = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lblTestExplain = new System.Windows.Forms.Label();
            this.lblOK = new System.Windows.Forms.Label();
            this.txtTestInput = new System.Windows.Forms.TextBox();
            this.lblNG = new System.Windows.Forms.Label();
            this.dgFilters = new System.Windows.Forms.DataGridView();
            this.IsEnableFileFilter = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.FileFilterName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileFilterString = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonRemoveFileFilter = new System.Windows.Forms.DataGridViewButtonColumn();
            this.timerCloseMessage = new System.Windows.Forms.Timer(this.components);
            this.grpFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgFilters)).BeginInit();
            this.SuspendLayout();
            // 
            // grpFilter
            // 
            this.grpFilter.Controls.Add(this.button1);
            this.grpFilter.Controls.Add(this.lblTestExplain);
            this.grpFilter.Controls.Add(this.lblOK);
            this.grpFilter.Controls.Add(this.txtTestInput);
            this.grpFilter.Controls.Add(this.lblNG);
            this.grpFilter.Controls.Add(this.dgFilters);
            this.grpFilter.Location = new System.Drawing.Point(3, 3);
            this.grpFilter.Name = "grpFilter";
            this.grpFilter.Size = new System.Drawing.Size(543, 343);
            this.grpFilter.TabIndex = 11;
            this.grpFilter.TabStop = false;
            this.grpFilter.Text = "ファイル";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button1.Location = new System.Drawing.Point(442, 311);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 23);
            this.button1.TabIndex = 16;
            this.button1.Text = "再読込み(&R)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // lblTestExplain
            // 
            this.lblTestExplain.AutoSize = true;
            this.lblTestExplain.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTestExplain.Location = new System.Drawing.Point(3, 315);
            this.lblTestExplain.Name = "lblTestExplain";
            this.lblTestExplain.Size = new System.Drawing.Size(244, 15);
            this.lblTestExplain.TabIndex = 1;
            this.lblTestExplain.Text = "フィルターテスト(Returnキーでテスト実行)";
            // 
            // lblOK
            // 
            this.lblOK.AutoSize = true;
            this.lblOK.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblOK.Font = new System.Drawing.Font("MS UI Gothic", 48F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblOK.ForeColor = System.Drawing.Color.Blue;
            this.lblOK.Location = new System.Drawing.Point(183, 138);
            this.lblOK.Name = "lblOK";
            this.lblOK.Size = new System.Drawing.Size(164, 64);
            this.lblOK.TabIndex = 15;
            this.lblOK.Text = "OK !!";
            this.lblOK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblOK.Visible = false;
            // 
            // txtTestInput
            // 
            this.txtTestInput.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtTestInput.Location = new System.Drawing.Point(250, 312);
            this.txtTestInput.Name = "txtTestInput";
            this.txtTestInput.Size = new System.Drawing.Size(186, 22);
            this.txtTestInput.TabIndex = 13;
            this.txtTestInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTestInput_KeyDown);
            this.txtTestInput.LostFocus += new System.EventHandler(this.txtTestInput_LostFocus);
            // 
            // lblNG
            // 
            this.lblNG.AutoSize = true;
            this.lblNG.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblNG.Font = new System.Drawing.Font("MS UI Gothic", 48F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblNG.ForeColor = System.Drawing.Color.Red;
            this.lblNG.Location = new System.Drawing.Point(190, 138);
            this.lblNG.Name = "lblNG";
            this.lblNG.Size = new System.Drawing.Size(157, 64);
            this.lblNG.TabIndex = 14;
            this.lblNG.Text = "NG...";
            this.lblNG.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNG.Visible = false;
            // 
            // dgFilters
            // 
            this.dgFilters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgFilters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgFilters.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IsEnableFileFilter,
            this.FileFilterName,
            this.FileFilterString,
            this.buttonRemoveFileFilter});
            this.dgFilters.Location = new System.Drawing.Point(6, 18);
            this.dgFilters.Name = "dgFilters";
            this.dgFilters.RowHeadersVisible = false;
            this.dgFilters.RowTemplate.Height = 21;
            this.dgFilters.Size = new System.Drawing.Size(531, 287);
            this.dgFilters.TabIndex = 0;
            this.dgFilters.CellContentClick += dgFilters_CellContentClick;
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
            // timerCloseMessage
            // 
            this.timerCloseMessage.Interval = 2000;
            this.timerCloseMessage.Tick += new System.EventHandler(this.timerCloseMessage_Tick);
            // 
            // FilterList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.grpFilter);
            this.Name = "FilterList";
            this.Size = new System.Drawing.Size(549, 349);
            this.grpFilter.ResumeLayout(false);
            this.grpFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgFilters)).EndInit();
            this.ResumeLayout(false);

        }

        

        

        #endregion

        private System.Windows.Forms.GroupBox grpFilter;
        private System.Windows.Forms.DataGridView dgFilters;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsEnableFileFilter;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileFilterName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileFilterString;
        private System.Windows.Forms.DataGridViewButtonColumn buttonRemoveFileFilter;
        private System.Windows.Forms.TextBox txtTestInput;
        private System.Windows.Forms.Label lblTestExplain;
        private System.Windows.Forms.Label lblNG;
        private System.Windows.Forms.Label lblOK;
        private System.Windows.Forms.Timer timerCloseMessage;
        private System.Windows.Forms.Button button1;
    }
}
