﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1433
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591

namespace ReturnToContractor {
    
    
    /// <summary>
    ///Represents a strongly typed in-memory cache of data.
    ///</summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    [global::System.Serializable()]
    [global::System.ComponentModel.DesignerCategoryAttribute("code")]
    [global::System.ComponentModel.ToolboxItem(true)]
    [global::System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedDataSetSchema")]
    [global::System.Xml.Serialization.XmlRootAttribute("ReturnToContractor_DS")]
    [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.DataSet")]
    public partial class ReturnToContractor_DS : global::System.Data.DataSet {
        
        private RETURN_TO_CONTRACTOR_TABLEDataTable tableRETURN_TO_CONTRACTOR_TABLE;
        
        private global::System.Data.SchemaSerializationMode _schemaSerializationMode = global::System.Data.SchemaSerializationMode.IncludeSchema;
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public ReturnToContractor_DS() {
            this.BeginInit();
            this.InitClass();
            global::System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new global::System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += schemaChangedHandler;
            base.Relations.CollectionChanged += schemaChangedHandler;
            this.EndInit();
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected ReturnToContractor_DS(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context) : 
                base(info, context, false) {
            if ((this.IsBinarySerialized(info, context) == true)) {
                this.InitVars(false);
                global::System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler1 = new global::System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
                this.Tables.CollectionChanged += schemaChangedHandler1;
                this.Relations.CollectionChanged += schemaChangedHandler1;
                return;
            }
            string strSchema = ((string)(info.GetValue("XmlSchema", typeof(string))));
            if ((this.DetermineSchemaSerializationMode(info, context) == global::System.Data.SchemaSerializationMode.IncludeSchema)) {
                global::System.Data.DataSet ds = new global::System.Data.DataSet();
                ds.ReadXmlSchema(new global::System.Xml.XmlTextReader(new global::System.IO.StringReader(strSchema)));
                if ((ds.Tables["RETURN_TO_CONTRACTOR_TABLE"] != null)) {
                    base.Tables.Add(new RETURN_TO_CONTRACTOR_TABLEDataTable(ds.Tables["RETURN_TO_CONTRACTOR_TABLE"]));
                }
                this.DataSetName = ds.DataSetName;
                this.Prefix = ds.Prefix;
                this.Namespace = ds.Namespace;
                this.Locale = ds.Locale;
                this.CaseSensitive = ds.CaseSensitive;
                this.EnforceConstraints = ds.EnforceConstraints;
                this.Merge(ds, false, global::System.Data.MissingSchemaAction.Add);
                this.InitVars();
            }
            else {
                this.ReadXmlSchema(new global::System.Xml.XmlTextReader(new global::System.IO.StringReader(strSchema)));
            }
            this.GetSerializationData(info, context);
            global::System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new global::System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += schemaChangedHandler;
            this.Relations.CollectionChanged += schemaChangedHandler;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.Browsable(false)]
        [global::System.ComponentModel.DesignerSerializationVisibility(global::System.ComponentModel.DesignerSerializationVisibility.Content)]
        public RETURN_TO_CONTRACTOR_TABLEDataTable RETURN_TO_CONTRACTOR_TABLE {
            get {
                return this.tableRETURN_TO_CONTRACTOR_TABLE;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.BrowsableAttribute(true)]
        [global::System.ComponentModel.DesignerSerializationVisibilityAttribute(global::System.ComponentModel.DesignerSerializationVisibility.Visible)]
        public override global::System.Data.SchemaSerializationMode SchemaSerializationMode {
            get {
                return this._schemaSerializationMode;
            }
            set {
                this._schemaSerializationMode = value;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.DesignerSerializationVisibilityAttribute(global::System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public new global::System.Data.DataTableCollection Tables {
            get {
                return base.Tables;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.DesignerSerializationVisibilityAttribute(global::System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public new global::System.Data.DataRelationCollection Relations {
            get {
                return base.Relations;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override void InitializeDerivedDataSet() {
            this.BeginInit();
            this.InitClass();
            this.EndInit();
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public override global::System.Data.DataSet Clone() {
            ReturnToContractor_DS cln = ((ReturnToContractor_DS)(base.Clone()));
            cln.InitVars();
            cln.SchemaSerializationMode = this.SchemaSerializationMode;
            return cln;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override bool ShouldSerializeTables() {
            return false;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override bool ShouldSerializeRelations() {
            return false;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override void ReadXmlSerializable(global::System.Xml.XmlReader reader) {
            if ((this.DetermineSchemaSerializationMode(reader) == global::System.Data.SchemaSerializationMode.IncludeSchema)) {
                this.Reset();
                global::System.Data.DataSet ds = new global::System.Data.DataSet();
                ds.ReadXml(reader);
                if ((ds.Tables["RETURN_TO_CONTRACTOR_TABLE"] != null)) {
                    base.Tables.Add(new RETURN_TO_CONTRACTOR_TABLEDataTable(ds.Tables["RETURN_TO_CONTRACTOR_TABLE"]));
                }
                this.DataSetName = ds.DataSetName;
                this.Prefix = ds.Prefix;
                this.Namespace = ds.Namespace;
                this.Locale = ds.Locale;
                this.CaseSensitive = ds.CaseSensitive;
                this.EnforceConstraints = ds.EnforceConstraints;
                this.Merge(ds, false, global::System.Data.MissingSchemaAction.Add);
                this.InitVars();
            }
            else {
                this.ReadXml(reader);
                this.InitVars();
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override global::System.Xml.Schema.XmlSchema GetSchemaSerializable() {
            global::System.IO.MemoryStream stream = new global::System.IO.MemoryStream();
            this.WriteXmlSchema(new global::System.Xml.XmlTextWriter(stream, null));
            stream.Position = 0;
            return global::System.Xml.Schema.XmlSchema.Read(new global::System.Xml.XmlTextReader(stream), null);
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        internal void InitVars() {
            this.InitVars(true);
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        internal void InitVars(bool initTable) {
            this.tableRETURN_TO_CONTRACTOR_TABLE = ((RETURN_TO_CONTRACTOR_TABLEDataTable)(base.Tables["RETURN_TO_CONTRACTOR_TABLE"]));
            if ((initTable == true)) {
                if ((this.tableRETURN_TO_CONTRACTOR_TABLE != null)) {
                    this.tableRETURN_TO_CONTRACTOR_TABLE.InitVars();
                }
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void InitClass() {
            this.DataSetName = "ReturnToContractor_DS";
            this.Prefix = "";
            this.Namespace = "http://tempuri.org/ReturnToContractor_DS.xsd";
            this.EnforceConstraints = true;
            this.SchemaSerializationMode = global::System.Data.SchemaSerializationMode.IncludeSchema;
            this.tableRETURN_TO_CONTRACTOR_TABLE = new RETURN_TO_CONTRACTOR_TABLEDataTable();
            base.Tables.Add(this.tableRETURN_TO_CONTRACTOR_TABLE);
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private bool ShouldSerializeRETURN_TO_CONTRACTOR_TABLE() {
            return false;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void SchemaChanged(object sender, global::System.ComponentModel.CollectionChangeEventArgs e) {
            if ((e.Action == global::System.ComponentModel.CollectionChangeAction.Remove)) {
                this.InitVars();
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static global::System.Xml.Schema.XmlSchemaComplexType GetTypedDataSetSchema(global::System.Xml.Schema.XmlSchemaSet xs) {
            ReturnToContractor_DS ds = new ReturnToContractor_DS();
            global::System.Xml.Schema.XmlSchemaComplexType type = new global::System.Xml.Schema.XmlSchemaComplexType();
            global::System.Xml.Schema.XmlSchemaSequence sequence = new global::System.Xml.Schema.XmlSchemaSequence();
            global::System.Xml.Schema.XmlSchemaAny any = new global::System.Xml.Schema.XmlSchemaAny();
            any.Namespace = ds.Namespace;
            sequence.Items.Add(any);
            type.Particle = sequence;
            global::System.Xml.Schema.XmlSchema dsSchema = ds.GetSchemaSerializable();
            if (xs.Contains(dsSchema.TargetNamespace)) {
                global::System.IO.MemoryStream s1 = new global::System.IO.MemoryStream();
                global::System.IO.MemoryStream s2 = new global::System.IO.MemoryStream();
                try {
                    global::System.Xml.Schema.XmlSchema schema = null;
                    dsSchema.Write(s1);
                    for (global::System.Collections.IEnumerator schemas = xs.Schemas(dsSchema.TargetNamespace).GetEnumerator(); schemas.MoveNext(); ) {
                        schema = ((global::System.Xml.Schema.XmlSchema)(schemas.Current));
                        s2.SetLength(0);
                        schema.Write(s2);
                        if ((s1.Length == s2.Length)) {
                            s1.Position = 0;
                            s2.Position = 0;
                            for (; ((s1.Position != s1.Length) 
                                        && (s1.ReadByte() == s2.ReadByte())); ) {
                                ;
                            }
                            if ((s1.Position == s1.Length)) {
                                return type;
                            }
                        }
                    }
                }
                finally {
                    if ((s1 != null)) {
                        s1.Close();
                    }
                    if ((s2 != null)) {
                        s2.Close();
                    }
                }
            }
            xs.Add(dsSchema);
            return type;
        }
        
        public delegate void RETURN_TO_CONTRACTOR_TABLERowChangeEventHandler(object sender, RETURN_TO_CONTRACTOR_TABLERowChangeEvent e);
        
        /// <summary>
        ///Represents the strongly named DataTable class.
        ///</summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        [global::System.Serializable()]
        [global::System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")]
        public partial class RETURN_TO_CONTRACTOR_TABLEDataTable : global::System.Data.DataTable, global::System.Collections.IEnumerable {
            
            private global::System.Data.DataColumn columnDATE;
            
            private global::System.Data.DataColumn columnNAME;
            
            private global::System.Data.DataColumn columnMNEMOCODE;
            
            private global::System.Data.DataColumn columnBASE_DOCUMENT_NAME;
            
            private global::System.Data.DataColumn columnTOTAL;
            
            private global::System.Data.DataColumn columnTOTAL_RET;
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public RETURN_TO_CONTRACTOR_TABLEDataTable() {
                this.TableName = "RETURN_TO_CONTRACTOR_TABLE";
                this.BeginInit();
                this.InitClass();
                this.EndInit();
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal RETURN_TO_CONTRACTOR_TABLEDataTable(global::System.Data.DataTable table) {
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
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected RETURN_TO_CONTRACTOR_TABLEDataTable(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context) : 
                    base(info, context) {
                this.InitVars();
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn DATEColumn {
                get {
                    return this.columnDATE;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn NAMEColumn {
                get {
                    return this.columnNAME;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn MNEMOCODEColumn {
                get {
                    return this.columnMNEMOCODE;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn BASE_DOCUMENT_NAMEColumn {
                get {
                    return this.columnBASE_DOCUMENT_NAME;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn TOTALColumn {
                get {
                    return this.columnTOTAL;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn TOTAL_RETColumn {
                get {
                    return this.columnTOTAL_RET;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.ComponentModel.Browsable(false)]
            public int Count {
                get {
                    return this.Rows.Count;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public RETURN_TO_CONTRACTOR_TABLERow this[int index] {
                get {
                    return ((RETURN_TO_CONTRACTOR_TABLERow)(this.Rows[index]));
                }
            }
            
            public event RETURN_TO_CONTRACTOR_TABLERowChangeEventHandler RETURN_TO_CONTRACTOR_TABLERowChanging;
            
            public event RETURN_TO_CONTRACTOR_TABLERowChangeEventHandler RETURN_TO_CONTRACTOR_TABLERowChanged;
            
            public event RETURN_TO_CONTRACTOR_TABLERowChangeEventHandler RETURN_TO_CONTRACTOR_TABLERowDeleting;
            
            public event RETURN_TO_CONTRACTOR_TABLERowChangeEventHandler RETURN_TO_CONTRACTOR_TABLERowDeleted;
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void AddRETURN_TO_CONTRACTOR_TABLERow(RETURN_TO_CONTRACTOR_TABLERow row) {
                this.Rows.Add(row);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public RETURN_TO_CONTRACTOR_TABLERow AddRETURN_TO_CONTRACTOR_TABLERow(string DATE, string NAME, string MNEMOCODE, string BASE_DOCUMENT_NAME, string TOTAL, string TOTAL_RET) {
                RETURN_TO_CONTRACTOR_TABLERow rowRETURN_TO_CONTRACTOR_TABLERow = ((RETURN_TO_CONTRACTOR_TABLERow)(this.NewRow()));
                object[] columnValuesArray = new object[] {
                        DATE,
                        NAME,
                        MNEMOCODE,
                        BASE_DOCUMENT_NAME,
                        TOTAL,
                        TOTAL_RET};
                rowRETURN_TO_CONTRACTOR_TABLERow.ItemArray = columnValuesArray;
                this.Rows.Add(rowRETURN_TO_CONTRACTOR_TABLERow);
                return rowRETURN_TO_CONTRACTOR_TABLERow;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public virtual global::System.Collections.IEnumerator GetEnumerator() {
                return this.Rows.GetEnumerator();
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public override global::System.Data.DataTable Clone() {
                RETURN_TO_CONTRACTOR_TABLEDataTable cln = ((RETURN_TO_CONTRACTOR_TABLEDataTable)(base.Clone()));
                cln.InitVars();
                return cln;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override global::System.Data.DataTable CreateInstance() {
                return new RETURN_TO_CONTRACTOR_TABLEDataTable();
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal void InitVars() {
                this.columnDATE = base.Columns["DATE"];
                this.columnNAME = base.Columns["NAME"];
                this.columnMNEMOCODE = base.Columns["MNEMOCODE"];
                this.columnBASE_DOCUMENT_NAME = base.Columns["BASE_DOCUMENT_NAME"];
                this.columnTOTAL = base.Columns["TOTAL"];
                this.columnTOTAL_RET = base.Columns["TOTAL_RET"];
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            private void InitClass() {
                this.columnDATE = new global::System.Data.DataColumn("DATE", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnDATE);
                this.columnNAME = new global::System.Data.DataColumn("NAME", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnNAME);
                this.columnMNEMOCODE = new global::System.Data.DataColumn("MNEMOCODE", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnMNEMOCODE);
                this.columnBASE_DOCUMENT_NAME = new global::System.Data.DataColumn("BASE_DOCUMENT_NAME", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnBASE_DOCUMENT_NAME);
                this.columnTOTAL = new global::System.Data.DataColumn("TOTAL", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnTOTAL);
                this.columnTOTAL_RET = new global::System.Data.DataColumn("TOTAL_RET", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnTOTAL_RET);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public RETURN_TO_CONTRACTOR_TABLERow NewRETURN_TO_CONTRACTOR_TABLERow() {
                return ((RETURN_TO_CONTRACTOR_TABLERow)(this.NewRow()));
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override global::System.Data.DataRow NewRowFromBuilder(global::System.Data.DataRowBuilder builder) {
                return new RETURN_TO_CONTRACTOR_TABLERow(builder);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override global::System.Type GetRowType() {
                return typeof(RETURN_TO_CONTRACTOR_TABLERow);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowChanged(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowChanged(e);
                if ((this.RETURN_TO_CONTRACTOR_TABLERowChanged != null)) {
                    this.RETURN_TO_CONTRACTOR_TABLERowChanged(this, new RETURN_TO_CONTRACTOR_TABLERowChangeEvent(((RETURN_TO_CONTRACTOR_TABLERow)(e.Row)), e.Action));
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowChanging(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowChanging(e);
                if ((this.RETURN_TO_CONTRACTOR_TABLERowChanging != null)) {
                    this.RETURN_TO_CONTRACTOR_TABLERowChanging(this, new RETURN_TO_CONTRACTOR_TABLERowChangeEvent(((RETURN_TO_CONTRACTOR_TABLERow)(e.Row)), e.Action));
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowDeleted(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowDeleted(e);
                if ((this.RETURN_TO_CONTRACTOR_TABLERowDeleted != null)) {
                    this.RETURN_TO_CONTRACTOR_TABLERowDeleted(this, new RETURN_TO_CONTRACTOR_TABLERowChangeEvent(((RETURN_TO_CONTRACTOR_TABLERow)(e.Row)), e.Action));
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowDeleting(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowDeleting(e);
                if ((this.RETURN_TO_CONTRACTOR_TABLERowDeleting != null)) {
                    this.RETURN_TO_CONTRACTOR_TABLERowDeleting(this, new RETURN_TO_CONTRACTOR_TABLERowChangeEvent(((RETURN_TO_CONTRACTOR_TABLERow)(e.Row)), e.Action));
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void RemoveRETURN_TO_CONTRACTOR_TABLERow(RETURN_TO_CONTRACTOR_TABLERow row) {
                this.Rows.Remove(row);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public static global::System.Xml.Schema.XmlSchemaComplexType GetTypedTableSchema(global::System.Xml.Schema.XmlSchemaSet xs) {
                global::System.Xml.Schema.XmlSchemaComplexType type = new global::System.Xml.Schema.XmlSchemaComplexType();
                global::System.Xml.Schema.XmlSchemaSequence sequence = new global::System.Xml.Schema.XmlSchemaSequence();
                ReturnToContractor_DS ds = new ReturnToContractor_DS();
                global::System.Xml.Schema.XmlSchemaAny any1 = new global::System.Xml.Schema.XmlSchemaAny();
                any1.Namespace = "http://www.w3.org/2001/XMLSchema";
                any1.MinOccurs = new decimal(0);
                any1.MaxOccurs = decimal.MaxValue;
                any1.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
                sequence.Items.Add(any1);
                global::System.Xml.Schema.XmlSchemaAny any2 = new global::System.Xml.Schema.XmlSchemaAny();
                any2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
                any2.MinOccurs = new decimal(1);
                any2.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
                sequence.Items.Add(any2);
                global::System.Xml.Schema.XmlSchemaAttribute attribute1 = new global::System.Xml.Schema.XmlSchemaAttribute();
                attribute1.Name = "namespace";
                attribute1.FixedValue = ds.Namespace;
                type.Attributes.Add(attribute1);
                global::System.Xml.Schema.XmlSchemaAttribute attribute2 = new global::System.Xml.Schema.XmlSchemaAttribute();
                attribute2.Name = "tableTypeName";
                attribute2.FixedValue = "RETURN_TO_CONTRACTOR_TABLEDataTable";
                type.Attributes.Add(attribute2);
                type.Particle = sequence;
                global::System.Xml.Schema.XmlSchema dsSchema = ds.GetSchemaSerializable();
                if (xs.Contains(dsSchema.TargetNamespace)) {
                    global::System.IO.MemoryStream s1 = new global::System.IO.MemoryStream();
                    global::System.IO.MemoryStream s2 = new global::System.IO.MemoryStream();
                    try {
                        global::System.Xml.Schema.XmlSchema schema = null;
                        dsSchema.Write(s1);
                        for (global::System.Collections.IEnumerator schemas = xs.Schemas(dsSchema.TargetNamespace).GetEnumerator(); schemas.MoveNext(); ) {
                            schema = ((global::System.Xml.Schema.XmlSchema)(schemas.Current));
                            s2.SetLength(0);
                            schema.Write(s2);
                            if ((s1.Length == s2.Length)) {
                                s1.Position = 0;
                                s2.Position = 0;
                                for (; ((s1.Position != s1.Length) 
                                            && (s1.ReadByte() == s2.ReadByte())); ) {
                                    ;
                                }
                                if ((s1.Position == s1.Length)) {
                                    return type;
                                }
                            }
                        }
                    }
                    finally {
                        if ((s1 != null)) {
                            s1.Close();
                        }
                        if ((s2 != null)) {
                            s2.Close();
                        }
                    }
                }
                xs.Add(dsSchema);
                return type;
            }
        }
        
        /// <summary>
        ///Represents strongly named DataRow class.
        ///</summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public partial class RETURN_TO_CONTRACTOR_TABLERow : global::System.Data.DataRow {
            
            private RETURN_TO_CONTRACTOR_TABLEDataTable tableRETURN_TO_CONTRACTOR_TABLE;
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal RETURN_TO_CONTRACTOR_TABLERow(global::System.Data.DataRowBuilder rb) : 
                    base(rb) {
                this.tableRETURN_TO_CONTRACTOR_TABLE = ((RETURN_TO_CONTRACTOR_TABLEDataTable)(this.Table));
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string DATE {
                get {
                    try {
                        return ((string)(this[this.tableRETURN_TO_CONTRACTOR_TABLE.DATEColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'DATE\' in table \'RETURN_TO_CONTRACTOR_TABLE\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableRETURN_TO_CONTRACTOR_TABLE.DATEColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string NAME {
                get {
                    try {
                        return ((string)(this[this.tableRETURN_TO_CONTRACTOR_TABLE.NAMEColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'NAME\' in table \'RETURN_TO_CONTRACTOR_TABLE\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableRETURN_TO_CONTRACTOR_TABLE.NAMEColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string MNEMOCODE {
                get {
                    try {
                        return ((string)(this[this.tableRETURN_TO_CONTRACTOR_TABLE.MNEMOCODEColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'MNEMOCODE\' in table \'RETURN_TO_CONTRACTOR_TABLE\' is DBNull." +
                                "", e);
                    }
                }
                set {
                    this[this.tableRETURN_TO_CONTRACTOR_TABLE.MNEMOCODEColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string BASE_DOCUMENT_NAME {
                get {
                    try {
                        return ((string)(this[this.tableRETURN_TO_CONTRACTOR_TABLE.BASE_DOCUMENT_NAMEColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'BASE_DOCUMENT_NAME\' in table \'RETURN_TO_CONTRACTOR_TABLE\' i" +
                                "s DBNull.", e);
                    }
                }
                set {
                    this[this.tableRETURN_TO_CONTRACTOR_TABLE.BASE_DOCUMENT_NAMEColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string TOTAL {
                get {
                    try {
                        return ((string)(this[this.tableRETURN_TO_CONTRACTOR_TABLE.TOTALColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'TOTAL\' in table \'RETURN_TO_CONTRACTOR_TABLE\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableRETURN_TO_CONTRACTOR_TABLE.TOTALColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string TOTAL_RET {
                get {
                    try {
                        return ((string)(this[this.tableRETURN_TO_CONTRACTOR_TABLE.TOTAL_RETColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'TOTAL_RET\' in table \'RETURN_TO_CONTRACTOR_TABLE\' is DBNull." +
                                "", e);
                    }
                }
                set {
                    this[this.tableRETURN_TO_CONTRACTOR_TABLE.TOTAL_RETColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsDATENull() {
                return this.IsNull(this.tableRETURN_TO_CONTRACTOR_TABLE.DATEColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetDATENull() {
                this[this.tableRETURN_TO_CONTRACTOR_TABLE.DATEColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsNAMENull() {
                return this.IsNull(this.tableRETURN_TO_CONTRACTOR_TABLE.NAMEColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetNAMENull() {
                this[this.tableRETURN_TO_CONTRACTOR_TABLE.NAMEColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsMNEMOCODENull() {
                return this.IsNull(this.tableRETURN_TO_CONTRACTOR_TABLE.MNEMOCODEColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetMNEMOCODENull() {
                this[this.tableRETURN_TO_CONTRACTOR_TABLE.MNEMOCODEColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsBASE_DOCUMENT_NAMENull() {
                return this.IsNull(this.tableRETURN_TO_CONTRACTOR_TABLE.BASE_DOCUMENT_NAMEColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetBASE_DOCUMENT_NAMENull() {
                this[this.tableRETURN_TO_CONTRACTOR_TABLE.BASE_DOCUMENT_NAMEColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsTOTALNull() {
                return this.IsNull(this.tableRETURN_TO_CONTRACTOR_TABLE.TOTALColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetTOTALNull() {
                this[this.tableRETURN_TO_CONTRACTOR_TABLE.TOTALColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsTOTAL_RETNull() {
                return this.IsNull(this.tableRETURN_TO_CONTRACTOR_TABLE.TOTAL_RETColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetTOTAL_RETNull() {
                this[this.tableRETURN_TO_CONTRACTOR_TABLE.TOTAL_RETColumn] = global::System.Convert.DBNull;
            }
        }
        
        /// <summary>
        ///Row event argument class
        ///</summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public class RETURN_TO_CONTRACTOR_TABLERowChangeEvent : global::System.EventArgs {
            
            private RETURN_TO_CONTRACTOR_TABLERow eventRow;
            
            private global::System.Data.DataRowAction eventAction;
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public RETURN_TO_CONTRACTOR_TABLERowChangeEvent(RETURN_TO_CONTRACTOR_TABLERow row, global::System.Data.DataRowAction action) {
                this.eventRow = row;
                this.eventAction = action;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public RETURN_TO_CONTRACTOR_TABLERow Row {
                get {
                    return this.eventRow;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataRowAction Action {
                get {
                    return this.eventAction;
                }
            }
        }
    }
}

#pragma warning restore 1591