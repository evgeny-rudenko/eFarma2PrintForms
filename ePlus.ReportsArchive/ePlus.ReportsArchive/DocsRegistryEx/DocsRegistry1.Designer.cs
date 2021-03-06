﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.42
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591

namespace DocsRegistryEx {
    using System;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    [Serializable()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.ComponentModel.ToolboxItem(true)]
    [System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedDataSetSchema")]
    [System.Xml.Serialization.XmlRootAttribute("NewDataSet")]
    [System.ComponentModel.Design.HelpKeywordAttribute("vs.data.DataSet")]
    public partial class NewDataSet : System.Data.DataSet {
        
        private TableDataTable tableTable;
        
        private System.Data.SchemaSerializationMode _schemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public NewDataSet() {
            this.BeginInit();
            this.InitClass();
            System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += schemaChangedHandler;
            base.Relations.CollectionChanged += schemaChangedHandler;
            this.EndInit();
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected NewDataSet(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : 
                base(info, context, false) {
            if ((this.IsBinarySerialized(info, context) == true)) {
                this.InitVars(false);
                System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler1 = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
                this.Tables.CollectionChanged += schemaChangedHandler1;
                this.Relations.CollectionChanged += schemaChangedHandler1;
                return;
            }
            string strSchema = ((string)(info.GetValue("XmlSchema", typeof(string))));
            if ((this.DetermineSchemaSerializationMode(info, context) == System.Data.SchemaSerializationMode.IncludeSchema)) {
                System.Data.DataSet ds = new System.Data.DataSet();
                ds.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(strSchema)));
                if ((ds.Tables["Table"] != null)) {
                    base.Tables.Add(new TableDataTable(ds.Tables["Table"]));
                }
                this.DataSetName = ds.DataSetName;
                this.Prefix = ds.Prefix;
                this.Namespace = ds.Namespace;
                this.Locale = ds.Locale;
                this.CaseSensitive = ds.CaseSensitive;
                this.EnforceConstraints = ds.EnforceConstraints;
                this.Merge(ds, false, System.Data.MissingSchemaAction.Add);
                this.InitVars();
            }
            else {
                this.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(strSchema)));
            }
            this.GetSerializationData(info, context);
            System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += schemaChangedHandler;
            this.Relations.CollectionChanged += schemaChangedHandler;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content)]
        public TableDataTable Table {
            get {
                return this.tableTable;
            }
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.BrowsableAttribute(true)]
        [System.ComponentModel.DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Visible)]
        public override System.Data.SchemaSerializationMode SchemaSerializationMode {
            get {
                return this._schemaSerializationMode;
            }
            set {
                this._schemaSerializationMode = value;
            }
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public new System.Data.DataTableCollection Tables {
            get {
                return base.Tables;
            }
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public new System.Data.DataRelationCollection Relations {
            get {
                return base.Relations;
            }
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override void InitializeDerivedDataSet() {
            this.BeginInit();
            this.InitClass();
            this.EndInit();
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public override System.Data.DataSet Clone() {
            NewDataSet cln = ((NewDataSet)(base.Clone()));
            cln.InitVars();
            cln.SchemaSerializationMode = this.SchemaSerializationMode;
            return cln;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override bool ShouldSerializeTables() {
            return false;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override bool ShouldSerializeRelations() {
            return false;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override void ReadXmlSerializable(System.Xml.XmlReader reader) {
            if ((this.DetermineSchemaSerializationMode(reader) == System.Data.SchemaSerializationMode.IncludeSchema)) {
                this.Reset();
                System.Data.DataSet ds = new System.Data.DataSet();
                ds.ReadXml(reader);
                if ((ds.Tables["Table"] != null)) {
                    base.Tables.Add(new TableDataTable(ds.Tables["Table"]));
                }
                this.DataSetName = ds.DataSetName;
                this.Prefix = ds.Prefix;
                this.Namespace = ds.Namespace;
                this.Locale = ds.Locale;
                this.CaseSensitive = ds.CaseSensitive;
                this.EnforceConstraints = ds.EnforceConstraints;
                this.Merge(ds, false, System.Data.MissingSchemaAction.Add);
                this.InitVars();
            }
            else {
                this.ReadXml(reader);
                this.InitVars();
            }
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override System.Xml.Schema.XmlSchema GetSchemaSerializable() {
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            this.WriteXmlSchema(new System.Xml.XmlTextWriter(stream, null));
            stream.Position = 0;
            return System.Xml.Schema.XmlSchema.Read(new System.Xml.XmlTextReader(stream), null);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        internal void InitVars() {
            this.InitVars(true);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        internal void InitVars(bool initTable) {
            this.tableTable = ((TableDataTable)(base.Tables["Table"]));
            if ((initTable == true)) {
                if ((this.tableTable != null)) {
                    this.tableTable.InitVars();
                }
            }
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void InitClass() {
            this.DataSetName = "NewDataSet";
            this.Prefix = "";
            this.EnforceConstraints = true;
            this.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            this.tableTable = new TableDataTable();
            base.Tables.Add(this.tableTable);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private bool ShouldSerializeTable() {
            return false;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void SchemaChanged(object sender, System.ComponentModel.CollectionChangeEventArgs e) {
            if ((e.Action == System.ComponentModel.CollectionChangeAction.Remove)) {
                this.InitVars();
            }
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static System.Xml.Schema.XmlSchemaComplexType GetTypedDataSetSchema(System.Xml.Schema.XmlSchemaSet xs) {
            NewDataSet ds = new NewDataSet();
            System.Xml.Schema.XmlSchemaComplexType type = new System.Xml.Schema.XmlSchemaComplexType();
            System.Xml.Schema.XmlSchemaSequence sequence = new System.Xml.Schema.XmlSchemaSequence();
            xs.Add(ds.GetSchemaSerializable());
            System.Xml.Schema.XmlSchemaAny any = new System.Xml.Schema.XmlSchemaAny();
            any.Namespace = ds.Namespace;
            sequence.Items.Add(any);
            type.Particle = sequence;
            return type;
        }
        
        public delegate void TableRowChangeEventHandler(object sender, TableRowChangeEvent e);
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        [System.Serializable()]
        [System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")]
        public partial class TableDataTable : System.Data.DataTable, System.Collections.IEnumerable {
            
            private System.Data.DataColumn columnDOC_DATE;
            
            private System.Data.DataColumn columnDOC_NAME;
            
            private System.Data.DataColumn columnDOC_NUM;
            
            private System.Data.DataColumn columnCLIENT;
            
            private System.Data.DataColumn columnDOC_BASE;
            
            private System.Data.DataColumn columnSTATE_NAME;
            
            private System.Data.DataColumn columnSUM_PRICE;
            
            private System.Data.DataColumn columnSUM_PRICE_VAT;
            
            private System.Data.DataColumn columnSUM_RETAIL;
            
            private System.Data.DataColumn columnSUM_RETAIL_VAT;
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public TableDataTable() {
                this.TableName = "Table";
                this.BeginInit();
                this.InitClass();
                this.EndInit();
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal TableDataTable(System.Data.DataTable table) {
                this.TableName = table.TableName;
                if ((table.CaseSensitive != table.DataSet.CaseSensitive)) {
                    this.CaseSensitive = table.CaseSensitive;
                }
                if ((table.Locale.ToString() != table.DataSet.Locale.ToString())) {
                    this.Locale = table.Locale;
                }
                if ((table.Namespace != table.DataSet.Namespace)) {
                    this.Namespace = table.Namespace;
                }
                this.Prefix = table.Prefix;
                this.MinimumCapacity = table.MinimumCapacity;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected TableDataTable(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : 
                    base(info, context) {
                this.InitVars();
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn DOC_DATEColumn {
                get {
                    return this.columnDOC_DATE;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn DOC_NAMEColumn {
                get {
                    return this.columnDOC_NAME;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn DOC_NUMColumn {
                get {
                    return this.columnDOC_NUM;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn CLIENTColumn {
                get {
                    return this.columnCLIENT;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn DOC_BASEColumn {
                get {
                    return this.columnDOC_BASE;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn STATE_NAMEColumn {
                get {
                    return this.columnSTATE_NAME;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn SUM_PRICEColumn {
                get {
                    return this.columnSUM_PRICE;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn SUM_PRICE_VATColumn {
                get {
                    return this.columnSUM_PRICE_VAT;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn SUM_RETAILColumn {
                get {
                    return this.columnSUM_RETAIL;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn SUM_RETAIL_VATColumn {
                get {
                    return this.columnSUM_RETAIL_VAT;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [System.ComponentModel.Browsable(false)]
            public int Count {
                get {
                    return this.Rows.Count;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public TableRow this[int index] {
                get {
                    return ((TableRow)(this.Rows[index]));
                }
            }
            
            public event TableRowChangeEventHandler TableRowChanging;
            
            public event TableRowChangeEventHandler TableRowChanged;
            
            public event TableRowChangeEventHandler TableRowDeleting;
            
            public event TableRowChangeEventHandler TableRowDeleted;
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void AddTableRow(TableRow row) {
                this.Rows.Add(row);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public TableRow AddTableRow(System.DateTime DOC_DATE, string DOC_NAME, string DOC_NUM, string CLIENT, string DOC_BASE, string STATE_NAME, decimal SUM_PRICE, decimal SUM_PRICE_VAT, decimal SUM_RETAIL, decimal SUM_RETAIL_VAT) {
                TableRow rowTableRow = ((TableRow)(this.NewRow()));
                rowTableRow.ItemArray = new object[] {
                        DOC_DATE,
                        DOC_NAME,
                        DOC_NUM,
                        CLIENT,
                        DOC_BASE,
                        STATE_NAME,
                        SUM_PRICE,
                        SUM_PRICE_VAT,
                        SUM_RETAIL,
                        SUM_RETAIL_VAT};
                this.Rows.Add(rowTableRow);
                return rowTableRow;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public virtual System.Collections.IEnumerator GetEnumerator() {
                return this.Rows.GetEnumerator();
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public override System.Data.DataTable Clone() {
                TableDataTable cln = ((TableDataTable)(base.Clone()));
                cln.InitVars();
                return cln;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override System.Data.DataTable CreateInstance() {
                return new TableDataTable();
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal void InitVars() {
                this.columnDOC_DATE = base.Columns["DOC_DATE"];
                this.columnDOC_NAME = base.Columns["DOC_NAME"];
                this.columnDOC_NUM = base.Columns["DOC_NUM"];
                this.columnCLIENT = base.Columns["CLIENT"];
                this.columnDOC_BASE = base.Columns["DOC_BASE"];
                this.columnSTATE_NAME = base.Columns["STATE_NAME"];
                this.columnSUM_PRICE = base.Columns["SUM_PRICE"];
                this.columnSUM_PRICE_VAT = base.Columns["SUM_PRICE_VAT"];
                this.columnSUM_RETAIL = base.Columns["SUM_RETAIL"];
                this.columnSUM_RETAIL_VAT = base.Columns["SUM_RETAIL_VAT"];
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            private void InitClass() {
                this.columnDOC_DATE = new System.Data.DataColumn("DOC_DATE", typeof(System.DateTime), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnDOC_DATE);
                this.columnDOC_NAME = new System.Data.DataColumn("DOC_NAME", typeof(string), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnDOC_NAME);
                this.columnDOC_NUM = new System.Data.DataColumn("DOC_NUM", typeof(string), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnDOC_NUM);
                this.columnCLIENT = new System.Data.DataColumn("CLIENT", typeof(string), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnCLIENT);
                this.columnDOC_BASE = new System.Data.DataColumn("DOC_BASE", typeof(string), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnDOC_BASE);
                this.columnSTATE_NAME = new System.Data.DataColumn("STATE_NAME", typeof(string), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnSTATE_NAME);
                this.columnSUM_PRICE = new System.Data.DataColumn("SUM_PRICE", typeof(decimal), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnSUM_PRICE);
                this.columnSUM_PRICE_VAT = new System.Data.DataColumn("SUM_PRICE_VAT", typeof(decimal), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnSUM_PRICE_VAT);
                this.columnSUM_RETAIL = new System.Data.DataColumn("SUM_RETAIL", typeof(decimal), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnSUM_RETAIL);
                this.columnSUM_RETAIL_VAT = new System.Data.DataColumn("SUM_RETAIL_VAT", typeof(decimal), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnSUM_RETAIL_VAT);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public TableRow NewTableRow() {
                return ((TableRow)(this.NewRow()));
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override System.Data.DataRow NewRowFromBuilder(System.Data.DataRowBuilder builder) {
                return new TableRow(builder);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override System.Type GetRowType() {
                return typeof(TableRow);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowChanged(System.Data.DataRowChangeEventArgs e) {
                base.OnRowChanged(e);
                if ((this.TableRowChanged != null)) {
                    this.TableRowChanged(this, new TableRowChangeEvent(((TableRow)(e.Row)), e.Action));
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowChanging(System.Data.DataRowChangeEventArgs e) {
                base.OnRowChanging(e);
                if ((this.TableRowChanging != null)) {
                    this.TableRowChanging(this, new TableRowChangeEvent(((TableRow)(e.Row)), e.Action));
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowDeleted(System.Data.DataRowChangeEventArgs e) {
                base.OnRowDeleted(e);
                if ((this.TableRowDeleted != null)) {
                    this.TableRowDeleted(this, new TableRowChangeEvent(((TableRow)(e.Row)), e.Action));
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowDeleting(System.Data.DataRowChangeEventArgs e) {
                base.OnRowDeleting(e);
                if ((this.TableRowDeleting != null)) {
                    this.TableRowDeleting(this, new TableRowChangeEvent(((TableRow)(e.Row)), e.Action));
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void RemoveTableRow(TableRow row) {
                this.Rows.Remove(row);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public static System.Xml.Schema.XmlSchemaComplexType GetTypedTableSchema(System.Xml.Schema.XmlSchemaSet xs) {
                System.Xml.Schema.XmlSchemaComplexType type = new System.Xml.Schema.XmlSchemaComplexType();
                System.Xml.Schema.XmlSchemaSequence sequence = new System.Xml.Schema.XmlSchemaSequence();
                NewDataSet ds = new NewDataSet();
                xs.Add(ds.GetSchemaSerializable());
                System.Xml.Schema.XmlSchemaAny any1 = new System.Xml.Schema.XmlSchemaAny();
                any1.Namespace = "http://www.w3.org/2001/XMLSchema";
                any1.MinOccurs = new decimal(0);
                any1.MaxOccurs = decimal.MaxValue;
                any1.ProcessContents = System.Xml.Schema.XmlSchemaContentProcessing.Lax;
                sequence.Items.Add(any1);
                System.Xml.Schema.XmlSchemaAny any2 = new System.Xml.Schema.XmlSchemaAny();
                any2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
                any2.MinOccurs = new decimal(1);
                any2.ProcessContents = System.Xml.Schema.XmlSchemaContentProcessing.Lax;
                sequence.Items.Add(any2);
                System.Xml.Schema.XmlSchemaAttribute attribute1 = new System.Xml.Schema.XmlSchemaAttribute();
                attribute1.Name = "namespace";
                attribute1.FixedValue = ds.Namespace;
                type.Attributes.Add(attribute1);
                System.Xml.Schema.XmlSchemaAttribute attribute2 = new System.Xml.Schema.XmlSchemaAttribute();
                attribute2.Name = "tableTypeName";
                attribute2.FixedValue = "TableDataTable";
                type.Attributes.Add(attribute2);
                type.Particle = sequence;
                return type;
            }
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public partial class TableRow : System.Data.DataRow {
            
            private TableDataTable tableTable;
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal TableRow(System.Data.DataRowBuilder rb) : 
                    base(rb) {
                this.tableTable = ((TableDataTable)(this.Table));
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.DateTime DOC_DATE {
                get {
                    try {
                        return ((System.DateTime)(this[this.tableTable.DOC_DATEColumn]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("The value for column \'DOC_DATE\' in table \'Table\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTable.DOC_DATEColumn] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string DOC_NAME {
                get {
                    try {
                        return ((string)(this[this.tableTable.DOC_NAMEColumn]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("The value for column \'DOC_NAME\' in table \'Table\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTable.DOC_NAMEColumn] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string DOC_NUM {
                get {
                    try {
                        return ((string)(this[this.tableTable.DOC_NUMColumn]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("The value for column \'DOC_NUM\' in table \'Table\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTable.DOC_NUMColumn] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string CLIENT {
                get {
                    try {
                        return ((string)(this[this.tableTable.CLIENTColumn]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("The value for column \'CLIENT\' in table \'Table\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTable.CLIENTColumn] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string DOC_BASE {
                get {
                    try {
                        return ((string)(this[this.tableTable.DOC_BASEColumn]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("The value for column \'DOC_BASE\' in table \'Table\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTable.DOC_BASEColumn] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string STATE_NAME {
                get {
                    try {
                        return ((string)(this[this.tableTable.STATE_NAMEColumn]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("The value for column \'STATE_NAME\' in table \'Table\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTable.STATE_NAMEColumn] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public decimal SUM_PRICE {
                get {
                    try {
                        return ((decimal)(this[this.tableTable.SUM_PRICEColumn]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("The value for column \'SUM_PRICE\' in table \'Table\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTable.SUM_PRICEColumn] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public decimal SUM_PRICE_VAT {
                get {
                    try {
                        return ((decimal)(this[this.tableTable.SUM_PRICE_VATColumn]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("The value for column \'SUM_PRICE_VAT\' in table \'Table\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTable.SUM_PRICE_VATColumn] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public decimal SUM_RETAIL {
                get {
                    try {
                        return ((decimal)(this[this.tableTable.SUM_RETAILColumn]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("The value for column \'SUM_RETAIL\' in table \'Table\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTable.SUM_RETAILColumn] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public decimal SUM_RETAIL_VAT {
                get {
                    try {
                        return ((decimal)(this[this.tableTable.SUM_RETAIL_VATColumn]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("The value for column \'SUM_RETAIL_VAT\' in table \'Table\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTable.SUM_RETAIL_VATColumn] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsDOC_DATENull() {
                return this.IsNull(this.tableTable.DOC_DATEColumn);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetDOC_DATENull() {
                this[this.tableTable.DOC_DATEColumn] = System.Convert.DBNull;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsDOC_NAMENull() {
                return this.IsNull(this.tableTable.DOC_NAMEColumn);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetDOC_NAMENull() {
                this[this.tableTable.DOC_NAMEColumn] = System.Convert.DBNull;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsDOC_NUMNull() {
                return this.IsNull(this.tableTable.DOC_NUMColumn);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetDOC_NUMNull() {
                this[this.tableTable.DOC_NUMColumn] = System.Convert.DBNull;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsCLIENTNull() {
                return this.IsNull(this.tableTable.CLIENTColumn);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetCLIENTNull() {
                this[this.tableTable.CLIENTColumn] = System.Convert.DBNull;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsDOC_BASENull() {
                return this.IsNull(this.tableTable.DOC_BASEColumn);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetDOC_BASENull() {
                this[this.tableTable.DOC_BASEColumn] = System.Convert.DBNull;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsSTATE_NAMENull() {
                return this.IsNull(this.tableTable.STATE_NAMEColumn);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetSTATE_NAMENull() {
                this[this.tableTable.STATE_NAMEColumn] = System.Convert.DBNull;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsSUM_PRICENull() {
                return this.IsNull(this.tableTable.SUM_PRICEColumn);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetSUM_PRICENull() {
                this[this.tableTable.SUM_PRICEColumn] = System.Convert.DBNull;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsSUM_PRICE_VATNull() {
                return this.IsNull(this.tableTable.SUM_PRICE_VATColumn);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetSUM_PRICE_VATNull() {
                this[this.tableTable.SUM_PRICE_VATColumn] = System.Convert.DBNull;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsSUM_RETAILNull() {
                return this.IsNull(this.tableTable.SUM_RETAILColumn);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetSUM_RETAILNull() {
                this[this.tableTable.SUM_RETAILColumn] = System.Convert.DBNull;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsSUM_RETAIL_VATNull() {
                return this.IsNull(this.tableTable.SUM_RETAIL_VATColumn);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetSUM_RETAIL_VATNull() {
                this[this.tableTable.SUM_RETAIL_VATColumn] = System.Convert.DBNull;
            }
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public class TableRowChangeEvent : System.EventArgs {
            
            private TableRow eventRow;
            
            private System.Data.DataRowAction eventAction;
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public TableRowChangeEvent(TableRow row, System.Data.DataRowAction action) {
                this.eventRow = row;
                this.eventAction = action;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public TableRow Row {
                get {
                    return this.eventRow;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataRowAction Action {
                get {
                    return this.eventAction;
                }
            }
        }
    }
}

#pragma warning restore 1591