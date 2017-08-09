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
            //запись в файл лога
            string fileNameLog = Path.Combine(Utils.TempDir(), "Импорт заказа.txt");
            string stateDescription = string.Empty;
            foreach (KeyValuePair<XmlDocument, string> pair in fileNamesDir)
            {
                //проверяем,можно ли загрузить заказ
                string save_state = orderBL.ValidateOrder(pair.Key, import_key);
                //заказ загружаем в систему с каким-то статусом
                if (save_state != "NL" && !string.IsNullOrEmpty(save_state.Trim()))
                {
                    orderBL.Save(save_state, pair.Key, confImport.ID_CONFIGURATION_IMPORT);
                    //перемещаем файлик в папку загруженных
                    orderBL.OrderMoveToArchiv(apmArchiv, apmImport, pair.Value);
                    //если стоит соотв.настройка,то создаем счет
                    if (confImport.IS_AUTO_BILL && save_state == "NEW")
                        orderBL.CreateBill(pair.Key);
                }
                //пишем в лог инфу о загружаемом заказе
                switch (save_state)
                {
                    case "NL":
                        stateDescription = "Отклонен(уже существует в системе)";
                        break;
                    case "NEW":
                        stateDescription = "Загружен";
                        break;
                    case "REJECT":
                        stateDescription = "Отклонен(не прошел валидацию)";
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
