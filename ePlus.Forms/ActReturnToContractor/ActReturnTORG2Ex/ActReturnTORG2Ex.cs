using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using Microsoft.Reporting.WinForms;

namespace ActReturnTORG2Ex
{
	public class ActReturnTORG2Ex : AbstractDocumentPrintForm
	{

        public long GetContractorNameFromDoc(string Contractor, long IdDoc, string ConnectionString)
        {
            long res = 0;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {                
                string Query = string.Format(@"SELECT ISNULL(ID_CONTRACTOR_TO, 0) as Res
                                               FROM ACT_RETURN_TO_CONTRACTOR ARTC
                                               INNER JOIN CONTRACTOR C ON C.ID_CONTRACTOR = ID_CONTRACTOR_TO
                                               WHERE C.NAME LIKE '{0}'
                                                    AND ID_ACT_RETURN_TO_CONTRACTOR = {1}", Contractor, IdDoc);
                SqlCommand comm = new SqlCommand(Query, conn);
                comm.CommandType = CommandType.Text;
                try
                {                    
                    conn.Open();
                    object Tmp = comm.ExecuteScalar();
                    if (Tmp == null)
                        res = 0;
                    else
                        res = (long)Tmp;
                }
                finally
                {
                    conn.Close();
                }
            }
            return res;
        }

        protected override IReportForm Print(DataRowItem dataRowItem, string[] reportFiles)
		{
            long Res = GetContractorNameFromDoc("%СТУ%", dataRowItem.Id, connectionString);
            if (Res != 0)
            {
                ChooseContractor frm = new ChooseContractor();
                frm.Init(Res);
                frm.ShowDialog();
                Res = frm.GetResult();
            }
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");
			Utils.AddNode(root, "ID_DOCUMENT", dataRowItem.Id);
            Utils.AddNode(root, "ID_CONTRACTOR", Res);
            ReportFormNew rep = new ReportFormNew();
            rep.ReportPath = reportFiles[0];
            rep.LoadData("REPEX_ACT_RETURN_TORG2_EX2", doc.InnerXml);
            rep.BindDataSource("ActReturnTORG2Ex2_DS_TableHeader", 0);
            rep.BindDataSource("ActReturnTORG2Ex2_DS_TableIncomingInfo", 1);
            rep.BindDataSource("ActReturnTORG2Ex2_DS_TableDetail", 2);
            rep.BindDataSource("ActReturnTORG2Ex2_DS_TableAbout", 3);
            string Tmp = System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;
            ReportParameter[] parameters = new ReportParameter[1] {
				new ReportParameter("VERSION", Tmp)
			};

            rep.ReportViewer.LocalReport.SetParameters(parameters);
            return rep;//(IReportForm)reportForm;
		}

        public override string PluginCode
		{
			get { return "ActReturnToContractor"; }
		}

        public override string GroupName
		{
			get { return string.Empty; }
		}

        public override string ReportName
		{
			get { return "ТОРГ-2"; }
		}
	}
}
