using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.MetaData.ExternReport;
using ePlus.MetaData.Core;
using ePlus.MetaData.Client;
using ePlus.CommonEx.Reporting;
using System.Text.RegularExpressions;
using Microsoft.Reporting.WinForms;
using System.IO;
using System.Data.SqlClient;


//RLO = ЗАБАЛАНС

namespace R23MUzoBalancePack
{
	public partial class UzoBalancePackParams : ExternalReportForm, IExternalReportFormMethods
	{
		public string[] keys;
		public int N_keys;
		private bool _formLoaded = false;

		public UzoBalancePackParams()
		{
			InitializeComponent();
		}

		void CreateStoredProc(string connectionString, string ScriptName)
		{
			Stream s = this.GetType().Assembly.GetManifestResourceStream("R23MUzoBalancePack." + ScriptName);

			using (StreamReader sr = new StreamReader(s, Encoding.GetEncoding(1251)))
			{
				string[] batch = Regex.Split(sr.ReadToEnd(), "^GO.*$", RegexOptions.Multiline);

				SqlCommand comm = null;
				foreach (string statement in batch)
				{
					if (statement == string.Empty)
						continue;

					using (SqlConnection con = new SqlConnection(connectionString))
					{
						comm = new SqlCommand(statement, con);
						con.Open();
						comm.ExecuteNonQuery();
					}
				}
			}
		}

		public void Print(string[] reportFiles)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");

			Utils.AddNode(root, "ID_PACK", keys[cbSelectedUzoPack.SelectedIndex]);

			//1_ActCompliteWork (Balance)
			if (chklstbForms.GetItemChecked(0))
			{
				ReportFormNew rep1 = new ReportFormNew();
				rep1.ReportFormName = "Акт приемки выполненных работ";
				rep1.ReportPath = reportFiles[0];
				rep1.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "1_ActCompliteWork (Balance).rdlc");

				rep1.LoadData("REP_UZO_BALANCE_PACK", doc.InnerXml);

				rep1.BindDataSource("UzoBalancePack_DS_Table1", 0);
				decimal retailSummory = 0;
				foreach (DataRow Row in rep1.DataSource.Tables[0].Rows)
				{
					retailSummory += Utils.GetDecimal(Row, "SUM_SAL");
				}
				retailSummory = Utils.Round(retailSummory, 2);
				rep1.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
				rep1.AddParameter("RETAIL_SUMMORY", RusCurrency.Str((double)retailSummory));
				rep1.ExecuteReport(this);
			}

			//2_Factura (2011) EasterEgg
			if (chklstbForms.Items.Count > 4)
				if (chklstbForms.GetItemChecked(4))
				{
					ReportFormNew rep2 = new ReportFormNew();
					rep2.ReportFormName = "Счет-фактура 2011";
					rep2.ReportPath = reportFiles[0];
					rep2.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "2_Factura (2011) (Balance).rdlc");

					rep2.LoadData("REP_UZO_BALANCE_PACK", doc.InnerXml);

