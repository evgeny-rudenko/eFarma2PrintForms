using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ePlus.MetaData.Core;

namespace RCKABC_analysis
{
	public enum RequestParameterType { Runtime, Db }

	public class RequestParamCategory
	{
		string name;
		IRequestParamMethods control;
		IRequestParam[] paramerets;

		public IRequestParam[] Paramerets
		{
			get { return paramerets; }
		}

		public string Name
		{
			get { return name; }
		}

		public IRequestParamMethods Control
		{
			get { return control; }
		}

		public RequestParamCategory(string name, IRequestParamMethods control, params IRequestParam[] parameters)
		{
			this.name = name;
			this.control = control;
			this.paramerets = parameters;
		}
	}

	public interface IRequestParam
	{
		void ToXml(XmlNode root);
		void FromXml(XmlNode root);

		string Name
		{
			get;
		}

		string ClassName
		{
			get;
		}

		RequestParameterType ParamType
		{
			get;
		}
	}

	public interface IRequestParamMethods
	{
		void Object2Control(params IRequestParam[] param);
		void Control2Object(params IRequestParam[] param);

		void ClearData();
		Control Control
		{
			get;
		}
		Type[] SupportsParams
		{
			get;
		}

		string Category
		{
			get;
		}
	}

	//public interface ICalculatableParam
	//{
	//  void Calculate(REQUEST_CALCULATION request, out string errorMessage);
	//}

	public class DaysPeriodParam : IRequestParam
	{
		int days_period;
		public int Days_period
		{
			get { return days_period; }
			set { days_period = value; }
		}

		public void ToXml(XmlNode root)
		{
			Utils.AddNode(root, "DAYS_PERIOD", days_period);
		}
		public void FromXml(XmlNode root)
		{
			int val = Utils.GetInt(root, "DAYS_PERIOD");
			days_period = val == 0 ? 31 : val;
		}
		public string Name
		{
			get { return "Количество дней для анализа данных"; }
		}
		public string ClassName
		{
			get { return "DaysPeriodParam"; }
		}
		public RequestParameterType ParamType
		{
			get { return RequestParameterType.Db; }
		}
		public override string ToString()
		{
			return string.Format("{0}: {1}", Name, days_period);
		}
	}

	public class StoreParam : IRequestParam
	{
		List<long> stores = new List<long>();
		System.Collections.Generic.Dictionary<long, string> storeDesc = new System.Collections.Generic.Dictionary<long, string>();

		public System.Collections.Generic.Dictionary<long, string> StoreDesc
		{
			get { return storeDesc; }
		}

		public List<long> Stores
		{
			get { return stores; }
		}

		public void ToXml(XmlNode root)
		{
			foreach (long value in stores)
			{
				Utils.AddNode(root, "ID_STORE", value);
			}
		}

		public void FromXml(XmlNode root)
		{
			stores.Clear();
			XmlNodeList listNodes = root.SelectNodes("ID_STORE");
			foreach (XmlNode node in listNodes)
			{
				stores.Add(Utils.GetLong(node, "."));
			}
		}

		public string Name
		{
			get { return "Склад(ы) для анализа данных"; }
		}

		public string ClassName
		{
			get { return "StoreParam"; }
		}

		public RequestParameterType ParamType
		{
			get { return RequestParameterType.Db; }
		}

		public override string ToString()
		{
			string storeStr = string.Empty;
			foreach (string s in storeDesc.Values)
			{
				if (storeStr == string.Empty)
					storeStr = s;
				else
					storeStr = string.Format("{0}, {1}", storeStr, s);
			}
			if (string.IsNullOrEmpty(storeStr))
				storeStr = "Все";
			return string.Format("{0}: {1}", Name, storeStr);
		}
	}
	public class SubOpTypeParam : IRequestParam
	{
		List<string> ops = new List<string>();

		public List<string> Ops
		{
			get { return ops; }
		}

		public void ToXml(XmlNode root)
		{
			foreach (string value in ops)
			{
				Utils.AddNode(root, "SUB_OP_TYPE", value);
			}
		}

		public void FromXml(XmlNode root)
		{
			ops.Clear();
			XmlNodeList listNodes = root.SelectNodes("SUB_OP_TYPE");
			foreach (XmlNode node in listNodes)
			{
				ops.Add(Utils.GetString(node, "."));
			}

		}

		public string Name
		{
			get { return "Вид(ы) расхода"; }
		}

		public string ClassName
		{
			get { return "SubOpTypeParam"; }
		}

		public RequestParameterType ParamType
		{
			get { return RequestParameterType.Db; }
		}

		public override string ToString()
		{
			string str = string.Empty;
			foreach (string s in ops)
			{
				if (string.IsNullOrEmpty(str))
					str = s == "INVOICE_OUT" ? "Расходные накладные" : s == "CHEQUE" ? "Чеки" : string.Empty;
				else
					str = string.Format("{0}, {1}", str, s == "INVOICE_OUT" ? "Расходные накладные" : s == "CHEQUE" ? "Чеки" : string.Empty);
			}
			return string.Format("{0}: {1}", Name, str);
		}
	}

