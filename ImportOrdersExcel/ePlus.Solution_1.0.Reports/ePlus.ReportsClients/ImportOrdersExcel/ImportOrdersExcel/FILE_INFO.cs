namespace RCRImportOrdersExcel
{
    using System;

    public class FILE_INFO
    {
        private string file_name;
        private string full_path;

        public override string ToString()
        {
            return this.FILE_NAME;
        }

        public string FILE_NAME
        {
            get
            {
                return this.file_name;
            }
            set
            {
                this.file_name = value;
            }
        }

        public string FULL_PATH
        {
            get
            {
                return this.full_path;
            }
            set
            {
                this.full_path = value;
            }
        }
    }
}

