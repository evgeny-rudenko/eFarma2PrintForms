using System;
using System.Collections.Generic;
using System.Text;
using ePlus.MetaData.Core;
using System.Xml;

namespace ImportNovgorod
{
    class CONTRACTS_ITEM : IObject, ISupportsErrorState
    {
        private Guid id_global = Guid.NewGuid();   // для уменьшения трафика с SQL сервера    
        private string name;
        private string notd;
        private string post;
        private string k39_1;
        private double kol39_1;
        private double z9c_1;
        private double fin;
        private string co9_1;
        private string nnom;
        private string izg;

        public Guid ID_GLOBAL
        {
            get { return id_global; }
            set { id_global = value; }
        }

        [Format]
        public string NAME
        {
            get { return name; }
            set { name = value; }
        }

        [Required]
        [Format]
        public string NOTD
        {
            get { return notd; }
            set { notd = value; }
        }

        [Required]
        [Format]
        public string POST
        {
            get { return post; }
            set { post = value; }
        }

        [Required]
        [Format]
        public string K39_1
        {
            get { return k39_1; }
            set { k39_1 = value; }
        }

        [Required]
        public double KOL39_1
        {
            get { return kol39_1; }
            set { kol39_1 = value; }
        }

        [Required]
        public double Z9C_1
        {
            get { return z9c_1; }
            set { z9c_1 = value; }
        }

        [Required]
        public double FIN
        {
            get { return fin; }
            set { fin = value; }
        }

        public string CO9_1
        {
            get { return co9_1; }
            set { co9_1 = value; }
        }

        public string NNOM
        {
            get { return nnom; }
            set { nnom = value; }
        }

        public string IZG
        {
            get { return izg; }
            set { izg = value; }
        }

        public void ToXml(XmlNode node)
        {
            if (!RequiredAttribute.IsEmpty(ID_GLOBAL))
                Utils.AddNode(node, "ID_GLOBAL", ID_GLOBAL);   // для уменьшения трафика с SQL сервера
            if (!RequiredAttribute.IsEmpty(NAME))
                Utils.AddNode(node, "NAME", NAME);
            if (!RequiredAttribute.IsEmpty(NOTD))
                Utils.AddNode(node, "NOTD", Utils.GetString(NOTD));
            if (!RequiredAttribute.IsEmpty(POST))
                Utils.AddNode(node, "POST", Utils.GetString(POST));
            if (!RequiredAttribute.IsEmpty(K39_1))
                Utils.AddNode(node, "K39_1", Utils.GetString(K39_1));
            if (!RequiredAttribute.IsEmpty(KOL39_1))
                Utils.AddNode(node, "KOL39_1", Utils.GetDecimal(KOL39_1));
            if (!RequiredAttribute.IsEmpty(Z9C_1))
                Utils.AddNode(node, "Z9C_1", Utils.GetDecimal(Z9C_1));
            if (!RequiredAttribute.IsEmpty(FIN))
                Utils.AddNode(node, "FIN", Utils.GetDecimal(FIN));
            if (!RequiredAttribute.IsEmpty(CO9_1))
                Utils.AddNode(node, "CO9_1", Utils.GetString(CO9_1));
            if (!RequiredAttribute.IsEmpty(NNOM))
                Utils.AddNode(node, "NNOM", Utils.GetString(NNOM));
            if (!RequiredAttribute.IsEmpty(IZG))
                Utils.AddNode(node, "IZG", Utils.GetString(IZG));
        }

        #region Errors

        private List<RowError> errors = new List<RowError>();

        public List<RowError> Errors
        {
            get { return errors; }
        }

        public void SetError(string errorText, RowErrorLevel errorLevel)
        {
            errors.Add(new RowError(errorLevel, errorText));
        }

        #endregion
    }
}
