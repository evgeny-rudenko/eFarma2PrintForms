using ePlus.CommonEx.Controls;

namespace RCYConvertData
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
        this.tbImportFile = new System.Windows.Forms.TextBox();
        this.bSelectFile = new System.Windows.Forms.Button();
        this.label2 = new System.Windows.Forms.Label();
        this.tbImportLogFolder = new System.Windows.Forms.TextBox();
        this.bSelectFolder = new System.Windows.Forms.Button();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.chkENVD = new System.Windows.Forms.CheckBox();
        this.panel2 = new System.Windows.Forms.Panel();
        this.panel3 = new System.Windows.Forms.Panel();
        this.panel4 = new System.Windows.Forms.Panel();
        this.tbImportFile2 = new System.Windows.Forms.TextBox();
        this.bSelectFile2 = new System.Windows.Forms.Button();
        this.label3 = new System.Windows.Forms.Label();
        this.panel5 = new System.Windows.Forms.Panel();
        this.label4 = new System.Windows.Forms.Label();
        this.cbFormats = new System.Windows.Forms.ComboBox();
        this.tabControl1 = new System.Windows.Forms.TabControl();
        this.tabPageRest = new System.Windows.Forms.TabPage();
        this.tabPageCards = new System.Windows.Forms.TabPage();
        this.panel7 = new System.Windows.Forms.Panel();
        this.textBoxDiscountCardType = new System.Windows.Forms.TextBox();
        this.buttonDiscountCardType = new System.Windows.Forms.Button();
        this.label6 = new System.Windows.Forms.Label();
        this.panel6 = new System.Windows.Forms.Panel();
        this.textBoxDiscountCard = new System.Windows.Forms.TextBox();
        this.buttonDiscountCard = new System.Windows.Forms.Button();
        this.label5 = new System.Windows.Forms.Label();
        this.chkUseVATFromEfarma = new System.Windows.Forms.CheckBox();
        this.panel1.SuspendLayout();
        this.groupBox1.SuspendLayout();
        this.panel2.SuspendLayout();
        this.panel3.SuspendLayout();
        this.panel4.SuspendLayout();
        this.panel5.SuspendLayout();
        this.tabControl1.SuspendLayout();
        this.tabPageRest.SuspendLayout();
        this.tabPageCards.SuspendLayout();
        this.panel7.SuspendLayout();
        this.panel6.SuspendLayout();
        this.SuspendLayout();
        // 
        // bOK
        // 
        this.bOK.Location = new System.Drawing.Point(257, 3);
        this.bOK.Size = new System.Drawing.Size(136, 23);
        this.bOK.Text = "Проверить и загрузить";
        // 
        // bClose
        // 
        this.bClose.Location = new System.Drawing.Point(393, 3);
        // 
        // panel1
        // 
        this.panel1.Location = new System.Drawing.Point(0, 325);
        this.panel1.Size = new System.Drawing.Size(471, 29);
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
        // tbImportFile
        // 
        this.tbImportFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.tbImportFile.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
        this.tbImportFile.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllSystemSources;
        this.tbImportFile.Location = new System.Drawing.Point(3, 20);
        this.tbImportFile.Name = "tbImportFile";
        this.tbImportFile.Size = new System.Drawing.Size(421, 20);
        this.tbImportFile.TabIndex = 5;
        // 
        // bSelectFile
        // 
        this.bSelectFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.bSelectFile.Location = new System.Drawing.Point(427, 20);
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
        this.tbImportLogFolder.Size = new System.Drawing.Size(435, 20);
        this.tbImportLogFolder.TabIndex = 8;
        // 
        // bSelectFolder
        // 
        this.bSelectFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.bSelectFolder.Location = new System.Drawing.Point(441, 21);
        this.bSelectFolder.Name = "bSelectFolder";
        this.bSelectFolder.Size = new System.Drawing.Size(24, 20);
        this.bSelectFolder.TabIndex = 9;
        this.bSelectFolder.Text = "...";
        this.bSelectFolder.UseVisualStyleBackColor = true;
        this.bSelectFolder.Click += new System.EventHandler(this.bSelectFolder_Click);
        // 
        // groupBox1
        // 
        this.groupBox1.Controls.Add(this.chkUseVATFromEfarma);
        this.groupBox1.Controls.Add(this.chkENVD);
        this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
        this.groupBox1.Location = new System.Drawing.Point(3, 93);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(457, 109);
        this.groupBox1.TabIndex = 10;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "Аптека и склад, на который переносятся остатки";
        // 
        // chkENVD
        // 
        this.chkENVD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.chkENVD.AutoSize = true;
        this.chkENVD.Checked = true;
        this.chkENVD.CheckState = System.Windows.Forms.CheckState.Checked;
        this.chkENVD.Location = new System.Drawing.Point(397, 76);
        this.chkENVD.Name = "chkENVD";
        this.chkENVD.Size = new System.Drawing.Size(57, 17);
        this.chkENVD.TabIndex = 4;
        this.chkENVD.Text = "ЕНВД";
        this.chkENVD.UseVisualStyleBackColor = true;
        // 
        // panel2
        // 
        this.panel2.Controls.Add(this.label2);
        this.panel2.Controls.Add(this.bSelectFolder);
        this.panel2.Controls.Add(this.tbImportLogFolder);
        this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
        this.panel2.Location = new System.Drawing.Point(0, 47);
        this.panel2.Name = "panel2";
        this.panel2.Size = new System.Drawing.Size(471, 47);
        this.panel2.TabIndex = 11;
        // 
        // panel3
        // 
        this.panel3.Controls.Add(this.tbImportFile);
        this.panel3.Controls.Add(this.bSelectFile);
        this.panel3.Controls.Add(this.label1);
        this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
        this.panel3.Location = new System.Drawing.Point(3, 3);
        this.panel3.Name = "panel3";
        this.panel3.Size = new System.Drawing.Size(457, 45);
        this.panel3.TabIndex = 12;
        // 
        // panel4
        // 
        this.panel4.Controls.Add(this.tbImportFile2);
        this.panel4.Controls.Add(this.bSelectFile2);
        this.panel4.Controls.Add(this.label3);
        this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
        this.panel4.Location = new System.Drawing.Point(3, 48);
        this.panel4.Name = "panel4";
        this.panel4.Size = new System.Drawing.Size(457, 45);
        this.panel4.TabIndex = 13;
        // 
        // tbImportFile2
        // 
        this.tbImportFile2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.tbImportFile2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
        this.tbImportFile2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllSystemSources;
        this.tbImportFile2.Location = new System.Drawing.Point(3, 20);
        this.tbImportFile2.Name = "tbImportFile2";
        this.tbImportFile2.Size = new System.Drawing.Size(421, 20);
        this.tbImportFile2.TabIndex = 5;
        // 
        // bSelectFile2
        // 
        this.bSelectFile2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.bSelectFile2.Location = new System.Drawing.Point(427, 20);
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
        this.label3.Size = new System.Drawing.Size(253, 13);
        this.label3.TabIndex = 4;
        this.label3.Text = "Путь к файлу соответствий кодов поставщиков:";
        // 
        // panel5
        // 
        this.panel5.Controls.Add(this.label4);
        this.panel5.Controls.Add(this.cbFormats);
        this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
        this.panel5.Location = new System.Drawing.Point(0, 0);
        this.panel5.Name = "panel5";
        this.panel5.Size = new System.Drawing.Size(471, 47);
        this.panel5.TabIndex = 14;
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
        this.cbFormats.Location = new System.Drawing.Point(4, 20);
        this.cbFormats.Name = "cbFormats";
        this.cbFormats.Size = new System.Drawing.Size(459, 21);
        this.cbFormats.TabIndex = 0;
        // 
        // tabControl1
        // 
        this.tabControl1.Controls.Add(this.tabPageRest);
        this.tabControl1.Controls.Add(this.tabPageCards);
        this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
        this.tabControl1.Location = new System.Drawing.Point(0, 94);
        this.tabControl1.Name = "tabControl1";
        this.tabControl1.SelectedIndex = 0;
        this.tabControl1.Size = new System.Drawing.Size(471, 231);
        this.tabControl1.TabIndex = 2;
        // 
        // tabPageRest
        // 
        this.tabPageRest.Controls.Add(this.groupBox1);
        this.tabPageRest.Controls.Add(this.panel4);
        this.tabPageRest.Controls.Add(this.panel3);
        this.tabPageRest.Location = new System.Drawing.Point(4, 22);
        this.tabPageRest.Name = "tabPageRest";
        this.tabPageRest.Padding = new System.Windows.Forms.Padding(3);
        this.tabPageRest.Size = new System.Drawing.Size(463, 205);
        this.tabPageRest.TabIndex = 0;
        this.tabPageRest.Text = "Остатки";
        this.tabPageRest.UseVisualStyleBackColor = true;
        // 
        // tabPageCards
        // 
        this.tabPageCards.Controls.Add(this.panel7);
        this.tabPageCards.Controls.Add(this.panel6);
        this.tabPageCards.Location = new System.Drawing.Point(4, 22);
        this.tabPageCards.Name = "tabPageCards";
        this.tabPageCards.Padding = new System.Windows.Forms.Padding(3);
        this.tabPageCards.Size = new System.Drawing.Size(463, 205);
        this.tabPageCards.TabIndex = 1;
        this.tabPageCards.Text = "Дисконтные карты";
        this.tabPageCards.UseVisualStyleBackColor = true;
        // 
        // panel7
        // 
        this.panel7.Controls.Add(this.textBoxDiscountCardType);
        this.panel7.Controls.Add(this.buttonDiscountCardType);
        this.panel7.Controls.Add(this.label6);
        this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
        this.panel7.Location = new System.Drawing.Point(3, 48);
        this.panel7.Name = "panel7";
        this.panel7.Size = new System.Drawing.Size(457, 45);
        this.panel7.TabIndex = 14;
        // 
        // textBoxDiscountCardType
        // 
        this.textBoxDiscountCardType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.textBoxDiscountCardType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
        this.textBoxDiscountCardType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllSystemSources;
        this.textBoxDiscountCardType.Location = new System.Drawing.Point(3, 20);
        this.textBoxDiscountCardType.Name = "textBoxDiscountCardType";
        this.textBoxDiscountCardType.Size = new System.Drawing.Size(421, 20);
        this.textBoxDiscountCardType.TabIndex = 5;
        // 
        // buttonDiscountCardType
        // 
        this.buttonDiscountCardType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.buttonDiscountCardType.Location = new System.Drawing.Point(427, 20);
        this.buttonDiscountCardType.Name = "buttonDiscountCardType";
        this.buttonDiscountCardType.Size = new System.Drawing.Size(24, 20);
        this.buttonDiscountCardType.TabIndex = 6;
        this.buttonDiscountCardType.Text = "...";
        this.buttonDiscountCardType.UseVisualStyleBackColor = true;
        this.buttonDiscountCardType.Click += new System.EventHandler(this.buttonDiscountCardType_Click);
        // 
        // label6
        // 
        this.label6.AutoSize = true;
        this.label6.Location = new System.Drawing.Point(3, 4);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(270, 13);
        this.label6.TabIndex = 4;
        this.label6.Text = "Путь к файлу соответствия типов дисконтных карт:";
        // 
        // panel6
        // 
        this.panel6.Controls.Add(this.textBoxDiscountCard);
        this.panel6.Controls.Add(this.buttonDiscountCard);
        this.panel6.Controls.Add(this.label5);
        this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
        this.panel6.Location = new System.Drawing.Point(3, 3);
        this.panel6.Name = "panel6";
        this.panel6.Size = new System.Drawing.Size(457, 45);
        this.panel6.TabIndex = 13;
        // 
        // textBoxDiscountCard
        // 
        this.textBoxDiscountCard.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.textBoxDiscountCard.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
        this.textBoxDiscountCard.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllSystemSources;
        this.textBoxDiscountCard.Location = new System.Drawing.Point(3, 20);
        this.textBoxDiscountCard.Name = "textBoxDiscountCard";
        this.textBoxDiscountCard.Size = new System.Drawing.Size(421, 20);
        this.textBoxDiscountCard.TabIndex = 5;
        // 
        // buttonDiscountCard
        // 
        this.buttonDiscountCard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.buttonDiscountCard.Location = new System.Drawing.Point(427, 20);
        this.buttonDiscountCard.Name = "buttonDiscountCard";
        this.buttonDiscountCard.Size = new System.Drawing.Size(24, 20);
        this.buttonDiscountCard.TabIndex = 6;
        this.buttonDiscountCard.Text = "...";
        this.buttonDiscountCard.UseVisualStyleBackColor = true;
        this.buttonDiscountCard.Click += new System.EventHandler(this.buttonDiscountCard_Click);
        // 
        // label5
        // 
        this.label5.AutoSize = true;
        this.label5.Location = new System.Drawing.Point(3, 4);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(166, 13);
        this.label5.TabIndex = 4;
        this.label5.Text = "Путь к файлу дисконтных карт:";
        // 
        // chkUseVATFromEfarma
        // 
        this.chkUseVATFromEfarma.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.chkUseVATFromEfarma.AutoSize = true;
        this.chkUseVATFromEfarma.Checked = true;
        this.chkUseVATFromEfarma.CheckState = System.Windows.Forms.CheckState.Checked;
        this.chkUseVATFromEfarma.Location = new System.Drawing.Point(227, 76);
        this.chkUseVATFromEfarma.Name = "chkUseVATFromEfarma";
        this.chkUseVATFromEfarma.Size = new System.Drawing.Size(159, 17);
        this.chkUseVATFromEfarma.TabIndex = 5;
        this.chkUseVATFromEfarma.Text = "Ставка НДС из еФарма-2";
        this.chkUseVATFromEfarma.UseVisualStyleBackColor = true;
        // 
        // ParamsForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(471, 354);
        this.Controls.Add(this.tabControl1);
        this.Controls.Add(this.panel2);
        this.Controls.Add(this.panel5);
        this.MinimumSize = new System.Drawing.Size(413, 340);
        this.Name = "ParamsForm";
        this.Text = "Конвертация данных";
        this.Load += new System.EventHandler(this.ParamsForm_Load);
        this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ParamsForm_FormClosed);
        this.Controls.SetChildIndex(this.panel5, 0);
        this.Controls.SetChildIndex(this.panel2, 0);
        this.Controls.SetChildIndex(this.panel1, 0);
        this.Controls.SetChildIndex(this.tabControl1, 0);
        this.panel1.ResumeLayout(false);
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        this.panel2.ResumeLayout(false);
        this.panel2.PerformLayout();
        this.panel3.ResumeLayout(false);
        this.panel3.PerformLayout();
        this.panel4.ResumeLayout(false);
        this.panel4.PerformLayout();
        this.panel5.ResumeLayout(false);
        this.panel5.PerformLayout();
        this.tabControl1.ResumeLayout(false);
        this.tabPageRest.ResumeLayout(false);
        this.tabPageCards.ResumeLayout(false);
        this.panel7.ResumeLayout(false);
        this.panel7.PerformLayout();
        this.panel6.ResumeLayout(false);
        this.panel6.PerformLayout();
        this.ResumeLayout(false);

    }

    #endregion

    private ePlus.CommonEx.Controls.StoreContractor ucStore;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox tbImportFile;
    private System.Windows.Forms.Button bSelectFile;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox tbImportLogFolder;
    private System.Windows.Forms.Button bSelectFolder;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.Panel panel4;
    private System.Windows.Forms.TextBox tbImportFile2;
    private System.Windows.Forms.Button bSelectFile2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.CheckBox chkENVD;
    private System.Windows.Forms.Panel panel5;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.ComboBox cbFormats;
      private System.Windows.Forms.TabControl tabControl1;
      private System.Windows.Forms.TabPage tabPageRest;
      private System.Windows.Forms.TabPage tabPageCards;
      private System.Windows.Forms.Panel panel7;
      private System.Windows.Forms.TextBox textBoxDiscountCardType;
      private System.Windows.Forms.Button buttonDiscountCardType;
      private System.Windows.Forms.Label label6;
      private System.Windows.Forms.Panel panel6;
      private System.Windows.Forms.TextBox textBoxDiscountCard;
      private System.Windows.Forms.Button buttonDiscountCard;
      private System.Windows.Forms.Label label5;
      private System.Windows.Forms.CheckBox chkUseVATFromEfarma;      
  }
}