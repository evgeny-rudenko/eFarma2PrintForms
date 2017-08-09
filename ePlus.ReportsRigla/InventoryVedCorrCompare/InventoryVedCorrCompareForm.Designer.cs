namespace InventoryVedCorrCompare
{
    partial class InventoryVedCorrCompareForm
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
            this.ucInventoryVed = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(380, 3);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(455, 3);
            // 
            // panel1
            // 
            this.panel1.Size = new System.Drawing.Size(533, 29);
            this.panel1.TabIndex = 1;
            // 
            // ucInventoryVed
            // 
            this.ucInventoryVed.AllowSaveState = false;
            this.ucInventoryVed.Caption = "";
            this.ucInventoryVed.Location = new System.Drawing.Point(12, 12);
            this.ucInventoryVed.Mnemocode = "INVENTORY_VED";
            this.ucInventoryVed.Name = "ucInventoryVed";
            this.ucInventoryVed.Size = new System.Drawing.Size(509, 219);
            this.ucInventoryVed.TabIndex = 0;
            // 
            // InventoryVedCorrCompareForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 266);
            this.Controls.Add(this.ucInventoryVed);
            this.Name = "InventoryVedCorrCompareForm";
            this.Text = "Параметры отчета";
            this.Controls.SetChildIndex(this.ucInventoryVed, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ePlus.MetaData.Client.UCPluginMultiSelect ucInventoryVed;
    }
}