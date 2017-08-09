namespace RCRImportOrdersExcel
{
    using ePlus.MetaData.Core;
    using System;
    using System.Data.OleDb;
    using System.Xml;

    public class ITEM
    {
        private long code_buyer;
        private string fileName;
        private Guid id_contracts_global;
        private Guid id_contracts_goods_global;
        private Guid id_goods_global;
        private Guid id_orders_item_global;
        private decimal quantity;
        private string store_mnemocode;

        public void FromReader(OleDbDataReader reader)
        {
        }

        public void ToXml(XmlNode node)
        {
            Utils.AddNode(node, "ID_ORDERS_ITEM_GLOBAL", Guid.NewGuid());
            Utils.AddNode(node, "STORE_MNEMOCODE", this.STORE_MNEMOCODE);
            Utils.AddNode(node, "ID_CONTRACTS_GLOBAL", this.ID_CONTRACTS_GLOBAL);
            Utils.AddNode(node, "ID_CONTRACTS_GOODS_GLOBAL", this.ID_CONTRACTS_GOODS_GLOBAL);
            Utils.AddNode(node, "CODE_BUYER", this.CODE_BUYER);
            Utils.AddNode(node, "QUANTITY", this.QUANTITY);
            Utils.AddNode(node, "ID_GOODS_GLOBAL", this.ID_GOODS_GLOBAL);
        }

        public long CODE_BUYER
        {
            get
            {
                return this.code_buyer;
            }
            set
            {
                this.code_buyer = value;
            }
        }

        public string FILE_NAME
        {
            get
            {
                return this.fileName;
            }
            set
            {
                this.fileName = value;
            }
        }

        public Guid ID_CONTRACTS_GLOBAL
        {
            get
            {
                return this.id_contracts_global;
            }
            set
            {
                this.id_contracts_global = value;
            }
        }

        public Guid ID_CONTRACTS_GOODS_GLOBAL
        {
            get
            {
                return this.id_contracts_goods_global;
            }
            set
            {
                this.id_contracts_goods_global = value;
            }
        }

        public Guid ID_GOODS_GLOBAL
        {
            get
            {
                return this.id_goods_global;
            }
            set
            {
                this.id_goods_global = value;
            }
        }

        public Guid ID_ORDERS_ITEM_GLOBAL
        {
            get
            {
                return this.id_orders_item_global;
            }
            set
            {
                this.id_orders_item_global = value;
            }
        }

        public decimal QUANTITY
        {
            get
            {
                return this.quantity;
            }
            set
            {
                this.quantity = value;
            }
        }

        public string STORE_MNEMOCODE
        {
            get
            {
                return this.store_mnemocode;
            }
            set
            {
                this.store_mnemocode = value;
            }
        }
    }
}

