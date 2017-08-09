using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.Reporting.WinForms;
using ePlus.CommonEx.Reporting;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;
using System.Runtime.InteropServices;
using System.Drawing.Text;


using System.Drawing;

using System.ComponentModel;

using System.Collections;

using System.Windows.Forms;


using System.Diagnostics; 

using System.Threading;
using System.Drawing.Imaging;



namespace FCSServicePriceList
{
	public class ServicePriceListEx : AbstractDocumentReport, IExternalDocumentPrintForm
	{
		const string CACHE_FOLDER = "Cache";
		string connectionString;
		string folderPath;
        public static List<Guid> GUPriceList;
        public static void AddGUPriceList(List<Guid> gUPriceList)
        {
            GUPriceList = gUPriceList;
        }
        void CreateStoredProc(string connectionString)
		{
            Stream s = this.GetType().Assembly.GetManifestResourceStream("FCSServicePriceList.ServicePriceList.sql");

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

		void ExtractReport()
		{
			string cachePath = Path.Combine(folderPath, CACHE_FOLDER);
			if (!Directory.Exists(cachePath))
				Directory.CreateDirectory(cachePath);
            Stream s = this.GetType().Assembly.GetManifestResourceStream("FCSServicePriceList.ServicePriceList.rdlc");
			using (StreamReader sr = new StreamReader(s))
			{
				using (StreamWriter sw = new StreamWriter(Path.Combine(cachePath, "ServicePriceList.rdlc")))
				{
					sw.Write(sr.ReadToEnd());
				}
			}

            s = this.GetType().Assembly.GetManifestResourceStream("FCSServicePriceList.ean13.ttf");
            using (BinaryReader br = new BinaryReader(s))
            {
                using (BinaryWriter bw = new BinaryWriter(File.Create(Path.Combine(cachePath, "ean13.ttf"))))
                {
                    bw.Write(br.ReadBytes((Int32)br.BaseStream.Length));
                }
            }
		}

		public override IReportForm GetReportForm(DataRowItem dataRowItem)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");
			Utils.AddNode(root, "ID_GLOBAL", dataRowItem.Guid);
            GUPriceList = null;
            using (FormSelectService fss = new FormSelectService(this, dataRowItem.Guid))
            {
                fss.ShowDialog();
            }
            if (GUPriceList!=null)
                foreach (Guid id in GUPriceList)
                {
                    Utils.AddNode(root, "SPLI", id);
                }
            ////////////////////////////////////////////////////////
            string PathTTF = Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "ean13.ttf");
            ////////////////////////////////////////////////////////
			ReportFormNew rep = new ReportFormNew();

			rep.Text = rep.ReportFormName = ReportName;
			rep.ReportPath = Path.Combine(Path.Combine(folderPath, CACHE_FOLDER), "ServicePriceList.rdlc");

			rep.LoadData("REPEX_SERVICE_PRICE_LIST", doc.InnerXml);
			rep.BindDataSource("ServicePriceList_Table0", 0);
			rep.BindDataSource("ServicePriceList_Table1", 1);

            foreach (DataRow item in rep.DataSource.Tables[1].Rows)
            {
                string bc = Convert.ToString(item["INTERNAL_BARCODE"]);
                if (bc.Length != 13) continue;
                Generator g = new Generator(PathTTF);
                g.Generate(EAN13(bc.Substring(0, 12)));
                byte[] bmpBin = g.Save().ToArray();
                item["DC_IMAGE"] = bmpBin;
            }
            List<ReportParameter> par = new List<ReportParameter>();
            par.Add(new ReportParameter("VER_DLL", System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName));
            rep.ReportViewer.LocalReport.SetParameters(par);
			return rep;
		}

        public class Generator
        {
            /// <summary>
            /// Шрифт
            /// </summary>
            private Font font;
            /// <summary>
            /// Изображение штрихкода
            /// </summary>
            private Bitmap barcodeBitmap;

            public Generator() : this(string.Empty) { }

            /// <summary>
            /// создает объект генератора
            /// </summary>
            /// <remarks>Размер шрифта по умолчанию - 50 em</remarks>
            /// <param name="fontPath">Путь к шрифту Code39</param>
            public Generator(string fontPath)
            {
                /*
                if (fontPath == string.Empty)
                {
                    fontPath = Environment.CurrentDirectory + "font.ttf";
                }
                */
                PrivateFontCollection pfc = new PrivateFontCollection();
                pfc.AddFontFile(fontPath);
                /*
                foreach (FontFamily fontFamily in pfc.Families)
                    MessageBox.Show(fontFamily.Name) ;
 */
                FontFamily family = new FontFamily("Code EAN13", pfc);

                font = new Font(family, 50);
            }

            /// <summary>
            /// Установка размера шрифта
            /// </summary>
            /// <param name="emSize">Размер шрифта в em</param>
            public void SetSize(float emSize)
            {
                font = new Font(font.FontFamily, emSize);
            }

