namespace SlichitStatementImportInvoice
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
			this.mpsImportRemains = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// bOK
			// 
			this.bOK.Location = new System.Drawing.Point(273, 3);
			// 
			// bClose
			// 
			this.bClose.Location = new System.Drawing.Point(348, 3);
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(0, 244);
			this.panel1.Size = new System.Drawing.Size(426, 29);
			// 
			// mpsImportRemains
			// 
			this.mpsImportRemains.AllowSaveState = false;
			this.mpsImportRemains.Caption = "¬вод остатков";
			this.mpsImportRemains.Location = new System.Drawing.Point(6, 12);
			this.mpsImportRemains.Mnemocode = "IMPORT_REMAINS";
			this.mpsImportRemains.Name = "mpsImportRemains";
			this.mpsImportRemains.Size = new System.Drawing.Size(408, 226);
			this.mpsImportRemains.TabIndex = 170;
			// 
			// FormParams
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(426, 273);
			this.Controls.Add(this.mpsImportRemains);
			this.KeyPreview = true;
			this.Name = "FormParams";
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormParams_KeyPress);
			this.Load += new System.EventHandler(this.FormParams_Load);
			this.Controls.SetChildIndex(this.mpsImportRemains, 0);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private ePlus.MetaData.Client.UCPluginMultiSelect mpsImportRemains;

    }
}