using ePlus.MetaData.Client;

namespace DocsRegistryEx
{
    partial class DocsRegistryForm
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocsRegistryForm));
			this.ucContractor = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.ucStore = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
			this.ucDocuments = new ePlus.CommonEx.Controls.SubtractionSelectControl();
			this.chkLbDocStatå = new System.Windows.Forms.CheckedListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.chkLbShowParams = new System.Windows.Forms.CheckedListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.ucGoods = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.label3 = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnReport = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// ucContractor
			// 
			this.ucContractor.AllowSaveState = false;
			this.ucContractor.Caption = "Êîíòðàãåíò(û)";
			this.ucContractor.Location = new System.Drawing.Point(12, 259);
			this.ucContractor.Mnemocode = "CONTRACTOR";
			this.ucContractor.Name = "ucContractor";
			this.ucContractor.Size = new System.Drawing.Size(304, 74);
			this.ucContractor.TabIndex = 1;
			// 
			// ucStore
			// 
			this.ucStore.AllowSaveState = false;
			this.ucStore.Caption = "Ñêëàä(û)";
			this.ucStore.Location = new System.Drawing.Point(319, 259);
			this.ucStore.Mnemocode = "STORE";
			this.ucStore.Name = "ucStore";
			this.ucStore.Size = new System.Drawing.Size(297, 74);
			this.ucStore.TabIndex = 2;
			// 
			// ucPeriod
			// 
			this.ucPeriod.DateFrom = new System.DateTime(2006, 10, 30, 14, 2, 0, 906);
			this.ucPeriod.DateTo = new System.DateTime(2006, 10, 30, 14, 2, 0, 921);
			this.ucPeriod.Location = new System.Drawing.Point(72, 30);
			this.ucPeriod.Name = "ucPeriod";
			this.ucPeriod.Size = new System.Drawing.Size(285, 21);
			this.ucPeriod.TabIndex = 0;
			// 
			// ucDocuments
			// 
			this.ucDocuments.DataSource = null;
			this.ucDocuments.DescriptionMember = null;
			this.ucDocuments.DisplayMember = null;
			this.ucDocuments.KeyFieldName = null;
			this.ucDocuments.LableForm = "";
			this.ucDocuments.LableTo = "";
			this.ucDocuments.Location = new System.Drawing.Point(21, 68);
			this.ucDocuments.Name = "ucDocuments";
			this.ucDocuments.SearchMember = null;
			this.ucDocuments.SelectedList = ((System.Collections.IList)(resources.GetObject("ucDocuments.SelectedList")));
			this.ucDocuments.Size = new System.Drawing.Size(452, 185);
			this.ucDocuments.TabIndex = 151;
			// 
			// chkLbDocStatå
			// 
			this.chkLbDocStatå.CheckOnClick = true;
			this.chkLbDocStatå.FormattingEnabled = true;
			this.chkLbDocStatå.Items.AddRange(new object[] {
            "Ñîõðàíåí",
            "Ïðîâåäåí",
            "Óäàëåí"});
			this.chkLbDocStatå.Location = new System.Drawing.Point(479, 68);
			this.chkLbDocStatå.Name = "chkLbDocStatå";
			this.chkLbDocStatå.Size = new System.Drawing.Size(137, 64);
			this.chkLbDocStatå.TabIndex = 153;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(476, 52);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(118, 13);
			this.label1.TabIndex = 154;
			this.label1.Text = "Ñîñòîÿíèå äîêóìåíòà";
			// 
			// chkLbShowParams
			// 
			this.chkLbShowParams.CheckOnClick = true;
			this.chkLbShowParams.FormattingEnabled = true;
			this.chkLbShowParams.Items.AddRange(new object[] {
            "Îïò. ñóììà",
            "ÍÄÑ",
            "Èòîãè"});
			this.chkLbShowParams.Location = new System.Drawing.Point(477, 159);
			this.chkLbShowParams.Name = "chkLbShowParams";
			this.chkLbShowParams.Size = new System.Drawing.Size(137, 64);
			this.chkLbShowParams.TabIndex = 155;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(474, 143);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(57, 13);
			this.label2.TabIndex = 156;
			this.label2.Text = "Âûâîäèòü";
			// 
			// ucGoods
			// 
			this.ucGoods.AllowSaveState = false;
			this.ucGoods.Caption = "Äîêóìåíòû ñîäåðæàò òîâàð(û)";
			this.ucGoods.Location = new System.Drawing.Point(12, 339);
			this.ucGoods.Mnemocode = "GOODS2";
			this.ucGoods.Name = "ucGoods";
			this.ucGoods.Size = new System.Drawing.Size(604, 74);
			this.ucGoods.TabIndex = 158;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(18, 34);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(45, 13);
			this.label3.TabIndex = 159;
			this.label3.Text = "Ïåðèîä";
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(539, 419);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 160;
			this.btnCancel.Text = "Îòìåíà";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnReport
			// 
			this.btnReport.Location = new System.Drawing.Point(456, 419);
			this.btnReport.Name = "btnReport";
			this.btnReport.Size = new System.Drawing.Size(75, 23);
			this.btnReport.TabIndex = 161;
			this.btnReport.Text = "Ïå÷àòü";
			this.btnReport.UseVisualStyleBackColor = true;
			this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(18, 60);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(66, 13);
			this.label4.TabIndex = 162;
			this.label4.Text = "Äîêóìåíòû";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(256, 60);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(125, 13);
			this.label5.TabIndex = 163;
			this.label5.Text = "Âûáðàííûå äîêóìåíòû";
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(628, 25);
			this.toolStrip1.TabIndex = 166;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(76, 22);
			this.toolStripButton1.Text = "Î÷èñòèòü";
			this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
			// 
			// DocsRegistryForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(628, 448);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.btnReport);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.ucGoods);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.chkLbShowParams);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.chkLbDocStatå);
			this.Controls.Add(this.ucDocuments);
			this.Controls.Add(this.ucContractor);
			this.Controls.Add(this.ucStore);
			this.Controls.Add(this.ucPeriod);
			this.Name = "DocsRegistryForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Ïàðàìåòðû îò÷åòà: ðååñòð äîêóìåíòîâ";
			this.Load += new System.EventHandler(this.DocsRegistryForm_Load);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private ePlus.MetaData.Client.UCPluginMultiSelect ucContractor;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucStore;
        private UCPeriod ucPeriod;
		//private ePlus.Controls.NewControls.ePlusButton btnReport;
		//private ePlus.Controls.NewControls.ePlusButton btnCancel;
      //private ePlus.Controls.Controls.ePlusLabel ePlusLabel3;
      private ePlus.CommonEx.Controls.SubtractionSelectControl ucDocuments;
      //private ePlus.Controls.Controls.ePlusLabel ePlusLabel1;
      private System.Windows.Forms.CheckedListBox chkLbDocStatå;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.CheckedListBox chkLbShowParams;
      private System.Windows.Forms.Label label2;
      //private ePlus.Controls.Controls.ePlusLabel ePlusLabel2;
      private UCPluginMultiSelect ucGoods;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnReport;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}