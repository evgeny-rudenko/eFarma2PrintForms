using ePlus.MetaData.Client;
using ePlus.Reports.Client;
using ePlus.ReportsEx;

namespace ePlus.ReportsEx
{
    partial class Goods_Reports_Form
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
          this.ePlusLabel1 = new ePlus.Controls.NewControls.ePlusLabel(this.components);
          this.btnReport = new ePlus.Controls.NewControls.ePlusButton(this.components);
          this.btnCancel = new ePlus.Controls.NewControls.ePlusButton(this.components);
          this.lblCondition = new ePlus.Controls.NewControls.ePlusLabel(this.components);
          this.ePlusLabel2 = new ePlus.Controls.NewControls.ePlusLabel(this.components);
          this.comboBox1 = new System.Windows.Forms.ComboBox();
          this.valDocNumber = new ePlus.Controls.SmartDecimal();
          this.label1 = new System.Windows.Forms.Label();
          this.checkBox_Detail = new System.Windows.Forms.CheckBox();
          this.groupBox1 = new System.Windows.Forms.GroupBox();
          this.radioButton_DateDoc = new System.Windows.Forms.RadioButton();
          this.radioButton_TypeDoc = new System.Windows.Forms.RadioButton();
          this.checkBox_Add = new System.Windows.Forms.CheckBox();
          this.checkBox_Sub = new System.Windows.Forms.CheckBox();
          this.groupBox2 = new System.Windows.Forms.GroupBox();
          this.selectContractorStoreControl1 = new ePlus.Reports.Client.SelectContractorStoreControl();
          this.ucPeriod1 = new ePlus.MetaData.Client.UCPeriod();
          ((System.ComponentModel.ISupportInitialize)(this.valDocNumber.Properties)).BeginInit();
          this.groupBox1.SuspendLayout();
          this.groupBox2.SuspendLayout();
          this.SuspendLayout();
          // 
          // ePlusLabel1
          // 
          this.ePlusLabel1.FlatStyle = System.Windows.Forms.FlatStyle.System;
          this.ePlusLabel1.Location = new System.Drawing.Point(60, 39);
          this.ePlusLabel1.Name = "ePlusLabel1";
          this.ePlusLabel1.Size = new System.Drawing.Size(13, 20);
          this.ePlusLabel1.TabIndex = 122;
          this.ePlusLabel1.Text = "с";
          // 
          // btnReport
          // 
          this.btnReport.Location = new System.Drawing.Point(249, 404);
          this.btnReport.Name = "btnReport";
          this.btnReport.Size = new System.Drawing.Size(75, 23);
          this.btnReport.TabIndex = 120;
          this.btnReport.Text = "Отчет";
          this.btnReport.UseCompatibleTextRendering = true;
          this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
          // 
          // btnCancel
          // 
          this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
          this.btnCancel.Location = new System.Drawing.Point(338, 404);
          this.btnCancel.Name = "btnCancel";
          this.btnCancel.Size = new System.Drawing.Size(75, 23);
          this.btnCancel.TabIndex = 121;
          this.btnCancel.Text = "Отмена";
          this.btnCancel.UseCompatibleTextRendering = true;
          this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
          // 
          // lblCondition
          // 
          this.lblCondition.FlatStyle = System.Windows.Forms.FlatStyle.System;
          this.lblCondition.Location = new System.Drawing.Point(8, 39);
          this.lblCondition.Name = "lblCondition";
          this.lblCondition.Size = new System.Drawing.Size(46, 20);
          this.lblCondition.TabIndex = 123;
          this.lblCondition.Text = "Период";
          // 
          // ePlusLabel2
          // 
          this.ePlusLabel2.FlatStyle = System.Windows.Forms.FlatStyle.System;
          this.ePlusLabel2.Location = new System.Drawing.Point(8, 66);
          this.ePlusLabel2.Name = "ePlusLabel2";
          this.ePlusLabel2.Size = new System.Drawing.Size(85, 20);
          this.ePlusLabel2.TabIndex = 123;
          this.ePlusLabel2.Text = "Форма отчета:";
          // 
          // comboBox1
          // 
          this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
          this.comboBox1.FormattingEnabled = true;
          this.comboBox1.Items.AddRange(new object[] {
            "По налоговым группам",
            "ТОРГ-29 Опт-Розница-Наложение",
            "Форма № АП-25",
            "Ведомость № 11",
            "ТОРГ-29 (суммы поставщика)",
            "ТОРГ-29 (розница)"});
          this.comboBox1.Location = new System.Drawing.Point(91, 66);
          this.comboBox1.Name = "comboBox1";
          this.comboBox1.Size = new System.Drawing.Size(321, 21);
          this.comboBox1.TabIndex = 124;
          // 
          // valDocNumber
          // 
          this.valDocNumber.DataMember = null;
          this.valDocNumber.EditMask = "### ### ###,##0";
          this.valDocNumber.EditValue = "1";
          this.valDocNumber.Format = ePlus.Controls.ENumFormat.Num0;
          this.valDocNumber.Location = new System.Drawing.Point(91, 93);
          this.valDocNumber.Name = "valDocNumber";
          this.valDocNumber.Properties.Appearance.Options.UseTextOptions = true;
          this.valDocNumber.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
          this.valDocNumber.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
          this.valDocNumber.Properties.AppearanceDisabled.Options.UseForeColor = true;
          this.valDocNumber.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
          this.valDocNumber.Properties.Mask.EditMask = "### ### ###,##0";
          this.valDocNumber.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
          this.valDocNumber.Properties.Mask.UseMaskAsDisplayFormat = true;
          this.valDocNumber.ReadOnly = false;
          this.valDocNumber.Size = new System.Drawing.Size(125, 20);
          this.valDocNumber.TabIndex = 126;
          this.valDocNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
          // 
          // label1
          // 
          this.label1.Location = new System.Drawing.Point(7, 86);
          this.label1.Name = "label1";
          this.label1.Size = new System.Drawing.Size(64, 32);
          this.label1.TabIndex = 127;
          this.label1.Text = "Номер документа";
          // 
          // checkBox_Detail
          // 
          this.checkBox_Detail.AutoSize = true;
          this.checkBox_Detail.Location = new System.Drawing.Point(226, 95);
          this.checkBox_Detail.Name = "checkBox_Detail";
          this.checkBox_Detail.Size = new System.Drawing.Size(98, 17);
          this.checkBox_Detail.TabIndex = 128;
          this.checkBox_Detail.Text = "Краткий отчет";
          this.checkBox_Detail.UseVisualStyleBackColor = true;
          // 
          // groupBox1
          // 
          this.groupBox1.Controls.Add(this.radioButton_DateDoc);
          this.groupBox1.Controls.Add(this.radioButton_TypeDoc);
          this.groupBox1.Location = new System.Drawing.Point(9, 303);
          this.groupBox1.Name = "groupBox1";
          this.groupBox1.Size = new System.Drawing.Size(404, 37);
          this.groupBox1.TabIndex = 129;
          this.groupBox1.TabStop = false;
          this.groupBox1.Text = "Вид сортировки";
          // 
          // radioButton_DateDoc
          // 
          this.radioButton_DateDoc.AutoSize = true;
          this.radioButton_DateDoc.Location = new System.Drawing.Point(244, 14);
          this.radioButton_DateDoc.Name = "radioButton_DateDoc";
          this.radioButton_DateDoc.Size = new System.Drawing.Size(128, 17);
          this.radioButton_DateDoc.TabIndex = 0;
          this.radioButton_DateDoc.Text = "по датам документа";
          this.radioButton_DateDoc.UseVisualStyleBackColor = true;
          // 
          // radioButton_TypeDoc
          // 
          this.radioButton_TypeDoc.AutoSize = true;
          this.radioButton_TypeDoc.Checked = true;
          this.radioButton_TypeDoc.Location = new System.Drawing.Point(38, 14);
          this.radioButton_TypeDoc.Name = "radioButton_TypeDoc";
          this.radioButton_TypeDoc.Size = new System.Drawing.Size(129, 17);
          this.radioButton_TypeDoc.TabIndex = 0;
          this.radioButton_TypeDoc.TabStop = true;
          this.radioButton_TypeDoc.Text = "по видам документа";
          this.radioButton_TypeDoc.UseVisualStyleBackColor = true;
          // 
          // checkBox_Add
          // 
          this.checkBox_Add.AutoSize = true;
          this.checkBox_Add.Checked = true;
          this.checkBox_Add.CheckState = System.Windows.Forms.CheckState.Checked;
          this.checkBox_Add.Location = new System.Drawing.Point(37, 15);
          this.checkBox_Add.Name = "checkBox_Add";
          this.checkBox_Add.Size = new System.Drawing.Size(127, 17);
          this.checkBox_Add.TabIndex = 130;
          this.checkBox_Add.Text = "Показывать приход";
          this.checkBox_Add.UseVisualStyleBackColor = true;
          this.checkBox_Add.CheckedChanged += new System.EventHandler(this.checkBox_Add_CheckedChanged);
          // 
          // checkBox_Sub
          // 
          this.checkBox_Sub.AutoSize = true;
          this.checkBox_Sub.Checked = true;
          this.checkBox_Sub.CheckState = System.Windows.Forms.CheckState.Checked;
          this.checkBox_Sub.Location = new System.Drawing.Point(243, 15);
          this.checkBox_Sub.Name = "checkBox_Sub";
          this.checkBox_Sub.Size = new System.Drawing.Size(127, 17);
          this.checkBox_Sub.TabIndex = 130;
          this.checkBox_Sub.Text = "Показывать расход";
          this.checkBox_Sub.UseVisualStyleBackColor = true;
          this.checkBox_Sub.CheckedChanged += new System.EventHandler(this.checkBox_Sub_CheckedChanged);
          // 
          // groupBox2
          // 
          this.groupBox2.Controls.Add(this.checkBox_Sub);
          this.groupBox2.Controls.Add(this.checkBox_Add);
          this.groupBox2.Location = new System.Drawing.Point(10, 346);
          this.groupBox2.Name = "groupBox2";
          this.groupBox2.Size = new System.Drawing.Size(403, 41);
          this.groupBox2.TabIndex = 131;
          this.groupBox2.TabStop = false;
          this.groupBox2.Text = "Отображение сведений";
          // 
          // selectContractorStoreControl1
          // 
          this.selectContractorStoreControl1.Location = new System.Drawing.Point(10, 121);
          this.selectContractorStoreControl1.Name = "selectContractorStoreControl1";
          this.selectContractorStoreControl1.Size = new System.Drawing.Size(408, 176);
          this.selectContractorStoreControl1.TabIndex = 125;
          // 
          // ucPeriod1
          // 
          this.ucPeriod1.DateFrom = new System.DateTime(2006, 10, 30, 14, 2, 0, 906);
          this.ucPeriod1.DateTo = new System.DateTime(2006, 10, 30, 14, 2, 0, 921);
          this.ucPeriod1.Location = new System.Drawing.Point(91, 38);
          this.ucPeriod1.Name = "ucPeriod1";
          this.ucPeriod1.Size = new System.Drawing.Size(327, 21);
          this.ucPeriod1.TabIndex = 119;
          // 
          // Goods_Reports_Form
          // 
          this.AcceptButton = this.btnReport;
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.ClientSize = new System.Drawing.Size(424, 435);
          this.Controls.Add(this.lblCondition);
          this.Controls.Add(this.valDocNumber);
          this.Controls.Add(this.label1);
          this.Controls.Add(this.groupBox2);
          this.Controls.Add(this.btnReport);
          this.Controls.Add(this.groupBox1);
          this.Controls.Add(this.selectContractorStoreControl1);
          this.Controls.Add(this.comboBox1);
          this.Controls.Add(this.ePlusLabel1);
          this.Controls.Add(this.ucPeriod1);
          this.Controls.Add(this.ePlusLabel2);
          this.Controls.Add(this.checkBox_Detail);
          this.Controls.Add(this.btnCancel);
          this.Name = "Goods_Reports_Form";
          this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
          this.Text = "Параметры отчета: \"Товарный отчет\" ";
          this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Goods_Reports_Form_FormClosed);
          this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GoodsReports_FormClosing);
          this.Controls.SetChildIndex(this.btnCancel, 0);
          this.Controls.SetChildIndex(this.checkBox_Detail, 0);
          this.Controls.SetChildIndex(this.ePlusLabel2, 0);
          this.Controls.SetChildIndex(this.ucPeriod1, 0);
          this.Controls.SetChildIndex(this.ePlusLabel1, 0);
          this.Controls.SetChildIndex(this.comboBox1, 0);
          this.Controls.SetChildIndex(this.selectContractorStoreControl1, 0);
          this.Controls.SetChildIndex(this.groupBox1, 0);
          this.Controls.SetChildIndex(this.btnReport, 0);
          this.Controls.SetChildIndex(this.groupBox2, 0);
          this.Controls.SetChildIndex(this.label1, 0);
          this.Controls.SetChildIndex(this.valDocNumber, 0);
          this.Controls.SetChildIndex(this.lblCondition, 0);
          ((System.ComponentModel.ISupportInitialize)(this.valDocNumber.Properties)).EndInit();
          this.groupBox1.ResumeLayout(false);
          this.groupBox1.PerformLayout();
          this.groupBox2.ResumeLayout(false);
          this.groupBox2.PerformLayout();
          this.ResumeLayout(false);
          this.PerformLayout();

        }

        #endregion

        private ePlus.Controls.NewControls.ePlusLabel ePlusLabel1;
        private UCPeriod ucPeriod1;
        private ePlus.Controls.NewControls.ePlusButton btnReport;
        private ePlus.Controls.NewControls.ePlusButton btnCancel;
        private ePlus.Controls.NewControls.ePlusLabel lblCondition;
        private ePlus.Controls.NewControls.ePlusLabel ePlusLabel2;
        private System.Windows.Forms.ComboBox comboBox1;
        private SelectContractorStoreControl selectContractorStoreControl1;
        private ePlus.Controls.SmartDecimal valDocNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox_Detail;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton_DateDoc;
        private System.Windows.Forms.RadioButton radioButton_TypeDoc;
        private System.Windows.Forms.CheckBox checkBox_Add;
        private System.Windows.Forms.CheckBox checkBox_Sub;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}