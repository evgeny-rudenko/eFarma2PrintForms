namespace EnterBalancesEx
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
            this.ucRemains = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.gbTypeReport = new System.Windows.Forms.GroupBox();
            this.rbEnterBal = new System.Windows.Forms.RadioButton();
            this.rbSummatySt = new System.Windows.Forms.RadioButton();
            this.cbSort = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.gbTypeReport.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(373, 3);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(448, 3);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 331);
            this.panel1.Size = new System.Drawing.Size(526, 29);
            // 
            // ucRemains
            // 
            this.ucRemains.AllowSaveState = false;
            this.ucRemains.Caption = "Список документов";
            this.ucRemains.Location = new System.Drawing.Point(5, 81);
            this.ucRemains.Mnemocode = "IMPORT_REMAINS";
            this.ucRemains.Name = "ucRemains";
            this.ucRemains.Size = new System.Drawing.Size(313, 237);
            this.ucRemains.TabIndex = 181;
            // 
            // gbTypeReport
            // 
            this.gbTypeReport.Controls.Add(this.rbEnterBal);
            this.gbTypeReport.Controls.Add(this.rbSummatySt);
            this.gbTypeReport.Location = new System.Drawing.Point(12, 9);
            this.gbTypeReport.Name = "gbTypeReport";
            this.gbTypeReport.Size = new System.Drawing.Size(154, 66);
            this.gbTypeReport.TabIndex = 182;
            this.gbTypeReport.TabStop = false;
            this.gbTypeReport.Text = "Вид отчета:";
            // 
            // rbEnterBal
            // 
            this.rbEnterBal.AutoSize = true;
            this.rbEnterBal.Location = new System.Drawing.Point(6, 43);
            this.rbEnterBal.Name = "rbEnterBal";
            this.rbEnterBal.Size = new System.Drawing.Size(99, 17);
            this.rbEnterBal.TabIndex = 1;
            this.rbEnterBal.Text = "Ввод остатков";
            this.rbEnterBal.UseVisualStyleBackColor = true;
            this.rbEnterBal.CheckedChanged += new System.EventHandler(this.rbEnterBal_CheckedChanged);
            // 
            // rbSummatySt
            // 
            this.rbSummatySt.AutoSize = true;
            this.rbSummatySt.Checked = true;
            this.rbSummatySt.Location = new System.Drawing.Point(7, 20);
            this.rbSummatySt.Name = "rbSummatySt";
            this.rbSummatySt.Size = new System.Drawing.Size(126, 17);
            this.rbSummatySt.TabIndex = 0;
            this.rbSummatySt.TabStop = true;
            this.rbSummatySt.Text = "Сводная ведомость";
            this.rbSummatySt.UseVisualStyleBackColor = true;
            // 
            // cbSort
            // 
            this.cbSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSort.FormattingEnabled = true;
            this.cbSort.Items.AddRange(new object[] {
            "товар",
            "поставщик",
            "ставка НДС",
            "место хранения"});
            this.cbSort.Location = new System.Drawing.Point(200, 27);
            this.cbSort.Name = "cbSort";
            this.cbSort.Size = new System.Drawing.Size(135, 21);
            this.cbSort.TabIndex = 183;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(197, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 184;
            this.label1.Text = "Сортировать по:";
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "Наименование товара /Ед.изм.",
            "Количество",
            "Серия /Сертификат",
            "Поставщик",
            "Опт.цена",
            "Розн.цена",
            "Ставка НДС /Сумма НДС",
            "Наценка",
            "Опт.сумма",
            "Розн.сумма",
            "ЖНВЛС",
            "ГТД",
            "Место хранения",
            "Рег.серт."});
            this.checkedListBox1.Location = new System.Drawing.Point(341, 100);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(178, 214);
            this.checkedListBox1.TabIndex = 185;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(343, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 13);
            this.label2.TabIndex = 186;
            this.label2.Text = "Показывать колонки:";
            // 
            // FormParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 360);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ucRemains);
            this.Controls.Add(this.cbSort);
            this.Controls.Add(this.gbTypeReport);
            this.Name = "FormParams";
            this.Shown += new System.EventHandler(this.FormParams_Shown);
            this.Controls.SetChildIndex(this.gbTypeReport, 0);
            this.Controls.SetChildIndex(this.cbSort, 0);
            this.Controls.SetChildIndex(this.ucRemains, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.checkedListBox1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.panel1.ResumeLayout(false);
            this.gbTypeReport.ResumeLayout(false);
            this.gbTypeReport.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ePlus.MetaData.Client.UCPluginMultiSelect ucRemains;
        private System.Windows.Forms.GroupBox gbTypeReport;
        private System.Windows.Forms.RadioButton rbEnterBal;
        private System.Windows.Forms.RadioButton rbSummatySt;
        private System.Windows.Forms.ComboBox cbSort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Label label2;
    }
}