					rep2.BindDataSource("UzoBalancePack_DS_Table2", 1);
					rep2.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
					rep2.ExecuteReport(this);
				}

			//2_Factura (2012) (Balance)
			if (chklstbForms.GetItemChecked(1))
			{
				ReportFormNew rep2 = new ReportFormNew();
				rep2.ReportFormName = "Счет-фактура";
				rep2.ReportPath = reportFiles[0];
				rep2.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "2_Factura (2012) (Balance).rdlc");

				rep2.LoadData("REP_UZO_BALANCE_PACK", doc.InnerXml);

				rep2.BindDataSource("UzoBalancePack_DS_Table2", 1);
				rep2.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
				rep2.ExecuteReport(this);
			}

			//3_Registry_Short (Balance)
			if (chklstbForms.GetItemChecked(2))
			{
				ReportFormNew rep3 = new ReportFormNew();
				rep3.ReportFormName = "Краткая форма реестра";
				rep3.ReportPath = reportFiles[0];
				rep3.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "3_Registry_Short (Balance).rdlc");

				rep3.LoadData("REP_UZO_BALANCE_PACK", doc.InnerXml);

				rep3.BindDataSource("UzoBalancePack_DS_Table3", 2);
				decimal retailSummory = 0;
				foreach (DataRow Row in rep3.DataSource.Tables[0].Rows)
				{
					retailSummory += Utils.GetDecimal(Row, "SUM_SAL");
				}
				retailSummory = Utils.Round(retailSummory, 2);
				rep3.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
				rep3.AddParameter("RETAIL_SUMMORY", RusCurrency.Str((double)retailSummory));
				rep3.ExecuteReport(this);
			}

			//4_Registry_Full (Balance)
			if (chklstbForms.GetItemChecked(3))
			{
				ReportFormNew rep4 = new ReportFormNew();
				rep4.ReportFormName = "Полная форма реестра";
				rep4.ReportPath = reportFiles[0];
				rep4.ReportPath = Path.Combine(Path.GetDirectoryName(reportFiles[0]), "4_Registry_Full (Balance).rdlc");

				rep4.LoadData("REP_UZO_BALANCE_PACK", doc.InnerXml);

				rep4.BindDataSource("UzoBalancePack_DS_Table4", 3);
				decimal retailSummory = 0;
				foreach (DataRow Row in rep4.DataSource.Tables[0].Rows)
				{
					retailSummory += Utils.GetDecimal(Row, "SUM_SAL");
				}
				retailSummory = Utils.Round(retailSummory, 2);
				rep4.AddParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
				rep4.AddParameter("RETAIL_SUMMORY", RusCurrency.Str((double)retailSummory));
				rep4.ExecuteReport(this);
			}
		}

		public string ReportName
		{
			get { return "Пакет документов для УЗО - баланс (СГАУ)"; }
		}

		public override string GroupName
		{
			get { return new ReportGroupDescription(ReportGroup.AccountingReports).Description; }
		}

		//---------------------------Новый ID
		public Int64 GetNextID(string id_contractor_global)
		{
			string query = "select ISNULL(MAX(ID_PACK)+1, 1) from SGAU_REP_UZO_PACK UP inner join contractor perf on UP.id_contractor_perform = perf.id_contractor where perf.id_contractor_global = '" + id_contractor_global + "'";

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand command = new SqlCommand(query, connection);
				connection.Open();

				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					return (Int64)reader[0];
				}
				reader.Close();
			}
			return 0;
		}

		//---------------------------Код БД
		public string GetBDCode(string id_contractor_global)
		{
			string query = "select GLOBAL_CODE, ID_CONTRACTOR_GLOBAL from REPLICATION_CONFIG where ";
			if (id_contractor_global == "00000000-0000-0000-0000-000000000000")
				query = query + "IS_SELF = 1";
			else
				query = query + "ID_CONTRACTOR_GLOBAL = '" + id_contractor_global + "'";

			string CodeBD = "";
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand command = new SqlCommand(query, connection);
				connection.Open();

				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
					CodeBD = reader[0].ToString();
				reader.Close();
			}
			return CodeBD;
		}

		//---------------------------Получение поля по ID
		public string GetFieldByID(string ID, int index_field)
		{
			string query = "select top 1 * from SGAU_REP_UZO_PACK where prefix_pack+'/'+CAST(id_pack as VARCHAR(10)) = '" + ID.ToString() + "'";

			string out_str = "";
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand command = new SqlCommand(query, connection);
				connection.Open();

				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					if (reader[index_field].ToString() == "")
						break;

					switch (index_field)
					{
						case 3: out_str = ((DateTime)reader[index_field]).ToString("yyyy-MM-dd HH:mm:ss.fff"); break;
						case 4: if ((bool)reader[index_field]) out_str = "1"; else out_str = "0"; break;
						case 5: out_str = ((Int64)reader[index_field]).ToString(); break;
						case 6: out_str = ((Int64)reader[index_field]).ToString(); break;
						case 7: out_str = ((Int64)reader[index_field]).ToString(); break;
						default: out_str = out_str = (string)reader[index_field]; break;
					}
				}
				reader.Close();
			}
			return out_str;
		}

		//---------------------------Получение строки связи между письмом и реестром
		public string GetJoinStringByID(string ID)
		{
			//[%ID%] %NUMBER% от %DATE%
			return "[" + ID + "] " + GetFieldByID(ID, 2) + " от " + DateTime.Parse(GetFieldByID(ID, 3)).ToString("dd.MM.yyyy");
		}

		//---------------------------Генерация имени реестра
		public string GetNextName(string id_contractor_global)
		{
			string NextNumber = "1";

			string query = "select COUNT(*)+1 from SGAU_REP_UZO_PACK where date_pack > '" + DateTime.Now.Year.ToString() + "'";
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand command = new SqlCommand(query, connection);
				connection.Open();

				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					NextNumber = reader[0].ToString();
				}
				reader.Close();
			}

			while (NextNumber.Length < 5)
				NextNumber = "0" + NextNumber;

			return GetBDCode(id_contractor_global) + "/" + NextNumber;
		}

		//---------------------------Создание таблицы для хранения опций
		public void InitOptions(string mnemocode, string value)
		{
			//CreateStoredProc(this.connectionString, "create_table_pack_options.sql");
			StringBuilder queryString = new StringBuilder("");
			queryString.Append("if not exists (select * from SGAU_REP_UZO_PACK_OPTIONS where mnemocode='" + mnemocode + "')    ");
			queryString.Append("insert into SGAU_REP_UZO_PACK_OPTIONS values ('" + mnemocode + "', '" + value + "')    ");
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand command = new SqlCommand(queryString.ToString(), connection);
				connection.Open();
				command.ExecuteNonQuery();
			}
		}

		//---------------------------Создание таблицы SGAU_REP_UZO_PACK
		private void UzoBalancePackParamsParams_Load(object sender, EventArgs e)
		{
			for (int i = 0; i < chklstbForms.Items.Count; i++)
				chklstbForms.SetItemChecked(i, true);

			StringBuilder queryString = new StringBuilder("");

			//queryString.Append("if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[SGAU_REP_UZO_PACK]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)  ");
			//queryString.Append("CREATE TABLE SGAU_REP_UZO_PACK  ");
			//queryString.Append("(    ");
			//queryString.Append("prefix_pack VARCHAR(10) NOT NULL,  ");
			//queryString.Append("id_pack bigint NOT NULL,  ");
			//queryString.Append("number_pack VARCHAR(100),  ");
			//queryString.Append("date_pack datetime,  ");
			//queryString.Append("balance bit,");
			//queryString.Append("id_medical bigint, ");
			//queryString.Append("id_contractor_perform bigint, ");
			//queryString.Append("id_contractor_custom bigint ");
			//queryString.Append(")    ");

			//using (SqlConnection connection = new SqlConnection(connectionString))
			//{
			//    SqlCommand command = new SqlCommand(queryString.ToString(), connection);
			//    connection.Open();
			//    command.ExecuteNonQuery();
			//}

			CreateStoredProc(this.connectionString, "create_pack_tables.sql");

			InitOptions("OFFICE_GLOBAL_CODE", "000");
			InitOptions("SERVICE_PERCENT", "15");
			InitOptions("SERVICE_NDS", "18");
			InitOptions("ENABLE_EDIT", "YES");
			InitOptions("BASE_SIGN_BALANCE", "Приказ № 32/1 от 15.03.10");
			InitOptions("RENUMBER_PACK_DATE_START", "2013-01-01");
			InitOptions("RENUMBER_PACK_DATE_END", "2013-03-01");
			InitOptions("RENUMBER_PACK_UPDATER", "1");
			InitOptions("NEW_PERCENT_PARAM_UPDATER", "1");

			CreateStoredProc(this.connectionString, "updater_1_pack_num.sql");
			CreateStoredProc(this.connectionString, "updater_2_percent_table.sql");

			ucPeriod1.DateFrom = new DateTime(DateTime.Now.Year, 1, 1);
			ucPeriod1.DateTo = DateTime.Now;

			//------------------------Автогенерация реестров по доверительным письмам
			queryString = new StringBuilder("");
			queryString.Append("select  ");
			queryString.Append("	number_pack = REPLACE(REPLACE(TL.reestr_number, '  ', ' '), '  ', ' '), ");
			queryString.Append("	balance = case when STT.MNEMOCODE in (select distinct PS_T.mnemocode_store_type from SGAU_REP_UZO_PACK_PERCENT_SERVICE PS_T) then 0 else 1 end, ");
			queryString.Append("	id_medical = TL.ID_DISCOUNT2_MEDICAL_ORGANIZATION, ");
			queryString.Append("	id_contractor_perform = ST.id_contractor ");
			queryString.Append(" from trust_letter TL ");
			queryString.Append(" inner join store ST on TL.id_store = ST.id_store ");
			queryString.Append(" inner join store_type STT on ST.id_store_type_global = STT.id_store_type_global ");
			queryString.Append(" where TL.document_state = 'PROC'  ");
			queryString.Append("    and ISNULL(TL.reestr_number,'') like '[[]%]% от %' ");
			queryString.Append("	and ");
			queryString.Append("	not TL.reestr_number in  ");
			queryString.Append("	( ");
			queryString.Append("		select '['+UP.prefix_pack+'/'+CAST(UP.id_pack as VARCHAR(10))+'] ' + UP.number_pack + ' от '+CONVERT(VARCHAR(10), UP.date_pack, 104) ");
			queryString.Append("		from SGAU_REP_UZO_PACK UP ");
			queryString.Append("	) ");
			queryString.Append("group by TL.reestr_number, TL.ID_DISCOUNT2_MEDICAL_ORGANIZATION, STT.MNEMOCODE, ST.id_contractor ");

			int n_gen_pack = 0;
			string message = "";
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand command = new SqlCommand(queryString.ToString(), connection);
				connection.Open();

				SqlDataReader reader = command.ExecuteReader();

				while (reader.Read())
				{
					Regex rgx = new Regex(@"\[(?<g1>\d+|\w+|\D+|\W+|\S+)/(?<g2>\d+|\w+|\D+|\W+|\S+)\] (?<g3>\d+|\w+|\D+|\W+|\S+) от (?<g4>\d+|\w+|\D+|\W+|\S+)\Z");
					Match match = rgx.Match(reader[0].ToString());
					string f1 = match.Groups[1].ToString();
					string f2 = match.Groups[2].ToString();
					string f3 = match.Groups[3].ToString();
					string f4 = match.Groups[4].ToString();
					string f5 = reader[1].ToString();
					string f6 = reader[2].ToString();
					string f7 = reader[3].ToString();
					///////////////////////////////////////
					StringBuilder queryString2 = new StringBuilder("");
					queryString2.Append("INSERT INTO SGAU_REP_UZO_PACK (prefix_pack, id_pack, number_pack, date_pack, balance, id_medical, id_contractor_perform, id_contractor_custom)");
					queryString2.Append("VALUES");
					queryString2.Append("(");
					queryString2.Append("'" + f1 + "'," + f2 + ",'" + f3 + "','" + DateTime.Parse(f4).ToString("yyyy-MM-dd HH:mm:ss.fff") + "', " + f5 + ", " + f6 + ", " + f7 + ", NULL");
					queryString2.Append(")");
					using (SqlConnection connection2 = new SqlConnection(connectionString))
					{
						SqlCommand command2 = new SqlCommand(queryString2.ToString(), connection2);
						connection2.Open();
						command2.ExecuteNonQuery();
					}
					n_gen_pack++;
					message += f3 + " от " + f4 + "\n";
				}
				reader.Close();

				if (n_gen_pack > 0)
					MessageBox.Show("Автосоздано реестров: " + n_gen_pack.ToString() + "\n---\n" + message);
			}

			this.btnRefresh_Click(sender, e);
			cbSelectedUzoPack.SelectedIndex = cbSelectedUzoPack.Items.Count - 1;
			cbUzoPack.SelectedIndex = cbUzoPack.Items.Count - 1;

			string query = "select value from SGAU_REP_UZO_PACK_OPTIONS where mnemocode = 'ENABLE_EDIT'";
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand command = new SqlCommand(query, connection);
				connection.Open();

				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					if (reader[0].ToString() != "YES")
					{
						groupBox1.Visible = false;
						groupBox2.Visible = false;
						groupBox3.Visible = false;
						btnSavePack.Visible = false;
						dataGridView1.ReadOnly = true;
						dataGridView1.Width = panel2.Width;
					}
				}
				reader.Close();
			}

			_formLoaded = true;
		}

		//---------------------------Обновить данные
		private void btnRefresh_Click(object sender, EventArgs e)
		{

			dataGridView1.Rows.Clear();

			int lo_index = cbUzoPack.SelectedIndex;

			string lo_key = "";
			if (lo_index != -1)
				lo_key = keys[lo_index];

			string date_from = ucPeriod1.DateFrom.ToString("d");
			//ucPeriod1.DateFrom.ToString("yyyy-MM-dd");
			string date_to = ucPeriod1.DateTo.ToString("d");
			//ucPeriod1.DateTo.ToString("yyyy-MM-dd");

			StringBuilder queryString = new StringBuilder("");

			queryString.Append("select distinct id = prefix_pack+'/'+CAST(id_pack as VARCHAR(10)), name = number_pack + ' от '+CONVERT(VARCHAR(10), date_pack, 104) ");
			queryString.Append("from SGAU_REP_UZO_PACK ");
			queryString.Append("where balance = 1 ");
			if (date_from != "0001-01-01")
				queryString.Append(" AND date_pack >= '" + date_from + " 00:00:00.000' ");
			if (date_to != "0001-01-01")
				queryString.Append(" AND date_pack <= '" + date_to + " 23:59:59.999' ");
			queryString.Append(" order by name ");

			cbUzoPack.Items.Clear();
			cbSelectedUzoPack.Items.Clear();

			N_keys = 0;
			keys = new string[100000];

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand command = new SqlCommand(queryString.ToString(), connection);
				connection.Open();

				SqlDataReader reader = command.ExecuteReader();

				while (reader.Read())
				{
					keys[N_keys] = reader[0].ToString();
					N_keys++;
					cbUzoPack.Items.Add(reader[1]);
					cbSelectedUzoPack.Items.Add(reader[1]);
				}
				reader.Close();
			}

			if (cbUzoPack.Items.Count != 0)
				cbUzoPack.SelectedIndex = 0;
			else
				cbUzoPack.SelectedIndex = cbUzoPack.Items.Count - 1;

			if (cbSelectedUzoPack.Items.Count != 0)
				cbSelectedUzoPack.SelectedIndex = 0;
			else
				cbSelectedUzoPack.SelectedIndex = cbSelectedUzoPack.Items.Count - 1;

			queryString = new StringBuilder("");
			queryString.Append(" select  ");
			queryString.Append("  	C1_ID = ISNULL(C1.id_contractor, 0), ");
			queryString.Append("  	C1_GUID = C1.id_contractor_global,  ");
			queryString.Append("  	C1_NAME = C1.name,  ");
			queryString.Append("  	C2_ID = ISNULL(C2.id_contractor, 0), ");
			queryString.Append("  	C2_GUID = C2.id_contractor_global,  ");
			queryString.Append("  	C2_NAME = C2.name,  ");
			queryString.Append("  	DMO_ID = ISNULL(DMO.ID_DISCOUNT2_MEDICAL_ORGANIZATION, 0), ");
			queryString.Append("  	DMO_GUID = DMO.ID_DISCOUNT2_MEDICAL_ORGANIZATION_GLOBAL,  ");
			queryString.Append("  	DMO_NAME = DMO.name  ");
			queryString.Append("  from SGAU_REP_UZO_PACK UP  ");
			queryString.Append("  left join contractor C1 on C1.id_contractor = UP.id_contractor_perform  ");
			queryString.Append("  left join contractor C2 on C2.id_contractor = UP.id_contractor_custom  ");
			queryString.Append("  left join DISCOUNT2_MEDICAL_ORGANIZATION DMO on DMO.ID_DISCOUNT2_MEDICAL_ORGANIZATION = UP.id_medical  ");
			queryString.Append("  where UP.id_pack = (select max(id_pack) from SGAU_REP_UZO_PACK where not id_contractor_custom is null)  ");

			ucHospital.Clear();
			ucContractor1.Clear();
			ucContractor2.Clear();
			ucStore.Clear();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand command = new SqlCommand(queryString.ToString(), connection);
				connection.Open();

				SqlDataReader reader = command.ExecuteReader();

				while (reader.Read())
				{
					if ((long)reader[0] != 0)
					{
						DataRowItem lo_item = new DataRowItem();
						lo_item.Id = (long)reader[0];
						lo_item.Guid = (Guid)reader[1];
						lo_item.Text = (string)reader[2];
						ucContractor1.SetValues(lo_item);
					}
					if ((long)reader[3] != 0)
					{
						DataRowItem lo_item = new DataRowItem();
						lo_item.Id = (long)reader[3];
						lo_item.Guid = (Guid)reader[4];
						lo_item.Text = (string)reader[5];
						ucContractor2.SetValues(lo_item);
					}
					if ((int)reader[6] != 0)
					{
						DataRowItem lo_item = new DataRowItem();
						lo_item.Id = (int)reader[6];
						lo_item.Guid = (Guid)reader[7];
						lo_item.Text = (string)reader[8];
						ucHospital.SetValues(lo_item);
					}
				}
				reader.Close();
			}

			if (lo_index != -1)
				for (int i = 0; i < N_keys; i++)
					if (keys[i] == lo_key)
					{
						cbUzoPack.SelectedIndex = i;
						break;
					}
			edNewUzoPack.Text = GetNextName(ucContractor1.Guid.ToString());

			//---Обновление таблицы неподобранных писем
			queryString = new StringBuilder("");

			queryString.Append(" select  ");
			queryString.Append("     id = TL.id_trust_letter_global,  ");
			queryString.Append("     recipe = TL.SERIES_RECIPE+'-'+TL.NUMBER_RECIPE,  ");
			queryString.Append("     name = TL.MNEMOCODE,   ");
			queryString.Append("     DATE_TL = CONVERT(VARCHAR(10), TL.DATE_SHIP, 104),   ");
			queryString.Append("     SUM_PAY = SUM(TLI.PRICE_SAL * TLI.QUANTITY),   ");
			queryString.Append("     DMO.name,   ");
			queryString.Append("     POLIS = TL.NUMBER_POLICY,   ");
			queryString.Append("     MEM_NAME = ISNULL(MEM.LASTNAME + ' ', '') + ISNULL(UPPER(LEFT(MEM.FIRSTNAME,1)) + '. ', '') + ISNULL(UPPER(LEFT(MEM.MIDDLENAME,1))+'.', ''),   ");
			queryString.Append("     STORE_NAME = ST.NAME");
			queryString.Append(" from trust_letter TL   ");
			queryString.Append(" inner join store ST on TL.id_store = ST.id_store ");
			queryString.Append(" inner join store_type STT on ST.id_store_type_global = STT.id_store_type_global ");
			queryString.Append(" inner join trust_letter_item TLI on TLI.ID_TRUST_LETTER_GLOBAL = TL.ID_TRUST_LETTER_GLOBAL ");
			queryString.Append(" left join SGAU_REP_UZO_PACK UP on ((TL.reestr_number = '['+UP.PREFIX_PACK+'/'+CAST(UP.ID_PACK as VARCHAR(10))+'] ' + UP.number_pack + ' от '+CONVERT(VARCHAR(10), UP.date_pack, 104)) and TL.DOCUMENT_STATE='PROC')   ");
			queryString.Append(" left join DISCOUNT2_MEDICAL_ORGANIZATION DMO on TL.ID_DISCOUNT2_MEDICAL_ORGANIZATION = DMO.ID_DISCOUNT2_MEDICAL_ORGANIZATION   ");
			queryString.Append(" left join DISCOUNT2_MEMBER MEM on TL.ID_DISCOUNT2_MEMBER_GLOBAL = MEM.ID_DISCOUNT2_MEMBER_GLOBAL   ");
			queryString.Append(" WHERE  ");
			queryString.Append("     ISNULL(UP.id_pack,'')='' AND TL.document_state = 'PROC' AND NOT STT.MNEMOCODE in (select distinct PS_T.mnemocode_store_type from SGAU_REP_UZO_PACK_PERCENT_SERVICE PS_T) ");
			queryString.Append(" GROUP BY  ");
			queryString.Append("	TL.id_trust_letter_global,  ");
			queryString.Append("    TL.SERIES_RECIPE+'-'+TL.NUMBER_RECIPE,  ");
			queryString.Append("    TL.MNEMOCODE,   ");
			queryString.Append("    TL.DATE_SHIP, ");
			queryString.Append("    DMO.name,   ");
			queryString.Append("    TL.NUMBER_POLICY,   ");
			queryString.Append("    MEM.LASTNAME, MEM.FIRSTNAME, MEM.MIDDLENAME, ");
			queryString.Append("    ST.NAME ");

			dataGridView2.Rows.Clear();

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand command = new SqlCommand(queryString.ToString(), connection);
				connection.Open();

				SqlDataReader reader = command.ExecuteReader();

				while (reader.Read())
				{
					dataGridView2.Rows.Add(reader[0], reader[1], reader[2], reader[3], reader[4], reader[5], reader[6], reader[7], reader[8]);
				}
				reader.Close();
			}
		}

		//---------------------------Создание нового реестра
		private void btnNewGroup_Click(object sender, EventArgs e)
		{
			if (ucHospital.Id == 0)
			{
				MessageBox.Show("Не выбрано лечебное учреждение!");
				return;
			}

			if (ucContractor1.Id == 0)
			{
				MessageBox.Show("Не выбран исполнитель!");
				return;
			}

			if (ucContractor2.Id == 0)
			{
				MessageBox.Show("Не выбран заказчик!");
				return;
			}

			if (edNewUzoPack.Text.ToString().Length != 0)
			{
				edNewUzoPack.Text = edNewUzoPack.Text.Replace("'", " ");
				StringBuilder queryString = new StringBuilder("");

				queryString.Append("select count(*) from SGAU_REP_UZO_PACK where CONVERT(VARCHAR(4), getdate(), 102) = CONVERT(VARCHAR(4), date_pack, 102) and number_pack = '" + edNewUzoPack.Text.ToString() + "'");
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					SqlCommand command = new SqlCommand(queryString.ToString(), connection);
					connection.Open();

					SqlDataReader reader = command.ExecuteReader();

					while (reader.Read())
					{
						if (((int)reader[0]) > 0)
						{
							MessageBox.Show("Такое наименование реестра уже существует!");
							return;
						}
					}
					reader.Close();
				}

				Int64 NewId = GetNextID(ucContractor1.Guid.ToString());
				string NewPrefix = GetBDCode(ucContractor1.Guid.ToString());

				queryString = new StringBuilder("");
				queryString.Append("INSERT INTO SGAU_REP_UZO_PACK (prefix_pack, id_pack, number_pack, date_pack, balance, id_medical, id_contractor_perform, id_contractor_custom)");
				queryString.Append("VALUES");
				queryString.Append("(");
				queryString.Append("'" + NewPrefix + "', " + NewId.ToString() + ",'" + edNewUzoPack.Text.ToString() + "', GETDATE(), 1, " + ucHospital.Id.ToString() + ", " + ucContractor1.Id.ToString() + ", " + ucContractor2.Id.ToString());
				queryString.Append(")");

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					SqlCommand command = new SqlCommand(queryString.ToString(), connection);
					connection.Open();
					command.ExecuteNonQuery();
				}

				queryString = new StringBuilder("");
				queryString.Append("UPDATE trust_letter set reestr_number = '" + GetJoinStringByID(NewPrefix + "/" + NewId.ToString()) + "' ");
				queryString.Append("where  ");
				queryString.Append("	date_ship<GETDATE() ");
				queryString.Append("	and document_state = 'PROC' ");
				queryString.Append("	and id_store in (select id_store from store where id_contractor = " + ucContractor1.Id.ToString() + ") ");
				queryString.Append("	and not (select top 1 STT_TMP.mnemocode from store_type STT_TMP inner join store ST_TMP on ST_TMP.id_store_type_global = STT_TMP.id_store_type_global and ST_TMP.id_store = trust_letter.id_store) in (select distinct PS_T.mnemocode_store_type from SGAU_REP_UZO_PACK_PERCENT_SERVICE PS_T) ");
				queryString.Append("	and ISNULL(reestr_number,'')='' ");
				queryString.Append("    and ID_DISCOUNT2_MEDICAL_ORGANIZATION = " + ucHospital.Id.ToString());
				if (ucStore.Id != 0)
					queryString.Append("    and ID_STORE = " + ucStore.Id.ToString());

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					SqlCommand command = new SqlCommand(queryString.ToString(), connection);
					connection.Open();
					command.ExecuteNonQuery();
				}

				MessageBox.Show("Реестр сформирован!");

				this.btnRefresh_Click(sender, e);

				for (int i = 0; i < N_keys; i++)
					if (keys[i] == NewPrefix + "/" + NewId.ToString())
					{
						cbUzoPack.SelectedIndex = i;
						break;
					}
			}
		}

		//---------------------------Редактирование реестра
		private void btnEditPack_Click(object sender, EventArgs e)
		{
			if (cbUzoPack.SelectedIndex == -1)
				return;

			if (edEditUzoPack.Text.ToString().Length != 0)
			{
				edEditUzoPack.Text = edEditUzoPack.Text.Replace("'", " ");

				string EditId = keys[cbUzoPack.SelectedIndex];

				StringBuilder queryString = new StringBuilder("");

				queryString.Append("select count(*) from SGAU_REP_UZO_PACK where CONVERT(VARCHAR(4), date_pack, 102) = '" + dtpEditUzoPack.Value.ToString("yyyy") + "' and number_pack = '" + edEditUzoPack.Text.ToString() + "' and not (prefix_pack+'/'+CAST(id_pack as VARCHAR(10)) = '" + EditId.ToString() + "')");
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					SqlCommand command = new SqlCommand(queryString.ToString(), connection);
					connection.Open();

					SqlDataReader reader = command.ExecuteReader();

					while (reader.Read())
					{
						if (((int)reader[0]) > 0)
						{
							MessageBox.Show("Такое наименование реестра уже существует!");
							return;
						}
					}
					reader.Close();
				}

				string old_join_str = GetJoinStringByID(EditId);

				queryString = new StringBuilder("");
				queryString.Append("UPDATE SGAU_REP_UZO_PACK SET number_pack = '" + edEditUzoPack.Text.ToString() + "', ");
				queryString.Append(" date_pack = '" + dtpEditUzoPack.Value.ToString("yyyy-MM-dd HH:mm:ss.fff") + "', ");
				queryString.Append(" id_contractor_custom = " + ucContractor2Edit.Id.ToString() + " ");
				queryString.Append(" WHERE prefix_pack+'/'+CAST(id_pack as VARCHAR(10)) =  '" + EditId + "'");

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					SqlCommand command = new SqlCommand(queryString.ToString(), connection);
					connection.Open();
					command.ExecuteNonQuery();
				}

				queryString = new StringBuilder("");
				queryString.Append("UPDATE trust_letter set reestr_number = '" + GetJoinStringByID(EditId) + "'");
				queryString.Append(" WHERE reestr_number = '" + old_join_str + "' ");

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					SqlCommand command = new SqlCommand(queryString.ToString(), connection);
					connection.Open();
					command.ExecuteNonQuery();
					MessageBox.Show("Реестр отредактирован!");
				}


				this.btnRefresh_Click(sender, e);
			}
		}

		//---------------------------Удаление реестра
		private void btnDelGroup_Click(object sender, EventArgs e)
		{
			if (cbUzoPack.SelectedIndex == -1)
				return;

			string DelId = keys[cbUzoPack.SelectedIndex];

			StringBuilder queryString = new StringBuilder("");

			queryString.Append("UPDATE trust_letter set reestr_number = NULL ");
			queryString.Append(" WHERE reestr_number = '" + GetJoinStringByID(DelId) + "' ");

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand command = new SqlCommand(queryString.ToString(), connection);
				connection.Open();
				command.ExecuteNonQuery();
			}

			queryString = new StringBuilder("");

			queryString.Append("DELETE FROM SGAU_REP_UZO_PACK");
			queryString.Append(" WHERE prefix_pack+'/'+CAST(id_pack as VARCHAR(10)) = '" + DelId + "'");

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand command = new SqlCommand(queryString.ToString(), connection);
				connection.Open();
				command.ExecuteNonQuery();
				MessageBox.Show("Реестр удален!");
			}

			this.btnRefresh_Click(sender, e);
		}

		//---------------------------Сохранение списка писем в реестре
		private void btnSaveGroup_Click(object sender, EventArgs e)
		{
			if (cbUzoPack.SelectedIndex == -1)
				return;

			string EditId = keys[cbUzoPack.SelectedIndex];

			StringBuilder queryString;
			queryString = new StringBuilder("");

			queryString.Append("UPDATE trust_letter set reestr_number = NULL ");
			queryString.Append(" WHERE reestr_number = '" + GetJoinStringByID(EditId) + "' ");

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand command = new SqlCommand(queryString.ToString(), connection);
				connection.Open();
				command.ExecuteNonQuery();
			}

			for (int i = 0; i < dataGridView1.Rows.Count; i++)
			{
				if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "True")
				{
					queryString = new StringBuilder("");
					queryString.Append("UPDATE trust_letter set reestr_number = '" + GetJoinStringByID(EditId) + "' ");
					queryString.Append(" WHERE id_trust_letter_global = '" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "' ");

					using (SqlConnection connection = new SqlConnection(connectionString))
					{
						SqlCommand command = new SqlCommand(queryString.ToString(), connection);
						connection.Open();
						command.ExecuteNonQuery();
					}
				}
			}

			MessageBox.Show("Список сохранен!");
			this.btnRefresh_Click(sender, e);
		}

		//---------------------------Загрузка списка писем выбранного реестра
		private void cbUzoPack_SelectedIndexChanged(object sender, EventArgs e)
		{
			dataGridView1.Rows.Clear();

			if (cbUzoPack.SelectedIndex == -1)
				return;

			cbSelectedUzoPack.SelectedIndex = cbUzoPack.SelectedIndex;

			string ShowId = keys[cbUzoPack.SelectedIndex];

			string lo_medical = GetFieldByID(ShowId, 5);
			string lo_perform = GetFieldByID(ShowId, 6);

			StringBuilder queryString = new StringBuilder("");

			queryString.Append(" select   ");
			queryString.Append("	checked = case when not UP.number_pack is null then 'True' else 'False' end,  ");
			queryString.Append("	id = TL.id_trust_letter_global,  ");
			queryString.Append("	recipe = TL.SERIES_RECIPE+'-'+TL.NUMBER_RECIPE,  ");
			queryString.Append("	name = TL.MNEMOCODE,  ");
			queryString.Append("	number_pack = UP.number_pack,  ");
			queryString.Append("	date_pack = CONVERT(VARCHAR(10), UP.date_pack, 104), ");
			queryString.Append("	date_letter = CONVERT(VARCHAR(10), TL.date_ship, 104), ");
			queryString.Append("	sum_letter = SUM(TLI.PRICE_SAL * TLI.QUANTITY), ");
			queryString.Append("	company_name = DMO.name, ");
			queryString.Append("	store_name = ST.name ");
			queryString.Append(" from trust_letter TL  ");
			queryString.Append(" inner join trust_letter_item TLI on TLI.ID_TRUST_LETTER_GLOBAL = TL.ID_TRUST_LETTER_GLOBAL ");
			queryString.Append(" inner join store ST on TL.id_store = ST.id_store ");
			queryString.Append(" left join SGAU_REP_UZO_PACK UP on ((TL.reestr_number = '['+UP.PREFIX_PACK+'/'+CAST(UP.ID_PACK as VARCHAR(10))+'] ' + UP.number_pack + ' от '+CONVERT(VARCHAR(10), UP.date_pack, 104)) and TL.DOCUMENT_STATE='PROC') ");
			queryString.Append(" left join DISCOUNT2_MEDICAL_ORGANIZATION DMO on TL.ID_DISCOUNT2_MEDICAL_ORGANIZATION = DMO.ID_DISCOUNT2_MEDICAL_ORGANIZATION ");
			queryString.Append(" where   ");
			queryString.Append("	(");
			queryString.Append("        UP.number_pack is null ");
			queryString.Append("        AND TL.ID_DISCOUNT2_MEDICAL_ORGANIZATION = " + lo_medical);
			queryString.Append("	    AND TL.ID_STORE in (select ID_STORE from store where id_contractor = " + lo_perform + ") ");
			queryString.Append("        AND not (select top 1 STT_TMP.mnemocode from store_type STT_TMP inner join store ST_TMP on ST_TMP.id_store_type_global = STT_TMP.id_store_type_global and ST_TMP.id_store = TL.id_store) in (select distinct PS_T.mnemocode_store_type from SGAU_REP_UZO_PACK_PERCENT_SERVICE PS_T) ");
			queryString.Append("	)");
			queryString.Append("	OR  ");
			queryString.Append("	UP.prefix_pack+'/'+CAST(UP.id_pack as VARCHAR(10)) = '" + ShowId.ToString() + "' ");
			queryString.Append(" group by ");
			queryString.Append("	UP.number_pack,  ");
			queryString.Append("	TL.id_trust_letter_global,  ");
			queryString.Append("	TL.SERIES_RECIPE+'-'+TL.NUMBER_RECIPE,  ");
			queryString.Append("	TL.MNEMOCODE,  ");
			queryString.Append("	UP.number_pack,  ");
			queryString.Append("	UP.date_pack,  ");
			queryString.Append("	TL.date_ship,  ");
			queryString.Append("	DMO.name, ");
			queryString.Append("	ST.name ");
			queryString.Append(" order by TL.MNEMOCODE ");

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand command = new SqlCommand(queryString.ToString(), connection);
				connection.Open();

				SqlDataReader reader = command.ExecuteReader();

				while (reader.Read())
				{
					dataGridView1.Rows.Add(reader[0], reader[1], reader[2], reader[3], reader[6], reader[7], reader[8], reader[9]);
				}
				reader.Close();
			}

			edEditUzoPack.Text = GetFieldByID(keys[cbUzoPack.SelectedIndex], 2);
			dtpEditUzoPack.Value = DateTime.Parse(GetFieldByID(keys[cbUzoPack.SelectedIndex], 3));
			if (GetFieldByID(keys[cbUzoPack.SelectedIndex], 7) != "")
			{
				queryString = new StringBuilder("");

				queryString.Append(" select ");
				queryString.Append(" 	C.id_contractor, ");
				queryString.Append(" 	C.id_contractor_global, ");
				queryString.Append(" 	C.name ");
				queryString.Append(" from contractor C");
				queryString.Append(" where C.id_contractor = " + GetFieldByID(keys[cbUzoPack.SelectedIndex], 7));
				ucContractor2Edit.Clear();
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					SqlCommand command = new SqlCommand(queryString.ToString(), connection);
					connection.Open();

					SqlDataReader reader = command.ExecuteReader();

					while (reader.Read())
					{
						DataRowItem lo_item = new DataRowItem();
						lo_item.Id = (long)reader[0];
						lo_item.Guid = (Guid)reader[1];
						lo_item.Text = (string)reader[2];
						ucContractor2Edit.SetValues(lo_item);
					}
					reader.Close();
				}
			}
		}

		private void ucPeriod1_ValueChanged(object sender, EventArgs e)
		{
			this.btnRefresh_Click(sender, e);
			if (ucPeriod1.DateTo.Year == 2011)
				if (chklstbForms.Items.Count == 4)
					chklstbForms.Items.Add("Счет-фактура 2011");
		}

		private void ucContractor1_ValueChanged()
		{
			edNewUzoPack.Text = GetNextName(ucContractor1.Guid.ToString());
		}

		private void cbSelectedUzoPack_SelectedIndexChanged(object sender, EventArgs e)
		{
			cbUzoPack.SelectedIndex = cbSelectedUzoPack.SelectedIndex;
		}

		private void ucStore_BeforePluginShow(object sender, EventArgs e)
		{
			if (ucContractor1.Id != 0)
				ucStore.PluginContol.Grid(0).SetParameterValue("@ADV_FILTER", "ID_CONTRACTOR = " + ucContractor1.Id.ToString());
			else
				ucStore.PluginContol.Grid(0).SetParameterValue("@ADV_FILTER", "1 = 1");
		}

		private void ucStore_ValueChanged()
		{
			if (ucStore.Id != 0)
			{
				StringBuilder queryString = new StringBuilder("");

				queryString.Append(" select ");
				queryString.Append(" 	C.id_contractor, ");
				queryString.Append(" 	C.id_contractor_global, ");
				queryString.Append(" 	C.name ");
				queryString.Append(" from contractor C");
				queryString.Append(" inner join store ST on ST.id_contractor = C.id_contractor");
				queryString.Append(" where ST.id_store = " + ucStore.Id.ToString());
				ucContractor1.Clear();
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					SqlCommand command = new SqlCommand(queryString.ToString(), connection);
					connection.Open();

					SqlDataReader reader = command.ExecuteReader();

					while (reader.Read())
					{
						DataRowItem lo_item = new DataRowItem();
						lo_item.Id = (Int64)reader[0];
						lo_item.Guid = (Guid)reader[1];
						lo_item.Text = (string)reader[2];
						ucContractor1.SetValues(lo_item);
					}
					reader.Close();
				}
			}
		}
	}
}