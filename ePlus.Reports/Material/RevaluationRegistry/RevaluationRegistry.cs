using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Core;
using ePlus.MetaData.Client;
using ePlus.CommonEx.Reporting;
using System.Text.RegularExpressions;
using Microsoft.Reporting.WinForms;
using System.IO;

namespace RevaluationRegistry
{
	public partial class RevaluationRegistryActs : ExternalReportForm, IExternalReportFormMethods
	{
    private string settingsFilePath;

		public RevaluationRegistryActs()
		{
      System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
      settingsFilePath = Path.Combine(Utils.TempDir(), asm.GetName().Name.ToString() + ".xml");
			InitializeComponent();
			ClearValues();
		}

		public void Print(string[] reportFiles)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");

			ucPeriod.AddValues(root);
			storesPluginMultiSelect.AddItems(root, "ID_STORE");
			Utils.AddNode(root, MARK, plusRadioButton.Checked ? "1" : AllRadioButton.Checked ? "2" : "0");
      Utils.AddNode(root, DETAILED, detailCheckBox.Checked);

			ReportFormNew rep = new ReportFormNew();
      if (detailCheckBox.Checked)
      {
        rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "RRMarkUpDown.rdlc");
        rep.LoadData("REPEX_REVALUATION_REGISTRY", doc.InnerXml);
        rep.BindDataSource("RevaluationRegistry_DS_Table0", 0);
      }
      else
      {
        rep.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "RRMarkUpDown_Whole.rdlc");
        rep.LoadData("REPEX_REVALUATION_REGISTRY", doc.InnerXml);
        rep.BindDataSource("RevaluationRegistry_DS_Table1", 0);
      }

			rep.AddParameter("date_fr", ucPeriod.DateFrText);
			rep.AddParameter("date_to", ucPeriod.DateToText);
			rep.AddParameter("stores", storesPluginMultiSelect.TextValues());
      rep.AddParameter("mark", plusRadioButton.Checked ? "1" : AllRadioButton.Checked ? "2" : "0");
      rep.AddParameter("detailed", detailCheckBox.Checked ? "1" : "0");
      
			rep.ExecuteReport(this);
		}

		private void ClearValues()
		{
			ucPeriod.DateTo = DateTime.Now;
			ucPeriod.DateFrom = DateTime.Now.AddDays(-13);

			storesPluginMultiSelect.Items.Clear();

			detailCheckBox.Checked = false;
			AllRadioButton.Checked = true;
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			ClearValues();
		}

    private UCPluginMultiSelect storesPluginMultiSelect;
    private Label label1;

		public string ReportName
		{
			get { return "Реестр переоценки"; }
		}

		public override string GroupName
		{
			get { return new ReportGroupDescription(ReportGroup.MaterialReports).Description; }
		}

		private const string DETAILED = "DETAILED";
    private UCPeriod ucPeriod;
    private IContainer components;
    private Panel panel2;
    private RadioButton minusRadioButton;
    private RadioButton plusRadioButton;
    private CheckBox detailCheckBox;
    private RadioButton AllRadioButton;
    private const string MARK = "MARK";

		private void LoadSettings()
		{
			if (!File.Exists(settingsFilePath))
				return;

			XmlDocument doc = new XmlDocument();
			doc.Load(settingsFilePath);
			XmlNode root = doc.SelectSingleNode("//XML");

			if (root == null)
				return;

      ucPeriod.DateFrom = Utils.GetDate(root, "DATE_FROM");
      ucPeriod.DateTo = Utils.GetDate(root, "DATE_TO");
			detailCheckBox.Checked = Utils.GetBool(root, DETAILED);
			int mark = Utils.GetInt(root, MARK);
      if (mark == 0)
      { minusRadioButton.Checked = true; }
      else if (mark == 1)
      { plusRadioButton.Checked = true; }
      else
      { AllRadioButton.Checked = true; }
		}

		private void SaveSettings()
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root;

			if (File.Exists(settingsFilePath))
			{
				doc.Load(settingsFilePath);
				root = doc.SelectSingleNode("//XML");
				root.RemoveAll();
			}
			else
			{
				root = Utils.AddNode(doc, "XML");
			}

			Utils.AddNode(root, DETAILED, detailCheckBox.Checked);
      Utils.AddNode(root, MARK, plusRadioButton.Checked ? "1" : AllRadioButton.Checked ? "2" : "0");
      Utils.AddNode(root, "DATE_FROM", ucPeriod.DateFrom);
      Utils.AddNode(root, "DATE_TO", ucPeriod.DateTo);

			doc.Save(settingsFilePath);
		}

    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.ucPeriod = new ePlus.MetaData.Client.UCPeriod();
      this.storesPluginMultiSelect = new ePlus.MetaData.Client.UCPluginMultiSelect();
      this.label1 = new System.Windows.Forms.Label();
      this.panel2 = new System.Windows.Forms.Panel();
      this.minusRadioButton = new System.Windows.Forms.RadioButton();
      this.plusRadioButton = new System.Windows.Forms.RadioButton();
      this.detailCheckBox = new System.Windows.Forms.CheckBox();
      this.AllRadioButton = new System.Windows.Forms.RadioButton();
      this.panel1.SuspendLayout();
      this.panel2.SuspendLayout();
      this.SuspendLayout();
      // 
      // bOK
      // 
      this.bOK.Location = new System.Drawing.Point(157, 3);
      // 
      // bClose
      // 
      this.bClose.Location = new System.Drawing.Point(232, 3);
      // 
      // panel1
      // 
      this.panel1.Location = new System.Drawing.Point(0, 281);
      this.panel1.Size = new System.Drawing.Size(310, 29);
      // 
      // ucPeriod
      // 
      this.ucPeriod.DateFrom = new System.DateTime(2010, 7, 28, 10, 5, 59, 234);
      this.ucPeriod.DateTo = new System.DateTime(2010, 7, 28, 10, 5, 59, 234);
      this.ucPeriod.Location = new System.Drawing.Point(74, 12);
      this.ucPeriod.Name = "ucPeriod";
      this.ucPeriod.Size = new System.Drawing.Size(222, 21);
      this.ucPeriod.TabIndex = 3;
      // 
      // storesPluginMultiSelect
      // 
      this.storesPluginMultiSelect.AllowSaveState = true;
      this.storesPluginMultiSelect.Caption = "Склады";
      this.storesPluginMultiSelect.Location = new System.Drawing.Point(12, 50);
      this.storesPluginMultiSelect.Mnemocode = "STORE";
      this.storesPluginMultiSelect.Name = "storesPluginMultiSelect";
      this.storesPluginMultiSelect.Size = new System.Drawing.Size(286, 133);
      this.storesPluginMultiSelect.TabIndex = 4;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(13, 15);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(51, 13);
      this.label1.TabIndex = 5;
      this.label1.Text = "Период: ";
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.AllRadioButton);
      this.panel2.Controls.Add(this.minusRadioButton);
      this.panel2.Controls.Add(this.plusRadioButton);
      this.panel2.Location = new System.Drawing.Point(12, 194);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(284, 39);
      this.panel2.TabIndex = 6;
      // 
      // minusRadioButton
      // 
      this.minusRadioButton.AutoSize = true;
      this.minusRadioButton.Location = new System.Drawing.Point(88, 12);
      this.minusRadioButton.Name = "minusRadioButton";
      this.minusRadioButton.Size = new System.Drawing.Size(63, 17);
      this.minusRadioButton.TabIndex = 1;
      this.minusRadioButton.TabStop = true;
      this.minusRadioButton.Text = "Уценка";
      this.minusRadioButton.UseVisualStyleBackColor = true;
      // 
      // plusRadioButton
      // 
      this.plusRadioButton.AutoSize = true;
      this.plusRadioButton.Location = new System.Drawing.Point(13, 12);
      this.plusRadioButton.Name = "plusRadioButton";
      this.plusRadioButton.Size = new System.Drawing.Size(69, 17);
      this.plusRadioButton.TabIndex = 0;
      this.plusRadioButton.TabStop = true;
      this.plusRadioButton.Text = "Наценка";
      this.plusRadioButton.UseVisualStyleBackColor = true;
      // 
      // detailCheckBox
      // 
      this.detailCheckBox.AutoSize = true;
      this.detailCheckBox.Location = new System.Drawing.Point(16, 248);
      this.detailCheckBox.Name = "detailCheckBox";
      this.detailCheckBox.Size = new System.Drawing.Size(184, 17);
      this.detailCheckBox.TabIndex = 7;
      this.detailCheckBox.Text = "Детализация по номенклатуре";
      this.detailCheckBox.UseVisualStyleBackColor = true;
      // 
      // AllRadioButton
      // 
      this.AllRadioButton.AutoSize = true;
      this.AllRadioButton.Location = new System.Drawing.Point(157, 12);
      this.AllRadioButton.Name = "AllRadioButton";
      this.AllRadioButton.Size = new System.Drawing.Size(44, 17);
      this.AllRadioButton.TabIndex = 2;
      this.AllRadioButton.TabStop = true;
      this.AllRadioButton.Text = "Все";
      this.AllRadioButton.UseVisualStyleBackColor = true;
      // 
      // RevaluationRegistryActs
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.ClientSize = new System.Drawing.Size(310, 310);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.detailCheckBox);
      this.Controls.Add(this.panel2);
      this.Controls.Add(this.ucPeriod);
      this.Controls.Add(this.storesPluginMultiSelect);
      this.Name = "RevaluationRegistryActs";
      this.Load += new System.EventHandler(this.RevaluationRegistryActs_Load);
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RevaluationRegistryActs_FormClosed);
      this.Controls.SetChildIndex(this.storesPluginMultiSelect, 0);
      this.Controls.SetChildIndex(this.ucPeriod, 0);
      this.Controls.SetChildIndex(this.panel2, 0);
      this.Controls.SetChildIndex(this.detailCheckBox, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.panel1, 0);
      this.panel1.ResumeLayout(false);
      this.panel2.ResumeLayout(false);
      this.panel2.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    private void RevaluationRegistryActs_Load(object sender, EventArgs e)
    {
      LoadSettings();
    }

    private void RevaluationRegistryActs_FormClosed(object sender, FormClosedEventArgs e)
    {
      SaveSettings();
    }
	}
}