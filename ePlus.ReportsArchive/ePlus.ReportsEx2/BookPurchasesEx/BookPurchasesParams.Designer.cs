namespace BookPurchasesEx
{
	partial class BookPurchasesParams
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BookPurchasesParams));
			this.lbPeriod = new System.Windows.Forms.Label();
			this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.supplierOperatorComboBox = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.ucPharmacies = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.ucSuppliers = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.panel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// bOK
			// 
			this.bOK.Location = new System.Drawing.Point(209, 3);
			// 
			// bClose
			// 
			this.bClose.Location = new System.Drawing.Point(284, 3);
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(0, 369);
			this.panel1.Size = new System.Drawing.Size(362, 29);
			// 
			// lbPeriod
			// 
			this.lbPeriod.AutoSize = true;
			this.lbPeriod.Location = new System.Drawing.Point(12, 38);
			this.lbPeriod.Name = "lbPeriod";
			this.lbPeriod.Size = new System.Drawing.Size(48, 13);
			this.lbPeriod.TabIndex = 6;
			this.lbPeriod.Text = "Период:";
			// 
			// ucPeriod
			// 
			this.ucPeriod.DateFrom = new System.DateTime(2006, 10, 30, 14, 2, 0, 906);
			this.ucPeriod.DateTo = new System.DateTime(2006, 10, 30, 14, 2, 0, 921);
			this.ucPeriod.Location = new System.Drawing.Point(60, 36);
			this.ucPeriod.Name = "ucPeriod";
			this.ucPeriod.Size = new System.Drawing.Size(253, 21);
			this.ucPeriod.TabIndex = 5;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
			this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(362, 25);
			this.toolStrip1.TabIndex = 10;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.Image = ((System.Drawing.Image) (resources.GetObject("toolStripButton1.Image")));
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(76, 22);
			this.toolStripButton1.Text = "Очистить";
			this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
			// 
			// supplierOperatorComboBox
			// 
			this.supplierOperatorComboBox.FormattingEnabled = true;
			this.supplierOperatorComboBox.Items.AddRange(new object[] {
            "по всем",
            "один из",
            "все кроме"});
			this.supplierOperatorComboBox.Location = new System.Drawing.Point(114, 192);
			this.supplierOperatorComboBox.Name = "supplierOperatorComboBox";
			this.supplierOperatorComboBox.Size = new System.Drawing.Size(199, 21);
			this.supplierOperatorComboBox.TabIndex = 21;
			this.supplierOperatorComboBox.SelectedIndexChanged += new System.EventHandler(this.supplierOperatorComboBox_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 195);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(93, 13);
			this.label3.TabIndex = 22;
			this.label3.Text = "Условие поиска:";
			// 
			// ucPharmacies
			// 
			this.ucPharmacies.AllowSaveState = true;
			this.ucPharmacies.Caption = "Аптеки";
			this.ucPharmacies.Location = new System.Drawing.Point(12, 63);
			this.ucPharmacies.Mnemocode = "CONTRACTOR";
			this.ucPharmacies.Name = "ucPharmacies";
			this.ucPharmacies.Size = new System.Drawing.Size(333, 123);
			this.ucPharmacies.TabIndex = 23;
			// 
			// ucSuppliers
			// 
			this.ucSuppliers.AllowSaveState = true;
			this.ucSuppliers.Caption = "Поставщики";
			this.ucSuppliers.Enabled = false;
			this.ucSuppliers.Location = new System.Drawing.Point(12, 219);
			this.ucSuppliers.Mnemocode = "CONTRACTOR";
			this.ucSuppliers.Name = "ucSuppliers";
			this.ucSuppliers.Size = new System.Drawing.Size(333, 123);
			this.ucSuppliers.TabIndex = 24;
			// 
			// BookPurchasesParams
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.ClientSize = new System.Drawing.Size(362, 398);
			this.Controls.Add(this.ucSuppliers);
			this.Controls.Add(this.ucPharmacies);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.supplierOperatorComboBox);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.lbPeriod);
			this.Controls.Add(this.ucPeriod);
			this.Name = "BookPurchasesParams";
			this.Load += new System.EventHandler(this.BookPurchasesParams_Load);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BookPurchasesParams_FormClosed);
			this.Controls.SetChildIndex(this.ucPeriod, 0);
			this.Controls.SetChildIndex(this.lbPeriod, 0);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.toolStrip1, 0);
			this.Controls.SetChildIndex(this.supplierOperatorComboBox, 0);
			this.Controls.SetChildIndex(this.label3, 0);
			this.Controls.SetChildIndex(this.ucPharmacies, 0);
			this.Controls.SetChildIndex(this.ucSuppliers, 0);
			this.panel1.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lbPeriod;
		private ePlus.MetaData.Client.UCPeriod ucPeriod;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.ComboBox supplierOperatorComboBox;
		private System.Windows.Forms.Label label3;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucPharmacies;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucSuppliers;
	}
}
