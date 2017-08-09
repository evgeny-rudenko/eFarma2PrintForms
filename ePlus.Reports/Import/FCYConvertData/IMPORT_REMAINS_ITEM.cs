using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Text;
using System.Xml;
using ePlus.MetaData.Core;

namespace RCYConvertData
{
  class RequiredAttribute:Attribute
  {
    bool required = true;

    public bool Required
    {
      get { return required; }
    }

    public RequiredAttribute()
    {
    }

    public RequiredAttribute(bool required)
    {
      this.required = required;
    }
    
    public static bool IsEmpty(object value)
    {
      if (value == null)
        return true;
      if (value is string && string.IsNullOrEmpty(((string)value).Trim()))
        return true;
      if (value is decimal && (decimal)value <= 0)
        return true;
      if (value is double && (double)value <= 0)
        return true;
      if (value is int && (int)value <= 0)
        return true;
      if (value is long && (long)value <= 0)
        return true;
      if (value is DateTime && 
          SqlDateTime.MinValue.Value >= (DateTime)value && 
          SqlDateTime.MaxValue.Value <= (DateTime)value && 
          Utils.GetSqlDate((DateTime)value) == DateTime.MinValue)
        return true;
      if (value is Guid && (Guid)value==Guid.Empty)
        return true;
      return false;
    }
    
  }
  
  class FormatAttribute:Attribute
  {
    private string format;

    public FormatAttribute()
    {
      this.format = string.Empty;
    }

    private bool checkEmpty = true;

    public bool CheckEmpty
    {
      get { return checkEmpty; }
    }

    public FormatAttribute(string format, bool checkEmpty)
    {
      this.format = format;
      this.checkEmpty = checkEmpty;
    }

    public FormatAttribute(string format)
    {
      this.format = format;
    }

    public string Format(object value)
    {
      if (value == null)
        return string.Empty;
      if (checkEmpty && RequiredAttribute.IsEmpty(value))
        return string.Empty;
      if (value is IFormattable && !string.IsNullOrEmpty(format))
        return ((IFormattable)value).ToString(format, null);
      return value.ToString();
    }
  }
  
  interface IObject
  {
    Guid ID_GLOBAL
    {
      get;
    }
  }
  
  interface ISupportsErrorState
  {
    List<RowError> Errors
    {
      get;
    }
  }
  class IMPORT_REMAINS_ITEM : IObject, ISupportsErrorState
  {
    private Guid id_global = Guid.NewGuid();   // для уменьшения трафика с SQL сервера
    
    private string name;
    private double kol;
    private string barcode;
    private string seriy;
    private DateTime srok_godn;
    private string num_sert;
    private string gtd;
    private double q_first;
    private double cost_price;
    private double sale_price;
    private double supplierid;
    private DateTime doc_date;
    private string doc_num;
    private string code_stu;

    public Guid ID_GLOBAL
    {
      get { return id_global; }
      set { id_global = value; }
    }

    [Required]
    [Format]
    public string NAME
    {
      get { return name; }
      set { name = value; }
    }

    [Required]
    [Format("#0.00")]
    public double KOL
    {
      get { return kol; }
      set { kol = value; }
    }

    [Required]
    [Format]
    public string BARCODE
    {
      get { return barcode; }
      set { barcode = value; }
    }

    [Format]
    public string SERIY
    {
      get { return seriy; }
      set { seriy = value; }
    }

    [Format("dd.MM.yyyy")]
    public DateTime SROK_GODN
    {
      get { return srok_godn; }
      set { srok_godn = value; }
    }

    [Format]
    public string NUM_SERT
    {
      get { return num_sert; }
      set { num_sert = value; }
    }

    [Format]
    public string GTD
    {
      get { return gtd; }
      set { gtd = value; }
    }

    [Format("#0")]
    public double Q_FIRST
    {
      get { return q_first; }
      set { q_first = value; }
    }

    private double man_price;
    [Format("#0.00")]
    public double MAN_PRICE
    {
      get { return man_price; }
      set { man_price = value; }
    }
    
    [Required]
    [Format("#0.00")]
    public double COST_PRICE
    {
      get { return cost_price; }
      set { cost_price = value; }
    }

