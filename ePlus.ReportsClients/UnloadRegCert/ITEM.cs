using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Xml;
using ePlus.MetaData.Core;

namespace UnloadRegCert
{
    public class ITEM
    {
        private Guid id_invoice_item_global;
        private string mnemocode; // название ПН
        private string name;    //наимен товара
        private string code;    //код
        private string prod;    //производитель
        private string post; //поставщик
        private string ser;      //серия
        private string nsert; //номер сертификата
        private string centrsert; //центр сертификации
        private decimal num;   //кол-во
        private DateTime datasert; //дата сертификата
        private bool is_selected;

        public string MNEMOCODE
        {
            get { return mnemocode; }
            set { mnemocode = value; }
        }

        public bool IS_SELECTED
        {
            get { return is_selected; }
            set { is_selected = value; }
        }

        public string NAME
        {
            get { return name; }
            set { name = value; }
        }

        public string CODE
        {
            get { return code; }
            set { code = value; }
        }

        public string PROD
        {
            get { return prod; }
            set { prod = value; }
        }

        public string POST
        {
            get { return post; }
            set { post = value; }
        }

        public string SER
        {
            get { return ser; }
            set { ser = value; }
        }

        public string NSERT
        {
            get { return nsert; }
            set { nsert = value; }
        }

        public string CENTRSERT
        {
            get { return centrsert; }
            set { centrsert = value; }
        }

        public decimal NUM
        {
            get { return num; }
            set { num = value; }
        }

        public DateTime DATASERT
        {
            get { return datasert; }
            set { datasert = value; }
        }

        public Guid ID_INVOICE_ITEM_GLOBAL
        {
            get { return id_invoice_item_global; }
            set { id_invoice_item_global = value; }
        }

        public void FromDataReader(SqlDataReader reader)
        {
            ID_INVOICE_ITEM_GLOBAL = Utils.GetGuid(reader["ID_INVOICE_ITEM_GLOBAL"]);
            MNEMOCODE = Utils.GetString(reader["MNEMOCODE"]);
            NAME = Utils.GetString(reader["GOODS_NAME"]);
            CODE = Utils.GetString(reader["CODE"]);
            PROD = Utils.GetString(reader["PRODUCER"]);
            POST = Utils.GetString(reader["SUPPLIER"]);
            SER = Utils.GetString(reader["SERIES"]);
            NSERT = Utils.GetString(reader["CERT_NUMBER"]);
            DATASERT = Utils.GetDate(reader["CERT_DATE"]);
            CENTRSERT = Utils.GetString(reader["CERT_CENTER"]);
            NUM = Utils.GetDecimal(reader["QUANTITY"]);
            IS_SELECTED = true;            
        }

        public void ToXml(XmlNode node)
        {
            Utils.AddNode(node, "ID_INVOICE_ITEM_GLOBAL", ID_INVOICE_ITEM_GLOBAL);
        }
    }
}
