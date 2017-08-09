using System.Windows.Forms;
using ePlus.MetaData.Client;
namespace InvoiceRemainsEx
{
    partial class InvoiceRemainsParams
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
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonClear = new System.Windows.Forms.ToolStripButton();
            this.periodLabel = new System.Windows.Forms.Label();
            this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
            this.filterComboBox = new System.Windows.Forms.ComboBox();
            this.filterLabel = new System.Windows.Forms.Label();
            this.ucPluginMultiSelect = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.panel1.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(230, 4);
            this.bOK.Margin = new System.Windows.Forms.Padding(4);
            this.bOK.Size = new System.Drawing.Size(75, 21);
            // 
            // bClose
            // 
            this.bClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bClose.Location = new System.Drawing.Point(305, 4);
            this.bClose.Margin = new System.Windows.Forms.Padding(4);
            this.bClose.Size = new System.Drawing.Size(75, 21);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 382);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Padding = new System.Windows.Forms.Padding(4);
            this.panel1.Size = new System.Drawing.Size(384, 29);
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonClear});
            this.toolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip.Size = new System.Drawing.Size(384, 25);
            this.toolStrip.TabIndex = 9;
            this.toolStrip.Text = "toolStrip1";
            // 
            // toolStripButtonClear
            // 
            this.toolStripButtonClear.Image = global::InvoiceRemainsEx.Properties.Resources.Clear;
            this.toolStripButtonClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonClear.Name = "toolStripButtonClear";
            this.toolStripButtonClear.Size = new System.Drawing.Size(79, 22);
            this.toolStripButtonClear.Text = "Очистить";
            this.toolStripButtonClear.Click += new System.EventHandler(this.toolStripButtonClear_Click);
            // 
            // periodLabel
            // 
            this.periodLabel.AutoSize = true;
            this.periodLabel.Location = new System.Drawing.Point(10, 36);
            this.periodLabel.Name = "periodLabel";
            this.periodLabel.Size = new System.Drawing.Size(45, 13);
            this.periodLabel.TabIndex = 11;
            this.periodLabel.Text = "Период";
            // 
            // ucPeriod
            // 
            this.ucPeriod.DateFrom = new System.DateTime(2006, 10, 30, 14, 2, 0, 906);
            this.ucPeriod.DateTo = new System.DateTime(2006, 10, 30, 14, 2, 0, 921);
            this.ucPeriod.Location = new System.Drawing.Point(74, 36);
            this.ucPeriod.Margin = new System.Windows.Forms.Padding(4);
            this.ucPeriod.Name = "ucPeriod";
            this.ucPeriod.Size = new System.Drawing.Size(256, 21);
            this.ucPeriod.TabIndex = 2;
            // 
            // filterComboBox
            // 
            this.filterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filterComboBox.FormattingEnabled = true;
            this.filterComboBox.Items.AddRange(new object[] {
            "Приходные документы",
            "Поставщики",
            "Пустой фильтр"});
            this.filterComboBox.Location = new System.Drawing.Point(74, 63);
            this.filterComboBox.Name = "filterComboBox";
            this.filterComboBox.Size = new System.Drawing.Size(223, 21);
            this.filterComboBox.TabIndex = 3;
            this.filterComboBox.SelectedIndexChanged += new System.EventHandler(this.filterComboBox_SelectedIndexChanged);
            // 
            // filterLabel
            // 
            this.filterLabel.AutoSize = true;
            this.filterLabel.Location = new System.Drawing.Point(10, 63);
            this.filterLabel.Name = "filterLabel";
            this.filterLabel.Size = new System.Drawing.Size(47, 13);
            this.filterLabel.TabIndex = 132;
            this.filterLabel.Text = "Фильтр";
            // 
            // ucPluginMultiSelect
            // 
            this.ucPluginMultiSelect.AllowSaveState = false;
            this.ucPluginMultiSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucPluginMultiSelect.Caption = "Приходные документы";
            this.ucPluginMultiSelect.Location = new System.Drawing.Point(13, 91);
            this.ucPluginMultiSelect.Margin = new System.Windows.Forms.Padding(4);
            this.ucPluginMultiSelect.Mnemocode = "INVOICE";
            this.ucPluginMultiSelect.MultiSelect = true;
            this.ucPluginMultiSelect.Name = "ucPluginMultiSelect";
            this.ucPluginMultiSelect.Pinnable = false;
            this.ucPluginMultiSelect.Size = new System.Drawing.Size(358, 283);
            this.ucPluginMultiSelect.TabIndex = 4;
            // 
            // InvoiceRemainsParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(384, 411);
            this.Controls.Add(this.ucPluginMultiSelect);
            this.Controls.Add(this.filterLabel);
            this.Controls.Add(this.filterComboBox);
            this.Controls.Add(this.ucPeriod);
            this.Controls.Add(this.periodLabel);
            this.Controls.Add(this.toolStrip);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "InvoiceRemainsParams";
            this.Load += new System.EventHandler(this.InvoiceRemainsParams_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.toolStrip, 0);
            this.Controls.SetChildIndex(this.periodLabel, 0);
            this.Controls.SetChildIndex(this.ucPeriod, 0);
            this.Controls.SetChildIndex(this.filterComboBox, 0);
            this.Controls.SetChildIndex(this.filterLabel, 0);
            this.Controls.SetChildIndex(this.ucPluginMultiSelect, 0);
            this.panel1.ResumeLayout(false);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private ToolStripButton toolStripButtonClear;
        private Label periodLabel;
        private UCPeriod ucPeriod;
        private ComboBox filterComboBox;
        private Label filterLabel;
        public UCPluginMultiSelect ucPluginMultiSelect;
    }
}