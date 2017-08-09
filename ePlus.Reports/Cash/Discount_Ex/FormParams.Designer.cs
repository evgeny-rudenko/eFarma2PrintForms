namespace RCSDiscount
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
            this.ucCardType = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.ucDiscountProgram = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.ucDiscountCardOwner = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.ucCashRegistr = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.ucDiscountCard = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.ucContractor = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.ucStore = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.ucPeriod1 = new ePlus.MetaData.Client.UCPeriod();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chbReturn = new System.Windows.Forms.CheckBox();
            this.chbGoodCode = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(487, 3);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(562, 3);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 369);
            this.panel1.Size = new System.Drawing.Size(640, 29);
            // 
            // ucCardType
            // 
            this.ucCardType.AllowSaveState = false;
            this.ucCardType.Caption = null;
            this.ucCardType.Location = new System.Drawing.Point(315, 114);
            this.ucCardType.Mnemocode = "DISCOUNT2_CARD_TYPE";
            this.ucCardType.Name = "ucCardType";
            this.ucCardType.Pinnable = false;
            this.ucCardType.Size = new System.Drawing.Size(295, 74);
            this.ucCardType.TabIndex = 15;
            this.ucCardType.ValuesListChanged += new ePlus.MetaData.Client.UCPluginMultiSelect.ValuesListChangedDelegate(this.ucCardType_ValuesListChanged);
            // 
            // ucDiscountProgram
            // 
            this.ucDiscountProgram.AllowSaveState = false;
            this.ucDiscountProgram.Caption = null;
            this.ucDiscountProgram.Location = new System.Drawing.Point(12, 274);
            this.ucDiscountProgram.Mnemocode = "DISCOUNT2_PROGRAM";
            this.ucDiscountProgram.Name = "ucDiscountProgram";
            this.ucDiscountProgram.Pinnable = false;
            this.ucDiscountProgram.Size = new System.Drawing.Size(295, 74);
            this.ucDiscountProgram.TabIndex = 13;
            this.ucDiscountProgram.ValuesListChanged += new ePlus.MetaData.Client.UCPluginMultiSelect.ValuesListChangedDelegate(this.ucDiscountProgram_ValuesListChanged);
            // 
            // ucDiscountCardOwner
            // 
            this.ucDiscountCardOwner.AllowSaveState = false;
            this.ucDiscountCardOwner.Caption = null;
            this.ucDiscountCardOwner.Location = new System.Drawing.Point(315, 194);
            this.ucDiscountCardOwner.Mnemocode = "DISCOUNT2_MEMBER";
            this.ucDiscountCardOwner.Name = "ucDiscountCardOwner";
            this.ucDiscountCardOwner.Pinnable = false;
            this.ucDiscountCardOwner.Size = new System.Drawing.Size(295, 74);
            this.ucDiscountCardOwner.TabIndex = 16;
            this.ucDiscountCardOwner.ValuesListChanged += new ePlus.MetaData.Client.UCPluginMultiSelect.ValuesListChangedDelegate(this.ucDiscountCardOwner_ValuesListChanged);
            // 
            // ucCashRegistr
            // 
            this.ucCashRegistr.AllowSaveState = false;
            this.ucCashRegistr.Caption = "ККМ";
            this.ucCashRegistr.Location = new System.Drawing.Point(12, 194);
            this.ucCashRegistr.Mnemocode = "CASH_REGISTER";
            this.ucCashRegistr.Name = "ucCashRegistr";
            this.ucCashRegistr.Pinnable = false;
            this.ucCashRegistr.Size = new System.Drawing.Size(295, 74);
            this.ucCashRegistr.TabIndex = 12;
            // 
            // ucDiscountCard
            // 
            this.ucDiscountCard.AllowSaveState = false;
            this.ucDiscountCard.Caption = "";
            this.ucDiscountCard.Location = new System.Drawing.Point(313, 34);
            this.ucDiscountCard.Mnemocode = "DISCOUNT2_CARD";
            this.ucDiscountCard.Name = "ucDiscountCard";
            this.ucDiscountCard.Pinnable = false;
            this.ucDiscountCard.Size = new System.Drawing.Size(297, 74);
            this.ucDiscountCard.TabIndex = 14;
            this.ucDiscountCard.ValuesListChanged += new ePlus.MetaData.Client.UCPluginMultiSelect.ValuesListChangedDelegate(this.ucDiscountCard_ValuesListChanged);
            // 
            // ucContractor
            // 
            this.ucContractor.AllowSaveState = false;
            this.ucContractor.Caption = "Аптеки";
            this.ucContractor.Location = new System.Drawing.Point(12, 34);
            this.ucContractor.Mnemocode = "CONTRACTOR";
            this.ucContractor.Name = "ucContractor";
            this.ucContractor.Pinnable = false;
            this.ucContractor.Size = new System.Drawing.Size(295, 74);
            this.ucContractor.TabIndex = 10;
            // 
            // ucStore
            // 
            this.ucStore.AllowSaveState = false;
            this.ucStore.Caption = "Склады";
            this.ucStore.Location = new System.Drawing.Point(12, 114);
            this.ucStore.Mnemocode = "STORE";
            this.ucStore.Name = "ucStore";
            this.ucStore.Pinnable = false;
            this.ucStore.Size = new System.Drawing.Size(295, 74);
            this.ucStore.TabIndex = 11;
            // 
            // ucPeriod1
            // 
            this.ucPeriod1.DateFrom = new System.DateTime(2006, 10, 30, 14, 2, 0, 906);
            this.ucPeriod1.DateTo = new System.DateTime(2006, 10, 30, 14, 2, 0, 921);
            this.ucPeriod1.Location = new System.Drawing.Point(75, 7);
            this.ucPeriod1.Name = "ucPeriod1";
            this.ucPeriod1.Size = new System.Drawing.Size(247, 21);
            this.ucPeriod1.TabIndex = 9;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(328, 7);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(163, 17);
            this.checkBox1.TabIndex = 17;
            this.checkBox1.Text = "разворачивать по товарам";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Период";
            // 
            // chbReturn
            // 
            this.chbReturn.AutoSize = true;
            this.chbReturn.Location = new System.Drawing.Point(498, 7);
            this.chbReturn.Name = "chbReturn";
            this.chbReturn.Size = new System.Drawing.Size(133, 17);
            this.chbReturn.TabIndex = 19;
            this.chbReturn.Text = "Учитывать возвраты";
            this.chbReturn.UseVisualStyleBackColor = true;
            // 
            // chbGoodCode
            // 
            this.chbGoodCode.AutoSize = true;
            this.chbGoodCode.Location = new System.Drawing.Point(315, 274);
            this.chbGoodCode.Name = "chbGoodCode";
            this.chbGoodCode.Size = new System.Drawing.Size(150, 17);
            this.chbGoodCode.TabIndex = 125;
            this.chbGoodCode.Text = "Отображать код товара ";
            this.chbGoodCode.UseVisualStyleBackColor = true;
            // 
            // FormParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 398);
            this.Controls.Add(this.chbGoodCode);
            this.Controls.Add(this.chbReturn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ucCardType);
            this.Controls.Add(this.ucDiscountProgram);
            this.Controls.Add(this.ucDiscountCardOwner);
            this.Controls.Add(this.ucCashRegistr);
            this.Controls.Add(this.ucDiscountCard);
            this.Controls.Add(this.ucContractor);
            this.Controls.Add(this.ucStore);
            this.Controls.Add(this.ucPeriod1);
            this.Controls.Add(this.checkBox1);
            this.Name = "FormParams";
            this.Load += new System.EventHandler(this.FormParams_Load);
            this.Controls.SetChildIndex(this.checkBox1, 0);
            this.Controls.SetChildIndex(this.ucPeriod1, 0);
            this.Controls.SetChildIndex(this.ucStore, 0);
            this.Controls.SetChildIndex(this.ucContractor, 0);
            this.Controls.SetChildIndex(this.ucDiscountCard, 0);
            this.Controls.SetChildIndex(this.ucCashRegistr, 0);
            this.Controls.SetChildIndex(this.ucDiscountCardOwner, 0);
            this.Controls.SetChildIndex(this.ucDiscountProgram, 0);
            this.Controls.SetChildIndex(this.ucCardType, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.chbReturn, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.chbGoodCode, 0);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ePlus.MetaData.Client.UCPluginMultiSelect ucCardType;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucDiscountProgram;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucDiscountCardOwner;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucCashRegistr;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucDiscountCard;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucContractor;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucStore;
        private ePlus.MetaData.Client.UCPeriod ucPeriod1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chbReturn;
		private System.Windows.Forms.CheckBox chbGoodCode;
    }
}