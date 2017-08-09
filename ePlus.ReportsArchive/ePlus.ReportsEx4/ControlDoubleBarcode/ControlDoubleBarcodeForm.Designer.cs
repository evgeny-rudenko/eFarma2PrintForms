namespace ControlDoubleBarcode
{
    partial class ControlDoubleBarcodeForm
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
            this.chbStock = new System.Windows.Forms.CheckBox();
            this.chbVisibleCode = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(138, 3);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(213, 3);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 56);
            this.panel1.Size = new System.Drawing.Size(291, 29);
            this.panel1.TabIndex = 6;
            // 
            // chbStock
            // 
            this.chbStock.AutoSize = true;
            this.chbStock.Checked = true;
            this.chbStock.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbStock.Location = new System.Drawing.Point(12, 12);
            this.chbStock.Name = "chbStock";
            this.chbStock.Size = new System.Drawing.Size(106, 17);
            this.chbStock.TabIndex = 8;
            this.chbStock.Text = "Только остатки";
            this.chbStock.UseVisualStyleBackColor = true;
            // 
            // chbVisibleCode
            // 
            this.chbVisibleCode.AutoSize = true;
            this.chbVisibleCode.Location = new System.Drawing.Point(12, 35);
            this.chbVisibleCode.Name = "chbVisibleCode";
            this.chbVisibleCode.Size = new System.Drawing.Size(147, 17);
            this.chbVisibleCode.TabIndex = 8;
            this.chbVisibleCode.Text = "Отображать код товара";
            this.chbVisibleCode.UseVisualStyleBackColor = true;
            // 
            // ControlDoubleBarcodeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 85);
            this.Controls.Add(this.chbVisibleCode);
            this.Controls.Add(this.chbStock);
            this.MinimumSize = new System.Drawing.Size(299, 119);
            this.Name = "ControlDoubleBarcodeForm";
            this.Text = "Отчет контроль дублей чеков";
            this.Controls.SetChildIndex(this.chbStock, 0);
            this.Controls.SetChildIndex(this.chbVisibleCode, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chbStock;
        private System.Windows.Forms.CheckBox chbVisibleCode;

    }
}