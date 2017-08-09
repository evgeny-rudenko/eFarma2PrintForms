namespace ePlus.PriceList.ImportZakaz
{
    using ePlus.MetaData.Core;
    using System;
    using System.Xml;

    public class CONFIGURATION_IMPORT
    {
        private string ap_archive;
        private string ap_import;
        private string ap_unload_refus;
        private long id_configuration_import;
        private bool is_auto_bill;

        public void ToXml(XmlNode root)
        {
            if (this.ap_import != string.Empty)
            {
                Utils.AddNode(root, "AP_IMPORT", this.AP_IMPORT);
            }
            if (this.ap_archive != string.Empty)
            {
                Utils.AddNode(root, "AP_ARCHIVE", this.AP_ARCHIVE);
            }
            if (this.ap_unload_refus != string.Empty)
            {
                Utils.AddNode(root, "AP_UNLOAD_REFUS", this.AP_UNLOAD_REFUS);
            }
            Utils.AddNode(root, "IS_AUTO_BILL", this.IS_AUTO_BILL);
        }

        public string AP_ARCHIVE
        {
            get
            {
                return this.ap_archive;
            }
            set
            {
                this.ap_archive = value;
            }
        }

        public string AP_IMPORT
        {
            get
            {
                return this.ap_import;
            }
            set
            {
                this.ap_import = value;
            }
        }

        public string AP_UNLOAD_REFUS
        {
            get
            {
                return this.ap_unload_refus;
            }
            set
            {
                this.ap_unload_refus = value;
            }
        }

        public long ID_CONFIGURATION_IMPORT
        {
            get
            {
                return this.id_configuration_import;
            }
            set
            {
                this.id_configuration_import = value;
            }
        }

        public bool IS_AUTO_BILL
        {
            get
            {
                return this.is_auto_bill;
            }
            set
            {
                this.is_auto_bill = value;
            }
        }
    }
}

