using System;
using System.Collections.Generic;
using System.Text;
using ePlus.MetaData.Core;

namespace RCYConvertData.DiscountCards
{
  class DCARD : IObject, ISupportsErrorState
  {
    #region IObject Members

    private Guid fId = Guid.NewGuid();

    public Guid ID_GLOBAL
    {
      get { return fId; }
    }

    #endregion

    #region ISupportsErrorState Members

    private List<RowError> fErrors = new List<RowError>();

    public List<RowError> Errors
    {
      get { return fErrors; }
    }

    #endregion

    #region DB FIELDS
    private string fNN;
    [Format]
    public string NN
    {
      get { return fNN; }
      set { fNN = value; }
    }

    private string fSHCOD = null;
    [Required]
    [Format]
    public string SHCOD
    {
      get { return fSHCOD; }
      set { fSHCOD = value; }
    }

    private double fTIPDK = 0;
    [Required]
    [Format]
    public double TIPDK
    {
      get { return fTIPDK; }
      set { fTIPDK = value; }
    }

    private string fNAME = null;
    [Format]
    public string NAME
    {
      get { return fNAME; }
      set { fNAME = value; }
    }

    private string fFAM = null;
    [Format]
    public string FAM
    {
      get { return fFAM; }
      set { fFAM = value; }
    }

    private string fOTCH = null;
    [Format]
    public string OTCH
    {
      get { return fOTCH; }
      set { fOTCH = value; }
    }

    private string fMOBPHONE = null;
    [Format]
    public string MOBPHONE
    {
      get { return fMOBPHONE; }
      set { fMOBPHONE = value; }
    }

    private DateTime fDTBTH = DateTime.MinValue;
    [Format("dd.MM.yyyy")]
    public DateTime DTBTH
    {
      get { return fDTBTH; }
      set { fDTBTH = value; }
    }

    private string fCITY = null;
    [Format]
    public string CITY
    {
      get { return fCITY; }
      set { fCITY = value; }
    }

    private string fCITYD = null;
    [Format]
    public string CITYD
    {
      get { return fCITYD; }
      set { fCITYD = value; }
    }

    private string fSTREET = null;
    [Format]
    public string STREET
    {
      get { return fSTREET; }
      set { fSTREET = value; }
    }

    private string fHOUSE = null;
    [Format]
    public string HOUSE
    {
      get { return fHOUSE; }
      set { fHOUSE = value; }
    }

    private string fFLAT = null;
    [Format]
    public string FLAT
    {
      get { return fFLAT; }
      set { fFLAT = value; }
    }

    private string fEMAIL = null;
    [Format]
    public string EMAIL
    {
      get { return fEMAIL; }
      set { fEMAIL = value; }
    }

    private decimal fSUMMA_OPL = 0.0m;
    [Format("#0.00")]
    public decimal SUMMA_OPL
    {
      get { return fSUMMA_OPL; }
      set { fSUMMA_OPL = value; }
    }

    private string fMALE;
    public string MALE
    {
      get { return fMALE; }
      set { fMALE = value; }
    }

    private string fDISTRICT;
    public string DISTRICT
    {
      get{ return fDISTRICT;}
      set{ fDISTRICT = value;}
    }
    #endregion

    internal void ToXml(System.Xml.XmlNode node)
    {
      if (!RequiredAttribute.IsEmpty(NN))
        Utils.AddNode(node, "NN", NN);
      if (!RequiredAttribute.IsEmpty(SHCOD))
        Utils.AddNode(node, "SHCOD", SHCOD);
      if (!RequiredAttribute.IsEmpty(TIPDK))
        Utils.AddNode(node, "TIPDK", TIPDK.ToString("#0"));
      if (!RequiredAttribute.IsEmpty(NAME))
        Utils.AddNode(node, "NAME", convertString(NAME));
      if (!RequiredAttribute.IsEmpty(FAM))
        Utils.AddNode(node, "FAM", convertString(FAM));
      if (!RequiredAttribute.IsEmpty(OTCH))
        Utils.AddNode(node, "OTCH", convertString(OTCH));
      if (!RequiredAttribute.IsEmpty(MOBPHONE))
        Utils.AddNode(node, "MOBPHONE", convertString(MOBPHONE));
      if (!RequiredAttribute.IsEmpty(DTBTH))
        Utils.AddNode(node, "DTBTH", DTBTH);
      if (!RequiredAttribute.IsEmpty(CITY))
        Utils.AddNode(node, "CITY", convertString(CITY));
      if (!RequiredAttribute.IsEmpty(CITYD))
        Utils.AddNode(node, "CITYD", convertString(CITYD));
      if (!RequiredAttribute.IsEmpty(STREET))
        Utils.AddNode(node, "STREET", convertString(STREET));
      if (!RequiredAttribute.IsEmpty(HOUSE))
        Utils.AddNode(node, "HOUSE", convertString(HOUSE));
      if (!RequiredAttribute.IsEmpty(FLAT))
        Utils.AddNode(node, "FLAT", convertString(FLAT));
      if (!RequiredAttribute.IsEmpty(EMAIL))
        Utils.AddNode(node, "EMAIL", EMAIL);
      if (!RequiredAttribute.IsEmpty(SUMMA_OPL))
        Utils.AddNode(node, "SUMMA_OPL", SUMMA_OPL);
      if (!RequiredAttribute.IsEmpty(MALE))
        Utils.AddNode(node, "MALE", MALE);
      if (!RequiredAttribute.IsEmpty(DISTRICT))
        Utils.AddNode(node, "DISTRICT", DISTRICT);
    }

    private string convertString(string s)
    {
      if (string.IsNullOrEmpty(s))
        return s;
      Encoding source = Encoding.GetEncoding(1251);
      Encoding dest = Encoding.GetEncoding(866);
      return source.GetString(dest.GetBytes(s));
    }
  }
}
