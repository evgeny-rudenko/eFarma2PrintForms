namespace R36RGLVNS_Law_reps
{
    partial class R36RGLVNS_Law_reps
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
            this.label1 = new System.Windows.Forms.Label();
            this.ucStore = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.dateDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rb20pos = new System.Windows.Forms.RadioButton();
            this.rbAllpos = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(206, 3);
            this.bOK.Size = new System.Drawing.Size(75, 27);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(281, 3);
            this.bClose.Size = new System.Drawing.Size(75, 27);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 256);
            this.panel1.Size = new System.Drawing.Size(359, 33);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Остатки на дату:";
            // 
            // ucStore
            // 
            this.ucStore.AllowSaveState = false;
            this.ucStore.Caption = "";
            this.ucStore.Location = new System.Drawing.Point(15, 134);
            this.ucStore.Mnemocode = "STORE";
            this.ucStore.Name = "ucStore";
            this.ucStore.Size = new System.Drawing.Size(328, 114);
            this.ucStore.TabIndex = 15;
            // 
            // dateDateTimePicker
            // 
            this.dateDateTimePicker.Location = new System.Drawing.Point(105, 36);
            this.dateDateTimePicker.Name = "dateDateTimePicker";
            this.dateDateTimePicker.Size = new System.Drawing.Size(143, 20);
            this.dateDateTimePicker.TabIndex = 17;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(359, 25);
            this.toolStrip1.TabIndex = 21;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(60, 22);
            this.toolStripButton1.Text = "Очистить";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click_1);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rb20pos);
            this.groupBox1.Controls.Add(this.rbAllpos);
            this.groupBox1.Location = new System.Drawing.Point(15, 64);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(323, 60);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Тип отчета";
            // 
            // rb20pos
            // 
            this.rb20pos.AutoSize = true;
            this.rb20pos.Location = new System.Drawing.Point(7, 36);
            this.rb20pos.Name = "rb20pos";
            this.rb20pos.Size = new System.Drawing.Size(303, 17);
            this.rb20pos.TabIndex = 1;
            this.rb20pos.Text = "По 20 международ. непатентованным наименованиям";
            this.rb20pos.UseVisualStyleBackColor = true;
            // 
            // rbAllpos
            // 
            this.rbAllpos.AutoSize = true;
            this.rbAllpos.Checked = true;
            this.rbAllpos.Location = new System.Drawing.Point(8, 15);
            this.rbAllpos.Name = "rbAllpos";
            this.rbAllpos.Size = new System.Drawing.Size(165, 17);
            this.rbAllpos.TabIndex = 0;
            this.rbAllpos.TabStop = true;
            this.rbAllpos.Text = "По всем позициям ЖЛВНС";
            this.rbAllpos.UseVisualStyleBackColor = true;
            // 
            // R36RGLVNS_Law_reps
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 289);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dateDateTimePicker);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ucStore);
            this.Name = "R36RGLVNS_Law_reps";
            this.Load += new System.EventHandler(this.PriceListParams_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PriceListParams_FormClosed);
            this.Controls.SetChildIndex(this.ucStore, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.dateDateTimePicker, 0);
            this.Controls.SetChildIndex(this.toolStrip1, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.panel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucStore;
        private System.Windows.Forms.DateTimePicker dateDateTimePicker;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rb20pos;
        private System.Windows.Forms.RadioButton rbAllpos;
    }
}
