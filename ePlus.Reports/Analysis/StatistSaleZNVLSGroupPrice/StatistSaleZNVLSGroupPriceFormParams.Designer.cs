namespace RCChStatistSaleZNVLSGroupPrice
{
    partial class StatistSaleZNVLSGroupPriceFormParams
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
            this.contractors = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.labelPeriod = new System.Windows.Forms.Label();
            this.period = new ePlus.MetaData.Client.UCPeriod();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(186, 3);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(261, 3);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 275);
            this.panel1.Size = new System.Drawing.Size(339, 29);
            // 
            // contractors
            // 
            this.contractors.AllowSaveState = false;
            this.contractors.Caption = "Аптеки";
            this.contractors.Location = new System.Drawing.Point(12, 39);
            this.contractors.Mnemocode = "CONTRACTOR";
            this.contractors.Name = "contractors";
            this.contractors.Size = new System.Drawing.Size(319, 220);
            this.contractors.TabIndex = 137;
            this.contractors.BeforePluginShow += new System.ComponentModel.CancelEventHandler(this.contractors_BeforePluginShow);
            // 
            // labelPeriod
            // 
            this.labelPeriod.Location = new System.Drawing.Point(9, 12);
            this.labelPeriod.Name = "labelPeriod";
            this.labelPeriod.Size = new System.Drawing.Size(51, 21);
            this.labelPeriod.TabIndex = 124;
            this.labelPeriod.Text = "Период : ";
            this.labelPeriod.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // period
            // 
            this.period.DateFrom = new System.DateTime(2006, 11, 15, 16, 50, 34, 515);
            this.period.DateTo = new System.DateTime(2006, 11, 15, 16, 50, 34, 515);
            this.period.Location = new System.Drawing.Point(106, 12);
            this.period.Name = "period";
            this.period.Size = new System.Drawing.Size(245, 21);
            this.period.TabIndex = 123;
            // 
            // StatistSaleZNVLSFormParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 304);
            this.Controls.Add(this.labelPeriod);
            this.Controls.Add(this.contractors);
            this.Controls.Add(this.period);
            this.Name = "StatistSaleZNVLSFormParams";
            this.Load += new System.EventHandler(this.FormParams_Load);
            this.Shown += new System.EventHandler(this.FormParams_Shown);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormParams_FormClosed);
            this.Controls.SetChildIndex(this.period, 0);
            this.Controls.SetChildIndex(this.contractors, 0);
            this.Controls.SetChildIndex(this.labelPeriod, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelPeriod;
        public ePlus.MetaData.Client.UCPeriod period;
        //public ePlus.Dictionary.Client.Goods.GoodsGroup.UserGroupControl ugcGroups;
        private ePlus.MetaData.Client.UCPluginMultiSelect contractors;
    }
}