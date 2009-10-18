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
            this.tabgrpAllFilter = new System.Windows.Forms.TabControl();
            this.tabExclueProject = new System.Windows.Forms.TabPage();
            this.tabgrpExcludeProject = new System.Windows.Forms.TabControl();
            this.tabExcludeFile = new System.Windows.Forms.TabPage();
            this.tabExcludeFolder = new System.Windows.Forms.TabPage();
            this.tabBuildAction = new System.Windows.Forms.TabPage();
            this.tabBuildActionItems = new System.Windows.Forms.TabControl();
            this.tabCompile = new System.Windows.Forms.TabPage();
            this.tabResources = new System.Windows.Forms.TabPage();
            this.tabContents = new System.Windows.Forms.TabPage();
            this.tabNoAction = new System.Windows.Forms.TabPage();
            this.tabOutputDirectoryCopy = new System.Windows.Forms.TabPage();
            this.tabgrpOutputDirectoryCopy = new System.Windows.Forms.TabControl();
            this.tabNoCopy = new System.Windows.Forms.TabPage();
            this.tabEverytimeCopy = new System.Windows.Forms.TabPage();
            this.tabCopyIfNew = new System.Windows.Forms.TabPage();
            this.btnEditDirect = new System.Windows.Forms.Button();
            this.filterFile = new VSArrange.Control.Window.FilterList();
            this.filterFolder = new VSArrange.Control.Window.FilterList();
            this.filterCompile = new VSArrange.Control.Window.FilterList();
            this.filterResource = new VSArrange.Control.Window.FilterList();
            this.filterContents = new VSArrange.Control.Window.FilterList();
            this.filterNoAction = new VSArrange.Control.Window.FilterList();
            this.filterNoCopy = new VSArrange.Control.Window.FilterList();
            this.filterEverytimeCopy = new VSArrange.Control.Window.FilterList();
            this.filterCopyIfNew = new VSArrange.Control.Window.FilterList();
            this.tabgrpAllFilter.SuspendLayout();
            this.tabExclueProject.SuspendLayout();
            this.tabgrpExcludeProject.SuspendLayout();
            this.tabExcludeFile.SuspendLayout();
            this.tabExcludeFolder.SuspendLayout();
            this.tabBuildAction.SuspendLayout();
            this.tabBuildActionItems.SuspendLayout();
            this.tabCompile.SuspendLayout();
            this.tabResources.SuspendLayout();
            this.tabContents.SuspendLayout();
            this.tabNoAction.SuspendLayout();
            this.tabOutputDirectoryCopy.SuspendLayout();
            this.tabgrpOutputDirectoryCopy.SuspendLayout();
            this.tabNoCopy.SuspendLayout();
            this.tabEverytimeCopy.SuspendLayout();
            this.tabCopyIfNew.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblNoticeFilterInput
            // 
            this.lblNoticeFilterInput.AutoSize = true;
            this.lblNoticeFilterInput.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNoticeFilterInput.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblNoticeFilterInput.Location = new System.Drawing.Point(12, 9);
            this.lblNoticeFilterInput.Name = "lblNoticeFilterInput";
            this.lblNoticeFilterInput.Size = new System.Drawing.Size(111, 17);
            this.lblNoticeFilterInput.TabIndex = 1;
            this.lblNoticeFilterInput.Text = "フィルターの設定";
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
            this.txtNotice.Size = new System.Drawing.Size(591, 35);
            this.txtNotice.TabIndex = 4;
            this.txtNotice.TabStop = false;
            this.txtNotice.Text = "プロジェクト要素整理時のフィルタを設定します。\r\n（正規表現も使えます。大文字小文字は区別しません）";
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnExit.Location = new System.Drawing.Point(467, 498);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(136, 29);
            this.btnExit.TabIndex = 303;
            this.btnExit.Text = "保存せずに終了(&C)";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSave.Location = new System.Drawing.Point(326, 498);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(135, 29);
            this.btnSave.TabIndex = 302;
            this.btnSave.Text = "保存して終了(&S)";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tabgrpAllFilter
            // 
            this.tabgrpAllFilter.Controls.Add(this.tabExclueProject);
            this.tabgrpAllFilter.Controls.Add(this.tabBuildAction);
            this.tabgrpAllFilter.Controls.Add(this.tabOutputDirectoryCopy);
            this.tabgrpAllFilter.Location = new System.Drawing.Point(14, 69);
            this.tabgrpAllFilter.Name = "tabgrpAllFilter";
            this.tabgrpAllFilter.SelectedIndex = 0;
            this.tabgrpAllFilter.Size = new System.Drawing.Size(590, 423);
            this.tabgrpAllFilter.TabIndex = 0;
            // 
            // tabExclueProject
            // 
            this.tabExclueProject.Controls.Add(this.tabgrpExcludeProject);
            this.tabExclueProject.Location = new System.Drawing.Point(4, 21);
            this.tabExclueProject.Name = "tabExclueProject";
            this.tabExclueProject.Padding = new System.Windows.Forms.Padding(3);
            this.tabExclueProject.Size = new System.Drawing.Size(582, 398);
            this.tabExclueProject.TabIndex = 0;
            this.tabExclueProject.Text = "プロジェクト除外";
            this.tabExclueProject.UseVisualStyleBackColor = true;
            // 
            // tabgrpExcludeProject
            // 
            this.tabgrpExcludeProject.Controls.Add(this.tabExcludeFile);
            this.tabgrpExcludeProject.Controls.Add(this.tabExcludeFolder);
            this.tabgrpExcludeProject.Location = new System.Drawing.Point(7, 7);
            this.tabgrpExcludeProject.Name = "tabgrpExcludeProject";
            this.tabgrpExcludeProject.SelectedIndex = 0;
            this.tabgrpExcludeProject.Size = new System.Drawing.Size(569, 385);
            this.tabgrpExcludeProject.TabIndex = 12;
            // 
            // tabExcludeFile
            // 
            this.tabExcludeFile.Controls.Add(this.filterFile);
            this.tabExcludeFile.Location = new System.Drawing.Point(4, 21);
            this.tabExcludeFile.Name = "tabExcludeFile";
            this.tabExcludeFile.Padding = new System.Windows.Forms.Padding(3);
            this.tabExcludeFile.Size = new System.Drawing.Size(561, 360);
            this.tabExcludeFile.TabIndex = 0;
            this.tabExcludeFile.Text = "ファイル";
            this.tabExcludeFile.UseVisualStyleBackColor = true;
            // 
            // tabExcludeFolder
            // 
            this.tabExcludeFolder.Controls.Add(this.filterFolder);
            this.tabExcludeFolder.Location = new System.Drawing.Point(4, 21);
            this.tabExcludeFolder.Name = "tabExcludeFolder";
            this.tabExcludeFolder.Padding = new System.Windows.Forms.Padding(3);
            this.tabExcludeFolder.Size = new System.Drawing.Size(561, 360);
            this.tabExcludeFolder.TabIndex = 1;
            this.tabExcludeFolder.Text = "フォルダ";
            this.tabExcludeFolder.UseVisualStyleBackColor = true;
            // 
            // tabBuildAction
            // 
            this.tabBuildAction.Controls.Add(this.tabBuildActionItems);
            this.tabBuildAction.Location = new System.Drawing.Point(4, 21);
            this.tabBuildAction.Name = "tabBuildAction";
            this.tabBuildAction.Padding = new System.Windows.Forms.Padding(3);
            this.tabBuildAction.Size = new System.Drawing.Size(582, 398);
            this.tabBuildAction.TabIndex = 1;
            this.tabBuildAction.Text = "ビルドアクション";
            this.tabBuildAction.UseVisualStyleBackColor = true;
            // 
            // tabBuildActionItems
            // 
            this.tabBuildActionItems.Controls.Add(this.tabCompile);
            this.tabBuildActionItems.Controls.Add(this.tabResources);
            this.tabBuildActionItems.Controls.Add(this.tabContents);
            this.tabBuildActionItems.Controls.Add(this.tabNoAction);
            this.tabBuildActionItems.Location = new System.Drawing.Point(7, 7);
            this.tabBuildActionItems.Name = "tabBuildActionItems";
            this.tabBuildActionItems.SelectedIndex = 0;
            this.tabBuildActionItems.Size = new System.Drawing.Size(569, 385);
            this.tabBuildActionItems.TabIndex = 101;
            // 
            // tabCompile
            // 
            this.tabCompile.Controls.Add(this.filterCompile);
            this.tabCompile.Location = new System.Drawing.Point(4, 21);
            this.tabCompile.Name = "tabCompile";
            this.tabCompile.Padding = new System.Windows.Forms.Padding(3);
            this.tabCompile.Size = new System.Drawing.Size(561, 360);
            this.tabCompile.TabIndex = 0;
            this.tabCompile.Text = "コンパイル";
            this.tabCompile.UseVisualStyleBackColor = true;
            // 
            // tabResources
            // 
            this.tabResources.Controls.Add(this.filterResource);
            this.tabResources.Location = new System.Drawing.Point(4, 21);
            this.tabResources.Name = "tabResources";
            this.tabResources.Padding = new System.Windows.Forms.Padding(3);
            this.tabResources.Size = new System.Drawing.Size(561, 360);
            this.tabResources.TabIndex = 1;
            this.tabResources.Text = "埋め込みリソース";
            this.tabResources.UseVisualStyleBackColor = true;
            // 
            // tabContents
            // 
            this.tabContents.Controls.Add(this.filterContents);
            this.tabContents.Location = new System.Drawing.Point(4, 21);
            this.tabContents.Name = "tabContents";
            this.tabContents.Size = new System.Drawing.Size(561, 360);
            this.tabContents.TabIndex = 2;
            this.tabContents.Text = "コンテンツ";
            this.tabContents.UseVisualStyleBackColor = true;
            // 
            // tabNoAction
            // 
            this.tabNoAction.Controls.Add(this.filterNoAction);
            this.tabNoAction.Location = new System.Drawing.Point(4, 21);
            this.tabNoAction.Name = "tabNoAction";
            this.tabNoAction.Size = new System.Drawing.Size(561, 360);
            this.tabNoAction.TabIndex = 3;
            this.tabNoAction.Text = "なし";
            this.tabNoAction.UseVisualStyleBackColor = true;
            // 
            // tabOutputDirectoryCopy
            // 
            this.tabOutputDirectoryCopy.Controls.Add(this.tabgrpOutputDirectoryCopy);
            this.tabOutputDirectoryCopy.Location = new System.Drawing.Point(4, 21);
            this.tabOutputDirectoryCopy.Name = "tabOutputDirectoryCopy";
            this.tabOutputDirectoryCopy.Size = new System.Drawing.Size(582, 398);
            this.tabOutputDirectoryCopy.TabIndex = 2;
            this.tabOutputDirectoryCopy.Text = "出力ディレクトリにコピー";
            this.tabOutputDirectoryCopy.UseVisualStyleBackColor = true;
            // 
            // tabgrpOutputDirectoryCopy
            // 
            this.tabgrpOutputDirectoryCopy.Controls.Add(this.tabNoCopy);
            this.tabgrpOutputDirectoryCopy.Controls.Add(this.tabEverytimeCopy);
            this.tabgrpOutputDirectoryCopy.Controls.Add(this.tabCopyIfNew);
            this.tabgrpOutputDirectoryCopy.Location = new System.Drawing.Point(7, 7);
            this.tabgrpOutputDirectoryCopy.Name = "tabgrpOutputDirectoryCopy";
            this.tabgrpOutputDirectoryCopy.SelectedIndex = 0;
            this.tabgrpOutputDirectoryCopy.Size = new System.Drawing.Size(569, 385);
            this.tabgrpOutputDirectoryCopy.TabIndex = 201;
            // 
            // tabNoCopy
            // 
            this.tabNoCopy.Controls.Add(this.filterNoCopy);
            this.tabNoCopy.Location = new System.Drawing.Point(4, 21);
            this.tabNoCopy.Name = "tabNoCopy";
            this.tabNoCopy.Padding = new System.Windows.Forms.Padding(3);
            this.tabNoCopy.Size = new System.Drawing.Size(561, 360);
            this.tabNoCopy.TabIndex = 0;
            this.tabNoCopy.Text = "コピーしない";
            this.tabNoCopy.UseVisualStyleBackColor = true;
            // 
            // tabEverytimeCopy
            // 
            this.tabEverytimeCopy.Controls.Add(this.filterEverytimeCopy);
            this.tabEverytimeCopy.Location = new System.Drawing.Point(4, 21);
            this.tabEverytimeCopy.Name = "tabEverytimeCopy";
            this.tabEverytimeCopy.Padding = new System.Windows.Forms.Padding(3);
            this.tabEverytimeCopy.Size = new System.Drawing.Size(561, 360);
            this.tabEverytimeCopy.TabIndex = 1;
            this.tabEverytimeCopy.Text = "常にコピー";
            this.tabEverytimeCopy.UseVisualStyleBackColor = true;
            // 
            // tabCopyIfNew
            // 
            this.tabCopyIfNew.Controls.Add(this.filterCopyIfNew);
            this.tabCopyIfNew.Location = new System.Drawing.Point(4, 21);
            this.tabCopyIfNew.Name = "tabCopyIfNew";
            this.tabCopyIfNew.Size = new System.Drawing.Size(561, 360);
            this.tabCopyIfNew.TabIndex = 2;
            this.tabCopyIfNew.Text = "新しい場合はコピー";
            this.tabCopyIfNew.UseVisualStyleBackColor = true;
            // 
            // btnEditDirect
            // 
            this.btnEditDirect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditDirect.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnEditDirect.Location = new System.Drawing.Point(185, 498);
            this.btnEditDirect.Name = "btnEditDirect";
            this.btnEditDirect.Size = new System.Drawing.Size(135, 29);
            this.btnEditDirect.TabIndex = 301;
            this.btnEditDirect.Text = "直接編集(&E)";
            this.btnEditDirect.UseVisualStyleBackColor = true;
            this.btnEditDirect.Click += new System.EventHandler(this.btnEditDirect_Click);
            // 
            // filterFile
            // 
            this.filterFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.filterFile.AutoSize = true;
            this.filterFile.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.filterFile.FilterName = "除外ファイル";
            this.filterFile.Location = new System.Drawing.Point(6, 3);
            this.filterFile.Name = "filterFile";
            this.filterFile.Size = new System.Drawing.Size(549, 349);
            this.filterFile.TabIndex = 11;
            // 
            // filterFolder
            // 
            this.filterFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.filterFolder.AutoSize = true;
            this.filterFolder.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.filterFolder.FilterName = "除外フォルダ";
            this.filterFolder.Location = new System.Drawing.Point(6, 3);
            this.filterFolder.Name = "filterFolder";
            this.filterFolder.Size = new System.Drawing.Size(549, 349);
            this.filterFolder.TabIndex = 12;
            // 
            // filterCompile
            // 
            this.filterCompile.AutoSize = true;
            this.filterCompile.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.filterCompile.FilterName = "「コンパイル」として登録";
            this.filterCompile.Location = new System.Drawing.Point(6, 3);
            this.filterCompile.Name = "filterCompile";
            this.filterCompile.Size = new System.Drawing.Size(549, 349);
            this.filterCompile.TabIndex = 111;
            // 
            // filterResource
            // 
            this.filterResource.AutoSize = true;
            this.filterResource.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.filterResource.FilterName = "「埋め込みリソース」として登録";
            this.filterResource.Location = new System.Drawing.Point(6, 3);
            this.filterResource.Name = "filterResource";
            this.filterResource.Size = new System.Drawing.Size(549, 349);
            this.filterResource.TabIndex = 112;
            // 
            // filterContents
            // 
            this.filterContents.AutoSize = true;
            this.filterContents.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.filterContents.FilterName = "「コンテンツ」として登録";
            this.filterContents.Location = new System.Drawing.Point(6, 3);
            this.filterContents.Name = "filterContents";
            this.filterContents.Size = new System.Drawing.Size(549, 349);
            this.filterContents.TabIndex = 113;
            // 
            // filterNoAction
            // 
            this.filterNoAction.AutoSize = true;
            this.filterNoAction.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.filterNoAction.FilterName = "「なし」として登録";
            this.filterNoAction.Location = new System.Drawing.Point(6, 3);
            this.filterNoAction.Name = "filterNoAction";
            this.filterNoAction.Size = new System.Drawing.Size(549, 349);
            this.filterNoAction.TabIndex = 114;
            // 
            // filterNoCopy
            // 
            this.filterNoCopy.AutoSize = true;
            this.filterNoCopy.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.filterNoCopy.FilterName = "「コピーしない」として登録";
            this.filterNoCopy.Location = new System.Drawing.Point(6, 3);
            this.filterNoCopy.Name = "filterNoCopy";
            this.filterNoCopy.Size = new System.Drawing.Size(549, 349);
            this.filterNoCopy.TabIndex = 211;
            // 
            // filterEverytimeCopy
            // 
            this.filterEverytimeCopy.AutoSize = true;
            this.filterEverytimeCopy.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.filterEverytimeCopy.FilterName = "「常にコピーする」として登録";
            this.filterEverytimeCopy.Location = new System.Drawing.Point(6, 3);
            this.filterEverytimeCopy.Name = "filterEverytimeCopy";
            this.filterEverytimeCopy.Size = new System.Drawing.Size(549, 349);
            this.filterEverytimeCopy.TabIndex = 221;
            // 
            // filterCopyIfNew
            // 
            this.filterCopyIfNew.AutoSize = true;
            this.filterCopyIfNew.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.filterCopyIfNew.FilterName = "「新しい場合はコピー」として登録";
            this.filterCopyIfNew.Location = new System.Drawing.Point(6, 3);
            this.filterCopyIfNew.Name = "filterCopyIfNew";
            this.filterCopyIfNew.Size = new System.Drawing.Size(549, 349);
            this.filterCopyIfNew.TabIndex = 222;
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(616, 533);
            this.ControlBox = false;
            this.Controls.Add(this.btnEditDirect);
            this.Controls.Add(this.tabgrpAllFilter);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.txtNotice);
            this.Controls.Add(this.lblNoticeFilterInput);
            this.MaximumSize = new System.Drawing.Size(624, 567);
            this.MinimumSize = new System.Drawing.Size(624, 567);
            this.Name = "ConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VSArrange設定";
            this.Load += new System.EventHandler(this.ConfigForm_Load);
            this.tabgrpAllFilter.ResumeLayout(false);
            this.tabExclueProject.ResumeLayout(false);
            this.tabgrpExcludeProject.ResumeLayout(false);
            this.tabExcludeFile.ResumeLayout(false);
            this.tabExcludeFile.PerformLayout();
            this.tabExcludeFolder.ResumeLayout(false);
            this.tabExcludeFolder.PerformLayout();
            this.tabBuildAction.ResumeLayout(false);
            this.tabBuildActionItems.ResumeLayout(false);
            this.tabCompile.ResumeLayout(false);
            this.tabCompile.PerformLayout();
            this.tabResources.ResumeLayout(false);
            this.tabResources.PerformLayout();
            this.tabContents.ResumeLayout(false);
            this.tabContents.PerformLayout();
            this.tabNoAction.ResumeLayout(false);
            this.tabNoAction.PerformLayout();
            this.tabOutputDirectoryCopy.ResumeLayout(false);
            this.tabgrpOutputDirectoryCopy.ResumeLayout(false);
            this.tabNoCopy.ResumeLayout(false);
            this.tabNoCopy.PerformLayout();
            this.tabEverytimeCopy.ResumeLayout(false);
            this.tabEverytimeCopy.PerformLayout();
            this.tabCopyIfNew.ResumeLayout(false);
            this.tabCopyIfNew.PerformLayout();
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
        private System.Windows.Forms.TabControl tabgrpAllFilter;
        private System.Windows.Forms.TabPage tabExclueProject;
        private System.Windows.Forms.TabPage tabBuildAction;
        private System.Windows.Forms.TabPage tabOutputDirectoryCopy;
        private System.Windows.Forms.TabControl tabBuildActionItems;
        private System.Windows.Forms.TabPage tabCompile;
        private System.Windows.Forms.TabPage tabResources;
        private System.Windows.Forms.TabControl tabgrpExcludeProject;
        private System.Windows.Forms.TabPage tabExcludeFile;
        private System.Windows.Forms.TabPage tabExcludeFolder;
        private System.Windows.Forms.TabPage tabContents;
        private System.Windows.Forms.TabPage tabNoAction;
        private FilterList filterCompile;
        private FilterList filterResource;
        private FilterList filterContents;
        private FilterList filterNoAction;
        private System.Windows.Forms.TabControl tabgrpOutputDirectoryCopy;
        private System.Windows.Forms.TabPage tabNoCopy;
        private System.Windows.Forms.TabPage tabEverytimeCopy;
        private FilterList filterNoCopy;
        private FilterList filterEverytimeCopy;
        private System.Windows.Forms.TabPage tabCopyIfNew;
        private FilterList filterCopyIfNew;
        private System.Windows.Forms.Button btnEditDirect;
    }
}