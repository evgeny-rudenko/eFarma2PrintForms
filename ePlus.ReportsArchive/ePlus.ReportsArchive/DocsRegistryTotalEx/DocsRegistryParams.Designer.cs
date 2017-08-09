namespace DocsRegistryTotalEx
{
	partial class DocsRegistryParams
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocsRegistryParams));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chkLbShowParams = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkLbDocStatå = new System.Windows.Forms.CheckedListBox();
            this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
            this.ucGoods = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.ucContractorFrom = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.ucStoreFrom = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.ucTypesDocument = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ucContractorTo = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.ucStoreTo = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.gbSort = new System.Windows.Forms.GroupBox();
            this.rbTypeDoc = new System.Windows.Forms.RadioButton();
            this.rbDateDoc = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gbSort.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(518, 3);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(593, 3);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 563);
            this.panel1.Size = new System.Drawing.Size(671, 29);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(671, 25);
            this.toolStrip1.TabIndex = 176;
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 173;
            this.label3.Text = "Ïåðèîä";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(465, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 172;
            this.label2.Text = "Âûâîäèòü";
            this.label2.Visible = false;
            // 
            // chkLbShowParams
            // 
            this.chkLbShowParams.CheckOnClick = true;
            this.chkLbShowParams.FormattingEnabled = true;
            this.chkLbShowParams.Items.AddRange(new object[] {
            "Îïò. ñóììà",
            "ÍÄÑ",
            "Èòîãè"});
            this.chkLbShowParams.Location = new System.Drawing.Point(468, 136);
            this.chkLbShowParams.Name = "chkLbShowParams";
            this.chkLbShowParams.Size = new System.Drawing.Size(137, 64);
            this.chkLbShowParams.TabIndex = 171;
            this.chkLbShowParams.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(467, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 13);
            this.label1.TabIndex = 170;
            this.label1.Text = "Ñîñòîÿíèå äîêóìåíòà";
            // 
            // chkLbDocStatå
            // 
            this.chkLbDocStatå.CheckOnClick = true;
            this.chkLbDocStatå.FormattingEnabled = true;
            this.chkLbDocStatå.Items.AddRange(new object[] {
            "Ñîõðàíåí",
            "Ïðîâåäåí",
            "Óäàëåí"});
            this.chkLbDocStatå.Location = new System.Drawing.Point(470, 53);
            this.chkLbDocStatå.Name = "chkLbDocStatå";
            this.chkLbDocStatå.Size = new System.Drawing.Size(137, 64);
            this.chkLbDocStatå.TabIndex = 169;
            // 
            // ucPeriod
            // 
            this.ucPeriod.DateFrom = new System.DateTime(2006, 10, 30, 14, 2, 0, 906);
            this.ucPeriod.DateTo = new System.DateTime(2006, 10, 30, 14, 2, 0, 921);
            this.ucPeriod.Location = new System.Drawing.Point(63, 26);
            this.ucPeriod.Name = "ucPeriod";
            this.ucPeriod.Size = new System.Drawing.Size(285, 21);
            this.ucPeriod.TabIndex = 167;
            // 
            // ucGoods
            // 
            this.ucGoods.AllowSaveState = false;
            this.ucGoods.Caption = "Äîêóìåíòû ñîäåðæàò òîâàð(û)";
            this.ucGoods.Location = new System.Drawing.Point(12, 400);
            this.ucGoods.Mnemocode = "GOODS2";
            this.ucGoods.Name = "ucGoods";
            this.ucGoods.Size = new System.Drawing.Size(650, 74);
            this.ucGoods.TabIndex = 179;
            // 
            // ucContractorFrom
            // 
            this.ucContractorFrom.AllowSaveState = false;
            this.ucContractorFrom.Caption = "Êîíòðàãåíò(û)";
            this.ucContractorFrom.Location = new System.Drawing.Point(6, 19);
            this.ucContractorFrom.Mnemocode = "CONTRACTOR";
            this.ucContractorFrom.Name = "ucContractorFrom";
            this.ucContractorFrom.Size = new System.Drawing.Size(304, 74);
            this.ucContractorFrom.TabIndex = 177;
            // 
            // ucStoreFrom
            // 
            this.ucStoreFrom.AllowSaveState = false;
            this.ucStoreFrom.Caption = "Ñêëàä(û)";
            this.ucStoreFrom.Location = new System.Drawing.Point(6, 99);
            this.ucStoreFrom.Mnemocode = "STORE";
            this.ucStoreFrom.Name = "ucStoreFrom";
            this.ucStoreFrom.Size = new System.Drawing.Size(304, 74);
            this.ucStoreFrom.TabIndex = 178;
            // 
            // ucTypesDocument
            // 
            this.ucTypesDocument.AllowSaveState = false;
            this.ucTypesDocument.Caption = "Òèïû äîêóìåíòà(îâ)";
            this.ucTypesDocument.Location = new System.Drawing.Point(12, 53);
            this.ucTypesDocument.Mnemocode = "TYPE_DOCUMENT";
            this.ucTypesDocument.Name = "ucTypesDocument";
            this.ucTypesDocument.Size = new System.Drawing.Size(449, 155);
            this.ucTypesDocument.TabIndex = 180;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ucContractorFrom);
            this.groupBox1.Controls.Add(this.ucStoreFrom);
            this.groupBox1.Location = new System.Drawing.Point(12, 214);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(322, 180);
            this.groupBox1.TabIndex = 181;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Îò êîãî";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ucContractorTo);
            this.groupBox2.Controls.Add(this.ucStoreTo);
            this.groupBox2.Location = new System.Drawing.Point(340, 214);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(322, 180);
            this.groupBox2.TabIndex = 182;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Êîìó";
            // 
            // ucContractorTo
            // 
            this.ucContractorTo.AllowSaveState = false;
            this.ucContractorTo.Caption = "Êîíòðàãåíò(û)";
            this.ucContractorTo.Location = new System.Drawing.Point(6, 19);
            this.ucContractorTo.Mnemocode = "CONTRACTOR";
            this.ucContractorTo.Name = "ucContractorTo";
            this.ucContractorTo.Size = new System.Drawing.Size(304, 74);
            this.ucContractorTo.TabIndex = 177;
            // 
            // ucStoreTo
            // 
            this.ucStoreTo.AllowSaveState = false;
            this.ucStoreTo.Caption = "Ñêëàä(û)";
            this.ucStoreTo.Location = new System.Drawing.Point(6, 99);
            this.ucStoreTo.Mnemocode = "STORE";
            this.ucStoreTo.Name = "ucStoreTo";
            this.ucStoreTo.Size = new System.Drawing.Size(304, 74);
            this.ucStoreTo.TabIndex = 178;
            // 
            // gbSort
            // 
            this.gbSort.Controls.Add(this.rbTypeDoc);
            this.gbSort.Controls.Add(this.rbDateDoc);
            this.gbSort.Location = new System.Drawing.Point(18, 481);
            this.gbSort.Name = "gbSort";
            this.gbSort.Size = new System.Drawing.Size(316, 49);
            this.gbSort.TabIndex = 183;
            this.gbSort.TabStop = false;
            this.gbSort.Text = "Ñîðòèðîâêà";
            // 
            // rbTypeDoc
            // 
            this.rbTypeDoc.AutoSize = true;
            this.rbTypeDoc.Checked = true;
            this.rbTypeDoc.Location = new System.Drawing.Point(6, 19);
            this.rbTypeDoc.Name = "rbTypeDoc";
            this.rbTypeDoc.Size = new System.Drawing.Size(119, 17);
            this.rbTypeDoc.TabIndex = 1;
            this.rbTypeDoc.TabStop = true;
            this.rbTypeDoc.Text = "ïî òèïó äîêóìåíòà";
            this.rbTypeDoc.UseVisualStyleBackColor = true;
            // 
            // rbDateDoc
            // 
            this.rbDateDoc.AutoSize = true;
            this.rbDateDoc.Location = new System.Drawing.Point(168, 19);
            this.rbDateDoc.Name = "rbDateDoc";
            this.rbDateDoc.Size = new System.Drawing.Size(120, 17);
            this.rbDateDoc.TabIndex = 0;
            this.rbDateDoc.Text = "ïî äàòå äîêóìåíòà";
            this.rbDateDoc.UseVisualStyleBackColor = true;
            // 
            // DocsRegistryParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 592);
            this.Controls.Add(this.gbSort);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ucTypesDocument);
            this.Controls.Add(this.ucGoods);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkLbShowParams);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkLbDocStatå);
            this.Controls.Add(this.ucPeriod);
            this.Name = "DocsRegistryParams";
            this.Load += new System.EventHandler(this.DocsRegistryParams_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.ucPeriod, 0);
            this.Controls.SetChildIndex(this.chkLbDocStatå, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.chkLbShowParams, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.toolStrip1, 0);
            this.Controls.SetChildIndex(this.ucGoods, 0);
            this.Controls.SetChildIndex(this.ucTypesDocument, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.gbSort, 0);
            this.panel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.gbSort.ResumeLayout(false);
            this.gbSort.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckedListBox chkLbShowParams;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckedListBox chkLbDocStatå;
		private ePlus.MetaData.Client.UCPeriod ucPeriod;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucGoods;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucContractorFrom;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucStoreFrom;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucTypesDocument;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucContractorTo;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucStoreTo;
        private System.Windows.Forms.GroupBox gbSort;
        private System.Windows.Forms.RadioButton rbTypeDoc;
        private System.Windows.Forms.RadioButton rbDateDoc;
	}
}