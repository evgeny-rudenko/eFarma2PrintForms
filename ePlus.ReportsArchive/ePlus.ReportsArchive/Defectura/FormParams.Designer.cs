namespace Defectura
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
            this.toDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.fromDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.lbPeriod = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nUDDays = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbSupplier = new System.Windows.Forms.RadioButton();
            this.rbGoods = new System.Windows.Forms.RadioButton();
            this.ucGoods = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.ucStores = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chESForm = new System.Windows.Forms.CheckBox();
            this.chbCollapse = new System.Windows.Forms.CheckBox();
            this.chbOA = new System.Windows.Forms.CheckBox();
            this.tbMin = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUDDays)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(259, 3);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(334, 3);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 437);
            this.panel1.Size = new System.Drawing.Size(412, 29);
            // 
            // toDateTimePicker
            // 
            this.toDateTimePicker.CustomFormat = "dd.MM.yyyy HH:mm";
            this.toDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.toDateTimePicker.Location = new System.Drawing.Point(181, 12);
            this.toDateTimePicker.Name = "toDateTimePicker";
            this.toDateTimePicker.Size = new System.Drawing.Size(113, 20);
            this.toDateTimePicker.TabIndex = 29;
            // 
            // fromDateTimePicker
            // 
            this.fromDateTimePicker.CustomFormat = "dd.MM.yyyy HH:mm";
            this.fromDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.fromDateTimePicker.Location = new System.Drawing.Point(62, 12);
            this.fromDateTimePicker.Name = "fromDateTimePicker";
            this.fromDateTimePicker.Size = new System.Drawing.Size(113, 20);
            this.fromDateTimePicker.TabIndex = 28;
            // 
            // lbPeriod
            // 
            this.lbPeriod.AutoSize = true;
            this.lbPeriod.Location = new System.Drawing.Point(12, 16);
            this.lbPeriod.Name = "lbPeriod";
            this.lbPeriod.Size = new System.Drawing.Size(45, 13);
            this.lbPeriod.TabIndex = 27;
            this.lbPeriod.Text = "Период";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "Минимальный остаток:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "Количество дней";
            // 
            // nUDDays
            // 
            this.nUDDays.Location = new System.Drawing.Point(174, 77);
            this.nUDDays.Name = "nUDDays";
            this.nUDDays.Size = new System.Drawing.Size(120, 20);
            this.nUDDays.TabIndex = 35;
            this.nUDDays.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbSupplier);
            this.groupBox1.Controls.Add(this.rbGoods);
            this.groupBox1.Location = new System.Drawing.Point(18, 303);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(176, 46);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Сортировка";
            // 
            // rbSupplier
            // 
            this.rbSupplier.AutoSize = true;
            this.rbSupplier.Location = new System.Drawing.Point(77, 21);
            this.rbSupplier.Name = "rbSupplier";
            this.rbSupplier.Size = new System.Drawing.Size(83, 17);
            this.rbSupplier.TabIndex = 1;
            this.rbSupplier.TabStop = true;
            this.rbSupplier.Text = "Поставщик";
            this.rbSupplier.UseVisualStyleBackColor = true;
            // 
            // rbGoods
            // 
            this.rbGoods.AutoSize = true;
            this.rbGoods.Checked = true;
            this.rbGoods.Location = new System.Drawing.Point(7, 21);
            this.rbGoods.Name = "rbGoods";
            this.rbGoods.Size = new System.Drawing.Size(56, 17);
            this.rbGoods.TabIndex = 0;
            this.rbGoods.TabStop = true;
            this.rbGoods.Text = "Товар";
            this.rbGoods.UseVisualStyleBackColor = true;
            // 
            // ucGoods
            // 
            this.ucGoods.AllowSaveState = false;
            this.ucGoods.Caption = "Товар(ы)";
            this.ucGoods.Location = new System.Drawing.Point(15, 103);
            this.ucGoods.Mnemocode = "GOODS2";
            this.ucGoods.Name = "ucGoods";
            this.ucGoods.Size = new System.Drawing.Size(390, 95);
            this.ucGoods.TabIndex = 37;
            // 
            // ucStores
            // 
            this.ucStores.AllowSaveState = false;
            this.ucStores.Caption = "Склад(ы)";
            this.ucStores.Location = new System.Drawing.Point(14, 204);
            this.ucStores.Mnemocode = "STORE";
            this.ucStores.Name = "ucStores";
            this.ucStores.Size = new System.Drawing.Size(390, 93);
            this.ucStores.TabIndex = 38;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chESForm);
            this.panel2.Controls.Add(this.chbCollapse);
            this.panel2.Controls.Add(this.chbOA);
            this.panel2.Location = new System.Drawing.Point(18, 356);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(208, 73);
            this.panel2.TabIndex = 39;
            // 
            // chESForm
            // 
            this.chESForm.AutoSize = true;
            this.chESForm.Location = new System.Drawing.Point(7, 50);
            this.chESForm.Name = "chESForm";
            this.chESForm.Size = new System.Drawing.Size(134, 17);
            this.chESForm.TabIndex = 2;
            this.chESForm.Text = "Сформировать по ЕС";
            this.chESForm.UseVisualStyleBackColor = true;
            // 
            // chbCollapse
            // 
            this.chbCollapse.AutoSize = true;
            this.chbCollapse.Location = new System.Drawing.Point(7, 28);
            this.chbCollapse.Name = "chbCollapse";
            this.chbCollapse.Size = new System.Drawing.Size(191, 17);
            this.chbCollapse.TabIndex = 1;
            this.chbCollapse.Text = "Сворачивать товары по группам";
            this.chbCollapse.UseVisualStyleBackColor = true;
            // 
            // chbOA
            // 
            this.chbOA.AutoSize = true;
            this.chbOA.Location = new System.Drawing.Point(7, 6);
            this.chbOA.Name = "chbOA";
            this.chbOA.Size = new System.Drawing.Size(189, 17);
            this.chbOA.TabIndex = 0;
            this.chbOA.Text = "Только товары с признаком ОА";
            this.chbOA.UseVisualStyleBackColor = true;
            // 
            // tbMin
            // 
            this.tbMin.Location = new System.Drawing.Point(174, 44);
            this.tbMin.Name = "tbMin";
            this.tbMin.Size = new System.Drawing.Size(120, 20);
            this.tbMin.TabIndex = 40;
            // 
            // FormParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 466);
            this.Controls.Add(this.tbMin);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.ucStores);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.nUDDays);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toDateTimePicker);
            this.Controls.Add(this.fromDateTimePicker);
            this.Controls.Add(this.lbPeriod);
            this.Controls.Add(this.ucGoods);
            this.Name = "FormParams";
            this.Controls.SetChildIndex(this.ucGoods, 0);
            this.Controls.SetChildIndex(this.lbPeriod, 0);
            this.Controls.SetChildIndex(this.fromDateTimePicker, 0);
            this.Controls.SetChildIndex(this.toDateTimePicker, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.nUDDays, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.ucStores, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.tbMin, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nUDDays)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker toDateTimePicker;
        private System.Windows.Forms.DateTimePicker fromDateTimePicker;
        private System.Windows.Forms.Label lbPeriod;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nUDDays;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbSupplier;
        private System.Windows.Forms.RadioButton rbGoods;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucGoods;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucStores;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox chESForm;
        private System.Windows.Forms.CheckBox chbCollapse;
        private System.Windows.Forms.CheckBox chbOA;
        private System.Windows.Forms.TextBox tbMin;
    }
}