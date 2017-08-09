namespace RCKAccountDataReserve
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
			this.ucPluginMulti_Contractor = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.ucPluginMulti_Store = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.period = new ePlus.MetaData.Client.UCPeriod();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
			this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.panel1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// bOK
			// 
			this.bOK.Location = new System.Drawing.Point(271, 3);
			// 
			// bClose
			// 
			this.bClose.Location = new System.Drawing.Point(346, 3);
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(0, 366);
			this.panel1.Size = new System.Drawing.Size(424, 29);
			// 
			// ucPluginMulti_Contractor
			// 
			this.ucPluginMulti_Contractor.AllowSaveState = true;
			this.ucPluginMulti_Contractor.Caption = "Покупатели";
			this.ucPluginMulti_Contractor.Location = new System.Drawing.Point(9, 196);
			this.ucPluginMulti_Contractor.Mnemocode = "CONTRACTOR";
			this.ucPluginMulti_Contractor.Name = "ucPluginMulti_Contractor";
			this.ucPluginMulti_Contractor.Size = new System.Drawing.Size(401, 73);
			this.ucPluginMulti_Contractor.TabIndex = 147;
			// 
			// ucPluginMulti_Store
			// 
			this.ucPluginMulti_Store.AllowSaveState = false;
			this.ucPluginMulti_Store.Caption = "Склады";
			this.ucPluginMulti_Store.Location = new System.Drawing.Point(9, 275);
			this.ucPluginMulti_Store.Mnemocode = "STORE";
			this.ucPluginMulti_Store.Name = "ucPluginMulti_Store";
			this.ucPluginMulti_Store.Size = new System.Drawing.Size(401, 73);
			this.ucPluginMulti_Store.TabIndex = 146;
			// 
			// period
			// 
			this.period.DateFrom = new System.DateTime(2006, 10, 30, 14, 2, 0, 906);
			this.period.DateTo = new System.DateTime(2006, 10, 30, 14, 2, 0, 921);
			this.period.Location = new System.Drawing.Point(68, 38);
			this.period.Name = "period";
			this.period.Size = new System.Drawing.Size(229, 21);
			this.period.TabIndex = 152;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(14, 42);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 13);
			this.label1.TabIndex = 151;
			this.label1.Text = "Период:";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.dateTimePicker2);
			this.groupBox1.Controls.Add(this.dateTimePicker1);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.comboBox1);
			this.groupBox1.Location = new System.Drawing.Point(17, 80);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(250, 110);
			this.groupBox1.TabIndex = 153;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Дата окончания срока резервирования";
			// 
			// dateTimePicker2
			// 
			this.dateTimePicker2.Enabled = false;
			this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePicker2.Location = new System.Drawing.Point(107, 72);
			this.dateTimePicker2.Name = "dateTimePicker2";
			this.dateTimePicker2.Size = new System.Drawing.Size(89, 20);
			this.dateTimePicker2.TabIndex = 4;
			this.dateTimePicker2.ValueChanged += new System.EventHandler(this.comboBox1_SelectedValueChanged);
			// 
			// dateTimePicker1
			// 
			this.dateTimePicker1.CustomFormat = "dd.mm.yyyy";
			this.dateTimePicker1.Enabled = false;
			this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePicker1.Location = new System.Drawing.Point(107, 46);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.Size = new System.Drawing.Size(89, 20);
			this.dateTimePicker1.TabIndex = 3;
			this.dateTimePicker1.ValueChanged += new System.EventHandler(this.comboBox1_SelectedValueChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 76);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(89, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Дата окончания";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(30, 50);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(71, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Дата начала";
			// 
			// comboBox1
			// 
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Items.AddRange(new object[] {
            "Больше даты начала",
            "Входит в интервал",
            "Любая дата",
            "Меньше даты начала",
            "Равна дате начала"});
			this.comboBox1.Location = new System.Drawing.Point(107, 19);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(130, 21);
			this.comboBox1.Sorted = true;
			this.comboBox1.TabIndex = 0;
			this.comboBox1.SelectedValueChanged += new System.EventHandler(this.comboBox1_SelectedValueChanged);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(424, 25);
			this.toolStrip1.TabIndex = 154;
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
			this.ClientSize = new System.Drawing.Size(424, 395);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.period);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.ucPluginMulti_Contractor);
			this.Controls.Add(this.ucPluginMulti_Store);
			this.Name = "FormParams";
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.ucPluginMulti_Store, 0);
			this.Controls.SetChildIndex(this.ucPluginMulti_Contractor, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.period, 0);
			this.Controls.SetChildIndex(this.groupBox1, 0);
			this.Controls.SetChildIndex(this.toolStrip1, 0);
			this.panel1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private ePlus.MetaData.Client.UCPluginMultiSelect ucPluginMulti_Contractor;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucPluginMulti_Store;
		private ePlus.MetaData.Client.UCPeriod period;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.DateTimePicker dateTimePicker2;
		private System.Windows.Forms.DateTimePicker dateTimePicker1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}