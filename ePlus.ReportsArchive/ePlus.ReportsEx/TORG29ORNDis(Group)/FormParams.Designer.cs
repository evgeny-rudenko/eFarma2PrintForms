namespace TORG29ORNDis_Group_
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
			this.chbGroupDiscount = new System.Windows.Forms.CheckBox();
			this.chkShortReport = new System.Windows.Forms.CheckBox();
			this.chkColumnSale = new System.Windows.Forms.CheckBox();
			this.chkDateReport = new System.Windows.Forms.CheckBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.chkShowSub = new System.Windows.Forms.CheckBox();
			this.chkShowAdd = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.chkShowSumByDocType = new System.Windows.Forms.CheckBox();
			this.rbDocDate = new System.Windows.Forms.RadioButton();
			this.rbDocType = new System.Windows.Forms.RadioButton();
			this.label1 = new System.Windows.Forms.Label();
			this.chkRefreshDocMov = new System.Windows.Forms.CheckBox();
			this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
			this.ucSelectStoresControl = new ePlus.CommonEx.Controls.ucSelectContractorStores();
			this.chbShowReturn = new System.Windows.Forms.CheckBox();
			this.serviceCheckBox = new System.Windows.Forms.CheckBox();
			this.panel1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// bOK
			// 
			this.bOK.Location = new System.Drawing.Point(529, 3);
			// 
			// bClose
			// 
			this.bClose.Location = new System.Drawing.Point(604, 3);
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(0, 383);
			this.panel1.Size = new System.Drawing.Size(682, 29);
			// 
			// chbGroupDiscount
			// 
			this.chbGroupDiscount.AutoSize = true;
			this.chbGroupDiscount.Location = new System.Drawing.Point(9, 307);
			this.chbGroupDiscount.Name = "chbGroupDiscount";
			this.chbGroupDiscount.Size = new System.Drawing.Size(158, 17);
			this.chbGroupDiscount.TabIndex = 183;
			this.chbGroupDiscount.Text = "������������ �� �������";
			this.chbGroupDiscount.UseVisualStyleBackColor = true;
			// 
			// chkShortReport
			// 
			this.chkShortReport.AutoSize = true;
			this.chkShortReport.Location = new System.Drawing.Point(9, 215);
			this.chkShortReport.Name = "chkShortReport";
			this.chkShortReport.Size = new System.Drawing.Size(98, 17);
			this.chkShortReport.TabIndex = 182;
			this.chkShortReport.Text = "������� �����";
			this.chkShortReport.UseVisualStyleBackColor = true;
			// 
			// chkColumnSale
			// 
			this.chkColumnSale.AutoSize = true;
			this.chkColumnSale.Checked = true;
			this.chkColumnSale.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkColumnSale.Location = new System.Drawing.Point(9, 261);
			this.chkColumnSale.Name = "chkColumnSale";
			this.chkColumnSale.Size = new System.Drawing.Size(183, 17);
			this.chkColumnSale.TabIndex = 181;
			this.chkColumnSale.Text = "���������� ������� \"������\"";
			this.chkColumnSale.UseVisualStyleBackColor = true;
			// 
			// chkDateReport
			// 
			this.chkDateReport.AutoSize = true;
			this.chkDateReport.Checked = true;
			this.chkDateReport.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkDateReport.Location = new System.Drawing.Point(9, 284);
			this.chkDateReport.Name = "chkDateReport";
			this.chkDateReport.Size = new System.Drawing.Size(193, 17);
			this.chkDateReport.TabIndex = 180;
			this.chkDateReport.Text = "���������� ���� ������������";
			this.chkDateReport.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.chkShowSub);
			this.groupBox2.Controls.Add(this.chkShowAdd);
			this.groupBox2.Location = new System.Drawing.Point(442, 121);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(230, 67);
			this.groupBox2.TabIndex = 177;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "�����������";
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
			this.chkShowSub.Text = "���������� ������";
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
			this.chkShowAdd.Text = "���������� ������";
			this.chkShowAdd.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.chkShowSumByDocType);
			this.groupBox1.Controls.Add(this.rbDocDate);
			this.groupBox1.Controls.Add(this.rbDocType);
			this.groupBox1.Location = new System.Drawing.Point(442, 34);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(230, 81);
			this.groupBox1.TabIndex = 176;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "����������";
			// 
			// chkShowSumByDocType
			// 
			this.chkShowSumByDocType.AutoSize = true;
			this.chkShowSumByDocType.Location = new System.Drawing.Point(20, 39);
			this.chkShowSumByDocType.Name = "chkShowSumByDocType";
			this.chkShowSumByDocType.Size = new System.Drawing.Size(201, 17);
			this.chkShowSumByDocType.TabIndex = 2;
			this.chkShowSumByDocType.Text = "���������� ����� �� ���������";
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
			this.rbDocDate.Text = "�� ����� ����������";
			this.rbDocDate.UseVisualStyleBackColor = true;
			// 
			// rbDocType
			// 
			this.rbDocType.AutoSize = true;
			this.rbDocType.Checked = true;
			this.rbDocType.Location = new System.Drawing.Point(7, 20);
			this.rbDocType.Name = "rbDocType";
			this.rbDocType.Size = new System.Drawing.Size(137, 17);
			this.rbDocType.TabIndex = 0;
			this.rbDocType.TabStop = true;
			this.rbDocType.Text = "�� ����� ����������";
			this.rbDocType.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 13);
			this.label1.TabIndex = 174;
			this.label1.Text = "������:";
			// 
			// chkRefreshDocMov
			// 
			this.chkRefreshDocMov.AutoSize = true;
			this.chkRefreshDocMov.Location = new System.Drawing.Point(9, 238);
			this.chkRefreshDocMov.Name = "chkRefreshDocMov";
			this.chkRefreshDocMov.Size = new System.Drawing.Size(317, 17);
			this.chkRefreshDocMov.TabIndex = 178;
			this.chkRefreshDocMov.Text = "�������� ������������� ������ (����������� ������)";
			this.chkRefreshDocMov.UseVisualStyleBackColor = true;
			// 
			// ucPeriod
			// 
			this.ucPeriod.DateFrom = new System.DateTime(((long) (0)));
			this.ucPeriod.DateTo = new System.DateTime(2100, 1, 1, 0, 0, 0, 0);
			this.ucPeriod.Location = new System.Drawing.Point(60, 9);
			this.ucPeriod.Name = "ucPeriod";
			this.ucPeriod.Size = new System.Drawing.Size(222, 21);
			this.ucPeriod.TabIndex = 173;
			// 
			// ucSelectStoresControl
			// 
			this.ucSelectStoresControl.Location = new System.Drawing.Point(0, 46);
			this.ucSelectStoresControl.Name = "ucSelectStoresControl";
			this.ucSelectStoresControl.Size = new System.Drawing.Size(434, 111);
			this.ucSelectStoresControl.TabIndex = 185;
			// 
			// chbShowReturn
			// 
			this.chbShowReturn.AutoSize = true;
			this.chbShowReturn.Location = new System.Drawing.Point(9, 192);
			this.chbShowReturn.Name = "chbShowReturn";
			this.chbShowReturn.Size = new System.Drawing.Size(241, 17);
			this.chbShowReturn.TabIndex = 184;
			this.chbShowReturn.Text = "���������� �������(� ������� � �������)";
			this.chbShowReturn.UseVisualStyleBackColor = true;
			// 
			// serviceCheckBox
			// 
			this.serviceCheckBox.AutoSize = true;
			this.serviceCheckBox.Location = new System.Drawing.Point(9, 330);
			this.serviceCheckBox.Name = "serviceCheckBox";
			this.serviceCheckBox.Size = new System.Drawing.Size(117, 17);
			this.serviceCheckBox.TabIndex = 186;
			this.serviceCheckBox.Text = "��������� ������";
			this.serviceCheckBox.UseVisualStyleBackColor = true;
			// 
			// FormParams
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(682, 412);
			this.Controls.Add(this.chbShowReturn);
			this.Controls.Add(this.serviceCheckBox);
			this.Controls.Add(this.chbGroupDiscount);
			this.Controls.Add(this.chkShortReport);
			this.Controls.Add(this.chkColumnSale);
			this.Controls.Add(this.chkDateReport);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.chkRefreshDocMov);
			this.Controls.Add(this.ucPeriod);
			this.Controls.Add(this.ucSelectStoresControl);
			this.Name = "FormParams";
			this.Controls.SetChildIndex(this.ucSelectStoresControl, 0);
			this.Controls.SetChildIndex(this.ucPeriod, 0);
			this.Controls.SetChildIndex(this.chkRefreshDocMov, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.groupBox1, 0);
			this.Controls.SetChildIndex(this.groupBox2, 0);
			this.Controls.SetChildIndex(this.chkDateReport, 0);
			this.Controls.SetChildIndex(this.chkColumnSale, 0);
			this.Controls.SetChildIndex(this.chkShortReport, 0);
			this.Controls.SetChildIndex(this.chbGroupDiscount, 0);
			this.Controls.SetChildIndex(this.serviceCheckBox, 0);
			this.Controls.SetChildIndex(this.chbShowReturn, 0);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.panel1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chbGroupDiscount;
        private System.Windows.Forms.CheckBox chkShortReport;
        private System.Windows.Forms.CheckBox chkColumnSale;
        private System.Windows.Forms.CheckBox chkDateReport;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkShowSub;
        private System.Windows.Forms.CheckBox chkShowAdd;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkShowSumByDocType;
        private System.Windows.Forms.RadioButton rbDocDate;
        private System.Windows.Forms.RadioButton rbDocType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkRefreshDocMov;
        private ePlus.MetaData.Client.UCPeriod ucPeriod;
        private ePlus.CommonEx.Controls.ucSelectContractorStores ucSelectStoresControl;
        private System.Windows.Forms.CheckBox chbShowReturn;
		private System.Windows.Forms.CheckBox serviceCheckBox;
    }
}