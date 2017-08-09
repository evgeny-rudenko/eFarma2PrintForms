namespace RCSLotHistory
{
    partial class UCMetaPluginSelect
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.textText = new System.Windows.Forms.TextBox();
            this.buttonSelect = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textText
            // 
            this.textText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.textText.BackColor = System.Drawing.Color.White;
            this.textText.Location = new System.Drawing.Point(0, 0);
            this.textText.Name = "textText";
            this.textText.ReadOnly = true;
            this.textText.Size = new System.Drawing.Size(177, 20);
            this.textText.TabIndex = 0;
            this.textText.TabStop = false;
            this.textText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextText_KeyDown);
            // 
            // buttonSelect
            // 
            this.buttonSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSelect.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonSelect.Location = new System.Drawing.Point(202, 0);
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.Size = new System.Drawing.Size(27, 20);
            this.buttonSelect.TabIndex = 1;
            this.buttonSelect.Text = "...";
            this.buttonSelect.UseVisualStyleBackColor = true;
            this.buttonSelect.Click += new System.EventHandler(this.ButtonSelect_Click);
            this.buttonSelect.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ButtonSelect_KeyDown);
            // 
            // buttonClear
            // 
            this.buttonClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClear.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonClear.Location = new System.Drawing.Point(235, 0);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(27, 20);
            this.buttonClear.TabIndex = 2;
            this.buttonClear.Text = "X";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.ButtonClear_Click);
            this.buttonClear.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ButtonClear_KeyDown);
            // 
            // UCMetaPluginSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonSelect);
            this.Controls.Add(this.textText);
            this.Name = "UCMetaPluginSelect";
            this.Size = new System.Drawing.Size(262, 21);
            this.Enter += new System.EventHandler(this.UCMetaPluginSelect_Enter);
            this.Leave += new System.EventHandler(this.UCMetaPluginSelect_Leave);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textText;
        private System.Windows.Forms.Button buttonSelect;
        private System.Windows.Forms.Button buttonClear;
    }
}
