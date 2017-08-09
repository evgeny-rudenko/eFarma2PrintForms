namespace R23MABC_analysis
{
    partial class R23MABC_analysisParams
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(R23MABC_analysisParams));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.btnDescription = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chklbTypeOut = new System.Windows.Forms.CheckedListBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.ucStore = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.rbStoreUnchecked = new System.Windows.Forms.RadioButton();
            this.rbStoreChecked = new System.Windows.Forms.RadioButton();
            this.ucPeriod = new ePlus.MetaData.Controls.UCPeriod();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ucGoodsKind = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.rbGoodsKindUnchecked = new System.Windows.Forms.RadioButton();
            this.rbGoodsKindChecked = new System.Windows.Forms.RadioButton();
            this.chkbAnalogUnite = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.ucGoods = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.rbGoodsUnchecked = new System.Windows.Forms.RadioButton();
            this.rbGoodsChecked = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ucGoodsGroup = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.rbGoodsGroupUnchecked = new System.Windows.Forms.RadioButton();
            this.rbGoodsGroupChecked = new System.Windows.Forms.RadioButton();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.numGrpC = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.numGrpB = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.numGrpA = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbTypeCalc = new System.Windows.Forms.ComboBox();
            this.lstbTabControl = new System.Windows.Forms.ListBox();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGrpC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGrpB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGrpA)).BeginInit();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(620, 3);
            this.bOK.Size = new System.Drawing.Size(75, 26);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(695, 3);
            this.bClose.Size = new System.Drawing.Size(75, 26);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDescription);
            this.panel1.Location = new System.Drawing.Point(0, 386);
            this.panel1.Size = new System.Drawing.Size(773, 32);
            this.panel1.Controls.SetChildIndex(this.bClose, 0);
            this.panel1.Controls.SetChildIndex(this.bOK, 0);
            this.panel1.Controls.SetChildIndex(this.btnDescription, 0);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(773, 25);
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
            // btnDescription
            // 
            this.btnDescription.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnDescription.Location = new System.Drawing.Point(3, 3);
            this.btnDescription.Name = "btnDescription";
            this.btnDescription.Size = new System.Drawing.Size(141, 26);
            this.btnDescription.TabIndex = 204;
            this.btnDescription.Text = "Описание отчета";
            this.btnDescription.UseVisualStyleBackColor = true;
            this.btnDescription.Click += new System.EventHandler(this.btnDescription_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(155, 32);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(7);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(606, 347);
            this.tabControl1.TabIndex = 177;
            // 
            // tabPage1
            // 
            this.tabPage1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.groupBox6);
            this.tabPage1.Controls.Add(this.ucPeriod);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(598, 321);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Извлечение данных";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chklbTypeOut);
            this.groupBox1.Location = new System.Drawing.Point(320, 45);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(248, 102);
            this.groupBox1.TabIndex = 208;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Виды расхода";
            // 
            // chklbTypeOut
            // 
            this.chklbTypeOut.BackColor = System.Drawing.SystemColors.Control;
            this.chklbTypeOut.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chklbTypeOut.CheckOnClick = true;
            this.chklbTypeOut.FormattingEnabled = true;
            this.chklbTypeOut.Items.AddRange(new object[] {
            "Чек",
            "Перемещение в ЦО",
            "Внутреннее перемещение",
            "Перемещение между подразделениями",
            "Расходная накладная"});
            this.chklbTypeOut.Location = new System.Drawing.Point(6, 19);
            this.chklbTypeOut.Name = "chklbTypeOut";
            this.chklbTypeOut.Size = new System.Drawing.Size(236, 75);
            this.chklbTypeOut.TabIndex = 2;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.ucStore);
            this.groupBox6.Controls.Add(this.rbStoreUnchecked);
            this.groupBox6.Controls.Add(this.rbStoreChecked);
            this.groupBox6.Location = new System.Drawing.Point(18, 45);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(282, 143);
            this.groupBox6.TabIndex = 207;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Склады";
            // 
            // ucStore
            // 
            this.ucStore.AllowSaveState = false;
            this.ucStore.Caption = "Склады";
            this.ucStore.Location = new System.Drawing.Point(6, 42);
            this.ucStore.Mnemocode = "STORE";
            this.ucStore.Name = "ucStore";
            this.ucStore.Size = new System.Drawing.Size(262, 95);
            this.ucStore.TabIndex = 195;
            // 
            // rbStoreUnchecked
            // 
            this.rbStoreUnchecked.AutoSize = true;
            this.rbStoreUnchecked.Location = new System.Drawing.Point(139, 19);
            this.rbStoreUnchecked.Name = "rbStoreUnchecked";
            this.rbStoreUnchecked.Size = new System.Drawing.Size(118, 17);
            this.rbStoreUnchecked.TabIndex = 2;
            this.rbStoreUnchecked.Text = "Кроме выбранных";
            this.rbStoreUnchecked.UseVisualStyleBackColor = true;
            // 
            // rbStoreChecked
            // 
            this.rbStoreChecked.AutoSize = true;
            this.rbStoreChecked.Checked = true;
            this.rbStoreChecked.Location = new System.Drawing.Point(6, 19);
            this.rbStoreChecked.Name = "rbStoreChecked";
            this.rbStoreChecked.Size = new System.Drawing.Size(84, 17);
            this.rbStoreChecked.TabIndex = 0;
            this.rbStoreChecked.TabStop = true;
            this.rbStoreChecked.Text = "Выбранные";
            this.rbStoreChecked.UseVisualStyleBackColor = true;
            // 
            // ucPeriod
            // 
            this.ucPeriod.DateFrom = new System.DateTime(2011, 2, 15, 14, 11, 25, 890);
            this.ucPeriod.DateTo = new System.DateTime(2011, 2, 15, 14, 11, 25, 890);
            this.ucPeriod.Location = new System.Drawing.Point(72, 18);
            this.ucPeriod.Name = "ucPeriod";
            this.ucPeriod.Size = new System.Drawing.Size(222, 21);
            this.ucPeriod.TabIndex = 206;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 205;
            this.label1.Text = "Период";
            // 
            // tabPage2
            // 
            this.tabPage2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.chkbAnalogUnite);
            this.tabPage2.Controls.Add(this.groupBox5);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(7);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(598, 321);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Товары";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ucGoodsKind);
            this.groupBox3.Controls.Add(this.rbGoodsKindUnchecked);
            this.groupBox3.Controls.Add(this.rbGoodsKindChecked);
            this.groupBox3.Location = new System.Drawing.Point(9, 164);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(282, 143);
            this.groupBox3.TabIndex = 201;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Виды товаров";
            // 
            // ucGoodsKind
            // 
            this.ucGoodsKind.AllowSaveState = false;
            this.ucGoodsKind.Caption = "Виды товаров";
            this.ucGoodsKind.Location = new System.Drawing.Point(6, 42);
            this.ucGoodsKind.Mnemocode = "GOODS_KIND";
            this.ucGoodsKind.Name = "ucGoodsKind";
            this.ucGoodsKind.Size = new System.Drawing.Size(262, 95);
            this.ucGoodsKind.TabIndex = 195;
            // 
            // rbGoodsKindUnchecked
            // 
            this.rbGoodsKindUnchecked.AutoSize = true;
            this.rbGoodsKindUnchecked.Location = new System.Drawing.Point(139, 19);
            this.rbGoodsKindUnchecked.Name = "rbGoodsKindUnchecked";
            this.rbGoodsKindUnchecked.Size = new System.Drawing.Size(118, 17);
            this.rbGoodsKindUnchecked.TabIndex = 2;
            this.rbGoodsKindUnchecked.Text = "Кроме выбранных";
            this.rbGoodsKindUnchecked.UseVisualStyleBackColor = true;
            // 
            // rbGoodsKindChecked
            // 
            this.rbGoodsKindChecked.AutoSize = true;
            this.rbGoodsKindChecked.Checked = true;
            this.rbGoodsKindChecked.Location = new System.Drawing.Point(6, 19);
            this.rbGoodsKindChecked.Name = "rbGoodsKindChecked";
            this.rbGoodsKindChecked.Size = new System.Drawing.Size(84, 17);
            this.rbGoodsKindChecked.TabIndex = 0;
            this.rbGoodsKindChecked.TabStop = true;
            this.rbGoodsKindChecked.Text = "Выбранные";
            this.rbGoodsKindChecked.UseVisualStyleBackColor = true;
            // 
            // chkbAnalogUnite
            // 
            this.chkbAnalogUnite.AutoSize = true;
            this.chkbAnalogUnite.Checked = true;
            this.chkbAnalogUnite.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkbAnalogUnite.Location = new System.Drawing.Point(332, 196);
            this.chkbAnalogUnite.Name = "chkbAnalogUnite";
            this.chkbAnalogUnite.Size = new System.Drawing.Size(201, 17);
            this.chkbAnalogUnite.TabIndex = 200;
            this.chkbAnalogUnite.Text = "Сворачивать по группам аналогов";
            this.chkbAnalogUnite.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.ucGoods);
            this.groupBox5.Controls.Add(this.rbGoodsUnchecked);
            this.groupBox5.Controls.Add(this.rbGoodsChecked);
            this.groupBox5.Location = new System.Drawing.Point(306, 15);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(282, 143);
            this.groupBox5.TabIndex = 199;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Товары";
            // 
            // ucGoods
            // 
            this.ucGoods.AllowSaveState = false;
            this.ucGoods.Caption = "Товары";
            this.ucGoods.Location = new System.Drawing.Point(6, 42);
            this.ucGoods.Mnemocode = "GOODS2";
            this.ucGoods.Name = "ucGoods";
            this.ucGoods.Size = new System.Drawing.Size(262, 95);
            this.ucGoods.TabIndex = 195;
            // 
            // rbGoodsUnchecked
            // 
            this.rbGoodsUnchecked.AutoSize = true;
            this.rbGoodsUnchecked.Location = new System.Drawing.Point(139, 19);
            this.rbGoodsUnchecked.Name = "rbGoodsUnchecked";
            this.rbGoodsUnchecked.Size = new System.Drawing.Size(118, 17);
            this.rbGoodsUnchecked.TabIndex = 2;
            this.rbGoodsUnchecked.Text = "Кроме выбранных";
            this.rbGoodsUnchecked.UseVisualStyleBackColor = true;
            // 
            // rbGoodsChecked
            // 
            this.rbGoodsChecked.AutoSize = true;
            this.rbGoodsChecked.Checked = true;
            this.rbGoodsChecked.Location = new System.Drawing.Point(6, 19);
            this.rbGoodsChecked.Name = "rbGoodsChecked";
            this.rbGoodsChecked.Size = new System.Drawing.Size(84, 17);
            this.rbGoodsChecked.TabIndex = 0;
            this.rbGoodsChecked.TabStop = true;
            this.rbGoodsChecked.Text = "Выбранные";
            this.rbGoodsChecked.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ucGoodsGroup);
            this.groupBox2.Controls.Add(this.rbGoodsGroupUnchecked);
            this.groupBox2.Controls.Add(this.rbGoodsGroupChecked);
            this.groupBox2.Location = new System.Drawing.Point(9, 15);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(282, 143);
            this.groupBox2.TabIndex = 196;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Группы товаров";
            // 
            // ucGoodsGroup
            // 
            this.ucGoodsGroup.AllowSaveState = false;
            this.ucGoodsGroup.Caption = "Группы товаров";
            this.ucGoodsGroup.Location = new System.Drawing.Point(6, 42);
            this.ucGoodsGroup.Mnemocode = "GOODS_GROUP";
            this.ucGoodsGroup.Name = "ucGoodsGroup";
            this.ucGoodsGroup.Size = new System.Drawing.Size(262, 95);
            this.ucGoodsGroup.TabIndex = 195;
            // 
            // rbGoodsGroupUnchecked
            // 
            this.rbGoodsGroupUnchecked.AutoSize = true;
            this.rbGoodsGroupUnchecked.Location = new System.Drawing.Point(139, 19);
            this.rbGoodsGroupUnchecked.Name = "rbGoodsGroupUnchecked";
            this.rbGoodsGroupUnchecked.Size = new System.Drawing.Size(118, 17);
            this.rbGoodsGroupUnchecked.TabIndex = 2;
            this.rbGoodsGroupUnchecked.Text = "Кроме выбранных";
            this.rbGoodsGroupUnchecked.UseVisualStyleBackColor = true;
            // 
            // rbGoodsGroupChecked
            // 
            this.rbGoodsGroupChecked.AutoSize = true;
            this.rbGoodsGroupChecked.Checked = true;
            this.rbGoodsGroupChecked.Location = new System.Drawing.Point(6, 19);
            this.rbGoodsGroupChecked.Name = "rbGoodsGroupChecked";
            this.rbGoodsGroupChecked.Size = new System.Drawing.Size(84, 17);
            this.rbGoodsGroupChecked.TabIndex = 0;
            this.rbGoodsGroupChecked.TabStop = true;
            this.rbGoodsGroupChecked.Text = "Выбранные";
            this.rbGoodsGroupChecked.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage3.Controls.Add(this.numGrpC);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.numGrpB);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.numGrpA);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.cbTypeCalc);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(598, 321);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Группы ABC";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // numGrpC
            // 
            this.numGrpC.Enabled = false;
            this.numGrpC.Location = new System.Drawing.Point(144, 105);
            this.numGrpC.Name = "numGrpC";
            this.numGrpC.Size = new System.Drawing.Size(200, 20);
            this.numGrpC.TabIndex = 9;
            this.numGrpC.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 107);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Процент группы C:";
            // 
            // numGrpB
            // 
            this.numGrpB.Location = new System.Drawing.Point(144, 79);
            this.numGrpB.Name = "numGrpB";
            this.numGrpB.Size = new System.Drawing.Size(200, 20);
            this.numGrpB.TabIndex = 7;
            this.numGrpB.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numGrpB.ValueChanged += new System.EventHandler(this.numGrpB_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 81);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Процент группы B:";
            // 
            // numGrpA
            // 
            this.numGrpA.Location = new System.Drawing.Point(144, 53);
            this.numGrpA.Name = "numGrpA";
            this.numGrpA.Size = new System.Drawing.Size(200, 20);
            this.numGrpA.TabIndex = 5;
            this.numGrpA.Value = new decimal(new int[] {
            75,
            0,
            0,
            0});
            this.numGrpA.ValueChanged += new System.EventHandler(this.numGrpA_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Процент группы A:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Способ расчета сумм";
            // 
            // cbTypeCalc
            // 
            this.cbTypeCalc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTypeCalc.FormattingEnabled = true;
            this.cbTypeCalc.Items.AddRange(new object[] {
            "Объем продаж",
            "Количество проданных",
            "Получение прибыли"});
            this.cbTypeCalc.Location = new System.Drawing.Point(144, 24);
            this.cbTypeCalc.Name = "cbTypeCalc";
            this.cbTypeCalc.Size = new System.Drawing.Size(200, 21);
            this.cbTypeCalc.TabIndex = 0;
            // 
            // lstbTabControl
            // 
            this.lstbTabControl.FormattingEnabled = true;
            this.lstbTabControl.Items.AddRange(new object[] {
            "Извлечение данных",
            "Товары",
            "Группы ABC"});
            this.lstbTabControl.Location = new System.Drawing.Point(20, 41);
            this.lstbTabControl.Name = "lstbTabControl";
            this.lstbTabControl.Size = new System.Drawing.Size(120, 329);
            this.lstbTabControl.TabIndex = 178;
            this.lstbTabControl.SelectedIndexChanged += new System.EventHandler(this.lstbTabControl_SelectedIndexChanged);
            // 
            // R23MABC_analysisParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(773, 418);
            this.Controls.Add(this.lstbTabControl);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "R23MABC_analysisParams";
            this.Load += new System.EventHandler(this.R23MABC_analysisParams_Load);
            this.Controls.SetChildIndex(this.toolStrip1, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.Controls.SetChildIndex(this.lstbTabControl, 0);
            this.panel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGrpC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGrpB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGrpA)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.Button btnDescription;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox chklbTypeOut;
        private System.Windows.Forms.GroupBox groupBox6;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucStore;
        private System.Windows.Forms.RadioButton rbStoreUnchecked;
        private System.Windows.Forms.RadioButton rbStoreChecked;
        private ePlus.MetaData.Controls.UCPeriod ucPeriod;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox3;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucGoodsKind;
        private System.Windows.Forms.RadioButton rbGoodsKindUnchecked;
        private System.Windows.Forms.RadioButton rbGoodsKindChecked;
        private System.Windows.Forms.CheckBox chkbAnalogUnite;
        private System.Windows.Forms.GroupBox groupBox5;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucGoods;
        private System.Windows.Forms.RadioButton rbGoodsUnchecked;
        private System.Windows.Forms.RadioButton rbGoodsChecked;
        private System.Windows.Forms.GroupBox groupBox2;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucGoodsGroup;
        private System.Windows.Forms.RadioButton rbGoodsGroupUnchecked;
        private System.Windows.Forms.RadioButton rbGoodsGroupChecked;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.NumericUpDown numGrpC;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numGrpB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numGrpA;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbTypeCalc;
        private System.Windows.Forms.ListBox lstbTabControl;
	}
}