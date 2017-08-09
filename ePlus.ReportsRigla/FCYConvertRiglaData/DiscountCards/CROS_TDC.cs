using System;
using System.Collections.Generic;
using System.Text;
using ePlus.MetaData.Core;

namespace RCYConvertRiglaData.DiscountCards
{
    class CROS_TDC: IObject, ISupportsErrorState
    {
        #region IObject Members

        private Guid fId = Guid.NewGuid();

        public Guid ID_GLOBAL
        {
            get { return fId; }
        }

        #endregion

        #region ISupportsErrorState Members

        private List<RowError> fErrors = new List<RowError>();

        public List<RowError> Errors
        {
            get { return fErrors; }
        }

        #endregion

        #region DB FIELDS
        private double fTIPDKSTU = 0;
        [Required]
        [Format]
        public double TIPDKSTU
        {
            get { return fTIPDKSTU; }
            set { fTIPDKSTU = value; }
        }

        private string fTIPDKEF = null;
        [Required]
        [Format]
        public string TIPDKEF
        {
            get { return fTIPDKEF; }
            set { fTIPDKEF = value; }
        }

        #endregion

        internal void ToXml(System.Xml.XmlNode node)
        {
          if (!RequiredAttribute.IsEmpty(TIPDKSTU))  
            Utils.AddNode(node, "TIPDKSTU", TIPDKSTU.ToString("#0"));
          if (!RequiredAttribute.IsEmpty(TIPDKEF))  
            Utils.AddNode(node, "TIPDKEF", TIPDKEF);
        }
    }
}
