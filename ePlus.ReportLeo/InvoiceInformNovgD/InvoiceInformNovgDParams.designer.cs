namespace InvoiceInformNovgD
{
	partial class InvoiceInformNosologParams
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.label3 = new System.Windows.Forms.Label();
            this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
            this.sortLabel = new System.Windows.Forms.Label();
            this.sortComboBox = new System.Windows.Forms.ComboBox();
            this.ContractLbl = new System.Windows.Forms.Label();
            this.ucContractorTo = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.ucContracts = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
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
            this.panel1.Location = new System.Drawing.Point(0, 281);
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
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(60, 22);
            this.toolStripButton1.Text = "Очистить";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 173;
            this.label3.Text = "Период:";
            // 
            // ucPeriod
            // 
            this.ucPeriod.DateFrom = new System.DateTime(2006, 10, 30, 14, 2, 0, 906);
            this.ucPeriod.DateTo = new System.DateTime(2006, 10, 30, 14, 2, 0, 921);
            this.ucPeriod.Location = new System.Drawing.Point(87, 32);
            this.ucPeriod.Name = "ucPeriod";
            this.ucPeriod.Size = new System.Drawing.Size(247, 21);
            this.ucPeriod.TabIndex = 167;
            // 
            // sortLabel
            // 
            this.sortLabel.AutoSize = true;
            this.sortLabel.Location = new System.Drawing.Point(15, 264);
            this.sortLabel.Name = "sortLabel";
            this.sortLabel.Size = new System.Drawing.Size(70, 13);
            this.sortLabel.TabIndex = 183;
            this.sortLabel.Text = "Сортировка:";
            // 
            // sortComboBox
            // 
            this.sortComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sortComboBox.FormattingEnabled = true;
            this.sortComboBox.Items.AddRange(new object[] {
            "По МНН",
            "По ТН"});
            this.sortComboBox.Location = new System.Drawing.Point(87, 261);
            this.sortComboBox.Name = "sortComboBox";
            this.sortComboBox.Size = new System.Drawing.Size(270, 21);
            this.sortComboBox.TabIndex = 184;
            // 
            // ContractLbl
            // 
            this.ContractLbl.AutoSize = true;
            this.ContractLbl.Location = new System.Drawing.Point(15, 161);
            this.ContractLbl.Name = "ContractLbl";
            this.ContractLbl.Size = new System.Drawing.Size(0, 13);
            this.ContractLbl.TabIndex = 173;
            // 
            // ucContractorTo
            // 
            this.ucContractorTo.AllowSaveState = false;
            this.ucContractorTo.Caption = "Контрагент(ы)";
            this.ucContractorTo.Location = new System.Drawing.Point(12, 59);
            this.ucContractorTo.Mnemocode = "CONTRACTOR";
            this.ucContractorTo.Name = "ucContractorTo";
            this.ucContractorTo.Size = new System.Drawing.Size(647, 87);
            this.ucContractorTo.TabIndex = 177;
            // 
            // ucContracts
            // 
            this.ucContracts.AllowSaveState = false;
            this.ucContracts.Caption = "Договор(ы)";
            this.ucContracts.Location = new System.Drawing.Point(12, 152);
            this.ucContracts.Mnemocode = "CONTRACTS";
            this.ucContracts.Name = "ucContracts";
            this.ucContracts.Size = new System.Drawing.Size(647, 99);
            this.ucContracts.TabIndex = 185;
            // 
            // InvoiceInformNosologParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 310);
            this.Controls.Add(this.ucContracts);
            this.Controls.Add(this.ucContractorTo);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.sortComboBox);
            this.Controls.Add(this.ContractLbl);
            this.Controls.Add(this.sortLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ucPeriod);
            this.Name = "InvoiceInformNosologParams";
            this.Controls.SetChildIndex(this.ucPeriod, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.sortLabel, 0);
            this.Controls.SetChildIndex(this.ContractLbl, 0);
            this.Controls.SetChildIndex(this.sortComboBox, 0);
            this.Controls.SetChildIndex(this.toolStrip1, 0);
            this.Controls.SetChildIndex(this.ucContractorTo, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.ucContracts, 0);
            this.panel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.Label label3;
        private ePlus.MetaData.Client.UCPeriod ucPeriod;
		private System.Windows.Forms.Label sortLabel;
        private System.Windows.Forms.ComboBox sortComboBox;
        private System.Windows.Forms.Label ContractLbl;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucContractorTo;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucContracts;
	}
}