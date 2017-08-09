namespace RCSUserLog_Rigla
{
    partial class UserLogForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.ucStore = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.ucUser = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.ucGoods = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(175, 3);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(250, 3);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 486);
            this.panel1.Size = new System.Drawing.Size(328, 29);
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
            this.ucDrugStore.Caption = "Аптека";
            this.ucDrugStore.Location = new System.Drawing.Point(13, 35);
            this.ucDrugStore.Mnemocode = "CONTRACTOR";
            this.ucDrugStore.Name = "ucDrugStore";
            //this.ucDrugStore.Pinnable = false;
            this.ucDrugStore.Size = new System.Drawing.Size(310, 101);
            this.ucDrugStore.TabIndex = 2;
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
            // ucStore
            // 
            this.ucStore.AllowSaveState = false;
            this.ucStore.Caption = "Склад";
            this.ucStore.Location = new System.Drawing.Point(13, 142);
            this.ucStore.Mnemocode = "STORE";
            this.ucStore.Name = "ucStore";
            //this.ucStore.Pinnable = false;
            this.ucStore.Size = new System.Drawing.Size(310, 101);
            this.ucStore.TabIndex = 4;
            // 
            // ucUser
            // 
            this.ucUser.AllowSaveState = false;
            this.ucUser.Caption = "Кассир";
            this.ucUser.Location = new System.Drawing.Point(13, 249);
            this.ucUser.Mnemocode = "CASH_REGISTER_USER";
            this.ucUser.Name = "ucUser";
           // this.ucUser.Pinnable = false;
            this.ucUser.Size = new System.Drawing.Size(310, 101);
            this.ucUser.TabIndex = 4;
            // 
            // ucGoods
            // 
            this.ucGoods.AllowSaveState = false;
            this.ucGoods.Caption = "Товары";
            this.ucGoods.Location = new System.Drawing.Point(12, 365);
            this.ucGoods.Mnemocode = "GOODS2";
            this.ucGoods.Name = "ucGoods";
            //this.ucGoods.Pinnable = false;
            this.ucGoods.Size = new System.Drawing.Size(310, 101);
            this.ucGoods.TabIndex = 7;
            // 
            // UserLogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 515);
            this.Controls.Add(this.ucGoods);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ucPeriod);
            this.Controls.Add(this.ucDrugStore);
            this.Controls.Add(this.ucStore);
            this.Controls.Add(this.ucUser);
            this.Name = "UserLogForm";
            this.Text = "CorrectPriceAuForm";
            this.Load += new System.EventHandler(this.UserLogForm_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.UserLogForm_FormClosed);
            this.Controls.SetChildIndex(this.ucUser, 0);
            this.Controls.SetChildIndex(this.ucStore, 0);
            this.Controls.SetChildIndex(this.ucDrugStore, 0);
            this.Controls.SetChildIndex(this.ucPeriod, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.ucGoods, 0);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ePlus.MetaData.Controls.UCPeriod ucPeriod;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucDrugStore;
        private System.Windows.Forms.Label label1;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucStore;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucUser;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucGoods;
    }
}