	public class GoodsKindParam : IRequestParam
	{
		System.Collections.Generic.Dictionary<long, string> goodsKindDesc = new System.Collections.Generic.Dictionary<long, string>();

		public System.Collections.Generic.Dictionary<long, string> GoodsKindDesc
		{
			get { return goodsKindDesc; }
		}

		List<long> goodsKinds = new List<long>();

		public List<long> GoodsKinds
		{
			get { return goodsKinds; }
		}

		public void ToXml(XmlNode root)
		{
			foreach (long l in GoodsKinds)
			{
				Utils.AddNode(root, "ID_GOODS_KIND", l);
			}
		}

		public void FromXml(XmlNode root)
		{
			GoodsKinds.Clear();
			XmlNodeList listNodes = root.SelectNodes("ID_GOODS_KIND");
			foreach (XmlNode node in listNodes)
			{
				GoodsKinds.Add(Utils.GetLong(node, "."));
			}
		}

		public string Name
		{
			get { return "Виды товара"; }
		}

		public string ClassName
		{
			get { return "GoodsKindParam"; }
		}

		public RequestParameterType ParamType
		{
			get { return RequestParameterType.Db; }
		}

		public override string ToString()
		{
			string str = string.Empty;
			foreach (string s in goodsKindDesc.Values)
			{
				if (str == string.Empty)
					str = s;
				else
					str = string.Format("{0}, {1}", str, s);
			}
			if (string.IsNullOrEmpty(str))
				str = "Все";
			return string.Format("{0}: {1}", Name, str);
		}

		public string GetFilterString()
		{
			string str = string.Empty;
			foreach (long l in goodsKinds)
			{
				if (str == string.Empty)
					str = string.Format("{0:n0}", l);
				else
					str = string.Format("{0}, {1}", str, string.Format("{0:n0}", l));
			}
			return str;
		}
	}


	public class GoodsClassifierParam : IRequestParam//, ICalculatableParam
	{
		private bool group;

		public bool Group
		{
			get { return group; }
			set { group = value; }
		}
		public GoodsClassifierParam()
		{
			group = true;
		}
		public void ToXml(XmlNode root)
		{
			Utils.AddNode(root, "GROUP", group);
		}

		public void FromXml(XmlNode root)
		{
			group = Utils.GetBool(root, "GROUP");
		}

		public string Name
		{
			get { return "Группы товаров"; }
		}

		public string ClassName
		{
			get { return "GoodsClassifierParam"; }
		}

		public RequestParameterType ParamType
		{
			get { return RequestParameterType.Runtime; }
		}

		//public void Calculate(REQUEST_CALCULATION request, out string errorMessage)
		//{
		//  List<REQUEST_CALCULATION_ITEM> items;
		//  List<REQUEST_CALCULATION_ITEM> groupedItems;
		//  request.GetInternalLists(out items, out groupedItems);
		//  request.CurrentList = group ? groupedItems : items;        
		//  errorMessage = string.Empty;
		//}

		public override string ToString()
		{
			return string.Format("{0}: {1}", "Сворачивать товары по группам", group ? "да" : "нет");
		}
	}

	public enum ABCGroupCalcType { Qty, Sum, Profit }
	public class ABCGroupCalcTypeDescription : IEquatable<ABCGroupCalcTypeDescription>, IEquatable<ABCGroupCalcType>
	{
		string description;
		ABCGroupCalcType type;

		public string Desscription
		{
			get { return description; }
		}

		public ABCGroupCalcType Type
		{
			get { return type; }
		}

		public ABCGroupCalcTypeDescription(ABCGroupCalcType type)
		{
			this.type = type;
			switch (type)
			{
				case ABCGroupCalcType.Sum:
					description = "от суммы продаж";
					break;
				case ABCGroupCalcType.Profit:
					description = "от маржинальной прибыли";
					break;
				case ABCGroupCalcType.Qty:
					description = "от количества продаж";
					break;
			}
		}

		public override string ToString()
		{
			return description;
		}

		public override bool Equals(object obj)
		{
			if (obj is ABCGroupCalcTypeDescription)
			{
				return ((ABCGroupCalcTypeDescription)obj).type == this.type;
			}
			return base.Equals(obj);
		}

		public bool Equals(ABCGroupCalcTypeDescription other)
		{
			return this.type == other.type;
		}

		public bool Equals(ABCGroupCalcType other)
		{
			return this.type == other;
		}
	}
	public class AbcGroupParam : IRequestParam//, ICalculatableParam
	{
		decimal aPercent;
		decimal bPercent;
		ABCGroupCalcType calcType = ABCGroupCalcType.Sum;

		public decimal APercent
		{
			get { return aPercent; }
			set { aPercent = value; }
		}

		public decimal BPercent
		{
			get { return bPercent; }
			set { bPercent = value; }
		}

