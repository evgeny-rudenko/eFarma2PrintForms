namespace ePlus.PriceList.ImportZakaz
{
    using ePlus.CommonEx.AccessPoint;
    using ePlus.MetaData.Server;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class CONFIGURATION_IMPORT_BL
    {
        private DataService_BL bl = new DataService_BL();
        private CONFIGURATION_IMPORT confImport = new CONFIGURATION_IMPORT();
        private SqlLoader<CONFIGURATION_IMPORT> loader = new SqlLoader<CONFIGURATION_IMPORT>();

        public AccessPointManager Load(string accessPoint)
        {
            AccessPointManager manager = null;
            try
            {
                manager = new AccessPointManager(accessPoint);
            }
            catch
            {
                throw new Exception(string.Format("Не удалось найти или загрузить точку доступа [{0}] для экспорта данных", (this.confImport.AP_IMPORT == string.Empty) ? "<Не задано>" : this.confImport.AP_IMPORT));
            }
            return manager;
        }

        public CONFIGURATION_IMPORT LoadConfiguration(string[] args)
        {
            CONFIGURATION_IMPORT configuration_import = null;
            using (SqlConnection connection = new SqlConnection(this.bl.ConnectionString))
            {
                connection.Open();
                using (SqlCommandEx ex = new SqlCommandEx("USP_CONFIGURATION_IMPORT_LOAD", connection))
                {
                    ex.AddParameterIn("@ID_CONFIGURATION_IMPORT", SqlDbType.BigInt, 0);
                    if (args.Length > 0)
                    {
                        ex.Parameters.Add(new SqlParameter("@IMPORT_KEY", SqlDbType.VarChar)).Value = args[0];
                    }
                    ex.AddParameterIn("@FOR_EDIT", SqlDbType.Bit, false);
                    SqlDataAdapter adapter = new SqlDataAdapter(ex.SqlCommand);
                    ex.CommandType = CommandType.StoredProcedure;
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);
                    if (dataSet.Tables.Count == 1)
                    {
                        configuration_import = this.loader.GetList(dataSet.Tables[0])[0];
                    }
                }
            }
            return configuration_import;
        }
    }
}

