using ePlus.CommonEx.Controls;

namespace ImportNovgorod
{
  partial class ParamsForm
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
      this.tbImportFileRemains = new System.Windows.Forms.TextBox();
      this.bSelectFile = new System.Windows.Forms.Button();
      this.label2 = new System.Windows.Forms.Label();
      this.tbImportLogFolder = new System.Windows.Forms.TextBox();
      this.bSelectFolder = new System.Windows.Forms.Button();
      this.panelResults = new System.Windows.Forms.Panel();
      this.panelRemains = new System.Windows.Forms.Panel();
      this.panelContracts = new System.Windows.Forms.Panel();
      this.tbImportFileContracts = new System.Windows.Forms.TextBox();
      this.bSelectFile2 = new System.Windows.Forms.Button();
      this.label3 = new System.Windows.Forms.Label();
      this.panelContractsFormat = new System.Windows.Forms.Panel();
      this.label4 = new System.Windows.Forms.Label();
      this.cbFormats = new System.Windows.Forms.ComboBox();
      this.groupBoxRemains = new System.Windows.Forms.GroupBox();
      this.checkBoxImportRemains = new System.Windows.Forms.CheckBox();
      this.groupBoxContracts = new System.Windows.Forms.GroupBox();
      this.panel1.SuspendLayout();
      this.panelResults.SuspendLayout();
      this.panelRemains.SuspendLayout();
      this.panelContracts.SuspendLayout();
      this.panelContractsFormat.SuspendLayout();
      this.groupBoxRemains.SuspendLayout();
      this.groupBoxContracts.SuspendLayout();
      this.SuspendLayout();
      // 
      // bOK
      // 
      this.bOK.Location = new System.Drawing.Point(191, 3);
      this.bOK.Size = new System.Drawing.Size(136, 23);
      this.bOK.Text = "Проверить и загрузить";
      // 
      // bClose
      // 
      this.bClose.Location = new System.Drawing.Point(327, 3);
      // 
      // panel1
      // 
      this.panel1.Location = new System.Drawing.Point(0, 239);
      this.panel1.Size = new System.Drawing.Size(405, 29);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(3, 4);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(126, 13);
      this.label1.TabIndex = 4;
      this.label1.Text = "Путь к файлу остатков:";
      // 
      // tbImportFileRemains
      // 
      this.tbImportFileRemains.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.tbImportFileRemains.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
      this.tbImportFileRemains.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllSystemSources;
      this.tbImportFileRemains.Location = new System.Drawing.Point(3, 20);
      this.tbImportFileRemains.Name = "tbImportFileRemains";
      this.tbImportFileRemains.Size = new System.Drawing.Size(363, 20);
      this.tbImportFileRemains.TabIndex = 5;
      // 
      // bSelectFile
      // 
      this.bSelectFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.bSelectFile.Location = new System.Drawing.Point(369, 20);
      this.bSelectFile.Name = "bSelectFile";
      this.bSelectFile.Size = new System.Drawing.Size(24, 20);
      this.bSelectFile.TabIndex = 6;
      this.bSelectFile.Text = "...";
      this.bSelectFile.UseVisualStyleBackColor = true;
      this.bSelectFile.Click += new System.EventHandler(this.bSelectFile_Click);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(3, 6);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(286, 13);
      this.label2.TabIndex = 7;
      this.label2.Text = "Путь к каталогу для сохранения результатов импорта:";
      // 
      // tbImportLogFolder
      // 
      this.tbImportLogFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.tbImportLogFolder.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
      this.tbImportLogFolder.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
      this.tbImportLogFolder.Location = new System.Drawing.Point(3, 22);
      this.tbImportLogFolder.Name = "tbImportLogFolder";
      this.tbImportLogFolder.Size = new System.Drawing.Size(369, 20);
      this.tbImportLogFolder.TabIndex = 8;
      // 
      // bSelectFolder
      // 
      this.bSelectFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.bSelectFolder.Location = new System.Drawing.Point(375, 21);
      this.bSelectFolder.Name = "bSelectFolder";
      this.bSelectFolder.Size = new System.Drawing.Size(24, 20);
      this.bSelectFolder.TabIndex = 9;
      this.bSelectFolder.Text = "...";
      this.bSelectFolder.UseVisualStyleBackColor = true;
      this.bSelectFolder.Click += new System.EventHandler(this.bSelectFolder_Click);
      // 
      // panelResults
      // 
      this.panelResults.Controls.Add(this.label2);
      this.panelResults.Controls.Add(this.bSelectFolder);
      this.panelResults.Controls.Add(this.tbImportLogFolder);
      this.panelResults.Dock = System.Windows.Forms.DockStyle.Top;
      this.panelResults.Location = new System.Drawing.Point(0, 193);
      this.panelResults.Name = "panelResults";
      this.panelResults.Size = new System.Drawing.Size(405, 47);
      this.panelResults.TabIndex = 11;
      // 
      // panelRemains
      // 
      this.panelRemains.Controls.Add(this.tbImportFileRemains);
      this.panelRemains.Controls.Add(this.bSelectFile);
      this.panelRemains.Controls.Add(this.label1);
      this.panelRemains.Enabled = false;
      this.panelRemains.Location = new System.Drawing.Point(3, 29);
      this.panelRemains.Name = "panelRemains";
      this.panelRemains.Size = new System.Drawing.Size(399, 50);
      this.panelRemains.TabIndex = 12;
      // 
      // panelContracts
      // 
      this.panelContracts.Controls.Add(this.tbImportFileContracts);
      this.panelContracts.Controls.Add(this.bSelectFile2);
      this.panelContracts.Controls.Add(this.label3);
      this.panelContracts.Dock = System.Windows.Forms.DockStyle.Top;
      this.panelContracts.Enabled = false;
      this.panelContracts.Location = new System.Drawing.Point(3, 63);
      this.panelContracts.Name = "panelContracts";
      this.panelContracts.Size = new System.Drawing.Size(399, 45);
      this.panelContracts.TabIndex = 13;
      // 
      // tbImportFileContracts
      // 
      this.tbImportFileContracts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.tbImportFileContracts.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
      this.tbImportFileContracts.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllSystemSources;
      this.tbImportFileContracts.Location = new System.Drawing.Point(3, 20);
      this.tbImportFileContracts.Name = "tbImportFileContracts";
      this.tbImportFileContracts.Size = new System.Drawing.Size(363, 20);
      this.tbImportFileContracts.TabIndex = 5;
      // 
      // bSelectFile2
      // 
      this.bSelectFile2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.bSelectFile2.Location = new System.Drawing.Point(369, 20);
      this.bSelectFile2.Name = "bSelectFile2";
      this.bSelectFile2.Size = new System.Drawing.Size(24, 20);
      this.bSelectFile2.TabIndex = 6;
      this.bSelectFile2.Text = "...";
      this.bSelectFile2.UseVisualStyleBackColor = true;
      this.bSelectFile2.Click += new System.EventHandler(this.bSelectFile2_Click);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(3, 4);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(133, 13);
      this.label3.TabIndex = 4;
      this.label3.Text = "Путь к файлу договоров:";
      // 
      // panelContractsFormat
      // 
      this.panelContractsFormat.Controls.Add(this.label4);
      this.panelContractsFormat.Controls.Add(this.cbFormats);
      this.panelContractsFormat.Dock = System.Windows.Forms.DockStyle.Top;
      this.panelContractsFormat.Location = new System.Drawing.Point(3, 16);
      this.panelContractsFormat.Name = "panelContractsFormat";
      this.panelContractsFormat.Size = new System.Drawing.Size(399, 47);
      this.panelContractsFormat.TabIndex = 14;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(4, 4);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(52, 13);
      this.label4.TabIndex = 1;
      this.label4.Text = "Формат:";
      // 
      // cbFormats
      // 
      this.cbFormats.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.cbFormats.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbFormats.FormattingEnabled = true;
      this.cbFormats.Items.AddRange(new object[] {
            "Нет файла договоров",
            "Файл ГК (спецификаций контрактов)",
            "Файл разнарядок (заявок)"});
      this.cbFormats.Location = new System.Drawing.Point(4, 20);
      this.cbFormats.Name = "cbFormats";
      this.cbFormats.Size = new System.Drawing.Size(387, 21);
      this.cbFormats.TabIndex = 0;
      this.cbFormats.SelectedIndexChanged += new System.EventHandler(this.cbFormats_SelectedIndexChanged);
      // 
      // groupBoxRemains
      // 
      this.groupBoxRemains.Controls.Add(this.checkBoxImportRemains);
      this.groupBoxRemains.Controls.Add(this.panelRemains);
      this.groupBoxRemains.Dock = System.Windows.Forms.DockStyle.Top;
      this.groupBoxRemains.Location = new System.Drawing.Point(0, 0);
      this.groupBoxRemains.Name = "groupBoxRemains";
      this.groupBoxRemains.Size = new System.Drawing.Size(405, 79);
      this.groupBoxRemains.TabIndex = 4;
      this.groupBoxRemains.TabStop = false;
      this.groupBoxRemains.Text = "Остатки";
      // 
      // checkBoxImportRemains
      // 
      this.checkBoxImportRemains.AutoSize = true;
      this.checkBoxImportRemains.Location = new System.Drawing.Point(7, 13);
      this.checkBoxImportRemains.Name = "checkBoxImportRemains";
      this.checkBoxImportRemains.Size = new System.Drawing.Size(149, 17);
      this.checkBoxImportRemains.TabIndex = 13;
      this.checkBoxImportRemains.Text = "Импортировать остатки";
      this.checkBoxImportRemains.UseVisualStyleBackColor = true;
      this.checkBoxImportRemains.CheckedChanged += new System.EventHandler(this.checkBoxImportRemains_CheckedChanged);
      // 
      // groupBoxContracts
      // 
      this.groupBoxContracts.Controls.Add(this.panelContracts);
      this.groupBoxContracts.Controls.Add(this.panelContractsFormat);
      this.groupBoxContracts.Dock = System.Windows.Forms.DockStyle.Top;
      this.groupBoxContracts.Location = new System.Drawing.Point(0, 79);
      this.groupBoxContracts.Name = "groupBoxContracts";
      this.groupBoxContracts.Size = new System.Drawing.Size(405, 114);
      this.groupBoxContracts.TabIndex = 4;
      this.groupBoxContracts.TabStop = false;
      this.groupBoxContracts.Text = "Договоры";
      // 
      // ParamsForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(405, 268);
      this.Controls.Add(this.panelResults);
      this.Controls.Add(this.groupBoxContracts);
      this.Controls.Add(this.groupBoxRemains);
      this.MinimumSize = new System.Drawing.Size(413, 292);
      this.Name = "ParamsForm";
      this.Text = "Импорт остатков, спецификаций договоров, заявок";
      this.Load += new System.EventHandler(this.ParamsForm_Load);
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ParamsForm_FormClosed);
      this.Controls.SetChildIndex(this.groupBoxRemains, 0);
      this.Controls.SetChildIndex(this.groupBoxContracts, 0);
      this.Controls.SetChildIndex(this.panelResults, 0);
      this.Controls.SetChildIndex(this.panel1, 0);
      this.panel1.ResumeLayout(false);
      this.panelResults.ResumeLayout(false);
      this.panelResults.PerformLayout();
      this.panelRemains.ResumeLayout(false);
      this.panelRemains.PerformLayout();
      this.panelContracts.ResumeLayout(false);
      this.panelContracts.PerformLayout();
      this.panelContractsFormat.ResumeLayout(false);
      this.panelContractsFormat.PerformLayout();
      this.groupBoxRemains.ResumeLayout(false);
      this.groupBoxRemains.PerformLayout();
      this.groupBoxContracts.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label label1;
      private System.Windows.Forms.TextBox tbImportFileRemains;
    private System.Windows.Forms.Button bSelectFile;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox tbImportLogFolder;
      private System.Windows.Forms.Button bSelectFolder;
    private System.Windows.Forms.Panel panelResults;
    private System.Windows.Forms.Panel panelRemains;
    private System.Windows.Forms.Panel panelContracts;
    private System.Windows.Forms.TextBox tbImportFileContracts;
    private System.Windows.Forms.Button bSelectFile2;
      private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Panel panelContractsFormat;
    private System.Windows.Forms.Label label4;
      private System.Windows.Forms.ComboBox cbFormats;
      private System.Windows.Forms.GroupBox groupBoxContracts;
      private System.Windows.Forms.GroupBox groupBoxRemains;
      private System.Windows.Forms.CheckBox checkBoxImportRemains;      
  }
}