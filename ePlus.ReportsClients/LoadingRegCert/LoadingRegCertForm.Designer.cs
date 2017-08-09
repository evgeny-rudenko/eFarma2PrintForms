using ePlus.MetaData.Client;

namespace LoadingRegCert
{
    partial class LoadingRegCertForm
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
            this.ucInvoice = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chlbFiles = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbOrgan = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnFiles = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(296, 3);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(371, 3);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 414);
            this.panel1.Size = new System.Drawing.Size(449, 29);
            // 
            // ucInvoice
            // 
            this.ucInvoice.AllowSaveState = false;
            this.ucInvoice.Caption = "";
            this.ucInvoice.Location = new System.Drawing.Point(3, 0);
            this.ucInvoice.Mnemocode = "INVOICE";
            this.ucInvoice.Name = "ucInvoice";
            this.ucInvoice.Size = new System.Drawing.Size(447, 147);
            this.ucInvoice.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnFiles);
            this.panel2.Controls.Add(this.ucInvoice);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(528, 180);
            this.panel2.TabIndex = 6;
            // 
            // chlbFiles
            // 
            this.chlbFiles.FormattingEnabled = true;
            this.chlbFiles.Location = new System.Drawing.Point(3, 186);
            this.chlbFiles.Name = "chlbFiles";
            this.chlbFiles.Size = new System.Drawing.Size(412, 199);
            this.chlbFiles.TabIndex = 7;
            this.chlbFiles.ThreeDCheckBoxes = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 392);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Орган выдачи:";
            // 
            // tbOrgan
            // 
            this.tbOrgan.Location = new System.Drawing.Point(90, 389);
            this.tbOrgan.Name = "tbOrgan";
            this.tbOrgan.Size = new System.Drawing.Size(325, 20);
            this.tbOrgan.TabIndex = 9;
            this.tbOrgan.Text = "ГУ КК \"Фармацевтический центр\"";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnFiles
            // 
            this.btnFiles.Location = new System.Drawing.Point(3, 154);
            this.btnFiles.Name = "btnFiles";
            this.btnFiles.Size = new System.Drawing.Size(75, 23);
            this.btnFiles.TabIndex = 10;
            this.btnFiles.Text = "Файлы";
            this.btnFiles.UseVisualStyleBackColor = true;
            this.btnFiles.Click += new System.EventHandler(this.btnFiles_Click);
            // 
            // LoadingRegCertForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 443);
            this.Controls.Add(this.tbOrgan);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chlbFiles);
            this.Controls.Add(this.panel2);
            this.Name = "LoadingRegCertForm";
            this.Text = "Загрузка рег.сертификатов";
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.chlbFiles, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.tbOrgan, 0);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ePlus.MetaData.Client.UCPluginMultiSelect ucInvoice;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckedListBox chlbFiles;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbOrgan;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnFiles;
    }
}

