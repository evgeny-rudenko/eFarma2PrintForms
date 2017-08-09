// Type: FCChInvoiceOutInvoice_Rigla.InvoiceOutInvoice_DS
// Assembly: FCChInvoiceOutInvoice_Rigla_8, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 508D15D3-CFC0-430C-86B9-F207DB5E1844
// Assembly location: D:\Work\eFarma\Rigla\reports 516.4\FCChInvoiceOutInvoice_Rigla_8.dll

using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FCChInvoiceOutInvoice_Rigla
{
  [ToolboxItem(true)]
  [XmlSchemaProvider("GetTypedDataSetSchema")]
  [XmlRoot("InvoiceOutInvoice_DS")]
  [HelpKeyword("vs.data.DataSet")]
  [DesignerCategory("code")]
  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
  [Serializable]
  public class InvoiceOutInvoice_DS : DataSet
  {
    private SchemaSerializationMode _schemaSerializationMode = SchemaSerializationMode.IncludeSchema;
    private InvoiceOutInvoice_DS.Table0DataTable tableTable0;
    private InvoiceOutInvoice_DS.Table1DataTable tableTable1;
    private InvoiceOutInvoice_DS.Table2DataTable tableTable2;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [DebuggerNonUserCode]
    [Browsable(false)]
    public InvoiceOutInvoice_DS.Table0DataTable Table0
    {
      get
      {
        return this.tableTable0;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [DebuggerNonUserCode]
    public InvoiceOutInvoice_DS.Table1DataTable Table1
    {
      get
      {
        return this.tableTable1;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(false)]
    [DebuggerNonUserCode]
    public InvoiceOutInvoice_DS.Table2DataTable Table2
    {
      get
      {
        return this.tableTable2;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    [DebuggerNonUserCode]
    public override SchemaSerializationMode SchemaSerializationMode
    {
      get
      {
        return this._schemaSerializationMode;
      }
      set
      {
        this._schemaSerializationMode = value;
      }
    }

    [DebuggerNonUserCode]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new DataTableCollection Tables
    {
      get
      {
        return base.Tables;
      }
    }

    [DebuggerNonUserCode]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new DataRelationCollection Relations
    {
      get
      {
        return base.Relations;
      }
    }

    [DebuggerNonUserCode]
    public InvoiceOutInvoice_DS()
    {
      this.BeginInit();
      this.InitClass();
      CollectionChangeEventHandler changeEventHandler = new CollectionChangeEventHandler(this.SchemaChanged);
      base.Tables.CollectionChanged += changeEventHandler;
      base.Relations.CollectionChanged += changeEventHandler;
      this.EndInit();
    }

    [DebuggerNonUserCode]
    protected InvoiceOutInvoice_DS(SerializationInfo info, StreamingContext context)
      : base(info, context, false)
    {
      if (this.IsBinarySerialized(info, context))
      {
        this.InitVars(false);
        CollectionChangeEventHandler changeEventHandler = new CollectionChangeEventHandler(this.SchemaChanged);
        this.Tables.CollectionChanged += changeEventHandler;
        this.Relations.CollectionChanged += changeEventHandler;
      }
      else
      {
        string s = (string) info.GetValue("XmlSchema", typeof (string));
        if (this.DetermineSchemaSerializationMode(info, context) == SchemaSerializationMode.IncludeSchema)
        {
          DataSet dataSet = new DataSet();
          dataSet.ReadXmlSchema((XmlReader) new XmlTextReader((TextReader) new StringReader(s)));
          if (dataSet.Tables["Table0"] != null)
            base.Tables.Add((DataTable) new InvoiceOutInvoice_DS.Table0DataTable(dataSet.Tables["Table0"]));
          if (dataSet.Tables["Table1"] != null)
            base.Tables.Add((DataTable) new InvoiceOutInvoice_DS.Table1DataTable(dataSet.Tables["Table1"]));
          if (dataSet.Tables["Table2"] != null)
            base.Tables.Add((DataTable) new InvoiceOutInvoice_DS.Table2DataTable(dataSet.Tables["Table2"]));
          this.DataSetName = dataSet.DataSetName;
          this.Prefix = dataSet.Prefix;
          this.Namespace = dataSet.Namespace;
          this.Locale = dataSet.Locale;
          this.CaseSensitive = dataSet.CaseSensitive;
          this.EnforceConstraints = dataSet.EnforceConstraints;
          this.Merge(dataSet, false, MissingSchemaAction.Add);
          this.InitVars();
        }
        else
          this.ReadXmlSchema((XmlReader) new XmlTextReader((TextReader) new StringReader(s)));
        this.GetSerializationData(info, context);
        CollectionChangeEventHandler changeEventHandler = new CollectionChangeEventHandler(this.SchemaChanged);
        base.Tables.CollectionChanged += changeEventHandler;
        this.Relations.CollectionChanged += changeEventHandler;
      }
    }

    [DebuggerNonUserCode]
    protected override void InitializeDerivedDataSet()
    {
      this.BeginInit();
      this.InitClass();
      this.EndInit();
    }

    [DebuggerNonUserCode]
    public override DataSet Clone()
    {
      InvoiceOutInvoice_DS invoiceOutInvoiceDs = (InvoiceOutInvoice_DS) base.Clone();
      invoiceOutInvoiceDs.InitVars();
      invoiceOutInvoiceDs.SchemaSerializationMode = this.SchemaSerializationMode;
      return (DataSet) invoiceOutInvoiceDs;
    }

    [DebuggerNonUserCode]
    protected override bool ShouldSerializeTables()
    {
      return false;
    }

    [DebuggerNonUserCode]
    protected override bool ShouldSerializeRelations()
    {
      return false;
    }

    [DebuggerNonUserCode]
    protected override void ReadXmlSerializable(XmlReader reader)
    {
      if (this.DetermineSchemaSerializationMode(reader) == SchemaSerializationMode.IncludeSchema)
      {
        this.Reset();
        DataSet dataSet = new DataSet();
        int num = (int) dataSet.ReadXml(reader);
        if (dataSet.Tables["Table0"] != null)
          base.Tables.Add((DataTable) new InvoiceOutInvoice_DS.Table0DataTable(dataSet.Tables["Table0"]));
        if (dataSet.Tables["Table1"] != null)
          base.Tables.Add((DataTable) new InvoiceOutInvoice_DS.Table1DataTable(dataSet.Tables["Table1"]));
        if (dataSet.Tables["Table2"] != null)
          base.Tables.Add((DataTable) new InvoiceOutInvoice_DS.Table2DataTable(dataSet.Tables["Table2"]));
        this.DataSetName = dataSet.DataSetName;
        this.Prefix = dataSet.Prefix;
        this.Namespace = dataSet.Namespace;
        this.Locale = dataSet.Locale;
        this.CaseSensitive = dataSet.CaseSensitive;
        this.EnforceConstraints = dataSet.EnforceConstraints;
        this.Merge(dataSet, false, MissingSchemaAction.Add);
        this.InitVars();
      }
      else
      {
        int num = (int) this.ReadXml(reader);
        this.InitVars();
      }
    }

    [DebuggerNonUserCode]
    protected override XmlSchema GetSchemaSerializable()
    {
      MemoryStream memoryStream = new MemoryStream();
      this.WriteXmlSchema((XmlWriter) new XmlTextWriter((Stream) memoryStream, (Encoding) null));
      memoryStream.Position = 0L;
      return XmlSchema.Read((XmlReader) new XmlTextReader((Stream) memoryStream), (ValidationEventHandler) null);
    }

    [DebuggerNonUserCode]
    internal void InitVars()
    {
      this.InitVars(true);
    }

    [DebuggerNonUserCode]
    internal void InitVars(bool initTable)
    {
      this.tableTable0 = (InvoiceOutInvoice_DS.Table0DataTable) base.Tables["Table0"];
      if (initTable && this.tableTable0 != null)
        this.tableTable0.InitVars();
      this.tableTable1 = (InvoiceOutInvoice_DS.Table1DataTable) base.Tables["Table1"];
      if (initTable && this.tableTable1 != null)
        this.tableTable1.InitVars();
      this.tableTable2 = (InvoiceOutInvoice_DS.Table2DataTable) base.Tables["Table2"];
      if (!initTable || this.tableTable2 == null)
        return;
      this.tableTable2.InitVars();
    }

    [DebuggerNonUserCode]
    private void InitClass()
    {
      this.DataSetName = "InvoiceOutInvoice_DS";
      this.Prefix = "";
      this.Namespace = "http://tempuri.org/InvoiceOutInvoice_DS.xsd";
      this.EnforceConstraints = true;
      this.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
      this.tableTable0 = new InvoiceOutInvoice_DS.Table0DataTable();
      base.Tables.Add((DataTable) this.tableTable0);
      this.tableTable1 = new InvoiceOutInvoice_DS.Table1DataTable();
      base.Tables.Add((DataTable) this.tableTable1);
      this.tableTable2 = new InvoiceOutInvoice_DS.Table2DataTable();
      base.Tables.Add((DataTable) this.tableTable2);
    }

    [DebuggerNonUserCode]
    private bool ShouldSerializeTable0()
    {
      return false;
    }

    [DebuggerNonUserCode]
    private bool ShouldSerializeTable1()
    {
      return false;
    }

    [DebuggerNonUserCode]
    private bool ShouldSerializeTable2()
    {
      return false;
    }

    [DebuggerNonUserCode]
    private void SchemaChanged(object sender, CollectionChangeEventArgs e)
    {
      if (e.Action != CollectionChangeAction.Remove)
        return;
      this.InitVars();
    }

    [DebuggerNonUserCode]
    public static XmlSchemaComplexType GetTypedDataSetSchema(XmlSchemaSet xs)
    {
      InvoiceOutInvoice_DS invoiceOutInvoiceDs = new InvoiceOutInvoice_DS();
      XmlSchemaComplexType schemaComplexType = new XmlSchemaComplexType();
      XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
      xmlSchemaSequence.Items.Add((XmlSchemaObject) new XmlSchemaAny()
      {
        Namespace = invoiceOutInvoiceDs.Namespace
      });
      schemaComplexType.Particle = (XmlSchemaParticle) xmlSchemaSequence;
      XmlSchema schemaSerializable = invoiceOutInvoiceDs.GetSchemaSerializable();
      if (xs.Contains(schemaSerializable.TargetNamespace))
      {
        MemoryStream memoryStream1 = new MemoryStream();
        MemoryStream memoryStream2 = new MemoryStream();
        try
        {
          schemaSerializable.Write((Stream) memoryStream1);
          foreach (XmlSchema xmlSchema in (IEnumerable) xs.Schemas(schemaSerializable.TargetNamespace))
          {
            memoryStream2.SetLength(0L);
            xmlSchema.Write((Stream) memoryStream2);
            if (memoryStream1.Length == memoryStream2.Length)
            {
              memoryStream1.Position = 0L;
              memoryStream2.Position = 0L;
              do
                ;
              while (memoryStream1.Position != memoryStream1.Length && memoryStream1.ReadByte() == memoryStream2.ReadByte());
              if (memoryStream1.Position == memoryStream1.Length)
                return schemaComplexType;
            }
          }
        }
        finally
        {
          if (memoryStream1 != null)
            memoryStream1.Close();
          if (memoryStream2 != null)
            memoryStream2.Close();
        }
      }
      xs.Add(schemaSerializable);
      return schemaComplexType;
    }

    public delegate void Table0RowChangeEventHandler(object sender, InvoiceOutInvoice_DS.Table0RowChangeEvent e);

    public delegate void Table1RowChangeEventHandler(object sender, InvoiceOutInvoice_DS.Table1RowChangeEvent e);

    public delegate void Table2RowChangeEventHandler(object sender, InvoiceOutInvoice_DS.Table2RowChangeEvent e);

    [XmlSchemaProvider("GetTypedTableSchema")]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    [Serializable]
    public class Table0DataTable : DataTable, IEnumerable
    {
      private DataColumn columnCONTRACTOR_FROM_NAME;
      private DataColumn columnCONTRACTOR_FROM_ADDRESS;
      private DataColumn columnCONTRACTOR_FROM_INN;
      private DataColumn columnCONTRACTOR_TO_NAME;
      private DataColumn columnCONTRACTOR_TO_ADDRESS;
      private DataColumn columnINVOICE_OUT_NAME;
      private DataColumn columnINVOICE_OUT_DATE;
      private DataColumn columnSUMM_WITHOUT_TAX;
      private DataColumn columnSUMM_TAX;
      private DataColumn columnSUMM_WITH_TAX;
      private DataColumn columnCONTRACTOR_TO_INN;
      private DataColumn columnGCONTRACTOR;
      private DataColumn columnGCONTRACTOR_TO_NAME;
      private DataColumn columnGCONTRACTOR_TO_ADDRESS;

      [DebuggerNonUserCode]
      public DataColumn CONTRACTOR_FROM_NAMEColumn
      {
        get
        {
          return this.columnCONTRACTOR_FROM_NAME;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn CONTRACTOR_FROM_ADDRESSColumn
      {
        get
        {
          return this.columnCONTRACTOR_FROM_ADDRESS;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn CONTRACTOR_FROM_INNColumn
      {
        get
        {
          return this.columnCONTRACTOR_FROM_INN;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn CONTRACTOR_TO_NAMEColumn
      {
        get
        {
          return this.columnCONTRACTOR_TO_NAME;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn CONTRACTOR_TO_ADDRESSColumn
      {
        get
        {
          return this.columnCONTRACTOR_TO_ADDRESS;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn INVOICE_OUT_NAMEColumn
      {
        get
        {
          return this.columnINVOICE_OUT_NAME;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn INVOICE_OUT_DATEColumn
      {
        get
        {
          return this.columnINVOICE_OUT_DATE;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn SUMM_WITHOUT_TAXColumn
      {
        get
        {
          return this.columnSUMM_WITHOUT_TAX;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn SUMM_TAXColumn
      {
        get
        {
          return this.columnSUMM_TAX;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn SUMM_WITH_TAXColumn
      {
        get
        {
          return this.columnSUMM_WITH_TAX;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn CONTRACTOR_TO_INNColumn
      {
        get
        {
          return this.columnCONTRACTOR_TO_INN;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn GCONTRACTORColumn
      {
        get
        {
          return this.columnGCONTRACTOR;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn GCONTRACTOR_TO_NAMEColumn
      {
        get
        {
          return this.columnGCONTRACTOR_TO_NAME;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn GCONTRACTOR_TO_ADDRESSColumn
      {
        get
        {
          return this.columnGCONTRACTOR_TO_ADDRESS;
        }
      }

      [DebuggerNonUserCode]
      [Browsable(false)]
      public int Count
      {
        get
        {
          return this.Rows.Count;
        }
      }

      [DebuggerNonUserCode]
      public InvoiceOutInvoice_DS.Table0Row this[int index]
      {
        get
        {
          return (InvoiceOutInvoice_DS.Table0Row) this.Rows[index];
        }
      }

      public event InvoiceOutInvoice_DS.Table0RowChangeEventHandler Table0RowChanging;

      public event InvoiceOutInvoice_DS.Table0RowChangeEventHandler Table0RowChanged;

      public event InvoiceOutInvoice_DS.Table0RowChangeEventHandler Table0RowDeleting;

      public event InvoiceOutInvoice_DS.Table0RowChangeEventHandler Table0RowDeleted;

      [DebuggerNonUserCode]
      public Table0DataTable()
      {
        this.TableName = "Table0";
        this.BeginInit();
        this.InitClass();
        this.EndInit();
      }

      [DebuggerNonUserCode]
      internal Table0DataTable(DataTable table)
      {
        this.TableName = table.TableName;
        if (table.CaseSensitive != table.DataSet.CaseSensitive)
          this.CaseSensitive = table.CaseSensitive;
        if (table.Locale.ToString() != table.DataSet.Locale.ToString())
          this.Locale = table.Locale;
        if (table.Namespace != table.DataSet.Namespace)
          this.Namespace = table.Namespace;
        this.Prefix = table.Prefix;
        this.MinimumCapacity = table.MinimumCapacity;
      }

      [DebuggerNonUserCode]
      protected Table0DataTable(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this.InitVars();
      }

      [DebuggerNonUserCode]
      public void AddTable0Row(InvoiceOutInvoice_DS.Table0Row row)
      {
        this.Rows.Add((DataRow) row);
      }

      [DebuggerNonUserCode]
      public InvoiceOutInvoice_DS.Table0Row AddTable0Row(string CONTRACTOR_FROM_NAME, string CONTRACTOR_FROM_ADDRESS, string CONTRACTOR_FROM_INN, string CONTRACTOR_TO_NAME, string CONTRACTOR_TO_ADDRESS, string INVOICE_OUT_NAME, string INVOICE_OUT_DATE, string SUMM_WITHOUT_TAX, string SUMM_TAX, string SUMM_WITH_TAX, string CONTRACTOR_TO_INN, string GCONTRACTOR, string GCONTRACTOR_TO_NAME, string GCONTRACTOR_TO_ADDRESS)
      {
        InvoiceOutInvoice_DS.Table0Row table0Row = (InvoiceOutInvoice_DS.Table0Row) this.NewRow();
        object[] objArray = new object[14]
        {
          (object) CONTRACTOR_FROM_NAME,
          (object) CONTRACTOR_FROM_ADDRESS,
          (object) CONTRACTOR_FROM_INN,
          (object) CONTRACTOR_TO_NAME,
          (object) CONTRACTOR_TO_ADDRESS,
          (object) INVOICE_OUT_NAME,
          (object) INVOICE_OUT_DATE,
          (object) SUMM_WITHOUT_TAX,
          (object) SUMM_TAX,
          (object) SUMM_WITH_TAX,
          (object) CONTRACTOR_TO_INN,
          (object) GCONTRACTOR,
          (object) GCONTRACTOR_TO_NAME,
          (object) GCONTRACTOR_TO_ADDRESS
        };
        table0Row.ItemArray = objArray;
        this.Rows.Add((DataRow) table0Row);
        return table0Row;
      }

      [DebuggerNonUserCode]
      public virtual IEnumerator GetEnumerator()
      {
        return this.Rows.GetEnumerator();
      }

      [DebuggerNonUserCode]
      public override DataTable Clone()
      {
        InvoiceOutInvoice_DS.Table0DataTable table0DataTable = (InvoiceOutInvoice_DS.Table0DataTable) base.Clone();
        table0DataTable.InitVars();
        return (DataTable) table0DataTable;
      }

      [DebuggerNonUserCode]
      protected override DataTable CreateInstance()
      {
        return (DataTable) new InvoiceOutInvoice_DS.Table0DataTable();
      }

      [DebuggerNonUserCode]
      internal void InitVars()
      {
        this.columnCONTRACTOR_FROM_NAME = this.Columns["CONTRACTOR_FROM_NAME"];
        this.columnCONTRACTOR_FROM_ADDRESS = this.Columns["CONTRACTOR_FROM_ADDRESS"];
        this.columnCONTRACTOR_FROM_INN = this.Columns["CONTRACTOR_FROM_INN"];
        this.columnCONTRACTOR_TO_NAME = this.Columns["CONTRACTOR_TO_NAME"];
        this.columnCONTRACTOR_TO_ADDRESS = this.Columns["CONTRACTOR_TO_ADDRESS"];
        this.columnINVOICE_OUT_NAME = this.Columns["INVOICE_OUT_NAME"];
        this.columnINVOICE_OUT_DATE = this.Columns["INVOICE_OUT_DATE"];
        this.columnSUMM_WITHOUT_TAX = this.Columns["SUMM_WITHOUT_TAX"];
        this.columnSUMM_TAX = this.Columns["SUMM_TAX"];
        this.columnSUMM_WITH_TAX = this.Columns["SUMM_WITH_TAX"];
        this.columnCONTRACTOR_TO_INN = this.Columns["CONTRACTOR_TO_INN"];
        this.columnGCONTRACTOR = this.Columns["GCONTRACTOR"];
        this.columnGCONTRACTOR_TO_NAME = this.Columns["GCONTRACTOR_TO_NAME"];
        this.columnGCONTRACTOR_TO_ADDRESS = this.Columns["GCONTRACTOR_TO_ADDRESS"];
      }

      [DebuggerNonUserCode]
      private void InitClass()
      {
        this.columnCONTRACTOR_FROM_NAME = new DataColumn("CONTRACTOR_FROM_NAME", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnCONTRACTOR_FROM_NAME);
        this.columnCONTRACTOR_FROM_ADDRESS = new DataColumn("CONTRACTOR_FROM_ADDRESS", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnCONTRACTOR_FROM_ADDRESS);
        this.columnCONTRACTOR_FROM_INN = new DataColumn("CONTRACTOR_FROM_INN", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnCONTRACTOR_FROM_INN);
        this.columnCONTRACTOR_TO_NAME = new DataColumn("CONTRACTOR_TO_NAME", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnCONTRACTOR_TO_NAME);
        this.columnCONTRACTOR_TO_ADDRESS = new DataColumn("CONTRACTOR_TO_ADDRESS", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnCONTRACTOR_TO_ADDRESS);
        this.columnINVOICE_OUT_NAME = new DataColumn("INVOICE_OUT_NAME", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnINVOICE_OUT_NAME);
        this.columnINVOICE_OUT_DATE = new DataColumn("INVOICE_OUT_DATE", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnINVOICE_OUT_DATE);
        this.columnSUMM_WITHOUT_TAX = new DataColumn("SUMM_WITHOUT_TAX", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnSUMM_WITHOUT_TAX);
        this.columnSUMM_TAX = new DataColumn("SUMM_TAX", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnSUMM_TAX);
        this.columnSUMM_WITH_TAX = new DataColumn("SUMM_WITH_TAX", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnSUMM_WITH_TAX);
        this.columnCONTRACTOR_TO_INN = new DataColumn("CONTRACTOR_TO_INN", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnCONTRACTOR_TO_INN);
        this.columnGCONTRACTOR = new DataColumn("GCONTRACTOR", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnGCONTRACTOR);
        this.columnGCONTRACTOR_TO_NAME = new DataColumn("GCONTRACTOR_TO_NAME", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnGCONTRACTOR_TO_NAME);
        this.columnGCONTRACTOR_TO_ADDRESS = new DataColumn("GCONTRACTOR_TO_ADDRESS", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnGCONTRACTOR_TO_ADDRESS);
      }

      [DebuggerNonUserCode]
      public InvoiceOutInvoice_DS.Table0Row NewTable0Row()
      {
        return (InvoiceOutInvoice_DS.Table0Row) this.NewRow();
      }

      [DebuggerNonUserCode]
      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return (DataRow) new InvoiceOutInvoice_DS.Table0Row(builder);
      }

      [DebuggerNonUserCode]
      protected override Type GetRowType()
      {
        return typeof (InvoiceOutInvoice_DS.Table0Row);
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if (this.Table0RowChanged == null)
          return;
        this.Table0RowChanged((object) this, new InvoiceOutInvoice_DS.Table0RowChangeEvent((InvoiceOutInvoice_DS.Table0Row) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if (this.Table0RowChanging == null)
          return;
        this.Table0RowChanging((object) this, new InvoiceOutInvoice_DS.Table0RowChangeEvent((InvoiceOutInvoice_DS.Table0Row) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if (this.Table0RowDeleted == null)
          return;
        this.Table0RowDeleted((object) this, new InvoiceOutInvoice_DS.Table0RowChangeEvent((InvoiceOutInvoice_DS.Table0Row) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if (this.Table0RowDeleting == null)
          return;
        this.Table0RowDeleting((object) this, new InvoiceOutInvoice_DS.Table0RowChangeEvent((InvoiceOutInvoice_DS.Table0Row) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      public void RemoveTable0Row(InvoiceOutInvoice_DS.Table0Row row)
      {
        this.Rows.Remove((DataRow) row);
      }

      [DebuggerNonUserCode]
      public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
      {
        XmlSchemaComplexType schemaComplexType = new XmlSchemaComplexType();
        XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
        InvoiceOutInvoice_DS invoiceOutInvoiceDs = new InvoiceOutInvoice_DS();
        XmlSchemaAny xmlSchemaAny1 = new XmlSchemaAny();
        xmlSchemaAny1.Namespace = "http://www.w3.org/2001/XMLSchema";
        xmlSchemaAny1.MinOccurs = new Decimal(0);
        xmlSchemaAny1.MaxOccurs = new Decimal(-1, -1, -1, false, (byte) 0);
        xmlSchemaAny1.ProcessContents = XmlSchemaContentProcessing.Lax;
        xmlSchemaSequence.Items.Add((XmlSchemaObject) xmlSchemaAny1);
        XmlSchemaAny xmlSchemaAny2 = new XmlSchemaAny();
        xmlSchemaAny2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
        xmlSchemaAny2.MinOccurs = new Decimal(1);
        xmlSchemaAny2.ProcessContents = XmlSchemaContentProcessing.Lax;
        xmlSchemaSequence.Items.Add((XmlSchemaObject) xmlSchemaAny2);
        schemaComplexType.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "namespace",
          FixedValue = invoiceOutInvoiceDs.Namespace
        });
        schemaComplexType.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "tableTypeName",
          FixedValue = "Table0DataTable"
        });
        schemaComplexType.Particle = (XmlSchemaParticle) xmlSchemaSequence;
        XmlSchema schemaSerializable = invoiceOutInvoiceDs.GetSchemaSerializable();
        if (xs.Contains(schemaSerializable.TargetNamespace))
        {
          MemoryStream memoryStream1 = new MemoryStream();
          MemoryStream memoryStream2 = new MemoryStream();
          try
          {
            schemaSerializable.Write((Stream) memoryStream1);
            foreach (XmlSchema xmlSchema in (IEnumerable) xs.Schemas(schemaSerializable.TargetNamespace))
            {
              memoryStream2.SetLength(0L);
              xmlSchema.Write((Stream) memoryStream2);
              if (memoryStream1.Length == memoryStream2.Length)
              {
                memoryStream1.Position = 0L;
                memoryStream2.Position = 0L;
                do
                  ;
                while (memoryStream1.Position != memoryStream1.Length && memoryStream1.ReadByte() == memoryStream2.ReadByte());
                if (memoryStream1.Position == memoryStream1.Length)
                  return schemaComplexType;
              }
            }
          }
          finally
          {
            if (memoryStream1 != null)
              memoryStream1.Close();
            if (memoryStream2 != null)
              memoryStream2.Close();
          }
        }
        xs.Add(schemaSerializable);
        return schemaComplexType;
      }
    }

    [XmlSchemaProvider("GetTypedTableSchema")]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    [Serializable]
    public class Table1DataTable : DataTable, IEnumerable
    {
      private DataColumn columnGOODS_NAME;
      private DataColumn columnUNIT_NAME;
      private DataColumn columnQUANTITY;
      private DataColumn columnPRICE_SAL;
      private DataColumn columnSUM_SAL_WITHOUT_VAT;
      private DataColumn columnVAT_SAL;
      private DataColumn columnPSUM_SAL;
      private DataColumn columnSUM_SAL;
      private DataColumn columnCOUNTRY;
      private DataColumn columnGTD_NUMBER;

      [DebuggerNonUserCode]
      public DataColumn GOODS_NAMEColumn
      {
        get
        {
          return this.columnGOODS_NAME;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn UNIT_NAMEColumn
      {
        get
        {
          return this.columnUNIT_NAME;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn QUANTITYColumn
      {
        get
        {
          return this.columnQUANTITY;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn PRICE_SALColumn
      {
        get
        {
          return this.columnPRICE_SAL;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn SUM_SAL_WITHOUT_VATColumn
      {
        get
        {
          return this.columnSUM_SAL_WITHOUT_VAT;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn VAT_SALColumn
      {
        get
        {
          return this.columnVAT_SAL;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn PSUM_SALColumn
      {
        get
        {
          return this.columnPSUM_SAL;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn SUM_SALColumn
      {
        get
        {
          return this.columnSUM_SAL;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn COUNTRYColumn
      {
        get
        {
          return this.columnCOUNTRY;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn GTD_NUMBERColumn
      {
        get
        {
          return this.columnGTD_NUMBER;
        }
      }

      [DebuggerNonUserCode]
      [Browsable(false)]
      public int Count
      {
        get
        {
          return this.Rows.Count;
        }
      }

      [DebuggerNonUserCode]
      public InvoiceOutInvoice_DS.Table1Row this[int index]
      {
        get
        {
          return (InvoiceOutInvoice_DS.Table1Row) this.Rows[index];
        }
      }

      public event InvoiceOutInvoice_DS.Table1RowChangeEventHandler Table1RowChanging;

      public event InvoiceOutInvoice_DS.Table1RowChangeEventHandler Table1RowChanged;

      public event InvoiceOutInvoice_DS.Table1RowChangeEventHandler Table1RowDeleting;

      public event InvoiceOutInvoice_DS.Table1RowChangeEventHandler Table1RowDeleted;

      [DebuggerNonUserCode]
      public Table1DataTable()
      {
        this.TableName = "Table1";
        this.BeginInit();
        this.InitClass();
        this.EndInit();
      }

      [DebuggerNonUserCode]
      internal Table1DataTable(DataTable table)
      {
        this.TableName = table.TableName;
        if (table.CaseSensitive != table.DataSet.CaseSensitive)
          this.CaseSensitive = table.CaseSensitive;
        if (table.Locale.ToString() != table.DataSet.Locale.ToString())
          this.Locale = table.Locale;
        if (table.Namespace != table.DataSet.Namespace)
          this.Namespace = table.Namespace;
        this.Prefix = table.Prefix;
        this.MinimumCapacity = table.MinimumCapacity;
      }

      [DebuggerNonUserCode]
      protected Table1DataTable(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this.InitVars();
      }

      [DebuggerNonUserCode]
      public void AddTable1Row(InvoiceOutInvoice_DS.Table1Row row)
      {
        this.Rows.Add((DataRow) row);
      }

      [DebuggerNonUserCode]
      public InvoiceOutInvoice_DS.Table1Row AddTable1Row(string GOODS_NAME, string UNIT_NAME, string QUANTITY, string PRICE_SAL, string SUM_SAL_WITHOUT_VAT, string VAT_SAL, string PSUM_SAL, string SUM_SAL, string COUNTRY, string GTD_NUMBER)
      {
        InvoiceOutInvoice_DS.Table1Row table1Row = (InvoiceOutInvoice_DS.Table1Row) this.NewRow();
        object[] objArray = new object[10]
        {
          (object) GOODS_NAME,
          (object) UNIT_NAME,
          (object) QUANTITY,
          (object) PRICE_SAL,
          (object) SUM_SAL_WITHOUT_VAT,
          (object) VAT_SAL,
          (object) PSUM_SAL,
          (object) SUM_SAL,
          (object) COUNTRY,
          (object) GTD_NUMBER
        };
        table1Row.ItemArray = objArray;
        this.Rows.Add((DataRow) table1Row);
        return table1Row;
      }

      [DebuggerNonUserCode]
      public virtual IEnumerator GetEnumerator()
      {
        return this.Rows.GetEnumerator();
      }

      [DebuggerNonUserCode]
      public override DataTable Clone()
      {
        InvoiceOutInvoice_DS.Table1DataTable table1DataTable = (InvoiceOutInvoice_DS.Table1DataTable) base.Clone();
        table1DataTable.InitVars();
        return (DataTable) table1DataTable;
      }

      [DebuggerNonUserCode]
      protected override DataTable CreateInstance()
      {
        return (DataTable) new InvoiceOutInvoice_DS.Table1DataTable();
      }

      [DebuggerNonUserCode]
      internal void InitVars()
      {
        this.columnGOODS_NAME = this.Columns["GOODS_NAME"];
        this.columnUNIT_NAME = this.Columns["UNIT_NAME"];
        this.columnQUANTITY = this.Columns["QUANTITY"];
        this.columnPRICE_SAL = this.Columns["PRICE_SAL"];
        this.columnSUM_SAL_WITHOUT_VAT = this.Columns["SUM_SAL_WITHOUT_VAT"];
        this.columnVAT_SAL = this.Columns["VAT_SAL"];
        this.columnPSUM_SAL = this.Columns["PSUM_SAL"];
        this.columnSUM_SAL = this.Columns["SUM_SAL"];
        this.columnCOUNTRY = this.Columns["COUNTRY"];
        this.columnGTD_NUMBER = this.Columns["GTD_NUMBER"];
      }

      [DebuggerNonUserCode]
      private void InitClass()
      {
        this.columnGOODS_NAME = new DataColumn("GOODS_NAME", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnGOODS_NAME);
        this.columnUNIT_NAME = new DataColumn("UNIT_NAME", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnUNIT_NAME);
        this.columnQUANTITY = new DataColumn("QUANTITY", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnQUANTITY);
        this.columnPRICE_SAL = new DataColumn("PRICE_SAL", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnPRICE_SAL);
        this.columnSUM_SAL_WITHOUT_VAT = new DataColumn("SUM_SAL_WITHOUT_VAT", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnSUM_SAL_WITHOUT_VAT);
        this.columnVAT_SAL = new DataColumn("VAT_SAL", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnVAT_SAL);
        this.columnPSUM_SAL = new DataColumn("PSUM_SAL", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnPSUM_SAL);
        this.columnSUM_SAL = new DataColumn("SUM_SAL", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnSUM_SAL);
        this.columnCOUNTRY = new DataColumn("COUNTRY", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnCOUNTRY);
        this.columnGTD_NUMBER = new DataColumn("GTD_NUMBER", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnGTD_NUMBER);
      }

      [DebuggerNonUserCode]
      public InvoiceOutInvoice_DS.Table1Row NewTable1Row()
      {
        return (InvoiceOutInvoice_DS.Table1Row) this.NewRow();
      }

      [DebuggerNonUserCode]
      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return (DataRow) new InvoiceOutInvoice_DS.Table1Row(builder);
      }

      [DebuggerNonUserCode]
      protected override Type GetRowType()
      {
        return typeof (InvoiceOutInvoice_DS.Table1Row);
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if (this.Table1RowChanged == null)
          return;
        this.Table1RowChanged((object) this, new InvoiceOutInvoice_DS.Table1RowChangeEvent((InvoiceOutInvoice_DS.Table1Row) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if (this.Table1RowChanging == null)
          return;
        this.Table1RowChanging((object) this, new InvoiceOutInvoice_DS.Table1RowChangeEvent((InvoiceOutInvoice_DS.Table1Row) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if (this.Table1RowDeleted == null)
          return;
        this.Table1RowDeleted((object) this, new InvoiceOutInvoice_DS.Table1RowChangeEvent((InvoiceOutInvoice_DS.Table1Row) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if (this.Table1RowDeleting == null)
          return;
        this.Table1RowDeleting((object) this, new InvoiceOutInvoice_DS.Table1RowChangeEvent((InvoiceOutInvoice_DS.Table1Row) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      public void RemoveTable1Row(InvoiceOutInvoice_DS.Table1Row row)
      {
        this.Rows.Remove((DataRow) row);
      }

      [DebuggerNonUserCode]
      public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
      {
        XmlSchemaComplexType schemaComplexType = new XmlSchemaComplexType();
        XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
        InvoiceOutInvoice_DS invoiceOutInvoiceDs = new InvoiceOutInvoice_DS();
        XmlSchemaAny xmlSchemaAny1 = new XmlSchemaAny();
        xmlSchemaAny1.Namespace = "http://www.w3.org/2001/XMLSchema";
        xmlSchemaAny1.MinOccurs = new Decimal(0);
        xmlSchemaAny1.MaxOccurs = new Decimal(-1, -1, -1, false, (byte) 0);
        xmlSchemaAny1.ProcessContents = XmlSchemaContentProcessing.Lax;
        xmlSchemaSequence.Items.Add((XmlSchemaObject) xmlSchemaAny1);
        XmlSchemaAny xmlSchemaAny2 = new XmlSchemaAny();
        xmlSchemaAny2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
        xmlSchemaAny2.MinOccurs = new Decimal(1);
        xmlSchemaAny2.ProcessContents = XmlSchemaContentProcessing.Lax;
        xmlSchemaSequence.Items.Add((XmlSchemaObject) xmlSchemaAny2);
        schemaComplexType.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "namespace",
          FixedValue = invoiceOutInvoiceDs.Namespace
        });
        schemaComplexType.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "tableTypeName",
          FixedValue = "Table1DataTable"
        });
        schemaComplexType.Particle = (XmlSchemaParticle) xmlSchemaSequence;
        XmlSchema schemaSerializable = invoiceOutInvoiceDs.GetSchemaSerializable();
        if (xs.Contains(schemaSerializable.TargetNamespace))
        {
          MemoryStream memoryStream1 = new MemoryStream();
          MemoryStream memoryStream2 = new MemoryStream();
          try
          {
            schemaSerializable.Write((Stream) memoryStream1);
            foreach (XmlSchema xmlSchema in (IEnumerable) xs.Schemas(schemaSerializable.TargetNamespace))
            {
              memoryStream2.SetLength(0L);
              xmlSchema.Write((Stream) memoryStream2);
              if (memoryStream1.Length == memoryStream2.Length)
              {
                memoryStream1.Position = 0L;
                memoryStream2.Position = 0L;
                do
                  ;
                while (memoryStream1.Position != memoryStream1.Length && memoryStream1.ReadByte() == memoryStream2.ReadByte());
                if (memoryStream1.Position == memoryStream1.Length)
                  return schemaComplexType;
              }
            }
          }
          finally
          {
            if (memoryStream1 != null)
              memoryStream1.Close();
            if (memoryStream2 != null)
              memoryStream2.Close();
          }
        }
        xs.Add(schemaSerializable);
        return schemaComplexType;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    [XmlSchemaProvider("GetTypedTableSchema")]
    [Serializable]
    public class Table2DataTable : DataTable, IEnumerable
    {
      private DataColumn columnDIRECTOR_DOC;
      private DataColumn columnBUH_DOC;
      private DataColumn columnDIR;
      private DataColumn columnBUH;

      [DebuggerNonUserCode]
      public DataColumn DIRECTOR_DOCColumn
      {
        get
        {
          return this.columnDIRECTOR_DOC;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn BUH_DOCColumn
      {
        get
        {
          return this.columnBUH_DOC;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn DIRColumn
      {
        get
        {
          return this.columnDIR;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn BUHColumn
      {
        get
        {
          return this.columnBUH;
        }
      }

      [DebuggerNonUserCode]
      [Browsable(false)]
      public int Count
      {
        get
        {
          return this.Rows.Count;
        }
      }

      [DebuggerNonUserCode]
      public InvoiceOutInvoice_DS.Table2Row this[int index]
      {
        get
        {
          return (InvoiceOutInvoice_DS.Table2Row) this.Rows[index];
        }
      }

      public event InvoiceOutInvoice_DS.Table2RowChangeEventHandler Table2RowChanging;

      public event InvoiceOutInvoice_DS.Table2RowChangeEventHandler Table2RowChanged;

      public event InvoiceOutInvoice_DS.Table2RowChangeEventHandler Table2RowDeleting;

      public event InvoiceOutInvoice_DS.Table2RowChangeEventHandler Table2RowDeleted;

      [DebuggerNonUserCode]
      public Table2DataTable()
      {
        this.TableName = "Table2";
        this.BeginInit();
        this.InitClass();
        this.EndInit();
      }

      [DebuggerNonUserCode]
      internal Table2DataTable(DataTable table)
      {
        this.TableName = table.TableName;
        if (table.CaseSensitive != table.DataSet.CaseSensitive)
          this.CaseSensitive = table.CaseSensitive;
        if (table.Locale.ToString() != table.DataSet.Locale.ToString())
          this.Locale = table.Locale;
        if (table.Namespace != table.DataSet.Namespace)
          this.Namespace = table.Namespace;
        this.Prefix = table.Prefix;
        this.MinimumCapacity = table.MinimumCapacity;
      }

      [DebuggerNonUserCode]
      protected Table2DataTable(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this.InitVars();
      }

      [DebuggerNonUserCode]
      public void AddTable2Row(InvoiceOutInvoice_DS.Table2Row row)
      {
        this.Rows.Add((DataRow) row);
      }

      [DebuggerNonUserCode]
      public InvoiceOutInvoice_DS.Table2Row AddTable2Row(string DIRECTOR_DOC, string BUH_DOC, string DIR, string BUH)
      {
        InvoiceOutInvoice_DS.Table2Row table2Row = (InvoiceOutInvoice_DS.Table2Row) this.NewRow();
        object[] objArray = new object[4]
        {
          (object) DIRECTOR_DOC,
          (object) BUH_DOC,
          (object) DIR,
          (object) BUH
        };
        table2Row.ItemArray = objArray;
        this.Rows.Add((DataRow) table2Row);
        return table2Row;
      }

      [DebuggerNonUserCode]
      public virtual IEnumerator GetEnumerator()
      {
        return this.Rows.GetEnumerator();
      }

      [DebuggerNonUserCode]
      public override DataTable Clone()
      {
        InvoiceOutInvoice_DS.Table2DataTable table2DataTable = (InvoiceOutInvoice_DS.Table2DataTable) base.Clone();
        table2DataTable.InitVars();
        return (DataTable) table2DataTable;
      }

      [DebuggerNonUserCode]
      protected override DataTable CreateInstance()
      {
        return (DataTable) new InvoiceOutInvoice_DS.Table2DataTable();
      }

      [DebuggerNonUserCode]
      internal void InitVars()
      {
        this.columnDIRECTOR_DOC = this.Columns["DIRECTOR_DOC"];
        this.columnBUH_DOC = this.Columns["BUH_DOC"];
        this.columnDIR = this.Columns["DIR"];
        this.columnBUH = this.Columns["BUH"];
      }

      [DebuggerNonUserCode]
      private void InitClass()
      {
        this.columnDIRECTOR_DOC = new DataColumn("DIRECTOR_DOC", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnDIRECTOR_DOC);
        this.columnBUH_DOC = new DataColumn("BUH_DOC", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnBUH_DOC);
        this.columnDIR = new DataColumn("DIR", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnDIR);
        this.columnBUH = new DataColumn("BUH", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnBUH);
      }

      [DebuggerNonUserCode]
      public InvoiceOutInvoice_DS.Table2Row NewTable2Row()
      {
        return (InvoiceOutInvoice_DS.Table2Row) this.NewRow();
      }

      [DebuggerNonUserCode]
      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return (DataRow) new InvoiceOutInvoice_DS.Table2Row(builder);
      }

      [DebuggerNonUserCode]
      protected override Type GetRowType()
      {
        return typeof (InvoiceOutInvoice_DS.Table2Row);
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if (this.Table2RowChanged == null)
          return;
        this.Table2RowChanged((object) this, new InvoiceOutInvoice_DS.Table2RowChangeEvent((InvoiceOutInvoice_DS.Table2Row) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if (this.Table2RowChanging == null)
          return;
        this.Table2RowChanging((object) this, new InvoiceOutInvoice_DS.Table2RowChangeEvent((InvoiceOutInvoice_DS.Table2Row) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if (this.Table2RowDeleted == null)
          return;
        this.Table2RowDeleted((object) this, new InvoiceOutInvoice_DS.Table2RowChangeEvent((InvoiceOutInvoice_DS.Table2Row) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if (this.Table2RowDeleting == null)
          return;
        this.Table2RowDeleting((object) this, new InvoiceOutInvoice_DS.Table2RowChangeEvent((InvoiceOutInvoice_DS.Table2Row) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      public void RemoveTable2Row(InvoiceOutInvoice_DS.Table2Row row)
      {
        this.Rows.Remove((DataRow) row);
      }

      [DebuggerNonUserCode]
      public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
      {
        XmlSchemaComplexType schemaComplexType = new XmlSchemaComplexType();
        XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
        InvoiceOutInvoice_DS invoiceOutInvoiceDs = new InvoiceOutInvoice_DS();
        XmlSchemaAny xmlSchemaAny1 = new XmlSchemaAny();
        xmlSchemaAny1.Namespace = "http://www.w3.org/2001/XMLSchema";
        xmlSchemaAny1.MinOccurs = new Decimal(0);
        xmlSchemaAny1.MaxOccurs = new Decimal(-1, -1, -1, false, (byte) 0);
        xmlSchemaAny1.ProcessContents = XmlSchemaContentProcessing.Lax;
        xmlSchemaSequence.Items.Add((XmlSchemaObject) xmlSchemaAny1);
        XmlSchemaAny xmlSchemaAny2 = new XmlSchemaAny();
        xmlSchemaAny2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
        xmlSchemaAny2.MinOccurs = new Decimal(1);
        xmlSchemaAny2.ProcessContents = XmlSchemaContentProcessing.Lax;
        xmlSchemaSequence.Items.Add((XmlSchemaObject) xmlSchemaAny2);
        schemaComplexType.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "namespace",
          FixedValue = invoiceOutInvoiceDs.Namespace
        });
        schemaComplexType.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "tableTypeName",
          FixedValue = "Table2DataTable"
        });
        schemaComplexType.Particle = (XmlSchemaParticle) xmlSchemaSequence;
        XmlSchema schemaSerializable = invoiceOutInvoiceDs.GetSchemaSerializable();
        if (xs.Contains(schemaSerializable.TargetNamespace))
        {
          MemoryStream memoryStream1 = new MemoryStream();
          MemoryStream memoryStream2 = new MemoryStream();
          try
          {
            schemaSerializable.Write((Stream) memoryStream1);
            foreach (XmlSchema xmlSchema in (IEnumerable) xs.Schemas(schemaSerializable.TargetNamespace))
            {
              memoryStream2.SetLength(0L);
              xmlSchema.Write((Stream) memoryStream2);
              if (memoryStream1.Length == memoryStream2.Length)
              {
                memoryStream1.Position = 0L;
                memoryStream2.Position = 0L;
                do
                  ;
                while (memoryStream1.Position != memoryStream1.Length && memoryStream1.ReadByte() == memoryStream2.ReadByte());
                if (memoryStream1.Position == memoryStream1.Length)
                  return schemaComplexType;
              }
            }
          }
          finally
          {
            if (memoryStream1 != null)
              memoryStream1.Close();
            if (memoryStream2 != null)
              memoryStream2.Close();
          }
        }
        xs.Add(schemaSerializable);
        return schemaComplexType;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class Table0Row : DataRow
    {
      private InvoiceOutInvoice_DS.Table0DataTable tableTable0;

      [DebuggerNonUserCode]
      public string CONTRACTOR_FROM_NAME
      {
        get
        {
          try
          {
            return (string) this[this.tableTable0.CONTRACTOR_FROM_NAMEColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'CONTRACTOR_FROM_NAME' in table 'Table0' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable0.CONTRACTOR_FROM_NAMEColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public string CONTRACTOR_FROM_ADDRESS
      {
        get
        {
          try
          {
            return (string) this[this.tableTable0.CONTRACTOR_FROM_ADDRESSColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'CONTRACTOR_FROM_ADDRESS' in table 'Table0' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable0.CONTRACTOR_FROM_ADDRESSColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public string CONTRACTOR_FROM_INN
      {
        get
        {
          try
          {
            return (string) this[this.tableTable0.CONTRACTOR_FROM_INNColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'CONTRACTOR_FROM_INN' in table 'Table0' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable0.CONTRACTOR_FROM_INNColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public string CONTRACTOR_TO_NAME
      {
        get
        {
          try
          {
            return (string) this[this.tableTable0.CONTRACTOR_TO_NAMEColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'CONTRACTOR_TO_NAME' in table 'Table0' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable0.CONTRACTOR_TO_NAMEColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public string CONTRACTOR_TO_ADDRESS
      {
        get
        {
          try
          {
            return (string) this[this.tableTable0.CONTRACTOR_TO_ADDRESSColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'CONTRACTOR_TO_ADDRESS' in table 'Table0' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable0.CONTRACTOR_TO_ADDRESSColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public string INVOICE_OUT_NAME
      {
        get
        {
          try
          {
            return (string) this[this.tableTable0.INVOICE_OUT_NAMEColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'INVOICE_OUT_NAME' in table 'Table0' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable0.INVOICE_OUT_NAMEColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public string INVOICE_OUT_DATE
      {
        get
        {
          try
          {
            return (string) this[this.tableTable0.INVOICE_OUT_DATEColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'INVOICE_OUT_DATE' in table 'Table0' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable0.INVOICE_OUT_DATEColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public string SUMM_WITHOUT_TAX
      {
        get
        {
          try
          {
            return (string) this[this.tableTable0.SUMM_WITHOUT_TAXColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'SUMM_WITHOUT_TAX' in table 'Table0' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable0.SUMM_WITHOUT_TAXColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public string SUMM_TAX
      {
        get
        {
          try
          {
            return (string) this[this.tableTable0.SUMM_TAXColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'SUMM_TAX' in table 'Table0' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable0.SUMM_TAXColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public string SUMM_WITH_TAX
      {
        get
        {
          try
          {
            return (string) this[this.tableTable0.SUMM_WITH_TAXColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'SUMM_WITH_TAX' in table 'Table0' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable0.SUMM_WITH_TAXColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public string CONTRACTOR_TO_INN
      {
        get
        {
          try
          {
            return (string) this[this.tableTable0.CONTRACTOR_TO_INNColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'CONTRACTOR_TO_INN' in table 'Table0' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable0.CONTRACTOR_TO_INNColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public string GCONTRACTOR
      {
        get
        {
          try
          {
            return (string) this[this.tableTable0.GCONTRACTORColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'GCONTRACTOR' in table 'Table0' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable0.GCONTRACTORColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public string GCONTRACTOR_TO_NAME
      {
        get
        {
          try
          {
            return (string) this[this.tableTable0.GCONTRACTOR_TO_NAMEColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'GCONTRACTOR_TO_NAME' in table 'Table0' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable0.GCONTRACTOR_TO_NAMEColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public string GCONTRACTOR_TO_ADDRESS
      {
        get
        {
          try
          {
            return (string) this[this.tableTable0.GCONTRACTOR_TO_ADDRESSColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'GCONTRACTOR_TO_ADDRESS' in table 'Table0' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable0.GCONTRACTOR_TO_ADDRESSColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      internal Table0Row(DataRowBuilder rb)
        : base(rb)
      {
        this.tableTable0 = (InvoiceOutInvoice_DS.Table0DataTable) this.Table;
      }

      [DebuggerNonUserCode]
      public bool IsCONTRACTOR_FROM_NAMENull()
      {
        return this.IsNull(this.tableTable0.CONTRACTOR_FROM_NAMEColumn);
      }

      [DebuggerNonUserCode]
      public void SetCONTRACTOR_FROM_NAMENull()
      {
        this[this.tableTable0.CONTRACTOR_FROM_NAMEColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsCONTRACTOR_FROM_ADDRESSNull()
      {
        return this.IsNull(this.tableTable0.CONTRACTOR_FROM_ADDRESSColumn);
      }

      [DebuggerNonUserCode]
      public void SetCONTRACTOR_FROM_ADDRESSNull()
      {
        this[this.tableTable0.CONTRACTOR_FROM_ADDRESSColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsCONTRACTOR_FROM_INNNull()
      {
        return this.IsNull(this.tableTable0.CONTRACTOR_FROM_INNColumn);
      }

      [DebuggerNonUserCode]
      public void SetCONTRACTOR_FROM_INNNull()
      {
        this[this.tableTable0.CONTRACTOR_FROM_INNColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsCONTRACTOR_TO_NAMENull()
      {
        return this.IsNull(this.tableTable0.CONTRACTOR_TO_NAMEColumn);
      }

      [DebuggerNonUserCode]
      public void SetCONTRACTOR_TO_NAMENull()
      {
        this[this.tableTable0.CONTRACTOR_TO_NAMEColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsCONTRACTOR_TO_ADDRESSNull()
      {
        return this.IsNull(this.tableTable0.CONTRACTOR_TO_ADDRESSColumn);
      }

      [DebuggerNonUserCode]
      public void SetCONTRACTOR_TO_ADDRESSNull()
      {
        this[this.tableTable0.CONTRACTOR_TO_ADDRESSColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsINVOICE_OUT_NAMENull()
      {
        return this.IsNull(this.tableTable0.INVOICE_OUT_NAMEColumn);
      }

      [DebuggerNonUserCode]
      public void SetINVOICE_OUT_NAMENull()
      {
        this[this.tableTable0.INVOICE_OUT_NAMEColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsINVOICE_OUT_DATENull()
      {
        return this.IsNull(this.tableTable0.INVOICE_OUT_DATEColumn);
      }

      [DebuggerNonUserCode]
      public void SetINVOICE_OUT_DATENull()
      {
        this[this.tableTable0.INVOICE_OUT_DATEColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsSUMM_WITHOUT_TAXNull()
      {
        return this.IsNull(this.tableTable0.SUMM_WITHOUT_TAXColumn);
      }

      [DebuggerNonUserCode]
      public void SetSUMM_WITHOUT_TAXNull()
      {
        this[this.tableTable0.SUMM_WITHOUT_TAXColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsSUMM_TAXNull()
      {
        return this.IsNull(this.tableTable0.SUMM_TAXColumn);
      }

      [DebuggerNonUserCode]
      public void SetSUMM_TAXNull()
      {
        this[this.tableTable0.SUMM_TAXColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsSUMM_WITH_TAXNull()
      {
        return this.IsNull(this.tableTable0.SUMM_WITH_TAXColumn);
      }

      [DebuggerNonUserCode]
      public void SetSUMM_WITH_TAXNull()
      {
        this[this.tableTable0.SUMM_WITH_TAXColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsCONTRACTOR_TO_INNNull()
      {
        return this.IsNull(this.tableTable0.CONTRACTOR_TO_INNColumn);
      }

      [DebuggerNonUserCode]
      public void SetCONTRACTOR_TO_INNNull()
      {
        this[this.tableTable0.CONTRACTOR_TO_INNColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsGCONTRACTORNull()
      {
        return this.IsNull(this.tableTable0.GCONTRACTORColumn);
      }

      [DebuggerNonUserCode]
      public void SetGCONTRACTORNull()
      {
        this[this.tableTable0.GCONTRACTORColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsGCONTRACTOR_TO_NAMENull()
      {
        return this.IsNull(this.tableTable0.GCONTRACTOR_TO_NAMEColumn);
      }

      [DebuggerNonUserCode]
      public void SetGCONTRACTOR_TO_NAMENull()
      {
        this[this.tableTable0.GCONTRACTOR_TO_NAMEColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsGCONTRACTOR_TO_ADDRESSNull()
      {
        return this.IsNull(this.tableTable0.GCONTRACTOR_TO_ADDRESSColumn);
      }

      [DebuggerNonUserCode]
      public void SetGCONTRACTOR_TO_ADDRESSNull()
      {
        this[this.tableTable0.GCONTRACTOR_TO_ADDRESSColumn] = Convert.DBNull;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class Table1Row : DataRow
    {
      private InvoiceOutInvoice_DS.Table1DataTable tableTable1;

      [DebuggerNonUserCode]
      public string GOODS_NAME
      {
        get
        {
          try
          {
            return (string) this[this.tableTable1.GOODS_NAMEColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'GOODS_NAME' in table 'Table1' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable1.GOODS_NAMEColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public string UNIT_NAME
      {
        get
        {
          try
          {
            return (string) this[this.tableTable1.UNIT_NAMEColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'UNIT_NAME' in table 'Table1' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable1.UNIT_NAMEColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public string QUANTITY
      {
        get
        {
          try
          {
            return (string) this[this.tableTable1.QUANTITYColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'QUANTITY' in table 'Table1' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable1.QUANTITYColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public string PRICE_SAL
      {
        get
        {
          try
          {
            return (string) this[this.tableTable1.PRICE_SALColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'PRICE_SAL' in table 'Table1' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable1.PRICE_SALColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public string SUM_SAL_WITHOUT_VAT
      {
        get
        {
          try
          {
            return (string) this[this.tableTable1.SUM_SAL_WITHOUT_VATColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'SUM_SAL_WITHOUT_VAT' in table 'Table1' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable1.SUM_SAL_WITHOUT_VATColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public string VAT_SAL
      {
        get
        {
          try
          {
            return (string) this[this.tableTable1.VAT_SALColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'VAT_SAL' in table 'Table1' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable1.VAT_SALColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public string PSUM_SAL
      {
        get
        {
          try
          {
            return (string) this[this.tableTable1.PSUM_SALColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'PSUM_SAL' in table 'Table1' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable1.PSUM_SALColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public string SUM_SAL
      {
        get
        {
          try
          {
            return (string) this[this.tableTable1.SUM_SALColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'SUM_SAL' in table 'Table1' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable1.SUM_SALColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public string COUNTRY
      {
        get
        {
          try
          {
            return (string) this[this.tableTable1.COUNTRYColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'COUNTRY' in table 'Table1' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable1.COUNTRYColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public string GTD_NUMBER
      {
        get
        {
          try
          {
            return (string) this[this.tableTable1.GTD_NUMBERColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'GTD_NUMBER' in table 'Table1' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable1.GTD_NUMBERColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      internal Table1Row(DataRowBuilder rb)
        : base(rb)
      {
        this.tableTable1 = (InvoiceOutInvoice_DS.Table1DataTable) this.Table;
      }

      [DebuggerNonUserCode]
      public bool IsGOODS_NAMENull()
      {
        return this.IsNull(this.tableTable1.GOODS_NAMEColumn);
      }

      [DebuggerNonUserCode]
      public void SetGOODS_NAMENull()
      {
        this[this.tableTable1.GOODS_NAMEColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsUNIT_NAMENull()
      {
        return this.IsNull(this.tableTable1.UNIT_NAMEColumn);
      }

      [DebuggerNonUserCode]
      public void SetUNIT_NAMENull()
      {
        this[this.tableTable1.UNIT_NAMEColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsQUANTITYNull()
      {
        return this.IsNull(this.tableTable1.QUANTITYColumn);
      }

      [DebuggerNonUserCode]
      public void SetQUANTITYNull()
      {
        this[this.tableTable1.QUANTITYColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsPRICE_SALNull()
      {
        return this.IsNull(this.tableTable1.PRICE_SALColumn);
      }

      [DebuggerNonUserCode]
      public void SetPRICE_SALNull()
      {
        this[this.tableTable1.PRICE_SALColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsSUM_SAL_WITHOUT_VATNull()
      {
        return this.IsNull(this.tableTable1.SUM_SAL_WITHOUT_VATColumn);
      }

      [DebuggerNonUserCode]
      public void SetSUM_SAL_WITHOUT_VATNull()
      {
        this[this.tableTable1.SUM_SAL_WITHOUT_VATColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsVAT_SALNull()
      {
        return this.IsNull(this.tableTable1.VAT_SALColumn);
      }

      [DebuggerNonUserCode]
      public void SetVAT_SALNull()
      {
        this[this.tableTable1.VAT_SALColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsPSUM_SALNull()
      {
        return this.IsNull(this.tableTable1.PSUM_SALColumn);
      }

      [DebuggerNonUserCode]
      public void SetPSUM_SALNull()
      {
        this[this.tableTable1.PSUM_SALColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsSUM_SALNull()
      {
        return this.IsNull(this.tableTable1.SUM_SALColumn);
      }

      [DebuggerNonUserCode]
      public void SetSUM_SALNull()
      {
        this[this.tableTable1.SUM_SALColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsCOUNTRYNull()
      {
        return this.IsNull(this.tableTable1.COUNTRYColumn);
      }

      [DebuggerNonUserCode]
      public void SetCOUNTRYNull()
      {
        this[this.tableTable1.COUNTRYColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsGTD_NUMBERNull()
      {
        return this.IsNull(this.tableTable1.GTD_NUMBERColumn);
      }

      [DebuggerNonUserCode]
      public void SetGTD_NUMBERNull()
      {
        this[this.tableTable1.GTD_NUMBERColumn] = Convert.DBNull;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class Table2Row : DataRow
    {
      private InvoiceOutInvoice_DS.Table2DataTable tableTable2;

      [DebuggerNonUserCode]
      public string DIRECTOR_DOC
      {
        get
        {
          try
          {
            return (string) this[this.tableTable2.DIRECTOR_DOCColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'DIRECTOR_DOC' in table 'Table2' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable2.DIRECTOR_DOCColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public string BUH_DOC
      {
        get
        {
          try
          {
            return (string) this[this.tableTable2.BUH_DOCColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'BUH_DOC' in table 'Table2' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable2.BUH_DOCColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public string DIR
      {
        get
        {
          try
          {
            return (string) this[this.tableTable2.DIRColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'DIR' in table 'Table2' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable2.DIRColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public string BUH
      {
        get
        {
          try
          {
            return (string) this[this.tableTable2.BUHColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'BUH' in table 'Table2' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable2.BUHColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      internal Table2Row(DataRowBuilder rb)
        : base(rb)
      {
        this.tableTable2 = (InvoiceOutInvoice_DS.Table2DataTable) this.Table;
      }

      [DebuggerNonUserCode]
      public bool IsDIRECTOR_DOCNull()
      {
        return this.IsNull(this.tableTable2.DIRECTOR_DOCColumn);
      }

      [DebuggerNonUserCode]
      public void SetDIRECTOR_DOCNull()
      {
        this[this.tableTable2.DIRECTOR_DOCColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsBUH_DOCNull()
      {
        return this.IsNull(this.tableTable2.BUH_DOCColumn);
      }

      [DebuggerNonUserCode]
      public void SetBUH_DOCNull()
      {
        this[this.tableTable2.BUH_DOCColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsDIRNull()
      {
        return this.IsNull(this.tableTable2.DIRColumn);
      }

      [DebuggerNonUserCode]
      public void SetDIRNull()
      {
        this[this.tableTable2.DIRColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsBUHNull()
      {
        return this.IsNull(this.tableTable2.BUHColumn);
      }

      [DebuggerNonUserCode]
      public void SetBUHNull()
      {
        this[this.tableTable2.BUHColumn] = Convert.DBNull;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class Table0RowChangeEvent : EventArgs
    {
      private InvoiceOutInvoice_DS.Table0Row eventRow;
      private DataRowAction eventAction;

      [DebuggerNonUserCode]
      public InvoiceOutInvoice_DS.Table0Row Row
      {
        get
        {
          return this.eventRow;
        }
      }

      [DebuggerNonUserCode]
      public DataRowAction Action
      {
        get
        {
          return this.eventAction;
        }
      }

      [DebuggerNonUserCode]
      public Table0RowChangeEvent(InvoiceOutInvoice_DS.Table0Row row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class Table1RowChangeEvent : EventArgs
    {
      private InvoiceOutInvoice_DS.Table1Row eventRow;
      private DataRowAction eventAction;

      [DebuggerNonUserCode]
      public InvoiceOutInvoice_DS.Table1Row Row
      {
        get
        {
          return this.eventRow;
        }
      }

      [DebuggerNonUserCode]
      public DataRowAction Action
      {
        get
        {
          return this.eventAction;
        }
      }

      [DebuggerNonUserCode]
      public Table1RowChangeEvent(InvoiceOutInvoice_DS.Table1Row row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class Table2RowChangeEvent : EventArgs
    {
      private InvoiceOutInvoice_DS.Table2Row eventRow;
      private DataRowAction eventAction;

      [DebuggerNonUserCode]
      public InvoiceOutInvoice_DS.Table2Row Row
      {
        get
        {
          return this.eventRow;
        }
      }

      [DebuggerNonUserCode]
      public DataRowAction Action
      {
        get
        {
          return this.eventAction;
        }
      }

      [DebuggerNonUserCode]
      public Table2RowChangeEvent(InvoiceOutInvoice_DS.Table2Row row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }
    }
  }
}
