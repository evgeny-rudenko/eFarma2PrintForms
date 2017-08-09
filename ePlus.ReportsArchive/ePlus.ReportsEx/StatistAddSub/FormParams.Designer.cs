namespace StatistAddSub
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
            this.components = new System.ComponentModel.Container();
            this.labelDocuments = new System.Windows.Forms.Label();
            this.period = new ePlus.MetaData.Client.UCPeriod();
            this.labelPeriod = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ucContractor = new ePlus.MetaData.Client.UCMetaPluginSelect();
            this.documents = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.documents)).BeginInit();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(318, 3);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(393, 3);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 399);
            this.panel1.Size = new System.Drawing.Size(471, 29);
            // 
            // labelDocuments
            // 
            this.labelDocuments.Location = new System.Drawing.Point(21, 82);
            this.labelDocuments.Name = "labelDocuments";
            this.labelDocuments.Size = new System.Drawing.Size(110, 20);
            this.labelDocuments.TabIndex = 111;
            this.labelDocuments.Text = "Виды документов : ";
            this.labelDocuments.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // period
            // 
            this.period.DateFrom = new System.DateTime(2006, 12, 25, 12, 31, 21, 187);
            this.period.DateTo = new System.DateTime(2006, 12, 25, 12, 31, 21, 187);
            this.period.Location = new System.Drawing.Point(116, 12);
            this.period.Name = "period";
            this.period.Size = new System.Drawing.Size(222, 21);
            this.period.TabIndex = 110;
            // 
            // labelPeriod
            // 
            this.labelPeriod.Location = new System.Drawing.Point(21, 12);
            this.labelPeriod.Name = "labelPeriod";
            this.labelPeriod.Size = new System.Drawing.Size(51, 20);
            this.labelPeriod.TabIndex = 109;
            this.labelPeriod.Text = "Период : ";
            this.labelPeriod.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 113;
            this.label1.Text = "Контрагент";
            // 
            // ucContractor
            // 
            this.ucContractor.ButtonStyle = ePlus.MetaData.Client.EButtonStyle.SelectClear;
            this.ucContractor.Location = new System.Drawing.Point(91, 48);
            this.ucContractor.Mnemocode = "CONTRACTOR";
            this.ucContractor.Name = "ucContractor";
            this.ucContractor.Size = new System.Drawing.Size(262, 21);
            this.ucContractor.TabIndex = 112;
            // 
            // documents
            // 
            this.documents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.documents.Location = new System.Drawing.Point(24, 116);
            this.documents.Name = "documents";
            this.documents.Size = new System.Drawing.Size(329, 240);
            this.documents.TabIndex = 114;
            // 
            // FormParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 428);
            this.Controls.Add(this.documents);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ucContractor);
            this.Controls.Add(this.labelDocuments);
            this.Controls.Add(this.period);
            this.Controls.Add(this.labelPeriod);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormParams";
            this.Load += new System.EventHandler(this.FormParams_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.labelPeriod, 0);
            this.Controls.SetChildIndex(this.period, 0);
            this.Controls.SetChildIndex(this.labelDocuments, 0);
            this.Controls.SetChildIndex(this.ucContractor, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.documents, 0);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.documents)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelDocuments;
        private ePlus.MetaData.Client.UCPeriod period;
        private System.Windows.Forms.Label labelPeriod;
        private System.Windows.Forms.Label label1;
        private ePlus.MetaData.Client.UCMetaPluginSelect ucContractor;
        private System.Windows.Forms.DataGridView documents;
    }
}