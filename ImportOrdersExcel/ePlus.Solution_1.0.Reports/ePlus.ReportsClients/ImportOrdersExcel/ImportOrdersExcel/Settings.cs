namespace RCRImportOrdersExcel
{
    using System;

    public class Settings
    {
        private int beginRow;
        private string buyerCol;
        private string contractCol;
        private string quantityCol;
        private string storeCol;

        public int BEGIN_ROW
        {
            get
            {
                return this.beginRow;
            }
            set
            {
                this.beginRow = value;
            }
        }

        public string BUYER_COL
        {
            get
            {
                return this.buyerCol;
            }
            set
            {
                this.buyerCol = value;
            }
        }

        public string CONTRACT_COL
        {
            get
            {
                return this.contractCol;
            }
            set
            {
                this.contractCol = value;
            }
        }

        public string QUANTITY_COL
        {
            get
            {
                return this.quantityCol;
            }
            set
            {
                this.quantityCol = value;
            }
        }

        public string STORE_COL
        {
            get
            {
                return this.storeCol;
            }
            set
            {
                this.storeCol = value;
            }
        }
    }
}

