namespace SlichititStatementEx
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
			this.mpsInventorySved = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// bOK
			// 
			this.bOK.Location = new System.Drawing.Point(273, 3);
			this.bOK.Click += new System.EventHandler(this.bOK_Click_1);
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
			// mpsInventorySved
			// 
			this.mpsInventorySved.AllowSaveState = false;
			this.mpsInventorySved.Caption = "Сводная инвентаризация";
			this.mpsInventorySved.Location = new System.Drawing.Point(6, 12);
			this.mpsInventorySved.Mnemocode = "INVENTORY_SVED";
			this.mpsInventorySved.Name = "mpsInventorySved";
			this.mpsInventorySved.Size = new System.Drawing.Size(408, 226);
			this.mpsInventorySved.TabIndex = 170;
			// 
			// FormParams
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(426, 273);
			this.Controls.Add(this.mpsInventorySved);
			this.KeyPreview = true;
			this.Name = "FormParams";
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormParams_KeyPress);
			this.Controls.SetChildIndex(this.mpsInventorySved, 0);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private ePlus.MetaData.Client.UCPluginMultiSelect mpsInventorySved;

    }
}