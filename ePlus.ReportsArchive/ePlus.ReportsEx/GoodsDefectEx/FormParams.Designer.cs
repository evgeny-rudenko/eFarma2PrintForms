namespace GoodsDefectEx
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
            this.ucPluginMulti_Contractor = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.ucPluginMulti_Store = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbSortName = new System.Windows.Forms.RadioButton();
            this.rbSortSeries = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(270, 3);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(345, 3);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 228);
            this.panel1.Size = new System.Drawing.Size(423, 29);
            // 
            // ucPluginMulti_Contractor
            // 
            this.ucPluginMulti_Contractor.AllowSaveState = false;
            this.ucPluginMulti_Contractor.Caption = null;
            this.ucPluginMulti_Contractor.Location = new System.Drawing.Point(12, 12);
            this.ucPluginMulti_Contractor.Mnemocode = "CONTRACTOR";
            this.ucPluginMulti_Contractor.Name = "ucPluginMulti_Contractor";
            this.ucPluginMulti_Contractor.Size = new System.Drawing.Size(401, 73);
            this.ucPluginMulti_Contractor.TabIndex = 147;
            // 
            // ucPluginMulti_Store
            // 
            this.ucPluginMulti_Store.AllowSaveState = false;
            this.ucPluginMulti_Store.Caption = "Склады";
            this.ucPluginMulti_Store.Location = new System.Drawing.Point(12, 91);
            this.ucPluginMulti_Store.Mnemocode = "STORE";
            this.ucPluginMulti_Store.Name = "ucPluginMulti_Store";
            this.ucPluginMulti_Store.Size = new System.Drawing.Size(401, 73);
            this.ucPluginMulti_Store.TabIndex = 146;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbSortSeries);
            this.groupBox1.Controls.Add(this.rbSortName);
            this.groupBox1.Location = new System.Drawing.Point(13, 171);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(236, 51);
            this.groupBox1.TabIndex = 148;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Сортировать:";
            // 
            // rbSortName
            // 
            this.rbSortName.AutoSize = true;
            this.rbSortName.Checked = true;
            this.rbSortName.Location = new System.Drawing.Point(6, 19);
            this.rbSortName.Name = "rbSortName";
            this.rbSortName.Size = new System.Drawing.Size(88, 17);
            this.rbSortName.TabIndex = 0;
            this.rbSortName.TabStop = true;
            this.rbSortName.Text = "по алфавиту";
            this.rbSortName.UseVisualStyleBackColor = true;
            // 
            // rbSortSeries
            // 
            this.rbSortSeries.AutoSize = true;
            this.rbSortSeries.Location = new System.Drawing.Point(116, 20);
            this.rbSortSeries.Name = "rbSortSeries";
            this.rbSortSeries.Size = new System.Drawing.Size(110, 17);
            this.rbSortSeries.TabIndex = 1;
            this.rbSortSeries.Text = "по номеру серии";
            this.rbSortSeries.UseVisualStyleBackColor = true;
            // 
            // FormParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 257);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ucPluginMulti_Contractor);
            this.Controls.Add(this.ucPluginMulti_Store);
            this.Name = "FormParams";
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.ucPluginMulti_Store, 0);
            this.Controls.SetChildIndex(this.ucPluginMulti_Contractor, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ePlus.MetaData.Client.UCPluginMultiSelect ucPluginMulti_Contractor;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucPluginMulti_Store;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbSortSeries;
        private System.Windows.Forms.RadioButton rbSortName;
    }
}