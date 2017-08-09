namespace ActReturnTORG2Ex
{
    partial class ChooseContractor
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
            this.btAccept = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.ucContractor = new ePlus.MetaData.Client.UCMetaPluginSelect();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btAccept
            // 
            this.btAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btAccept.Location = new System.Drawing.Point(168, 46);
            this.btAccept.Name = "btAccept";
            this.btAccept.Size = new System.Drawing.Size(75, 25);
            this.btAccept.TabIndex = 0;
            this.btAccept.Text = "Выбрать";
            this.btAccept.UseVisualStyleBackColor = true;
            this.btAccept.Click += new System.EventHandler(this.button1_Click);
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btClose.Location = new System.Drawing.Point(249, 46);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(77, 25);
            this.btClose.TabIndex = 1;
            this.btClose.Text = "Закрыть";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.button2_Click);
            // 
            // ucContractor
            // 
            this.ucContractor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ucContractor.ButtonStyle = ePlus.MetaData.Client.EButtonStyle.SelectClear;
            this.ucContractor.Location = new System.Drawing.Point(86, 18);
            this.ucContractor.Mnemocode = "CONTRACTOR";
            this.ucContractor.Name = "ucContractor";
            this.ucContractor.Size = new System.Drawing.Size(240, 21);
            this.ucContractor.TabIndex = 183;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 182;
            this.label6.Text = "Контрагент:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 82);
            this.Controls.Add(this.ucContractor);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.btAccept);
            this.MinimumSize = new System.Drawing.Size(350, 120);
            this.Name = "Form1";
            this.Text = "Выберите поставщика";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btAccept;
        private System.Windows.Forms.Button btClose;
        private ePlus.MetaData.Client.UCMetaPluginSelect ucContractor;
        private System.Windows.Forms.Label label6;
    }
}