		public ABCGroupCalcType CalcType
		{
			get { return calcType; }
			set { calcType = value; }
		}
		public AbcGroupParam()
		{
			aPercent = 75;
			bPercent = 20;
		}

		public void ToXml(XmlNode root)
		{
			Utils.AddNode(root, "APercent", aPercent);
			Utils.AddNode(root, "BPercent", bPercent);
			Utils.AddNode(root, "CalcType", calcType.ToString());
		}

		public void FromXml(XmlNode root)
		{
			aPercent = Utils.GetDecimal(root, "APercent");
			bPercent = Utils.GetDecimal(root, "BPercent");
			string calcTypeStr = Utils.GetString(root, "CalcType");
			string[] names = Enum.GetNames(typeof(ABCGroupCalcType));
			bool exists = false;
			foreach (string name in names)
			{
				if (name == calcTypeStr)
				{
					this.calcType = (ABCGroupCalcType)Enum.Parse(typeof(ABCGroupCalcType), name);
					exists = true;
					break;
				}
			}
			if (!exists)
				calcType = ABCGroupCalcType.Sum;
		}

		public string Name
		{
			get { return "Группы ABC"; }
		}

		public string ClassName
		{
			get { return "AbcGroupParam"; }
		}

		public RequestParameterType ParamType
		{
			get { return RequestParameterType.Runtime; }
		}

		//public void Calculate(REQUEST_CALCULATION request, out string errorMessage)
		//{
		//  RequestItemsSorter sorter = new RequestItemsSorter();
		//  switch (calcType)
		//  {
		//    case ABCGroupCalcType.Sum:
		//      sorter.SortType = RequestItemsSortType.retail_sum;
		//      sorter.Direction = ListSortDirection.Descending;
		//      break;
		//    case ABCGroupCalcType.Profit:
		//      sorter.SortType = RequestItemsSortType.profit;
		//      sorter.Direction = ListSortDirection.Descending;
		//      break;
		//    case ABCGroupCalcType.Qty:
		//      sorter.SortType = RequestItemsSortType.qty_sub;
		//      sorter.Direction = ListSortDirection.Descending;
		//      break;
		//  }

		//  List<REQUEST_CALCULATION_ITEM> items;
		//  List<REQUEST_CALCULATION_ITEM> groupedItems;
		//  if (!request.GetInternalLists(out items, out groupedItems))
		//  {
		//    CalculateList(request.CurrentList, sorter);
		//  }
		//  else
		//  {
		//    CalculateList(items, sorter);
		//    CalculateList(groupedItems, sorter);
		//  }
		//  errorMessage = string.Empty;
		//}

		//private void CalculateList(List<REQUEST_CALCULATION_ITEM> items, RequestItemsSorter sorter)
		//{
		//  sorter.SaveOrder(items);
		//  items.Sort(sorter);
		//  int aGroupFrom = 0;
		//  int aGroupTo = aGroupFrom + (int)Math.Ceiling(items.Count * (aPercent / 100));
		//  int bGroupFrom = aGroupTo;
		//  int bGroupTo = bGroupFrom + (int)Math.Ceiling(items.Count * (bPercent / 100));
		//  for (int i = 0; i < items.Count; i++)
		//  {
		//    if (i >= aGroupFrom && i < aGroupTo)
		//      items[i].ABCCATEGORY = "A";
		//    else if (i >= bGroupFrom && i < bGroupTo)
		//      items[i].ABCCATEGORY = "B";
		//    else
		//      items[i].ABCCATEGORY = "С";
		//  }
		//  sorter.RestoreOrder(items);
		//}

		public override string ToString()
		{
			string s = new ABCGroupCalcTypeDescription(calcType).Desscription;
			return string.Format("{0}\r\n\t{1}",
								 string.Format("Процент {0} для группы А: {1}", s, aPercent),
								 string.Format("Процент {0} для группы B: {1}", s, bPercent));
		}
	}

	public enum SalesSpeedCalcType { Avg, SmoothPeaks, AvgAndSmoothPeaks, Mediana }
	public class SalesSpeedCalcTypeDescription : IEquatable<SalesSpeedCalcType>, IEquatable<SalesSpeedCalcTypeDescription>
	{
		string description;
		SalesSpeedCalcType type;

		public string Desscription
		{
			get { return description; }
		}

		public SalesSpeedCalcType Type
		{
			get { return type; }
		}

		public SalesSpeedCalcTypeDescription(SalesSpeedCalcType type)
		{
			this.type = type;
			switch (type)
			{
				case SalesSpeedCalcType.Avg:
					description = "расчет среднего арифметического количества продаж";
					break;
				case SalesSpeedCalcType.SmoothPeaks:
					description = "сглаженное количество продаж / количество дней продаж";
					break;
				case SalesSpeedCalcType.AvgAndSmoothPeaks:
					description = "((сглаженное количество продаж / количество дней продаж) + среднее арифметичское) / 2";
					break;
				case SalesSpeedCalcType.Mediana:
					description = "Расчет медианы от количества продаж";
					break;
			}
		}

