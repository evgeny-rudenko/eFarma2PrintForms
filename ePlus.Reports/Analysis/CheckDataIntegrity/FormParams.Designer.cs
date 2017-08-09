namespace RCChCheckDataIntegrity
{
    partial class FormParams
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormParams));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.buttonSelectAll = new System.Windows.Forms.Button();
            this.gridDocs = new ePlus.MetaData.Core.MetaGe.TableEditorControl();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxCheckDatabase = new System.Windows.Forms.GroupBox();
            this.buttonDeselectAll = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.groupBoxCheckDatabase.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(781, 3);
            this.bOK.Text = "Проверить";
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(856, 3);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 481);
            this.panel1.Size = new System.Drawing.Size(934, 29);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(934, 25);
            this.toolStrip1.TabIndex = 156;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(79, 22);
            this.toolStripButton1.Text = "Очистить";
            // 
            // buttonSelectAll
            // 
            this.buttonSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSelectAll.Location = new System.Drawing.Point(12, 413);
            this.buttonSelectAll.Name = "buttonSelectAll";
            this.buttonSelectAll.Size = new System.Drawing.Size(92, 28);
            this.buttonSelectAll.TabIndex = 0;
            this.buttonSelectAll.Text = "Выделить все";
            this.buttonSelectAll.Click += new System.EventHandler(this.buttonSelectAll_Click);
            // 
            // gridDocs
            // 
            this.gridDocs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridDocs.DataSource = null;
            this.gridDocs.Location = new System.Drawing.Point(12, 19);
            this.gridDocs.Mnemocode = "CHECK_DATABASE";
            this.gridDocs.Name = "gridDocs";
            this.gridDocs.ObjectList = null;
            this.gridDocs.SearchingEnabled = true;
            this.gridDocs.Size = new System.Drawing.Size(891, 388);
            this.gridDocs.TabIndex = 4;
            this.gridDocs.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnGridDocsKeyDon);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(142, 0);
            this.label1.MaximumSize = new System.Drawing.Size(13, 13);
            this.label1.MinimumSize = new System.Drawing.Size(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "?";
            this.label1.Visible = false;
            // 
            // groupBoxCheckDatabase
            // 
            this.groupBoxCheckDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxCheckDatabase.Controls.Add(this.buttonDeselectAll);
            this.groupBoxCheckDatabase.Controls.Add(this.label1);
            this.groupBoxCheckDatabase.Controls.Add(this.gridDocs);
            this.groupBoxCheckDatabase.Controls.Add(this.buttonSelectAll);
            this.groupBoxCheckDatabase.Location = new System.Drawing.Point(12, 28);
            this.groupBoxCheckDatabase.Name = "groupBoxCheckDatabase";
            this.groupBoxCheckDatabase.Size = new System.Drawing.Size(909, 447);
            this.groupBoxCheckDatabase.TabIndex = 157;
            this.groupBoxCheckDatabase.TabStop = false;
            this.groupBoxCheckDatabase.Text = "Проверка состояния БД";
            // 
            // buttonDeselectAll
            // 
            this.buttonDeselectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDeselectAll.Location = new System.Drawing.Point(110, 413);
            this.buttonDeselectAll.Name = "buttonDeselectAll";
            this.buttonDeselectAll.Size = new System.Drawing.Size(115, 28);
            this.buttonDeselectAll.TabIndex = 6;
            this.buttonDeselectAll.Text = "Снять выделение";
            this.buttonDeselectAll.Click += new System.EventHandler(this.buttonDeselectAll_Click);
            // 
            // FormParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 510);
            this.Controls.Add(this.groupBoxCheckDatabase);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FormParams";
            this.Load += new System.EventHandler(this.FormParams_Load);
            this.Controls.SetChildIndex(this.toolStrip1, 0);
            this.Controls.SetChildIndex(this.groupBoxCheckDatabase, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBoxCheckDatabase.ResumeLayout(false);
            this.groupBoxCheckDatabase.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.Button buttonSelectAll;
        private ePlus.MetaData.Core.MetaGe.TableEditorControl gridDocs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxCheckDatabase;
        private System.Windows.Forms.Button buttonDeselectAll;
    }
}