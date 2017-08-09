namespace SumSupAndDiscountForRigla
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
            this.labelPeriod = new System.Windows.Forms.Label();
            this.period = new ePlus.MetaData.Client.UCPeriod();
            this.SuspendLayout();
            // 
            // labelPeriod
            // 
            this.labelPeriod.Location = new System.Drawing.Point(8, 12);
            this.labelPeriod.Name = "labelPeriod";
            this.labelPeriod.Size = new System.Drawing.Size(51, 21);
            this.labelPeriod.TabIndex = 143;
            this.labelPeriod.Text = "Период : ";
            this.labelPeriod.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // period
            // 
            this.period.DateFrom = new System.DateTime(2006, 11, 15, 16, 50, 34, 515);
            this.period.DateTo = new System.DateTime(2006, 11, 15, 16, 50, 34, 515);
            this.period.Location = new System.Drawing.Point(65, 12);
            this.period.Name = "period";
            this.period.Size = new System.Drawing.Size(195, 21);
            this.period.TabIndex = 142;
            // 
            // FormParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 106);
            this.Controls.Add(this.labelPeriod);
            this.Controls.Add(this.period);
            this.Name = "FormParams";
            this.Text = "Параметры внешнего отчета";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelPeriod;
        public ePlus.MetaData.Client.UCPeriod period;
    }
}