		public override string ToString()
		{
			return description;
		}

		public override bool Equals(object obj)
		{
			if (obj is SalesSpeedCalcTypeDescription)
			{
				return ((SalesSpeedCalcTypeDescription)obj).type == this.type;
			}
			return base.Equals(obj);
		}

		public bool Equals(SalesSpeedCalcTypeDescription other)
		{
			return this.type == other.type;
		}

		public bool Equals(SalesSpeedCalcType other)
		{
			return this.type == other;
		}
	}
	public class SalesSpeedCalcParam : IRequestParam//, ICalculatableParam
	{
		SalesSpeedCalcType calcType;
		decimal smoothCoeff;
		int statDaysCount;

		public SalesSpeedCalcType CalcType
		{
			get { return calcType; }
			set { calcType = value; }
		}

		public decimal SmoothCoeff
		{
			get { return smoothCoeff; }
			set { smoothCoeff = value; }
		}

		public int StatDaysCount
		{
			get { return statDaysCount; }
			set { statDaysCount = value; }
		}

		public void ToXml(XmlNode root)
		{
			Utils.AddNode(root, "CALC_TYPE", calcType.ToString());
			Utils.AddNode(root, "SMOOTH_COEFF", smoothCoeff);
			Utils.AddNode(root, "STAT_DAYS_COUNT", statDaysCount);
		}

		public void FromXml(XmlNode root)
		{
			smoothCoeff = Utils.GetDecimal(root, "SMOOTH_COEFF");
			statDaysCount = Utils.GetInt(root, "STAT_DAYS_COUNT");

			string calcTypeStr = Utils.GetString(root, "CALC_TYPE");
			string[] names = Enum.GetNames(typeof(SalesSpeedCalcType));
			bool exists = false;
			foreach (string name in names)
			{
				if (name == calcTypeStr)
				{
					this.calcType = (SalesSpeedCalcType)Enum.Parse(typeof(SalesSpeedCalcType), name);
					exists = true;
					break;
				}
			}
			if (!exists)
				calcType = SalesSpeedCalcType.Avg;
		}

		public string Name
		{
			get { return "Скорость продаж"; }
		}

		public string ClassName
		{
			get { return "SalesSpeedCalcParam"; }
		}

		public RequestParameterType ParamType
		{
			get { return RequestParameterType.Runtime; }
		}

		//public void Calculate(REQUEST_CALCULATION request, out string errorMessage)
		//{
		//  List<REQUEST_CALCULATION_ITEM> items;
		//  List<REQUEST_CALCULATION_ITEM> groupedItems;
		//  request.GetInternalLists(out items, out groupedItems);
		//  CalculateList(items);
		//  CalculateList(groupedItems);
		//  errorMessage = string.Empty;
		//}

		//private void CalculateList(List<REQUEST_CALCULATION_ITEM> items)
		//{
		//  System.Collections.Generic.Dictionary<Guid, decimal> calculatedValuesAvg = null;
		//  System.Collections.Generic.Dictionary<Guid, decimal> calculatedValuesSmoothPeaks = null;

		//  System.Collections.Generic.Dictionary<Guid, decimal> calculatedValues = null;

		//  if (calcType == SalesSpeedCalcType.Avg || calcType == SalesSpeedCalcType.AvgAndSmoothPeaks)
		//  {
		//    calculatedValuesAvg = CalcAvg(items);
		//  }
		//  if (calcType == SalesSpeedCalcType.SmoothPeaks || calcType == SalesSpeedCalcType.AvgAndSmoothPeaks)
		//  {
		//    calculatedValuesSmoothPeaks = CalcSmoothPeaks(items);
		//  }
		//  if (calcType==SalesSpeedCalcType.Mediana)
		//  {
		//    calculatedValues = CalcMediana(items);
		//  }
		//  if (calcType == SalesSpeedCalcType.AvgAndSmoothPeaks)
		//  {
		//    calculatedValues = new System.Collections.Generic.Dictionary<Guid, decimal>();
		//    foreach (KeyValuePair<Guid, decimal> kvp in calculatedValuesSmoothPeaks)
		//    {
		//      if (kvp.Value > 0 && calculatedValuesAvg[kvp.Key]>0)          
		//        calculatedValues.Add(kvp.Key, (kvp.Value+calculatedValuesAvg[kvp.Key])/2);
		//      else
		//        calculatedValues.Add(kvp.Key, -1);
		//    }
		//  }
		//  else
		//  {
		//    if (calcType == SalesSpeedCalcType.Avg)
		//    {
		//      calculatedValues = calculatedValuesAvg;
		//    }
		//    if (calcType == SalesSpeedCalcType.SmoothPeaks)
		//    {
		//      calculatedValues = calculatedValuesSmoothPeaks;          
		//    }
		//  }
		//  ApplyValues(items, calculatedValues);
		//}

