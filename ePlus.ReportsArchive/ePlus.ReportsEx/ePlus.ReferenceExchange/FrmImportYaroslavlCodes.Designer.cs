namespace ePlus.ReferenceExchange
{
	partial class FrmImportYaroslavlCodes
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
			this.btnSelectFileName = new System.Windows.Forms.Button();
			this.txtFileName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnImport = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnSelectFileName
			// 
			this.btnSelectFileName.Location = new System.Drawing.Point(448, 21);
			this.btnSelectFileName.Name = "btnSelectFileName";
			this.btnSelectFileName.Size = new System.Drawing.Size(24, 20);
			this.btnSelectFileName.TabIndex = 0;
			this.btnSelectFileName.Text = "...";
			this.btnSelectFileName.UseVisualStyleBackColor = true;
			this.btnSelectFileName.Click += new System.EventHandler(this.btnSelectFileName_Click);
			// 
			// txtFileName
			// 
			this.txtFileName.Location = new System.Drawing.Point(6, 21);
			this.txtFileName.Name = "txtFileName";
			this.txtFileName.Size = new System.Drawing.Size(441, 20);
			this.txtFileName.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(85, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Файл импорта:";
			// 
			// btnImport
			// 
			this.btnImport.Location = new System.Drawing.Point(316, 47);
			this.btnImport.Name = "btnImport";
			this.btnImport.Size = new System.Drawing.Size(75, 23);
			this.btnImport.TabIndex = 3;
			this.btnImport.Text = "Загрузить";
			this.btnImport.UseVisualStyleBackColor = true;
			this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
			// 
			// btnClose
			// 
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new System.Drawing.Point(397, 47);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 3;
			this.btnClose.Text = "Закрыть";
			this.btnClose.UseVisualStyleBackColor = true;
			// 
			// FrmImportYaroslavlCodes
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(480, 77);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnImport);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtFileName);
			this.Controls.Add(this.btnSelectFileName);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmImportYaroslavlCodes";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Загрузка внешних кодов фармсправки Ярославль";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnSelectFileName;
		private System.Windows.Forms.TextBox txtFileName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnImport;
		private System.Windows.Forms.Button btnClose;
	}
}