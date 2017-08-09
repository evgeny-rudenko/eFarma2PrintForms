namespace Inv19_Rigla
{
  partial class Inv19Form
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
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.rbSUP = new System.Windows.Forms.RadioButton();
      this.rbSAL = new System.Windows.Forms.RadioButton();
      this.btnOK = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.rbSUP);
      this.groupBox1.Controls.Add(this.rbSAL);
      this.groupBox1.Location = new System.Drawing.Point(12, 12);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(169, 64);
      this.groupBox1.TabIndex = 1;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Отображение отчета";
      // 
      // rbSUP
      // 
      this.rbSUP.AutoSize = true;
      this.rbSUP.Location = new System.Drawing.Point(28, 40);
      this.rbSUP.Name = "rbSUP";
      this.rbSUP.Size = new System.Drawing.Size(108, 17);
      this.rbSUP.TabIndex = 1;
      this.rbSUP.TabStop = true;
      this.rbSUP.Text = "в оптовых ценах";
      this.rbSUP.UseVisualStyleBackColor = true;
      // 
      // rbSAL
      // 
      this.rbSAL.AutoSize = true;
      this.rbSAL.Checked = true;
      this.rbSAL.Location = new System.Drawing.Point(28, 17);
      this.rbSAL.Name = "rbSAL";
      this.rbSAL.Size = new System.Drawing.Size(120, 17);
      this.rbSAL.TabIndex = 0;
      this.rbSAL.TabStop = true;
      this.rbSAL.Text = "в розничных ценах";
      this.rbSAL.UseVisualStyleBackColor = true;
      // 
      // btnOK
      // 
      this.btnOK.Location = new System.Drawing.Point(12, 82);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(75, 23);
      this.btnOK.TabIndex = 2;
      this.btnOK.Text = "Отчет";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(106, 82);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 3;
      this.btnCancel.Text = "Отмена";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // Inv19Form
      // 
      this.AcceptButton = this.btnOK;
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
      this.ClientSize = new System.Drawing.Size(195, 112);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOK);
      this.Controls.Add(this.groupBox1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "Inv19Form";
      this.ShowIcon = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Параметры";
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.RadioButton rbSUP;
    private System.Windows.Forms.RadioButton rbSAL;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Button btnCancel;
  }
}