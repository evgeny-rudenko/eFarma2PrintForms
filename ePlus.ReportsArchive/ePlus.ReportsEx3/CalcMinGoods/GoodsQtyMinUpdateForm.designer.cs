namespace CalcMinGoods
{
  partial class GoodsQtyMinUpdateForm
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
		this.panel1 = new System.Windows.Forms.Panel();
		this.bNewRequest = new System.Windows.Forms.Button();
		this.bSave = new System.Windows.Forms.Button();
		this.bMove = new System.Windows.Forms.Button();
		this.bClose = new System.Windows.Forms.Button();
		this.bOk = new System.Windows.Forms.Button();
		this.gridGoods = new ePlus.MetaData.Core.MetaGe.TableEditorControl();
		this.reportButton = new System.Windows.Forms.Button();
		this.panel1.SuspendLayout();
		this.SuspendLayout();
		// 
		// panel1
		// 
		this.panel1.Controls.Add(this.reportButton);
		this.panel1.Controls.Add(this.bNewRequest);
		this.panel1.Controls.Add(this.bSave);
		this.panel1.Controls.Add(this.bMove);
		this.panel1.Controls.Add(this.bClose);
		this.panel1.Controls.Add(this.bOk);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Location = new System.Drawing.Point(0, 420);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(598, 28);
		this.panel1.TabIndex = 0;
		// 
		// bNewRequest
		// 
		this.bNewRequest.Location = new System.Drawing.Point(12, 2);
		this.bNewRequest.Name = "bNewRequest";
		this.bNewRequest.Size = new System.Drawing.Size(95, 23);
		this.bNewRequest.TabIndex = 4;
		this.bNewRequest.Text = "Новая заявка";
		this.bNewRequest.UseVisualStyleBackColor = true;
		this.bNewRequest.Click += new System.EventHandler(this.bNewRequest_Click);
		// 
		// bSave
		// 
		this.bSave.Location = new System.Drawing.Point(358, 2);
		this.bSave.Name = "bSave";
		this.bSave.Size = new System.Drawing.Size(75, 23);
		this.bSave.TabIndex = 3;
		this.bSave.Text = "Сохранить";
		this.bSave.UseVisualStyleBackColor = true;
		this.bSave.Click += new System.EventHandler(this.bSave_Click);
		// 
		// bMove
		// 
		this.bMove.Location = new System.Drawing.Point(277, 2);
		this.bMove.Name = "bMove";
		this.bMove.Size = new System.Drawing.Size(75, 23);
		this.bMove.TabIndex = 2;
		this.bMove.Text = "Перенести";
		this.bMove.UseVisualStyleBackColor = true;
		this.bMove.Click += new System.EventHandler(this.bMove_Click);
		// 
		// bClose
		// 
		this.bClose.Location = new System.Drawing.Point(520, 2);
		this.bClose.Name = "bClose";
		this.bClose.Size = new System.Drawing.Size(75, 23);
		this.bClose.TabIndex = 1;
		this.bClose.Text = "Закрыть";
		this.bClose.UseVisualStyleBackColor = true;
		this.bClose.Click += new System.EventHandler(this.bClose_Click);
		// 
		// bOk
		// 
		this.bOk.Location = new System.Drawing.Point(439, 2);
		this.bOk.Name = "bOk";
		this.bOk.Size = new System.Drawing.Size(75, 23);
		this.bOk.TabIndex = 0;
		this.bOk.Text = "OK";
		this.bOk.UseVisualStyleBackColor = true;
		this.bOk.Click += new System.EventHandler(this.bOk_Click);
		// 
		// gridGoods
		// 
		this.gridGoods.DataSource = null;
		this.gridGoods.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridGoods.Location = new System.Drawing.Point(0, 0);
		this.gridGoods.Mnemocode = "GOODS_QTY_MIN";
		this.gridGoods.Name = "gridGoods";
		this.gridGoods.ObjectList = null;
		this.gridGoods.Size = new System.Drawing.Size(598, 420);
		this.gridGoods.TabIndex = 4;
		// 
		// reportButton
		// 
		this.reportButton.Location = new System.Drawing.Point(113, 2);
		this.reportButton.Name = "reportButton";
		this.reportButton.Size = new System.Drawing.Size(75, 23);
		this.reportButton.TabIndex = 5;
		this.reportButton.Text = "Отчет";
		this.reportButton.UseVisualStyleBackColor = true;
		this.reportButton.Click += new System.EventHandler(this.reportButton_Click);
		// 
		// GoodsQtyMinUpdateForm
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.ClientSize = new System.Drawing.Size(598, 448);
		this.Controls.Add(this.gridGoods);
		this.Controls.Add(this.panel1);
		this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		this.MaximizeBox = false;
		this.MinimizeBox = false;
		this.Name = "GoodsQtyMinUpdateForm";
		this.ShowInTaskbar = false;
		this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Изменение минимального количества";
		this.panel1.ResumeLayout(false);
		this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button bMove;
    private System.Windows.Forms.Button bClose;
    private System.Windows.Forms.Button bOk;
    private ePlus.MetaData.Core.MetaGe.TableEditorControl gridGoods;
    private System.Windows.Forms.Button bSave;
	  private System.Windows.Forms.Button bNewRequest;
	  private System.Windows.Forms.Button reportButton;
  }
}