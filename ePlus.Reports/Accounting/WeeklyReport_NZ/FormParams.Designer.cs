namespace R32PWeeklyReport_NZ
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
            this.ucPeriod1 = new ePlus.MetaData.Client.UCPeriod();
            this.label3 = new System.Windows.Forms.Label();
            this.ucContractor = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(223, 3);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(298, 3);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 250);
            this.panel1.Size = new System.Drawing.Size(376, 29);
            // 
            // ucPeriod1
            // 
            this.ucPeriod1.DateFrom = new System.DateTime(2006, 10, 30, 14, 2, 0, 906);
            this.ucPeriod1.DateTo = new System.DateTime(2006, 10, 30, 14, 2, 0, 921);
            this.ucPeriod1.Location = new System.Drawing.Point(81, 12);
            this.ucPeriod1.Name = "ucPeriod1";
            this.ucPeriod1.Size = new System.Drawing.Size(285, 21);
            this.ucPeriod1.TabIndex = 116;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 117;
            this.label3.Text = "Период:";
            // 
            // ucContractor
            // 
            this.ucContractor.AllowSaveState = true;
            this.ucContractor.Caption = "Аптеки";
            this.ucContractor.Location = new System.Drawing.Point(12, 39);
            this.ucContractor.Mnemocode = "CONTRACTOR";
            this.ucContractor.Name = "ucContractor";
            this.ucContractor.Pinnable = false;
            this.ucContractor.Size = new System.Drawing.Size(354, 201);
            this.ucContractor.TabIndex = 118;
            // 
            // FormParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 279);
            this.Controls.Add(this.ucPeriod1);
            this.Controls.Add(this.ucContractor);
            this.Controls.Add(this.label3);
            this.Name = "FormParams";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultLocation;
            this.Load += new System.EventHandler(this.FormParams_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormParams_FormClosed);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.ucContractor, 0);
            this.Controls.SetChildIndex(this.ucPeriod1, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ePlus.MetaData.Client.UCPeriod ucPeriod1;
        private System.Windows.Forms.Label label3;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucContractor;
    }
}