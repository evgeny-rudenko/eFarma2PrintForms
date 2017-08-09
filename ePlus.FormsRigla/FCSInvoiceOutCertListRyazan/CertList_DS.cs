// Type: FCSInvoiceOutCertListRyazan.CertList_DS
// Assembly: FCSInvoiceOutCertListRyazan_7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 62F19D78-BE35-4424-992B-C749CDF51CF6
// Assembly location: D:\Work\eFarma\Rigla\FCSInvoiceOutCertListRyazan_7.dll

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

namespace FCSInvoiceOutCertListRyazan
{
  [DesignerCategory("code")]
  [HelpKeyword("vs.data.DataSet")]
  [ToolboxItem(true)]
  [XmlSchemaProvider("GetTypedDataSetSchema")]
  [XmlRoot("CertList_DS")]
  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
  [Serializable]
  public class CertList_DS : DataSet
  {
    private SchemaSerializationMode _schemaSerializationMode = SchemaSerializationMode.IncludeSchema;
    private CertList_DS.Table0DataTable tableTable0;
    private CertList_DS.Table1DataTable tableTable1;
    private CertList_DS.Table2DataTable tableTable2;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [DebuggerNonUserCode]
    [Browsable(false)]
    public CertList_DS.Table0DataTable Table0
    {
      get
      {
        return this.tableTable0;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [DebuggerNonUserCode]
    [Browsable(false)]
    public CertList_DS.Table1DataTable Table1
    {
      get
      {
        return this.tableTable1;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(false)]
    [DebuggerNonUserCode]
    public CertList_DS.Table2DataTable Table2
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

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DebuggerNonUserCode]
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
    public CertList_DS()
    {
      this.BeginInit();
      this.InitClass();
      CollectionChangeEventHandler changeEventHandler = new CollectionChangeEventHandler(this.SchemaChanged);
      base.Tables.CollectionChanged += changeEventHandler;
      base.Relations.CollectionChanged += changeEventHandler;
      this.EndInit();
    }

    [DebuggerNonUserCode]
    protected CertList_DS(SerializationInfo info, StreamingContext context)
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
            base.Tables.Add((DataTable) new CertList_DS.Table0DataTable(dataSet.Tables["Table0"]));
          if (dataSet.Tables["Table1"] != null)
            base.Tables.Add((DataTable) new CertList_DS.Table1DataTable(dataSet.Tables["Table1"]));
          if (dataSet.Tables["Table2"] != null)
            base.Tables.Add((DataTable) new CertList_DS.Table2DataTable(dataSet.Tables["Table2"]));
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
      CertList_DS certListDs = (CertList_DS) base.Clone();
      certListDs.InitVars();
      certListDs.SchemaSerializationMode = this.SchemaSerializationMode;
      return (DataSet) certListDs;
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
          base.Tables.Add((DataTable) new CertList_DS.Table0DataTable(dataSet.Tables["Table0"]));
        if (dataSet.Tables["Table1"] != null)
          base.Tables.Add((DataTable) new CertList_DS.Table1DataTable(dataSet.Tables["Table1"]));
        if (dataSet.Tables["Table2"] != null)
          base.Tables.Add((DataTable) new CertList_DS.Table2DataTable(dataSet.Tables["Table2"]));
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
      this.tableTable0 = (CertList_DS.Table0DataTable) base.Tables["Table0"];
      if (initTable && this.tableTable0 != null)
        this.tableTable0.InitVars();
      this.tableTable1 = (CertList_DS.Table1DataTable) base.Tables["Table1"];
      if (initTable && this.tableTable1 != null)
        this.tableTable1.InitVars();
      this.tableTable2 = (CertList_DS.Table2DataTable) base.Tables["Table2"];
      if (!initTable || this.tableTable2 == null)
        return;
      this.tableTable2.InitVars();
    }

    [DebuggerNonUserCode]
    private void InitClass()
    {
      this.DataSetName = "CertList_DS";
      this.Prefix = "";
      this.Namespace = "http://tempuri.org/CertList_DS.xsd";
      this.EnforceConstraints = true;
      this.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
      this.tableTable0 = new CertList_DS.Table0DataTable();
      base.Tables.Add((DataTable) this.tableTable0);
      this.tableTable1 = new CertList_DS.Table1DataTable();
      base.Tables.Add((DataTable) this.tableTable1);
      this.tableTable2 = new CertList_DS.Table2DataTable();
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
      CertList_DS certListDs = new CertList_DS();
      XmlSchemaComplexType schemaComplexType = new XmlSchemaComplexType();
      XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
      xmlSchemaSequence.Items.Add((XmlSchemaObject) new XmlSchemaAny()
      {
        Namespace = certListDs.Namespace
      });
      schemaComplexType.Particle = (XmlSchemaParticle) xmlSchemaSequence;
      XmlSchema schemaSerializable = certListDs.GetSchemaSerializable();
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

    public delegate void Table0RowChangeEventHandler(object sender, CertList_DS.Table0RowChangeEvent e);

    public delegate void Table1RowChangeEventHandler(object sender, CertList_DS.Table1RowChangeEvent e);

    public delegate void Table2RowChangeEventHandler(object sender, CertList_DS.Table2RowChangeEvent e);

    [XmlSchemaProvider("GetTypedTableSchema")]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    [Serializable]
    public class Table0DataTable : DataTable, IEnumerable
    {
      private DataColumn columnDOC_NUMBER;
      private DataColumn columnDOC_DATE;
      private DataColumn columnSUP_NAME;
      private DataColumn columnSUP_INN;
      private DataColumn columnSUP_ADDRESS;
      private DataColumn columnCONT_NAME;
      private DataColumn columnCON_INN;

      [DebuggerNonUserCode]
      public DataColumn DOC_NUMBERColumn
      {
        get
        {
          return this.columnDOC_NUMBER;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn DOC_DATEColumn
      {
        get
        {
          return this.columnDOC_DATE;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn SUP_NAMEColumn
      {
        get
        {
          return this.columnSUP_NAME;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn SUP_INNColumn
      {
        get
        {
          return this.columnSUP_INN;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn SUP_ADDRESSColumn
      {
        get
        {
          return this.columnSUP_ADDRESS;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn CONT_NAMEColumn
      {
        get
        {
          return this.columnCONT_NAME;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn CON_INNColumn
      {
        get
        {
          return this.columnCON_INN;
        }
      }

      [Browsable(false)]
      [DebuggerNonUserCode]
      public int Count
      {
        get
        {
          return this.Rows.Count;
        }
      }

      [DebuggerNonUserCode]
      public CertList_DS.Table0Row this[int index]
      {
        get
        {
          return (CertList_DS.Table0Row) this.Rows[index];
        }
      }

      public event CertList_DS.Table0RowChangeEventHandler Table0RowChanging;

      public event CertList_DS.Table0RowChangeEventHandler Table0RowChanged;

      public event CertList_DS.Table0RowChangeEventHandler Table0RowDeleting;

      public event CertList_DS.Table0RowChangeEventHandler Table0RowDeleted;

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
      public void AddTable0Row(CertList_DS.Table0Row row)
      {
        this.Rows.Add((DataRow) row);
      }

      [DebuggerNonUserCode]
      public CertList_DS.Table0Row AddTable0Row(string DOC_NUMBER, string DOC_DATE, string SUP_NAME, string SUP_INN, string SUP_ADDRESS, string CONT_NAME, string CON_INN)
      {
        CertList_DS.Table0Row table0Row = (CertList_DS.Table0Row) this.NewRow();
        object[] objArray = new object[7]
        {
          (object) DOC_NUMBER,
          (object) DOC_DATE,
          (object) SUP_NAME,
          (object) SUP_INN,
          (object) SUP_ADDRESS,
          (object) CONT_NAME,
          (object) CON_INN
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
        CertList_DS.Table0DataTable table0DataTable = (CertList_DS.Table0DataTable) base.Clone();
        table0DataTable.InitVars();
        return (DataTable) table0DataTable;
      }

      [DebuggerNonUserCode]
      protected override DataTable CreateInstance()
      {
        return (DataTable) new CertList_DS.Table0DataTable();
      }

      [DebuggerNonUserCode]
      internal void InitVars()
      {
        this.columnDOC_NUMBER = this.Columns["DOC_NUMBER"];
        this.columnDOC_DATE = this.Columns["DOC_DATE"];
        this.columnSUP_NAME = this.Columns["SUP_NAME"];
        this.columnSUP_INN = this.Columns["SUP_INN"];
        this.columnSUP_ADDRESS = this.Columns["SUP_ADDRESS"];
        this.columnCONT_NAME = this.Columns["CONT_NAME"];
        this.columnCON_INN = this.Columns["CON_INN"];
      }

      [DebuggerNonUserCode]
      private void InitClass()
      {
        this.columnDOC_NUMBER = new DataColumn("DOC_NUMBER", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnDOC_NUMBER);
        this.columnDOC_DATE = new DataColumn("DOC_DATE", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnDOC_DATE);
        this.columnSUP_NAME = new DataColumn("SUP_NAME", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnSUP_NAME);
        this.columnSUP_INN = new DataColumn("SUP_INN", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnSUP_INN);
        this.columnSUP_ADDRESS = new DataColumn("SUP_ADDRESS", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnSUP_ADDRESS);
        this.columnCONT_NAME = new DataColumn("CONT_NAME", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnCONT_NAME);
        this.columnCON_INN = new DataColumn("CON_INN", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnCON_INN);
      }

      [DebuggerNonUserCode]
      public CertList_DS.Table0Row NewTable0Row()
      {
        return (CertList_DS.Table0Row) this.NewRow();
      }

      [DebuggerNonUserCode]
      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return (DataRow) new CertList_DS.Table0Row(builder);
      }

      [DebuggerNonUserCode]
      protected override Type GetRowType()
      {
        return typeof (CertList_DS.Table0Row);
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if (this.Table0RowChanged == null)
          return;
        this.Table0RowChanged((object) this, new CertList_DS.Table0RowChangeEvent((CertList_DS.Table0Row) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if (this.Table0RowChanging == null)
          return;
        this.Table0RowChanging((object) this, new CertList_DS.Table0RowChangeEvent((CertList_DS.Table0Row) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if (this.Table0RowDeleted == null)
          return;
        this.Table0RowDeleted((object) this, new CertList_DS.Table0RowChangeEvent((CertList_DS.Table0Row) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if (this.Table0RowDeleting == null)
          return;
        this.Table0RowDeleting((object) this, new CertList_DS.Table0RowChangeEvent((CertList_DS.Table0Row) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      public void RemoveTable0Row(CertList_DS.Table0Row row)
      {
        this.Rows.Remove((DataRow) row);
      }

      [DebuggerNonUserCode]
      public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
      {
        XmlSchemaComplexType schemaComplexType = new XmlSchemaComplexType();
        XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
        CertList_DS certListDs = new CertList_DS();
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
          FixedValue = certListDs.Namespace
        });
        schemaComplexType.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "tableTypeName",
          FixedValue = "Table0DataTable"
        });
        schemaComplexType.Particle = (XmlSchemaParticle) xmlSchemaSequence;
        XmlSchema schemaSerializable = certListDs.GetSchemaSerializable();
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
    public class Table1DataTable : DataTable, IEnumerable
    {
      private DataColumn columnNN;
      private DataColumn columnGOODS_NAME;
      private DataColumn columnPRODUCER;
      private DataColumn columnSERIES_NUMBER;
      private DataColumn columnCERT_NUMBER;
      private DataColumn columnCERT_DATE;
      private DataColumn columnISSUED_BY;
      private DataColumn columnBEST_BEFORE;
      private DataColumn columnCOMMENT;
      private DataColumn columnREG_CERT_NAME;
      private DataColumn columnREG_CERT_DATE;

      [DebuggerNonUserCode]
      public DataColumn NNColumn
      {
        get
        {
          return this.columnNN;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn GOODS_NAMEColumn
      {
        get
        {
          return this.columnGOODS_NAME;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn PRODUCERColumn
      {
        get
        {
          return this.columnPRODUCER;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn SERIES_NUMBERColumn
      {
        get
        {
          return this.columnSERIES_NUMBER;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn CERT_NUMBERColumn
      {
        get
        {
          return this.columnCERT_NUMBER;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn CERT_DATEColumn
      {
        get
        {
          return this.columnCERT_DATE;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn ISSUED_BYColumn
      {
        get
        {
          return this.columnISSUED_BY;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn BEST_BEFOREColumn
      {
        get
        {
          return this.columnBEST_BEFORE;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn COMMENTColumn
      {
        get
        {
          return this.columnCOMMENT;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn REG_CERT_NAMEColumn
      {
        get
        {
          return this.columnREG_CERT_NAME;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn REG_CERT_DATEColumn
      {
        get
        {
          return this.columnREG_CERT_DATE;
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
      public CertList_DS.Table1Row this[int index]
      {
        get
        {
          return (CertList_DS.Table1Row) this.Rows[index];
        }
      }

      public event CertList_DS.Table1RowChangeEventHandler Table1RowChanging;

      public event CertList_DS.Table1RowChangeEventHandler Table1RowChanged;

      public event CertList_DS.Table1RowChangeEventHandler Table1RowDeleting;

      public event CertList_DS.Table1RowChangeEventHandler Table1RowDeleted;

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
      public void AddTable1Row(CertList_DS.Table1Row row)
      {
        this.Rows.Add((DataRow) row);
      }

      [DebuggerNonUserCode]
      public CertList_DS.Table1Row AddTable1Row(int NN, string GOODS_NAME, string PRODUCER, string SERIES_NUMBER, string CERT_NUMBER, DateTime CERT_DATE, string ISSUED_BY, DateTime BEST_BEFORE, string COMMENT, string REG_CERT_NAME, DateTime REG_CERT_DATE)
      {
        CertList_DS.Table1Row table1Row = (CertList_DS.Table1Row) this.NewRow();
        object[] objArray = new object[11]
        {
          (object) NN,
          (object) GOODS_NAME,
          (object) PRODUCER,
          (object) SERIES_NUMBER,
          (object) CERT_NUMBER,
          (object) CERT_DATE,
          (object) ISSUED_BY,
          (object) BEST_BEFORE,
          (object) COMMENT,
          (object) REG_CERT_NAME,
          (object) REG_CERT_DATE
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
        CertList_DS.Table1DataTable table1DataTable = (CertList_DS.Table1DataTable) base.Clone();
        table1DataTable.InitVars();
        return (DataTable) table1DataTable;
      }

      [DebuggerNonUserCode]
      protected override DataTable CreateInstance()
      {
        return (DataTable) new CertList_DS.Table1DataTable();
      }

      [DebuggerNonUserCode]
      internal void InitVars()
      {
        this.columnNN = this.Columns["NN"];
        this.columnGOODS_NAME = this.Columns["GOODS_NAME"];
        this.columnPRODUCER = this.Columns["PRODUCER"];
        this.columnSERIES_NUMBER = this.Columns["SERIES_NUMBER"];
        this.columnCERT_NUMBER = this.Columns["CERT_NUMBER"];
        this.columnCERT_DATE = this.Columns["CERT_DATE"];
        this.columnISSUED_BY = this.Columns["ISSUED_BY"];
        this.columnBEST_BEFORE = this.Columns["BEST_BEFORE"];
        this.columnCOMMENT = this.Columns["COMMENT"];
        this.columnREG_CERT_NAME = this.Columns["REG_CERT_NAME"];
        this.columnREG_CERT_DATE = this.Columns["REG_CERT_DATE"];
      }

      [DebuggerNonUserCode]
      private void InitClass()
      {
        this.columnNN = new DataColumn("NN", typeof (int), (string) null, MappingType.Element);
        this.Columns.Add(this.columnNN);
        this.columnGOODS_NAME = new DataColumn("GOODS_NAME", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnGOODS_NAME);
        this.columnPRODUCER = new DataColumn("PRODUCER", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnPRODUCER);
        this.columnSERIES_NUMBER = new DataColumn("SERIES_NUMBER", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnSERIES_NUMBER);
        this.columnCERT_NUMBER = new DataColumn("CERT_NUMBER", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnCERT_NUMBER);
        this.columnCERT_DATE = new DataColumn("CERT_DATE", typeof (DateTime), (string) null, MappingType.Element);
        this.Columns.Add(this.columnCERT_DATE);
        this.columnISSUED_BY = new DataColumn("ISSUED_BY", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnISSUED_BY);
        this.columnBEST_BEFORE = new DataColumn("BEST_BEFORE", typeof (DateTime), (string) null, MappingType.Element);
        this.Columns.Add(this.columnBEST_BEFORE);
        this.columnCOMMENT = new DataColumn("COMMENT", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnCOMMENT);
        this.columnREG_CERT_NAME = new DataColumn("REG_CERT_NAME", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnREG_CERT_NAME);
        this.columnREG_CERT_DATE = new DataColumn("REG_CERT_DATE", typeof (DateTime), (string) null, MappingType.Element);
        this.Columns.Add(this.columnREG_CERT_DATE);
      }

      [DebuggerNonUserCode]
      public CertList_DS.Table1Row NewTable1Row()
      {
        return (CertList_DS.Table1Row) this.NewRow();
      }

      [DebuggerNonUserCode]
      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return (DataRow) new CertList_DS.Table1Row(builder);
      }

      [DebuggerNonUserCode]
      protected override Type GetRowType()
      {
        return typeof (CertList_DS.Table1Row);
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if (this.Table1RowChanged == null)
          return;
        this.Table1RowChanged((object) this, new CertList_DS.Table1RowChangeEvent((CertList_DS.Table1Row) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if (this.Table1RowChanging == null)
          return;
        this.Table1RowChanging((object) this, new CertList_DS.Table1RowChangeEvent((CertList_DS.Table1Row) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if (this.Table1RowDeleted == null)
          return;
        this.Table1RowDeleted((object) this, new CertList_DS.Table1RowChangeEvent((CertList_DS.Table1Row) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if (this.Table1RowDeleting == null)
          return;
        this.Table1RowDeleting((object) this, new CertList_DS.Table1RowChangeEvent((CertList_DS.Table1Row) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      public void RemoveTable1Row(CertList_DS.Table1Row row)
      {
        this.Rows.Remove((DataRow) row);
      }

      [DebuggerNonUserCode]
      public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
      {
        XmlSchemaComplexType schemaComplexType = new XmlSchemaComplexType();
        XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
        CertList_DS certListDs = new CertList_DS();
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
          FixedValue = certListDs.Namespace
        });
        schemaComplexType.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "tableTypeName",
          FixedValue = "Table1DataTable"
        });
        schemaComplexType.Particle = (XmlSchemaParticle) xmlSchemaSequence;
        XmlSchema schemaSerializable = certListDs.GetSchemaSerializable();
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
      private DataColumn columnDIR;
      private DataColumn columnBUH;

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
      public CertList_DS.Table2Row this[int index]
      {
        get
        {
          return (CertList_DS.Table2Row) this.Rows[index];
        }
      }

      public event CertList_DS.Table2RowChangeEventHandler Table2RowChanging;

      public event CertList_DS.Table2RowChangeEventHandler Table2RowChanged;

      public event CertList_DS.Table2RowChangeEventHandler Table2RowDeleting;

      public event CertList_DS.Table2RowChangeEventHandler Table2RowDeleted;

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
      public void AddTable2Row(CertList_DS.Table2Row row)
      {
        this.Rows.Add((DataRow) row);
      }

      [DebuggerNonUserCode]
      public CertList_DS.Table2Row AddTable2Row(string DIR, string BUH)
      {
        CertList_DS.Table2Row table2Row = (CertList_DS.Table2Row) this.NewRow();
        object[] objArray = new object[2]
        {
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
        CertList_DS.Table2DataTable table2DataTable = (CertList_DS.Table2DataTable) base.Clone();
        table2DataTable.InitVars();
        return (DataTable) table2DataTable;
      }

      [DebuggerNonUserCode]
      protected override DataTable CreateInstance()
      {
        return (DataTable) new CertList_DS.Table2DataTable();
      }

      [DebuggerNonUserCode]
      internal void InitVars()
      {
        this.columnDIR = this.Columns["DIR"];
        this.columnBUH = this.Columns["BUH"];
      }

      [DebuggerNonUserCode]
      private void InitClass()
      {
        this.columnDIR = new DataColumn("DIR", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnDIR);
        this.columnBUH = new DataColumn("BUH", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnBUH);
      }

      [DebuggerNonUserCode]
      public CertList_DS.Table2Row NewTable2Row()
      {
        return (CertList_DS.Table2Row) this.NewRow();
      }

      [DebuggerNonUserCode]
      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return (DataRow) new CertList_DS.Table2Row(builder);
      }

      [DebuggerNonUserCode]
      protected override Type GetRowType()
      {
        return typeof (CertList_DS.Table2Row);
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if (this.Table2RowChanged == null)
          return;
        this.Table2RowChanged((object) this, new CertList_DS.Table2RowChangeEvent((CertList_DS.Table2Row) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if (this.Table2RowChanging == null)
          return;
        this.Table2RowChanging((object) this, new CertList_DS.Table2RowChangeEvent((CertList_DS.Table2Row) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if (this.Table2RowDeleted == null)
          return;
        this.Table2RowDeleted((object) this, new CertList_DS.Table2RowChangeEvent((CertList_DS.Table2Row) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if (this.Table2RowDeleting == null)
          return;
        this.Table2RowDeleting((object) this, new CertList_DS.Table2RowChangeEvent((CertList_DS.Table2Row) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      public void RemoveTable2Row(CertList_DS.Table2Row row)
      {
        this.Rows.Remove((DataRow) row);
      }

      [DebuggerNonUserCode]
      public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
      {
        XmlSchemaComplexType schemaComplexType = new XmlSchemaComplexType();
        XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
        CertList_DS certListDs = new CertList_DS();
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
          FixedValue = certListDs.Namespace
        });
        schemaComplexType.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "tableTypeName",
          FixedValue = "Table2DataTable"
        });
        schemaComplexType.Particle = (XmlSchemaParticle) xmlSchemaSequence;
        XmlSchema schemaSerializable = certListDs.GetSchemaSerializable();
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
      private CertList_DS.Table0DataTable tableTable0;

      [DebuggerNonUserCode]
      public string DOC_NUMBER
      {
        get
        {
          try
          {
            return (string) this[this.tableTable0.DOC_NUMBERColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'DOC_NUMBER' in table 'Table0' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable0.DOC_NUMBERColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public string DOC_DATE
      {
        get
        {
          try
          {
            return (string) this[this.tableTable0.DOC_DATEColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'DOC_DATE' in table 'Table0' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable0.DOC_DATEColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public string SUP_NAME
      {
        get
        {
          try
          {
            return (string) this[this.tableTable0.SUP_NAMEColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'SUP_NAME' in table 'Table0' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable0.SUP_NAMEColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public string SUP_INN
      {
        get
        {
          try
          {
            return (string) this[this.tableTable0.SUP_INNColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'SUP_INN' in table 'Table0' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable0.SUP_INNColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public string SUP_ADDRESS
      {
        get
        {
          try
          {
            return (string) this[this.tableTable0.SUP_ADDRESSColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'SUP_ADDRESS' in table 'Table0' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable0.SUP_ADDRESSColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public string CONT_NAME
      {
        get
        {
          try
          {
            return (string) this[this.tableTable0.CONT_NAMEColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'CONT_NAME' in table 'Table0' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable0.CONT_NAMEColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public string CON_INN
      {
        get
        {
          try
          {
            return (string) this[this.tableTable0.CON_INNColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'CON_INN' in table 'Table0' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable0.CON_INNColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      internal Table0Row(DataRowBuilder rb)
        : base(rb)
      {
        this.tableTable0 = (CertList_DS.Table0DataTable) this.Table;
      }

      [DebuggerNonUserCode]
      public bool IsDOC_NUMBERNull()
      {
        return this.IsNull(this.tableTable0.DOC_NUMBERColumn);
      }

      [DebuggerNonUserCode]
      public void SetDOC_NUMBERNull()
      {
        this[this.tableTable0.DOC_NUMBERColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsDOC_DATENull()
      {
        return this.IsNull(this.tableTable0.DOC_DATEColumn);
      }

      [DebuggerNonUserCode]
      public void SetDOC_DATENull()
      {
        this[this.tableTable0.DOC_DATEColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsSUP_NAMENull()
      {
        return this.IsNull(this.tableTable0.SUP_NAMEColumn);
      }

      [DebuggerNonUserCode]
      public void SetSUP_NAMENull()
      {
        this[this.tableTable0.SUP_NAMEColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsSUP_INNNull()
      {
        return this.IsNull(this.tableTable0.SUP_INNColumn);
      }

      [DebuggerNonUserCode]
      public void SetSUP_INNNull()
      {
        this[this.tableTable0.SUP_INNColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsSUP_ADDRESSNull()
      {
        return this.IsNull(this.tableTable0.SUP_ADDRESSColumn);
      }

      [DebuggerNonUserCode]
      public void SetSUP_ADDRESSNull()
      {
        this[this.tableTable0.SUP_ADDRESSColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsCONT_NAMENull()
      {
        return this.IsNull(this.tableTable0.CONT_NAMEColumn);
      }

      [DebuggerNonUserCode]
      public void SetCONT_NAMENull()
      {
        this[this.tableTable0.CONT_NAMEColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsCON_INNNull()
      {
        return this.IsNull(this.tableTable0.CON_INNColumn);
      }

      [DebuggerNonUserCode]
      public void SetCON_INNNull()
      {
        this[this.tableTable0.CON_INNColumn] = Convert.DBNull;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class Table1Row : DataRow
    {
      private CertList_DS.Table1DataTable tableTable1;

      [DebuggerNonUserCode]
      public int NN
      {
        get
        {
          try
          {
            return (int) this[this.tableTable1.NNColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'NN' in table 'Table1' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable1.NNColumn] = (object) value;
        }
      }

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
      public string PRODUCER
      {
        get
        {
          try
          {
            return (string) this[this.tableTable1.PRODUCERColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'PRODUCER' in table 'Table1' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable1.PRODUCERColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public string SERIES_NUMBER
      {
        get
        {
          try
          {
            return (string) this[this.tableTable1.SERIES_NUMBERColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'SERIES_NUMBER' in table 'Table1' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable1.SERIES_NUMBERColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public string CERT_NUMBER
      {
        get
        {
          try
          {
            return (string) this[this.tableTable1.CERT_NUMBERColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'CERT_NUMBER' in table 'Table1' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable1.CERT_NUMBERColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public DateTime CERT_DATE
      {
        get
        {
          try
          {
            return (DateTime) this[this.tableTable1.CERT_DATEColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'CERT_DATE' in table 'Table1' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable1.CERT_DATEColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public string ISSUED_BY
      {
        get
        {
          try
          {
            return (string) this[this.tableTable1.ISSUED_BYColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'ISSUED_BY' in table 'Table1' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable1.ISSUED_BYColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public DateTime BEST_BEFORE
      {
        get
        {
          try
          {
            return (DateTime) this[this.tableTable1.BEST_BEFOREColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'BEST_BEFORE' in table 'Table1' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable1.BEST_BEFOREColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public string COMMENT
      {
        get
        {
          try
          {
            return (string) this[this.tableTable1.COMMENTColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'COMMENT' in table 'Table1' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable1.COMMENTColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public string REG_CERT_NAME
      {
        get
        {
          try
          {
            return (string) this[this.tableTable1.REG_CERT_NAMEColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'REG_CERT_NAME' in table 'Table1' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable1.REG_CERT_NAMEColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public DateTime REG_CERT_DATE
      {
        get
        {
          try
          {
            return (DateTime) this[this.tableTable1.REG_CERT_DATEColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'REG_CERT_DATE' in table 'Table1' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable1.REG_CERT_DATEColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      internal Table1Row(DataRowBuilder rb)
        : base(rb)
      {
        this.tableTable1 = (CertList_DS.Table1DataTable) this.Table;
      }

      [DebuggerNonUserCode]
      public bool IsNNNull()
      {
        return this.IsNull(this.tableTable1.NNColumn);
      }

      [DebuggerNonUserCode]
      public void SetNNNull()
      {
        this[this.tableTable1.NNColumn] = Convert.DBNull;
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
      public bool IsPRODUCERNull()
      {
        return this.IsNull(this.tableTable1.PRODUCERColumn);
      }

      [DebuggerNonUserCode]
      public void SetPRODUCERNull()
      {
        this[this.tableTable1.PRODUCERColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsSERIES_NUMBERNull()
      {
        return this.IsNull(this.tableTable1.SERIES_NUMBERColumn);
      }

      [DebuggerNonUserCode]
      public void SetSERIES_NUMBERNull()
      {
        this[this.tableTable1.SERIES_NUMBERColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsCERT_NUMBERNull()
      {
        return this.IsNull(this.tableTable1.CERT_NUMBERColumn);
      }

      [DebuggerNonUserCode]
      public void SetCERT_NUMBERNull()
      {
        this[this.tableTable1.CERT_NUMBERColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsCERT_DATENull()
      {
        return this.IsNull(this.tableTable1.CERT_DATEColumn);
      }

      [DebuggerNonUserCode]
      public void SetCERT_DATENull()
      {
        this[this.tableTable1.CERT_DATEColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsISSUED_BYNull()
      {
        return this.IsNull(this.tableTable1.ISSUED_BYColumn);
      }

      [DebuggerNonUserCode]
      public void SetISSUED_BYNull()
      {
        this[this.tableTable1.ISSUED_BYColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsBEST_BEFORENull()
      {
        return this.IsNull(this.tableTable1.BEST_BEFOREColumn);
      }

      [DebuggerNonUserCode]
      public void SetBEST_BEFORENull()
      {
        this[this.tableTable1.BEST_BEFOREColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsCOMMENTNull()
      {
        return this.IsNull(this.tableTable1.COMMENTColumn);
      }

      [DebuggerNonUserCode]
      public void SetCOMMENTNull()
      {
        this[this.tableTable1.COMMENTColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsREG_CERT_NAMENull()
      {
        return this.IsNull(this.tableTable1.REG_CERT_NAMEColumn);
      }

      [DebuggerNonUserCode]
      public void SetREG_CERT_NAMENull()
      {
        this[this.tableTable1.REG_CERT_NAMEColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsREG_CERT_DATENull()
      {
        return this.IsNull(this.tableTable1.REG_CERT_DATEColumn);
      }

      [DebuggerNonUserCode]
      public void SetREG_CERT_DATENull()
      {
        this[this.tableTable1.REG_CERT_DATEColumn] = Convert.DBNull;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class Table2Row : DataRow
    {
      private CertList_DS.Table2DataTable tableTable2;

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
        this.tableTable2 = (CertList_DS.Table2DataTable) this.Table;
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
      private CertList_DS.Table0Row eventRow;
      private DataRowAction eventAction;

      [DebuggerNonUserCode]
      public CertList_DS.Table0Row Row
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
      public Table0RowChangeEvent(CertList_DS.Table0Row row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class Table1RowChangeEvent : EventArgs
    {
      private CertList_DS.Table1Row eventRow;
      private DataRowAction eventAction;

      [DebuggerNonUserCode]
      public CertList_DS.Table1Row Row
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
      public Table1RowChangeEvent(CertList_DS.Table1Row row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class Table2RowChangeEvent : EventArgs
    {
      private CertList_DS.Table2Row eventRow;
      private DataRowAction eventAction;

      [DebuggerNonUserCode]
      public CertList_DS.Table2Row Row
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
      public Table2RowChangeEvent(CertList_DS.Table2Row row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }
    }
  }
}
