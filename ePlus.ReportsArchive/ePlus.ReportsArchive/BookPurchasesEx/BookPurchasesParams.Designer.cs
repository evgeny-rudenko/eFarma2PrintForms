namespace BookPurchasesEx
{
	partial class BookPurchasesParams
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
            this.components = new System.ComponentModel.Container();
            this.lbPeriod = new System.Windows.Forms.Label();
            this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
            this.ucContractors = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(194, 3);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(269, 3);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 191);
            this.panel1.Size = new System.Drawing.Size(347, 29);
            // 
            // lbPeriod
            // 
            this.lbPeriod.AutoSize = true;
            this.lbPeriod.Location = new System.Drawing.Point(12, 9);
            this.lbPeriod.Name = "lbPeriod";
            this.lbPeriod.Size = new System.Drawing.Size(45, 13);
            this.lbPeriod.TabIndex = 6;
            this.lbPeriod.Text = "Период";
            // 
            // ucPeriod
            // 
            this.ucPeriod.DateFrom = new System.DateTime(2006, 10, 30, 14, 2, 0, 906);
            this.ucPeriod.DateTo = new System.DateTime(2006, 10, 30, 14, 2, 0, 921);
            this.ucPeriod.Location = new System.Drawing.Point(63, 9);
            this.ucPeriod.Name = "ucPeriod";
            this.ucPeriod.Size = new System.Drawing.Size(253, 21);
            this.ucPeriod.TabIndex = 5;
            // 
            // ucContractors
            // 
            this.ucContractors.AllowSaveState = false;
            this.ucContractors.Caption = "Покупатель(и)";
            this.ucContractors.Location = new System.Drawing.Point(9, 43);
            this.ucContractors.Mnemocode = "CONTRACTOR";
            this.ucContractors.Name = "ucContractors";
            this.ucContractors.Size = new System.Drawing.Size(333, 123);
            this.ucContractors.TabIndex = 7;
            // 
            // BookPurchasesParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(347, 220);
            this.Controls.Add(this.lbPeriod);
            this.Controls.Add(this.ucPeriod);
            this.Controls.Add(this.ucContractors);
            this.Name = "BookPurchasesParams";
            this.Controls.SetChildIndex(this.ucContractors, 0);
            this.Controls.SetChildIndex(this.ucPeriod, 0);
            this.Controls.SetChildIndex(this.lbPeriod, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lbPeriod;
		private ePlus.MetaData.Client.UCPeriod ucPeriod;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucContractors;
	}
}
