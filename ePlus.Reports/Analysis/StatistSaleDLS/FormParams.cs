using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using ePlus.MetaData.ExternReport;
using Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
namespace RCChStatistSaleDLS
{
	public class FormParams : ExternalReportForm, IExternalReportFormMethods
	{
		private IContainer components = null;
		private Label labelPeriod;
		public UCPeriod period;
		private ToolStrip toolStrip1;
		private ToolStripButton toolStripButton1;
		private string SettingsFilePath
		{
			get
			{
				Assembly executingAssembly = Assembly.GetExecutingAssembly();
				return Path.Combine(Utils.TempDir(), executingAssembly.GetName().Name.ToString() + ".xml");
			}
		}
		public override string GroupName
		{
			get
			{
				return new ReportGroupDescription(ReportGroup.AnalisysReports).Description;
			}
		}
		public string ReportName
		{
			get
			{
				return "Отчет по продажам ДЛС";
			}
		}
		public FormParams()
		{
			this.InitializeComponent();
		}
		private void ClearValues()
		{
			this.period.DateTo = DateTime.Now;
			this.period.DateFrom = this.period.DateTo.AddDays(-13.0);
		}
		private void FormParams_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.SaveSettings();
		}
		private void FormParams_Load(object sender, EventArgs e)
		{
			this.ClearValues();
			this.LoadSettings();
		}
		private void LoadSettings()
		{
			if (File.Exists(this.SettingsFilePath))
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(this.SettingsFilePath);
				if (xmlDocument.SelectSingleNode("//XML") == null)
				{
				}
			}
		}
		public void Print(string[] reportFiles)
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlNode root = Utils.AddNode(xmlDocument, "XML");
			this.period.AddValues(root);
			Utils.AddNode(root, "DATE_FR", this.period.DateFrom);
			Utils.AddNode(root, "DATE_TO", this.period.DateTo);
			ReportFormNew reportFormNew = new ReportFormNew();
			reportFormNew.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "StatistSaleDLS.rdlc");
			reportFormNew.LoadData("DBO.REP_STATIST_SALE_DLS", xmlDocument.InnerXml);
			reportFormNew.BindDataSource("STATIST_SALE_DLS_DS_Table0", 0);
			reportFormNew.AddParameter("Pm_DateFrom", this.period.DateFrText);
			reportFormNew.AddParameter("Pm_DateTo", this.period.DateToText);
			reportFormNew.AddParameter("VER_DLL", Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
			reportFormNew.ExecuteReport(this);
		}
		private void SaveSettings()
		{
			XmlDocument xmlDocument = new XmlDocument();
			if (!File.Exists(this.SettingsFilePath))
			{
				Utils.AddNode(xmlDocument, "XML");
			}
			else
			{
				xmlDocument.Load(this.SettingsFilePath);
				xmlDocument.SelectSingleNode("//XML").RemoveAll();
			}
			xmlDocument.Save(this.SettingsFilePath);
		}
		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			this.ClearValues();
		}
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}
		private void InitializeComponent()
		{
			this.components = new Container();
			this.labelPeriod = new Label();
			this.period = new UCPeriod();
			this.toolStrip1 = new ToolStrip();
			this.toolStripButton1 = new ToolStripButton();
			this.panel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			base.SuspendLayout();
			this.bOK.Location = new Point(170, 3);
			this.bClose.Location = new Point(245, 3);
			this.panel1.Location = new Point(0, 79);
			this.panel1.Size = new Size(323, 29);
			this.labelPeriod.Location = new Point(9, 42);
			this.labelPeriod.Name = "labelPeriod";
			this.labelPeriod.Size = new Size(51, 21);
			this.labelPeriod.TabIndex = 151;
			this.labelPeriod.Text = "Период : ";
			this.labelPeriod.TextAlign = ContentAlignment.MiddleLeft;
			this.period.DateFrom = new DateTime(2006, 11, 15, 16, 50, 34, 515);
			this.period.DateTo = new DateTime(2006, 11, 15, 16, 50, 34, 515);
			this.period.Location = new Point(76, 42);
			this.period.Name = "period";
			this.period.Size = new Size(233, 21);
			this.period.TabIndex = 124;
			this.toolStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.toolStripButton1
			});
			this.toolStrip1.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.toolStrip1.Location = new Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = ToolStripRenderMode.System;
			this.toolStrip1.Size = new Size(323, 25);
			this.toolStrip1.TabIndex = 170;
			this.toolStrip1.Text = "toolStrip1";
			this.toolStripButton1.Image = Resources.image;
			this.toolStripButton1.ImageTransparentColor = Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new Size(79, 22);
			this.toolStripButton1.Text = "Очистить";
			this.toolStripButton1.Click += new EventHandler(this.toolStripButton1_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(323, 108);
			base.Controls.Add(this.toolStrip1);
			base.Controls.Add(this.period);
			base.Controls.Add(this.labelPeriod);
			base.Name = "FormParams";
			base.Load += new EventHandler(this.FormParams_Load);
			base.FormClosed += new FormClosedEventHandler(this.FormParams_FormClosed);
			base.Controls.SetChildIndex(this.labelPeriod, 0);
			base.Controls.SetChildIndex(this.period, 0);
			base.Controls.SetChildIndex(this.panel1, 0);
			base.Controls.SetChildIndex(this.toolStrip1, 0);
			this.panel1.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
