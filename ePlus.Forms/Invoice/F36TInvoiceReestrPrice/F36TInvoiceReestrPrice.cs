//
// Decompiled with: Decompiler.NET, Version=2.0.0.230, Culture=neutral, PublicKeyToken=null, Version: 2.0.0.230
// Decompilation Started at: 19.06.2012 14:27:26
// Copyright 2003 - 2004, Jungle Creatures, Inc., All Rights Reserved. http://www.junglecreatures.com/
// Written by Jonathan Pierce, Email: support@junglecreatures.com
//

namespace F36TInvoiceReestrPrice

{
		
		#region Namespace Import Declarations
		
			using ePlus.CommonEx.Reporting;
			using ePlus.MetaData.Client;
			using ePlus.MetaData.Core;
			using Microsoft.Reporting.WinForms;
			using System.Collections.Generic;
			using System.Collections;
			using System.Data.Common;
			using System.Data;
			using System.Data.SqlClient;
			using System.IO;
			using System.Reflection;
			using System;
			using System.Text;
			using System.Text.RegularExpressions;
			using System.Xml;
			
		#endregion
		
	public class R36TInvoiceReestrPrice : AbstractDocumentReport, IExternalDocumentPrintForm
	
	{
		
		#region Fields
			private const string CACHE_FOLDER = "Cache";
			private string connectionString;
			private string folderPath;
		#endregion
		
		#region Constructors
		
			public R36TInvoiceReestrPrice ()
			
			{
			}
			
		#endregion
		
		#region Properties
		
			public virtual string GroupName
			
			{
				get
				{
					return string.Empty;
				}
			}
			
			
			public virtual string PluginCode
			
			{
				get
				{
					return "INVOICE";
				}
			}
			
			
			public virtual string ReportName
			
			{
				get
				{
					return "Протокол согласования розничных цен (отображение номера накл.)";
				}
			}
			
		#endregion
		
		#region Methods
		
			private void CreateStoredProc (string connectionString)
			
			{
				string[] stringArray1;
				SqlCommand sqlCommand1;
				Stream stream1 = this.GetType().Assembly.GetManifestResourceStream("F36TInvoiceReestrPrice.F36TInvoiceReestrPrice.sql");
				using (StreamReader streamReader1 = new StreamReader (stream1, Encoding.GetEncoding (1251)))
				{
					stringArray1 = Regex.Split (streamReader1.ReadToEnd (), "^GO.*$", RegexOptions.Multiline);
					sqlCommand1 = ((SqlCommand) null);
					foreach (string string1 in stringArray1)
					{
						if (string1 != string.Empty)
						{
							using (SqlConnection sqlConnection1 = new SqlConnection (connectionString))
							{
								sqlCommand1 = new SqlCommand (string1, sqlConnection1);
								sqlConnection1.Open ();
								int i2 = sqlCommand1.ExecuteNonQuery ();
							}
						}
					}
				}
			}
			
			public void Execute (string connectionString, string folderPath)
			
			{
				this.connectionString = connectionString;
				this.folderPath = folderPath;
				this.CreateStoredProc (this.connectionString);
				this.ExtractReport ();
			}
			
			private void ExtractReport ()
			
			{
				string string1 = Path.Combine (this.folderPath, "Cache");
				if (! Directory.Exists (string1))
				{
					DirectoryInfo directoryInfo1 = Directory.CreateDirectory (string1);
				}
				Stream stream1 = this.GetType ().Assembly.GetManifestResourceStream ("F36TInvoiceReestrPrice.F36TInvoiceReestrPrice.rdlc");
				using (StreamReader streamReader1 = new StreamReader (stream1))
				{
					using (StreamWriter streamWriter1 = new StreamWriter (Path.Combine (string1, "F36TInvoiceReestrPrice.rdlc")))
					{
						streamWriter1.Write (streamReader1.ReadToEnd ());
					}
				}
			}
			
			public override IReportForm GetReportForm (DataRowItem dataRowItem)			
			{
				string string4;
				XmlDocument xmlDocument1 = new XmlDocument ();
				XmlNode xmlNode1 = Utils.AddNode (((XmlNode) xmlDocument1), "XML");
				XmlNode xmlNode2 = Utils.AddNode (xmlNode1, "ID_INVOICE", dataRowItem.Id);
				ReportFormNew reportFormNew1 = new ReportFormNew ();
				reportFormNew1.ReportFormName = (string4 = this.ReportName);
				reportFormNew1.Text = string4;
                reportFormNew1.LoadData("REPEX_F36T_INVOICE_REESTR_PRICE", xmlDocument1.InnerXml);
				reportFormNew1.BindDataSource ("F36TInvoiceReestrPrice_DS_Table", 0);
				reportFormNew1.BindDataSource ("F36TInvoiceReestrPrice_DS_Table1", 1);
				reportFormNew1.BindDataSource ("F36TInvoiceReestrPrice_DS_Table2", 2);
                string string1 = Path.Combine(this.folderPath, CACHE_FOLDER);
				string string2 = Path.Combine (string1, "F36TInvoiceReestrPrice.rdlc");
				reportFormNew1.ReportPath = string2;
				decimal decimal1 = new decimal (0);
				decimal decimal2 = new decimal (0);
				foreach (DataRow dataRow1 in reportFormNew1.DataSource.Tables[1].Rows)
				{
					decimal1 += Utils.GetDecimal (dataRow1, "RETAIL_SUM_VAT");
                    decimal2 = decimal2++;
				}
				string string3 = RusCurrency.Str (((double) decimal1));
			    string invoicenum  = String.Format("{0} от {1} {2} от {3}",
                    Utils.GetString(dataRowItem.Row, "MNEMOCODE"),
                    Utils.GetString(dataRowItem.Row, "DOCUMENT_DATE"),
                    Utils.GetString(dataRowItem.Row, "INCOMING_NUMBER"),
                    Utils.GetString(dataRowItem.Row, "INCOMING_DATE")
                    );
                /*reportFormNew1.AddParameter("str_summory", string3);
                reportFormNew1.AddParameter("count", decimal1.ToString("#0.00"));
                reportFormNew1.AddParameter("VER_DLL", Assembly.GetExecutingAssembly ().ManifestModule.ScopeName);
                reportFormNew1.AddParameter("invoicenum", invoicenum);*/

                ReportParameter[] reportParameters = new ReportParameter[4] 
                {
                    new ReportParameter("str_summory", string3),
                    new ReportParameter("count", decimal1.ToString("#0.00")),
                    new ReportParameter("VER_DLL", Assembly.GetExecutingAssembly ().ManifestModule.ScopeName),
                    new ReportParameter("invoicenum", invoicenum),
                };
                reportFormNew1.ReportViewer.LocalReport.SetParameters(reportParameters);

				return ((IReportForm) reportFormNew1);
			}
			
		#endregion
	}
	
}

