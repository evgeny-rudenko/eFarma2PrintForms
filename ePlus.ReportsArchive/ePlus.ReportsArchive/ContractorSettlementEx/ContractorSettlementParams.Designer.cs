namespace RCRContractorSettlement
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
			this.period = new ePlus.MetaData.Client.UCPeriod();
			this.label1 = new System.Windows.Forms.Label();
			this.contractor = new ePlus.MetaData.Client.UCMetaPluginSelect();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.label2 = new System.Windows.Forms.Label();
			this.ucFilter = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.cbFilter = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.cbReportType = new System.Windows.Forms.ComboBox();
			this.chkShowOnTheWay = new System.Windows.Forms.CheckBox();
			this.panel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// bOK
			// 
			this.bOK.Location = new System.Drawing.Point(258, 3);
			// 
			// bClose
			// 
			this.bClose.Location = new System.Drawing.Point(333, 3);
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(0, 320);
			this.panel1.Size = new System.Drawing.Size(411, 29);
			// 
			// period
			// 
			this.period.DateFrom = new System.DateTime(2008, 10, 15, 14, 28, 29, 15);
			this.period.DateTo = new System.DateTime(2008, 10, 15, 14, 28, 29, 15);
			this.period.Location = new System.Drawing.Point(104, 34);
			this.period.Name = "period";
			this.period.Size = new System.Drawing.Size(222, 21);
			this.period.TabIndex = 4;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(29, 37);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(45, 13);
			this.label1.TabIndex = 6;
			this.label1.Text = "Период";
			// 
			// contractor
			// 
			this.contractor.ButtonStyle = ePlus.MetaData.Client.EButtonStyle.SelectClear;
			this.contractor.Location = new System.Drawing.Point(104, 64);
			this.contractor.Mnemocode = "CONTRACTOR";
			this.contractor.Name = "contractor";
			this.contractor.Size = new System.Drawing.Size(262, 21);
			this.contractor.TabIndex = 7;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
			this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(411, 25);
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
			this.label2.Location = new System.Drawing.Point(29, 67);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(65, 13);
			this.label2.TabIndex = 9;
			this.label2.Text = "Контрагент";
			// 
			// ucFilter
			// 
			this.ucFilter.AllowSaveState = false;
			this.ucFilter.Caption = "";
			this.ucFilter.Location = new System.Drawing.Point(32, 167);
			this.ucFilter.Mnemocode = "CONTRACTOR";
			this.ucFilter.Name = "ucFilter";
			this.ucFilter.Size = new System.Drawing.Size(334, 103);
			this.ucFilter.TabIndex = 10;
			// 
			// cbFilter
			// 
			this.cbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbFilter.FormattingEnabled = true;
			this.cbFilter.Location = new System.Drawing.Point(104, 96);
			this.cbFilter.Name = "cbFilter";
			this.cbFilter.Size = new System.Drawing.Size(262, 21);
			this.cbFilter.TabIndex = 11;
			this.cbFilter.SelectedIndexChanged += new System.EventHandler(this.cbFilter_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(31, 100);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(47, 13);
			this.label3.TabIndex = 12;
			this.label3.Text = "Фильтр";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(32, 134);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(62, 13);
			this.label4.TabIndex = 13;
			this.label4.Text = "Вид отчета";
			// 
			// cbReportType
			// 
			this.cbReportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbReportType.FormattingEnabled = true;
			this.cbReportType.Location = new System.Drawing.Point(104, 130);
			this.cbReportType.Name = "cbReportType";
			this.cbReportType.Size = new System.Drawing.Size(262, 21);
			this.cbReportType.TabIndex = 14;
			// 
			// chkShowOnTheWay
			// 
			this.chkShowOnTheWay.AutoSize = true;
			this.chkShowOnTheWay.Location = new System.Drawing.Point(36, 276);
			this.chkShowOnTheWay.Name = "chkShowOnTheWay";
			this.chkShowOnTheWay.Size = new System.Drawing.Size(147, 17);
			this.chkShowOnTheWay.TabIndex = 15;
			this.chkShowOnTheWay.Text = "Учитывать товар в пути";
			this.chkShowOnTheWay.UseVisualStyleBackColor = true;
			// 
			// ContractorSettlementParams
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(411, 349);
			this.Controls.Add(this.chkShowOnTheWay);
			this.Controls.Add(this.cbReportType);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.cbFilter);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.period);
			this.Controls.Add(this.contractor);
			this.Controls.Add(this.ucFilter);
			this.Name = "ContractorSettlementParams";
			this.Load += new System.EventHandler(this.ContractorSettlementParams_Load);
			this.Controls.SetChildIndex(this.ucFilter, 0);
			this.Controls.SetChildIndex(this.contractor, 0);
			this.Controls.SetChildIndex(this.period, 0);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.toolStrip1, 0);
			this.Controls.SetChildIndex(this.label2, 0);
			this.Controls.SetChildIndex(this.cbFilter, 0);
			this.Controls.SetChildIndex(this.label3, 0);
			this.Controls.SetChildIndex(this.label4, 0);
			this.Controls.SetChildIndex(this.cbReportType, 0);
			this.Controls.SetChildIndex(this.chkShowOnTheWay, 0);
			this.panel1.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private ePlus.MetaData.Client.UCPeriod period;
		private System.Windows.Forms.Label label1;
		private ePlus.MetaData.Client.UCMetaPluginSelect contractor;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.Label label2;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucFilter;
		private System.Windows.Forms.ComboBox cbFilter;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox cbReportType;
		private System.Windows.Forms.CheckBox chkShowOnTheWay;
	}
}