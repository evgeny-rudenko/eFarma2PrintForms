namespace GoodsReportsTaxGroup_Groups
{
    partial class TaxGroupsParamsGroup
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
            this.label2 = new System.Windows.Forms.Label();
            this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkShowSub = new System.Windows.Forms.CheckBox();
            this.chkShowAdd = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkShowSumByDocType = new System.Windows.Forms.CheckBox();
            this.rbDocDate = new System.Windows.Forms.RadioButton();
            this.rbDocType = new System.Windows.Forms.RadioButton();
            this.serviceCheckBox = new System.Windows.Forms.CheckBox();
            this.chbShowNal = new System.Windows.Forms.CheckBox();
            this.chkContractorGroup = new System.Windows.Forms.CheckBox();
            this.chkRefreshDocMov = new System.Windows.Forms.CheckBox();
            this.chkShortReport = new System.Windows.Forms.CheckBox();
            this.ucSelectContractorStores = new ucSelectContractorStores();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(469, 3);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(544, 3);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 357);
            this.panel1.Size = new System.Drawing.Size(622, 29);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 142;
            this.label2.Text = "Период:";
            // 
            // ucPeriod
            // 
            this.ucPeriod.DateFrom = new System.DateTime(((long)(0)));
            this.ucPeriod.DateTo = new System.DateTime(2100, 1, 1, 0, 0, 0, 0);
            this.ucPeriod.Location = new System.Drawing.Point(61, 12);
            this.ucPeriod.Name = "ucPeriod";
            this.ucPeriod.Size = new System.Drawing.Size(222, 21);
            this.ucPeriod.TabIndex = 141;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkShowSub);
            this.groupBox2.Controls.Add(this.chkShowAdd);
            this.groupBox2.Location = new System.Drawing.Point(380, 126);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(230, 67);
            this.groupBox2.TabIndex = 144;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Отображение";
            // 
            // chkShowSub
            // 
            this.chkShowSub.AutoSize = true;
            this.chkShowSub.Checked = true;
            this.chkShowSub.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowSub.Location = new System.Drawing.Point(7, 42);
            this.chkShowSub.Name = "chkShowSub";
            this.chkShowSub.Size = new System.Drawing.Size(127, 17);
            this.chkShowSub.TabIndex = 1;
            this.chkShowSub.Text = "Показывать расход";
            this.chkShowSub.UseVisualStyleBackColor = true;
            // 
            // chkShowAdd
            // 
            this.chkShowAdd.AutoSize = true;
            this.chkShowAdd.Checked = true;
            this.chkShowAdd.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowAdd.Location = new System.Drawing.Point(7, 19);
            this.chkShowAdd.Name = "chkShowAdd";
            this.chkShowAdd.Size = new System.Drawing.Size(127, 17);
            this.chkShowAdd.TabIndex = 0;
            this.chkShowAdd.Text = "Показывать приход";
            this.chkShowAdd.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkShowSumByDocType);
            this.groupBox1.Controls.Add(this.rbDocDate);
            this.groupBox1.Controls.Add(this.rbDocType);
            this.groupBox1.Location = new System.Drawing.Point(380, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(230, 81);
            this.groupBox1.TabIndex = 143;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Сортировка";
            // 
            // chkShowSumByDocType
            // 
            this.chkShowSumByDocType.AutoSize = true;
            this.chkShowSumByDocType.Location = new System.Drawing.Point(20, 39);
            this.chkShowSumByDocType.Name = "chkShowSumByDocType";
            this.chkShowSumByDocType.Size = new System.Drawing.Size(201, 17);
            this.chkShowSumByDocType.TabIndex = 2;
            this.chkShowSumByDocType.Text = "Показывать суммы по операциям";
            this.chkShowSumByDocType.UseVisualStyleBackColor = true;
            this.chkShowSumByDocType.Visible = false;
            // 
            // rbDocDate
            // 
            this.rbDocDate.AutoSize = true;
            this.rbDocDate.Location = new System.Drawing.Point(7, 57);
            this.rbDocDate.Name = "rbDocDate";
            this.rbDocDate.Size = new System.Drawing.Size(136, 17);
            this.rbDocDate.TabIndex = 1;
            this.rbDocDate.Text = "По датам документов";
            this.rbDocDate.UseVisualStyleBackColor = true;
            // 
            // rbDocType
            // 
            this.rbDocType.AutoSize = true;
            this.rbDocType.Checked = true;
            this.rbDocType.Location = new System.Drawing.Point(7, 19);
            this.rbDocType.Name = "rbDocType";
            this.rbDocType.Size = new System.Drawing.Size(137, 17);
            this.rbDocType.TabIndex = 0;
            this.rbDocType.TabStop = true;
            this.rbDocType.Text = "По видам документов";
            this.rbDocType.UseVisualStyleBackColor = true;
            // 
            // serviceCheckBox
            // 
            this.serviceCheckBox.AutoSize = true;
            this.serviceCheckBox.Location = new System.Drawing.Point(12, 287);
            this.serviceCheckBox.Name = "serviceCheckBox";
            this.serviceCheckBox.Size = new System.Drawing.Size(117, 17);
            this.serviceCheckBox.TabIndex = 149;
            this.serviceCheckBox.Text = "Учитывать услуги";
            this.serviceCheckBox.UseVisualStyleBackColor = true;
            // 
            // chbShowNal
            // 
            this.chbShowNal.AutoSize = true;
            this.chbShowNal.Location = new System.Drawing.Point(12, 241);
            this.chbShowNal.Name = "chbShowNal";
            this.chbShowNal.Size = new System.Drawing.Size(148, 17);
            this.chbShowNal.TabIndex = 148;
            this.chbShowNal.Text = "Показывать наложение";
            this.chbShowNal.UseVisualStyleBackColor = true;
            // 
            // chkContractorGroup
            // 
            this.chkContractorGroup.AutoSize = true;
            this.chkContractorGroup.Location = new System.Drawing.Point(12, 310);
            this.chkContractorGroup.Name = "chkContractorGroup";
            this.chkContractorGroup.Size = new System.Drawing.Size(185, 17);
            this.chkContractorGroup.TabIndex = 147;
            this.chkContractorGroup.Text = "Группировать по контрагентам";
            this.chkContractorGroup.UseVisualStyleBackColor = true;
            this.chkContractorGroup.Visible = false;
            // 
            // chkRefreshDocMov
            // 
            this.chkRefreshDocMov.AutoSize = true;
            this.chkRefreshDocMov.Location = new System.Drawing.Point(12, 264);
            this.chkRefreshDocMov.Name = "chkRefreshDocMov";
            this.chkRefreshDocMov.Size = new System.Drawing.Size(317, 17);
            this.chkRefreshDocMov.TabIndex = 146;
            this.chkRefreshDocMov.Text = "Обновить промежуточные данные (выполняется дольше)";
            this.chkRefreshDocMov.UseVisualStyleBackColor = true;
            // 
            // chkShortReport
            // 
            this.chkShortReport.AutoSize = true;
            this.chkShortReport.Location = new System.Drawing.Point(12, 333);
            this.chkShortReport.Name = "chkShortReport";
            this.chkShortReport.Size = new System.Drawing.Size(98, 17);
            this.chkShortReport.TabIndex = 145;
            this.chkShortReport.Text = "Краткий отчет";
            this.chkShortReport.UseVisualStyleBackColor = true;
            this.chkShortReport.Visible = false;
            // 
            // ucSelectContractorStores
            // 
            this.ucSelectContractorStores.Location = new System.Drawing.Point(12, 39);
            this.ucSelectContractorStores.Name = "ucSelectContractorStores";
            this.ucSelectContractorStores.Size = new System.Drawing.Size(331, 196);
            this.ucSelectContractorStores.TabIndex = 150;
            // 
            // TaxGroupsParamsGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 386);
            this.Controls.Add(this.serviceCheckBox);
            this.Controls.Add(this.chbShowNal);
            this.Controls.Add(this.chkContractorGroup);
            this.Controls.Add(this.chkRefreshDocMov);
            this.Controls.Add(this.chkShortReport);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ucPeriod);
            this.Controls.Add(this.ucSelectContractorStores);
            this.Name = "TaxGroupsParamsGroup";
            this.Controls.SetChildIndex(this.ucSelectContractorStores, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.ucPeriod, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.chkShortReport, 0);
            this.Controls.SetChildIndex(this.chkRefreshDocMov, 0);
            this.Controls.SetChildIndex(this.chkContractorGroup, 0);
            this.Controls.SetChildIndex(this.chbShowNal, 0);
            this.Controls.SetChildIndex(this.serviceCheckBox, 0);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private ePlus.MetaData.Client.UCPeriod ucPeriod;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkShowSub;
        private System.Windows.Forms.CheckBox chkShowAdd;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkShowSumByDocType;
        private System.Windows.Forms.RadioButton rbDocDate;
        private System.Windows.Forms.RadioButton rbDocType;
        private System.Windows.Forms.CheckBox serviceCheckBox;
        private System.Windows.Forms.CheckBox chbShowNal;
        private System.Windows.Forms.CheckBox chkContractorGroup;
        private System.Windows.Forms.CheckBox chkRefreshDocMov;
        private System.Windows.Forms.CheckBox chkShortReport;
        private ePlus.CommonEx.Controls.ucSelectContractorStores ucSelectContractorStores;


    }
}