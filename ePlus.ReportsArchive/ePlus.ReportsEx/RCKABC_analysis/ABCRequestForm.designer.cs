using ePlus.CommonEx.Controls;
using ePlus.MetaData.Core.MetaGe;

namespace RCKABC_analysis
{
  partial class RequestCalculationForm
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
		System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Параметры");
		this.panel2 = new System.Windows.Forms.Panel();
		this.gbParams = new System.Windows.Forms.GroupBox();
		this.pParams = new System.Windows.Forms.Panel();
		this.tvParams = new System.Windows.Forms.TreeView();
		this.panel3 = new System.Windows.Forms.Panel();
		this.label1 = new System.Windows.Forms.Label();
		this.dtpDate = new ePlus.CommonEx.Controls.DateControl();
		this.beContractor = new ePlus.CommonEx.Controls.MetaPluginDictionarySelectControl();
		this.label2 = new System.Windows.Forms.Label();
		this.panel1.SuspendLayout();
		this.panel2.SuspendLayout();
		this.gbParams.SuspendLayout();
		this.panel3.SuspendLayout();
		this.SuspendLayout();
		// 
		// bOK
		// 
		this.bOK.Location = new System.Drawing.Point(579, 3);
		// 
		// bClose
		// 
		this.bClose.Location = new System.Drawing.Point(654, 3);
		// 
		// panel1
		// 
		this.panel1.Location = new System.Drawing.Point(0, 207);
		this.panel1.Size = new System.Drawing.Size(732, 29);
		// 
		// panel2
		// 
		this.panel2.Controls.Add(this.gbParams);
		this.panel2.Controls.Add(this.tvParams);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel2.Location = new System.Drawing.Point(0, 27);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(732, 177);
		this.panel2.TabIndex = 4;
		// 
		// gbParams
		// 
		this.gbParams.Controls.Add(this.pParams);
		this.gbParams.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gbParams.Location = new System.Drawing.Point(160, 0);
		this.gbParams.Name = "gbParams";
		this.gbParams.Size = new System.Drawing.Size(572, 177);
		this.gbParams.TabIndex = 1;
		this.gbParams.TabStop = false;
		// 
		// pParams
		// 
		this.pParams.AutoScroll = true;
		this.pParams.Dock = System.Windows.Forms.DockStyle.Fill;
		this.pParams.Location = new System.Drawing.Point(3, 16);
		this.pParams.Name = "pParams";
		this.pParams.Size = new System.Drawing.Size(566, 158);
		this.pParams.TabIndex = 0;
		// 
		// tvParams
		// 
		this.tvParams.Dock = System.Windows.Forms.DockStyle.Left;
		this.tvParams.FullRowSelect = true;
		this.tvParams.HideSelection = false;
		this.tvParams.Location = new System.Drawing.Point(0, 0);
		this.tvParams.Name = "tvParams";
		treeNode1.Name = "categoryNode";
		treeNode1.Text = "Параметры";
		this.tvParams.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
		this.tvParams.ShowLines = false;
		this.tvParams.ShowNodeToolTips = true;
		this.tvParams.ShowPlusMinus = false;
		this.tvParams.Size = new System.Drawing.Size(160, 177);
		this.tvParams.TabIndex = 0;
		this.tvParams.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvParams_AfterSelect);
		this.tvParams.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvParams_BeforeSelect);
		// 
		// panel3
		// 
		this.panel3.Controls.Add(this.label1);
		this.panel3.Controls.Add(this.dtpDate);
		this.panel3.Controls.Add(this.beContractor);
		this.panel3.Controls.Add(this.label2);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel3.Location = new System.Drawing.Point(0, 0);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(732, 27);
		this.panel3.TabIndex = 3;
		// 
		// label1
		// 
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(6, 7);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(68, 13);
		this.label1.TabIndex = 0;
		this.label1.Text = "Контрагент:";
		// 
		// dtpDate
		// 
		this.dtpDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.dtpDate.BackColor = System.Drawing.SystemColors.Window;
		this.dtpDate.Checked = true;
		this.dtpDate.DefaultDay = 10;
		this.dtpDate.DefaultMonth = 1;
		this.dtpDate.DefaultYear = 2008;
		this.dtpDate.ErrorColor = System.Drawing.Color.Red;
		this.dtpDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
		this.dtpDate.Location = new System.Drawing.Point(618, 4);
		this.dtpDate.Mask = "00/00/0000";
		this.dtpDate.MaxDate = new System.DateTime(((long)(0)));
		this.dtpDate.MinDate = new System.DateTime(((long)(0)));
		this.dtpDate.Name = "dtpDate";
		this.dtpDate.NormalColor = System.Drawing.SystemColors.Window;
		this.dtpDate.Size = new System.Drawing.Size(115, 20);
		this.dtpDate.TabIndex = 5;
		this.dtpDate.Text = "05072006";
		this.dtpDate.Value = new System.DateTime(2006, 7, 5, 0, 0, 0, 0);
		// 
		// beContractor
		// 
		this.beContractor.BackColor = System.Drawing.SystemColors.Window;
		this.beContractor.ELikeTextOption = ePlus.MetaData.Core.ELikeTextOption.MaskLeftRight;
		this.beContractor.Location = new System.Drawing.Point(80, 4);
		this.beContractor.Name = "beContractor";
		this.beContractor.PluginMnemocode = "CONTRACTOR";
		this.beContractor.Size = new System.Drawing.Size(340, 20);
		this.beContractor.TabIndex = 1;
		// 
		// label2
		// 
		this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(576, 7);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(36, 13);
		this.label2.TabIndex = 4;
		this.label2.Text = "Дата:";
		// 
		// RequestCalculationForm
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.ClientSize = new System.Drawing.Size(732, 236);
		this.Controls.Add(this.panel2);
		this.Controls.Add(this.panel3);
		this.MinimumSize = new System.Drawing.Size(100, 100);
		this.Name = "RequestCalculationForm";
		this.Text = "Расчет потребности";
		this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
		this.Shown += new System.EventHandler(this.RequestCalculationForm_Shown);
		this.Load += new System.EventHandler(this.Form1_Load);
		this.Controls.SetChildIndex(this.panel1, 0);
		this.Controls.SetChildIndex(this.panel3, 0);
		this.Controls.SetChildIndex(this.panel2, 0);
		this.panel1.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.gbParams.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		this.panel3.PerformLayout();
		this.ResumeLayout(false);

    }

    #endregion

	  private System.Windows.Forms.Panel panel2;
	  private System.Windows.Forms.GroupBox gbParams;
	  private System.Windows.Forms.Panel pParams;
	  private System.Windows.Forms.TreeView tvParams;
	  private System.Windows.Forms.Panel panel3;
	  private System.Windows.Forms.Label label1;
	  private DateControl dtpDate;
	  //private ePlus.Dictionary.Client.DictionaryControl.ucContractorSelect beContractor;	   
	  private System.Windows.Forms.Label label2;
	  public ePlus.CommonEx.Controls.MetaPluginDictionarySelectControl beContractor;

}
}