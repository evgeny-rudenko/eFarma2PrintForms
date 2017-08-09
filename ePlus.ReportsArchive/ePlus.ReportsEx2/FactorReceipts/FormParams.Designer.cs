using ePlus.CommonEx.Controls;
namespace FactorReceipts
{
    partial class FormParams
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormParams));
			this.cbType = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
			this.pDay = new System.Windows.Forms.Panel();
			this.label2 = new System.Windows.Forms.Label();
			this.dtpDay = new System.Windows.Forms.DateTimePicker();
			this.label3 = new System.Windows.Forms.Label();
			this.pMonths = new System.Windows.Forms.Panel();
			this.dtpDateFr = new System.Windows.Forms.DateTimePicker();
			this.dtpDateTo = new System.Windows.Forms.DateTimePicker();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.ucContractor = new ePlus.CommonEx.Controls.MetaPluginDictionarySelectControl();
			this.ucApt = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.pPeriod = new System.Windows.Forms.Panel();
			this.yearPanel = new System.Windows.Forms.Panel();
			this.label7 = new System.Windows.Forms.Label();
			this.yearDateTimePicker = new System.Windows.Forms.DateTimePicker();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.panel1.SuspendLayout();
			this.pDay.SuspendLayout();
			this.pMonths.SuspendLayout();
			this.pPeriod.SuspendLayout();
			this.yearPanel.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// bOK
			// 
			this.bOK.Location = new System.Drawing.Point(186, 3);
			// 
			// bClose
			// 
			this.bClose.Location = new System.Drawing.Point(261, 3);
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(0, 231);
			this.panel1.Size = new System.Drawing.Size(339, 29);
			this.panel1.TabIndex = 6;
			// 
			// cbType
			// 
			this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbType.FormattingEnabled = true;
			this.cbType.Items.AddRange(new object[] {
            "по часам",
            "по дням",
            "по месяцам",
            "по месяцам (прирост)"});
			this.cbType.Location = new System.Drawing.Point(81, 37);
			this.cbType.Name = "cbType";
			this.cbType.Size = new System.Drawing.Size(243, 21);
			this.cbType.TabIndex = 1;
			this.cbType.SelectedIndexChanged += new System.EventHandler(this.cbType_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(10, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(65, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Вид отчета:";
			// 
			// ucPeriod
			// 
			this.ucPeriod.DateFrom = new System.DateTime(((long) (0)));
			this.ucPeriod.DateTo = new System.DateTime(2100, 1, 1, 0, 0, 0, 0);
			this.ucPeriod.Location = new System.Drawing.Point(71, 3);
			this.ucPeriod.Name = "ucPeriod";
			this.ucPeriod.Size = new System.Drawing.Size(233, 21);
			this.ucPeriod.TabIndex = 1;
			// 
			// pDay
			// 
			this.pDay.Controls.Add(this.label2);
			this.pDay.Controls.Add(this.dtpDay);
			this.pDay.Location = new System.Drawing.Point(10, 63);
			this.pDay.Name = "pDay";
			this.pDay.Size = new System.Drawing.Size(314, 26);
			this.pDay.TabIndex = 7;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(0, 7);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(37, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "День:";
			// 
			// dtpDay
			// 
			this.dtpDay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dtpDay.Location = new System.Drawing.Point(70, 3);
			this.dtpDay.Name = "dtpDay";
			this.dtpDay.Size = new System.Drawing.Size(139, 20);
			this.dtpDay.TabIndex = 1;
			this.dtpDay.Value = new System.DateTime(2009, 3, 5, 0, 0, 0, 0);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(0, 7);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "Период:";
			// 
			// pMonths
			// 
			this.pMonths.Controls.Add(this.dtpDateFr);
			this.pMonths.Controls.Add(this.dtpDateTo);
			this.pMonths.Controls.Add(this.label5);
			this.pMonths.Controls.Add(this.label4);
			this.pMonths.Location = new System.Drawing.Point(10, 63);
			this.pMonths.Name = "pMonths";
			this.pMonths.Size = new System.Drawing.Size(314, 25);
			this.pMonths.TabIndex = 3;
			// 
			// dtpDateFr
			// 
			this.dtpDateFr.CustomFormat = "MMMM yyyy";
			this.dtpDateFr.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpDateFr.Location = new System.Drawing.Point(70, 3);
			this.dtpDateFr.Name = "dtpDateFr";
			this.dtpDateFr.ShowUpDown = true;
			this.dtpDateFr.Size = new System.Drawing.Size(107, 20);
			this.dtpDateFr.TabIndex = 10;
			// 
			// dtpDateTo
			// 
			this.dtpDateTo.CustomFormat = "MMMM yyyy";
			this.dtpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpDateTo.Location = new System.Drawing.Point(197, 3);
			this.dtpDateTo.Name = "dtpDateTo";
			this.dtpDateTo.ShowUpDown = true;
			this.dtpDateTo.Size = new System.Drawing.Size(107, 20);
			this.dtpDateTo.TabIndex = 9;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(181, 6);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(10, 13);
			this.label5.TabIndex = 1;
			this.label5.Text = "-";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(0, 7);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(51, 13);
			this.label4.TabIndex = 0;
			this.label4.Text = "Месяцы:";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(10, 100);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(68, 13);
			this.label6.TabIndex = 4;
			this.label6.Text = "Контрагент:";
			// 
			// ucContractor
			// 
			this.ucContractor.ELikeTextOption = ePlus.MetaData.Core.ELikeTextOption.None;
			this.ucContractor.Location = new System.Drawing.Point(81, 97);
			this.ucContractor.Name = "ucContractor";
			this.ucContractor.PluginMnemocode = "CONTRACTOR";
			this.ucContractor.Size = new System.Drawing.Size(243, 20);
			this.ucContractor.TabIndex = 5;
			// 
			// ucApt
			// 
			this.ucApt.AllowSaveState = true;
			this.ucApt.Caption = "Аптеки";
			this.ucApt.Location = new System.Drawing.Point(10, 123);
			this.ucApt.Mnemocode = "CONTRACTOR";
			this.ucApt.Name = "ucApt";
			this.ucApt.Size = new System.Drawing.Size(313, 94);
			this.ucApt.TabIndex = 178;
			// 
			// pPeriod
			// 
			this.pPeriod.Controls.Add(this.label3);
			this.pPeriod.Controls.Add(this.ucPeriod);
			this.pPeriod.Location = new System.Drawing.Point(10, 63);
			this.pPeriod.Name = "pPeriod";
			this.pPeriod.Size = new System.Drawing.Size(314, 25);
			this.pPeriod.TabIndex = 2;
			// 
			// yearPanel
			// 
			this.yearPanel.Controls.Add(this.label7);
			this.yearPanel.Controls.Add(this.yearDateTimePicker);
			this.yearPanel.Location = new System.Drawing.Point(10, 63);
			this.yearPanel.Name = "yearPanel";
			this.yearPanel.Size = new System.Drawing.Size(314, 26);
			this.yearPanel.TabIndex = 179;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(0, 7);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(28, 13);
			this.label7.TabIndex = 0;
			this.label7.Text = "Год:";
			// 
			// yearDateTimePicker
			// 
			this.yearDateTimePicker.CustomFormat = "yyyy";
			this.yearDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.yearDateTimePicker.Location = new System.Drawing.Point(71, 3);
			this.yearDateTimePicker.Name = "yearDateTimePicker";
			this.yearDateTimePicker.ShowUpDown = true;
			this.yearDateTimePicker.Size = new System.Drawing.Size(64, 20);
			this.yearDateTimePicker.TabIndex = 1;
			this.yearDateTimePicker.Value = new System.DateTime(2009, 3, 5, 0, 0, 0, 0);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
			this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(339, 25);
			this.toolStrip1.TabIndex = 180;
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
			// FormParams
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(339, 260);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.yearPanel);
			this.Controls.Add(this.ucApt);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.pPeriod);
			this.Controls.Add(this.pMonths);
			this.Controls.Add(this.pDay);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cbType);
			this.Controls.Add(this.ucContractor);
			this.Name = "FormParams";
			this.Load += new System.EventHandler(this.FormParams_Load);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormParams_FormClosed);
			this.Controls.SetChildIndex(this.ucContractor, 0);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.cbType, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.pDay, 0);
			this.Controls.SetChildIndex(this.pMonths, 0);
			this.Controls.SetChildIndex(this.pPeriod, 0);
			this.Controls.SetChildIndex(this.label6, 0);
			this.Controls.SetChildIndex(this.ucApt, 0);
			this.Controls.SetChildIndex(this.yearPanel, 0);
			this.Controls.SetChildIndex(this.toolStrip1, 0);
			this.panel1.ResumeLayout(false);
			this.pDay.ResumeLayout(false);
			this.pDay.PerformLayout();
			this.pMonths.ResumeLayout(false);
			this.pMonths.PerformLayout();
			this.pPeriod.ResumeLayout(false);
			this.pPeriod.PerformLayout();
			this.yearPanel.ResumeLayout(false);
			this.yearPanel.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.Label label1;
        private ePlus.MetaData.Client.UCPeriod ucPeriod;
        private System.Windows.Forms.Panel pDay;
        private System.Windows.Forms.Label label2;
		private System.Windows.Forms.DateTimePicker dtpDay;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pMonths;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private MetaPluginDictionarySelectControl ucContractor;
        private System.Windows.Forms.DateTimePicker dtpDateTo;
        private System.Windows.Forms.DateTimePicker dtpDateFr;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucApt;
		private System.Windows.Forms.Panel pPeriod;
		private System.Windows.Forms.Panel yearPanel;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.DateTimePicker yearDateTimePicker;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}