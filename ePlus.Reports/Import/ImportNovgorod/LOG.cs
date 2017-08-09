using System;
using System.Collections.Generic;
using System.Text;

namespace ImportNovgorod
{
    public class LOG
    {
        private Guid id_global;
        private string reason;

        public Guid ID_GLOBAL
        {
            get { return id_global; }
            set { id_global = value; }
        }

        public string REASON
        {
            get { return reason; }
            set { reason = value; }
        }
    }
}
