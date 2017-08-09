namespace RCBGoodsDefectUnionRigla
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbSortSeries = new System.Windows.Forms.RadioButton();
            this.rbSortName = new System.Windows.Forms.RadioButton();
            this.ucPluginMulti_Invoice = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
            this.periodLabel = new System.Windows.Forms.Label();
            this.showCodeCheckBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.typeComboBox = new System.Windows.Forms.ComboBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.textSeries = new System.Windows.Forms.TextBox();
            this.numericLengthSeries = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericLengthSeries)).BeginInit();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(229, 3);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(304, 3);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 461);
            this.panel1.Size = new System.Drawing.Size(382, 29);
            // 
            // ucPluginMulti_Contractor
            // 
            this.ucPluginMulti_Contractor.AllowSaveState = true;
            this.ucPluginMulti_Contractor.Caption = "Аптеки:";
            this.ucPluginMulti_Contractor.Location = new System.Drawing.Point(11, 143);
            this.ucPluginMulti_Contractor.Mnemocode = "CONTRACTOR";
            this.ucPluginMulti_Contractor.MultiSelect = true;
            this.ucPluginMulti_Contractor.Name = "ucPluginMulti_Contractor";
            this.ucPluginMulti_Contractor.Pinnable = false;
            this.ucPluginMulti_Contractor.Size = new System.Drawing.Size(365, 73);
            this.ucPluginMulti_Contractor.TabIndex = 147;
            // 
            // ucPluginMulti_Store
            // 
            this.ucPluginMulti_Store.AllowSaveState = true;
            this.ucPluginMulti_Store.Caption = "Склады:";
            this.ucPluginMulti_Store.Location = new System.Drawing.Point(11, 222);
            this.ucPluginMulti_Store.Mnemocode = "STORE";
            this.ucPluginMulti_Store.MultiSelect = true;
            this.ucPluginMulti_Store.Name = "ucPluginMulti_Store";
            this.ucPluginMulti_Store.Pinnable = false;
            this.ucPluginMulti_Store.Size = new System.Drawing.Size(365, 73);
            this.ucPluginMulti_Store.TabIndex = 146;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbSortSeries);
            this.groupBox1.Controls.Add(this.rbSortName);
            this.groupBox1.Location = new System.Drawing.Point(12, 380);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(175, 70);
            this.groupBox1.TabIndex = 148;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Сортировать:";
            // 
            // rbSortSeries
            // 
            this.rbSortSeries.AutoSize = true;
            this.rbSortSeries.Location = new System.Drawing.Point(6, 43);
            this.rbSortSeries.Name = "rbSortSeries";
            this.rbSortSeries.Size = new System.Drawing.Size(110, 17);
            this.rbSortSeries.TabIndex = 1;
            this.rbSortSeries.Text = "по номеру серии";
            this.rbSortSeries.UseVisualStyleBackColor = true;
            // 
            // rbSortName
            // 
            this.rbSortName.AutoSize = true;
            this.rbSortName.Checked = true;
            this.rbSortName.Location = new System.Drawing.Point(6, 20);
            this.rbSortName.Name = "rbSortName";
            this.rbSortName.Size = new System.Drawing.Size(88, 17);
            this.rbSortName.TabIndex = 0;
            this.rbSortName.TabStop = true;
            this.rbSortName.Text = "по алфавиту";
            this.rbSortName.UseVisualStyleBackColor = true;
            // 
            // ucPluginMulti_Invoice
            // 
            this.ucPluginMulti_Invoice.AllowSaveState = true;
            this.ucPluginMulti_Invoice.Caption = "Приходные накладные:";
            this.ucPluginMulti_Invoice.Location = new System.Drawing.Point(11, 301);
            this.ucPluginMulti_Invoice.Mnemocode = "INVOICE";
            this.ucPluginMulti_Invoice.MultiSelect = true;
            this.ucPluginMulti_Invoice.Name = "ucPluginMulti_Invoice";
            this.ucPluginMulti_Invoice.Pinnable = false;
            this.ucPluginMulti_Invoice.Size = new System.Drawing.Size(365, 73);
            this.ucPluginMulti_Invoice.TabIndex = 149;
            // 
            // ucPeriod
            // 
            this.ucPeriod.DateFrom = new System.DateTime(2006, 10, 30, 14, 2, 0, 906);
            this.ucPeriod.DateTo = new System.DateTime(2006, 10, 30, 14, 2, 0, 921);
            this.ucPeriod.Location = new System.Drawing.Point(86, 116);
            this.ucPeriod.Name = "ucPeriod";
            this.ucPeriod.Size = new System.Drawing.Size(229, 21);
            this.ucPeriod.TabIndex = 152;
            // 
            // periodLabel
            // 
            this.periodLabel.AutoSize = true;
            this.periodLabel.Location = new System.Drawing.Point(8, 118);
            this.periodLabel.Name = "periodLabel";
            this.periodLabel.Size = new System.Drawing.Size(48, 13);
            this.periodLabel.TabIndex = 151;
            this.periodLabel.Text = "Период:";
            // 
            // showCodeCheckBox
            // 
            this.showCodeCheckBox.AutoSize = true;
            this.showCodeCheckBox.Location = new System.Drawing.Point(193, 389);
            this.showCodeCheckBox.Name = "showCodeCheckBox";
            this.showCodeCheckBox.Size = new System.Drawing.Size(150, 17);
            this.showCodeCheckBox.TabIndex = 153;
            this.showCodeCheckBox.Text = "Отображать код товара ";
            this.showCodeCheckBox.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 154;
            this.label2.Text = "Вид отчёта:";
            // 
            // typeComboBox
            // 
            this.typeComboBox.FormattingEnabled = true;
            this.typeComboBox.Items.AddRange(new object[] {
            "По остаткам",
            "По приходным накладным",
            "По периоду прихода товара"});
            this.typeComboBox.Location = new System.Drawing.Point(86, 37);
            this.typeComboBox.Name = "typeComboBox";
            this.typeComboBox.Size = new System.Drawing.Size(257, 21);
            this.typeComboBox.TabIndex = 155;
            this.typeComboBox.SelectedIndexChanged += new System.EventHandler(this.typeComboBox_SelectedIndexChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(382, 25);
            this.toolStrip1.TabIndex = 156;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(79, 22);
            this.toolStripButton1.Text = "Очистить";
            // 
            // textSeries
            // 
            this.textSeries.Location = new System.Drawing.Point(86, 64);
            this.textSeries.Name = "textSeries";
            this.textSeries.Size = new System.Drawing.Size(257, 20);
            this.textSeries.TabIndex = 157;
            // 
            // numericLengthSeries
            // 
            this.numericLengthSeries.Location = new System.Drawing.Point(183, 90);
            this.numericLengthSeries.Name = "numericLengthSeries";
            this.numericLengthSeries.Size = new System.Drawing.Size(57, 20);
            this.numericLengthSeries.TabIndex = 158;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 13);
            this.label1.TabIndex = 159;
            this.label1.Text = "Отклонение от названия серии:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 160;
            this.label3.Text = "Серия:";
            // 
            // FormParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 490);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericLengthSeries);
            this.Controls.Add(this.textSeries);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.typeComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.showCodeCheckBox);
            this.Controls.Add(this.ucPeriod);
            this.Controls.Add(this.periodLabel);
            this.Controls.Add(this.ucPluginMulti_Invoice);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ucPluginMulti_Contractor);
            this.Controls.Add(this.ucPluginMulti_Store);
            this.Name = "FormParams";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormParams_FormClosed);
            this.Load += new System.EventHandler(this.FormParams_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.ucPluginMulti_Store, 0);
            this.Controls.SetChildIndex(this.ucPluginMulti_Contractor, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.ucPluginMulti_Invoice, 0);
            this.Controls.SetChildIndex(this.periodLabel, 0);
            this.Controls.SetChildIndex(this.ucPeriod, 0);
            this.Controls.SetChildIndex(this.showCodeCheckBox, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.typeComboBox, 0);
            this.Controls.SetChildIndex(this.toolStrip1, 0);
            this.Controls.SetChildIndex(this.textSeries, 0);
            this.Controls.SetChildIndex(this.numericLengthSeries, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericLengthSeries)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ePlus.MetaData.Client.UCPluginMultiSelect ucPluginMulti_Contractor;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucPluginMulti_Store;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbSortSeries;
        private System.Windows.Forms.RadioButton rbSortName;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucPluginMulti_Invoice;
        private ePlus.MetaData.Client.UCPeriod ucPeriod;
        private System.Windows.Forms.Label periodLabel;
        private System.Windows.Forms.CheckBox showCodeCheckBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox typeComboBox;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.TextBox textSeries;
        private System.Windows.Forms.NumericUpDown numericLengthSeries;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
    }
}