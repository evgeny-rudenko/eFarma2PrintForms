using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using ePlus.MetaData.Core;

namespace ePlus.PriceList.ImportZakaz
{
    public class CONFIGURATION_IMPORT
    {
        private long id_configuration_import;
        private string ap_import;
        private string ap_archive;
        private string ap_unload_refus;
        private bool is_auto_bill;

        public string AP_IMPORT
        {
            get { return ap_import; }
            set { ap_import = value; }
        }        

        public string AP_ARCHIVE
        {
            get { return ap_archive; }
            set { ap_archive = value; }
        }

        public string AP_UNLOAD_REFUS
        {
            get { return ap_unload_refus; }
            set { ap_unload_refus = value; }
        }
        
        public bool IS_AUTO_BILL
        {
            get { return is_auto_bill; }
            set { is_auto_bill = value; }
        }

        public long ID_CONFIGURATION_IMPORT
        {
            get { return id_configuration_import; }
            set { id_configuration_import = value; }
        }

        public void ToXml(XmlNode root)
        {            
            if (ap_import != string.Empty)
                Utils.AddNode(root, "AP_IMPORT",AP_IMPORT);
            if (ap_archive != string.Empty)
                Utils.AddNode(root, "AP_ARCHIVE",AP_ARCHIVE);
            if (ap_unload_refus != string.Empty)
                Utils.AddNode(root, "AP_UNLOAD_REFUS",AP_UNLOAD_REFUS);
            Utils.AddNode(root, "IS_AUTO_BILL", IS_AUTO_BILL);
        }        
    }
}
