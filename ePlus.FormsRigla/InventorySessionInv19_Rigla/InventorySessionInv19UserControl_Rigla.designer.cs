namespace FCKInventorySessionInv19_Rigla
{
    partial class InventorySessionInv19UserControl_Rigla
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.groupPrice = new System.Windows.Forms.GroupBox();
            this.rbSUP = new System.Windows.Forms.RadioButton();
            this.rbSAL = new System.Windows.Forms.RadioButton();
            this.groupData = new System.Windows.Forms.GroupBox();
            this.table = new System.Windows.Forms.TableLayoutPanel();
            this.rbStore = new System.Windows.Forms.RadioButton();
            this.rbContractor = new System.Windows.Forms.RadioButton();
            this.ucStore = new ePlus.MetaData.Client.UCPluginMultiSelect();
            this.groupPrice.SuspendLayout();
            this.groupData.SuspendLayout();
            this.table.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupPrice
            // 
            this.groupPrice.Controls.Add(this.rbSUP);
            this.groupPrice.Controls.Add(this.rbSAL);
            this.groupPrice.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupPrice.Location = new System.Drawing.Point(10, 5);
            this.groupPrice.Margin = new System.Windows.Forms.Padding(0);
            this.groupPrice.Name = "groupPrice";
            this.groupPrice.Padding = new System.Windows.Forms.Padding(5, 0, 5, 5);
            this.groupPrice.Size = new System.Drawing.Size(480, 50);
            this.groupPrice.TabIndex = 1;
            this.groupPrice.TabStop = false;
            this.groupPrice.Text = "Цены";
            // 
            // rbSUP
            // 
            this.rbSUP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbSUP.Location = new System.Drawing.Point(135, 13);
            this.rbSUP.Margin = new System.Windows.Forms.Padding(0);
            this.rbSUP.Name = "rbSUP";
            this.rbSUP.Padding = new System.Windows.Forms.Padding(5, 5, 5, 7);
            this.rbSUP.Size = new System.Drawing.Size(340, 32);
            this.rbSUP.TabIndex = 1;
            this.rbSUP.TabStop = true;
            this.rbSUP.Text = "Оптовые";
            this.rbSUP.UseVisualStyleBackColor = true;
            // 
            // rbSAL
            // 
            this.rbSAL.Checked = true;
            this.rbSAL.Dock = System.Windows.Forms.DockStyle.Left;
            this.rbSAL.Location = new System.Drawing.Point(5, 13);
            this.rbSAL.Margin = new System.Windows.Forms.Padding(0);
            this.rbSAL.Name = "rbSAL";
            this.rbSAL.Padding = new System.Windows.Forms.Padding(40, 5, 5, 7);
            this.rbSAL.Size = new System.Drawing.Size(130, 32);
            this.rbSAL.TabIndex = 0;
            this.rbSAL.TabStop = true;
            this.rbSAL.Text = "Розничные";
            this.rbSAL.UseVisualStyleBackColor = true;
            // 
            // groupData
            // 
            this.groupData.Controls.Add(this.table);
            this.groupData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupData.Location = new System.Drawing.Point(10, 55);
            this.groupData.Margin = new System.Windows.Forms.Padding(0);
            this.groupData.Name = "groupData";
            this.groupData.Padding = new System.Windows.Forms.Padding(5, 0, 5, 5);
            this.groupData.Size = new System.Drawing.Size(480, 135);
            this.groupData.TabIndex = 2;
            this.groupData.TabStop = false;
            this.groupData.Text = "Данные";
            // 
            // table
            // 
            this.table.ColumnCount = 2;
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.table.Controls.Add(this.ucStore, 0, 1);
            this.table.Controls.Add(this.rbStore, 1, 0);
            this.table.Controls.Add(this.rbContractor, 0, 0);
            this.table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.table.Location = new System.Drawing.Point(5, 13);
            this.table.Name = "table";
            this.table.RowCount = 2;
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.table.Size = new System.Drawing.Size(470, 117);
            this.table.TabIndex = 2;
            // 
            // rbStore
            // 
            this.rbStore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbStore.Location = new System.Drawing.Point(130, 0);
            this.rbStore.Margin = new System.Windows.Forms.Padding(0);
            this.rbStore.Name = "rbStore";
            this.rbStore.Padding = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.rbStore.Size = new System.Drawing.Size(340, 25);
            this.rbStore.TabIndex = 2;
            this.rbStore.TabStop = true;
            this.rbStore.Text = "По складам";
            this.rbStore.UseVisualStyleBackColor = true;
            // 
            // rbContractor
            // 
            this.rbContractor.Checked = true;
            this.rbContractor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbContractor.Location = new System.Drawing.Point(0, 0);
            this.rbContractor.Margin = new System.Windows.Forms.Padding(0);
            this.rbContractor.Name = "rbContractor";
            this.rbContractor.Padding = new System.Windows.Forms.Padding(40, 5, 5, 0);
            this.rbContractor.Size = new System.Drawing.Size(130, 25);
            this.rbContractor.TabIndex = 1;
            this.rbContractor.TabStop = true;
            this.rbContractor.Text = "По аптеке";
            this.rbContractor.UseVisualStyleBackColor = true;
            this.rbContractor.CheckedChanged += new System.EventHandler(this.RadioCheckedChanged);
            // 
            // ucStore
            // 
            this.ucStore.AllowSaveState = false;
            this.ucStore.Caption = "Склады";
            this.table.SetColumnSpan(this.ucStore, 2);
            this.ucStore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucStore.Enabled = false;
            this.ucStore.Location = new System.Drawing.Point(0, 25);
            this.ucStore.Margin = new System.Windows.Forms.Padding(0);
            this.ucStore.Mnemocode = "STORE";
            this.ucStore.Name = "ucStore";
            this.ucStore.Padding = new System.Windows.Forms.Padding(38, 0, 0, 5);
            this.ucStore.Pinnable = false;
            this.ucStore.Size = new System.Drawing.Size(470, 92);
            this.ucStore.TabIndex = 112;
            // 
            // InventorySessionInv19UserControl_Rigla
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.groupData);
            this.Controls.Add(this.groupPrice);
            this.Name = "InventorySessionInv19UserControl_Rigla";
            this.Padding = new System.Windows.Forms.Padding(10, 5, 10, 10);
            this.Size = new System.Drawing.Size(500, 200);
            this.groupPrice.ResumeLayout(false);
            this.groupData.ResumeLayout(false);
            this.table.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupPrice;
        private System.Windows.Forms.RadioButton rbSUP;
        private System.Windows.Forms.RadioButton rbSAL;
        private System.Windows.Forms.GroupBox groupData;
        private System.Windows.Forms.TableLayoutPanel table;
        private System.Windows.Forms.RadioButton rbStore;
        private System.Windows.Forms.RadioButton rbContractor;
        private ePlus.MetaData.Client.UCPluginMultiSelect ucStore;
    }
}