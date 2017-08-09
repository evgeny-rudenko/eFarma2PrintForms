namespace RCBTorg29ORNDisGroup
{
    partial class NumRepForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.nbNum = new ePlus.CommonEx.Controls.ePlusNumericBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Табельный номер:";
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(222, 0);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(49, 21);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonOK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 45);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(273, 21);
            this.panel1.TabIndex = 4;
            // 
            // nbNum
            // 
            this.nbNum.DecimalPlaces = 0;
            this.nbNum.DecimalSeparator = '.';
            this.nbNum.Location = new System.Drawing.Point(112, 12);
            this.nbNum.MaxLength = 18;
            this.nbNum.Name = "nbNum";
            this.nbNum.Positive = true;
            this.nbNum.Size = new System.Drawing.Size(159, 20);
            this.nbNum.TabIndex = 5;
            this.nbNum.Text = "0";
            this.nbNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nbNum.ThousandSeparator = ' ';
            this.nbNum.TypingMode = ePlus.CommonEx.Controls.TypingMode.Replace;
            this.nbNum.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // NumRepForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(273, 66);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nbNum);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NumRepForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Табельный номер";
            this.Load += new System.EventHandler(this.NumRepForm_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Panel panel1;
        private ePlus.CommonEx.Controls.ePlusNumericBox nbNum;
    }
}