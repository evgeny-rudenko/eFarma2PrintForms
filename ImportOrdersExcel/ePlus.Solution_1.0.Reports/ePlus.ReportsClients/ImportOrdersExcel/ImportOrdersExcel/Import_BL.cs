namespace RCRImportOrdersExcel
{
    using ePlus.MetaData.Server;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class Import_BL
    {
        private DataService_BL bl = new DataService_BL();

        public string LoadConfigurationImport(long id_configuration_import)
        {
            using (SqlConnection connection = new SqlConnection(this.bl.ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(string.Format("select top 1 ap_import from CONFIGURATION_IMPORT where ID_CONFIGURATION_IMPORT = {0}", id_configuration_import), connection))
                {
                    command.CommandType = CommandType.Text;
                    string str = (string) command.ExecuteScalar();
                }
            }
            return string.Empty;
        }
    }
}

