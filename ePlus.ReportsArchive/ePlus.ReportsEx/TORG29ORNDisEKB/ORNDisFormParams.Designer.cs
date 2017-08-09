namespace TORG29ORNDisEKB
{
    partial class ORNDisFormParams
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
            this.chkShortReport = new System.Windows.Forms.CheckBox();
            this.chkColumnSale = new System.Windows.Forms.CheckBox();
            this.chkDateReport = new System.Windows.Forms.CheckBox();
            this.mpsStore = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkShowSub = new System.Windows.Forms.CheckBox();
            this.chkShowAdd = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkShowSumByDocType = new System.Windows.Forms.CheckBox();
            this.rbDocDate = new System.Windows.Forms.RadioButton();
            this.rbDocType = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.chkRefreshDocMov = new System.Windows.Forms.CheckBox();
            this.mpsContractor = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkShortReport
            // 
            this.chkShortReport.AutoSize = true;
            this.chkShortReport.Location = new System.Drawing.Point(15, 187);
            this.chkShortReport.Name = "chkShortReport";
            this.chkShortReport.Size = new System.Drawing.Size(98, 17);
            this.chkShortReport.TabIndex = 179;
            this.chkShortReport.Text = "Краткий отчет";
            this.chkShortReport.UseVisualStyleBackColor = true;
            // 
            // chkColumnSale
            // 
            this.chkColumnSale.AutoSize = true;
            this.chkColumnSale.Checked = true;
            this.chkColumnSale.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkColumnSale.Location = new System.Drawing.Point(15, 233);
            this.chkColumnSale.Name = "chkColumnSale";
            this.chkColumnSale.Size = new System.Drawing.Size(183, 17);
            this.chkColumnSale.TabIndex = 178;
            this.chkColumnSale.Text = "Показывать колонку \"Скидка\"";
            this.chkColumnSale.UseVisualStyleBackColor = true;
            // 
            // chkDateReport
            // 
            this.chkDateReport.AutoSize = true;
            this.chkDateReport.Checked = true;
            this.chkDateReport.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDateReport.Location = new System.Drawing.Point(15, 256);
            this.chkDateReport.Name = "chkDateReport";
            this.chkDateReport.Size = new System.Drawing.Size(193, 17);
            this.chkDateReport.TabIndex = 177;
            this.chkDateReport.Text = "Показывать дату формирования";
            this.chkDateReport.UseVisualStyleBackColor = true;
            // 
            // mpsStore
            // 
            this.mpsStore.AllowSaveState = false;
            this.mpsStore.Caption = "Склады";
            this.mpsStore.Location = new System.Drawing.Point(12, 111);
            this.mpsStore.Mnemocode = "STORE";
            this.mpsStore.Name = "mpsStore";
            this.mpsStore.Size = new System.Drawing.Size(408, 74);
            this.mpsStore.TabIndex = 176;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkShowSub);
            this.groupBox2.Controls.Add(this.chkShowAdd);
            this.groupBox2.Location = new System.Drawing.Point(426, 118);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(230, 67);
            this.groupBox2.TabIndex = 174;
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
            this.groupBox1.Location = new System.Drawing.Point(426, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(230, 81);
            this.groupBox1.TabIndex = 173;
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
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 171;
            this.label1.Text = "Период:";
            // 
            // chkRefreshDocMov
            // 
            this.chkRefreshDocMov.AutoSize = true;
            this.chkRefreshDocMov.Location = new System.Drawing.Point(15, 210);
            this.chkRefreshDocMov.Name = "chkRefreshDocMov";
            this.chkRefreshDocMov.Size = new System.Drawing.Size(317, 17);
            this.chkRefreshDocMov.TabIndex = 175;
            this.chkRefreshDocMov.Text = "Обновить промежуточные данные (выполняется дольше)";
            this.chkRefreshDocMov.UseVisualStyleBackColor = true;
            // 
            // mpsContractor
            // 
            this.mpsContractor.AllowSaveState = false;
            this.mpsContractor.Caption = "Контрагенты";
            this.mpsContractor.Location = new System.Drawing.Point(12, 31);
            this.mpsContractor.Mnemocode = "CONTRACTOR";
            this.mpsContractor.Name = "mpsContractor";
            this.mpsContractor.Size = new System.Drawing.Size(408, 74);
            this.mpsContractor.TabIndex = 172;
            // 
            // ucPeriod
            // 
            this.ucPeriod.DateFrom = new System.DateTime(((long)(0)));
            this.ucPeriod.DateTo = new System.DateTime(2100, 1, 1, 0, 0, 0, 0);
            this.ucPeriod.Location = new System.Drawing.Point(66, 6);
            this.ucPeriod.Name = "ucPeriod";
            this.ucPeriod.Size = new System.Drawing.Size(222, 21);
            this.ucPeriod.TabIndex = 170;
            // 
            // ORNDisFormParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 324);
            this.Controls.Add(this.chkShortReport);
            this.Controls.Add(this.chkColumnSale);
            this.Controls.Add(this.chkDateReport);
            this.Controls.Add(this.mpsStore);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkRefreshDocMov);
            this.Controls.Add(this.mpsContractor);
            this.Controls.Add(this.ucPeriod);
            this.Name = "ORNDisFormParams";
            this.Text = "Параметры внешнего отчета";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkShortReport;
        private System.Windows.Forms.CheckBox chkColumnSale;
        private System.Windows.Forms.CheckBox chkDateReport;
        private ePlus.MetaData.Client.UCPluginMultiSelect mpsStore;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkShowSub;
        private System.Windows.Forms.CheckBox chkShowAdd;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkShowSumByDocType;
        private System.Windows.Forms.RadioButton rbDocDate;
        private System.Windows.Forms.RadioButton rbDocType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkRefreshDocMov;
        private ePlus.MetaData.Client.UCPluginMultiSelect mpsContractor;
        private ePlus.MetaData.Client.UCPeriod ucPeriod;
    }
}