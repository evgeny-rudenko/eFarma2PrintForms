namespace RCKMatVet_CNIIS_1
{
    partial class RCKMatVedFormParams
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkImportantOnly = new System.Windows.Forms.CheckBox();
            this.chkShowSeries = new System.Windows.Forms.CheckBox();
            this.ucFilter = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.label2 = new System.Windows.Forms.Label();
            this.clbFilters = new System.Windows.Forms.CheckedListBox();
            this.period = new ePlus.MetaData.Client.UCPeriod();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(328, 3);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(403, 3);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 239);
            this.panel1.Size = new System.Drawing.Size(481, 29);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkImportantOnly);
            this.groupBox1.Controls.Add(this.chkShowSeries);
            this.groupBox1.Location = new System.Drawing.Point(12, 152);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 68);
            this.groupBox1.TabIndex = 124;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Опции";
            // 
            // chkImportantOnly
            // 
            this.chkImportantOnly.AutoSize = true;
            this.chkImportantOnly.Location = new System.Drawing.Point(6, 42);
            this.chkImportantOnly.Name = "chkImportantOnly";
            this.chkImportantOnly.Size = new System.Drawing.Size(159, 17);
            this.chkImportantOnly.TabIndex = 1;
            this.chkImportantOnly.Text = "Только жизненно-важные";
            this.chkImportantOnly.UseVisualStyleBackColor = true;
            // 
            // chkShowSeries
            // 
            this.chkShowSeries.AutoSize = true;
            this.chkShowSeries.Location = new System.Drawing.Point(6, 19);
            this.chkShowSeries.Name = "chkShowSeries";
            this.chkShowSeries.Size = new System.Drawing.Size(109, 17);
            this.chkShowSeries.TabIndex = 0;
            this.chkShowSeries.Text = "Выводить серии";
            this.chkShowSeries.UseVisualStyleBackColor = true;
            // 
            // ucFilter
            // 
            this.ucFilter.AllowSaveState = false;
            this.ucFilter.Caption = "Поставщики:";
            this.ucFilter.Location = new System.Drawing.Point(145, 50);
            this.ucFilter.Mnemocode = "CONTRACTOR";
            this.ucFilter.Name = "ucFilter";
            this.ucFilter.Size = new System.Drawing.Size(329, 98);
            this.ucFilter.TabIndex = 123;
            this.ucFilter.ValuesListChangedNew += new ePlus.MetaData.Client.ValuesListChangedEventHandler(this.ucFilter_ValuesListChangedNew);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 122;
            this.label2.Text = "Отбор:";
            // 
            // clbFilters
            // 
            this.clbFilters.FormattingEnabled = true;
            this.clbFilters.Location = new System.Drawing.Point(12, 52);
            this.clbFilters.Name = "clbFilters";
            this.clbFilters.Size = new System.Drawing.Size(127, 94);
            this.clbFilters.TabIndex = 121;
            this.clbFilters.SelectedIndexChanged += new System.EventHandler(this.clbFilters_SelectedIndexChanged);
            // 
            // period
            // 
            this.period.DateFrom = new System.DateTime(2006, 10, 30, 14, 2, 0, 906);
            this.period.DateTo = new System.DateTime(2006, 10, 30, 14, 2, 0, 921);
            this.period.Location = new System.Drawing.Point(64, 12);
            this.period.Name = "period";
            this.period.Size = new System.Drawing.Size(229, 21);
            this.period.TabIndex = 120;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 119;
            this.label1.Text = "Период:";
            // 
            // FormParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 268);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ucFilter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.clbFilters);
            this.Controls.Add(this.period);
            this.Controls.Add(this.label1);
            this.Name = "FormParams";
            this.Load += new System.EventHandler(this.FormParams_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.period, 0);
            this.Controls.SetChildIndex(this.clbFilters, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.ucFilter, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkImportantOnly;
        private System.Windows.Forms.CheckBox chkShowSeries;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucFilter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox clbFilters;
        private ePlus.MetaData.Client.UCPeriod period;
        private System.Windows.Forms.Label label1;
    }
}