namespace FCBMarginControlZNVLS
{
	partial class MarginControlParams
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MarginControlParams));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.ucContractors = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.ucGoodsGroup = new ePlus.CommonEx.Controls.SelectGoodsGroup();
			this.importantCheckBox = new System.Windows.Forms.CheckBox();
			this.filterComboBox = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.emptyCheckBox = new System.Windows.Forms.CheckBox();
			this.codeCheckBox = new System.Windows.Forms.CheckBox();
			this.filtersDataGridView = new System.Windows.Forms.DataGridView();
			this.lOWDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.hIGHDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.vALUEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.table1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.marginControl_DS = new FCBMarginControlZNVLS.MarginControl_DS();
			this.bindingNavigator1 = new System.Windows.Forms.BindingNavigator(this.components);
			this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
			this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
			this.panel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) (this.filtersDataGridView)).BeginInit();
			((System.ComponentModel.ISupportInitialize) (this.table1BindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize) (this.marginControl_DS)).BeginInit();
			((System.ComponentModel.ISupportInitialize) (this.bindingNavigator1)).BeginInit();
			this.bindingNavigator1.SuspendLayout();
			this.SuspendLayout();
			// 
			// bOK
			// 
			this.bOK.Location = new System.Drawing.Point(461, 3);
			// 
			// bClose
			// 
			this.bClose.Location = new System.Drawing.Point(536, 3);
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(0, 391);
			this.panel1.Size = new System.Drawing.Size(614, 29);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
			this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(614, 25);
			this.toolStrip1.TabIndex = 9;
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
			// ucContractors
			// 
			this.ucContractors.AllowSaveState = true;
			this.ucContractors.Caption = "Аптеки";
			this.ucContractors.Location = new System.Drawing.Point(12, 37);
			this.ucContractors.Mnemocode = "CONTRACTOR";
			this.ucContractors.Name = "ucContractors";
			this.ucContractors.Size = new System.Drawing.Size(337, 118);
			this.ucContractors.TabIndex = 104;
			// 
			// ucGoodsGroup
			// 
			this.ucGoodsGroup.Location = new System.Drawing.Point(357, 37);
			this.ucGoodsGroup.Name = "ucGoodsGroup";
			this.ucGoodsGroup.Size = new System.Drawing.Size(245, 124);
			this.ucGoodsGroup.TabIndex = 145;
			// 
			// importantCheckBox
			// 
			this.importantCheckBox.AutoSize = true;
			this.importantCheckBox.Location = new System.Drawing.Point(12, 304);
			this.importantCheckBox.Name = "importantCheckBox";
			this.importantCheckBox.Size = new System.Drawing.Size(225, 17);
			this.importantCheckBox.TabIndex = 147;
			this.importantCheckBox.Text = "Проверять товар с признаком ЖНВЛС";
			this.importantCheckBox.UseVisualStyleBackColor = true;
			// 
			// filterComboBox
			// 
			this.filterComboBox.FormattingEnabled = true;
			this.filterComboBox.Items.AddRange(new object[] {
            "Наценка превышена",
            "Наценка не превышена",
            "Все"});
			this.filterComboBox.Location = new System.Drawing.Point(389, 304);
			this.filterComboBox.Name = "filterComboBox";
			this.filterComboBox.Size = new System.Drawing.Size(213, 21);
			this.filterComboBox.TabIndex = 150;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(285, 307);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(98, 13);
			this.label2.TabIndex = 151;
			this.label2.Text = "Выводить строки:";
			// 
			// emptyCheckBox
			// 
			this.emptyCheckBox.AutoSize = true;
			this.emptyCheckBox.Location = new System.Drawing.Point(12, 327);
			this.emptyCheckBox.Name = "emptyCheckBox";
			this.emptyCheckBox.Size = new System.Drawing.Size(318, 17);
			this.emptyCheckBox.TabIndex = 152;
			this.emptyCheckBox.Text = "Включить в отчет позиции с пустой ценой производителя";
			this.emptyCheckBox.UseVisualStyleBackColor = true;
			// 
			// codeCheckBox
			// 
			this.codeCheckBox.AutoSize = true;
			this.codeCheckBox.Location = new System.Drawing.Point(12, 350);
			this.codeCheckBox.Name = "codeCheckBox";
			this.codeCheckBox.Size = new System.Drawing.Size(147, 17);
			this.codeCheckBox.TabIndex = 153;
			this.codeCheckBox.Text = "Отображать код товара";
			this.codeCheckBox.UseVisualStyleBackColor = true;
			// 
			// filtersDataGridView
			// 
			this.filtersDataGridView.AllowUserToAddRows = false;
			this.filtersDataGridView.AllowUserToDeleteRows = false;
			this.filtersDataGridView.AllowUserToResizeRows = false;
			this.filtersDataGridView.AutoGenerateColumns = false;
			this.filtersDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.filtersDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.lOWDataGridViewTextBoxColumn,
            this.hIGHDataGridViewTextBoxColumn,
            this.vALUEDataGridViewTextBoxColumn});
			this.filtersDataGridView.DataSource = this.table1BindingSource;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.Format = "N2";
			dataGridViewCellStyle2.NullValue = "0,00";
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.filtersDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
			this.filtersDataGridView.Location = new System.Drawing.Point(12, 161);
			this.filtersDataGridView.MultiSelect = false;
			this.filtersDataGridView.Name = "filtersDataGridView";
			this.filtersDataGridView.Size = new System.Drawing.Size(563, 137);
			this.filtersDataGridView.TabIndex = 154;
			this.filtersDataGridView.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.filtersDataGridView_DefaultValuesNeeded);
			this.filtersDataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.filtersDataGridView_DataError);
			// 
			// lOWDataGridViewTextBoxColumn
			// 
			this.lOWDataGridViewTextBoxColumn.DataPropertyName = "LOW";
			this.lOWDataGridViewTextBoxColumn.HeaderText = "Нижняя граница цены производителя";
			this.lOWDataGridViewTextBoxColumn.Name = "lOWDataGridViewTextBoxColumn";
			this.lOWDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.lOWDataGridViewTextBoxColumn.Width = 150;
			// 
			// hIGHDataGridViewTextBoxColumn
			// 
			this.hIGHDataGridViewTextBoxColumn.DataPropertyName = "HIGH";
			this.hIGHDataGridViewTextBoxColumn.HeaderText = "Верхяя граница цены производителя";
			this.hIGHDataGridViewTextBoxColumn.Name = "hIGHDataGridViewTextBoxColumn";
			this.hIGHDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.hIGHDataGridViewTextBoxColumn.Width = 150;
			// 
			// vALUEDataGridViewTextBoxColumn
			// 
			this.vALUEDataGridViewTextBoxColumn.DataPropertyName = "VALUE";
			this.vALUEDataGridViewTextBoxColumn.HeaderText = "Предельная наценка, %";
			this.vALUEDataGridViewTextBoxColumn.Name = "vALUEDataGridViewTextBoxColumn";
			this.vALUEDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.vALUEDataGridViewTextBoxColumn.Width = 150;
			// 
			// table1BindingSource
			// 
			this.table1BindingSource.DataMember = "Table1";
			this.table1BindingSource.DataSource = this.marginControl_DS;
			// 
			// marginControl_DS
			// 
			this.marginControl_DS.DataSetName = "MarginControl_DS";
			this.marginControl_DS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
			// 
			// bindingNavigator1
			// 
			this.bindingNavigator1.AddNewItem = this.bindingNavigatorAddNewItem;
			this.bindingNavigator1.BindingSource = this.table1BindingSource;
			this.bindingNavigator1.CountItem = null;
			this.bindingNavigator1.DeleteItem = this.bindingNavigatorDeleteItem;
			this.bindingNavigator1.Dock = System.Windows.Forms.DockStyle.None;
			this.bindingNavigator1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.bindingNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem});
			this.bindingNavigator1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
			this.bindingNavigator1.Location = new System.Drawing.Point(575, 161);
			this.bindingNavigator1.MoveFirstItem = null;
			this.bindingNavigator1.MoveLastItem = null;
			this.bindingNavigator1.MoveNextItem = null;
			this.bindingNavigator1.MovePreviousItem = null;
			this.bindingNavigator1.Name = "bindingNavigator1";
			this.bindingNavigator1.PositionItem = null;
			this.bindingNavigator1.Size = new System.Drawing.Size(24, 48);
			this.bindingNavigator1.TabIndex = 155;
			this.bindingNavigator1.Text = "bindingNavigator1";
			// 
			// bindingNavigatorAddNewItem
			// 
			this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image) (resources.GetObject("bindingNavigatorAddNewItem.Image")));
			this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
			this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
			this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(22, 20);
			this.bindingNavigatorAddNewItem.Text = "Add new";
			// 
			// bindingNavigatorDeleteItem
			// 
			this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image) (resources.GetObject("bindingNavigatorDeleteItem.Image")));
			this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
			this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
			this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(22, 20);
			this.bindingNavigatorDeleteItem.Text = "Delete";
			// 
			// MarginControlParams
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(614, 420);
			this.Controls.Add(this.bindingNavigator1);
			this.Controls.Add(this.filtersDataGridView);
			this.Controls.Add(this.codeCheckBox);
			this.Controls.Add(this.emptyCheckBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.filterComboBox);
			this.Controls.Add(this.importantCheckBox);
			this.Controls.Add(this.ucGoodsGroup);
			this.Controls.Add(this.ucContractors);
			this.Controls.Add(this.toolStrip1);
			this.Name = "MarginControlParams";
			this.Load += new System.EventHandler(this.MarginControlParams_Load);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MarginControlParams_FormClosed);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.toolStrip1, 0);
			this.Controls.SetChildIndex(this.ucContractors, 0);
			this.Controls.SetChildIndex(this.ucGoodsGroup, 0);
			this.Controls.SetChildIndex(this.importantCheckBox, 0);
			this.Controls.SetChildIndex(this.filterComboBox, 0);
			this.Controls.SetChildIndex(this.label2, 0);
			this.Controls.SetChildIndex(this.emptyCheckBox, 0);
			this.Controls.SetChildIndex(this.codeCheckBox, 0);
			this.Controls.SetChildIndex(this.filtersDataGridView, 0);
			this.Controls.SetChildIndex(this.bindingNavigator1, 0);
			this.panel1.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize) (this.filtersDataGridView)).EndInit();
			((System.ComponentModel.ISupportInitialize) (this.table1BindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize) (this.marginControl_DS)).EndInit();
			((System.ComponentModel.ISupportInitialize) (this.bindingNavigator1)).EndInit();
			this.bindingNavigator1.ResumeLayout(false);
			this.bindingNavigator1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private ePlus.MetaData.Client.UCPluginMultiSelect ucContractors;
		private System.Windows.Forms.CheckBox importantCheckBox;
		private ePlus.CommonEx.Controls.SelectGoodsGroup ucGoodsGroup;
		private System.Windows.Forms.ComboBox filterComboBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox emptyCheckBox;
		private System.Windows.Forms.CheckBox codeCheckBox;
		private System.Windows.Forms.DataGridView filtersDataGridView;
		private System.Windows.Forms.DataGridViewTextBoxColumn lOWDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn hIGHDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn vALUEDataGridViewTextBoxColumn;
		private System.Windows.Forms.BindingSource table1BindingSource;
        private FCBMarginControlZNVLS.MarginControl_DS marginControl_DS;
		private System.Windows.Forms.BindingNavigator bindingNavigator1;
		private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
		private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
	}
}