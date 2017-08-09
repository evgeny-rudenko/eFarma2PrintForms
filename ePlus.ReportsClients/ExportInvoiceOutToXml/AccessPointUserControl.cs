using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ePlus.MetaData.Core;

namespace ExportInvoiceOutToXml
{
    public partial class AccessPointUserControl : UserControl, IExternalDocumentPrintFormParamsControl
    {
        private ExportForm _p;
        public AccessPointUserControl()
        {
            InitializeComponent();
        }

        public void Object2Control(IExternalDocumentPrintFormParams param)
        {
            _p = (ExportForm) param;
            ucAccessPoint.Code = _p.AccessPointMnemocode;
            
        }

        public void Control2Object()
        {
            _p.AccessPointMnemocode = ucAccessPoint.Code;
        }

        public void Clear()
        {
            //ucAccessPoint.Id = 0;
        }

        public Control Control
        {
            get { return this; }
        }
    }
}