		//private System.Collections.Generic.Dictionary<Guid, decimal> CalcAvg(List<REQUEST_CALCULATION_ITEM> items)
		//{
		//  System.Collections.Generic.Dictionary<Guid, decimal> calculatedValues = new System.Collections.Generic.Dictionary<Guid, decimal>();
		//  foreach (REQUEST_CALCULATION_ITEM item in items)
		//  {
		//    decimal qty = 0;
		//    int count = 0;
		//    foreach (NEED_GOODS_GRAPH ngg in item.Graph)
		//    {
		//      if (ngg.REMAIN>0 || ngg.QTY>0)
		//      {
		//        count++;
		//        qty += ngg.QTY;
		//      }
		//    }
		//    calculatedValues.Add(item.ID_GOODS, count > 0 && count >= statDaysCount?qty / count:-1);
		//  }
		//  return calculatedValues;
		//}

		//private System.Collections.Generic.Dictionary<Guid, decimal> CalcSmoothPeaks(List<REQUEST_CALCULATION_ITEM> items)
		//{
		//  System.Collections.Generic.Dictionary<Guid, decimal> calculatedValues = new System.Collections.Generic.Dictionary<Guid, decimal>();
		//  foreach (REQUEST_CALCULATION_ITEM item in items)
		//  {
		//    item.Graph.Sort(NeedsGoodsGraphComparator.CompareByQtyDesc);
		//    decimal qty=0;
		//    int count=0;
		//    for (int i=0;i<item.Graph.Count;i++)
		//    {
		//      NEED_GOODS_GRAPH ngg = item.Graph[i];
		//      if (ngg.QTY >0)
		//      {
		//        count++;
		//        if (i<=(item.Graph.Count *smoothCoeff))
		//          qty+=ngg.QTY*(1-smoothCoeff);
		//        else
		//          qty+=ngg.QTY;
		//      }
		//    }
		//    calculatedValues.Add(item.ID_GOODS, count >0 && count >= statDaysCount ? qty / count : -1);
		//  }
		//  return calculatedValues;
		//}

		//private System.Collections.Generic.Dictionary<Guid, decimal> CalcMediana(List<REQUEST_CALCULATION_ITEM> items)
		//{
		//  System.Collections.Generic.Dictionary<Guid, decimal> calculatedValues = new System.Collections.Generic.Dictionary<Guid, decimal>();
		//  foreach (REQUEST_CALCULATION_ITEM item in items)
		//  {
		//    item.Graph.Sort(NeedsGoodsGraphComparator.CompareByQtyDesc);
		//    int counter = 0;
		//    decimal qty =-1;
		//    for (int i = 0; i < item.Graph.Count; i++)
		//    {
		//      NEED_GOODS_GRAPH ngg = item.Graph[i];
		//      if (ngg.QTY>0)
		//      {
		//        counter++;
		//      }
		//    }
		//    if (counter > 0 && counter>=statDaysCount)
		//    {
		//      qty = (item.Graph.Count % 2)==1?
		//            item.Graph[(item.Graph.Count+1)/2].QTY:
		//            (item.Graph[item.Graph.Count / 2].QTY+item.Graph[(item.Graph.Count/2)+1].QTY)/2;
		//    }
		//    calculatedValues.Add(item.ID_GOODS, qty);
		//  }
		//  return calculatedValues;
		//}

		//private void ApplyValues(List<REQUEST_CALCULATION_ITEM> items, System.Collections.Generic.Dictionary<Guid, decimal> values)
		//{
		//  foreach (REQUEST_CALCULATION_ITEM item in items)
		//  {
		//    if (item.Graph.Count == 0) continue;
		//    decimal sales_speed = values[item.ID_GOODS];
		//    if (sales_speed > 0)
		//      item.SALES_SPEED = sales_speed;
		//    else
		//      item.SALES_SPEED = 0;          
		//  }
		//}

		public override string ToString()
		{
			return string.Format("{0}\r\n\t{1}\r\n\t{2}",
								 string.Format("Способ расчета: {0}", new SalesSpeedCalcTypeDescription(this.calcType).Desscription),
								 string.Format("Коэфффициент сглаживания пиков продаж: {0}", smoothCoeff),
								 string.Format("Минимальное количество дней продаж для успешного анализа: {0}", statDaysCount));
		}
	}

	public enum NeedsCalcType { NoRemain, MinQtyRemain }
	public class NeedsCalcTypeDescription : IEquatable<NeedsCalcType>, IEquatable<NeedsCalcTypeDescription>
	{
		string description;
		NeedsCalcType type;

		public string Desscription
		{
			get { return description; }
		}

		public NeedsCalcType Type
		{
			get { return type; }
		}

		public NeedsCalcTypeDescription(NeedsCalcType type)
		{
			this.type = type;
			switch (type)
			{
				case NeedsCalcType.NoRemain:
					description = "товар продается весь";
					break;
				case NeedsCalcType.MinQtyRemain:
					description = "товар остается в минимальном количестве";
					break;
			}
		}

		public override string ToString()
		{
			return description;
		}

