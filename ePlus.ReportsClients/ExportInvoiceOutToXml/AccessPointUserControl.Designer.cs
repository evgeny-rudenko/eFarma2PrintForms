using ePlus.CommonEx.Controls;

namespace ExportInvoiceOutToXml
{
    partial class AccessPointUserControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.ucAccessPoint = new ePlus.CommonEx.Controls.MetaPluginDictionarySelectControl();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Точка доступа для экспорта:";
            // 
            // ucAccessPoint
            // 
            this.ucAccessPoint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ucAccessPoint.ClearTextOnValidatingIfValueIsEmpty = true;
            this.ucAccessPoint.ELikeTextOption = ePlus.MetaData.Core.ELikeTextOption.None;
            this.ucAccessPoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic);
            this.ucAccessPoint.Location = new System.Drawing.Point(14, 25);
            this.ucAccessPoint.Name = "ucAccessPoint";
            this.ucAccessPoint.PluginMnemocode = "ACCESS_POINT";
            this.ucAccessPoint.SelectNextControlAfterSelectEntity = false;
            this.ucAccessPoint.Size = new System.Drawing.Size(228, 20);
            this.ucAccessPoint.TabIndex = 1;
            this.ucAccessPoint.UseEnterToOpenPlugin = true;
            this.ucAccessPoint.UseSpaceToOpenPlugin = true;
            // 
            // AccessPointUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ucAccessPoint);
            this.Name = "AccessPointUserControl";
            this.Size = new System.Drawing.Size(251, 68);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private MetaPluginDictionarySelectControl ucAccessPoint;
    }
}
