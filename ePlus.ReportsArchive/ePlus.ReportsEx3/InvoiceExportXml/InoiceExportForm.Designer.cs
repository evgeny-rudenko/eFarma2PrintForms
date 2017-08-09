namespace InvoiceExportXml
{
    partial class InoiceExportForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.ucAccessPoint = new ePlus.CommonEx.Controls.MetaPluginDictionarySelectControl();
            this.label1 = new System.Windows.Forms.Label();
            this.ucInvoice = new ePlus.CommonEx.Controls.MetaPluginDictionarySelectControl();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(242, 3);
            this.bOK.Text = "&Экспорт";
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(317, 3);
            this.bClose.Text = "&Закрыть";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 71);
            this.panel1.Size = new System.Drawing.Size(395, 29);
            this.panel1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(93, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Куда:";
            // 
            // ucAccessPoint
            // 
            this.ucAccessPoint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ucAccessPoint.ELikeTextOption = ePlus.MetaData.Core.ELikeTextOption.None;
            this.ucAccessPoint.Location = new System.Drawing.Point(130, 12);
            this.ucAccessPoint.Name = "ucAccessPoint";
            this.ucAccessPoint.PluginMnemocode = "ACCESS_POINT";
            this.ucAccessPoint.ReadOnly = true;
            this.ucAccessPoint.Size = new System.Drawing.Size(262, 20);
            this.ucAccessPoint.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Приходная накладная:";
            // 
            // ucInvoice
            // 
            this.ucInvoice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ucInvoice.ELikeTextOption = ePlus.MetaData.Core.ELikeTextOption.None;
            this.ucInvoice.Location = new System.Drawing.Point(130, 42);
            this.ucInvoice.Name = "ucInvoice";
            this.ucInvoice.PluginMnemocode = "INVOICE";
            this.ucInvoice.ReadOnly = true;
            this.ucInvoice.Size = new System.Drawing.Size(262, 20);
            this.ucInvoice.TabIndex = 3;
            // 
            // InoiceExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 100);
            this.Controls.Add(this.ucInvoice);
            this.Controls.Add(this.ucAccessPoint);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InoiceExportForm";
            this.Text = "Выгрузка остатков";
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.ucAccessPoint, 0);
            this.Controls.SetChildIndex(this.ucInvoice, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private ePlus.CommonEx.Controls.MetaPluginDictionarySelectControl ucAccessPoint;
        private System.Windows.Forms.Label label1;
        private ePlus.CommonEx.Controls.MetaPluginDictionarySelectControl ucInvoice;

    }
}

