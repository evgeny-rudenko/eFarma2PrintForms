namespace RCGConvertRiglaData
{
  partial class ActionDialogForm
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
      this.rbCheck = new System.Windows.Forms.RadioButton();
      this.rbLoad = new System.Windows.Forms.RadioButton();
      this.panel1 = new System.Windows.Forms.Panel();
      this.bCancel = new System.Windows.Forms.Button();
      this.bOK = new System.Windows.Forms.Button();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // rbCheck
      // 
      this.rbCheck.AutoSize = true;
      this.rbCheck.Checked = true;
      this.rbCheck.Location = new System.Drawing.Point(12, 12);
      this.rbCheck.Name = "rbCheck";
      this.rbCheck.Size = new System.Drawing.Size(80, 17);
      this.rbCheck.TabIndex = 0;
      this.rbCheck.TabStop = true;
      this.rbCheck.Text = "Проверить";
      this.rbCheck.UseVisualStyleBackColor = true;
      this.rbCheck.CheckedChanged += new System.EventHandler(this.rbCheck_CheckedChanged);
      // 
      // rbLoad
      // 
      this.rbLoad.AutoSize = true;
      this.rbLoad.Location = new System.Drawing.Point(12, 35);
      this.rbLoad.Name = "rbLoad";
      this.rbLoad.Size = new System.Drawing.Size(143, 17);
      this.rbLoad.TabIndex = 1;
      this.rbLoad.TabStop = true;
      this.rbLoad.Text = "Проверить и загрузить";
      this.rbLoad.UseVisualStyleBackColor = true;
      this.rbLoad.CheckedChanged += new System.EventHandler(this.rbLoad_CheckedChanged);
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.bOK);
      this.panel1.Controls.Add(this.bCancel);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel1.Location = new System.Drawing.Point(0, 59);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(292, 29);
      this.panel1.TabIndex = 2;
      // 
      // bCancel
      // 
      this.bCancel.Location = new System.Drawing.Point(214, 3);
      this.bCancel.Name = "bCancel";
      this.bCancel.Size = new System.Drawing.Size(75, 23);
      this.bCancel.TabIndex = 0;
      this.bCancel.Text = "Отмена";
      this.bCancel.UseVisualStyleBackColor = true;
      this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
      // 
      // bOK
      // 
      this.bOK.Location = new System.Drawing.Point(133, 3);
      this.bOK.Name = "bOK";
      this.bOK.Size = new System.Drawing.Size(75, 23);
      this.bOK.TabIndex = 1;
      this.bOK.Text = "OK";
      this.bOK.UseVisualStyleBackColor = true;
      this.bOK.Click += new System.EventHandler(this.bOK_Click);
      // 
      // ActionDialogForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(292, 88);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.rbLoad);
      this.Controls.Add(this.rbCheck);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "ActionDialogForm";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Выберите желаемое действие";
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.RadioButton rbCheck;
    private System.Windows.Forms.RadioButton rbLoad;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button bOK;
    private System.Windows.Forms.Button bCancel;
  }
}