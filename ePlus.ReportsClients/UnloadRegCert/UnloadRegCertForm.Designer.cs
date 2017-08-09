using ePlus.MetaData.Client;

namespace UnloadRegCert
{
    partial class UnloadRegCertForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.tecItems = new ePlus.MetaData.Core.MetaGe.TableEditorControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tbDirectory = new UnloadRegCert.DirectorySelectControl();
            this.ucInvoice = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(429, 3);
            this.bOK.Text = "Выгрузить";
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(504, 3);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 360);
            this.panel1.Size = new System.Drawing.Size(582, 29);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Директория экспорта:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Приходная накладная:";
            // 
            // tecItems
            // 
            this.tecItems.DataSource = null;
            this.tecItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tecItems.Location = new System.Drawing.Point(0, 134);
            this.tecItems.Margin = new System.Windows.Forms.Padding(0);
            this.tecItems.Mnemocode = "UNLOAD_REG_CERT";
            this.tecItems.Name = "tecItems";
            this.tecItems.ObjectList = null;
            this.tecItems.SearchingEnabled = true;
            this.tecItems.Size = new System.Drawing.Size(582, 226);
            this.tecItems.TabIndex = 17;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.tbDirectory);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.ucInvoice);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(582, 134);
            this.panel2.TabIndex = 18;
            // 
            // tbDirectory
            // 
            this.tbDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDirectory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.tbDirectory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.tbDirectory.ClearTextOnValidatingIfValueIsEmpty = false;
            this.tbDirectory.ELikeTextOption = ePlus.MetaData.Core.ELikeTextOption.None;
            this.tbDirectory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.tbDirectory.Location = new System.Drawing.Point(128, 4);
            this.tbDirectory.Name = "tbDirectory";
            this.tbDirectory.PluginMnemocode = "";
            this.tbDirectory.SelectNextControlAfterSelectEntity = true;
            this.tbDirectory.Size = new System.Drawing.Size(451, 20);
            this.tbDirectory.TabIndex = 4;
            this.tbDirectory.UseEnterToOpenPlugin = true;
            this.tbDirectory.UseSpaceToOpenPlugin = false;
            // 
            // ucInvoice
            // 
            this.ucInvoice.AllowSaveState = false;
            this.ucInvoice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ucInvoice.Caption = "";
            this.ucInvoice.Location = new System.Drawing.Point(125, 27);
            this.ucInvoice.Mnemocode = "INVOICE";
            this.ucInvoice.Name = "ucInvoice";
            this.ucInvoice.Size = new System.Drawing.Size(451, 102);
            this.ucInvoice.TabIndex = 17;
            this.ucInvoice.BeforePluginShow += new System.ComponentModel.CancelEventHandler(this.ucInvoice_BeforePluginShow);
            this.ucInvoice.ValuesListChanged += new ePlus.MetaData.Client.UCPluginMultiSelect.ValuesListChangedDelegate(this.ucContractor_ValuesListChanged);
            // 
            // UnloadRegCertForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 389);
            this.Controls.Add(this.tecItems);
            this.Controls.Add(this.panel2);
            this.Name = "UnloadRegCertForm";
            this.Text = "Выгрузка для рег. серт. ограна";
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.tecItems, 0);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private DirectorySelectControl tbDirectory;
        private System.Windows.Forms.Label label2;
        private ePlus.MetaData.Core.MetaGe.TableEditorControl tecItems;
        private System.Windows.Forms.Panel panel2;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucInvoice;
    }
}

