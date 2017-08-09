namespace UnloadingInvoiceOutEx
{
    partial class FormParam
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
            this.OK = new System.Windows.Forms.Button();
            this.tbIdSupplier = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ucSelectFTP = new ePlus.CommonEx.Controls.MetaPluginDictionarySelectControl();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbFileName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // OK
            // 
            this.OK.Location = new System.Drawing.Point(312, 101);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(75, 23);
            this.OK.TabIndex = 0;
            this.OK.Text = "ОK";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // tbIdSupplier
            // 
            this.tbIdSupplier.Location = new System.Drawing.Point(118, 13);
            this.tbIdSupplier.Name = "tbIdSupplier";
            this.tbIdSupplier.Size = new System.Drawing.Size(269, 20);
            this.tbIdSupplier.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "ID поcтавщика:";
            // 
            // ucSelectFTP
            // 
            this.ucSelectFTP.ELikeTextOption = ePlus.MetaData.Core.ELikeTextOption.None;
            this.ucSelectFTP.Location = new System.Drawing.Point(118, 39);
            this.ucSelectFTP.Name = "ucSelectFTP";
            this.ucSelectFTP.PluginMnemocode = "ACCESS_POINT";
            this.ucSelectFTP.Size = new System.Drawing.Size(269, 20);
            this.ucSelectFTP.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Электронный адрес:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Название файла:";
            // 
            // tbFileName
            // 
            this.tbFileName.Location = new System.Drawing.Point(118, 66);
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.Size = new System.Drawing.Size(269, 20);
            this.tbFileName.TabIndex = 6;
            // 
            // FormParam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 129);
            this.Controls.Add(this.tbFileName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbIdSupplier);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.ucSelectFTP);
            this.MaximumSize = new System.Drawing.Size(407, 237);
            this.MinimumSize = new System.Drawing.Size(407, 156);
            this.Name = "FormParam";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Параметры";
            this.Load += new System.EventHandler(this.FormParam_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormParam_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.TextBox tbIdSupplier;
        private System.Windows.Forms.Label label1;
        private ePlus.CommonEx.Controls.MetaPluginDictionarySelectControl ucSelectFTP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbFileName;
    }
}