using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using ePlus.MetaData.Core;

namespace LoadingRegCert
{
    public class INVOICE 
    {
        private Guid id_invoice_global;
        private string mnemocode;

        public Guid ID_INVOICE_GLOBAL
        {
            get { return id_invoice_global; }
            set { id_invoice_global = value; }
        }

        public string MNEMOCODE
        {
            get { return mnemocode; }
            set { mnemocode = value; }
        }

        public void ToXml(XmlNode node)
        {
            Utils.AddNode(node, "ID_INVOICE_GLOBAL", ID_INVOICE_GLOBAL);
        }
    }
}
