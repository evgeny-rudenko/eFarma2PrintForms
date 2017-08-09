namespace RCBSvyazCards_Rigla
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormParams));
			this.ucDiscountCard = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.ucContractor = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
			this.codeCheckBox = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.goodsCheckBox = new System.Windows.Forms.CheckBox();
			this.cardsCheckBox = new System.Windows.Forms.CheckBox();
			this.tranCheckBox = new System.Windows.Forms.CheckBox();
			this.chequeCheckBox = new System.Windows.Forms.CheckBox();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.panel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// bOK
			// 
			this.bOK.Location = new System.Drawing.Point(168, 3);
			// 
			// bClose
			// 
			this.bClose.Location = new System.Drawing.Point(243, 3);
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(0, 335);
			this.panel1.Size = new System.Drawing.Size(321, 29);
			// 
			// ucDiscountCard
			// 
			this.ucDiscountCard.AllowSaveState = true;
			this.ucDiscountCard.Caption = "";
			this.ucDiscountCard.Location = new System.Drawing.Point(11, 135);
			this.ucDiscountCard.Mnemocode = "DISCOUNT2_CARD";
			this.ucDiscountCard.Name = "ucDiscountCard";
			this.ucDiscountCard.Size = new System.Drawing.Size(297, 74);
			this.ucDiscountCard.TabIndex = 14;
			// 
			// ucContractor
			// 
			this.ucContractor.AllowSaveState = true;
			this.ucContractor.Caption = null;
			this.ucContractor.Location = new System.Drawing.Point(11, 55);
			this.ucContractor.Mnemocode = "CONTRACTOR";
			this.ucContractor.Name = "ucContractor";
			this.ucContractor.Size = new System.Drawing.Size(295, 74);
			this.ucContractor.TabIndex = 10;
			// 
			// ucPeriod
			// 
			this.ucPeriod.DateFrom = new System.DateTime(2006, 10, 30, 14, 2, 0, 906);
			this.ucPeriod.DateTo = new System.DateTime(2006, 10, 30, 14, 2, 0, 921);
			this.ucPeriod.Location = new System.Drawing.Point(67, 28);
			this.ucPeriod.Name = "ucPeriod";
			this.ucPeriod.Size = new System.Drawing.Size(247, 21);
			this.ucPeriod.TabIndex = 9;
			// 
			// codeCheckBox
			// 
			this.codeCheckBox.AutoSize = true;
			this.codeCheckBox.Location = new System.Drawing.Point(11, 215);
			this.codeCheckBox.Name = "codeCheckBox";
			this.codeCheckBox.Size = new System.Drawing.Size(147, 17);
			this.codeCheckBox.TabIndex = 17;
			this.codeCheckBox.Text = "Отображать код товара";
			this.codeCheckBox.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(16, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(45, 13);
			this.label1.TabIndex = 18;
			this.label1.Text = "Период";
			// 
			// goodsCheckBox
			// 
			this.goodsCheckBox.AutoSize = true;
			this.goodsCheckBox.Location = new System.Drawing.Point(11, 261);
			this.goodsCheckBox.Name = "goodsCheckBox";
			this.goodsCheckBox.Size = new System.Drawing.Size(164, 17);
			this.goodsCheckBox.TabIndex = 19;
			this.goodsCheckBox.Text = "Разворачивать по товарам";
			this.goodsCheckBox.UseVisualStyleBackColor = true;
			this.goodsCheckBox.CheckedChanged += new System.EventHandler(this.goodsCheckBox_CheckedChanged);
			// 
			// cardsCheckBox
			// 
			this.cardsCheckBox.AutoSize = true;
			this.cardsCheckBox.Location = new System.Drawing.Point(11, 284);
			this.cardsCheckBox.Name = "cardsCheckBox";
			this.cardsCheckBox.Size = new System.Drawing.Size(193, 17);
			this.cardsCheckBox.TabIndex = 20;
			this.cardsCheckBox.Text = "Разворачивать по номерам карт";
			this.cardsCheckBox.UseVisualStyleBackColor = true;
			// 
			// tranCheckBox
			// 
			this.tranCheckBox.AutoSize = true;
			this.tranCheckBox.Location = new System.Drawing.Point(11, 238);
			this.tranCheckBox.Name = "tranCheckBox";
			this.tranCheckBox.Size = new System.Drawing.Size(171, 17);
			this.tranCheckBox.TabIndex = 21;
			this.tranCheckBox.Text = "Отображать код транзакции";
			this.tranCheckBox.UseVisualStyleBackColor = true;
			// 
			// chequeCheckBox
			// 
			this.chequeCheckBox.AutoSize = true;
			this.chequeCheckBox.Location = new System.Drawing.Point(11, 307);
			this.chequeCheckBox.Name = "chequeCheckBox";
			this.chequeCheckBox.Size = new System.Drawing.Size(169, 17);
			this.chequeCheckBox.TabIndex = 22;
			this.chequeCheckBox.Text = "Разворачивать по типу чека";
			this.chequeCheckBox.UseVisualStyleBackColor = true;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(321, 25);
			this.toolStrip1.TabIndex = 119;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.Image = ((System.Drawing.Image) (resources.GetObject("toolStripButton1.Image")));
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(76, 22);
			this.toolStripButton1.Text = "Очистить";
			this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
			// 
			// FormParams
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(321, 364);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.ucPeriod);
			this.Controls.Add(this.chequeCheckBox);
			this.Controls.Add(this.tranCheckBox);
			this.Controls.Add(this.cardsCheckBox);
			this.Controls.Add(this.goodsCheckBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.ucDiscountCard);
			this.Controls.Add(this.ucContractor);
			this.Controls.Add(this.codeCheckBox);
			this.Name = "FormParams";
			this.Load += new System.EventHandler(this.FormParams_Load);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormParams_FormClosed);
			this.Controls.SetChildIndex(this.codeCheckBox, 0);
			this.Controls.SetChildIndex(this.ucContractor, 0);
			this.Controls.SetChildIndex(this.ucDiscountCard, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.goodsCheckBox, 0);
			this.Controls.SetChildIndex(this.cardsCheckBox, 0);
			this.Controls.SetChildIndex(this.tranCheckBox, 0);
			this.Controls.SetChildIndex(this.chequeCheckBox, 0);
			this.Controls.SetChildIndex(this.ucPeriod, 0);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.toolStrip1, 0);
			this.panel1.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private ePlus.MetaData.Client.UCPluginMultiSelect ucDiscountCard;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucContractor;
        private ePlus.MetaData.Client.UCPeriod ucPeriod;
        private System.Windows.Forms.CheckBox codeCheckBox;
        private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox goodsCheckBox;
		private System.Windows.Forms.CheckBox cardsCheckBox;
		private System.Windows.Forms.CheckBox tranCheckBox;
		private System.Windows.Forms.CheckBox chequeCheckBox;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}