    [Required]
    [Format("#0.00")]
    public double SALE_PRICE
    {
      get { return sale_price; }
      set { sale_price = value; }
    }

    private double nds;
    [Format("#0", false)]
    public double NDS
    {
      get { return nds; }
      set { nds = value; }
    }

//    private Int16 nds;
//    [Format("#0", false)]
//    public Int16 NDS
//    {
//      get { return nds; }
//      set { nds = value; }
//    }

    [Required]
    [Format("#0")]
    public double SUPPLIERID
    {
      get { return supplierid; }
      set { supplierid = value; }
    }

    [Format("dd.MM.yyyy")]
    public DateTime DOK_DATE
    {
      get { return doc_date; }
      set { doc_date = value; }
    }

    [Format]
    public string DOK_NUM
    {
      get { return doc_num; }
      set { doc_num = value; }
    }

    [Required]
    [Format]
    public string CODE_STU
    {
      get { return code_stu; }
      set { code_stu = value; }
    }


    private string num_sf;
    private DateTime data_sf;

    public string NUM_SF
    {
      get { return num_sf; }
      set { num_sf = value; }
    }

    public DateTime DATA_SF
    {
      get { return data_sf; }
      set { data_sf = value; }
    }

    public void ToXml(XmlNode node)
    {
      if (!RequiredAttribute.IsEmpty(ID_GLOBAL))
        Utils.AddNode(node, "ID_GLOBAL", ID_GLOBAL);   // для уменьшения трафика с SQL сервера
      if (!RequiredAttribute.IsEmpty(NAME))
        Utils.AddNode(node, "NAME", NAME);
      if (!RequiredAttribute.IsEmpty(KOL))
        Utils.AddNode(node, "KOL", Utils.GetDecimal(KOL));
      if (!RequiredAttribute.IsEmpty(BARCODE))
        Utils.AddNode(node, "BARCODE", BARCODE);
      if (!RequiredAttribute.IsEmpty(SERIY))
        Utils.AddNode(node, "SERIY", SERIY);
      if (!RequiredAttribute.IsEmpty(SROK_GODN))
        Utils.AddNode(node, "SROK_GODN", Utils.GetSqlDate(SROK_GODN));
      if (!RequiredAttribute.IsEmpty(NUM_SERT))
        Utils.AddNode(node, "NUM_SERT", NUM_SERT);
      if (!RequiredAttribute.IsEmpty(GTD))
        Utils.AddNode(node, "GTD", GTD);
      if (!RequiredAttribute.IsEmpty(Q_FIRST))
        Utils.AddNode(node, "Q_FIRST", Utils.GetInt(Q_FIRST));
      Utils.AddNode(node, "MAN_PRICE", Utils.GetDecimal(MAN_PRICE));
      if (!RequiredAttribute.IsEmpty(COST_PRICE))
        Utils.AddNode(node, "COST_PRICE", Utils.GetDecimal(COST_PRICE));
      if (!RequiredAttribute.IsEmpty(SALE_PRICE))
        Utils.AddNode(node, "SALE_PRICE", Utils.GetDecimal(SALE_PRICE));

      Utils.AddNode(node, "NDS", Utils.GetDecimal(NDS));

      if (!RequiredAttribute.IsEmpty(SUPPLIERID))
        Utils.AddNode(node, "SUPPLIERID", Utils.GetLong(SUPPLIERID));
      if (!RequiredAttribute.IsEmpty(DOK_DATE))
        Utils.AddNode(node, "DOC_DATE", DOK_DATE);
      if (!RequiredAttribute.IsEmpty(DOK_NUM))
        Utils.AddNode(node, "DOC_NUM", DOK_NUM);
      if (!RequiredAttribute.IsEmpty(CODE_STU))
        Utils.AddNode(node, "CODE_STU", CODE_STU);
      if (!RequiredAttribute.IsEmpty(NUM_SF))
        Utils.AddNode(node, "NUM_SF", NUM_SF);
      if (!RequiredAttribute.IsEmpty(DATA_SF))
        Utils.AddNode(node, "DATA_SF", DATA_SF);
    }

//    public static IMPORT_REMAINS_ITEM Merge(IMPORT_REMAINS_ITEM original, IMPORT_REMAINS_ITEM item)
//    {
//      IMPORT_REMAINS_ITEM merged = original.Copy();
//      merged.kol = item.kol;
//      merged.q_first = item.q_first;
//      merged.sale_price = item.sale_price;
//      merged.cost_price = item.cost_price;
//      return merged;
//    }
    
