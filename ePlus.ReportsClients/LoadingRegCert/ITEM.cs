using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Xml;
using ePlus.MetaData.Core;
using ePlus.CommonEx.DataAccess;

namespace LoadingRegCert
{
    public class ITEM
    {
        private string file_name;
        private string code;
        private string ser;
        private decimal num;
        //private string post;
        private string numreestr;
        private string organ;

        public string ORGAN
        {
            get { return organ; }
            set { organ = value; }
        }

        public string FILE_NAME
        {
            get { return file_name; }
            set { file_name = value; }
        }

        public string CODE
        {
            get { return code; }
            set { code = value; }
        }

        public string SER
        {
            get { return ser; }
            set { ser = value; }
        }

        public decimal NUM
        {
            get { return num; }
            set { num = value; }
        }

        //public string POST
        //{
        //    get { return post; }
        //    set { post = value; }
        //}

        public string NUMREESTR
        {
            get { return numreestr; }
            set { numreestr = value; }
        }

        public void ToXml(XmlNode node)
        {
//            Utils.AddNode(node, "FILE_NAME", FILE_NAME);
            Utils.AddNode(node, "CODE", CODE);
            Utils.AddNode(node, "SER", SER);
            Utils.AddNode(node, "NUM", NUM);
            //Utils.AddNode(node, "POST", POST);
            Utils.AddNode(node, "NUMREESTR", NUMREESTR);
            Utils.AddNode(node, "ORGAN", ORGAN);
        }

        public void FromReader(DbDataReader reader)
        {
            //ORGAN = Utils.GetString(reader["ORGAN"]);
            //FILE_NAME = Utils.GetString(reader["FILE_NAME"]);
            CODE = Utils.GetString(reader["CODE"]);
            SER = Utils.GetString(reader["SER"]);
            NUM = Utils.GetDecimal(reader["NUM"]);
            //POST = Utils.GetString(reader["POST"]);
            NUMREESTR = Utils.GetString(reader["NUMREESTR"]);
        }
    }
}

