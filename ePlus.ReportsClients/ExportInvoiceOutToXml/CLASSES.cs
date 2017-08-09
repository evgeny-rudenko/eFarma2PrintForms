using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using ePlus.MetaData.Core;

namespace ExportInvoiceOutToXml
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ToXml:Attribute
    {
      
      //public static string ToString(object value)
      //{
          
      //}  
    }
    
    public class DOC
    {
        private long fid;
        private string doc_name;
        private HEADER header;

        public HEADER Header
        {
            get { return header; }
            set { header = value; }
        }
        
        public long FID
        {
            get { return fid; }
            set { fid = value; }
        }
        
        public string DOC_NAME
        {
            get { return doc_name; }
            set { doc_name = value; }
        }

        public void FromReader(SqlDataReader dr)
        {
            FID = Utils.GetLong(dr["FID"]);
            DOC_NAME = Utils.GetString(dr["DOC_NAME"]);            
        }
    }
    public class HEADER
    {
        private List<ITEMS> items = new List<ITEMS>();
        private long fid;

        public List<ITEMS> ITEMS
        {
            get { return items; }
            set { items = value; }
        }

        private long sid;
        private long supplier_code;
        private long contractor_code;
        private decimal svat_supplier;
        private decimal sum_supplier;
        private decimal svat_retail;
        private decimal sum_retail;
        private string incoming_number;
        private DateTime incoming_date;
        private string incoming_bill_number;
        private DateTime incoming_bill_date;
        private string comment;

        public long FID
        {
            get { return fid; }
            set { fid = value; }
        }

        public long SID
        {
            get { return sid; }
            set { sid = value; }
        }

        [ToXml]
        public long SUPPLIER_CODE
        {
            get { return supplier_code; }
            set { supplier_code = value; }
        }
        [ToXml]
        public long CONTRACTOR_CODE
        {
            get { return contractor_code; }
            set { contractor_code = value; }
        }

        [ToXml]
        public decimal SVAT_SUPPLIER
        {
            get { return svat_supplier; }
            set { svat_supplier = value; }
        }

        [ToXml]
        public decimal SUM_SUPPLIER
        {
            get { return sum_supplier; }
            set { sum_supplier = value; }
        }

        [ToXml]
        public decimal SVAT_RETAIL
        {
            get { return svat_retail; }
            set { svat_retail = value; }
        }

        [ToXml]
        public decimal SUM_RETAIL
        {
            get { return sum_retail; }
            set { sum_retail = value; }
        }

        [ToXml]
        public string INCOMING_NUMBER
        {
            get { return incoming_number; }
            set { incoming_number = value; }
        }

        [ToXml]
        public DateTime INCOMING_DATE
        {
            get { return incoming_date; }
            set { incoming_date = value; }
        }

        [ToXml]
        public string INCOMING_BILL_NUMBER
        {
            get { return incoming_bill_number; }
            set { incoming_bill_number = value; }
        }

        [ToXml]
        public DateTime INCOMING_BILL_DATE
        {
            get { return incoming_bill_date; }
            set { incoming_bill_date = value; }
        }

        [ToXml]
        public string COMMENT
        {
            get { return comment; }
            set { comment = value; }
        }

        public void FromReader(SqlDataReader dr)
        {
            FID = Utils.GetLong(dr["FID"]);
            SID = Utils.GetLong(dr["SID"]);
            SUPPLIER_CODE = Utils.GetLong(dr["SUPPLIER_CODE"]);
            CONTRACTOR_CODE = Utils.GetLong(dr["CONTRACTOR_CODE"]);
            SVAT_SUPPLIER = Utils.GetDecimal(dr["SVAT_SUPPLIER"]);
            SUM_SUPPLIER = Utils.GetDecimal(dr["SUM_SUPPLIER"]);
            SVAT_RETAIL = Utils.GetDecimal(dr["SVAT_RETAIL"]);
            SUM_RETAIL = Utils.GetDecimal(dr["SUM_RETAIL"]);
            INCOMING_NUMBER = Utils.GetString(dr["INCOMING_NUMBER"]);
            INCOMING_BILL_NUMBER = Utils.GetString(dr["INCOMING_BILL_NUMBER"]);
            COMMENT = Utils.GetString(dr["COMMENT"]);
            INCOMING_DATE = Utils.GetDate(dr["INCOMING_DATE"]);
            INCOMING_BILL_DATE = Utils.GetDate(dr["INCOMING_BILL_DATE"]);
        }
    }

    //public class lala:ICustomDataServiceReader
    //public class HeaderLoader
    //{
    //    public static void Lalal()
    //    {
    //        using (ePlus.MetaData.Server.CustomDataService.ExecCustomDataServiceReader(())
    //    }
    //}

    public class ITEMS
    {
        private List<CERT> cert = new List<CERT>();
        private long sid;
        private long tid;
        private int numerator;
        private int denominator;
        private string unit_name;

        private string goods_code;
        private string goods;
        private string producer;
        private string  country;
        private bool important;
        private decimal register_price;
        private DateTime registration_date;
        
        private decimal quantity;
        private decimal producer_price;
        
        private decimal supplier_vat_per_unit;
        private decimal supplier_adprice;
        private decimal supplier_price;
        private decimal supplier_vat;
        private decimal supplier_price_vat;
        private decimal supplier_sum;
        private decimal supplier_vat_sum;
        private decimal supplier_sum_vat;
        
        private decimal retail_adprice;
        private decimal retail_price;
        private decimal retail_vat;
        private decimal retail_price_vat;
        private decimal retail_sum;
        private decimal retail_vat_sum;
        private decimal retail_sum_vat;
        
        private string series_number;
        private DateTime best_before;
        private string gtd_number;
        private string bar_code;
        private string box;

        [ToXml]
        public string BOX
        {
            get { return box; }
            set { box = value; }
        }

        public long SID
        {
            get { return sid; }
            set { sid = value; }
        }
        
        public long TID
        {
            get { return tid; }
            set { tid = value; }
        }

        public List<CERT> CERT
        {
            get { return cert; }
            set { cert = value; }
        }


        [ToXml]
        public int NUMERATOR
        {
            get { return numerator; }
            set { numerator = value; }
        }
        [ToXml]
        public int DENOMINATOR
        {
            get { return denominator; }
            set { denominator = value; }
        }
        [ToXml]
        public string UNIT_NAME
        {
            get { return unit_name; }
            set { unit_name = value; }
        }
        [ToXml]
        public string GOODS_CODE
        {
            get { return goods_code; }
            set { goods_code = value; }
        }
        [ToXml]
        public string GOODS
        {
            get { return goods; }
            set { goods = value; }
        }
        [ToXml]
        public string PRODUCER
        {
            get { return producer; }
            set { producer = value; }
        }
        [ToXml]
        public string COUNTRY
        {
            get { return country; }
            set { country = value; }
        }
        [ToXml]
        public bool IMPORTANT
        {
            get { return important; }
            set { important = value; }
        }
        [ToXml]
        public decimal REGISTER_PRICE
        {
            get { return register_price; }
            set { register_price = value; }
        }
        [ToXml]
        public DateTime REGISTRATION_DATE
        {
            get { return registration_date; }
            set { registration_date = value; }
        }
        [ToXml]
        public decimal QUANTITY
        {
            get { return quantity; }
            set { quantity = value; }
        }
        [ToXml]
        public decimal PRODUCER_PRICE
        {
            get { return producer_price; }
            set { producer_price = value; }
        }
        [ToXml]
        public decimal SUPPLIER_VAT_PER_UNIT
        {
            get { return supplier_vat_per_unit; }
            set { supplier_vat_per_unit = value; }
        }
        [ToXml]
        public decimal SUPPLIER_ADPRICE
        {
            get { return supplier_adprice; }
            set { supplier_adprice = value; }
        }
        [ToXml]
        public decimal SUPPLIER_PRICE
        {
            get { return supplier_price; }
            set { supplier_price = value; }
        }
        [ToXml]
        public decimal SUPPLIER_VAT
        {
            get { return supplier_vat; }
            set { supplier_vat = value; }
        }
        [ToXml]
        public decimal SUPPLIER_PRICE_VAT
        {
            get { return supplier_price_vat; }
            set { supplier_price_vat = value; }
        }
        [ToXml]
        public decimal SUPPLIER_SUM
        {
            get { return supplier_sum; }
            set { supplier_sum = value; }
        }
        [ToXml]
        public decimal SUPPLIER_VAT_SUM
        {
            get { return supplier_vat_sum; }
            set { supplier_vat_sum = value; }
        }
        [ToXml]
        public decimal SUPPLIER_SUM_VAT
        {
            get { return supplier_sum_vat; }
            set { supplier_sum_vat = value; }
        }
        [ToXml]
        public decimal RETAIL_ADPRICE
        {
            get { return retail_adprice; }
            set { retail_adprice = value; }
        }
        [ToXml]
        public decimal RETAIL_PRICE
        {
            get { return retail_price; }
            set { retail_price = value; }
        }
        [ToXml]
        public decimal RETAIL_VAT
        {
            get { return retail_vat; }
            set { retail_vat = value; }
        }
        [ToXml]
        public decimal RETAIL_PRICE_VAT
        {
            get { return retail_price_vat; }
            set { retail_price_vat = value; }
        }
        [ToXml]
        public decimal RETAIL_SUM
        {
            get { return retail_sum; }
            set { retail_sum = value; }
        }
        [ToXml]
        public decimal RETAIL_VAT_SUM
        {
            get { return retail_vat_sum; }
            set { retail_vat_sum = value; }
        }
        [ToXml]
        public decimal RETAIL_SUM_VAT
        {
            get { return retail_sum_vat; }
            set { retail_sum_vat = value; }
        }
        [ToXml]
        public string SERIES_NUMBER
        {
            get { return series_number; }
            set { series_number = value; }
        }
        [ToXml]
        public DateTime BEST_BEFORE
        {
            get { return best_before; }
            set { best_before = value; }
        }
        [ToXml]
        public string GTD_NUMBER
        {
            get { return gtd_number; }
            set { gtd_number = value; }
        }
        [ToXml]
        public string BAR_CODE
        {
            get { return bar_code; }
            set { bar_code = value; }
        }

        public void FromReader(SqlDataReader dr)
        {
            TID = Utils.GetLong(dr["TID"]);
            SID = Utils.GetLong(dr["SID"]);
            NUMERATOR = Utils.GetInt(dr["NUMERATOR"]);
            DENOMINATOR = Utils.GetInt(dr["DENOMINATOR"]);
            UNIT_NAME = Utils.GetString(dr["UNIT_NAME"]);
            GOODS_CODE = Utils.GetString(dr["GOODS_CODE"]);
            GOODS = Utils.GetString(dr["GOODS"]);
            PRODUCER = Utils.GetString(dr["PRODUCER"]);
            COUNTRY = Utils.GetString(dr["COUNTRY"]);
            UNIT_NAME = Utils.GetString(dr["UNIT_NAME"]);
            IMPORTANT = Utils.GetBool(dr["IMPORTANT"]);

            REGISTER_PRICE = Utils.GetDecimal(dr["REGISTER_PRICE"]);
            QUANTITY = Utils.GetDecimal(dr["QUANTITY"]);
            PRODUCER_PRICE = Utils.GetDecimal(dr["PRODUCER_PRICE"]);
            SUPPLIER_VAT_PER_UNIT = Utils.GetDecimal(dr["SUPPLIER_VAT_PER_UNIT"]);
            SUPPLIER_ADPRICE = Utils.GetDecimal(dr["SUPPLIER_ADPRICE"]);
            REGISTRATION_DATE = Utils.GetDate(dr["REGISTRATION_DATE"]);
            SUPPLIER_PRICE = Utils.GetDecimal(dr["SUPPLIER_PRICE"]);
            SUPPLIER_VAT = Utils.GetDecimal(dr["SUPPLIER_VAT"]);
            SUPPLIER_PRICE_VAT = Utils.GetDecimal(dr["SUPPLIER_PRICE_VAT"]);
            SUPPLIER_SUM = Utils.GetDecimal(dr["SUPPLIER_SUM"]);
            SUPPLIER_VAT_SUM = Utils.GetDecimal(dr["SUPPLIER_VAT_SUM"]);

            SUPPLIER_SUM_VAT = Utils.GetDecimal(dr["SUPPLIER_SUM_VAT"]);
            RETAIL_ADPRICE = Utils.GetDecimal(dr["RETAIL_ADPRICE"]);
            RETAIL_PRICE = Utils.GetDecimal(dr["RETAIL_PRICE"]);
            RETAIL_VAT = Utils.GetDecimal(dr["RETAIL_VAT"]);
 
            RETAIL_PRICE_VAT = Utils.GetDecimal(dr["RETAIL_PRICE_VAT"]);
            RETAIL_SUM = Utils.GetDecimal(dr["RETAIL_SUM"]);
            RETAIL_VAT_SUM = Utils.GetDecimal(dr["RETAIL_VAT_SUM"]);
            RETAIL_SUM_VAT = Utils.GetDecimal(dr["RETAIL_SUM_VAT"]);


            SERIES_NUMBER = Utils.GetString(dr["SERIES_NUMBER"]);
            BEST_BEFORE = Utils.GetDate(dr["BEST_BEFORE"]);
            GTD_NUMBER = Utils.GetString(dr["GTD_NUMBER"]);
            BAR_CODE = Utils.GetString(dr["BAR_CODE"]);
            BOX = Utils.GetString(dr["BOX"]);
        }
    }

    public class CERT
    {
        private long tid;
        private string cert_number;
        private string cert_organ;
        private DateTime cert_date;
        private DateTime cert_end_date;

        public long TID
        {
            get { return tid; }
            set { tid = value; }
        }
        [ToXml]
        public string CERT_NUMBER
        {
            get { return cert_number; }
            set { cert_number = value; }
        }
        [ToXml]
        public string CERT_ORGAN
        {
            get { return cert_organ; }
            set { cert_organ = value; }
        }
        [ToXml]
        public DateTime CERT_DATE
        {
            get { return cert_date; }
            set { cert_date = value; }
        }
        [ToXml]
        public DateTime CERT_END_DATE
        {
            get { return cert_end_date; }
            set { cert_end_date = value; }
        }

        public void FromReader(SqlDataReader dr)
        {
            TID = Utils.GetLong(dr["TID"]);
            CERT_NUMBER = Utils.GetString(dr["CERT_NUMBER"]);
            CERT_ORGAN = Utils.GetString(dr["CERT_ORGAN"]);
            CERT_DATE = Utils.GetDate(dr["CERT_DATE"]);
            CERT_END_DATE = Utils.GetDate(dr["CERT_END_DATE"]);            
        }
    }
}


