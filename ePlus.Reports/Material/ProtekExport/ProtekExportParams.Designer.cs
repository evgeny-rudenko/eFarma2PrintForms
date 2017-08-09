namespace RCChProtekExport
{
	partial class ProtekExportParams
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
            this.label1 = new System.Windows.Forms.Label();
            this.clstStore = new System.Windows.Forms.CheckedListBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnExport = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnContractor = new System.Windows.Forms.Button();
            this.btnExportDirectory = new System.Windows.Forms.Button();
            this.lblExportDirectory = new System.Windows.Forms.Label();
            this.txtContractor = new System.Windows.Forms.TextBox();
            this.txtExportDirectory = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.checkBoxSelfStores = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(227, 3);
            this.bOK.Click += new System.EventHandler(this.bOK_Click_1);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(302, 3);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 288);
            this.panel1.Size = new System.Drawing.Size(380, 29);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-111, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 132;
            this.label1.Text = "Период:";
            // 
            // clstStore
            // 
            this.clstStore.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.clstStore.FormattingEnabled = true;
            this.clstStore.Location = new System.Drawing.Point(12, 115);
            this.clstStore.Name = "clstStore";
            this.clstStore.Size = new System.Drawing.Size(356, 139);
            this.clstStore.TabIndex = 141;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(12, 263);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(355, 14);
            this.progressBar1.TabIndex = 140;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(284, 66);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(76, 21);
            this.btnExport.TabIndex = 139;
            this.btnExport.Text = "Экспорт";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Visible = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 135;
            this.label2.Text = "Поставщик:";
            // 
            // btnContractor
            // 
            this.btnContractor.Location = new System.Drawing.Point(247, 66);
            this.btnContractor.Name = "btnContractor";
            this.btnContractor.Size = new System.Drawing.Size(27, 21);
            this.btnContractor.TabIndex = 138;
            this.btnContractor.Text = "...";
            this.btnContractor.UseVisualStyleBackColor = true;
            this.btnContractor.Click += new System.EventHandler(this.btnContractor_Click);
            // 
            // btnExportDirectory
            // 
            this.btnExportDirectory.Location = new System.Drawing.Point(332, 27);
            this.btnExportDirectory.Name = "btnExportDirectory";
            this.btnExportDirectory.Size = new System.Drawing.Size(27, 20);
            this.btnExportDirectory.TabIndex = 137;
            this.btnExportDirectory.Text = "...";
            this.btnExportDirectory.UseVisualStyleBackColor = true;
            this.btnExportDirectory.Click += new System.EventHandler(this.btnExportDirectory_Click);
            // 
            // lblExportDirectory
            // 
            this.lblExportDirectory.AutoSize = true;
            this.lblExportDirectory.Location = new System.Drawing.Point(9, 10);
            this.lblExportDirectory.Name = "lblExportDirectory";
            this.lblExportDirectory.Size = new System.Drawing.Size(101, 13);
            this.lblExportDirectory.TabIndex = 136;
            this.lblExportDirectory.Text = "Каталог экспорта:";
            // 
            // txtContractor
            // 
            this.txtContractor.BackColor = System.Drawing.SystemColors.Window;
            this.txtContractor.Location = new System.Drawing.Point(12, 66);
            this.txtContractor.Name = "txtContractor";
            this.txtContractor.ReadOnly = true;
            this.txtContractor.Size = new System.Drawing.Size(229, 20);
            this.txtContractor.TabIndex = 133;
            // 
            // txtExportDirectory
            // 
            this.txtExportDirectory.Location = new System.Drawing.Point(12, 27);
            this.txtExportDirectory.Name = "txtExportDirectory";
            this.txtExportDirectory.Size = new System.Drawing.Size(314, 20);
            this.txtExportDirectory.TabIndex = 134;
            this.txtExportDirectory.TextChanged += new System.EventHandler(this.txtExportDirectory_TextChanged);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Description = "Выберите каталог в который будет производиться экспорт:";
            // 
            // checkBoxSelfStores
            // 
            this.checkBoxSelfStores.AutoSize = true;
            this.checkBoxSelfStores.Location = new System.Drawing.Point(12, 92);
            this.checkBoxSelfStores.Name = "checkBoxSelfStores";
            this.checkBoxSelfStores.Size = new System.Drawing.Size(177, 17);
            this.checkBoxSelfStores.TabIndex = 142;
            this.checkBoxSelfStores.Text = "Только  собственные склады";
            this.checkBoxSelfStores.UseVisualStyleBackColor = true;
            this.checkBoxSelfStores.CheckedChanged += new System.EventHandler(this.checkBoxSelfStores_CheckedChanged);
            // 
            // ProtekExportParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 317);
            this.Controls.Add(this.checkBoxSelfStores);
            this.Controls.Add(this.clstStore);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnContractor);
            this.Controls.Add(this.btnExportDirectory);
            this.Controls.Add(this.lblExportDirectory);
            this.Controls.Add(this.txtContractor);
            this.Controls.Add(this.txtExportDirectory);
            this.Controls.Add(this.label1);
            this.Name = "ProtekExportParams";
            this.Load += new System.EventHandler(this.TaxGroupsParams_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TaxGroupsParams_FormClosed);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.txtExportDirectory, 0);
            this.Controls.SetChildIndex(this.txtContractor, 0);
            this.Controls.SetChildIndex(this.lblExportDirectory, 0);
            this.Controls.SetChildIndex(this.btnExportDirectory, 0);
            this.Controls.SetChildIndex(this.btnContractor, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.btnExport, 0);
            this.Controls.SetChildIndex(this.progressBar1, 0);
            this.Controls.SetChildIndex(this.clstStore, 0);
            this.Controls.SetChildIndex(this.checkBoxSelfStores, 0);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.Label label1;
        protected System.Windows.Forms.CheckedListBox clstStore;
        protected System.Windows.Forms.ProgressBar progressBar1;
        protected System.Windows.Forms.Button btnExport;
        protected System.Windows.Forms.Label label2;
        protected System.Windows.Forms.Button btnContractor;
        protected System.Windows.Forms.Button btnExportDirectory;
        protected System.Windows.Forms.Label lblExportDirectory;
        protected System.Windows.Forms.TextBox txtContractor;
        protected System.Windows.Forms.TextBox txtExportDirectory;
        protected System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.CheckBox checkBoxSelfStores;		
	}
}