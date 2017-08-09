using System;
using System.Collections.Generic;
using System.Text;
using ePlus.MetaData.Server;
using System.Xml;
using System.Data;

namespace RCBAptekaRuExport
{
  public class APTEKA_RU_EXPORT_BL
  {
    private REPORTS_BL rbl;
    public APTEKA_RU_EXPORT_BL()
    {
      rbl = new REPORTS_BL();     
    }

    public bool Export(string xml_text, out List<EXPORT_ROW> rows, out List<EXPORT_ROW> id_contractors)
    {
      rows = new List<EXPORT_ROW>();
      id_contractors = new List<EXPORT_ROW>();
      DataSet ds = rbl.GetData("REPEX_APTEKA_RU_EXPORT", xml_text);
      if (ds.Tables.Count != 2 ||
           ds.Tables[0].Rows.Count == 0 ||
           ds.Tables[0].Columns.Count == 0 ||
           ds.Tables[0].Rows[0].IsNull(0)
         ) return false;
      SqlLoader<EXPORT_ROW> rowsLoader = new SqlLoader<EXPORT_ROW>();
      rows.AddRange(rowsLoader.GetList(ds.Tables[0]));
      SqlLoader<EXPORT_ROW> contsLoader = new SqlLoader<EXPORT_ROW>();
      id_contractors.AddRange(contsLoader.GetList(ds.Tables[1]));
      return rows.Count > 0;
    }
  }
}
