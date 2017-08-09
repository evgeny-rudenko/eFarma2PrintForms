namespace InventoryVedCompare
{
  partial class InventoryVedCompareParams
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
      this.mpsList1 = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.mpsList2 = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.panel1.SuspendLayout();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.SuspendLayout();
      // 
      // bOK
      // 
      this.bOK.Location = new System.Drawing.Point(406, 3);
      this.bOK.Text = "Сравнить";
      // 
      // bClose
      // 
      this.bClose.Location = new System.Drawing.Point(481, 3);
      // 
      // panel1
      // 
      this.panel1.Location = new System.Drawing.Point(0, 248);
      this.panel1.Size = new System.Drawing.Size(559, 29);
      // 
      // mpsList1
      // 
      this.mpsList1.AllowSaveState = false;
      this.mpsList1.Caption = "Счет 1";
      this.mpsList1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.mpsList1.Location = new System.Drawing.Point(0, 0);
      this.mpsList1.Mnemocode = "INVENTORY_VED";
      this.mpsList1.Name = "mpsList1";
      this.mpsList1.Size = new System.Drawing.Size(267, 248);
      this.mpsList1.TabIndex = 3;
      this.mpsList1.BeforePluginShow += new System.ComponentModel.CancelEventHandler(this.mpsList1_BeforePluginShow);
      // 
      // mpsList2
      // 
      this.mpsList2.AllowSaveState = false;
      this.mpsList2.Caption = "Счет 2";
      this.mpsList2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.mpsList2.Location = new System.Drawing.Point(0, 0);
      this.mpsList2.Mnemocode = "INVENTORY_VED";
      this.mpsList2.Name = "mpsList2";
      this.mpsList2.Size = new System.Drawing.Size(288, 248);
      this.mpsList2.TabIndex = 4;
      this.mpsList2.BeforePluginShow += new System.ComponentModel.CancelEventHandler(this.mpsList1_BeforePluginShow);
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.mpsList1);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.mpsList2);
      this.splitContainer1.Size = new System.Drawing.Size(559, 248);
      this.splitContainer1.SplitterDistance = 267;
      this.splitContainer1.TabIndex = 5;
      // 
      // InventoryVedCompareParams
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(559, 277);
      this.Controls.Add(this.splitContainer1);
      this.Name = "InventoryVedCompareParams";
      this.Text = "InventoryVedCompareParams";
      this.Load += new System.EventHandler(this.InventoryVedCompareParams_Load);
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.InventoryVedCompareParams_FormClosed);
      this.Controls.SetChildIndex(this.panel1, 0);
      this.Controls.SetChildIndex(this.splitContainer1, 0);
      this.panel1.ResumeLayout(false);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private ePlus.MetaData.Client.UCPluginMultiSelect mpsList1;
    private ePlus.MetaData.Client.UCPluginMultiSelect mpsList2;
    private System.Windows.Forms.SplitContainer splitContainer1;
  }
}