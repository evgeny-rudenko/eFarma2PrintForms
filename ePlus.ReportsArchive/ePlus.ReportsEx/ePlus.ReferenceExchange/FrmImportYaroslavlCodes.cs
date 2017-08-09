using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.CommonEx.DataAccess;

namespace ePlus.ReferenceExchange
{
	public partial class FrmImportYaroslavlCodes : Form
	{
		public FrmImportYaroslavlCodes()
		{
			InitializeComponent();
		}

		private void btnSelectFileName_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.FileName = txtFileName.Text;
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				txtFileName.Text = openFileDialog.FileName;
			}
		}

		private void btnImport_Click(object sender, EventArgs e)
		{
			string fileName = txtFileName.Text;
			if (!File.Exists(fileName))
			{
				MessageBox.Show("Файл импорта не найден");
				return;
			}
			
			try
			{
				string connectionString = string.Format(@"Provider=VFPOLEDB.1;Data Source={0};Collating Sequence=RUSSIAN", Path.GetDirectoryName(fileName));
				using (OleDbConnection conn = new OleDbConnection(connectionString))
				{
					if (conn == null)
					{
						MessageBox.Show("Не удается открыть файл импорта");
						return;
					}
					conn.Open();
					string selectCommand = string.Format("SELECT * FROM {0}", Path.GetFileNameWithoutExtension(fileName));
					DataTable table = new DataTable("table");
					using (OleDbDataAdapter ad = new OleDbDataAdapter(selectCommand, conn))
					{
						ad.Fill(table);
					}

					List<XmlDocument> docList = ReferenceUtils.AddTable(table, "", "CODE", 500);
					YaroslavlImporter.Import(docList, null);
					conn.Close();
					MessageBox.Show("Импорт успешно завершен.");
				}
				
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}