            /// <summary>
            /// Генерирует штрих-код по заданному коду
            /// </summary>
            /// <param name="code">Код</param>
            public void Generate(string code)
            {
                /*
                if (code.IndexOf('*') != 0)
                {
                    code = '*' + code;
                }
                if (code.LastIndexOf('*') != code.Length - 1)
                {
                    code += '*';
                }
                */
                barcodeBitmap = new Bitmap(1, 1);
                Graphics objGraphics = Graphics.FromImage(barcodeBitmap);
                SizeF barCodeSize = objGraphics.MeasureString(code, font);
                barcodeBitmap = new Bitmap((int)barCodeSize.Width, (int)barCodeSize.Height);
                objGraphics = Graphics.FromImage(barcodeBitmap);
                objGraphics.FillRectangle(new SolidBrush(Color.White), 0, 0, barcodeBitmap.Width, barcodeBitmap.Height);
                objGraphics.DrawString(code, font, new SolidBrush(Color.Black), 0, 0);
                //barcodeBitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }

            /// <summary>
            /// Сохраняет изображение штрих-кода в поток
            /// </summary>
            /// <returns>Поток с изображением штрих-кода</returns>
            public MemoryStream Save()
            {
                MemoryStream ms = new MemoryStream();
                barcodeBitmap.Save(ms, ImageFormat.Gif);
                return ms;
            }

            /// <summary>
            /// Сохраняет изображение штрихкода на диск в формате GIF
            /// </summary>
            /// <param name="path">Путь для сохранения</param>
            public void SaveToDisc(string path)
            {
                barcodeBitmap.Save(path, ImageFormat.Gif);
            }
        }



        public string EAN13(string chaine)
        {
            //V 1.0
            //Paramиtres : une chaine de 12 chiffres
            //Parameters : a 12 digits length string
            //Retour : * une chaine qui, affichйe avec la police EAN13.TTF, donne le code barre
            //         * une chaine vide si paramиtre fourni incorrect
            //Return : * a string which give the bar code when it is dispayed with EAN13.TTF font
            //         * an empty string if the supplied parameter is no good
            int i;
            int first;
            int checksum = 0;
            string CodeBarre = "";
            bool tableA;

            //Vйrifier qu'il y a 12 caractиres
            //Check for 12 characters
            //Et que ce sont bien des chiffres
            //And they are really digits
            if (Regex.IsMatch(chaine, "^\\d{12}$"))
            {
                // Calcul de la clй de contrфle
                // Calculation of the checksum
                for (i = 1; i < 12; i += 2)
                {
                    System.Diagnostics.Debug.WriteLine(chaine.Substring(i, 1));
                    checksum += Convert.ToInt32(chaine.Substring(i, 1));
                }
                checksum *= 3;
                for (i = 0; i < 12; i += 2)
                {
                    checksum += Convert.ToInt32(chaine.Substring(i, 1));
                }

                chaine += (10 - checksum % 10) % 10;
                //Le premier chiffre est pris tel quel, le deuxiиme vient de la table A
                //The first digit is taken just as it is, the second one come from table A
                CodeBarre = chaine.Substring(0, 1) + (char)(65 + Convert.ToInt32(chaine.Substring(1, 1)));
                first = Convert.ToInt32(chaine.Substring(0, 1));
                for (i = 2; i <= 6; i++)
                {
                    tableA = false;
                    switch (i)
                    {
                        case 2:
                            if (first >= 0 && first <= 3) tableA = true;
                            break;
                        case 3:
                            if (first == 0 || first == 4 || first == 7 || first == 8) tableA = true;
                            break;
                        case 4:
                            if (first == 0 || first == 1 || first == 4 || first == 5 || first == 9) tableA = true;
                            break;
                        case 5:
                            if (first == 0 || first == 2 || first == 5 || first == 6 || first == 7) tableA = true;
                            break;
                        case 6:
                            if (first == 0 || first == 3 || first == 6 || first == 8 || first == 9) tableA = true;
                            break;
                    }

                    if (tableA)
                        CodeBarre += (char)(65 + Convert.ToInt32(chaine.Substring(i, 1)));
                    else
                        CodeBarre += (char)(75 + Convert.ToInt32(chaine.Substring(i, 1)));
                }
                CodeBarre += "*"; //Ajout sйparateur central / Add middle separator

                for (i = 7; i <= 12; i++)
                {
                    CodeBarre += (char)(97 + Convert.ToInt32(chaine.Substring(i, 1)));
                }
                CodeBarre += "+"; //Ajout de la marque de fin / Add end mark
            }
            return CodeBarre;
        }

		public string PluginCode
		{
			get { return "SERVICE_PRICE_LIST"; }
		}

		public void Execute(string connectionString, string folderPath)
		{
			this.connectionString = connectionString;
			this.folderPath = folderPath;
			CreateStoredProc(this.connectionString);
			ExtractReport();
		}

		public string GroupName
		{
			get { return string.Empty; }
		}

		public string ReportName
		{
			get { return "Прайс-лист"; }
		}
	}


}