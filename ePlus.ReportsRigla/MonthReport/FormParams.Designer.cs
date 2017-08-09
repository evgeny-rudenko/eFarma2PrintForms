using ePlus.CommonEx.Controls;
namespace RCBMonthReport_Rigla
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormParams));
            this.monthDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.planLabel = new System.Windows.Forms.Label();
            this.planNumericBox = new ePlus.CommonEx.Controls.ePlusNumericBox();
            this.daysLabel = new System.Windows.Forms.Label();
            this.daysTextBox = new System.Windows.Forms.TextBox();
            this.employeesLabel = new System.Windows.Forms.Label();
            this.employeesTextBox = new System.Windows.Forms.TextBox();
            this.fullAreaTextBox = new System.Windows.Forms.TextBox();
            this.fullAreaLabel = new System.Windows.Forms.Label();
            this.areaTextBox = new System.Windows.Forms.TextBox();
            this.areaLabel = new System.Windows.Forms.Label();
            this.ucContractor = new ePlus.CommonEx.Controls.MetaPluginDictionarySelectControl();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(183, 3);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(258, 3);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 229);
            this.panel1.Size = new System.Drawing.Size(336, 29);
            this.panel1.TabIndex = 6;
            // 
            // monthDateTimePicker
            // 
            this.monthDateTimePicker.CustomFormat = "MMMM yyyy";
            this.monthDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.monthDateTimePicker.Location = new System.Drawing.Point(82, 66);
            this.monthDateTimePicker.Name = "monthDateTimePicker";
            this.monthDateTimePicker.ShowUpDown = true;
            this.monthDateTimePicker.Size = new System.Drawing.Size(107, 20);
            this.monthDateTimePicker.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Месяц:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Контрагент:";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(336, 25);
            this.toolStrip1.TabIndex = 180;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(79, 22);
            this.toolStripButton1.Text = "Очистить";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // planLabel
            // 
            this.planLabel.AutoSize = true;
            this.planLabel.Location = new System.Drawing.Point(8, 97);
            this.planLabel.Name = "planLabel";
            this.planLabel.Size = new System.Drawing.Size(186, 13);
            this.planLabel.TabIndex = 182;
            this.planLabel.Text = "Выручка от реализации, план, руб.:";
            // 
            // planNumericBox
            // 
            this.planNumericBox.DecimalPlaces = 2;
            this.planNumericBox.DecimalSeparator = '.';
            this.planNumericBox.Location = new System.Drawing.Point(197, 94);
            this.planNumericBox.MaxLength = 18;
            this.planNumericBox.Name = "planNumericBox";
            this.planNumericBox.Positive = true;
            this.planNumericBox.Size = new System.Drawing.Size(125, 20);
            this.planNumericBox.TabIndex = 184;
            this.planNumericBox.Text = "0.00";
            this.planNumericBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.planNumericBox.ThousandSeparator = ' ';
            this.planNumericBox.TypingMode = ePlus.CommonEx.Controls.TypingMode.Replace;
            this.planNumericBox.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // daysLabel
            // 
            this.daysLabel.AutoSize = true;
            this.daysLabel.Location = new System.Drawing.Point(8, 123);
            this.daysLabel.Name = "daysLabel";
            this.daysLabel.Size = new System.Drawing.Size(139, 13);
            this.daysLabel.TabIndex = 185;
            this.daysLabel.Text = "Количество рабочих дней:";
            // 
            // daysTextBox
            // 
            this.daysTextBox.Location = new System.Drawing.Point(197, 120);
            this.daysTextBox.MaxLength = 2;
            this.daysTextBox.Name = "daysTextBox";
            this.daysTextBox.Size = new System.Drawing.Size(125, 20);
            this.daysTextBox.TabIndex = 186;
            this.daysTextBox.Text = "20";
            this.daysTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // employeesLabel
            // 
            this.employeesLabel.AutoSize = true;
            this.employeesLabel.Location = new System.Drawing.Point(8, 147);
            this.employeesLabel.Name = "employeesLabel";
            this.employeesLabel.Size = new System.Drawing.Size(136, 13);
            this.employeesLabel.TabIndex = 187;
            this.employeesLabel.Text = "Количество сотрудников:";
            // 
            // employeesTextBox
            // 
            this.employeesTextBox.Location = new System.Drawing.Point(196, 144);
            this.employeesTextBox.MaxLength = 2;
            this.employeesTextBox.Name = "employeesTextBox";
            this.employeesTextBox.Size = new System.Drawing.Size(125, 20);
            this.employeesTextBox.TabIndex = 188;
            this.employeesTextBox.Text = "2";
            this.employeesTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // fullAreaTextBox
            // 
            this.fullAreaTextBox.Location = new System.Drawing.Point(196, 193);
            this.fullAreaTextBox.MaxLength = 2;
            this.fullAreaTextBox.Name = "fullAreaTextBox";
            this.fullAreaTextBox.Size = new System.Drawing.Size(125, 20);
            this.fullAreaTextBox.TabIndex = 192;
            this.fullAreaTextBox.Text = "100";
            this.fullAreaTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // fullAreaLabel
            // 
            this.fullAreaLabel.AutoSize = true;
            this.fullAreaLabel.Location = new System.Drawing.Point(8, 196);
            this.fullAreaLabel.Name = "fullAreaLabel";
            this.fullAreaLabel.Size = new System.Drawing.Size(131, 13);
            this.fullAreaLabel.TabIndex = 191;
            this.fullAreaLabel.Text = "Общая площадь аптеки:";
            // 
            // areaTextBox
            // 
            this.areaTextBox.Location = new System.Drawing.Point(196, 169);
            this.areaTextBox.MaxLength = 2;
            this.areaTextBox.Name = "areaTextBox";
            this.areaTextBox.Size = new System.Drawing.Size(125, 20);
            this.areaTextBox.TabIndex = 190;
            this.areaTextBox.Text = "40";
            this.areaTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // areaLabel
            // 
            this.areaLabel.AutoSize = true;
            this.areaLabel.Location = new System.Drawing.Point(8, 172);
            this.areaLabel.Name = "areaLabel";
            this.areaLabel.Size = new System.Drawing.Size(138, 13);
            this.areaLabel.TabIndex = 189;
            this.areaLabel.Text = "Площадь торгового зала:";
            // 
            // ucContractor
            // 
            this.ucContractor.ClearTextOnValidatingIfValueIsEmpty = true;
            this.ucContractor.ELikeTextOption = ePlus.MetaData.Core.ELikeTextOption.None;
            this.ucContractor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic);
            this.ucContractor.Location = new System.Drawing.Point(82, 40);
            this.ucContractor.Name = "ucContractor";
            this.ucContractor.PluginMnemocode = "CONTRACTOR";
            this.ucContractor.SelectNextControlAfterSelectEntity = false;
            this.ucContractor.Size = new System.Drawing.Size(242, 20);
            this.ucContractor.TabIndex = 193;
            this.ucContractor.UseEnterToOpenPlugin = true;
            this.ucContractor.UseSpaceToOpenPlugin = true;
            this.ucContractor.TextChanged += new System.EventHandler(this.ucContractor_TextChanged);
            // 
            // FormParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 258);
            this.Controls.Add(this.ucContractor);
            this.Controls.Add(this.fullAreaTextBox);
            this.Controls.Add(this.fullAreaLabel);
            this.Controls.Add(this.areaTextBox);
            this.Controls.Add(this.areaLabel);
            this.Controls.Add(this.employeesTextBox);
            this.Controls.Add(this.employeesLabel);
            this.Controls.Add(this.daysTextBox);
            this.Controls.Add(this.daysLabel);
            this.Controls.Add(this.planNumericBox);
            this.Controls.Add(this.planLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.monthDateTimePicker);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.label6);
            this.Name = "FormParams";
            this.Load += new System.EventHandler(this.FormParams_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormParams_FormClosed);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.toolStrip1, 0);
            this.Controls.SetChildIndex(this.monthDateTimePicker, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.planLabel, 0);
            this.Controls.SetChildIndex(this.planNumericBox, 0);
            this.Controls.SetChildIndex(this.daysLabel, 0);
            this.Controls.SetChildIndex(this.daysTextBox, 0);
            this.Controls.SetChildIndex(this.employeesLabel, 0);
            this.Controls.SetChildIndex(this.employeesTextBox, 0);
            this.Controls.SetChildIndex(this.areaLabel, 0);
            this.Controls.SetChildIndex(this.areaTextBox, 0);
            this.Controls.SetChildIndex(this.fullAreaLabel, 0);
            this.Controls.SetChildIndex(this.fullAreaTextBox, 0);
            this.Controls.SetChildIndex(this.ucContractor, 0);
            this.panel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetaPluginDictionarySelectControl ucContractor;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.DateTimePicker monthDateTimePicker;
		private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.Label planLabel;
		private ePlusNumericBox planNumericBox;
		private System.Windows.Forms.Label daysLabel;
		private System.Windows.Forms.TextBox daysTextBox;
		private System.Windows.Forms.Label employeesLabel;
		private System.Windows.Forms.TextBox employeesTextBox;
		private System.Windows.Forms.TextBox fullAreaTextBox;
		private System.Windows.Forms.Label fullAreaLabel;
		private System.Windows.Forms.TextBox areaTextBox;
        private System.Windows.Forms.Label areaLabel;
        
    }
}