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

namespace DefecturaEx {
    using System;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    [Serializable()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.ComponentModel.ToolboxItem(true)]
    [System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedDataSetSchema")]
    [System.Xml.Serialization.XmlRootAttribute("Defectura")]
    [System.ComponentModel.Design.HelpKeywordAttribute("vs.data.DataSet")]
    public partial class Defectura : System.Data.DataSet {
        
        private Table0DataTable tableTable0;
        
        private System.Data.SchemaSerializationMode _schemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public Defectura() {
            this.BeginInit();
            this.InitClass();
            System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += schemaChangedHandler;
            base.Relations.CollectionChanged += schemaChangedHandler;
            this.EndInit();
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected Defectura(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : 
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
            Defectura cln = ((Defectura)(base.Clone()));
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
            this.DataSetName = "Defectura";
            this.Prefix = "";
            this.Namespace = "http://tempuri.org/Defectura.xsd";
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
            Defectura ds = new Defectura();
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
            
            private System.Data.DataColumn columnQUANTITY;
            
            private System.Data.DataColumn columnMIN_VALUE;
            
            private System.Data.DataColumn columnINCOME_DATE;
            
            private System.Data.DataColumn columnSUP;
            
            private System.Data.DataColumn columnPRICE_SUP;
            
            private System.Data.DataColumn columnPRICE_SAL;
            
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
            public System.Data.DataColumn QUANTITYColumn {
                get {
                    return this.columnQUANTITY;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn MIN_VALUEColumn {
                get {
                    return this.columnMIN_VALUE;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn INCOME_DATEColumn {
                get {
                    return this.columnINCOME_DATE;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn SUPColumn {
                get {
                    return this.columnSUP;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn PRICE_SUPColumn {
                get {
                    return this.columnPRICE_SUP;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn PRICE_SALColumn {
                get {
                    return this.columnPRICE_SAL;
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
            public Table0Row AddTable0Row(string GOODS_NAME, string QUANTITY, string MIN_VALUE, string INCOME_DATE, string SUP, string PRICE_SUP, string PRICE_SAL, string G_CODE) {
                Table0Row rowTable0Row = ((Table0Row)(this.NewRow()));
                rowTable0Row.ItemArray = new object[] {
                        GOODS_NAME,
                        QUANTITY,
                        MIN_VALUE,
                        INCOME_DATE,
                        SUP,
                        PRICE_SUP,
                        PRICE_SAL,
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
                this.columnQUANTITY = base.Columns["QUANTITY"];
                this.columnMIN_VALUE = base.Columns["MIN_VALUE"];
                this.columnINCOME_DATE = base.Columns["INCOME_DATE"];
                this.columnSUP = base.Columns["SUP"];
                this.columnPRICE_SUP = base.Columns["PRICE_SUP"];
                this.columnPRICE_SAL = base.Columns["PRICE_SAL"];
                this.columnG_CODE = base.Columns["G_CODE"];
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            private void InitClass() {
                this.columnGOODS_NAME = new System.Data.DataColumn("GOODS_NAME", typeof(string), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnGOODS_NAME);
                this.columnQUANTITY = new System.Data.DataColumn("QUANTITY", typeof(string), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnQUANTITY);
                this.columnMIN_VALUE = new System.Data.DataColumn("MIN_VALUE", typeof(string), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnMIN_VALUE);
                this.columnINCOME_DATE = new System.Data.DataColumn("INCOME_DATE", typeof(string), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnINCOME_DATE);
                this.columnSUP = new System.Data.DataColumn("SUP", typeof(string), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnSUP);
                this.columnPRICE_SUP = new System.Data.DataColumn("PRICE_SUP", typeof(string), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnPRICE_SUP);
                this.columnPRICE_SAL = new System.Data.DataColumn("PRICE_SAL", typeof(string), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnPRICE_SAL);
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
                Defectura ds = new Defectura();
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
            public string QUANTITY {
                get {
                    try {
                        return ((string)(this[this.tableTable0.QUANTITYColumn]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("The value for column \'QUANTITY\' in table \'Table0\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTable0.QUANTITYColumn] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string MIN_VALUE {
                get {
                    try {
                        return ((string)(this[this.tableTable0.MIN_VALUEColumn]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("The value for column \'MIN_VALUE\' in table \'Table0\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTable0.MIN_VALUEColumn] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string INCOME_DATE {
                get {
                    try {
                        return ((string)(this[this.tableTable0.INCOME_DATEColumn]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("The value for column \'INCOME_DATE\' in table \'Table0\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTable0.INCOME_DATEColumn] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string SUP {
                get {
                    try {
                        return ((string)(this[this.tableTable0.SUPColumn]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("The value for column \'SUP\' in table \'Table0\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTable0.SUPColumn] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string PRICE_SUP {
                get {
                    try {
                        return ((string)(this[this.tableTable0.PRICE_SUPColumn]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("The value for column \'PRICE_SUP\' in table \'Table0\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTable0.PRICE_SUPColumn] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string PRICE_SAL {
                get {
                    try {
                        return ((string)(this[this.tableTable0.PRICE_SALColumn]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("The value for column \'PRICE_SAL\' in table \'Table0\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableTable0.PRICE_SALColumn] = value;
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
            public bool IsQUANTITYNull() {
                return this.IsNull(this.tableTable0.QUANTITYColumn);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetQUANTITYNull() {
                this[this.tableTable0.QUANTITYColumn] = System.Convert.DBNull;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsMIN_VALUENull() {
                return this.IsNull(this.tableTable0.MIN_VALUEColumn);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetMIN_VALUENull() {
                this[this.tableTable0.MIN_VALUEColumn] = System.Convert.DBNull;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsINCOME_DATENull() {
                return this.IsNull(this.tableTable0.INCOME_DATEColumn);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetINCOME_DATENull() {
                this[this.tableTable0.INCOME_DATEColumn] = System.Convert.DBNull;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsSUPNull() {
                return this.IsNull(this.tableTable0.SUPColumn);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetSUPNull() {
                this[this.tableTable0.SUPColumn] = System.Convert.DBNull;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsPRICE_SUPNull() {
                return this.IsNull(this.tableTable0.PRICE_SUPColumn);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetPRICE_SUPNull() {
                this[this.tableTable0.PRICE_SUPColumn] = System.Convert.DBNull;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsPRICE_SALNull() {
                return this.IsNull(this.tableTable0.PRICE_SALColumn);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetPRICE_SALNull() {
                this[this.tableTable0.PRICE_SALColumn] = System.Convert.DBNull;
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