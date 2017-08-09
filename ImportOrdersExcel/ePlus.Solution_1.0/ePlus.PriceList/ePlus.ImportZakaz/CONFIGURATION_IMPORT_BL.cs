using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using ePlus.MetaData.Core;
using System.Data.SqlClient;
using ePlus.MetaData.Server;
using ePlus.CommonEx.AccessPoint;
using System.Data;
using System.IO;

namespace ePlus.PriceList.ImportZakaz
{
    public class CONFIGURATION_IMPORT_BL
    {
        DataService_BL bl = new DataService_BL();
        SqlLoader<CONFIGURATION_IMPORT> loader = new SqlLoader<CONFIGURATION_IMPORT>();
        CONFIGURATION_IMPORT confImport = new CONFIGURATION_IMPORT();

        /// <summary>
        /// �������� �������� ������� ������� �� �����
        /// </summary>
        /// <param name="args">����</param>
        /// <returns>��������� ��������</returns>
        public CONFIGURATION_IMPORT LoadConfiguration(string[] args)
        {
            CONFIGURATION_IMPORT confImport = null;
            using (SqlConnection conn = new SqlConnection(bl.ConnectionString))
            {
                conn.Open();
                using (SqlCommandEx cmd = new SqlCommandEx("USP_CONFIGURATION_IMPORT_LOAD", conn))
                {
                    cmd.AddParameterIn("@ID_CONFIGURATION_IMPORT", SqlDbType.BigInt, 0);
                    if (args.Length > 0)
                        cmd.Parameters.Add(new SqlParameter("@IMPORT_KEY", SqlDbType.VarChar)).Value = args[0];
                    //cmd.AddParameterIn("@IMPORT_KEY", SqlDbType.VarChar, args.Length>0?args[0]:string.Empty);
                    cmd.AddParameterIn("@FOR_EDIT", SqlDbType.Bit, false);
                    SqlDataAdapter da = new SqlDataAdapter(cmd.SqlCommand);
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables.Count == 1)
                    {
                        
                        confImport = loader.GetList(ds.Tables[0])[0];                        
                    }
                }
            }
            return confImport;
        }

        /// <summary>
        /// �������� ����� ������� �� ���������
        /// </summary>
        /// <param name="accessPoint">��������</param>
        /// <returns>����� �������</returns>
        public AccessPointManager Load(string accessPoint)
        {
            AccessPointManager apm = null;
            try
            {
                apm = new AccessPointManager(accessPoint);//confImport.AP_IMPORT
            }
            catch
            {
                throw new Exception(string.Format("�� ������� ����� ��� ��������� ����� ������� [{0}] ��� �������� ������", confImport.AP_IMPORT == string.Empty ? "<�� ������>" : confImport.AP_IMPORT));
            }
            return apm;
        }
    }
}
