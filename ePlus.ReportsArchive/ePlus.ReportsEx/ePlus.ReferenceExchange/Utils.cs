using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml;
using ePlus.MetaData.Core;

namespace ePlus.ReferenceExchange
{
	public class ReferenceUtils
	{
		public static List<XmlDocument> AddTable(DataTable table, string tableNodeName, string rowNodeName, int recordCount)
		{
			List<XmlDocument> list = new List<XmlDocument>();
			XmlNode topLevelNode = null;
			int recordCounter = 0;
			foreach (DataRow row in table.Rows)
			{
				if (recordCounter == recordCount)
					recordCounter = 0;
				if (recordCounter == 0)
				{
					XmlDocument doc = new XmlDocument();
					list.Add(doc);
					topLevelNode = Utils.AddNode(doc, "XML", null);
				}
				XmlNode rowNode = Utils.AddNode(topLevelNode, rowNodeName, null);
				foreach (DataColumn col in table.Columns)
				{
					string val = null;
					object obj = row[col.Ordinal];
					if (obj == null || obj == DBNull.Value)
					{
						val = null;
					}
					else if (obj is bool)
					{
						bool bval = (bool)obj;
						val = (bval) ? "1" : "0";
					}
					else if (obj is DateTime)
					{
						DateTime dat = (DateTime)obj;
						val = Utils.SqlDate(dat);
					}
					else if (obj is decimal)
					{
						val = Utils.SqlNumber((decimal)obj);
					}
					else if (obj is double)
					{
						val = Utils.SqlNumber((double)obj);
					}
					else if (obj is float)
					{
						val = Utils.SqlNumber((float)obj);
					}
					else
					{
						val = obj.ToString();
					}

					if (val != null)
					{
						Utils.AddNode(rowNode, col.ColumnName.ToUpper(), val);
					}
				}
				++recordCounter;
			}
			return list;
		}
	}
}
