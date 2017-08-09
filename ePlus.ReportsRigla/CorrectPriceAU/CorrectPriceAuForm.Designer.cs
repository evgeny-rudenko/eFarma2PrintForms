namespace CorrectPriceAU
{
    partial class CorrectPriceAuForm
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
            this.ucPeriod = new ePlus.MetaData.Controls.UCPeriod();
            this.ucDrugStore = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.ucProducer = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.ucContractor = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.ucGoods = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(494, 3);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(569, 3);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 245);
            this.panel1.Size = new System.Drawing.Size(647, 29);
            this.panel1.TabIndex = 6;
            // 
            // ucPeriod
            // 
            this.ucPeriod.DateFrom = new System.DateTime(2009, 5, 13, 13, 41, 14, 156);
            this.ucPeriod.DateTo = new System.DateTime(2009, 5, 13, 13, 41, 14, 156);
            this.ucPeriod.Location = new System.Drawing.Point(67, 8);
            this.ucPeriod.Name = "ucPeriod";
            this.ucPeriod.Size = new System.Drawing.Size(222, 21);
            this.ucPeriod.TabIndex = 1;
            // 
            // ucDrugStore
            // 
            this.ucDrugStore.AllowSaveState = false;
            this.ucDrugStore.Caption = "";
            this.ucDrugStore.Location = new System.Drawing.Point(13, 35);
            this.ucDrugStore.Mnemocode = "CONTRACTOR";
            this.ucDrugStore.Name = "ucDrugStore";
            this.ucDrugStore.Size = new System.Drawing.Size(310, 101);
            this.ucDrugStore.TabIndex = 2;
            // 
            // ucProducer
            // 
            this.ucProducer.AllowSaveState = false;
            this.ucProducer.Caption = "";
            this.ucProducer.Location = new System.Drawing.Point(329, 35);
            this.ucProducer.Mnemocode = "PRODUCER";
            this.ucProducer.Name = "ucProducer";
            this.ucProducer.Size = new System.Drawing.Size(310, 101);
            this.ucProducer.TabIndex = 3;
            // 
            // ucContractor
            // 
            this.ucContractor.AllowSaveState = false;
            this.ucContractor.Caption = "Поставщик";
            this.ucContractor.Location = new System.Drawing.Point(13, 142);
            this.ucContractor.Mnemocode = "CONTRACTOR";
            this.ucContractor.Name = "ucContractor";
            this.ucContractor.Size = new System.Drawing.Size(310, 101);
            this.ucContractor.TabIndex = 4;
            // 
            // ucGoods
            // 
            this.ucGoods.AllowSaveState = false;
            this.ucGoods.Caption = "";
            this.ucGoods.Location = new System.Drawing.Point(329, 142);
            this.ucGoods.Mnemocode = "GOODS2";
            this.ucGoods.Name = "ucGoods";
            this.ucGoods.Size = new System.Drawing.Size(310, 101);
            this.ucGoods.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Период:";
            // 
            // CorrectPriceAuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 274);
            this.Controls.Add(this.ucGoods);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ucPeriod);
            this.Controls.Add(this.ucDrugStore);
            this.Controls.Add(this.ucProducer);
            this.Controls.Add(this.ucContractor);
            this.Name = "CorrectPriceAuForm";
            this.Text = "CorrectPriceAuForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CorrectPriceAuForm_FormClosed);
            this.Load += new System.EventHandler(this.CorrectPriceAuForm_Load);
            this.Controls.SetChildIndex(this.ucContractor, 0);
            this.Controls.SetChildIndex(this.ucProducer, 0);
            this.Controls.SetChildIndex(this.ucDrugStore, 0);
            this.Controls.SetChildIndex(this.ucPeriod, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.ucGoods, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ePlus.MetaData.Controls.UCPeriod ucPeriod;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucDrugStore;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucProducer;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucContractor;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucGoods;
        private System.Windows.Forms.Label label1;
    }
}