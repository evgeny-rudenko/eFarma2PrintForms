using ePlus.MetaData.Client;

namespace NonLiquidGoodsEsEx
{
    partial class NonLiquidGoodsEsExForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.period = new ePlus.MetaData.Client.UCPeriod();
            this.store = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.chkShowLots = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(338, 3);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(413, 3);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkShowLots);
            this.panel1.Location = new System.Drawing.Point(0, 159);
            this.panel1.Size = new System.Drawing.Size(491, 29);
            this.panel1.Controls.SetChildIndex(this.bClose, 0);
            this.panel1.Controls.SetChildIndex(this.chkShowLots, 0);
            this.panel1.Controls.SetChildIndex(this.bOK, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 107;
            this.label1.Text = "Период:";
            // 
            // period
            // 
            this.period.DateFrom = new System.DateTime(2006, 10, 30, 14, 2, 0, 906);
            this.period.DateTo = new System.DateTime(2006, 10, 30, 14, 2, 0, 921);
            this.period.Location = new System.Drawing.Point(66, 13);
            this.period.Name = "period";
            this.period.Size = new System.Drawing.Size(229, 21);
            this.period.TabIndex = 109;
            // 
            // store
            // 
            this.store.AllowSaveState = false;
            this.store.Caption = "Склады";
            this.store.Location = new System.Drawing.Point(12, 40);
            this.store.Mnemocode = "STORE";
            this.store.Name = "store";
            this.store.Size = new System.Drawing.Size(467, 113);
            this.store.TabIndex = 110;
            // 
            // chkShowLots
            // 
            this.chkShowLots.AutoSize = true;
            this.chkShowLots.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkShowLots.Location = new System.Drawing.Point(12, 7);
            this.chkShowLots.Name = "chkShowLots";
            this.chkShowLots.Size = new System.Drawing.Size(127, 17);
            this.chkShowLots.TabIndex = 111;
            this.chkShowLots.Text = "Показывать партии";
            this.chkShowLots.UseVisualStyleBackColor = true;
            // 
            // NonLiquidGoodsEsExForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(491, 188);
            this.Controls.Add(this.period);
            this.Controls.Add(this.store);
            this.Controls.Add(this.label1);
            this.Name = "NonLiquidGoodsEsExForm";
            this.Text = "Параметры отчета по неликвидным товарам";
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.store, 0);
            this.Controls.SetChildIndex(this.period, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private UCPeriod period;
        private UCPluginMultiSelect store;
        private System.Windows.Forms.CheckBox chkShowLots;
    }
}