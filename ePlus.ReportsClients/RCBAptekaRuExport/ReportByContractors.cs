using System;
using System.Collections.Generic;
using System.Text;

namespace RCBAptekaRuExport
{
  class ReportByContractors
  {
    private long id_contractor;
    private List<EXPORT_ROW> rows;

    public long ID_CONTRACTOR
    {
      get { return id_contractor; }
    }

    public List<EXPORT_ROW> Rows
    {
      get { return rows; }
    }

    public ReportByContractors(long _id_contractor, List<EXPORT_ROW> _rows)
    {
      this.id_contractor = _id_contractor;
      this.rows = new List<EXPORT_ROW>();
      this.rows = _rows.FindAll(delegate(EXPORT_ROW er)
      {
        return er.ID_CONTRACTOR == this.id_contractor;
      });
    }
  }
}
