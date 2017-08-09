namespace RCRKKM_Z_Report_PlanetEx
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
            this.ucPeriod1 = new ePlus.MetaData.Client.UCPeriod();
            this.checkBox_detail = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ucMetaPluginSelect2 = new ePlus.MetaData.Client.UCMetaPluginSelect();
            this.ucMetaPluginSelect1 = new ePlus.MetaData.Client.UCMetaPluginSelect();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(318, 3);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(393, 3);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 128);
            this.panel1.Size = new System.Drawing.Size(471, 29);
            // 
            // ucPeriod1
            // 
            this.ucPeriod1.DateFrom = new System.DateTime(2006, 10, 30, 14, 2, 0, 906);
            this.ucPeriod1.DateTo = new System.DateTime(2006, 10, 30, 14, 2, 0, 921);
            this.ucPeriod1.Location = new System.Drawing.Point(87, 9);
            this.ucPeriod1.Name = "ucPeriod1";
            this.ucPeriod1.Size = new System.Drawing.Size(285, 21);
            this.ucPeriod1.TabIndex = 116;
            // 
            // checkBox_detail
            // 
            this.checkBox_detail.AutoSize = true;
            this.checkBox_detail.Location = new System.Drawing.Point(19, 93);
            this.checkBox_detail.Name = "checkBox_detail";
            this.checkBox_detail.Size = new System.Drawing.Size(68, 17);
            this.checkBox_detail.TabIndex = 115;
            this.checkBox_detail.Text = "Краткий";
            this.checkBox_detail.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 114;
            this.label2.Text = "Контрагент";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 113;
            this.label1.Text = "ККМ";
            // 
            // ucMetaPluginSelect2
            // 
            this.ucMetaPluginSelect2.ButtonStyle = ePlus.MetaData.Client.EButtonStyle.SelectClear;
            this.ucMetaPluginSelect2.Location = new System.Drawing.Point(87, 63);
            this.ucMetaPluginSelect2.Mnemocode = "CONTRACTOR";
            this.ucMetaPluginSelect2.Name = "ucMetaPluginSelect2";
            this.ucMetaPluginSelect2.Size = new System.Drawing.Size(318, 22);
            this.ucMetaPluginSelect2.TabIndex = 112;
            // 
            // ucMetaPluginSelect1
            // 
            this.ucMetaPluginSelect1.ButtonStyle = ePlus.MetaData.Client.EButtonStyle.SelectClear;
            this.ucMetaPluginSelect1.Location = new System.Drawing.Point(87, 35);
            this.ucMetaPluginSelect1.Mnemocode = "CASH_REGISTER";
            this.ucMetaPluginSelect1.Name = "ucMetaPluginSelect1";
            this.ucMetaPluginSelect1.Size = new System.Drawing.Size(318, 22);
            this.ucMetaPluginSelect1.TabIndex = 111;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 117;
            this.label3.Text = "Период";
            // 
            // FormParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 157);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ucPeriod1);
            this.Controls.Add(this.checkBox_detail);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ucMetaPluginSelect2);
            this.Controls.Add(this.ucMetaPluginSelect1);
            this.Name = "FormParams";
            this.Load += new System.EventHandler(this.FormParams_Load);
            this.Controls.SetChildIndex(this.ucMetaPluginSelect1, 0);
            this.Controls.SetChildIndex(this.ucMetaPluginSelect2, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.checkBox_detail, 0);
            this.Controls.SetChildIndex(this.ucPeriod1, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ePlus.MetaData.Client.UCPeriod ucPeriod1;
        private System.Windows.Forms.CheckBox checkBox_detail;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private ePlus.MetaData.Client.UCMetaPluginSelect ucMetaPluginSelect2;
        private ePlus.MetaData.Client.UCMetaPluginSelect ucMetaPluginSelect1;
        private System.Windows.Forms.Label label3;
    }
}