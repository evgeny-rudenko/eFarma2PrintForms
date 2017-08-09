namespace RCYConvertData
{
  partial class ProgressStatusForm
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
      this.lStatus = new System.Windows.Forms.Label();
      this.worker = new System.ComponentModel.BackgroundWorker();
      this.SuspendLayout();
      // 
      // lStatus
      // 
      this.lStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lStatus.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lStatus.Location = new System.Drawing.Point(0, 0);
      this.lStatus.Name = "lStatus";
      this.lStatus.Size = new System.Drawing.Size(310, 90);
      this.lStatus.TabIndex = 0;
      this.lStatus.Text = "Состояние загрузки...";
      this.lStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // worker
      // 
      this.worker.WorkerReportsProgress = true;
      // 
      // ProgressStatusForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(310, 90);
      this.ControlBox = false;
      this.Controls.Add(this.lStatus);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "ProgressStatusForm";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "ProgressStatusForm";
      this.Shown += new System.EventHandler(this.ProgressStatusForm_Shown);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label lStatus;
    private System.ComponentModel.BackgroundWorker worker;
  }
}