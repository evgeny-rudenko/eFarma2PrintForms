namespace R23MCardOfGoodEx
{
    partial class CardOfGoodParams
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CardOfGoodParams));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.sortLabel = new System.Windows.Forms.Label();
            this.sortComboBox = new System.Windows.Forms.ComboBox();
            this.auCheckBox = new System.Windows.Forms.CheckBox();
            this.typeComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.zaprosView = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zaprosView)).BeginInit();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(360, 3);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(435, 3);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 410);
            this.panel1.Size = new System.Drawing.Size(513, 29);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(513, 25);
            this.toolStrip1.TabIndex = 176;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(76, 22);
            this.toolStripButton1.Text = "Очистить";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // sortLabel
            // 
            this.sortLabel.AutoSize = true;
            this.sortLabel.Location = new System.Drawing.Point(22, 345);
            this.sortLabel.Name = "sortLabel";
            this.sortLabel.Size = new System.Drawing.Size(70, 13);
            this.sortLabel.TabIndex = 183;
            this.sortLabel.Text = "Сортировка:";
            // 
            // sortComboBox
            // 
            this.sortComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sortComboBox.FormattingEnabled = true;
            this.sortComboBox.Items.AddRange(new object[] {
            "По дате документа",
            "По типу документа",
            "По приходу/расходу"});
            this.sortComboBox.Location = new System.Drawing.Point(114, 342);
            this.sortComboBox.Name = "sortComboBox";
            this.sortComboBox.Size = new System.Drawing.Size(387, 21);
            this.sortComboBox.TabIndex = 184;
            // 
            // auCheckBox
            // 
            this.auCheckBox.AutoSize = true;
            this.auCheckBox.Location = new System.Drawing.Point(25, 373);
            this.auCheckBox.Name = "auCheckBox";
            this.auCheckBox.Size = new System.Drawing.Size(248, 17);
            this.auCheckBox.TabIndex = 187;
            this.auCheckBox.Text = "Использовать только собственные склады";
            this.auCheckBox.UseVisualStyleBackColor = true;
            // 
            // typeComboBox
            // 
            this.typeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeComboBox.FormattingEnabled = true;
            this.typeComboBox.Items.AddRange(new object[] {
            "Полный",
            "Проверка на браки"});
            this.typeComboBox.Location = new System.Drawing.Point(114, 315);
            this.typeComboBox.Name = "typeComboBox";
            this.typeComboBox.Size = new System.Drawing.Size(387, 21);
            this.typeComboBox.TabIndex = 189;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 318);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 188;
            this.label1.Text = "Тип отчета:";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(25, 214);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(476, 95);
            this.listBox1.TabIndex = 191;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(435, 37);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(66, 22);
            this.button1.TabIndex = 192;
            this.button1.Text = "Обновить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(25, 39);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(404, 20);
            this.textBox1.TabIndex = 193;
            // 
            // zaprosView
            // 
            this.zaprosView.AllowUserToAddRows = false;
            this.zaprosView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.zaprosView.Location = new System.Drawing.Point(25, 67);
            this.zaprosView.MultiSelect = false;
            this.zaprosView.Name = "zaprosView";
            this.zaprosView.ReadOnly = true;
            this.zaprosView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.zaprosView.Size = new System.Drawing.Size(476, 141);
            this.zaprosView.TabIndex = 194;
            this.zaprosView.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.zaprosView_CellClick);
            // 
            // CardOfGoodParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 439);
            this.Controls.Add(this.zaprosView);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.typeComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.auCheckBox);
            this.Controls.Add(this.sortComboBox);
            this.Controls.Add(this.sortLabel);
            this.Controls.Add(this.toolStrip1);
            this.Name = "CardOfGoodParams";
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.toolStrip1, 0);
            this.Controls.SetChildIndex(this.sortLabel, 0);
            this.Controls.SetChildIndex(this.sortComboBox, 0);
            this.Controls.SetChildIndex(this.auCheckBox, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.typeComboBox, 0);
            this.Controls.SetChildIndex(this.listBox1, 0);
            this.Controls.SetChildIndex(this.button1, 0);
            this.Controls.SetChildIndex(this.textBox1, 0);
            this.Controls.SetChildIndex(this.zaprosView, 0);
            this.panel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zaprosView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.Label sortLabel;
        private System.Windows.Forms.ComboBox sortComboBox;
        private System.Windows.Forms.CheckBox auCheckBox;
        private System.Windows.Forms.ComboBox typeComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView zaprosView;
	}
}