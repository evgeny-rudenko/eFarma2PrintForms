using ePlus.CommonEx.Controls;

namespace ImportOrdersExcel
{
    partial class ImportOrdersExcel
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
            this.label1 = new System.Windows.Forms.Label();
            this.ucImport = new ePlus.CommonEx.Controls.MetaPluginDictionarySelectControl();
            this.label2 = new System.Windows.Forms.Label();
            this.tbCatalog = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chbSelectAll = new System.Windows.Forms.CheckBox();
            this.btnSetting = new System.Windows.Forms.Button();
            this.chlbFiles = new System.Windows.Forms.CheckedListBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(236, 3);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(311, 3);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSetting);
            this.panel1.Location = new System.Drawing.Point(0, 272);
            this.panel1.Size = new System.Drawing.Size(389, 29);
            this.panel1.Controls.SetChildIndex(this.btnSetting, 0);
            this.panel1.Controls.SetChildIndex(this.bClose, 0);
            this.panel1.Controls.SetChildIndex(this.bOK, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Настройки импорта заказов:";
            // 
            // ucImport
            // 
            this.ucImport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ucImport.ClearTextOnValidatingIfValueIsEmpty = true;
            this.ucImport.ELikeTextOption = ePlus.MetaData.Core.ELikeTextOption.None;
            this.ucImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic);
            this.ucImport.Location = new System.Drawing.Point(4, 18);
            this.ucImport.Name = "ucImport";
            this.ucImport.PluginMnemocode = "CONFIGURATION_IMPORT";
            this.ucImport.SelectNextControlAfterSelectEntity = false;
            this.ucImport.Size = new System.Drawing.Size(382, 20);
            this.ucImport.TabIndex = 4;
            this.ucImport.UseEnterToOpenPlugin = true;
            this.ucImport.UseSpaceToOpenPlugin = true;
            this.ucImport.ValueChanged += new System.EventHandler(this.ucImport_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Каталог:";
            // 
            // tbCatalog
            // 
            this.tbCatalog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCatalog.Enabled = false;
            this.tbCatalog.Location = new System.Drawing.Point(58, 47);
            this.tbCatalog.Name = "tbCatalog";
            this.tbCatalog.Size = new System.Drawing.Size(328, 20);
            this.tbCatalog.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tbCatalog);
            this.panel2.Controls.Add(this.ucImport);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(389, 73);
            this.panel2.TabIndex = 7;
            // 
            // chbSelectAll
            // 
            this.chbSelectAll.AutoSize = true;
            this.chbSelectAll.Location = new System.Drawing.Point(4, 77);
            this.chbSelectAll.Name = "chbSelectAll";
            this.chbSelectAll.Size = new System.Drawing.Size(97, 17);
            this.chbSelectAll.TabIndex = 8;
            this.chbSelectAll.Text = "Выделить все";
            this.chbSelectAll.UseVisualStyleBackColor = true;
            this.chbSelectAll.CheckedChanged += new System.EventHandler(this.chbSelectAll_CheckedChanged);
            // 
            // btnSetting
            // 
            this.btnSetting.Location = new System.Drawing.Point(4, 3);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(75, 23);
            this.btnSetting.TabIndex = 2;
            this.btnSetting.Text = "Настройка";
            this.btnSetting.UseVisualStyleBackColor = true;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // chlbFiles
            // 
            this.chlbFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chlbFiles.FormattingEnabled = true;
            this.chlbFiles.Location = new System.Drawing.Point(2, 99);
            this.chlbFiles.Name = "chlbFiles";
            this.chlbFiles.Size = new System.Drawing.Size(384, 169);
            this.chlbFiles.TabIndex = 9;
            this.chlbFiles.ThreeDCheckBoxes = true;
            // 
            // ImportOrdersExcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 301);
            this.Controls.Add(this.chlbFiles);
            this.Controls.Add(this.chbSelectAll);
            this.Controls.Add(this.panel2);
            this.MinimumSize = new System.Drawing.Size(397, 334);
            this.Name = "ImportOrdersExcel";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Импорт заказов покупателя";
            this.Load += new System.EventHandler(this.ImportOrdersExcel_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ImportOrdersExcel_FormClosed);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.chbSelectAll, 0);
            this.Controls.SetChildIndex(this.chlbFiles, 0);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private MetaPluginDictionarySelectControl ucImport;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbCatalog;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSetting;
        private System.Windows.Forms.CheckBox chbSelectAll;
        private System.Windows.Forms.CheckedListBox chlbFiles;
    }
}