		public override bool Equals(object obj)
		{
			if (obj is NeedsCalcTypeDescription)
			{
				return ((NeedsCalcTypeDescription)obj).type == this.type;
			}
			return base.Equals(obj);
		}

		public bool Equals(NeedsCalcTypeDescription other)
		{
			return this.type == other.type;
		}

		public bool Equals(NeedsCalcType other)
		{
			return this.type == other;
		}
	}
	public class NeedsCalcParam : IRequestParam//, ICalculatableParam
	{
		NeedsCalcType calcType;
		int daysNeeds;
		int daysOnWay;

		public NeedsCalcType CalcType
		{
			get { return calcType; }
			set { calcType = value; }
		}

		public int DaysNeeds
		{
			get { return daysNeeds; }
			set { daysNeeds = value; }
		}

		public int DaysOnWay
		{
			get { return daysOnWay; }
			set { daysOnWay = value; }
		}

		public void ToXml(XmlNode node)
		{
			Utils.AddNode(node, "calcType", calcType.ToString());
			Utils.AddNode(node, "daysNeeds", daysNeeds);
			Utils.AddNode(node, "daysOnWay", daysOnWay);
		}

		public void FromXml(XmlNode node)
		{
			daysNeeds = Utils.GetInt(node, "daysNeeds");
			daysOnWay = Utils.GetInt(node, "daysOnWay");
			string calcTypeStr = Utils.GetString(node, "calcType");
			string[] names = Enum.GetNames(typeof(NeedsCalcType));
			bool exists = false;
			foreach (string name in names)
			{
				if (name == calcTypeStr)
				{
					this.calcType = (NeedsCalcType)Enum.Parse(typeof(NeedsCalcType), name);
					exists = true;
					break;
				}
			}
			if (!exists)
				calcType = NeedsCalcType.NoRemain;
		}

		public string Name
		{
			get { return "Расчет потребности"; }
		}

		public string ClassName
		{
			get { return "NeedsCalcParam"; }
		}

		public RequestParameterType ParamType
		{
			get { return RequestParameterType.Runtime; }
		}

		//public void Calculate(REQUEST_CALCULATION request, out string errorMessage)
		//{
		//  List<REQUEST_CALCULATION_ITEM> items;
		//  List<REQUEST_CALCULATION_ITEM> groupedItems;
		//  if (!request.GetInternalLists(out items, out groupedItems))
		//  {
		//    CalculateList(request.CurrentList);
		//  }
		//  else
		//  {
		//    CalculateList(items);
		//    CalculateList(groupedItems);
		//  }
		//  errorMessage = string.Empty;
		//}

		//    private void CalculateList(List<REQUEST_CALCULATION_ITEM> list)
		//    {
		//      switch (calcType)
		//      {
		//        case NeedsCalcType.MinQtyRemain:
		//          foreach (REQUEST_CALCULATION_ITEM item in list)
		//          {
		//            if (item.FIXED) continue;
		//            decimal ost = item.REMAIN - (item.SALES_SPEED * (daysOnWay + daysNeeds));
		//            decimal usl = item.MIN_QTY - ((item.REMAIN - daysOnWay * item.SALES_SPEED) > 0 ? item.REMAIN - daysOnWay * item.SALES_SPEED : 0) + daysNeeds * item.SALES_SPEED;
		//            item.NEEDS = ost < item.MIN_QTY ? usl : 0;

		//            item.NEEDS = Math.Ceiling(item.NEEDS);
		//          }
		//          break;
		//        case NeedsCalcType.NoRemain:

		////   update a set
		////        nado = case when (remain - yxod*@days_on_way)<0 then potreb
		////                    else case when potreb-(remain - yxod*@days_on_way)<0 then 0
		////                              else potreb-(remain - yxod*@days_on_way) 
		////                              end
		////                    end
		////    from #all a
		////    inner join goods g on g.id_goods = a.id_goods
		////    where isnull(g.quantity_min,0)=0    
		////    and a.yxod is not null
		////
		////    update a set
		////        nado = case when (remain-(yxod* @days_on_way))<0 then case when g.quantity_min>potreb then g.quantity_min
		////                                                                   else potreb 
		////                                                                   end
		////                    else case when g.quantity_min<(remain-yxod*@days_on_way) then 0
		////                              else g.quantity_min-(remain-yxod*@days_on_way)
		////                              end 
		////                    end                   
		////    from #all a
		////    inner join goods g on g.id_goods = a.id_goods
		////    where g.quantity_min > 0
		////    and a.yxod is not null
		////
		//        foreach (REQUEST_CALCULATION_ITEM item in list)
		//        {
		//          if (item.FIXED) continue;
		//          if (item.MIN_QTY==0 && item.SALES_SPEED>0)
		//          {
		//            item.NEEDS = (item.REMAIN - (item.SALES_SPEED * daysOnWay)<0)?
		//                         item.SALES_SPEED*daysNeeds:
		//                           ((item.SALES_SPEED * daysNeeds) - (item.REMAIN - (item.SALES_SPEED * daysOnWay))) < 0 ? 
		//                           0: 
		//                           (item.SALES_SPEED * daysNeeds) - (item.REMAIN - (item.SALES_SPEED * daysOnWay));
		//          }
		//          else if (item.MIN_QTY>0 && item.SALES_SPEED>0)
		//          {
		//            item.NEEDS = item.REMAIN - (item.SALES_SPEED * daysOnWay) < 0 ?
		//                         item.MIN_QTY > item.SALES_SPEED * daysNeeds ?
		//                           item.MIN_QTY :
		//                           item.SALES_SPEED * daysNeeds :
		//                         item.MIN_QTY > item.SALES_SPEED * daysNeeds - (item.REMAIN - (item.SALES_SPEED * daysOnWay)) ?
		//                         item.MIN_QTY - (item.SALES_SPEED * daysNeeds - (item.REMAIN - (item.SALES_SPEED * daysOnWay))) :
		//                         item.SALES_SPEED * daysNeeds - (item.REMAIN - (item.SALES_SPEED * daysOnWay));

