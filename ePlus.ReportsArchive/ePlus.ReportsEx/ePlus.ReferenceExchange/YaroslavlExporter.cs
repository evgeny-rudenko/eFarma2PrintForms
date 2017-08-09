using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.CommonEx.AccessPoint;
using ePlus.CommonEx.DataAccess;
using ePlus.MetaData.Core;
using ePlus.MetaData.Server;

namespace ePlus.ReferenceExchange
{
	public class YaroslavlExporter
	{
		public static void Export()
		{
			string fileName =  Utils.AppDir("YarImp.dbf");

      //U4_DbfImp
		  try
			{
				ACCESS_POINT_BL apBl = new ACCESS_POINT_BL();
				ACCESS_POINT ap = apBl.Load("YAROSLAVL_EXPORT", true);
				
				DataService_BL dataService = new DataService_BL();
				using (SqlConnection conn = new SqlConnection(dataService.ConnectionString))
				{
					conn.Open();
					using (SqlCommand cmd = new SqlCommand("USP_YAROSLAVL_EXPORT", conn))
					{
						cmd.CommandType = CommandType.StoredProcedure;
						SqlDataAdapter da = new SqlDataAdapter(cmd);
						DataTable table = new DataTable();
						da.Fill(table);

						if (table.Rows.Count == 0)
						{
							MessageBox.Show("Получено 0 записей для экспорта");
							return;
						}

						if (File.Exists(fileName))
						  File.Delete(fileName);
					  DbfTable dbfTable = new DbfTable(fileName, FileFormat.dBASEIV);
						dbfTable.Columns.Add(new DbfColumnNum("KOD_LEC", 7, 0));
						dbfTable.Columns.Add(new DbfColumnChar("SERV_KEY", 1));
						dbfTable.Columns.Add(new DbfColumnNum("CEN", 14, 2));
						dbfTable.Create(fileName);
						Trace.WriteLine("U4_DbfImp.dbf создан");
						if (File.Exists(fileName))
						{
							OleDbConnection connection = dbfTable.Open(fileName);
							foreach (DataRow row in table.Rows)
							{
								dbfTable.Insert(connection, row);
							}
						}
						else
						{
							throw new Exception("Ошибка при создании файла");
						}
					}
				}
        string newFileName = Utils.AppDir("U4_DbfImp.dbf");
        if (File.Exists(newFileName))
          File.Delete(newFileName);
			  File.Move(fileName, newFileName);
			  AccessPointManager manager = new AccessPointManager(ap);
			  manager.Send(newFileName, Path.GetFileName(newFileName));


//магия Lenor	
//			  if (File.Exists(fileName))
//				{
//				  string newFileName = Path.Combine(Path.GetDirectoryName(fileName), "U4_DbfImp.dbf");
//					if (File.Exists(newFileName))
//						File.Delete(newFileName);
//					File.Move(fileName, newFileName);
//					AccessPointManager manager = new AccessPointManager(ap);
//					manager.Send(newFileName, Path.GetFileName(newFileName));
//				}

				MessageBox.Show("Экспорт завершен успешно");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}
