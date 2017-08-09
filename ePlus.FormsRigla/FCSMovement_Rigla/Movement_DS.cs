// Type: FCSMovement_Rigla.Movement_DS
// Assembly: FCSMovement_Rigla_7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 91072545-6A2F-4365-978D-1647934ECF6A
// Assembly location: D:\Work\eFarma\Rigla\reports 516.4\FCSMovement_Rigla_7.dll

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

namespace FCSMovement_Rigla
{
  [DesignerCategory("code")]
  [HelpKeyword("vs.data.DataSet")]
  [ToolboxItem(true)]
  [XmlSchemaProvider("GetTypedDataSetSchema")]
  [XmlRoot("Movement_DS")]
  [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
  [Serializable]
  public class Movement_DS : DataSet
  {
    private SchemaSerializationMode _schemaSerializationMode = SchemaSerializationMode.IncludeSchema;
    private Movement_DS.Table0DataTable tableTable0;
    private Movement_DS.Table1DataTable tableTable1;
    private Movement_DS.Table2DataTable tableTable2;
    private Movement_DS.Table3DataTable tableTable3;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [DebuggerNonUserCode]
    [Browsable(false)]
    public Movement_DS.Table0DataTable Table0
    {
      get
      {
        return this.tableTable0;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [DebuggerNonUserCode]
    [Browsable(false)]
    public Movement_DS.Table1DataTable Table1
    {
      get
      {
        return this.tableTable1;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(false)]
    [DebuggerNonUserCode]
    public Movement_DS.Table2DataTable Table2
    {
      get
      {
        return this.tableTable2;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(false)]
    [DebuggerNonUserCode]
    public Movement_DS.Table3DataTable Table3
    {
      get
      {
        return this.tableTable3;
      }
    }

    [Browsable(true)]
    [DebuggerNonUserCode]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
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

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DebuggerNonUserCode]
    public new DataRelationCollection Relations
    {
      get
      {
        return base.Relations;
      }
    }

    [DebuggerNonUserCode]
    public Movement_DS()
    {
      this.BeginInit();
      this.InitClass();
      CollectionChangeEventHandler changeEventHandler = new CollectionChangeEventHandler(this.SchemaChanged);
      base.Tables.CollectionChanged += changeEventHandler;
      base.Relations.CollectionChanged += changeEventHandler;
      this.EndInit();
    }

    [DebuggerNonUserCode]
    protected Movement_DS(SerializationInfo info, StreamingContext context)
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
            base.Tables.Add((DataTable) new Movement_DS.Table0DataTable(dataSet.Tables["Table0"]));
          if (dataSet.Tables["Table1"] != null)
            base.Tables.Add((DataTable) new Movement_DS.Table1DataTable(dataSet.Tables["Table1"]));
          if (dataSet.Tables["Table2"] != null)
            base.Tables.Add((DataTable) new Movement_DS.Table2DataTable(dataSet.Tables["Table2"]));
          if (dataSet.Tables["Table3"] != null)
            base.Tables.Add((DataTable) new Movement_DS.Table3DataTable(dataSet.Tables["Table3"]));
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
      Movement_DS movementDs = (Movement_DS) base.Clone();
      movementDs.InitVars();
      movementDs.SchemaSerializationMode = this.SchemaSerializationMode;
      return (DataSet) movementDs;
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
          base.Tables.Add((DataTable) new Movement_DS.Table0DataTable(dataSet.Tables["Table0"]));
        if (dataSet.Tables["Table1"] != null)
          base.Tables.Add((DataTable) new Movement_DS.Table1DataTable(dataSet.Tables["Table1"]));
        if (dataSet.Tables["Table2"] != null)
          base.Tables.Add((DataTable) new Movement_DS.Table2DataTable(dataSet.Tables["Table2"]));
        if (dataSet.Tables["Table3"] != null)
          base.Tables.Add((DataTable) new Movement_DS.Table3DataTable(dataSet.Tables["Table3"]));
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
      this.tableTable0 = (Movement_DS.Table0DataTable) base.Tables["Table0"];
      if (initTable && this.tableTable0 != null)
        this.tableTable0.InitVars();
      this.tableTable1 = (Movement_DS.Table1DataTable) base.Tables["Table1"];
      if (initTable && this.tableTable1 != null)
        this.tableTable1.InitVars();
      this.tableTable2 = (Movement_DS.Table2DataTable) base.Tables["Table2"];
      if (initTable && this.tableTable2 != null)
        this.tableTable2.InitVars();
      this.tableTable3 = (Movement_DS.Table3DataTable) base.Tables["Table3"];
      if (!initTable || this.tableTable3 == null)
        return;
      this.tableTable3.InitVars();
    }

    [DebuggerNonUserCode]
    private void InitClass()
    {
      this.DataSetName = "Movement_DS";
      this.Prefix = "";
      this.Namespace = "http://tempuri.org/Movement_DS.xsd";
      this.EnforceConstraints = true;
      this.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
      this.tableTable0 = new Movement_DS.Table0DataTable();
      base.Tables.Add((DataTable) this.tableTable0);
      this.tableTable1 = new Movement_DS.Table1DataTable();
      base.Tables.Add((DataTable) this.tableTable1);
      this.tableTable2 = new Movement_DS.Table2DataTable();
      base.Tables.Add((DataTable) this.tableTable2);
      this.tableTable3 = new Movement_DS.Table3DataTable();
      base.Tables.Add((DataTable) this.tableTable3);
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
    private bool ShouldSerializeTable3()
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
      Movement_DS movementDs = new Movement_DS();
      XmlSchemaComplexType schemaComplexType = new XmlSchemaComplexType();
      XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
      xmlSchemaSequence.Items.Add((XmlSchemaObject) new XmlSchemaAny()
      {
        Namespace = movementDs.Namespace
      });
      schemaComplexType.Particle = (XmlSchemaParticle) xmlSchemaSequence;
      XmlSchema schemaSerializable = movementDs.GetSchemaSerializable();
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

    public delegate void Table0RowChangeEventHandler(object sender, Movement_DS.Table0RowChangeEvent e);

    public delegate void Table1RowChangeEventHandler(object sender, Movement_DS.Table1RowChangeEvent e);

    public delegate void Table2RowChangeEventHandler(object sender, Movement_DS.Table2RowChangeEvent e);

    public delegate void Table3RowChangeEventHandler(object sender, Movement_DS.Table3RowChangeEvent e);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    [XmlSchemaProvider("GetTypedTableSchema")]
    [Serializable]
    public class Table0DataTable : DataTable, IEnumerable
    {
      private DataColumn columnDOC_NUM;
      private DataColumn columnDOC_DATE;
      private DataColumn columnCONTRACTOR_TO;
      private DataColumn columnAUTH_VALID;

      [DebuggerNonUserCode]
      public DataColumn DOC_NUMColumn
      {
        get
        {
          return this.columnDOC_NUM;
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
      public DataColumn CONTRACTOR_TOColumn
      {
        get
        {
          return this.columnCONTRACTOR_TO;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn AUTH_VALIDColumn
      {
        get
        {
          return this.columnAUTH_VALID;
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
      public Movement_DS.Table0Row this[int index]
      {
        get
        {
          return (Movement_DS.Table0Row) this.Rows[index];
        }
      }

      public event Movement_DS.Table0RowChangeEventHandler Table0RowChanging;

      public event Movement_DS.Table0RowChangeEventHandler Table0RowChanged;

      public event Movement_DS.Table0RowChangeEventHandler Table0RowDeleting;

      public event Movement_DS.Table0RowChangeEventHandler Table0RowDeleted;

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
      public void AddTable0Row(Movement_DS.Table0Row row)
      {
        this.Rows.Add((DataRow) row);
      }

      [DebuggerNonUserCode]
      public Movement_DS.Table0Row AddTable0Row(string DOC_NUM, string DOC_DATE, string CONTRACTOR_TO, string AUTH_VALID)
      {
        Movement_DS.Table0Row table0Row = (Movement_DS.Table0Row) this.NewRow();
        object[] objArray = new object[4]
        {
          (object) DOC_NUM,
          (object) DOC_DATE,
          (object) CONTRACTOR_TO,
          (object) AUTH_VALID
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
        Movement_DS.Table0DataTable table0DataTable = (Movement_DS.Table0DataTable) base.Clone();
        table0DataTable.InitVars();
        return (DataTable) table0DataTable;
      }

      [DebuggerNonUserCode]
      protected override DataTable CreateInstance()
      {
        return (DataTable) new Movement_DS.Table0DataTable();
      }

      [DebuggerNonUserCode]
      internal void InitVars()
      {
        this.columnDOC_NUM = this.Columns["DOC_NUM"];
        this.columnDOC_DATE = this.Columns["DOC_DATE"];
        this.columnCONTRACTOR_TO = this.Columns["CONTRACTOR_TO"];
        this.columnAUTH_VALID = this.Columns["AUTH_VALID"];
      }

      [DebuggerNonUserCode]
      private void InitClass()
      {
        this.columnDOC_NUM = new DataColumn("DOC_NUM", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnDOC_NUM);
        this.columnDOC_DATE = new DataColumn("DOC_DATE", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnDOC_DATE);
        this.columnCONTRACTOR_TO = new DataColumn("CONTRACTOR_TO", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnCONTRACTOR_TO);
        this.columnAUTH_VALID = new DataColumn("AUTH_VALID", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnAUTH_VALID);
      }

      [DebuggerNonUserCode]
      public Movement_DS.Table0Row NewTable0Row()
      {
        return (Movement_DS.Table0Row) this.NewRow();
      }

      [DebuggerNonUserCode]
      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return (DataRow) new Movement_DS.Table0Row(builder);
      }

      [DebuggerNonUserCode]
      protected override Type GetRowType()
      {
        return typeof (Movement_DS.Table0Row);
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if (this.Table0RowChanged == null)
          return;
        this.Table0RowChanged((object) this, new Movement_DS.Table0RowChangeEvent((Movement_DS.Table0Row) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if (this.Table0RowChanging == null)
          return;
        this.Table0RowChanging((object) this, new Movement_DS.Table0RowChangeEvent((Movement_DS.Table0Row) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if (this.Table0RowDeleted == null)
          return;
        this.Table0RowDeleted((object) this, new Movement_DS.Table0RowChangeEvent((Movement_DS.Table0Row) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if (this.Table0RowDeleting == null)
          return;
        this.Table0RowDeleting((object) this, new Movement_DS.Table0RowChangeEvent((Movement_DS.Table0Row) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      public void RemoveTable0Row(Movement_DS.Table0Row row)
      {
        this.Rows.Remove((DataRow) row);
      }

      [DebuggerNonUserCode]
      public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
      {
        XmlSchemaComplexType schemaComplexType = new XmlSchemaComplexType();
        XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
        Movement_DS movementDs = new Movement_DS();
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
          FixedValue = movementDs.Namespace
        });
        schemaComplexType.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "tableTypeName",
          FixedValue = "Table0DataTable"
        });
        schemaComplexType.Particle = (XmlSchemaParticle) xmlSchemaSequence;
        XmlSchema schemaSerializable = movementDs.GetSchemaSerializable();
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
      private DataColumn columnGOODS_NAME;
      private DataColumn columnQUANTITY;
      private DataColumn columnPRICE_SAL;
      private DataColumn columnSUM_SAL;

      [DebuggerNonUserCode]
      public DataColumn GOODS_NAMEColumn
      {
        get
        {
          return this.columnGOODS_NAME;
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
      public DataColumn SUM_SALColumn
      {
        get
        {
          return this.columnSUM_SAL;
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
      public Movement_DS.Table1Row this[int index]
      {
        get
        {
          return (Movement_DS.Table1Row) this.Rows[index];
        }
      }

      public event Movement_DS.Table1RowChangeEventHandler Table1RowChanging;

      public event Movement_DS.Table1RowChangeEventHandler Table1RowChanged;

      public event Movement_DS.Table1RowChangeEventHandler Table1RowDeleting;

      public event Movement_DS.Table1RowChangeEventHandler Table1RowDeleted;

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
      public void AddTable1Row(Movement_DS.Table1Row row)
      {
        this.Rows.Add((DataRow) row);
      }

      [DebuggerNonUserCode]
      public Movement_DS.Table1Row AddTable1Row(string GOODS_NAME, string QUANTITY, string PRICE_SAL, string SUM_SAL)
      {
        Movement_DS.Table1Row table1Row = (Movement_DS.Table1Row) this.NewRow();
        object[] objArray = new object[4]
        {
          (object) GOODS_NAME,
          (object) QUANTITY,
          (object) PRICE_SAL,
          (object) SUM_SAL
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
        Movement_DS.Table1DataTable table1DataTable = (Movement_DS.Table1DataTable) base.Clone();
        table1DataTable.InitVars();
        return (DataTable) table1DataTable;
      }

      [DebuggerNonUserCode]
      protected override DataTable CreateInstance()
      {
        return (DataTable) new Movement_DS.Table1DataTable();
      }

      [DebuggerNonUserCode]
      internal void InitVars()
      {
        this.columnGOODS_NAME = this.Columns["GOODS_NAME"];
        this.columnQUANTITY = this.Columns["QUANTITY"];
        this.columnPRICE_SAL = this.Columns["PRICE_SAL"];
        this.columnSUM_SAL = this.Columns["SUM_SAL"];
      }

      [DebuggerNonUserCode]
      private void InitClass()
      {
        this.columnGOODS_NAME = new DataColumn("GOODS_NAME", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnGOODS_NAME);
        this.columnQUANTITY = new DataColumn("QUANTITY", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnQUANTITY);
        this.columnPRICE_SAL = new DataColumn("PRICE_SAL", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnPRICE_SAL);
        this.columnSUM_SAL = new DataColumn("SUM_SAL", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnSUM_SAL);
      }

      [DebuggerNonUserCode]
      public Movement_DS.Table1Row NewTable1Row()
      {
        return (Movement_DS.Table1Row) this.NewRow();
      }

      [DebuggerNonUserCode]
      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return (DataRow) new Movement_DS.Table1Row(builder);
      }

      [DebuggerNonUserCode]
      protected override Type GetRowType()
      {
        return typeof (Movement_DS.Table1Row);
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if (this.Table1RowChanged == null)
          return;
        this.Table1RowChanged((object) this, new Movement_DS.Table1RowChangeEvent((Movement_DS.Table1Row) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if (this.Table1RowChanging == null)
          return;
        this.Table1RowChanging((object) this, new Movement_DS.Table1RowChangeEvent((Movement_DS.Table1Row) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if (this.Table1RowDeleted == null)
          return;
        this.Table1RowDeleted((object) this, new Movement_DS.Table1RowChangeEvent((Movement_DS.Table1Row) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if (this.Table1RowDeleting == null)
          return;
        this.Table1RowDeleting((object) this, new Movement_DS.Table1RowChangeEvent((Movement_DS.Table1Row) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      public void RemoveTable1Row(Movement_DS.Table1Row row)
      {
        this.Rows.Remove((DataRow) row);
      }

      [DebuggerNonUserCode]
      public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
      {
        XmlSchemaComplexType schemaComplexType = new XmlSchemaComplexType();
        XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
        Movement_DS movementDs = new Movement_DS();
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
          FixedValue = movementDs.Namespace
        });
        schemaComplexType.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "tableTypeName",
          FixedValue = "Table1DataTable"
        });
        schemaComplexType.Particle = (XmlSchemaParticle) xmlSchemaSequence;
        XmlSchema schemaSerializable = movementDs.GetSchemaSerializable();
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
      private DataColumn columnSELF_NAME;
      private DataColumn columnDIR;
      private DataColumn columnADDRESS;

      [DebuggerNonUserCode]
      public DataColumn SELF_NAMEColumn
      {
        get
        {
          return this.columnSELF_NAME;
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
      public DataColumn ADDRESSColumn
      {
        get
        {
          return this.columnADDRESS;
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
      public Movement_DS.Table2Row this[int index]
      {
        get
        {
          return (Movement_DS.Table2Row) this.Rows[index];
        }
      }

      public event Movement_DS.Table2RowChangeEventHandler Table2RowChanging;

      public event Movement_DS.Table2RowChangeEventHandler Table2RowChanged;

      public event Movement_DS.Table2RowChangeEventHandler Table2RowDeleting;

      public event Movement_DS.Table2RowChangeEventHandler Table2RowDeleted;

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
      public void AddTable2Row(Movement_DS.Table2Row row)
      {
        this.Rows.Add((DataRow) row);
      }

      [DebuggerNonUserCode]
      public Movement_DS.Table2Row AddTable2Row(string SELF_NAME, string DIR, string ADDRESS)
      {
        Movement_DS.Table2Row table2Row = (Movement_DS.Table2Row) this.NewRow();
        object[] objArray = new object[3]
        {
          (object) SELF_NAME,
          (object) DIR,
          (object) ADDRESS
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
        Movement_DS.Table2DataTable table2DataTable = (Movement_DS.Table2DataTable) base.Clone();
        table2DataTable.InitVars();
        return (DataTable) table2DataTable;
      }

      [DebuggerNonUserCode]
      protected override DataTable CreateInstance()
      {
        return (DataTable) new Movement_DS.Table2DataTable();
      }

      [DebuggerNonUserCode]
      internal void InitVars()
      {
        this.columnSELF_NAME = this.Columns["SELF_NAME"];
        this.columnDIR = this.Columns["DIR"];
        this.columnADDRESS = this.Columns["ADDRESS"];
      }

      [DebuggerNonUserCode]
      private void InitClass()
      {
        this.columnSELF_NAME = new DataColumn("SELF_NAME", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnSELF_NAME);
        this.columnDIR = new DataColumn("DIR", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnDIR);
        this.columnADDRESS = new DataColumn("ADDRESS", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnADDRESS);
      }

      [DebuggerNonUserCode]
      public Movement_DS.Table2Row NewTable2Row()
      {
        return (Movement_DS.Table2Row) this.NewRow();
      }

      [DebuggerNonUserCode]
      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return (DataRow) new Movement_DS.Table2Row(builder);
      }

      [DebuggerNonUserCode]
      protected override Type GetRowType()
      {
        return typeof (Movement_DS.Table2Row);
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if (this.Table2RowChanged == null)
          return;
        this.Table2RowChanged((object) this, new Movement_DS.Table2RowChangeEvent((Movement_DS.Table2Row) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if (this.Table2RowChanging == null)
          return;
        this.Table2RowChanging((object) this, new Movement_DS.Table2RowChangeEvent((Movement_DS.Table2Row) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if (this.Table2RowDeleted == null)
          return;
        this.Table2RowDeleted((object) this, new Movement_DS.Table2RowChangeEvent((Movement_DS.Table2Row) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if (this.Table2RowDeleting == null)
          return;
        this.Table2RowDeleting((object) this, new Movement_DS.Table2RowChangeEvent((Movement_DS.Table2Row) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      public void RemoveTable2Row(Movement_DS.Table2Row row)
      {
        this.Rows.Remove((DataRow) row);
      }

      [DebuggerNonUserCode]
      public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
      {
        XmlSchemaComplexType schemaComplexType = new XmlSchemaComplexType();
        XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
        Movement_DS movementDs = new Movement_DS();
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
          FixedValue = movementDs.Namespace
        });
        schemaComplexType.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "tableTypeName",
          FixedValue = "Table2DataTable"
        });
        schemaComplexType.Particle = (XmlSchemaParticle) xmlSchemaSequence;
        XmlSchema schemaSerializable = movementDs.GetSchemaSerializable();
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
    public class Table3DataTable : DataTable, IEnumerable
    {
      private DataColumn columnC_NAME;
      private DataColumn columnC_DATE;
      private DataColumn columnA_NUM;
      private DataColumn columnA_DATE;

      [DebuggerNonUserCode]
      public DataColumn C_NAMEColumn
      {
        get
        {
          return this.columnC_NAME;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn C_DATEColumn
      {
        get
        {
          return this.columnC_DATE;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn A_NUMColumn
      {
        get
        {
          return this.columnA_NUM;
        }
      }

      [DebuggerNonUserCode]
      public DataColumn A_DATEColumn
      {
        get
        {
          return this.columnA_DATE;
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
      public Movement_DS.Table3Row this[int index]
      {
        get
        {
          return (Movement_DS.Table3Row) this.Rows[index];
        }
      }

      public event Movement_DS.Table3RowChangeEventHandler Table3RowChanging;

      public event Movement_DS.Table3RowChangeEventHandler Table3RowChanged;

      public event Movement_DS.Table3RowChangeEventHandler Table3RowDeleting;

      public event Movement_DS.Table3RowChangeEventHandler Table3RowDeleted;

      [DebuggerNonUserCode]
      public Table3DataTable()
      {
        this.TableName = "Table3";
        this.BeginInit();
        this.InitClass();
        this.EndInit();
      }

      [DebuggerNonUserCode]
      internal Table3DataTable(DataTable table)
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
      protected Table3DataTable(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this.InitVars();
      }

      [DebuggerNonUserCode]
      public void AddTable3Row(Movement_DS.Table3Row row)
      {
        this.Rows.Add((DataRow) row);
      }

      [DebuggerNonUserCode]
      public Movement_DS.Table3Row AddTable3Row(string C_NAME, string C_DATE, string A_NUM, string A_DATE)
      {
        Movement_DS.Table3Row table3Row = (Movement_DS.Table3Row) this.NewRow();
        object[] objArray = new object[4]
        {
          (object) C_NAME,
          (object) C_DATE,
          (object) A_NUM,
          (object) A_DATE
        };
        table3Row.ItemArray = objArray;
        this.Rows.Add((DataRow) table3Row);
        return table3Row;
      }

      [DebuggerNonUserCode]
      public virtual IEnumerator GetEnumerator()
      {
        return this.Rows.GetEnumerator();
      }

      [DebuggerNonUserCode]
      public override DataTable Clone()
      {
        Movement_DS.Table3DataTable table3DataTable = (Movement_DS.Table3DataTable) base.Clone();
        table3DataTable.InitVars();
        return (DataTable) table3DataTable;
      }

      [DebuggerNonUserCode]
      protected override DataTable CreateInstance()
      {
        return (DataTable) new Movement_DS.Table3DataTable();
      }

      [DebuggerNonUserCode]
      internal void InitVars()
      {
        this.columnC_NAME = this.Columns["C_NAME"];
        this.columnC_DATE = this.Columns["C_DATE"];
        this.columnA_NUM = this.Columns["A_NUM"];
        this.columnA_DATE = this.Columns["A_DATE"];
      }

      [DebuggerNonUserCode]
      private void InitClass()
      {
        this.columnC_NAME = new DataColumn("C_NAME", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnC_NAME);
        this.columnC_DATE = new DataColumn("C_DATE", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnC_DATE);
        this.columnA_NUM = new DataColumn("A_NUM", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnA_NUM);
        this.columnA_DATE = new DataColumn("A_DATE", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnA_DATE);
      }

      [DebuggerNonUserCode]
      public Movement_DS.Table3Row NewTable3Row()
      {
        return (Movement_DS.Table3Row) this.NewRow();
      }

      [DebuggerNonUserCode]
      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return (DataRow) new Movement_DS.Table3Row(builder);
      }

      [DebuggerNonUserCode]
      protected override Type GetRowType()
      {
        return typeof (Movement_DS.Table3Row);
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if (this.Table3RowChanged == null)
          return;
        this.Table3RowChanged((object) this, new Movement_DS.Table3RowChangeEvent((Movement_DS.Table3Row) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if (this.Table3RowChanging == null)
          return;
        this.Table3RowChanging((object) this, new Movement_DS.Table3RowChangeEvent((Movement_DS.Table3Row) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if (this.Table3RowDeleted == null)
          return;
        this.Table3RowDeleted((object) this, new Movement_DS.Table3RowChangeEvent((Movement_DS.Table3Row) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if (this.Table3RowDeleting == null)
          return;
        this.Table3RowDeleting((object) this, new Movement_DS.Table3RowChangeEvent((Movement_DS.Table3Row) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      public void RemoveTable3Row(Movement_DS.Table3Row row)
      {
        this.Rows.Remove((DataRow) row);
      }

      [DebuggerNonUserCode]
      public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
      {
        XmlSchemaComplexType schemaComplexType = new XmlSchemaComplexType();
        XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
        Movement_DS movementDs = new Movement_DS();
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
          FixedValue = movementDs.Namespace
        });
        schemaComplexType.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "tableTypeName",
          FixedValue = "Table3DataTable"
        });
        schemaComplexType.Particle = (XmlSchemaParticle) xmlSchemaSequence;
        XmlSchema schemaSerializable = movementDs.GetSchemaSerializable();
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
      private Movement_DS.Table0DataTable tableTable0;

      [DebuggerNonUserCode]
      public string DOC_NUM
      {
        get
        {
          try
          {
            return (string) this[this.tableTable0.DOC_NUMColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'DOC_NUM' in table 'Table0' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable0.DOC_NUMColumn] = (object) value;
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
      public string CONTRACTOR_TO
      {
        get
        {
          try
          {
            return (string) this[this.tableTable0.CONTRACTOR_TOColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'CONTRACTOR_TO' in table 'Table0' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable0.CONTRACTOR_TOColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public string AUTH_VALID
      {
        get
        {
          try
          {
            return (string) this[this.tableTable0.AUTH_VALIDColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'AUTH_VALID' in table 'Table0' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable0.AUTH_VALIDColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      internal Table0Row(DataRowBuilder rb)
        : base(rb)
      {
        this.tableTable0 = (Movement_DS.Table0DataTable) this.Table;
      }

      [DebuggerNonUserCode]
      public bool IsDOC_NUMNull()
      {
        return this.IsNull(this.tableTable0.DOC_NUMColumn);
      }

      [DebuggerNonUserCode]
      public void SetDOC_NUMNull()
      {
        this[this.tableTable0.DOC_NUMColumn] = Convert.DBNull;
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
      public bool IsCONTRACTOR_TONull()
      {
        return this.IsNull(this.tableTable0.CONTRACTOR_TOColumn);
      }

      [DebuggerNonUserCode]
      public void SetCONTRACTOR_TONull()
      {
        this[this.tableTable0.CONTRACTOR_TOColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsAUTH_VALIDNull()
      {
        return this.IsNull(this.tableTable0.AUTH_VALIDColumn);
      }

      [DebuggerNonUserCode]
      public void SetAUTH_VALIDNull()
      {
        this[this.tableTable0.AUTH_VALIDColumn] = Convert.DBNull;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class Table1Row : DataRow
    {
      private Movement_DS.Table1DataTable tableTable1;

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
      internal Table1Row(DataRowBuilder rb)
        : base(rb)
      {
        this.tableTable1 = (Movement_DS.Table1DataTable) this.Table;
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
      public bool IsSUM_SALNull()
      {
        return this.IsNull(this.tableTable1.SUM_SALColumn);
      }

      [DebuggerNonUserCode]
      public void SetSUM_SALNull()
      {
        this[this.tableTable1.SUM_SALColumn] = Convert.DBNull;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class Table2Row : DataRow
    {
      private Movement_DS.Table2DataTable tableTable2;

      [DebuggerNonUserCode]
      public string SELF_NAME
      {
        get
        {
          try
          {
            return (string) this[this.tableTable2.SELF_NAMEColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'SELF_NAME' in table 'Table2' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable2.SELF_NAMEColumn] = (object) value;
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
      public string ADDRESS
      {
        get
        {
          try
          {
            return (string) this[this.tableTable2.ADDRESSColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'ADDRESS' in table 'Table2' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable2.ADDRESSColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      internal Table2Row(DataRowBuilder rb)
        : base(rb)
      {
        this.tableTable2 = (Movement_DS.Table2DataTable) this.Table;
      }

      [DebuggerNonUserCode]
      public bool IsSELF_NAMENull()
      {
        return this.IsNull(this.tableTable2.SELF_NAMEColumn);
      }

      [DebuggerNonUserCode]
      public void SetSELF_NAMENull()
      {
        this[this.tableTable2.SELF_NAMEColumn] = Convert.DBNull;
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
      public bool IsADDRESSNull()
      {
        return this.IsNull(this.tableTable2.ADDRESSColumn);
      }

      [DebuggerNonUserCode]
      public void SetADDRESSNull()
      {
        this[this.tableTable2.ADDRESSColumn] = Convert.DBNull;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class Table3Row : DataRow
    {
      private Movement_DS.Table3DataTable tableTable3;

      [DebuggerNonUserCode]
      public string C_NAME
      {
        get
        {
          try
          {
            return (string) this[this.tableTable3.C_NAMEColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'C_NAME' in table 'Table3' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable3.C_NAMEColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public string C_DATE
      {
        get
        {
          try
          {
            return (string) this[this.tableTable3.C_DATEColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'C_DATE' in table 'Table3' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable3.C_DATEColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public string A_NUM
      {
        get
        {
          try
          {
            return (string) this[this.tableTable3.A_NUMColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'A_NUM' in table 'Table3' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable3.A_NUMColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      public string A_DATE
      {
        get
        {
          try
          {
            return (string) this[this.tableTable3.A_DATEColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'A_DATE' in table 'Table3' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableTable3.A_DATEColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      internal Table3Row(DataRowBuilder rb)
        : base(rb)
      {
        this.tableTable3 = (Movement_DS.Table3DataTable) this.Table;
      }

      [DebuggerNonUserCode]
      public bool IsC_NAMENull()
      {
        return this.IsNull(this.tableTable3.C_NAMEColumn);
      }

      [DebuggerNonUserCode]
      public void SetC_NAMENull()
      {
        this[this.tableTable3.C_NAMEColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsC_DATENull()
      {
        return this.IsNull(this.tableTable3.C_DATEColumn);
      }

      [DebuggerNonUserCode]
      public void SetC_DATENull()
      {
        this[this.tableTable3.C_DATEColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsA_NUMNull()
      {
        return this.IsNull(this.tableTable3.A_NUMColumn);
      }

      [DebuggerNonUserCode]
      public void SetA_NUMNull()
      {
        this[this.tableTable3.A_NUMColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      public bool IsA_DATENull()
      {
        return this.IsNull(this.tableTable3.A_DATEColumn);
      }

      [DebuggerNonUserCode]
      public void SetA_DATENull()
      {
        this[this.tableTable3.A_DATEColumn] = Convert.DBNull;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class Table0RowChangeEvent : EventArgs
    {
      private Movement_DS.Table0Row eventRow;
      private DataRowAction eventAction;

      [DebuggerNonUserCode]
      public Movement_DS.Table0Row Row
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
      public Table0RowChangeEvent(Movement_DS.Table0Row row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class Table1RowChangeEvent : EventArgs
    {
      private Movement_DS.Table1Row eventRow;
      private DataRowAction eventAction;

      [DebuggerNonUserCode]
      public Movement_DS.Table1Row Row
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
      public Table1RowChangeEvent(Movement_DS.Table1Row row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class Table2RowChangeEvent : EventArgs
    {
      private Movement_DS.Table2Row eventRow;
      private DataRowAction eventAction;

      [DebuggerNonUserCode]
      public Movement_DS.Table2Row Row
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
      public Table2RowChangeEvent(Movement_DS.Table2Row row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    public class Table3RowChangeEvent : EventArgs
    {
      private Movement_DS.Table3Row eventRow;
      private DataRowAction eventAction;

      [DebuggerNonUserCode]
      public Movement_DS.Table3Row Row
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
      public Table3RowChangeEvent(Movement_DS.Table3Row row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }
    }
  }
}
