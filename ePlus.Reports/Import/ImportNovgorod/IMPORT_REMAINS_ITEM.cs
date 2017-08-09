using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml;
using ePlus.MetaData.Core;

namespace ImportNovgorod
{
  class IMPORT_REMAINS_ITEM : IObject, ISupportsErrorState
  {
    private Guid id_global = Guid.NewGuid();   // для уменьшения трафика с SQL сервера    
    private string name;
    private string anl;   //код товара в льготе
    private string nnom;  //код товара комменрческий
    private string izg;   //код производителя
    private string notd;  //код отдела
    private string post;  //код поставщика товара
    private DateTime dsc;  //дата ПН
    private string nsc;   //номер ПН поставщика
    private string kon7;  //код договора
    private double ost;   // кол-во в упаковках
    private double ost2;   // кол-во для договоров, округленное в большую сторону
    private double co;    //цена производителя за упаковку
    private double nacp;  //наценка(процент) поставщика
    private string ser;   //серия товара
    private double tmt;   //срок годности серии
    private string sert;  //номер сертификата
    private string kem;   //орган сертификации
    private DateTime s;   //дата начала действия сертификата
    private DateTime po;  //дата коночания действия сертификата
    private string tam;   //ГТД
    private double p_sup; //цена поставщика за упаковку
    private double p_sup2; //цена поставщика за упаковку
    private double p_apt; //цена аптеки за упаковку
    private string barcod; // штрихкод
    private double denominator; //деноминатор,который найдем в скрипте при подборе
    private long id_goods;
    private string control_type;


    public Guid ID_GLOBAL
    {
      get { return id_global; }
      set { id_global = value; }
    }

    [Format]
    public string NAME
    {
      get { return name; }
      set { name = value; }
    }

    [Format]
    public string ANL
    {
      get { return anl; }
      set { anl = value; }
    }

    [Format]
    public string NNOM
    {
      get { return nnom; }
      set { nnom = value; }
    }

    [Format]
    public string IZG
    {
      get { return izg; }
      set { izg = value; }
    }

    [Required]
    [Format]
    public string NOTD
    {
      get { return notd; }
      set { notd = value; }
    }

    [Required]
    [Format]
    public string POST
    {
      get { return post; }
      set { post = value; }
    }

    [Format]
    public DateTime DSC
    {
      get { return dsc; }
      set { dsc = value; }
    }

    [Format]
    public string NSC
    {
      get { return nsc; }
      set { nsc = value; }
    }

    [Format]
    public string KON7
    {
      get { return kon7; }
      set { kon7 = value; }
    }

    [Required]
    [Format("#0.000")]
    public double OST
    {
      get { return ost; }
      set { ost = value; }
    }

    public double OST2
    {
      get { return ost2; }
      set { ost2 = value; }
    }

    [Required]
    [Format("#0.000")]
    public double CO
    {
      get { return co; }
      set { co = value; }
    }

    [Format("#0.00")]
    public double NACP
    {
      get { return nacp; }
      set { nacp = value; }
    }

    [Format]
    public string SER
    {
      get { return ser; }
      set { ser = value; }
    }

    [Format]
    public double TMT
    {
      get { return tmt; }
      set { tmt = value; }
    }

    [Format]
    public string SERT
    {
      get { return sert; }
      set { sert = value; }
    }

    [Format]
    public string KEM
    {
      get { return kem; }
      set { kem = value; }
    }

    [Format]
    public DateTime S
    {
      get { return s; }
      set { s = value; }
    }

    [Format]
    public DateTime PO
    {
      get { return po; }
      set { po = value; }
    }

    [Format]
    public string TAM
    {
      get { return tam; }
      set { tam = value; }
    }

    [Format("#0.00")]
    public double P_SUP
    {
      get { return p_sup; }
      set { p_sup = value; }
    }

    public double P_SUP2
    {
      get { return p_sup2; }
      set { p_sup2 = value; }
    }
    
    [Format("#0.00")]
    public double P_APT
    {
      get { return p_apt; }
      set { p_apt = value; }
    }

    [Format]
    public string BARCOD
    {
      get { return barcod; }
      set { barcod = value; }
    }

    public double DENOMINATOR
    {
      get { return denominator; }
      set { denominator = value; }
    }

    public long ID_GOODS
    {
      get { return id_goods; }
      set { id_goods = value; }
    }

    public string CONTROL_TYPE
    {
      get { return control_type; }
      set { control_type = value; }
    }

    private double nds_sup;
    [Format("#0.00")]
    public double NDS_SUP
    {
      get { return nds_sup; }
      set { nds_sup = value; }
    }

    private double nds_apt;
    [Format("#0.00")]
    public double NDS_APT
    {
      get { return nds_apt; }
      set { nds_apt = value; }
    }

