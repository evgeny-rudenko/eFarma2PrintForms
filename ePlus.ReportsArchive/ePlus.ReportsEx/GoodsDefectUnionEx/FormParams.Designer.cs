namespace GoodsDefectUnionEx
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
			this.ucPluginMulti_Contractor = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.ucPluginMulti_Store = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.rbSortSeries = new System.Windows.Forms.RadioButton();
			this.rbSortName = new System.Windows.Forms.RadioButton();
			this.ucPluginMulti_Invoice = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.checkBoxRemains = new System.Windows.Forms.CheckBox();
			this.period = new ePlus.MetaData.Client.UCPeriod();
			this.label1 = new System.Windows.Forms.Label();
			this.chbGoodCode = new System.Windows.Forms.CheckBox();
			this.panel1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// bOK
			// 
			this.bOK.Location = new System.Drawing.Point(270, 3);
			// 
			// bClose
			// 
			this.bClose.Location = new System.Drawing.Point(345, 3);
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(0, 354);
			this.panel1.Size = new System.Drawing.Size(423, 29);
			// 
			// ucPluginMulti_Contractor
			// 
			this.ucPluginMulti_Contractor.AllowSaveState = false;
			this.ucPluginMulti_Contractor.Caption = null;
			this.ucPluginMulti_Contractor.Location = new System.Drawing.Point(12, 39);
			this.ucPluginMulti_Contractor.Mnemocode = "CONTRACTOR";
			this.ucPluginMulti_Contractor.Name = "ucPluginMulti_Contractor";
			this.ucPluginMulti_Contractor.Size = new System.Drawing.Size(401, 73);
			this.ucPluginMulti_Contractor.TabIndex = 147;
			// 
			// ucPluginMulti_Store
			// 
			this.ucPluginMulti_Store.AllowSaveState = false;
			this.ucPluginMulti_Store.Caption = "Склады";
			this.ucPluginMulti_Store.Location = new System.Drawing.Point(12, 118);
			this.ucPluginMulti_Store.Mnemocode = "STORE";
			this.ucPluginMulti_Store.Name = "ucPluginMulti_Store";
			this.ucPluginMulti_Store.Size = new System.Drawing.Size(401, 73);
			this.ucPluginMulti_Store.TabIndex = 146;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.rbSortSeries);
			this.groupBox1.Controls.Add(this.rbSortName);
			this.groupBox1.Location = new System.Drawing.Point(12, 299);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(236, 51);
			this.groupBox1.TabIndex = 148;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Сортировать:";
			// 
			// rbSortSeries
			// 
			this.rbSortSeries.AutoSize = true;
			this.rbSortSeries.Location = new System.Drawing.Point(116, 20);
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
			this.rbSortName.Location = new System.Drawing.Point(6, 19);
			this.rbSortName.Name = "rbSortName";
			this.rbSortName.Size = new System.Drawing.Size(88, 17);
			this.rbSortName.TabIndex = 0;
			this.rbSortName.TabStop = true;
			this.rbSortName.Text = "по алфавиту";
			this.rbSortName.UseVisualStyleBackColor = true;
			// 
			// ucPluginMulti_Invoice
			// 
			this.ucPluginMulti_Invoice.AllowSaveState = false;
			this.ucPluginMulti_Invoice.Caption = "Приходные накладные";
			this.ucPluginMulti_Invoice.Location = new System.Drawing.Point(12, 197);
			this.ucPluginMulti_Invoice.Mnemocode = "INVOICE";
			this.ucPluginMulti_Invoice.Name = "ucPluginMulti_Invoice";
			this.ucPluginMulti_Invoice.Size = new System.Drawing.Size(401, 73);
			this.ucPluginMulti_Invoice.TabIndex = 149;
			// 
			// checkBoxRemains
			// 
			this.checkBoxRemains.AutoSize = true;
			this.checkBoxRemains.Location = new System.Drawing.Point(12, 276);
			this.checkBoxRemains.Name = "checkBoxRemains";
			this.checkBoxRemains.Size = new System.Drawing.Size(68, 17);
			this.checkBoxRemains.TabIndex = 150;
			this.checkBoxRemains.Text = "Остатки";
			this.checkBoxRemains.UseVisualStyleBackColor = true;
			// 
			// period
			// 
			this.period.DateFrom = new System.DateTime(2006, 10, 30, 14, 2, 0, 906);
			this.period.DateTo = new System.DateTime(2006, 10, 30, 14, 2, 0, 921);
			this.period.Location = new System.Drawing.Point(73, 12);
			this.period.Name = "period";
			this.period.Size = new System.Drawing.Size(229, 21);
			this.period.TabIndex = 152;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(19, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 13);
			this.label1.TabIndex = 151;
			this.label1.Text = "Период:";
			// 
			// chbGoodCode
			// 
			this.chbGoodCode.AutoSize = true;
			this.chbGoodCode.Location = new System.Drawing.Point(88, 276);
			this.chbGoodCode.Name = "chbGoodCode";
			this.chbGoodCode.Size = new System.Drawing.Size(150, 17);
			this.chbGoodCode.TabIndex = 153;
			this.chbGoodCode.Text = "Отображать код товара ";
			this.chbGoodCode.UseVisualStyleBackColor = true;
			// 
			// FormParams
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(423, 383);
			this.Controls.Add(this.chbGoodCode);
			this.Controls.Add(this.period);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.checkBoxRemains);
			this.Controls.Add(this.ucPluginMulti_Invoice);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.ucPluginMulti_Contractor);
			this.Controls.Add(this.ucPluginMulti_Store);
			this.Name = "FormParams";
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.ucPluginMulti_Store, 0);
			this.Controls.SetChildIndex(this.ucPluginMulti_Contractor, 0);
			this.Controls.SetChildIndex(this.groupBox1, 0);
			this.Controls.SetChildIndex(this.ucPluginMulti_Invoice, 0);
			this.Controls.SetChildIndex(this.checkBoxRemains, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.period, 0);
			this.Controls.SetChildIndex(this.chbGoodCode, 0);
			this.panel1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
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
		private System.Windows.Forms.CheckBox checkBoxRemains;
		private ePlus.MetaData.Client.UCPeriod period;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox chbGoodCode;
    }
}