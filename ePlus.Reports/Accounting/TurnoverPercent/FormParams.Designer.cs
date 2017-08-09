using ePlus.CommonEx.Controls;
namespace RCSTurnoverPercent
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
			this.label6 = new System.Windows.Forms.Label();
			this.ucContractor = new ePlus.CommonEx.Controls.MetaPluginDictionarySelectControl();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.vatComboBox = new System.Windows.Forms.ComboBox();
			this.vatLabel = new System.Windows.Forms.Label();
			this.sortLabel = new System.Windows.Forms.Label();
			this.sortComboBox = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
			this.panel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// bOK
			// 
			this.bOK.Location = new System.Drawing.Point(189, 3);
			// 
			// bClose
			// 
			this.bClose.Location = new System.Drawing.Point(264, 3);
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(0, 152);
			this.panel1.Size = new System.Drawing.Size(342, 29);
			this.panel1.TabIndex = 6;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(11, 65);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(68, 13);
			this.label6.TabIndex = 4;
			this.label6.Text = "Контрагент:";
			// 
			// ucContractor
			// 
			this.ucContractor.ELikeTextOption = ePlus.MetaData.Core.ELikeTextOption.None;
			this.ucContractor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic);
			this.ucContractor.Location = new System.Drawing.Point(82, 62);
			this.ucContractor.Name = "ucContractor";
			this.ucContractor.PluginMnemocode = "CONTRACTOR";
			this.ucContractor.Size = new System.Drawing.Size(243, 20);
			this.ucContractor.TabIndex = 5;			
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
			this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(342, 25);
			this.toolStrip1.TabIndex = 180;
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
			// vatComboBox
			// 
			this.vatComboBox.FormattingEnabled = true;
			this.vatComboBox.Items.AddRange(new object[] {
            "Оптовый",
            "Розничный"});
			this.vatComboBox.Location = new System.Drawing.Point(81, 88);
			this.vatComboBox.Name = "vatComboBox";
			this.vatComboBox.Size = new System.Drawing.Size(244, 21);
			this.vatComboBox.TabIndex = 181;
			// 
			// vatLabel
			// 
			this.vatLabel.AutoSize = true;
			this.vatLabel.Location = new System.Drawing.Point(11, 91);
			this.vatLabel.Name = "vatLabel";
			this.vatLabel.Size = new System.Drawing.Size(34, 13);
			this.vatLabel.TabIndex = 182;
			this.vatLabel.Text = "НДС:";
			// 
			// sortLabel
			// 
			this.sortLabel.AutoSize = true;
			this.sortLabel.Location = new System.Drawing.Point(11, 118);
			this.sortLabel.Name = "sortLabel";
			this.sortLabel.Size = new System.Drawing.Size(70, 13);
			this.sortLabel.TabIndex = 183;
			this.sortLabel.Text = "Сортировка:";
			// 
			// sortComboBox
			// 
			this.sortComboBox.FormattingEnabled = true;
			this.sortComboBox.Items.AddRange(new object[] {
            "Название товара",
            "Ставка НДС",
            "% от общего оборота"});
			this.sortComboBox.Location = new System.Drawing.Point(81, 115);
			this.sortComboBox.Name = "sortComboBox";
			this.sortComboBox.Size = new System.Drawing.Size(243, 21);
			this.sortComboBox.TabIndex = 184;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(11, 37);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 13);
			this.label3.TabIndex = 186;
			this.label3.Text = "Период:";
			// 
			// ucPeriod
			// 
			this.ucPeriod.DateFrom = new System.DateTime(2006, 10, 30, 14, 2, 0, 906);
			this.ucPeriod.DateTo = new System.DateTime(2006, 10, 30, 14, 2, 0, 921);
			this.ucPeriod.Location = new System.Drawing.Point(82, 35);
			this.ucPeriod.Name = "ucPeriod";
			this.ucPeriod.Size = new System.Drawing.Size(247, 21);
			this.ucPeriod.TabIndex = 185;
			// 
			// FormParams
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(342, 181);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.ucPeriod);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.ucContractor);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.sortComboBox);
			this.Controls.Add(this.sortLabel);
			this.Controls.Add(this.vatLabel);
			this.Controls.Add(this.vatComboBox);
			this.Name = "FormParams";
			this.Load += new System.EventHandler(this.FormParams_Load);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormParams_FormClosed);
			this.Controls.SetChildIndex(this.vatComboBox, 0);
			this.Controls.SetChildIndex(this.vatLabel, 0);
			this.Controls.SetChildIndex(this.sortLabel, 0);
			this.Controls.SetChildIndex(this.sortComboBox, 0);
			this.Controls.SetChildIndex(this.label6, 0);
			this.Controls.SetChildIndex(this.ucContractor, 0);
			this.Controls.SetChildIndex(this.toolStrip1, 0);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.ucPeriod, 0);
			this.Controls.SetChildIndex(this.label3, 0);
			this.panel1.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.Label label6;
		private MetaPluginDictionarySelectControl ucContractor;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.ComboBox vatComboBox;
		private System.Windows.Forms.Label vatLabel;
		private System.Windows.Forms.Label sortLabel;
		private System.Windows.Forms.ComboBox sortComboBox;
		private System.Windows.Forms.Label label3;
		private ePlus.MetaData.Client.UCPeriod ucPeriod;
    }
}