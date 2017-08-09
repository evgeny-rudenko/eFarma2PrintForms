using System;
using System.Collections.Generic;
using System.Text;

namespace ImportNovgorod
{
    enum ImportFormat { None, GK, RN }

    class ImportFormatDescription
    {
        private ImportFormat importFormat;
        private string description;

        public ImportFormat ImportFormat
        {
            get { return importFormat; }
        }

        public string Description
        {
            get { return description; }
        }

        public ImportFormatDescription(ImportFormat importFormat)
        {
            this.importFormat = importFormat;
            switch (importFormat)
            {
                case ImportFormat.None:
                    description = "Нет файла договоров";
                    break;
                case ImportFormat.GK:
                    description = "Файл ГК (спецификаций контрактов)";
                    break;
                case ImportFormat.RN:
                    description = "Файл разнарядок (заявок)";
                    break;
            }
        }

        public override bool Equals(object obj)
        {
            ImportFormatDescription d = obj as ImportFormatDescription;
            if (d != null)
                return d.importFormat == this.importFormat;
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return ImportFormat.GetHashCode();
        }

        public override string ToString()
        {
            return description;
        }
    }
}
