namespace TORG29Roz
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
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.chkShowSub = new System.Windows.Forms.CheckBox();
			this.chkShowAdd = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.chkShowSumByDocType = new System.Windows.Forms.CheckBox();
			this.rbDocDate = new System.Windows.Forms.RadioButton();
			this.rbDocType = new System.Windows.Forms.RadioButton();
			this.label1 = new System.Windows.Forms.Label();
			this.chkRefreshDocMov = new System.Windows.Forms.CheckBox();
			this.chkShortReport = new System.Windows.Forms.CheckBox();
			this.chkContractorGroup = new System.Windows.Forms.CheckBox();
			this.mpsContractor = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
			this.mpsStore = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.serviceCheckBox = new System.Windows.Forms.CheckBox();
			this.panel1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// bOK
			// 
			this.bOK.Location = new System.Drawing.Point(507, 3);
			// 
			// bClose
			// 
			this.bClose.Location = new System.Drawing.Point(582, 3);
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(0, 270);
			this.panel1.Size = new System.Drawing.Size(660, 29);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.chkShowSub);
			this.groupBox2.Controls.Add(this.chkShowAdd);
			this.groupBox2.Location = new System.Drawing.Point(426, 120);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(230, 67);
			this.groupBox2.TabIndex = 172;
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
			this.groupBox1.Location = new System.Drawing.Point(426, 33);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(230, 81);
			this.groupBox1.TabIndex = 171;
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
			this.rbDocType.Location = new System.Drawing.Point(7, 20);
			this.rbDocType.Name = "rbDocType";
			this.rbDocType.Size = new System.Drawing.Size(137, 17);
			this.rbDocType.TabIndex = 0;
			this.rbDocType.TabStop = true;
			this.rbDocType.Text = "По видам документов";
			this.rbDocType.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 13);
			this.label1.TabIndex = 168;
			this.label1.Text = "Период:";
			// 
			// chkRefreshDocMov
			// 
			this.chkRefreshDocMov.AutoSize = true;
			this.chkRefreshDocMov.Location = new System.Drawing.Point(15, 216);
			this.chkRefreshDocMov.Name = "chkRefreshDocMov";
			this.chkRefreshDocMov.Size = new System.Drawing.Size(317, 17);
			this.chkRefreshDocMov.TabIndex = 174;
			this.chkRefreshDocMov.Text = "Обновить промежуточные данные (выполняется дольше)";
			this.chkRefreshDocMov.UseVisualStyleBackColor = true;
			// 
			// chkShortReport
			// 
			this.chkShortReport.AutoSize = true;
			this.chkShortReport.Location = new System.Drawing.Point(206, 193);
			this.chkShortReport.Name = "chkShortReport";
			this.chkShortReport.Size = new System.Drawing.Size(98, 17);
			this.chkShortReport.TabIndex = 173;
			this.chkShortReport.Text = "Краткий отчет";
			this.chkShortReport.UseVisualStyleBackColor = true;
			this.chkShortReport.Visible = false;
			// 
			// chkContractorGroup
			// 
			this.chkContractorGroup.AutoSize = true;
			this.chkContractorGroup.Location = new System.Drawing.Point(15, 193);
			this.chkContractorGroup.Name = "chkContractorGroup";
			this.chkContractorGroup.Size = new System.Drawing.Size(185, 17);
			this.chkContractorGroup.TabIndex = 175;
			this.chkContractorGroup.Text = "Группировать по контрагентам";
			this.chkContractorGroup.UseVisualStyleBackColor = true;
			this.chkContractorGroup.Visible = false;
			// 
			// mpsContractor
			// 
			this.mpsContractor.AllowSaveState = false;
			this.mpsContractor.Caption = "Контрагенты";
			this.mpsContractor.Location = new System.Drawing.Point(12, 33);
			this.mpsContractor.Mnemocode = "CONTRACTOR";
			this.mpsContractor.Name = "mpsContractor";
			this.mpsContractor.Size = new System.Drawing.Size(408, 74);
			this.mpsContractor.TabIndex = 169;
			// 
			// ucPeriod
			// 
			this.ucPeriod.DateFrom = new System.DateTime(((long) (0)));
			this.ucPeriod.DateTo = new System.DateTime(2100, 1, 1, 0, 0, 0, 0);
			this.ucPeriod.Location = new System.Drawing.Point(66, 8);
			this.ucPeriod.Name = "ucPeriod";
			this.ucPeriod.Size = new System.Drawing.Size(222, 21);
			this.ucPeriod.TabIndex = 167;
			// 
			// mpsStore
			// 
			this.mpsStore.AllowSaveState = false;
			this.mpsStore.Caption = "Склады";
			this.mpsStore.Location = new System.Drawing.Point(12, 113);
			this.mpsStore.Mnemocode = "STORE";
			this.mpsStore.Name = "mpsStore";
			this.mpsStore.Size = new System.Drawing.Size(408, 74);
			this.mpsStore.TabIndex = 170;
			// 
			// serviceCheckBox
			// 
			this.serviceCheckBox.AutoSize = true;
			this.serviceCheckBox.Location = new System.Drawing.Point(15, 239);
			this.serviceCheckBox.Name = "serviceCheckBox";
			this.serviceCheckBox.Size = new System.Drawing.Size(117, 17);
			this.serviceCheckBox.TabIndex = 176;
			this.serviceCheckBox.Text = "Учитывать услуги";
			this.serviceCheckBox.UseVisualStyleBackColor = true;
			// 
			// FormParams
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(660, 299);
			this.Controls.Add(this.serviceCheckBox);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.chkRefreshDocMov);
			this.Controls.Add(this.chkShortReport);
			this.Controls.Add(this.chkContractorGroup);
			this.Controls.Add(this.mpsContractor);
			this.Controls.Add(this.ucPeriod);
			this.Controls.Add(this.mpsStore);
			this.Name = "FormParams";
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.mpsStore, 0);
			this.Controls.SetChildIndex(this.ucPeriod, 0);
			this.Controls.SetChildIndex(this.mpsContractor, 0);
			this.Controls.SetChildIndex(this.chkContractorGroup, 0);
			this.Controls.SetChildIndex(this.chkShortReport, 0);
			this.Controls.SetChildIndex(this.chkRefreshDocMov, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.groupBox1, 0);
			this.Controls.SetChildIndex(this.groupBox2, 0);
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

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkShowSub;
        private System.Windows.Forms.CheckBox chkShowAdd;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkShowSumByDocType;
        private System.Windows.Forms.RadioButton rbDocDate;
        private System.Windows.Forms.RadioButton rbDocType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkRefreshDocMov;
        private System.Windows.Forms.CheckBox chkShortReport;
        private System.Windows.Forms.CheckBox chkContractorGroup;
        private ePlus.MetaData.Client.UCPluginMultiSelect mpsContractor;
        private ePlus.MetaData.Client.UCPeriod ucPeriod;
        private ePlus.MetaData.Client.UCPluginMultiSelect mpsStore;
		private System.Windows.Forms.CheckBox serviceCheckBox;
    }
}