		//          }
		//          else if (item.SALES_SPEED==0)
		//          {
		//            item.NEEDS = item.MIN_QTY>item.REMAIN?item.MIN_QTY - item.REMAIN:0;
		//          }
		//          item.NEEDS = Math.Ceiling(item.NEEDS);
		//        }
		//        break;
		//      }
		//    }

		public override string ToString()
		{
			return string.Format("{0}\r\n\t{1}\r\n\t{2}",
								 string.Format("Способ расчета: {0}", new NeedsCalcTypeDescription(calcType).Desscription),
								 string.Format("Количество дней потребности: {0}", daysNeeds),
								 string.Format("Количество дней пока товар в пути: {0}", daysOnWay));

		}
	}

	public class GoodsParam : IRequestParam
	{
		List<long> goods;

		public List<long> Goods
		{
			get { return goods; }
			set { goods = value; }
		}

		public void ToXml(XmlNode root)
		{
			if (goods != null && goods.Count > 0)
			{
				foreach (long l in goods)
				{
					if (l != 0)
						Utils.AddNode(root, "GOODS", l);
				}
			}
		}

		public void FromXml(XmlNode root)
		{
			return;
		}

		public string Name
		{
			get { return "Товар"; }
		}

		public string ClassName
		{
			get { return "GoodsParam"; }
		}

		public RequestParameterType ParamType
		{
			get { return RequestParameterType.Db; }
		}
	}

	public class GlobalGoodsParam : IRequestParam
	{
		List<Guid> goods = new List<Guid>();

		public List<Guid> Goods
		{
			get { return goods; }
			set { goods = value; }
		}

		public void ToXml(XmlNode root)
		{
			if (goods != null && goods.Count > 0)
			{
				foreach (Guid g in goods)
				{
					if (g != Guid.Empty)
						Utils.AddNode(root, "GOODS", g);
				}
			}
		}

		public void FromXml(XmlNode root)
		{

		}

		public string Name
		{
			get { return "Товары (глобальные)"; }
		}

		public string ClassName
		{
			get { return "GlobalGoodsParam"; }
		}

		public RequestParameterType ParamType
		{
			get { return RequestParameterType.Db; }
		}
	}

	internal enum RequestItemsSortType { qty_sub, retail_sum, abcgroup, profit }

	//internal class RequestItemsSorter : IComparer<REQUEST_CALCULATION_ITEM>
	//{
	//  private ListSortDirection direction;

	//  public ListSortDirection Direction
	//  {
	//    get { return direction; }
	//    set { direction = value; }
	//  }

	//  RequestItemsSortType sortType;

	//  public RequestItemsSortType SortType
	//  {
	//    get { return sortType; }
	//    set { sortType = value; }
	//  }

	//  public int Compare(REQUEST_CALCULATION_ITEM x, REQUEST_CALCULATION_ITEM y)
	//  {
	//    int result = 0;
	//    switch (sortType)
	//    {
	//      case RequestItemsSortType.qty_sub:
	//        result = x.QTY_SUB.CompareTo(y.QTY_SUB);
	//        break;
	//      case RequestItemsSortType.retail_sum:
	//        result = x.RETAIL_SUM.CompareTo(y.RETAIL_SUM);
	//        break;
	//      case RequestItemsSortType.profit:
	//        result = (x.RETAIL_SUM - x.SUPPLIER_SUM).CompareTo(y.RETAIL_SUM-y.SUPPLIER_SUM);
	//        break;
	//      case RequestItemsSortType.abcgroup:
	//        result = x.ABCCATEGORY.CompareTo(y.ABCCATEGORY);
	//        break;
	//    }
	//    if (direction == ListSortDirection.Descending)
	//      result = result * -1;
	//    return result;
	//  }

	//  private System.Collections.Generic.Dictionary<int, REQUEST_CALCULATION_ITEM> orderDict = new System.Collections.Generic.Dictionary<int, REQUEST_CALCULATION_ITEM>();
	//  public void SaveOrder(List<REQUEST_CALCULATION_ITEM> items)
	//  {
	//    orderDict.Clear();
	//    for (int i=0;i<items.Count;i++)
	//      orderDict.Add(i, items[i]);  
	//  }

