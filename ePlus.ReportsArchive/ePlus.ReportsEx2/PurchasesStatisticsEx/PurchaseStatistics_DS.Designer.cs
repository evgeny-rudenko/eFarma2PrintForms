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

namespace PurchasesStatisticsEx {
    using System;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    [Serializable()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.ComponentModel.ToolboxItem(true)]
    [System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedDataSetSchema")]
    [System.Xml.Serialization.XmlRootAttribute("PurchaseStatistics_DS")]
    [System.ComponentModel.Design.HelpKeywordAttribute("vs.data.DataSet")]
    public partial class PurchaseStatistics_DS : System.Data.DataSet {
        
        private Table0DataTable tableTable0;
        
        private System.Data.SchemaSerializationMode _schemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public PurchaseStatistics_DS() {
            this.BeginInit();
            this.InitClass();
            System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += schemaChangedHandler;
            base.Relations.CollectionChanged += schemaChangedHandler;
            this.EndInit();
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected PurchaseStatistics_DS(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : 
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
                if ((ds.Tables["Table0"] != null)) {
                    base.Tables.Add(new Table0DataTable(ds.Tables["Table0"]));
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
        public Table0DataTable Table0 {
            get {
                return this.tableTable0;
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
            PurchaseStatistics_DS cln = ((PurchaseStatistics_DS)(base.Clone()));
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
                if ((ds.Tables["Table0"] != null)) {
                    base.Tables.Add(new Table0DataTable(ds.Tables["Table0"]));
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
            this.tableTable0 = ((Table0DataTable)(base.Tables["Table0"]));
            if ((initTable == true)) {
                if ((this.tableTable0 != null)) {
                    this.tableTable0.InitVars();
                }
            }
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void InitClass() {
            this.DataSetName = "PurchaseStatistics_DS";
            this.Prefix = "";
            this.Namespace = "http://tempuri.org/PurchaseStatistics_DS.xsd";
            this.EnforceConstraints = true;
            this.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            this.tableTable0 = new Table0DataTable();
            base.Tables.Add(this.tableTable0);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private bool ShouldSerializeTable0() {
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
            PurchaseStatistics_DS ds = new PurchaseStatistics_DS();
            System.Xml.Schema.XmlSchemaComplexType type = new System.Xml.Schema.XmlSchemaComplexType();
            System.Xml.Schema.XmlSchemaSequence sequence = new System.Xml.Schema.XmlSchemaSequence();
            xs.Add(ds.GetSchemaSerializable());
            System.Xml.Schema.XmlSchemaAny any = new System.Xml.Schema.XmlSchemaAny();
            any.Namespace = ds.Namespace;
            sequence.Items.Add(any);
            type.Particle = sequence;
            return type;
        }
        
        public delegate void Table0RowChangeEventHandler(object sender, Table0RowChangeEvent e);
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        [System.Serializable()]
        [System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")]
        public partial class Table0DataTable : System.Data.DataTable, System.Collections.IEnumerable {
            
            private System.Data.DataColumn columnGOODS_NAME;
            
            private System.Data.DataColumn columnSTORE_NAME;
            
            private System.Data.DataColumn columnSUPPLIER_PRICE;
            
            private System.Data.DataColumn columnRETAIL_PRICE;
            
            private System.Data.DataColumn columnSOLD_ITEMS;
            
            private System.Data.DataColumn columnREMAIN_ITEMS;
            
            private System.Data.DataColumn columnSOLD_ITEMS_AVERAGE;
            
            private System.Data.DataColumn columnREMAIN_DAYS;
            
            private System.Data.DataColumn columnNEED_DAYS;
            
            private System.Data.DataColumn columnNEED_ITEMS;
            
            private System.Data.DataColumn columnG_CODE;
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public Table0DataTable() {
                this.TableName = "Table0";
                this.BeginInit();
                this.InitClass();
                this.EndInit();
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal Table0DataTable(System.Data.DataTable table) {
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
            protected Table0DataTable(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : 
                    base(info, context) {
                this.InitVars();
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn GOODS_NAMEColumn {
                get {
                    return this.columnGOODS_NAME;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn STORE_NAMEColumn {
                get {
                    return this.columnSTORE_NAME;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn SUPPLIER_PRICEColumn {
                get {
                    return this.columnSUPPLIER_PRICE;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn RETAIL_PRICEColumn {
                get {
                    return this.columnRETAIL_PRICE;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn SOLD_ITEMSColumn {
                get {
                    return this.columnSOLD_ITEMS;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn REMAIN_ITEMSColumn {
                get {
                    return this.columnREMAIN_ITEMS;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn SOLD_ITEMS_AVERAGEColumn {
                get {
                    return this.columnSOLD_ITEMS_AVERAGE;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn REMAIN_DAYSColumn {
                get {
                    return this.columnREMAIN_DAYS;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn NEED_DAYSColumn {
                get {
                    return this.columnNEED_DAYS;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn NEED_ITEMSColumn {
                get {
                    return this.columnNEED_ITEMS;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn G_CODEColumn {
                get {
                    return this.columnG_CODE;
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
            public Table0Row this[int index] {
                get {
                    return ((Table0Row)(this.Rows[index]));
                }
            }
            
            public event Table0RowChangeEventHandler Table0RowChanging;
            
            public event Table0RowChangeEventHandler Table0RowChanged;
            
            public event Table0RowChangeEventHandler Table0RowDeleting;
            
            public event Table0RowChangeEventHandler Table0RowDeleted;
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void AddTable0Row(Table0Row row) {
                this.Rows.Add(row);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public Table0Row AddTable0Row(string GOODS_NAME, string STORE_NAME, string SUPPLIER_PRICE, string RETAIL_PRICE, string SOLD_ITEMS, string REMAIN_ITEMS, string SOLD_ITEMS_AVERAGE, string REMAIN_DAYS, string NEED_DAYS, string NEED_ITEMS, string G_CODE) {
                Table0Row rowTable0Row = ((Table0Row)(this.NewRow()));
                rowTable0Row.ItemArray = new object[] {
                        GOODS_NAME,
                        STORE_NAME,
                        SUPPLIER_PRICE,
                        RETAIL_PRICE,
                        SOLD_ITEMS,
                        REMAIN_ITEMS,
                        SOLD_ITEMS_AVERAGE,
                        REMAIN_DAYS,
                        NEED_DAYS,
                        NEED_ITEMS,
                        G_CODE};
                this.Rows.Add(rowTable0Row);
                return rowTable0Row;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public virtual System.Collections.IEnumerator GetEnumerator() {
                return this.Rows.GetEnumerator();
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public override System.Data.DataTable Clone() {
                Table0DataTable cln = ((Table0DataTable)(base.Clone()));
                cln.InitVars();
                return cln;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override System.Data.DataTable CreateInstance() {
                return new Table0DataTable();
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal void InitVars() {
                this.columnGOODS_NAME = base.Columns["GOODS_NAME"];
                this.columnSTORE_NAME = base.Columns["STORE_NAME"];
                this.columnSUPPLIER_PRICE = base.Columns["SUPPLIER_PRICE"];
                this.columnRETAIL_PRICE = base.Columns["RETAIL_PRICE"];
                this.columnSOLD_ITEMS = base.Columns["SOLD_ITEMS"];
                this.columnREMAIN_ITEMS = base.Columns["REMAIN_ITEMS"];
                this.columnSOLD_ITEMS_AVERAGE = base.Columns["SOLD_ITEMS_AVERAGE"];
                this.columnREMAIN_DAYS = base.Columns["REMAIN_DAYS"];
                this.columnNEED_DAYS = base.Columns["NEED_DAYS"];
                this.columnNEED_ITEMS = base.Columns["NEED_ITEMS"];
                this.columnG_CODE = base.Columns["G_CODE"];
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            private void InitClass() {
                this.columnGOODS_NAME = new System.Data.DataColumn("GOODS_NAME", typeof(string), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnGOODS_NAME);
                this.columnSTORE_NAME = new System.Data.DataColumn("STORE_NAME", typeof(string), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnSTORE_NAME);
                this.columnSUPPLIER_PRICE = new System.Data.DataColumn("SUPPLIER_PRICE", typeof(string), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnSUPPLIER_PRICE);
                this.columnRETAIL_PRICE = new System.Data.DataColumn("RETAIL_PRICE", typeof(string), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnRETAIL_PRICE);
                this.columnSOLD_ITEMS = new System.Data.DataColumn("SOLD_ITEMS", typeof(string), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnSOLD_ITEMS);
                this.columnREMAIN_ITEMS = new System.Data.DataColumn("REMAIN_ITEMS", typeof(string), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnREMAIN_ITEMS);
                this.columnSOLD_ITEMS_AVERAGE = new System.Data.DataColumn("SOLD_ITEMS_AVERAGE", typeof(string), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnSOLD_ITEMS_AVERAGE);
                this.columnREMAIN_DAYS = new System.Data.DataColumn("REMAIN_DAYS", typeof(string), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnREMAIN_DAYS);
                this.columnNEED_DAYS = new System.Data.DataColumn("NEED_DAYS", typeof(string), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnNEED_DAYS);
                this.columnNEED_ITEMS = new System.Data.DataColumn("NEED_ITEMS", typeof(string), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnNEED_ITEMS);
                this.columnG_CODE = new System.Data.DataColumn("G_CODE", typeof(string), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnG_CODE);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public Table0Row NewTable0Row() {
                return ((Table0Row)(this.NewRow()));
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override System.Data.DataRow NewRowFromBuilder(System.Data.DataRowBuilder builder) {
                return new Table0Row(builder);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override System.Type GetRowType() {
                return typeof(Table0Row);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowChanged(System.Data.DataRowChangeEventArgs e) {
                base.OnRowChanged(e);
                if ((this.Table0RowChanged != null)) {
                    this.Table0RowChanged(this, new Table0RowChangeEvent(((Table0Row)(e.Row)), e.Action));
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowChanging(System.Data.DataRowChangeEventArgs e) {
                base.OnRowChanging(e);
                if ((this.Table0RowChanging != null)) {
                    this.Table0RowChanging(this, new Table0RowChangeEvent(((Table0Row)(e.Row)), e.Action));
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowDeleted(System.Data.DataRowChangeEventArgs e) {
                base.OnRowDeleted(e);
                if ((this.Table0RowDeleted != null)) {
                    this.Table0RowDeleted(this, new Table0RowChangeEvent(((Table0Row)(e.Row)), e.Action));
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowDeleting(System.Data.DataRowChangeEventArgs e) {
                base.OnRowDeleting(e);
                if ((this.Table0RowDeleting != null)) {
                    this.Table0RowDeleting(this, new Table0RowChangeEvent(((Table0Row)(e.Row)), e.Action));
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void RemoveTable0Row(Table0Row row) {
                this.Rows.Remove(row);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public static System.Xml.Schema.XmlSchemaComplexType GetTypedTableSchema(System.Xml.Schema.XmlSchemaSet xs) {
                System.Xml.Schema.XmlSchemaComplexType type = new System.Xml.Schema.XmlSchemaComplexType();
                System.Xml.Schema.XmlSchemaSequence sequence = new System.Xml.Schema.XmlSchemaSequence();
                PurchaseStatistics_DS ds = new PurchaseStatistics_DS();
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
                attribute2.FixedValue = "Table0DataTable";
                type.Attributes.Add(attribute2);
                type.Particle = sequence;
                return type;
            }
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public partial class Table0Row : System.Data.DataRow {
            
            private Table0DataTable tableTable0;
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal Table0Row(System.Data.DataRowBuilder rb) : 
                    base(rb) {
                this.tableTable0 = ((Table0DataTable)(this.Table));
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string GOODS_NAME {
                get {
                    try {
                        return ((string)(this[this.tableTable0.GOODS_NAMEColumn]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("The value for column \'GOODS_NAME\' in table \'Table0\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTable0.GOODS_NAMEColumn] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string STORE_NAME {
                get {
                    try {
                        return ((string)(this[this.tableTable0.STORE_NAMEColumn]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("The value for column \'STORE_NAME\' in table \'Table0\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTable0.STORE_NAMEColumn] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string SUPPLIER_PRICE {
                get {
                    try {
                        return ((string)(this[this.tableTable0.SUPPLIER_PRICEColumn]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("The value for column \'SUPPLIER_PRICE\' in table \'Table0\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTable0.SUPPLIER_PRICEColumn] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string RETAIL_PRICE {
                get {
                    try {
                        return ((string)(this[this.tableTable0.RETAIL_PRICEColumn]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("The value for column \'RETAIL_PRICE\' in table \'Table0\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTable0.RETAIL_PRICEColumn] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string SOLD_ITEMS {
                get {
                    try {
                        return ((string)(this[this.tableTable0.SOLD_ITEMSColumn]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("The value for column \'SOLD_ITEMS\' in table \'Table0\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTable0.SOLD_ITEMSColumn] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string REMAIN_ITEMS {
                get {
                    try {
                        return ((string)(this[this.tableTable0.REMAIN_ITEMSColumn]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("The value for column \'REMAIN_ITEMS\' in table \'Table0\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTable0.REMAIN_ITEMSColumn] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string SOLD_ITEMS_AVERAGE {
                get {
                    try {
                        return ((string)(this[this.tableTable0.SOLD_ITEMS_AVERAGEColumn]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("The value for column \'SOLD_ITEMS_AVERAGE\' in table \'Table0\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTable0.SOLD_ITEMS_AVERAGEColumn] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string REMAIN_DAYS {
                get {
                    try {
                        return ((string)(this[this.tableTable0.REMAIN_DAYSColumn]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("The value for column \'REMAIN_DAYS\' in table \'Table0\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTable0.REMAIN_DAYSColumn] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string NEED_DAYS {
                get {
                    try {
                        return ((string)(this[this.tableTable0.NEED_DAYSColumn]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("The value for column \'NEED_DAYS\' in table \'Table0\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTable0.NEED_DAYSColumn] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string NEED_ITEMS {
                get {
                    try {
                        return ((string)(this[this.tableTable0.NEED_ITEMSColumn]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("The value for column \'NEED_ITEMS\' in table \'Table0\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTable0.NEED_ITEMSColumn] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string G_CODE {
                get {
                    try {
                        return ((string)(this[this.tableTable0.G_CODEColumn]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("The value for column \'G_CODE\' in table \'Table0\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTable0.G_CODEColumn] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsGOODS_NAMENull() {
                return this.IsNull(this.tableTable0.GOODS_NAMEColumn);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetGOODS_NAMENull() {
                this[this.tableTable0.GOODS_NAMEColumn] = System.Convert.DBNull;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsSTORE_NAMENull() {
                return this.IsNull(this.tableTable0.STORE_NAMEColumn);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetSTORE_NAMENull() {
                this[this.tableTable0.STORE_NAMEColumn] = System.Convert.DBNull;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsSUPPLIER_PRICENull() {
                return this.IsNull(this.tableTable0.SUPPLIER_PRICEColumn);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetSUPPLIER_PRICENull() {
                this[this.tableTable0.SUPPLIER_PRICEColumn] = System.Convert.DBNull;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsRETAIL_PRICENull() {
                return this.IsNull(this.tableTable0.RETAIL_PRICEColumn);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetRETAIL_PRICENull() {
                this[this.tableTable0.RETAIL_PRICEColumn] = System.Convert.DBNull;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsSOLD_ITEMSNull() {
                return this.IsNull(this.tableTable0.SOLD_ITEMSColumn);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetSOLD_ITEMSNull() {
                this[this.tableTable0.SOLD_ITEMSColumn] = System.Convert.DBNull;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsREMAIN_ITEMSNull() {
                return this.IsNull(this.tableTable0.REMAIN_ITEMSColumn);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetREMAIN_ITEMSNull() {
                this[this.tableTable0.REMAIN_ITEMSColumn] = System.Convert.DBNull;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsSOLD_ITEMS_AVERAGENull() {
                return this.IsNull(this.tableTable0.SOLD_ITEMS_AVERAGEColumn);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetSOLD_ITEMS_AVERAGENull() {
                this[this.tableTable0.SOLD_ITEMS_AVERAGEColumn] = System.Convert.DBNull;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsREMAIN_DAYSNull() {
                return this.IsNull(this.tableTable0.REMAIN_DAYSColumn);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetREMAIN_DAYSNull() {
                this[this.tableTable0.REMAIN_DAYSColumn] = System.Convert.DBNull;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsNEED_DAYSNull() {
                return this.IsNull(this.tableTable0.NEED_DAYSColumn);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetNEED_DAYSNull() {
                this[this.tableTable0.NEED_DAYSColumn] = System.Convert.DBNull;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsNEED_ITEMSNull() {
                return this.IsNull(this.tableTable0.NEED_ITEMSColumn);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetNEED_ITEMSNull() {
                this[this.tableTable0.NEED_ITEMSColumn] = System.Convert.DBNull;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsG_CODENull() {
                return this.IsNull(this.tableTable0.G_CODEColumn);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetG_CODENull() {
                this[this.tableTable0.G_CODEColumn] = System.Convert.DBNull;
            }
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public class Table0RowChangeEvent : System.EventArgs {
            
            private Table0Row eventRow;
            
            private System.Data.DataRowAction eventAction;
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public Table0RowChangeEvent(Table0Row row, System.Data.DataRowAction action) {
                this.eventRow = row;
                this.eventAction = action;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public Table0Row Row {
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