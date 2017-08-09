namespace TO_Planet_Ex
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
            this.mpsContractor = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.label1 = new System.Windows.Forms.Label();
            this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
            this.SuspendLayout();
            // 
            // mpsContractor
            // 
            this.mpsContractor.AllowSaveState = false;
            this.mpsContractor.Caption = "Контрагенты";
            this.mpsContractor.Location = new System.Drawing.Point(12, 48);
            this.mpsContractor.Mnemocode = "CONTRACTOR";
            this.mpsContractor.Name = "mpsContractor";
            this.mpsContractor.Size = new System.Drawing.Size(408, 74);
            this.mpsContractor.TabIndex = 161;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 163;
            this.label1.Text = "Период:";
            // 
            // ucPeriod
            // 
            this.ucPeriod.DateFrom = new System.DateTime(((long)(0)));
            this.ucPeriod.DateTo = new System.DateTime(2100, 1, 1, 0, 0, 0, 0);
            this.ucPeriod.Location = new System.Drawing.Point(64, 12);
            this.ucPeriod.Name = "ucPeriod";
            this.ucPeriod.Size = new System.Drawing.Size(222, 21);
            this.ucPeriod.TabIndex = 162;
            // 
            // FormParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 177);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ucPeriod);
            this.Controls.Add(this.mpsContractor);
            this.Name = "FormParams";
            this.Text = "Параметры внешнего отчета";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ePlus.MetaData.Client.UCPluginMultiSelect mpsContractor;
        private System.Windows.Forms.Label label1;
        private ePlus.MetaData.Client.UCPeriod ucPeriod;
    }
}