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

namespace GoodsKeepingTimeListEx {
    using System;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    [Serializable()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.ComponentModel.ToolboxItem(true)]
    [System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedDataSetSchema")]
    [System.Xml.Serialization.XmlRootAttribute("GoodsKeepingTimeList")]
    [System.ComponentModel.Design.HelpKeywordAttribute("vs.data.DataSet")]
    public partial class GoodsKeepingTimeList : System.Data.DataSet {
        
        private rep_GoodsKeepingTimeListDataTable tablerep_GoodsKeepingTimeList;
        
        private System.Data.SchemaSerializationMode _schemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public GoodsKeepingTimeList() {
            this.BeginInit();
            this.InitClass();
            System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += schemaChangedHandler;
            base.Relations.CollectionChanged += schemaChangedHandler;
            this.EndInit();
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected GoodsKeepingTimeList(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : 
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
                if ((ds.Tables["rep_GoodsKeepingTimeList"] != null)) {
                    base.Tables.Add(new rep_GoodsKeepingTimeListDataTable(ds.Tables["rep_GoodsKeepingTimeList"]));
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
        public rep_GoodsKeepingTimeListDataTable rep_GoodsKeepingTimeList {
            get {
                return this.tablerep_GoodsKeepingTimeList;
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
            GoodsKeepingTimeList cln = ((GoodsKeepingTimeList)(base.Clone()));
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
                if ((ds.Tables["rep_GoodsKeepingTimeList"] != null)) {
                    base.Tables.Add(new rep_GoodsKeepingTimeListDataTable(ds.Tables["rep_GoodsKeepingTimeList"]));
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
            this.tablerep_GoodsKeepingTimeList = ((rep_GoodsKeepingTimeListDataTable)(base.Tables["rep_GoodsKeepingTimeList"]));
            if ((initTable == true)) {
                if ((this.tablerep_GoodsKeepingTimeList != null)) {
                    this.tablerep_GoodsKeepingTimeList.InitVars();
                }
            }
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void InitClass() {
            this.DataSetName = "GoodsKeepingTimeList";
            this.Prefix = "";
            this.Namespace = "http://tempuri.org/GoodsKeepingTimeList.xsd";
            this.EnforceConstraints = true;
            this.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            this.tablerep_GoodsKeepingTimeList = new rep_GoodsKeepingTimeListDataTable();
            base.Tables.Add(this.tablerep_GoodsKeepingTimeList);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private bool ShouldSerializerep_GoodsKeepingTimeList() {
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
            GoodsKeepingTimeList ds = new GoodsKeepingTimeList();
            System.Xml.Schema.XmlSchemaComplexType type = new System.Xml.Schema.XmlSchemaComplexType();
            System.Xml.Schema.XmlSchemaSequence sequence = new System.Xml.Schema.XmlSchemaSequence();
            xs.Add(ds.GetSchemaSerializable());
            System.Xml.Schema.XmlSchemaAny any = new System.Xml.Schema.XmlSchemaAny();
            any.Namespace = ds.Namespace;
            sequence.Items.Add(any);
            type.Particle = sequence;
            return type;
        }
        
        public delegate void rep_GoodsKeepingTimeListRowChangeEventHandler(object sender, rep_GoodsKeepingTimeListRowChangeEvent e);
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        [System.Serializable()]
        [System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")]
        public partial class rep_GoodsKeepingTimeListDataTable : System.Data.DataTable, System.Collections.IEnumerable {
            
            private System.Data.DataColumn columnSERIES_NUMBER;
            
            private System.Data.DataColumn columnNAME;
            
            private System.Data.DataColumn columnBEST_BEFORE;
            
            private System.Data.DataColumn columnAMOUNT;
            
            private System.Data.DataColumn columnPRODUCER_NAME;
            
            private System.Data.DataColumn columnPRICE;
            
            private System.Data.DataColumn columnSUMM;
            
            private System.Data.DataColumn columnG_CODE;
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public rep_GoodsKeepingTimeListDataTable() {
                this.TableName = "rep_GoodsKeepingTimeList";
                this.BeginInit();
                this.InitClass();
                this.EndInit();
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal rep_GoodsKeepingTimeListDataTable(System.Data.DataTable table) {
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
            protected rep_GoodsKeepingTimeListDataTable(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : 
                    base(info, context) {
                this.InitVars();
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn SERIES_NUMBERColumn {
                get {
                    return this.columnSERIES_NUMBER;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn NAMEColumn {
                get {
                    return this.columnNAME;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn BEST_BEFOREColumn {
                get {
                    return this.columnBEST_BEFORE;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn AMOUNTColumn {
                get {
                    return this.columnAMOUNT;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn PRODUCER_NAMEColumn {
                get {
                    return this.columnPRODUCER_NAME;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn PRICEColumn {
                get {
                    return this.columnPRICE;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn SUMMColumn {
                get {
                    return this.columnSUMM;
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
            public rep_GoodsKeepingTimeListRow this[int index] {
                get {
                    return ((rep_GoodsKeepingTimeListRow)(this.Rows[index]));
                }
            }
            
            public event rep_GoodsKeepingTimeListRowChangeEventHandler rep_GoodsKeepingTimeListRowChanging;
            
            public event rep_GoodsKeepingTimeListRowChangeEventHandler rep_GoodsKeepingTimeListRowChanged;
            
            public event rep_GoodsKeepingTimeListRowChangeEventHandler rep_GoodsKeepingTimeListRowDeleting;
            
            public event rep_GoodsKeepingTimeListRowChangeEventHandler rep_GoodsKeepingTimeListRowDeleted;
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void Addrep_GoodsKeepingTimeListRow(rep_GoodsKeepingTimeListRow row) {
                this.Rows.Add(row);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public rep_GoodsKeepingTimeListRow Addrep_GoodsKeepingTimeListRow(string SERIES_NUMBER, string NAME, System.DateTime BEST_BEFORE, decimal AMOUNT, string PRODUCER_NAME, decimal PRICE, decimal SUMM, string G_CODE) {
                rep_GoodsKeepingTimeListRow rowrep_GoodsKeepingTimeListRow = ((rep_GoodsKeepingTimeListRow)(this.NewRow()));
                rowrep_GoodsKeepingTimeListRow.ItemArray = new object[] {
                        SERIES_NUMBER,
                        NAME,
                        BEST_BEFORE,
                        AMOUNT,
                        PRODUCER_NAME,
                        PRICE,
                        SUMM,
                        G_CODE};
                this.Rows.Add(rowrep_GoodsKeepingTimeListRow);
                return rowrep_GoodsKeepingTimeListRow;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public virtual System.Collections.IEnumerator GetEnumerator() {
                return this.Rows.GetEnumerator();
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public override System.Data.DataTable Clone() {
                rep_GoodsKeepingTimeListDataTable cln = ((rep_GoodsKeepingTimeListDataTable)(base.Clone()));
                cln.InitVars();
                return cln;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override System.Data.DataTable CreateInstance() {
                return new rep_GoodsKeepingTimeListDataTable();
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal void InitVars() {
                this.columnSERIES_NUMBER = base.Columns["SERIES_NUMBER"];
                this.columnNAME = base.Columns["NAME"];
                this.columnBEST_BEFORE = base.Columns["BEST_BEFORE"];
                this.columnAMOUNT = base.Columns["AMOUNT"];
                this.columnPRODUCER_NAME = base.Columns["PRODUCER_NAME"];
                this.columnPRICE = base.Columns["PRICE"];
                this.columnSUMM = base.Columns["SUMM"];
                this.columnG_CODE = base.Columns["G_CODE"];
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            private void InitClass() {
                this.columnSERIES_NUMBER = new System.Data.DataColumn("SERIES_NUMBER", typeof(string), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnSERIES_NUMBER);
                this.columnNAME = new System.Data.DataColumn("NAME", typeof(string), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnNAME);
                this.columnBEST_BEFORE = new System.Data.DataColumn("BEST_BEFORE", typeof(System.DateTime), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnBEST_BEFORE);
                this.columnAMOUNT = new System.Data.DataColumn("AMOUNT", typeof(decimal), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnAMOUNT);
                this.columnPRODUCER_NAME = new System.Data.DataColumn("PRODUCER_NAME", typeof(string), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnPRODUCER_NAME);
                this.columnPRICE = new System.Data.DataColumn("PRICE", typeof(decimal), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnPRICE);
                this.columnSUMM = new System.Data.DataColumn("SUMM", typeof(decimal), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnSUMM);
                this.columnG_CODE = new System.Data.DataColumn("G_CODE", typeof(string), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnG_CODE);
                this.columnSERIES_NUMBER.MaxLength = 40;
                this.columnNAME.MaxLength = 100;
                this.columnPRODUCER_NAME.MaxLength = 100;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public rep_GoodsKeepingTimeListRow Newrep_GoodsKeepingTimeListRow() {
                return ((rep_GoodsKeepingTimeListRow)(this.NewRow()));
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override System.Data.DataRow NewRowFromBuilder(System.Data.DataRowBuilder builder) {
                return new rep_GoodsKeepingTimeListRow(builder);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override System.Type GetRowType() {
                return typeof(rep_GoodsKeepingTimeListRow);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowChanged(System.Data.DataRowChangeEventArgs e) {
                base.OnRowChanged(e);
                if ((this.rep_GoodsKeepingTimeListRowChanged != null)) {
                    this.rep_GoodsKeepingTimeListRowChanged(this, new rep_GoodsKeepingTimeListRowChangeEvent(((rep_GoodsKeepingTimeListRow)(e.Row)), e.Action));
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowChanging(System.Data.DataRowChangeEventArgs e) {
                base.OnRowChanging(e);
                if ((this.rep_GoodsKeepingTimeListRowChanging != null)) {
                    this.rep_GoodsKeepingTimeListRowChanging(this, new rep_GoodsKeepingTimeListRowChangeEvent(((rep_GoodsKeepingTimeListRow)(e.Row)), e.Action));
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowDeleted(System.Data.DataRowChangeEventArgs e) {
                base.OnRowDeleted(e);
                if ((this.rep_GoodsKeepingTimeListRowDeleted != null)) {
                    this.rep_GoodsKeepingTimeListRowDeleted(this, new rep_GoodsKeepingTimeListRowChangeEvent(((rep_GoodsKeepingTimeListRow)(e.Row)), e.Action));
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowDeleting(System.Data.DataRowChangeEventArgs e) {
                base.OnRowDeleting(e);
                if ((this.rep_GoodsKeepingTimeListRowDeleting != null)) {
                    this.rep_GoodsKeepingTimeListRowDeleting(this, new rep_GoodsKeepingTimeListRowChangeEvent(((rep_GoodsKeepingTimeListRow)(e.Row)), e.Action));
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void Removerep_GoodsKeepingTimeListRow(rep_GoodsKeepingTimeListRow row) {
                this.Rows.Remove(row);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public static System.Xml.Schema.XmlSchemaComplexType GetTypedTableSchema(System.Xml.Schema.XmlSchemaSet xs) {
                System.Xml.Schema.XmlSchemaComplexType type = new System.Xml.Schema.XmlSchemaComplexType();
                System.Xml.Schema.XmlSchemaSequence sequence = new System.Xml.Schema.XmlSchemaSequence();
                GoodsKeepingTimeList ds = new GoodsKeepingTimeList();
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
                attribute2.FixedValue = "rep_GoodsKeepingTimeListDataTable";
                type.Attributes.Add(attribute2);
                type.Particle = sequence;
                return type;
            }
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public partial class rep_GoodsKeepingTimeListRow : System.Data.DataRow {
            
            private rep_GoodsKeepingTimeListDataTable tablerep_GoodsKeepingTimeList;
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal rep_GoodsKeepingTimeListRow(System.Data.DataRowBuilder rb) : 
                    base(rb) {
                this.tablerep_GoodsKeepingTimeList = ((rep_GoodsKeepingTimeListDataTable)(this.Table));
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string SERIES_NUMBER {
                get {
                    try {
                        return ((string)(this[this.tablerep_GoodsKeepingTimeList.SERIES_NUMBERColumn]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("The value for column \'SERIES_NUMBER\' in table \'rep_GoodsKeepingTimeList\' is DBNul" +
                                "l.", e);
                    }
                }
                set {
                    this[this.tablerep_GoodsKeepingTimeList.SERIES_NUMBERColumn] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string NAME {
                get {
                    try {
                        return ((string)(this[this.tablerep_GoodsKeepingTimeList.NAMEColumn]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("The value for column \'NAME\' in table \'rep_GoodsKeepingTimeList\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tablerep_GoodsKeepingTimeList.NAMEColumn] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.DateTime BEST_BEFORE {
                get {
                    try {
                        return ((System.DateTime)(this[this.tablerep_GoodsKeepingTimeList.BEST_BEFOREColumn]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("The value for column \'BEST_BEFORE\' in table \'rep_GoodsKeepingTimeList\' is DBNull." +
                                "", e);
                    }
                }
                set {
                    this[this.tablerep_GoodsKeepingTimeList.BEST_BEFOREColumn] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public decimal AMOUNT {
                get {
                    try {
                        return ((decimal)(this[this.tablerep_GoodsKeepingTimeList.AMOUNTColumn]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("The value for column \'AMOUNT\' in table \'rep_GoodsKeepingTimeList\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tablerep_GoodsKeepingTimeList.AMOUNTColumn] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string PRODUCER_NAME {
                get {
                    try {
                        return ((string)(this[this.tablerep_GoodsKeepingTimeList.PRODUCER_NAMEColumn]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("The value for column \'PRODUCER_NAME\' in table \'rep_GoodsKeepingTimeList\' is DBNul" +
                                "l.", e);
                    }
                }
                set {
                    this[this.tablerep_GoodsKeepingTimeList.PRODUCER_NAMEColumn] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public decimal PRICE {
                get {
                    try {
                        return ((decimal)(this[this.tablerep_GoodsKeepingTimeList.PRICEColumn]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("The value for column \'PRICE\' in table \'rep_GoodsKeepingTimeList\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tablerep_GoodsKeepingTimeList.PRICEColumn] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public decimal SUMM {
                get {
                    try {
                        return ((decimal)(this[this.tablerep_GoodsKeepingTimeList.SUMMColumn]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("The value for column \'SUMM\' in table \'rep_GoodsKeepingTimeList\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tablerep_GoodsKeepingTimeList.SUMMColumn] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string G_CODE {
                get {
                    try {
                        return ((string)(this[this.tablerep_GoodsKeepingTimeList.G_CODEColumn]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("The value for column \'G_CODE\' in table \'rep_GoodsKeepingTimeList\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tablerep_GoodsKeepingTimeList.G_CODEColumn] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsSERIES_NUMBERNull() {
                return this.IsNull(this.tablerep_GoodsKeepingTimeList.SERIES_NUMBERColumn);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetSERIES_NUMBERNull() {
                this[this.tablerep_GoodsKeepingTimeList.SERIES_NUMBERColumn] = System.Convert.DBNull;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsNAMENull() {
                return this.IsNull(this.tablerep_GoodsKeepingTimeList.NAMEColumn);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetNAMENull() {
                this[this.tablerep_GoodsKeepingTimeList.NAMEColumn] = System.Convert.DBNull;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsBEST_BEFORENull() {
                return this.IsNull(this.tablerep_GoodsKeepingTimeList.BEST_BEFOREColumn);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetBEST_BEFORENull() {
                this[this.tablerep_GoodsKeepingTimeList.BEST_BEFOREColumn] = System.Convert.DBNull;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsAMOUNTNull() {
                return this.IsNull(this.tablerep_GoodsKeepingTimeList.AMOUNTColumn);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetAMOUNTNull() {
                this[this.tablerep_GoodsKeepingTimeList.AMOUNTColumn] = System.Convert.DBNull;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsPRODUCER_NAMENull() {
                return this.IsNull(this.tablerep_GoodsKeepingTimeList.PRODUCER_NAMEColumn);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetPRODUCER_NAMENull() {
                this[this.tablerep_GoodsKeepingTimeList.PRODUCER_NAMEColumn] = System.Convert.DBNull;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsPRICENull() {
                return this.IsNull(this.tablerep_GoodsKeepingTimeList.PRICEColumn);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetPRICENull() {
                this[this.tablerep_GoodsKeepingTimeList.PRICEColumn] = System.Convert.DBNull;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsSUMMNull() {
                return this.IsNull(this.tablerep_GoodsKeepingTimeList.SUMMColumn);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetSUMMNull() {
                this[this.tablerep_GoodsKeepingTimeList.SUMMColumn] = System.Convert.DBNull;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsG_CODENull() {
                return this.IsNull(this.tablerep_GoodsKeepingTimeList.G_CODEColumn);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetG_CODENull() {
                this[this.tablerep_GoodsKeepingTimeList.G_CODEColumn] = System.Convert.DBNull;
            }
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public class rep_GoodsKeepingTimeListRowChangeEvent : System.EventArgs {
            
            private rep_GoodsKeepingTimeListRow eventRow;
            
            private System.Data.DataRowAction eventAction;
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public rep_GoodsKeepingTimeListRowChangeEvent(rep_GoodsKeepingTimeListRow row, System.Data.DataRowAction action) {
                this.eventRow = row;
                this.eventAction = action;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public rep_GoodsKeepingTimeListRow Row {
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