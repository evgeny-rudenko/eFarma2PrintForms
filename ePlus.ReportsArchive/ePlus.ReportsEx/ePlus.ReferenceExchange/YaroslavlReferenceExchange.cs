using System;
using System.Collections.Generic;
using System.Text;
using ePlus.CommonEx.DataAccess;

namespace ePlus.ReferenceExchange
{
	public class YaroslavlReferenceExchange
	{
		public void Export(string exportFileName)
		{
			try
			{
				DbfTable table = new DbfTable(exportFileName, FileFormat.dBASEIV);
				table.Columns.Add(new DbfColumnNum("KOD_LEC", 7, 0));
				table.Columns.Add(new DbfColumnChar("NAME_LEC", 20));
				table.Columns.Add(new DbfColumnNum("KOD_NLEC", 5, 0));
				table.Columns.Add(new DbfColumnChar("NAME_NLEC", 20));
				table.Columns.Add(new DbfColumnNum("KOD_FLEC", 5, 0));
				table.Columns.Add(new DbfColumnChar("NAME_FLEC", 20));
				table.Columns.Add(new DbfColumnNum("KOD_TAR", 3, 0));
				table.Columns.Add(new DbfColumnChar("NAME_TAR", 20));
				table.Columns.Add(new DbfColumnNum("KOD_MER", 4, 0));
				table.Columns.Add(new DbfColumnChar("NAME_MER", 20));
				table.Columns.Add(new DbfColumnChar("SERV_KEY", 1));
				table.Columns.Add(new DbfColumnNum("CEN", 14, 0));
				table.Create();
			}
			catch (Exception ex)
			{
				
			}
			
		}
	}
}
