namespace R32PSaleAdprice_Rosa
{
    partial class R32PSaleAdprice_Rosa_Params
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
            this.fGoodsKind = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.fGoods = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.fPeriod = new ePlus.MetaData.Controls.UCPeriod();
            this.label1 = new System.Windows.Forms.Label();
            this.chGoods = new System.Windows.Forms.CheckBox();
            this.fStore = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(556, 3);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(631, 3);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 396);
            this.panel1.Size = new System.Drawing.Size(709, 29);
            // 
            // fGoodsKind
            // 
            this.fGoodsKind.AllowSaveState = true;
            this.fGoodsKind.Caption = "Группы товаров";
            this.fGoodsKind.Location = new System.Drawing.Point(358, 58);
            this.fGoodsKind.Mnemocode = "GOODS_KIND";
            this.fGoodsKind.Name = "fGoodsKind";
            this.fGoodsKind.Pinnable = false;
            this.fGoodsKind.Size = new System.Drawing.Size(340, 142);
            this.fGoodsKind.TabIndex = 3;
            // 
            // fGoods
            // 
            this.fGoods.AllowSaveState = true;
            this.fGoods.Caption = "Товары";
            this.fGoods.Location = new System.Drawing.Point(12, 206);
            this.fGoods.Mnemocode = "GOODS2";
            this.fGoods.Name = "fGoods";
            this.fGoods.Pinnable = false;
            this.fGoods.Size = new System.Drawing.Size(686, 142);
            this.fGoods.TabIndex = 4;
            // 
            // fPeriod
            // 
            this.fPeriod.DateFrom = new System.DateTime(2012, 4, 10, 14, 16, 56, 801);
            this.fPeriod.DateTo = new System.DateTime(2012, 4, 10, 14, 16, 56, 802);
            this.fPeriod.Location = new System.Drawing.Point(12, 31);
            this.fPeriod.Name = "fPeriod";
            this.fPeriod.Size = new System.Drawing.Size(222, 21);
            this.fPeriod.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Период";
            // 
            // chGoods
            // 
            this.chGoods.AutoSize = true;
            this.chGoods.Location = new System.Drawing.Point(12, 354);
            this.chGoods.Name = "chGoods";
            this.chGoods.Size = new System.Drawing.Size(155, 17);
            this.chGoods.TabIndex = 7;
            this.chGoods.Text = "Детализация по товарам";
            this.chGoods.UseVisualStyleBackColor = true;
            // 
            // fStore
            // 
            this.fStore.AllowSaveState = true;
            this.fStore.Caption = "Склады";
            this.fStore.Location = new System.Drawing.Point(12, 58);
            this.fStore.Mnemocode = "STORE";
            this.fStore.Name = "fStore";
            this.fStore.Pinnable = false;
            this.fStore.Size = new System.Drawing.Size(340, 142);
            this.fStore.TabIndex = 8;
            // 
            // R32PGoodsKind_Rosa_Params
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 425);
            this.Controls.Add(this.fStore);
            this.Controls.Add(this.chGoods);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.fPeriod);
            this.Controls.Add(this.fGoods);
            this.Controls.Add(this.fGoodsKind);
            this.Name = "R32PGoodsKind_Rosa_Params";
            this.Load += new System.EventHandler(this.R32PSaleAdprice_Rosa_Params_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.R32PSaleAdprice_Rosa_Params_FormClosed);
            this.Controls.SetChildIndex(this.fGoodsKind, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.fGoods, 0);
            this.Controls.SetChildIndex(this.fPeriod, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.chGoods, 0);
            this.Controls.SetChildIndex(this.fStore, 0);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private ePlus.MetaData.Client.UCPluginMultiSelect fGoodsKind;
        private ePlus.MetaData.Client.UCPluginMultiSelect fGoods;
        private ePlus.MetaData.Controls.UCPeriod fPeriod;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chGoods;
        private ePlus.MetaData.Client.UCPluginMultiSelect fStore;

    }
}