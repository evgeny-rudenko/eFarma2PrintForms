using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Xml;
using ePlus.MetaData.Server;

namespace ePlus.ReferenceExchange
{
	public class YaroslavlImporter
	{
		public static void Import(List<XmlDocument> packets, BackgroundWorker worker)
		{
			DataService_BL dataService = new DataService_BL();
			using (SqlConnection conn = new SqlConnection(dataService.ConnectionString))
			{
				conn.Open();
				using (SqlCommandEx cmd = new SqlCommandEx("USP_IMPORT_YAROSLAVL_CODES", conn))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.AddParameterIn("@XMLDATA", SqlDbType.NText, null);
					foreach (XmlDocument doc in packets)
					{
						cmd.Parameters[0].Value = doc.InnerXml;
						cmd.ExecuteNonQuery();
					}
				}
			}
		}
	}
}
