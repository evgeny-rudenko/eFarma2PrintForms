using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using ePlus.CommonEx.AccessPoint;
using ePlus.MetaData.Core;
using ePlus.PriceList.ImportZakaz;

namespace ePlus.ImportZakaz
{
    public static class IMPORT
    {        
        public static void Import(ORDERS_BL orderBL, Dictionary<XmlDocument, string> fileNamesDir, CONFIGURATION_IMPORT confImport, AccessPointManager apmArchiv, AccessPointManager apmImport, string import_key)
        {
            //������ � ���� ����
            string fileNameLog = Path.Combine(Utils.TempDir(), "������ ������.txt");
            string stateDescription = string.Empty;
            foreach (KeyValuePair<XmlDocument, string> pair in fileNamesDir)
            {
                //���������,����� �� ��������� �����
                string save_state = orderBL.ValidateOrder(pair.Key, import_key);
                //����� ��������� � ������� � �����-�� ��������
                if (save_state != "NL" && !string.IsNullOrEmpty(save_state.Trim()))
                {
                    orderBL.Save(save_state, pair.Key, confImport.ID_CONFIGURATION_IMPORT);
                    //���������� ������ � ����� �����������
                    orderBL.OrderMoveToArchiv(apmArchiv, apmImport, pair.Value);
                    //���� ����� �����.���������,�� ������� ����
                    if (confImport.IS_AUTO_BILL && save_state == "NEW")
                        orderBL.CreateBill(pair.Key);
                }
                //����� � ��� ���� � ����������� ������
                switch (save_state)
                {
                    case "NL":
                        stateDescription = "��������(��� ���������� � �������)";
                        break;
                    case "NEW":
                        stateDescription = "��������";
                        break;
                    case "REJECT":
                        stateDescription = "��������(�� ������ ���������)";
                        break;
                    default:
                        break;
                }
                using (StreamWriter sw = new StreamWriter(fileNameLog, true, Encoding.Default))
                {
                    string NextLine = pair.Value + "; " + DateTime.Now.Date + "; " + stateDescription;
                    sw.WriteLine(NextLine);
                    sw.Close();
                }
            }

        }
    }
}
