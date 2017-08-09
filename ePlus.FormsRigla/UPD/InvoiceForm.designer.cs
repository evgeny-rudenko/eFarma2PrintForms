namespace UPD
{
	partial class InvoiceForm
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
			this.numberTextBox = new System.Windows.Forms.TextBox();
			this.numberLabel = new System.Windows.Forms.Label();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.chiefName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.accountantName = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.docDetails = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.docStatus = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// numberTextBox
			// 
			this.numberTextBox.Location = new System.Drawing.Point(15, 31);
			this.numberTextBox.Name = "numberTextBox";
			this.numberTextBox.Size = new System.Drawing.Size(156, 20);
			this.numberTextBox.TabIndex = 0;
			// 
			// numberLabel
			// 
			this.numberLabel.AutoSize = true;
			this.numberLabel.Location = new System.Drawing.Point(12, 9);
			this.numberLabel.Name = "numberLabel";
			this.numberLabel.Size = new System.Drawing.Size(112, 13);
			this.numberLabel.TabIndex = 3;
			this.numberLabel.Text = "Введите номер УПД";
			// 
			// okButton
			// 
			this.okButton.Location = new System.Drawing.Point(15, 259);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 1;
			this.okButton.Text = "Отчет";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.OkButtonClick);
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(96, 259);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 4;
			this.cancelButton.Text = "Отмена";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.CancelButtonClick);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 54);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(146, 13);
			this.label1.TabIndex = 6;
			this.label1.Text = "Руководитель организации";
			// 
			// chiefName
			// 
			this.chiefName.Location = new System.Drawing.Point(15, 76);
			this.chiefName.Name = "chiefName";
			this.chiefName.Size = new System.Drawing.Size(156, 20);
			this.chiefName.TabIndex = 5;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 102);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(104, 13);
			this.label2.TabIndex = 8;
			this.label2.Text = "Главный бухгалтер";
			// 
			// accountantName
			// 
			this.accountantName.Location = new System.Drawing.Point(15, 124);
			this.accountantName.Name = "accountantName";
			this.accountantName.Size = new System.Drawing.Size(156, 20);
			this.accountantName.TabIndex = 7;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 151);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(113, 13);
			this.label3.TabIndex = 10;
			this.label3.Text = "Реквизиты договора";
			// 
			// docDetails
			// 
			this.docDetails.Location = new System.Drawing.Point(15, 173);
			this.docDetails.Name = "docDetails";
			this.docDetails.Size = new System.Drawing.Size(156, 20);
			this.docDetails.TabIndex = 9;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(14, 201);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(98, 13);
			this.label4.TabIndex = 12;
			this.label4.Text = "Статус документа";
			// 
			// docStatus
			// 
			this.docStatus.Location = new System.Drawing.Point(17, 223);
			this.docStatus.Name = "docStatus";
			this.docStatus.Size = new System.Drawing.Size(156, 20);
			this.docStatus.TabIndex = 11;
			// 
			// InvoiceForm
			// 
			this.AcceptButton = this.okButton;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(185, 293);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.docStatus);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.docDetails);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.accountantName);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.chiefName);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.numberLabel);
			this.Controls.Add(this.numberTextBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "InvoiceForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Параметры";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox numberTextBox;
		private System.Windows.Forms.Label numberLabel;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox chiefName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox accountantName;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox docDetails;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox docStatus;
	}
}