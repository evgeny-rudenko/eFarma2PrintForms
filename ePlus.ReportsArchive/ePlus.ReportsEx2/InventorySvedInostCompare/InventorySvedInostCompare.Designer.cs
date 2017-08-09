namespace InventoryVedInostCompare
{
    partial class InventorySvedInostCompare
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
        this.textFileName = new ePlus.CommonEx.Controls.TextBoxEx();
        this.btSelectFile = new System.Windows.Forms.Button();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.ucInventoryVed = new ePlus.MetaData.Client.UCPluginMultiSelect();
        this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
        this.panel1.SuspendLayout();
        this.groupBox1.SuspendLayout();
        this.SuspendLayout();
        // 
        // bOK
        // 
        this.bOK.Location = new System.Drawing.Point(328, 3);
        this.bOK.Text = "Отчет";
        // 
        // bClose
        // 
        this.bClose.Location = new System.Drawing.Point(403, 3);
        // 
        // panel1
        // 
        this.panel1.Location = new System.Drawing.Point(0, 186);
        this.panel1.Size = new System.Drawing.Size(481, 29);
        this.panel1.TabIndex = 3;
        // 
        // textFileName
        // 
        this.textFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.textFileName.Location = new System.Drawing.Point(6, 19);
        this.textFileName.Name = "textFileName";
        this.textFileName.ReadOnly = true;
        this.textFileName.Size = new System.Drawing.Size(417, 20);
        this.textFileName.TabIndex = 0;
        // 
        // btSelectFile
        // 
        this.btSelectFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.btSelectFile.Location = new System.Drawing.Point(425, 17);
        this.btSelectFile.Name = "btSelectFile";
        this.btSelectFile.Size = new System.Drawing.Size(26, 23);
        this.btSelectFile.TabIndex = 1;
        this.btSelectFile.Text = "...";
        this.btSelectFile.UseVisualStyleBackColor = true;
        this.btSelectFile.Click += new System.EventHandler(this.btSelectFile_Click);
        // 
        // groupBox1
        // 
        this.groupBox1.Controls.Add(this.textFileName);
        this.groupBox1.Controls.Add(this.btSelectFile);
        this.groupBox1.Location = new System.Drawing.Point(12, 12);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(458, 50);
        this.groupBox1.TabIndex = 0;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "Исходный файл старой СТУ";
        // 
        // ucInventoryVed
        // 
        this.ucInventoryVed.AllowSaveState = false;
        this.ucInventoryVed.Caption = "";
        this.ucInventoryVed.Location = new System.Drawing.Point(12, 69);
        this.ucInventoryVed.Mnemocode = "INVENTORY_VED";
        this.ucInventoryVed.Name = "ucInventoryVed";
        this.ucInventoryVed.Size = new System.Drawing.Size(458, 108);
        this.ucInventoryVed.TabIndex = 1;
        // 
        // InventorySvedInostCompare
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(481, 215);
        this.Controls.Add(this.groupBox1);
        this.Controls.Add(this.ucInventoryVed);
        this.Name = "InventorySvedInostCompare";
        this.Text = "Параметры формирования отчета";
        this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.InventoryVedInostCompare_FormClosed);
        this.Load += new System.EventHandler(this.InventoryVedInostCompare_Load);
        this.Controls.SetChildIndex(this.ucInventoryVed, 0);
        this.Controls.SetChildIndex(this.groupBox1, 0);
        this.Controls.SetChildIndex(this.panel1, 0);
        this.panel1.ResumeLayout(false);
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        this.ResumeLayout(false);

    }

    #endregion

        private ePlus.CommonEx.Controls.TextBoxEx textFileName;
        private System.Windows.Forms.Button btSelectFile;
        private System.Windows.Forms.GroupBox groupBox1;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucInventoryVed;
        private System.Windows.Forms.OpenFileDialog openFileDialog;

}
}