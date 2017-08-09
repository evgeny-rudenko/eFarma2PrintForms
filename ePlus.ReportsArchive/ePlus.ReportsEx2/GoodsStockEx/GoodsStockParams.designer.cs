namespace GoodsKeepingTimeListEx
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InvoiceRemainsParams));
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.filtersTabContol = new System.Windows.Forms.TabControl();
			this.storesTabPage = new System.Windows.Forms.TabPage();
			this.storesPluginMultiSelect = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.contractorsTabPage = new System.Windows.Forms.TabPage();
			this.contractorsPluginMultiSelect = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.producersTabPage = new System.Windows.Forms.TabPage();
			this.producersPluginMultiSelect = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.goodsTabPage = new System.Windows.Forms.TabPage();
			this.goodsPluginMultiSelect = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.farmGroupsTabPage = new System.Windows.Forms.TabPage();
			this.farmGroupsPluginMultiSelect = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.bestBeforesTabPage = new System.Windows.Forms.TabPage();
			this.toDateTimePicker = new System.Windows.Forms.DateTimePicker();
			this.fromDateTimePicker = new System.Windows.Forms.DateTimePicker();
			this.bestBeforeComboBox = new System.Windows.Forms.ComboBox();
			this.toLabel = new System.Windows.Forms.Label();
			this.fromLabel = new System.Windows.Forms.Label();
			this.bestBeforeLabel = new System.Windows.Forms.Label();
			this.vatRatesTabPage = new System.Windows.Forms.TabPage();
			this.vatRatesPluginMultiSelect = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.remainsDateTabPage = new System.Windows.Forms.TabPage();
			this.remainDateCheckBox = new System.Windows.Forms.CheckBox();
			this.remainsDateDateTimePicker = new System.Windows.Forms.DateTimePicker();
			this.remainsDateLabel = new System.Windows.Forms.Label();
			this.storePlacesTabPage = new System.Windows.Forms.TabPage();
			this.storePlacesPluginMultiSelect = new ePlus.MetaData.Client.UCPluginMultiSelect();
			this.sortingTabPage = new System.Windows.Forms.TabPage();
			this.goodsNameSortingComboBox = new System.Windows.Forms.ComboBox();
			this.storePlaceSortingComboBox = new System.Windows.Forms.ComboBox();
			this.storeNameSortingComboBox = new System.Windows.Forms.ComboBox();
			this.goodsNameSortingLabel = new System.Windows.Forms.Label();
			this.storePlaceSortingLabel = new System.Windows.Forms.Label();
			this.storeNameSortingLabel = new System.Windows.Forms.Label();
			this.filtersLabel = new System.Windows.Forms.Label();
			this.filtersDataGridView = new System.Windows.Forms.DataGridView();
			this.shortFormCheckBox = new System.Windows.Forms.CheckBox();
			this.panel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.filtersTabContol.SuspendLayout();
			this.storesTabPage.SuspendLayout();
			this.contractorsTabPage.SuspendLayout();
			this.producersTabPage.SuspendLayout();
			this.goodsTabPage.SuspendLayout();
			this.farmGroupsTabPage.SuspendLayout();
			this.bestBeforesTabPage.SuspendLayout();
			this.vatRatesTabPage.SuspendLayout();
			this.remainsDateTabPage.SuspendLayout();
			this.storePlacesTabPage.SuspendLayout();
			this.sortingTabPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) (this.filtersDataGridView)).BeginInit();
			this.SuspendLayout();
			// 
			// bOK
			// 
			this.bOK.Location = new System.Drawing.Point(528, 3);
			// 
			// bClose
			// 
			this.bClose.Location = new System.Drawing.Point(603, 3);
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(0, 473);
			this.panel1.Size = new System.Drawing.Size(681, 29);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
			this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(681, 25);
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
			// filtersTabContol
			// 
			this.filtersTabContol.Controls.Add(this.storesTabPage);
			this.filtersTabContol.Controls.Add(this.contractorsTabPage);
			this.filtersTabContol.Controls.Add(this.producersTabPage);
			this.filtersTabContol.Controls.Add(this.goodsTabPage);
			this.filtersTabContol.Controls.Add(this.farmGroupsTabPage);
			this.filtersTabContol.Controls.Add(this.bestBeforesTabPage);
			this.filtersTabContol.Controls.Add(this.vatRatesTabPage);
			this.filtersTabContol.Controls.Add(this.remainsDateTabPage);
			this.filtersTabContol.Controls.Add(this.storePlacesTabPage);
			this.filtersTabContol.Controls.Add(this.sortingTabPage);
			this.filtersTabContol.Location = new System.Drawing.Point(12, 39);
			this.filtersTabContol.Name = "filtersTabContol";
			this.filtersTabContol.SelectedIndex = 0;
			this.filtersTabContol.Size = new System.Drawing.Size(607, 182);
			this.filtersTabContol.TabIndex = 10;
			// 
			// storesTabPage
			// 
			this.storesTabPage.Controls.Add(this.storesPluginMultiSelect);
			this.storesTabPage.Location = new System.Drawing.Point(4, 22);
			this.storesTabPage.Name = "storesTabPage";
			this.storesTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.storesTabPage.Size = new System.Drawing.Size(599, 156);
			this.storesTabPage.TabIndex = 0;
			this.storesTabPage.Text = "Отделы аптеки";
			// 
			// storesPluginMultiSelect
			// 
			this.storesPluginMultiSelect.AllowSaveState = false;
			this.storesPluginMultiSelect.Caption = "Склады";
			this.storesPluginMultiSelect.Dock = System.Windows.Forms.DockStyle.Fill;
			this.storesPluginMultiSelect.Location = new System.Drawing.Point(3, 3);
			this.storesPluginMultiSelect.Mnemocode = "STORE";
			this.storesPluginMultiSelect.Name = "storesPluginMultiSelect";
			this.storesPluginMultiSelect.Size = new System.Drawing.Size(593, 150);
			this.storesPluginMultiSelect.TabIndex = 11;
			this.storesPluginMultiSelect.ValuesListChanged += new ePlus.MetaData.Client.UCPluginMultiSelect.ValuesListChangedDelegate(this.storesPluginMultiSelect_ValuesListChanged);
			// 
			// contractorsTabPage
			// 
			this.contractorsTabPage.Controls.Add(this.contractorsPluginMultiSelect);
			this.contractorsTabPage.Location = new System.Drawing.Point(4, 22);
			this.contractorsTabPage.Name = "contractorsTabPage";
			this.contractorsTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.contractorsTabPage.Size = new System.Drawing.Size(599, 156);
			this.contractorsTabPage.TabIndex = 1;
			this.contractorsTabPage.Text = "Поставщики";
			// 
			// contractorsPluginMultiSelect
			// 
			this.contractorsPluginMultiSelect.AllowSaveState = false;
			this.contractorsPluginMultiSelect.Caption = "Поставщики";
			this.contractorsPluginMultiSelect.Dock = System.Windows.Forms.DockStyle.Fill;
			this.contractorsPluginMultiSelect.Location = new System.Drawing.Point(3, 3);
			this.contractorsPluginMultiSelect.Mnemocode = "CONTRACTOR";
			this.contractorsPluginMultiSelect.Name = "contractorsPluginMultiSelect";
			this.contractorsPluginMultiSelect.Size = new System.Drawing.Size(593, 150);
			this.contractorsPluginMultiSelect.TabIndex = 12;
			this.contractorsPluginMultiSelect.ValuesListChanged += new ePlus.MetaData.Client.UCPluginMultiSelect.ValuesListChangedDelegate(this.contractorsPluginMultiSelect_ValuesListChanged);
			// 
			// producersTabPage
			// 
			this.producersTabPage.Controls.Add(this.producersPluginMultiSelect);
			this.producersTabPage.Location = new System.Drawing.Point(4, 22);
			this.producersTabPage.Name = "producersTabPage";
			this.producersTabPage.Size = new System.Drawing.Size(599, 156);
			this.producersTabPage.TabIndex = 2;
			this.producersTabPage.Text = "Производители";
			// 
			// producersPluginMultiSelect
			// 
			this.producersPluginMultiSelect.AllowSaveState = false;
			this.producersPluginMultiSelect.Caption = "Производители";
			this.producersPluginMultiSelect.Dock = System.Windows.Forms.DockStyle.Fill;
			this.producersPluginMultiSelect.Location = new System.Drawing.Point(0, 0);
			this.producersPluginMultiSelect.Mnemocode = "PRODUCER";
			this.producersPluginMultiSelect.Name = "producersPluginMultiSelect";
			this.producersPluginMultiSelect.Size = new System.Drawing.Size(599, 156);
			this.producersPluginMultiSelect.TabIndex = 12;
			this.producersPluginMultiSelect.ValuesListChanged += new ePlus.MetaData.Client.UCPluginMultiSelect.ValuesListChangedDelegate(this.producersPluginMultiSelect_ValuesListChanged);
			// 
			// goodsTabPage
			// 
			this.goodsTabPage.Controls.Add(this.goodsPluginMultiSelect);
			this.goodsTabPage.Location = new System.Drawing.Point(4, 22);
			this.goodsTabPage.Name = "goodsTabPage";
			this.goodsTabPage.Size = new System.Drawing.Size(599, 156);
			this.goodsTabPage.TabIndex = 3;
			this.goodsTabPage.Text = "Медикаменты";
			// 
			// goodsPluginMultiSelect
			// 
			this.goodsPluginMultiSelect.AllowSaveState = false;
			this.goodsPluginMultiSelect.Caption = "Медикаменты";
			this.goodsPluginMultiSelect.Dock = System.Windows.Forms.DockStyle.Fill;
			this.goodsPluginMultiSelect.Location = new System.Drawing.Point(0, 0);
			this.goodsPluginMultiSelect.Mnemocode = "GOODS2";
			this.goodsPluginMultiSelect.Name = "goodsPluginMultiSelect";
			this.goodsPluginMultiSelect.Size = new System.Drawing.Size(599, 156);
			this.goodsPluginMultiSelect.TabIndex = 12;
			this.goodsPluginMultiSelect.ValuesListChanged += new ePlus.MetaData.Client.UCPluginMultiSelect.ValuesListChangedDelegate(this.goodsPluginMultiSelect_ValuesListChanged);
			// 
			// farmGroupsTabPage
			// 
			this.farmGroupsTabPage.Controls.Add(this.farmGroupsPluginMultiSelect);
			this.farmGroupsTabPage.Location = new System.Drawing.Point(4, 22);
			this.farmGroupsTabPage.Name = "farmGroupsTabPage";
			this.farmGroupsTabPage.Size = new System.Drawing.Size(599, 156);
			this.farmGroupsTabPage.TabIndex = 4;
			this.farmGroupsTabPage.Text = "Фарм. группы";
			// 
			// farmGroupsPluginMultiSelect
			// 
			this.farmGroupsPluginMultiSelect.AllowSaveState = false;
			this.farmGroupsPluginMultiSelect.Caption = "Фармацевтические группы";
			this.farmGroupsPluginMultiSelect.Dock = System.Windows.Forms.DockStyle.Fill;
			this.farmGroupsPluginMultiSelect.Location = new System.Drawing.Point(0, 0);
			this.farmGroupsPluginMultiSelect.Mnemocode = "ATS_Classifier";
			this.farmGroupsPluginMultiSelect.Name = "farmGroupsPluginMultiSelect";
			this.farmGroupsPluginMultiSelect.Size = new System.Drawing.Size(599, 156);
			this.farmGroupsPluginMultiSelect.TabIndex = 12;
			this.farmGroupsPluginMultiSelect.ValuesListChanged += new ePlus.MetaData.Client.UCPluginMultiSelect.ValuesListChangedDelegate(this.farmGroupsPluginMultiSelect_ValuesListChanged);
			// 
			// bestBeforesTabPage
			// 
			this.bestBeforesTabPage.Controls.Add(this.toDateTimePicker);
			this.bestBeforesTabPage.Controls.Add(this.fromDateTimePicker);
			this.bestBeforesTabPage.Controls.Add(this.bestBeforeComboBox);
			this.bestBeforesTabPage.Controls.Add(this.toLabel);
			this.bestBeforesTabPage.Controls.Add(this.fromLabel);
			this.bestBeforesTabPage.Controls.Add(this.bestBeforeLabel);
			this.bestBeforesTabPage.Location = new System.Drawing.Point(4, 22);
			this.bestBeforesTabPage.Name = "bestBeforesTabPage";
			this.bestBeforesTabPage.Size = new System.Drawing.Size(599, 156);
			this.bestBeforesTabPage.TabIndex = 5;
			this.bestBeforesTabPage.Text = "Сроки годности";
			// 
			// toDateTimePicker
			// 
			this.toDateTimePicker.Location = new System.Drawing.Point(122, 74);
			this.toDateTimePicker.Name = "toDateTimePicker";
			this.toDateTimePicker.Size = new System.Drawing.Size(200, 20);
			this.toDateTimePicker.TabIndex = 5;
			// 
			// fromDateTimePicker
			// 
			this.fromDateTimePicker.Location = new System.Drawing.Point(122, 48);
			this.fromDateTimePicker.Name = "fromDateTimePicker";
			this.fromDateTimePicker.Size = new System.Drawing.Size(200, 20);
			this.fromDateTimePicker.TabIndex = 4;
			// 
			// bestBeforeComboBox
			// 
			this.bestBeforeComboBox.BackColor = System.Drawing.SystemColors.Window;
			this.bestBeforeComboBox.FormattingEnabled = true;
			this.bestBeforeComboBox.Location = new System.Drawing.Point(122, 21);
			this.bestBeforeComboBox.Name = "bestBeforeComboBox";
			this.bestBeforeComboBox.Size = new System.Drawing.Size(200, 21);
			this.bestBeforeComboBox.TabIndex = 3;
			this.bestBeforeComboBox.SelectedIndexChanged += new System.EventHandler(this.bestBeforeComboBox_SelectedIndexChanged);
			// 
			// toLabel
			// 
			this.toLabel.AutoSize = true;
			this.toLabel.Location = new System.Drawing.Point(18, 78);
			this.toLabel.Name = "toLabel";
			this.toLabel.Size = new System.Drawing.Size(19, 13);
			this.toLabel.TabIndex = 2;
			this.toLabel.Text = "по";
			// 
			// fromLabel
			// 
			this.fromLabel.AutoSize = true;
			this.fromLabel.Location = new System.Drawing.Point(18, 52);
			this.fromLabel.Name = "fromLabel";
			this.fromLabel.Size = new System.Drawing.Size(45, 13);
			this.fromLabel.TabIndex = 1;
			this.fromLabel.Text = "Период";
			// 
			// bestBeforeLabel
			// 
			this.bestBeforeLabel.AutoSize = true;
			this.bestBeforeLabel.Location = new System.Drawing.Point(18, 24);
			this.bestBeforeLabel.Name = "bestBeforeLabel";
			this.bestBeforeLabel.Size = new System.Drawing.Size(84, 13);
			this.bestBeforeLabel.TabIndex = 0;
			this.bestBeforeLabel.Text = "Срок годности:";
			// 
			// vatRatesTabPage
			// 
			this.vatRatesTabPage.Controls.Add(this.vatRatesPluginMultiSelect);
			this.vatRatesTabPage.Location = new System.Drawing.Point(4, 22);
			this.vatRatesTabPage.Name = "vatRatesTabPage";
			this.vatRatesTabPage.Size = new System.Drawing.Size(599, 156);
			this.vatRatesTabPage.TabIndex = 6;
			this.vatRatesTabPage.Text = "Ставки НДС";
			// 
			// vatRatesPluginMultiSelect
			// 
			this.vatRatesPluginMultiSelect.AllowSaveState = false;
			this.vatRatesPluginMultiSelect.Caption = "Ставки НДС";
			this.vatRatesPluginMultiSelect.Dock = System.Windows.Forms.DockStyle.Fill;
			this.vatRatesPluginMultiSelect.Location = new System.Drawing.Point(0, 0);
			this.vatRatesPluginMultiSelect.Mnemocode = "TAX_TYPE";
			this.vatRatesPluginMultiSelect.Name = "vatRatesPluginMultiSelect";
			this.vatRatesPluginMultiSelect.Size = new System.Drawing.Size(599, 156);
			this.vatRatesPluginMultiSelect.TabIndex = 12;
			this.vatRatesPluginMultiSelect.ValuesListChanged += new ePlus.MetaData.Client.UCPluginMultiSelect.ValuesListChangedDelegate(this.vatRatesPluginMultiSelect_ValuesListChanged);
			// 
			// remainsDateTabPage
			// 
			this.remainsDateTabPage.Controls.Add(this.remainDateCheckBox);
			this.remainsDateTabPage.Controls.Add(this.remainsDateDateTimePicker);
			this.remainsDateTabPage.Controls.Add(this.remainsDateLabel);
			this.remainsDateTabPage.Location = new System.Drawing.Point(4, 22);
			this.remainsDateTabPage.Name = "remainsDateTabPage";
			this.remainsDateTabPage.Size = new System.Drawing.Size(599, 156);
			this.remainsDateTabPage.TabIndex = 7;
			this.remainsDateTabPage.Text = "Дата остатков";
			// 
			// remainDateCheckBox
			// 
			this.remainDateCheckBox.AutoSize = true;
			this.remainDateCheckBox.Location = new System.Drawing.Point(101, 25);
			this.remainDateCheckBox.Name = "remainDateCheckBox";
			this.remainDateCheckBox.Size = new System.Drawing.Size(15, 14);
			this.remainDateCheckBox.TabIndex = 2;
			this.remainDateCheckBox.UseVisualStyleBackColor = false;
			this.remainDateCheckBox.Visible = false;
			this.remainDateCheckBox.CheckedChanged += new System.EventHandler(this.remainDateCheckBox_CheckedChanged);
			// 
			// remainsDateDateTimePicker
			// 
			this.remainsDateDateTimePicker.Location = new System.Drawing.Point(122, 21);
			this.remainsDateDateTimePicker.Name = "remainsDateDateTimePicker";
			this.remainsDateDateTimePicker.Size = new System.Drawing.Size(200, 20);
			this.remainsDateDateTimePicker.TabIndex = 1;
			this.remainsDateDateTimePicker.ValueChanged += new System.EventHandler(this.remainDateCheckBox_CheckedChanged);
			// 
			// remainsDateLabel
			// 
			this.remainsDateLabel.AutoSize = true;
			this.remainsDateLabel.Location = new System.Drawing.Point(18, 24);
			this.remainsDateLabel.Name = "remainsDateLabel";
			this.remainsDateLabel.Size = new System.Drawing.Size(67, 13);
			this.remainsDateLabel.TabIndex = 0;
			this.remainsDateLabel.Text = "Остатки на:";
			// 
			// storePlacesTabPage
			// 
			this.storePlacesTabPage.Controls.Add(this.storePlacesPluginMultiSelect);
			this.storePlacesTabPage.Location = new System.Drawing.Point(4, 22);
			this.storePlacesTabPage.Name = "storePlacesTabPage";
			this.storePlacesTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.storePlacesTabPage.Size = new System.Drawing.Size(599, 156);
			this.storePlacesTabPage.TabIndex = 9;
			this.storePlacesTabPage.Text = "Места хранения";
			// 
			// storePlacesPluginMultiSelect
			// 
			this.storePlacesPluginMultiSelect.AllowSaveState = false;
			this.storePlacesPluginMultiSelect.Caption = "Места хранения";
			this.storePlacesPluginMultiSelect.Dock = System.Windows.Forms.DockStyle.Fill;
			this.storePlacesPluginMultiSelect.Location = new System.Drawing.Point(3, 3);
			this.storePlacesPluginMultiSelect.Mnemocode = "STORE_PLACE";
			this.storePlacesPluginMultiSelect.Name = "storePlacesPluginMultiSelect";
			this.storePlacesPluginMultiSelect.Size = new System.Drawing.Size(593, 150);
			this.storePlacesPluginMultiSelect.TabIndex = 13;
			this.storePlacesPluginMultiSelect.ValuesListChanged += new ePlus.MetaData.Client.UCPluginMultiSelect.ValuesListChangedDelegate(this.storePlacesPluginMultiSelect_ValuesListChanged);
			// 
			// sortingTabPage
			// 
			this.sortingTabPage.Controls.Add(this.goodsNameSortingComboBox);
			this.sortingTabPage.Controls.Add(this.storePlaceSortingComboBox);
			this.sortingTabPage.Controls.Add(this.storeNameSortingComboBox);
			this.sortingTabPage.Controls.Add(this.goodsNameSortingLabel);
			this.sortingTabPage.Controls.Add(this.storePlaceSortingLabel);
			this.sortingTabPage.Controls.Add(this.storeNameSortingLabel);
			this.sortingTabPage.Location = new System.Drawing.Point(4, 22);
			this.sortingTabPage.Name = "sortingTabPage";
			this.sortingTabPage.Size = new System.Drawing.Size(599, 156);
			this.sortingTabPage.TabIndex = 8;
			this.sortingTabPage.Text = "Сортировка";
			// 
			// goodsNameSortingComboBox
			// 
			this.goodsNameSortingComboBox.BackColor = System.Drawing.SystemColors.Window;
			this.goodsNameSortingComboBox.FormattingEnabled = true;
			this.goodsNameSortingComboBox.Location = new System.Drawing.Point(122, 75);
			this.goodsNameSortingComboBox.Name = "goodsNameSortingComboBox";
			this.goodsNameSortingComboBox.Size = new System.Drawing.Size(200, 21);
			this.goodsNameSortingComboBox.TabIndex = 5;
			// 
			// storePlaceSortingComboBox
			// 
			this.storePlaceSortingComboBox.BackColor = System.Drawing.SystemColors.Window;
			this.storePlaceSortingComboBox.FormattingEnabled = true;
			this.storePlaceSortingComboBox.Location = new System.Drawing.Point(122, 48);
			this.storePlaceSortingComboBox.Name = "storePlaceSortingComboBox";
			this.storePlaceSortingComboBox.Size = new System.Drawing.Size(200, 21);
			this.storePlaceSortingComboBox.TabIndex = 4;
			// 
			// storeNameSortingComboBox
			// 
			this.storeNameSortingComboBox.BackColor = System.Drawing.SystemColors.Window;
			this.storeNameSortingComboBox.FormattingEnabled = true;
			this.storeNameSortingComboBox.Location = new System.Drawing.Point(122, 21);
			this.storeNameSortingComboBox.Name = "storeNameSortingComboBox";
			this.storeNameSortingComboBox.Size = new System.Drawing.Size(200, 21);
			this.storeNameSortingComboBox.TabIndex = 3;
			this.storeNameSortingComboBox.SelectedIndexChanged += new System.EventHandler(this.storeNameSortingComboBox_SelectedIndexChanged);
			// 
			// goodsNameSortingLabel
			// 
			this.goodsNameSortingLabel.AutoSize = true;
			this.goodsNameSortingLabel.Location = new System.Drawing.Point(18, 78);
			this.goodsNameSortingLabel.Name = "goodsNameSortingLabel";
			this.goodsNameSortingLabel.Size = new System.Drawing.Size(86, 13);
			this.goodsNameSortingLabel.TabIndex = 2;
			this.goodsNameSortingLabel.Text = "Наименование:";
			// 
			// storePlaceSortingLabel
			// 
			this.storePlaceSortingLabel.AutoSize = true;
			this.storePlaceSortingLabel.Location = new System.Drawing.Point(18, 51);
			this.storePlaceSortingLabel.Name = "storePlaceSortingLabel";
			this.storePlaceSortingLabel.Size = new System.Drawing.Size(92, 13);
			this.storePlaceSortingLabel.TabIndex = 1;
			this.storePlaceSortingLabel.Text = "Место хранения:";
			// 
			// storeNameSortingLabel
			// 
			this.storeNameSortingLabel.AutoSize = true;
			this.storeNameSortingLabel.Location = new System.Drawing.Point(18, 24);
			this.storeNameSortingLabel.Name = "storeNameSortingLabel";
			this.storeNameSortingLabel.Size = new System.Drawing.Size(41, 13);
			this.storeNameSortingLabel.TabIndex = 0;
			this.storeNameSortingLabel.Text = "Склад:";
			// 
			// filtersLabel
			// 
			this.filtersLabel.AutoSize = true;
			this.filtersLabel.Location = new System.Drawing.Point(16, 236);
			this.filtersLabel.Name = "filtersLabel";
			this.filtersLabel.Size = new System.Drawing.Size(151, 13);
			this.filtersLabel.TabIndex = 104;
			this.filtersLabel.Text = "Итоговый список фильтров:";
			// 
			// filtersDataGridView
			// 
			this.filtersDataGridView.AllowUserToAddRows = false;
			this.filtersDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.filtersDataGridView.Location = new System.Drawing.Point(12, 264);
			this.filtersDataGridView.Name = "filtersDataGridView";
			this.filtersDataGridView.ReadOnly = true;
			this.filtersDataGridView.Size = new System.Drawing.Size(607, 149);
			this.filtersDataGridView.TabIndex = 105;
			// 
			// shortFormCheckBox
			// 
			this.shortFormCheckBox.AutoSize = true;
			this.shortFormCheckBox.Location = new System.Drawing.Point(19, 437);
			this.shortFormCheckBox.Name = "shortFormCheckBox";
			this.shortFormCheckBox.Size = new System.Drawing.Size(105, 17);
			this.shortFormCheckBox.TabIndex = 106;
			this.shortFormCheckBox.Text = "Краткая форма";
			this.shortFormCheckBox.UseVisualStyleBackColor = true;
			// 
			// InvoiceRemainsParams
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(681, 502);
			this.Controls.Add(this.shortFormCheckBox);
			this.Controls.Add(this.filtersDataGridView);
			this.Controls.Add(this.filtersLabel);
			this.Controls.Add(this.filtersTabContol);
			this.Controls.Add(this.toolStrip1);
			this.Name = "InvoiceRemainsParams";
			this.Controls.SetChildIndex(this.toolStrip1, 0);
			this.Controls.SetChildIndex(this.filtersTabContol, 0);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.filtersLabel, 0);
			this.Controls.SetChildIndex(this.filtersDataGridView, 0);
			this.Controls.SetChildIndex(this.shortFormCheckBox, 0);
			this.panel1.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.filtersTabContol.ResumeLayout(false);
			this.storesTabPage.ResumeLayout(false);
			this.contractorsTabPage.ResumeLayout(false);
			this.producersTabPage.ResumeLayout(false);
			this.goodsTabPage.ResumeLayout(false);
			this.farmGroupsTabPage.ResumeLayout(false);
			this.bestBeforesTabPage.ResumeLayout(false);
			this.bestBeforesTabPage.PerformLayout();
			this.vatRatesTabPage.ResumeLayout(false);
			this.remainsDateTabPage.ResumeLayout(false);
			this.remainsDateTabPage.PerformLayout();
			this.storePlacesTabPage.ResumeLayout(false);
			this.sortingTabPage.ResumeLayout(false);
			this.sortingTabPage.PerformLayout();
			((System.ComponentModel.ISupportInitialize) (this.filtersDataGridView)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.TabControl filtersTabContol;
		private System.Windows.Forms.TabPage storesTabPage;
		private ePlus.MetaData.Client.UCPluginMultiSelect storesPluginMultiSelect;
		private System.Windows.Forms.TabPage contractorsTabPage;
		private ePlus.MetaData.Client.UCPluginMultiSelect contractorsPluginMultiSelect;
		private System.Windows.Forms.TabPage producersTabPage;
		private ePlus.MetaData.Client.UCPluginMultiSelect producersPluginMultiSelect;
		private System.Windows.Forms.TabPage goodsTabPage;
		private ePlus.MetaData.Client.UCPluginMultiSelect goodsPluginMultiSelect;
		private System.Windows.Forms.TabPage farmGroupsTabPage;
		private ePlus.MetaData.Client.UCPluginMultiSelect farmGroupsPluginMultiSelect;
		private System.Windows.Forms.TabPage vatRatesTabPage;
		private ePlus.MetaData.Client.UCPluginMultiSelect vatRatesPluginMultiSelect;
		private System.Windows.Forms.TabPage remainsDateTabPage;
		private System.Windows.Forms.TabPage storePlacesTabPage;
		private ePlus.MetaData.Client.UCPluginMultiSelect storePlacesPluginMultiSelect;
		private System.Windows.Forms.TabPage sortingTabPage;
		private System.Windows.Forms.Label filtersLabel;
		private System.Windows.Forms.DataGridView filtersDataGridView;
		private System.Windows.Forms.Label toLabel;
		private System.Windows.Forms.Label fromLabel;
		private System.Windows.Forms.Label bestBeforeLabel;
		private System.Windows.Forms.TabPage bestBeforesTabPage;
		private System.Windows.Forms.DateTimePicker toDateTimePicker;
		private System.Windows.Forms.DateTimePicker fromDateTimePicker;
		private System.Windows.Forms.ComboBox bestBeforeComboBox;
		private System.Windows.Forms.DateTimePicker remainsDateDateTimePicker;
		private System.Windows.Forms.Label remainsDateLabel;
		private System.Windows.Forms.CheckBox shortFormCheckBox;
		private System.Windows.Forms.ComboBox goodsNameSortingComboBox;
		private System.Windows.Forms.ComboBox storePlaceSortingComboBox;
		private System.Windows.Forms.ComboBox storeNameSortingComboBox;
		private System.Windows.Forms.Label goodsNameSortingLabel;
		private System.Windows.Forms.Label storePlaceSortingLabel;
		private System.Windows.Forms.Label storeNameSortingLabel;
		private System.Windows.Forms.CheckBox remainDateCheckBox;
	}
}