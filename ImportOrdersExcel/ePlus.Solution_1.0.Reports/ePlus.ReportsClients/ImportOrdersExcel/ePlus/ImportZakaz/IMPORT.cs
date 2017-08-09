namespace ePlus.ImportZakaz
{
    using ePlus.CommonEx.AccessPoint;
    using ePlus.MetaData.Core;
    using ePlus.PriceList.ImportZakaz;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml;

    public static class IMPORT
    {
        public static void Import(ORDERS_BL orderBL, Dictionary<XmlDocument, string> fileNamesDir, CONFIGURATION_IMPORT confImport, AccessPointManager apmArchiv, AccessPointManager apmImport, string import_key)
        {
            string path = Path.Combine(Utils.TempDir(), "Импорт заказа.txt");
            string str2 = string.Empty;
            foreach (KeyValuePair<XmlDocument, string> pair in fileNamesDir)
            {
                string str3 = orderBL.ValidateOrder(pair.Key, import_key);
                if ((str3 != "NL") && !string.IsNullOrEmpty(str3.Trim()))
                {
                    orderBL.Save(str3, pair.Key, confImport.ID_CONFIGURATION_IMPORT);
                    orderBL.OrderMoveToArchiv(apmArchiv, apmImport, pair.Value);
                    if (confImport.IS_AUTO_BILL && (str3 == "NEW"))
                    {
                        orderBL.CreateBill(pair.Key);
                    }
                }
                string str5 = str3;
                if (str5 != null)
                {
                    if (!(str5 == "NL"))
                    {
                        if (str5 == "NEW")
                        {
                            goto Label_00EF;
                        }
                        if (str5 == "REJECT")
                        {
                            goto Label_00F7;
                        }
                    }
                    else
                    {
                        str2 = "Отклонен(уже существует в системе)";
                    }
                }
                goto Label_0101;
            Label_00EF:
                str2 = "Загружен";
                goto Label_0101;
            Label_00F7:
                str2 = "Отклонен(не прошел валидацию)";
            Label_0101:
                using (StreamWriter writer = new StreamWriter(path, true, Encoding.Default))
                {
                    string str4 = string.Concat(new object[] { pair.Value, "; ", DateTime.Now.Date, "; ", str2 });
                    writer.WriteLine(str4);
                    writer.Close();
                }
            }
        }
    }
}