    public IMPORT_REMAINS_ITEM Copy()
    {
      IMPORT_REMAINS_ITEM item = new IMPORT_REMAINS_ITEM();
      item.name = name;
      item.kol = kol;
      item.barcode = barcode;
      item.seriy = seriy;
      item.srok_godn = srok_godn;
      item.num_sert = num_sert;
      item.gtd = gtd;
      item.q_first = q_first;
      item.man_price = man_price;
      item.cost_price = cost_price;
      item.sale_price = sale_price;
      item.supplierid = supplierid;
      item.doc_date = doc_date;
      item.doc_num = doc_num;
      item.code_stu = code_stu;
      item.nds = nds;

      item.num_sf = num_sf;
      item.data_sf = data_sf;

      foreach (RowError re in errors)
        item.errors.Add(new RowError(re.RowErrorLevel, re.ErrorText));
      return item;
    }

    private List<RowError> errors = new List<RowError>();

    public List<RowError> Errors
    {
      get { return errors; }
    }

    public void SetError(string errorText, RowErrorLevel errorLevel)
    {
      errors.Add(new RowError(errorLevel, errorText));
    }
  }
  
  enum RowErrorLevel{Critical, Warning, None}
  
  class RowError
  {
    RowErrorLevel rowErrorLevel;
    string errorText =string.Empty;

    public RowErrorLevel RowErrorLevel
    {
      get { return rowErrorLevel; }
    }

    public string ErrorText
    {
      get { return errorText; }
    }

    public RowError(RowErrorLevel rowErrorLevel, string errorText)
    {
      this.rowErrorLevel = rowErrorLevel;
      this.errorText = errorText;
    }
    
    public static RowError None()
    {
      return new RowError(RowErrorLevel.None, string.Empty);
    }
    
    public static RowError Critical(string errorText)
    {
      return new RowError(RowErrorLevel.Critical, errorText);      
    }
    
    public static RowError Warning(string errorText)
    {
      return new RowError(RowErrorLevel.Warning, errorText);            
    }
  }
  
  class RowErrorLevelDescription
  {
    RowErrorLevel rowErrorLevel;
    string description = string.Empty;

    public RowErrorLevel RowErrorLevel
    {
      get { return rowErrorLevel; }
    }

    public string Description
    {
      get { return description; }
    }

    public RowErrorLevelDescription(RowErrorLevel rowErrorLevel)
    {
      this.rowErrorLevel = rowErrorLevel;
      switch (rowErrorLevel)
      {
        case RowErrorLevel.Critical:
          description = "ОШИБКА";
          break;
        case RowErrorLevel.Warning:
          description = "ПРЕДУПРЕЖДЕНИЕ";
          break;
        case RowErrorLevel.None:
          break;
      }
    }

    public override string ToString()
    {
      return description;
    }
  }

  class CROSS_SUP_ITEM : IObject, ISupportsErrorState
  {
    private Guid id_global = Guid.NewGuid();
    private double supplierid;
    private double supidap;
    private List<RowError> errors = new List<RowError>();

    public Guid ID_GLOBAL
    {
      get { return id_global; }
    }

    [Format("#0")]
    public double SUPPLIERID
    {
      get { return supplierid; }
      set { supplierid = value; }
    }

    [Format("#0")]
    public double SUPIDAP
    {
      get { return supidap; }
      set { supidap = value; }
    }
    
    public void ToXml(XmlNode node)
    {
      if (!RequiredAttribute.IsEmpty(ID_GLOBAL))
        Utils.AddNode(node, "ID_GLOBAL", ID_GLOBAL);
      if (!RequiredAttribute.IsEmpty(SUPPLIERID))
        Utils.AddNode(node, "SUPPLIERID", Utils.GetLong(SUPPLIERID));
      if (!RequiredAttribute.IsEmpty(SUPIDAP))
        Utils.AddNode(node, "SUPIDAP", Utils.GetLong(SUPIDAP));
    }

    public List<RowError> Errors
    {
      get { return errors; }
    }
  }
}
