namespace ContractorSettlementEx
{
	partial class ContractorSettlementParams
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContractorSettlementParams));
      this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
      this.label1 = new System.Windows.Forms.Label();
      this.ucContractor = new ePlus.MetaData.Client.UCMetaPluginSelect();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
      this.label2 = new System.Windows.Forms.Label();
      this.viewComboBox = new System.Windows.Forms.ComboBox();
      this.label4 = new System.Windows.Forms.Label();
      this.wayCheckBox = new System.Windows.Forms.CheckBox();
      this.contractorsListBox = new System.Windows.Forms.ListBox();
      this.shortCheckBox = new System.Windows.Forms.CheckBox();
      this.auCheckBox = new System.Windows.Forms.CheckBox();
      this.panel1.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // bOK
      // 
      this.bOK.Location = new System.Drawing.Point(226, 3);
      // 
      // bClose
      // 
      this.bClose.Location = new System.Drawing.Point(301, 3);
      // 
      // panel1
      // 
      this.panel1.Location = new System.Drawing.Point(0, 386);
      this.panel1.Size = new System.Drawing.Size(379, 29);
      // 
      // ucPeriod
      // 
      this.ucPeriod.DateFrom = new System.DateTime(2008, 10, 15, 14, 28, 29, 15);
      this.ucPeriod.DateTo = new System.DateTime(2008, 10, 15, 14, 28, 29, 15);
      this.ucPeriod.Location = new System.Drawing.Point(87, 34);
      this.ucPeriod.Name = "ucPeriod";
      this.ucPeriod.Size = new System.Drawing.Size(222, 21);
      this.ucPeriod.TabIndex = 4;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 37);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(48, 13);
      this.label1.TabIndex = 6;
      this.label1.Text = "Период:";
      // 
      // ucContractor
      // 
      this.ucContractor.ButtonStyle = ePlus.MetaData.Client.EButtonStyle.SelectClear;
      this.ucContractor.Location = new System.Drawing.Point(87, 64);
      this.ucContractor.Mnemocode = "CONTRACTOR";
      this.ucContractor.Name = "ucContractor";
      this.ucContractor.Size = new System.Drawing.Size(262, 21);
      this.ucContractor.TabIndex = 7;
      // 
      // toolStrip1
      // 
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
      this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      this.toolStrip1.Size = new System.Drawing.Size(379, 25);
      this.toolStrip1.TabIndex = 8;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // toolStripButton1
      // 
      this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
      this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton1.Name = "toolStripButton1";
      this.toolStripButton1.Size = new System.Drawing.Size(76, 22);
      this.toolStripButton1.Text = "Очистить";
      this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(12, 67);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(68, 13);
      this.label2.TabIndex = 9;
      this.label2.Text = "Контрагент:";
      // 
      // viewComboBox
      // 
      this.viewComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.viewComboBox.FormattingEnabled = true;
      this.viewComboBox.Items.AddRange(new object[] {
            "Поставщики",
            "Аптеки",
            "Покупатели",
            "Поставщики/Покупатели"});
      this.viewComboBox.Location = new System.Drawing.Point(87, 96);
      this.viewComboBox.Name = "viewComboBox";
      this.viewComboBox.Size = new System.Drawing.Size(262, 21);
      this.viewComboBox.TabIndex = 11;
      this.viewComboBox.SelectedIndexChanged += new System.EventHandler(this.viewComboBox_SelectedIndexChanged);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(12, 99);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(65, 13);
      this.label4.TabIndex = 13;
      this.label4.Text = "Вид отчета:";
      // 
      // wayCheckBox
      // 
      this.wayCheckBox.AutoSize = true;
      this.wayCheckBox.Location = new System.Drawing.Point(15, 308);
      this.wayCheckBox.Name = "wayCheckBox";
      this.wayCheckBox.Size = new System.Drawing.Size(147, 17);
      this.wayCheckBox.TabIndex = 15;
      this.wayCheckBox.Text = "Учитывать товар в пути";
      this.wayCheckBox.UseVisualStyleBackColor = true;
      // 
      // contractorsListBox
      // 
      this.contractorsListBox.FormattingEnabled = true;
      this.contractorsListBox.Location = new System.Drawing.Point(12, 123);
      this.contractorsListBox.Name = "contractorsListBox";
      this.contractorsListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.contractorsListBox.Size = new System.Drawing.Size(333, 173);
      this.contractorsListBox.TabIndex = 16;
      // 
      // shortCheckBox
      // 
      this.shortCheckBox.AutoSize = true;
      this.shortCheckBox.Location = new System.Drawing.Point(15, 331);
      this.shortCheckBox.Name = "shortCheckBox";
      this.shortCheckBox.Size = new System.Drawing.Size(68, 17);
      this.shortCheckBox.TabIndex = 17;
      this.shortCheckBox.Text = "Краткий";
      this.shortCheckBox.UseVisualStyleBackColor = true;
      // 
      // auCheckBox
      // 
      this.auCheckBox.AutoSize = true;
      this.auCheckBox.Location = new System.Drawing.Point(15, 354);
      this.auCheckBox.Name = "auCheckBox";
      this.auCheckBox.Size = new System.Drawing.Size(234, 17);
      this.auCheckBox.TabIndex = 18;
      this.auCheckBox.Text = "Отфильтровать перемещения внутри АУ";
      this.auCheckBox.UseVisualStyleBackColor = true;
      // 
      // ContractorSettlementParams
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(379, 415);
      this.Controls.Add(this.auCheckBox);
      this.Controls.Add(this.shortCheckBox);
      this.Controls.Add(this.contractorsListBox);
      this.Controls.Add(this.wayCheckBox);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.viewComboBox);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.toolStrip1);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.ucPeriod);
      this.Controls.Add(this.ucContractor);
      this.MinimumSize = new System.Drawing.Size(380, 397);
      this.Name = "ContractorSettlementParams";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ContractorSettlementParams_FormClosed);
      this.Load += new System.EventHandler(this.ContractorSettlementParams_Load);
      this.Controls.SetChildIndex(this.ucContractor, 0);
      this.Controls.SetChildIndex(this.ucPeriod, 0);
      this.Controls.SetChildIndex(this.panel1, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.toolStrip1, 0);
      this.Controls.SetChildIndex(this.label2, 0);
      this.Controls.SetChildIndex(this.viewComboBox, 0);
      this.Controls.SetChildIndex(this.label4, 0);
      this.Controls.SetChildIndex(this.wayCheckBox, 0);
      this.Controls.SetChildIndex(this.contractorsListBox, 0);
      this.Controls.SetChildIndex(this.shortCheckBox, 0);
      this.Controls.SetChildIndex(this.auCheckBox, 0);
      this.panel1.ResumeLayout(false);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

		}

		#endregion

		private ePlus.MetaData.Client.UCPeriod ucPeriod;
		private System.Windows.Forms.Label label1;
		private ePlus.MetaData.Client.UCMetaPluginSelect ucContractor;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox viewComboBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.CheckBox wayCheckBox;
		private System.Windows.Forms.ListBox contractorsListBox;
		private System.Windows.Forms.CheckBox shortCheckBox;
		private System.Windows.Forms.CheckBox auCheckBox;
	}
}