namespace KKM_Z_Report_Ex
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
          this.ucPeriod1 = new ePlus.MetaData.Client.UCPeriod();
          this.checkBox_detail = new System.Windows.Forms.CheckBox();
          this.label2 = new System.Windows.Forms.Label();
          this.label1 = new System.Windows.Forms.Label();
          this.ucMetaPluginSelect2 = new ePlus.MetaData.Client.UCMetaPluginSelect();
          this.ucMetaPluginSelect1 = new ePlus.MetaData.Client.UCMetaPluginSelect();
          this.label3 = new System.Windows.Forms.Label();
          this.toolStrip1 = new System.Windows.Forms.ToolStrip();
          this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
          this.label4 = new System.Windows.Forms.Label();
          this.reportTypeComboBox = new System.Windows.Forms.ComboBox();
          this.groupByTypeCheckBox = new System.Windows.Forms.CheckBox();
          this.panel1.SuspendLayout();
          this.toolStrip1.SuspendLayout();
          this.SuspendLayout();
          // 
          // bOK
          // 
          this.bOK.Location = new System.Drawing.Point(345, 3);
          // 
          // bClose
          // 
          this.bClose.Location = new System.Drawing.Point(420, 3);
          // 
          // panel1
          // 
          this.panel1.Location = new System.Drawing.Point(0, 213);
          this.panel1.Size = new System.Drawing.Size(498, 29);
          // 
          // ucPeriod1
          // 
          this.ucPeriod1.DateFrom = new System.DateTime(2006, 10, 30, 14, 2, 0, 906);
          this.ucPeriod1.DateTo = new System.DateTime(2006, 10, 30, 14, 2, 0, 921);
          this.ucPeriod1.Location = new System.Drawing.Point(81, 36);
          this.ucPeriod1.Name = "ucPeriod1";
          this.ucPeriod1.Size = new System.Drawing.Size(285, 21);
          this.ucPeriod1.TabIndex = 116;
          // 
          // checkBox_detail
          // 
          this.checkBox_detail.AutoSize = true;
          this.checkBox_detail.Location = new System.Drawing.Point(14, 159);
          this.checkBox_detail.Name = "checkBox_detail";
          this.checkBox_detail.Size = new System.Drawing.Size(68, 17);
          this.checkBox_detail.TabIndex = 115;
          this.checkBox_detail.Text = "Краткий";
          this.checkBox_detail.UseVisualStyleBackColor = true;
          // 
          // label2
          // 
          this.label2.AutoSize = true;
          this.label2.Location = new System.Drawing.Point(11, 124);
          this.label2.Name = "label2";
          this.label2.Size = new System.Drawing.Size(68, 13);
          this.label2.TabIndex = 114;
          this.label2.Text = "Контрагент:";
          // 
          // label1
          // 
          this.label1.AutoSize = true;
          this.label1.Location = new System.Drawing.Point(11, 96);
          this.label1.Name = "label1";
          this.label1.Size = new System.Drawing.Size(33, 13);
          this.label1.TabIndex = 113;
          this.label1.Text = "ККМ:";
          // 
          // ucMetaPluginSelect2
          // 
          this.ucMetaPluginSelect2.ButtonStyle = ePlus.MetaData.Client.EButtonStyle.SelectClear;
          this.ucMetaPluginSelect2.Location = new System.Drawing.Point(81, 121);
          this.ucMetaPluginSelect2.Mnemocode = "CONTRACTOR";
          this.ucMetaPluginSelect2.Name = "ucMetaPluginSelect2";
          this.ucMetaPluginSelect2.Size = new System.Drawing.Size(318, 22);
          this.ucMetaPluginSelect2.TabIndex = 112;
          // 
          // ucMetaPluginSelect1
          // 
          this.ucMetaPluginSelect1.ButtonStyle = ePlus.MetaData.Client.EButtonStyle.SelectClear;
          this.ucMetaPluginSelect1.Location = new System.Drawing.Point(81, 93);
          this.ucMetaPluginSelect1.Mnemocode = "CASH_REGISTER";
          this.ucMetaPluginSelect1.Name = "ucMetaPluginSelect1";
          this.ucMetaPluginSelect1.Size = new System.Drawing.Size(318, 22);
          this.ucMetaPluginSelect1.TabIndex = 111;
          // 
          // label3
          // 
          this.label3.AutoSize = true;
          this.label3.Location = new System.Drawing.Point(11, 39);
          this.label3.Name = "label3";
          this.label3.Size = new System.Drawing.Size(48, 13);
          this.label3.TabIndex = 117;
          this.label3.Text = "Период:";
          // 
          // toolStrip1
          // 
          this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
          this.toolStrip1.Location = new System.Drawing.Point(0, 0);
          this.toolStrip1.Name = "toolStrip1";
          this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
          this.toolStrip1.Size = new System.Drawing.Size(498, 25);
          this.toolStrip1.TabIndex = 118;
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
          // label4
          // 
          this.label4.AutoSize = true;
          this.label4.Location = new System.Drawing.Point(11, 68);
          this.label4.Name = "label4";
          this.label4.Size = new System.Drawing.Size(65, 13);
          this.label4.TabIndex = 119;
          this.label4.Text = "Вид отчета:";
          // 
          // reportTypeComboBox
          // 
          this.reportTypeComboBox.FormattingEnabled = true;
          this.reportTypeComboBox.Items.AddRange(new object[] {
            "Базовый",
            "По типу оплаты"});
          this.reportTypeComboBox.Location = new System.Drawing.Point(83, 65);
          this.reportTypeComboBox.Name = "reportTypeComboBox";
          this.reportTypeComboBox.Size = new System.Drawing.Size(220, 21);
          this.reportTypeComboBox.TabIndex = 120;
          this.reportTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.reportTypeComboBox_SelectedIndexChanged);
          // 
          // groupByTypeCheckBox
          // 
          this.groupByTypeCheckBox.AutoSize = true;
          this.groupByTypeCheckBox.Location = new System.Drawing.Point(14, 182);
          this.groupByTypeCheckBox.Name = "groupByTypeCheckBox";
          this.groupByTypeCheckBox.Size = new System.Drawing.Size(214, 17);
          this.groupByTypeCheckBox.TabIndex = 121;
          this.groupByTypeCheckBox.Text = "Разбивать по типам платёжных карт";
          this.groupByTypeCheckBox.UseVisualStyleBackColor = true;
          // 
          // FormParams
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.ClientSize = new System.Drawing.Size(498, 242);
          this.Controls.Add(this.groupByTypeCheckBox);
          this.Controls.Add(this.reportTypeComboBox);
          this.Controls.Add(this.label4);
          this.Controls.Add(this.toolStrip1);
          this.Controls.Add(this.ucPeriod1);
          this.Controls.Add(this.label3);
          this.Controls.Add(this.ucMetaPluginSelect1);
          this.Controls.Add(this.checkBox_detail);
          this.Controls.Add(this.label2);
          this.Controls.Add(this.label1);
          this.Controls.Add(this.ucMetaPluginSelect2);
          this.Name = "FormParams";
          this.Load += new System.EventHandler(this.FormParams_Load);
          this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormParams_FormClosed);
          this.Controls.SetChildIndex(this.ucMetaPluginSelect2, 0);
          this.Controls.SetChildIndex(this.label1, 0);
          this.Controls.SetChildIndex(this.label2, 0);
          this.Controls.SetChildIndex(this.checkBox_detail, 0);
          this.Controls.SetChildIndex(this.ucMetaPluginSelect1, 0);
          this.Controls.SetChildIndex(this.label3, 0);
          this.Controls.SetChildIndex(this.ucPeriod1, 0);
          this.Controls.SetChildIndex(this.panel1, 0);
          this.Controls.SetChildIndex(this.toolStrip1, 0);
          this.Controls.SetChildIndex(this.label4, 0);
          this.Controls.SetChildIndex(this.reportTypeComboBox, 0);
          this.Controls.SetChildIndex(this.groupByTypeCheckBox, 0);
          this.panel1.ResumeLayout(false);
          this.toolStrip1.ResumeLayout(false);
          this.toolStrip1.PerformLayout();
          this.ResumeLayout(false);
          this.PerformLayout();

        }

        #endregion

        private ePlus.MetaData.Client.UCPeriod ucPeriod1;
        private System.Windows.Forms.CheckBox checkBox_detail;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private ePlus.MetaData.Client.UCMetaPluginSelect ucMetaPluginSelect2;
        private ePlus.MetaData.Client.UCMetaPluginSelect ucMetaPluginSelect1;
        private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox reportTypeComboBox;
		private System.Windows.Forms.CheckBox groupByTypeCheckBox;
    }
}