    public void ToXml(XmlNode node)
    {
      if (!RequiredAttribute.IsEmpty(ID_GLOBAL))
        Utils.AddNode(node, "ID_GLOBAL", ID_GLOBAL);   // для уменьшения трафика с SQL сервера
      if (!RequiredAttribute.IsEmpty(NAME))
        Utils.AddNode(node, "NAME", NAME);
      if (!RequiredAttribute.IsEmpty(ANL))
        Utils.AddNode(node, "ANL", Utils.GetString(ANL));
      if (!RequiredAttribute.IsEmpty(NNOM))
        Utils.AddNode(node, "NNOM", Utils.GetString(NNOM));
      if (!RequiredAttribute.IsEmpty(IZG))
        Utils.AddNode(node, "IZG", Utils.GetString(IZG));
      if (!RequiredAttribute.IsEmpty(NOTD))
        Utils.AddNode(node, "NOTD", Utils.GetString(NOTD));
      if (!RequiredAttribute.IsEmpty(POST))
        Utils.AddNode(node, "POST", Utils.GetString(POST));
      if (!RequiredAttribute.IsEmpty(DSC))
        Utils.AddNode(node, "DSC", Utils.GetDate(DSC));
      if (!RequiredAttribute.IsEmpty(NSC))
        Utils.AddNode(node, "NSC", Utils.GetString(NSC));
      if (!RequiredAttribute.IsEmpty(KON7))
        Utils.AddNode(node, "KON7", Utils.GetString(KON7));
      if (!RequiredAttribute.IsEmpty(OST))
        Utils.AddNode(node, "OST", Utils.GetDecimal(OST));
      if (!RequiredAttribute.IsEmpty(OST2))
        Utils.AddNode(node, "OST2", Utils.GetDecimal(OST2));
      if (!RequiredAttribute.IsEmpty(CO))
        Utils.AddNode(node, "CO", Utils.GetDecimal(CO));
      if (!RequiredAttribute.IsEmpty(NACP))
        Utils.AddNode(node, "NACP", Utils.GetDecimal(NACP));
      if (!RequiredAttribute.IsEmpty(SER))
        Utils.AddNode(node, "SER", Utils.GetString(SER));
      if (!RequiredAttribute.IsEmpty(TMT))
        Utils.AddNode(node, "TMT", Utils.GetDecimal(TMT));
      if (!RequiredAttribute.IsEmpty(SERT))
        Utils.AddNode(node, "SERT", Utils.GetString(SERT));
      if (!RequiredAttribute.IsEmpty(KEM))
        Utils.AddNode(node, "KEM", Utils.GetString(KEM));
      if (!RequiredAttribute.IsEmpty(S))
        Utils.AddNode(node, "S", Utils.GetDate(S));
      if (!RequiredAttribute.IsEmpty(PO))
        Utils.AddNode(node, "PO", Utils.GetDate(PO));
      if (!RequiredAttribute.IsEmpty(TAM))
        Utils.AddNode(node, "TAM", Utils.GetString(TAM));
      if (!RequiredAttribute.IsEmpty(P_SUP))
        Utils.AddNode(node, "P_SUP", Utils.GetDecimal(P_SUP));
      if (!RequiredAttribute.IsEmpty(P_SUP2))
        Utils.AddNode(node, "P_SUP2", Utils.GetDecimal(P_SUP2));
      if (!RequiredAttribute.IsEmpty(P_APT))
        Utils.AddNode(node, "P_APT", Utils.GetDecimal(P_APT));
      if (!RequiredAttribute.IsEmpty(BARCOD))
        Utils.AddNode(node, "BARCOD", Utils.GetDecimal(BARCOD));

      Utils.AddNode(node, "NDS_SUP", Utils.GetDecimal(NDS_SUP));
      Utils.AddNode(node, "NDS_APT", Utils.GetDecimal(NDS_APT));
      
      Utils.AddNode(node, "DENOMINATOR", Utils.GetDecimal(DENOMINATOR));
      Utils.AddNode(node, "ID_GOODS", ID_GOODS);
      Utils.AddNode(node, "CONTROL_TYPE", CONTROL_TYPE);
    }

    public IMPORT_REMAINS_ITEM Copy()
    {
      IMPORT_REMAINS_ITEM item = new IMPORT_REMAINS_ITEM();
      item.name = name;
      item.anl = anl;   //код товара в льготе
      item.nnom = nnom;
      item.izg = izg;
      item.notd = notd;
      item.post = post;
      item.dsc = dsc;
      item.nsc = nsc;
      item.kon7 = kon7;
      item.ost = ost;
      item.ost2 = ost2;
      item.co = co;
      item.nacp = nacp;
      item.ser = ser;
      item.tmt = tmt;
      item.sert = sert;
      item.kem = kem;
      item.s = s;
      item.po = po;
      item.tam = tam;
      item.p_sup = p_sup;
      item.p_sup2 = p_sup2;
      item.p_apt = p_apt;
      item.denominator = denominator;
      item.id_goods = id_goods;
      item.nds_sup = nds_sup;
      item.nds_apt = nds_apt;

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
}
