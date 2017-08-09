namespace RCSNegativeBalance
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
			this.ucStores = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.chbGoodCode = new System.Windows.Forms.CheckBox();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// bOK
			// 
			this.bOK.Location = new System.Drawing.Point(174, 3);
			// 
			// bClose
			// 
			this.bClose.Location = new System.Drawing.Point(249, 3);
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(0, 185);
			this.panel1.Size = new System.Drawing.Size(327, 29);
			// 
			// ucStores
			// 
			this.ucStores.AllowSaveState = false;
			this.ucStores.Caption = "Склад(ы)";
			this.ucStores.Location = new System.Drawing.Point(0, 0);
			this.ucStores.Mnemocode = "STORE";
			this.ucStores.Name = "ucStores";
			this.ucStores.Size = new System.Drawing.Size(324, 154);
			this.ucStores.TabIndex = 1;
			// 
			// chbGoodCode
			// 
			this.chbGoodCode.AutoSize = true;
			this.chbGoodCode.Location = new System.Drawing.Point(12, 160);
			this.chbGoodCode.Name = "chbGoodCode";
			this.chbGoodCode.Size = new System.Drawing.Size(150, 17);
			this.chbGoodCode.TabIndex = 124;
			this.chbGoodCode.Text = "Отображать код товара ";
			this.chbGoodCode.UseVisualStyleBackColor = true;
			// 
			// FormParams
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(327, 214);
			this.Controls.Add(this.chbGoodCode);
			this.Controls.Add(this.ucStores);
			this.Name = "FormParams";
			this.Controls.SetChildIndex(this.ucStores, 0);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.chbGoodCode, 0);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private ePlus.MetaData.Client.UCPluginMultiSelect ucStores;
		private System.Windows.Forms.CheckBox chbGoodCode;
    }
}