	//  public void RestoreOrder(List<REQUEST_CALCULATION_ITEM> items)
	//  {
	//    for (int i = 0; i < items.Count; i++)
	//      items[i] = orderDict[i];
	//    orderDict.Clear();
	//  }
	//}

	public class RequestCalculationParams
	{
		List<IRequestParam> parameters = new List<IRequestParam>();

		public List<IRequestParam> Parameters
		{
			get { return parameters; }
		}

		public XmlDocument MakeDbXml()
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");
			foreach (IRequestParam param in parameters)
			{
				if (param.ParamType == RequestParameterType.Db)
				{
					param.ToXml(root);
				}
			}
			return doc;
		}

		public XmlDocument ToXml()
		{
			XmlDocument doc = new XmlDocument();
			XmlNode root = Utils.AddNode(doc, "XML");
			foreach (IRequestParam param in parameters)
			{
				XmlNode paramNode = Utils.AddNode(root, "REQUEST_PARAM");
				Utils.AddNode(paramNode, "CLASS", param.ClassName);
				XmlNode config = Utils.AddNode(paramNode, "CONFIG");
				param.ToXml(config);
			}
			return doc;
		}

		public void FromXml(XmlDocument doc)
		{
			//parameters.Clear();
			XmlNodeList list = doc.SelectNodes("/XML/REQUEST_PARAM");
			foreach (XmlNode node in list)
			{
				string className = Utils.GetString(node, "CLASS");
				XmlNode config = node.SelectSingleNode("CONFIG");
				IRequestParam param = null;
				switch (className)
				{
					case "DaysPeriodParam":
						param = new DaysPeriodParam();
						param.FromXml(config);
						break;
					case "StoreParam":
						param = new StoreParam();
						param.FromXml(config);
						break;
					case "SubOpTypeParam":
						param = new SubOpTypeParam();
						param.FromXml(config);
						break;
					case "AbcGroupParam":
						param = new AbcGroupParam();
						param.FromXml(config);
						break;
					case "GoodsClassifierParam":
						param = new GoodsClassifierParam();
						param.FromXml(config);
						break;
					case "SalesSpeedCalcParam":
						param = new SalesSpeedCalcParam();
						param.FromXml(config);
						break;
					case "NeedsCalcParam":
						param = new NeedsCalcParam();
						param.FromXml(config);
						break;
					case "GoodsKindParam":
						param = new GoodsKindParam();
						param.FromXml(config);
						break;
				}
				if (param != null)
				{
					IRequestParam existing = null;
					foreach (IRequestParam prm in parameters)
					{
						if (prm.ClassName == param.ClassName)
						{
							existing = prm;
							break;
						}
					}
					if (existing == null)
						parameters.Add(param);
					else
						parameters[parameters.IndexOf(existing)] = param;
				}

			}
		}

		public bool Grouped()
		{
			foreach (IRequestParam param in parameters)
			{
				if (param is GoodsClassifierParam)
					return ((GoodsClassifierParam)param).Group;
			}
			return false;
		}

		public IRequestParam GetParameter(Type t)
		{
			foreach (IRequestParam p in parameters)
			{
				if (p.GetType() == t)
					return p;
			}
			return null;
		}
	}
	//Значения инициализации
	public class RequestCategoryTag
	{
		private List<IRequestParamMethods> controls = new List<IRequestParamMethods>();
		private List<IRequestParam> prms = new List<IRequestParam>();

		public List<IRequestParamMethods> Controls
		{
			get { return controls; }
		}

		public List<IRequestParam> Params
		{
			get { return prms; }
		}

		private string category;

		public string Category
		{
			get { return category; }
		}

		public RequestCategoryTag(string category)
		{
			this.category = category;

		}

		public static List<RequestCategoryTag> InitTagList(List<IRequestParamMethods> categories, List<IRequestParam> prms)
		{
			List<RequestCategoryTag> tags = new List<RequestCategoryTag>();
			foreach (IRequestParamMethods category in categories)
			{
				List<IRequestParam> prmsInCategory = new List<IRequestParam>();
				foreach (Type t in category.SupportsParams)
				{
					foreach (IRequestParam prm in prms)
					{
						if (prm.GetType() == t)
							prmsInCategory.Add(prm);
					}
				}
				bool contains = false;
				foreach (RequestCategoryTag existing in tags)
				{
					if (existing.Category == category.Category)
					{

						existing.Controls.Add(category);
						existing.Params.AddRange(prmsInCategory);
						contains = true;
						break;
					}
				}
				if (!contains)
				{
					RequestCategoryTag tag = new RequestCategoryTag(category.Category);
					tag.Controls.Add(category);
					tag.Params.AddRange(prmsInCategory);
					tags.Add(tag);
				}
			}
			return tags;
		}
	}

}
