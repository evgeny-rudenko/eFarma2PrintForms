using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using ePlus.MetaData.Server;
using System.Data;
using System.Data.SqlClient;
using ePlus.MetaData.Core;

namespace RCChCheckDataIntegrity
{
    public class CHECK_DATABASE 
    {
        #region Fields

        private string code = Guid.NewGuid().ToString();
        private string name = "";
        private string description = "";
        private bool is_run = false;
        private bool is_correct = false;
        private bool is_correctable = true;

        #endregion

        #region Methods

        public void ToXml(XmlNode root)
        {
            Utils.AddNode(root, "CODE", this.CODE);
            Utils.AddNode(root, "IS_RUN", this.IS_RUN);
            Utils.AddNode(root, "IS_CORRECT", this.IS_CORRECT);
        }

        #endregion

        #region Properties

        [TableField]
        public string CODE
        {
            get { return code; }
            set { code = value; }
        }
        [TableField]
        public string NAME
        {
            get { return name; }
            set { name = value; }
        }
        [TableField]
        public string DESCRIPTION
        {
            get { return description; }
            set { description = value; }
        }
        [TableField]
        public bool IS_RUN
        {
            get { return is_run; }
            set { is_run = value; }
        }
        [TableField]
        public bool IS_CORRECT
        {
            get { return is_correct; }
            set { is_correct = value; }
        }
        [TableField]
        public bool IS_CORRECTABLE
        {
            get { return is_correctable; }
            set { is_correctable = value; }
        }

        #endregion
    }

    public class CHECK_DATABASE_BL 
    {
        DataService_BL dataService = null;
        SqlLoader<CHECK_DATABASE> loader = null;

        public CHECK_DATABASE_BL()
        {
            dataService = new DataService_BL();
            loader = new SqlLoader<CHECK_DATABASE>("MV_CHECK_DATABASE", dataService);
        }

        public List<CHECK_DATABASE> List()
        {
            using (SqlConnection conn = new SqlConnection(dataService.ConnectionString))
            {
                conn.Open();
                using (SqlCommandEx cmd = new SqlCommandEx("UTL_CHECK_DATABASE_LIST", conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd.SqlCommand);
                    cmd.CommandType = CommandType.StoredProcedure;
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables.Count < 1) return null;
                    return loader.GetList(ds.Tables[0]);
                }
            }
        }
        public DataTable Check(List<CHECK_DATABASE> list)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = Utils.AddNode(doc, "XML", null);
            DataTable DTRes = new DataTable();
            int RowCounter = 0;
            DTRes.Columns.Add("IdRow", typeof(int));
            DTRes.Columns.Add("FieldDescript", typeof(string));
            DTRes.Columns.Add("Field0", typeof(string));
            DTRes.Columns.Add("Field1", typeof(string));
            DTRes.Columns.Add("Field2", typeof(string));
            DTRes.Columns.Add("Field3", typeof(string));
            DTRes.Columns.Add("Field4", typeof(string));
            DTRes.Columns.Add("Field5", typeof(string));
            DTRes.Columns.Add("Field6", typeof(string));
            DTRes.Columns.Add("Field7", typeof(string));
            DTRes.Columns.Add("Field8", typeof(string));
            DTRes.Columns.Add("Field9", typeof(string));
            DTRes.Columns.Add("Field10", typeof(string));
            foreach (CHECK_DATABASE item in list)
            {
                item.ToXml(Utils.AddNode(root, "ROW", null));
            }
            using (SqlConnection conn = new SqlConnection(dataService.ConnectionString))
            {
                conn.Open();
                using (SqlCommandEx cmd = new SqlCommandEx("UTL_CHECK_DATABASE", conn))
                {
                    cmd.AddParameterIn("@XML_DATA", SqlDbType.NText, doc.InnerXml);
                    SqlDataAdapter da = new SqlDataAdapter(cmd.SqlCommand);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.SqlCommand.CommandTimeout = 5184000;
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    foreach (DataTable tab in ds.Tables)
                    {
                        if (tab.Rows.Count == 0) continue;

                        foreach (DataRow row in tab.Rows)
                        {
                            int counter = 1;
                            DataRow DrNew = DTRes.NewRow();
                            DrNew["IdRow"] = RowCounter;
                            if ((tab.Rows.Count == 1) && (tab.Columns.Count == 1))
                                DrNew["FieldDescript"] = row[0];
                            else
                            {

                                foreach (DataColumn col in tab.Columns)
                                {
                                    counter++;
                                    DrNew[counter] = row[col];
                                }
                            }
                            DTRes.Rows.Add(DrNew);
                            RowCounter++;
                        }
                    }
                    return DTRes;
                }
            }
        }
    }

}
