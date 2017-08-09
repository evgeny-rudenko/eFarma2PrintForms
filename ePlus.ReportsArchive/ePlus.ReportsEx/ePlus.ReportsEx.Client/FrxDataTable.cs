using System;
using System.Data;
using System.ComponentModel;
using FastReport;

namespace ePlus.ReportsEx
{
	public class FrxDataTable : DataTable
    {
        #region Fields

        private int nItem = 0;
        private DataTable dataTable = null;
		private TfrxUserDataSetClass frxDataSet;
        public event FrxOnFirst FrxEventOnFirst;
        public event FrxOnNext FrxEventOnNext;
        public event FrxOnPrior FrxEventOnPrior;

        #endregion

        #region Initialize

        public FrxDataTable(string name)
        {
            Initialize(name);
        }
        public FrxDataTable(DataTable tab)
        {
            Initialize(tab.TableName);
            string fieldNames = "";
            foreach (DataColumn col in tab.Columns)
            {
                fieldNames += col.Caption + "\n";
            }
            frxDataSet.Fields = fieldNames;
            dataTable = tab;
        }
        private void Initialize(string name)
        {
            frxDataSet = new TfrxUserDataSetClass();
            frxDataSet.OnCheckEOF += new IfrxUserDataSetEventDispatcher_OnCheckEOFEventHandler(OnCheckEOFEventHandler);
            frxDataSet.OnGetValue += new IfrxUserDataSetEventDispatcher_OnGetValueEventHandler(OnGetValueHandler);
            frxDataSet.OnFirst += new IfrxUserDataSetEventDispatcher_OnFirstEventHandler(OnFirstEventHandler);
            frxDataSet.OnNext += new IfrxUserDataSetEventDispatcher_OnNextEventHandler(OnNextEventHandler);
            frxDataSet.OnPrior += new IfrxUserDataSetEventDispatcher_OnPriorEventHandler(OnPriorEventHandler);
            frxDataSet.Name = name;
            Columns.CollectionChanged += new CollectionChangeEventHandler(ColumnsCollection_Changed);
        }

        #endregion

        #region Properties

        public new string TableName 
		{
			get { return frxDataSet.Name; }
		}

		public IfrxDataSet FrxTable
		{
            get { return frxDataSet as IfrxDataSet; }
        }

        #endregion

        #region Methods

        public void AssignToDataBand(string BandName, TfrxReportClass report)
		{
			IfrxComponent frxComponent = ((IfrxComponent)report).FindObject(BandName);
            ((IfrxDataBand)frxComponent).DataSet = (IfrxDataSet)frxDataSet;
		}

		public void AssignToReport(bool Enable, TfrxReportClass report)
		{
            report.SelectDataset(Enable, frxDataSet as IfrxDataSet);
        }

        #endregion

        #region Events

        private void OnFirstEventHandler() 
		{
			bool end;
			nItem = 0;
			OnCheckEOFEventHandler(out end);
			if (!end && FrxEventOnFirst != null) FrxEventOnFirst();
		}

		private void OnNextEventHandler()
		{
            bool end;
			nItem++;
            OnCheckEOFEventHandler(out end);
            if (!end && FrxEventOnNext != null) FrxEventOnNext();
		}

		private void OnPriorEventHandler()
		{
            bool end;
			nItem--;
            OnCheckEOFEventHandler(out end);
            if (!end && FrxEventOnPrior != null) FrxEventOnPrior();
		}

		private void OnCheckEOFEventHandler(out bool end)
		{
			if (dataTable == null) 
			{
				end = (nItem >= Rows.Count); 
			}
			else
			{
				end = (nItem >= dataTable.Rows.Count);
			}
		}

		public void OnGetValueHandler(object name, out object value)
		{
			if (dataTable == null) 
			{
				value = Rows[nItem][name.ToString()];
			}
			else
			{
				value = dataTable.Rows[nItem][name.ToString()];
			}
			// FastReport does not know about System.Decimal object type
			// so convert it to Integer
            if (value is Decimal)
            {
                value = Decimal.ToSingle((Decimal)value);
            }
		}

		private void ColumnsCollection_Changed(object sender, CollectionChangeEventArgs e)
		{
			DataColumnCollection cols = (DataColumnCollection)sender;
			string fieldNames = "";
			foreach (DataColumn col in cols)
			{
			    fieldNames += col.Caption + "\n";
			}
			frxDataSet.Fields = fieldNames;
        }

        #endregion
    }

    #region Delegates

    public delegate void FrxOnFirst();
    public delegate void FrxOnNext();
    public delegate void FrxOnPrior();

    #endregion
}

