namespace RCUDReportCourieService
{
    partial class ReportCourieService
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ucContractors = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.selFixReport = new System.Windows.Forms.RadioButton();
            this.selReport = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDateReport = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.selDeliveryService = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(270, 3);
            this.bOK.Text = "Запустить";
            this.bOK.Click += new System.EventHandler(this.bOK_Click_1);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(345, 3);
            this.bClose.Click += new System.EventHandler(this.bClose_Click_1);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 149);
            this.panel1.Size = new System.Drawing.Size(423, 29);
            // 
            // ucContractors
            // 
            this.ucContractors.AllowSaveState = true;
            this.ucContractors.Caption = "Аптеки";
            this.ucContractors.Enabled = false;
            this.ucContractors.Location = new System.Drawing.Point(12, 133);
            this.ucContractors.Mnemocode = "CONTRACTOR";
            this.ucContractors.MultiSelect = true;
            this.ucContractors.Name = "ucContractors";
            this.ucContractors.Pinnable = false;
            this.ucContractors.Size = new System.Drawing.Size(45, 10);
            this.ucContractors.TabIndex = 135;
            this.ucContractors.Visible = false;
            // 
            // selFixReport
            // 
            this.selFixReport.AutoSize = true;
            this.selFixReport.Location = new System.Drawing.Point(58, 89);
            this.selFixReport.Name = "selFixReport";
            this.selFixReport.Size = new System.Drawing.Size(337, 17);
            this.selFixReport.TabIndex = 136;
            this.selFixReport.Text = "Исправить статусы Интернет-заказов и сформировать отчет";
            this.selFixReport.UseVisualStyleBackColor = true;
            this.selFixReport.CheckedChanged += new System.EventHandler(this.selFixReport_CheckedChanged);
            // 
            // selReport
            // 
            this.selReport.AutoSize = true;
            this.selReport.Checked = true;
            this.selReport.Location = new System.Drawing.Point(58, 66);
            this.selReport.Name = "selReport";
            this.selReport.Size = new System.Drawing.Size(131, 17);
            this.selReport.TabIndex = 137;
            this.selReport.TabStop = true;
            this.selReport.Text = "Сформировать отчет";
            this.selReport.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(91, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 139;
            this.label2.Text = "Отчет на дату:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // txtDateReport
            // 
            this.txtDateReport.Location = new System.Drawing.Point(176, 10);
            this.txtDateReport.Name = "txtDateReport";
            this.txtDateReport.Size = new System.Drawing.Size(143, 20);
            this.txtDateReport.TabIndex = 140;
            this.txtDateReport.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(63, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 141;
            this.label1.Text = "Курьерская служба";
            // 
            // selDeliveryService
            // 
            this.selDeliveryService.FormattingEnabled = true;
            this.selDeliveryService.Items.AddRange(new object[] {
            "CDL",
            "Альянс"});
            this.selDeliveryService.Location = new System.Drawing.Point(176, 39);
            this.selDeliveryService.Name = "selDeliveryService";
            this.selDeliveryService.Size = new System.Drawing.Size(143, 21);
            this.selDeliveryService.TabIndex = 142;
            this.selDeliveryService.Text = "CDL";
            // 
            // ReportCourieService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 178);
            this.Controls.Add(this.selDeliveryService);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDateReport);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ucContractors);
            this.Controls.Add(this.selReport);
            this.Controls.Add(this.selFixReport);
            this.Name = "ReportCourieService";
            this.Text = "ДС от Курьерской службы";
            this.Controls.SetChildIndex(this.selFixReport, 0);
            this.Controls.SetChildIndex(this.selReport, 0);
            this.Controls.SetChildIndex(this.ucContractors, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.txtDateReport, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.selDeliveryService, 0);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ePlus.MetaData.Client.UCPluginMultiSelect ucContractors;
        private System.Windows.Forms.RadioButton selFixReport;
        private System.Windows.Forms.RadioButton selReport;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker txtDateReport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox selDeliveryService;        
    }
}
