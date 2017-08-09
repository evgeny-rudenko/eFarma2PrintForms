using System;
using System.Collections.Generic;
using System.Text;
using ePlus.MetaData.Core;

namespace RCBAptekaRuExport
{
  public class EXPORT_ROW
  {
    private long id_lot;
    private long id_contractor;
    private long v_products_id;
    private long v_products_model; // такое же как и v_products_id
    private string v_products_name_1;
    private decimal v_products_price;
    private string v_products_weight = "0.1";
    private decimal v_products_quantity;
    private string v_products_quantity_order_min = "1";
    private string v_products_quantity_order_units = "1";
    private string v_products_sort_order = "100";
    private string v_manufacturers_name;
    private string v_categories_name_1 = "Лекарства, медицинские товары, предметы ухода";
    private string v_tax_class_title = "--нет--";
    private string v_status;
    private string v_action = "";
    private string EOREOR = "EOREOR";

    public long ID_LOT
    {
      get { return id_lot; }
      set { id_lot = value; }
    }

    public long ID_CONTRACTOR
    {
      get { return id_contractor; }
      set { id_contractor = value; }
    }

    public long V_PRODUCTS_ID
    {
      get { return v_products_id; }
      set { v_products_id = value; }
    }

    public string V_PRODUCTS_NAME_1
    {
      get { return v_products_name_1; }
      set { v_products_name_1 = value; }
    }

    public decimal V_PRODUCTS_PRICE
    {
      get { return v_products_price; }
      set { v_products_price = value; }
    }

    public decimal V_PRODUCTS_QUANTITY
    {
      get { return v_products_quantity; }
      set { v_products_quantity = value; }
    }

    public string V_MANUFACTURERS_NAME
    {
      get { return v_manufacturers_name; }
      set { v_manufacturers_name = value; }
    }

    public string V_STATUS
    {
      get { return v_status; }
      set { v_status = value; }
    }

    public String ToText()
    {
      string[] cols = {v_products_id.ToString(), v_products_id.ToString(), v_products_name_1, 
        Utils.GetString(v_products_price).Replace(',', '.').Remove(Utils.GetString(v_products_price).Length-2, 2), v_products_weight, String.Format("{0:N0}",v_products_quantity), 
        v_products_quantity_order_min, v_products_quantity_order_units, v_products_sort_order, 
        v_manufacturers_name, v_categories_name_1, v_tax_class_title, v_status, v_action, EOREOR};
      return string.Join("\t", cols);
    }

    public static String HeaderText()
    {
      string[] cols = {"v_products_id", "v_products_model", "v_products_name_1", 
        "v_products_price", "v_products_weight", "v_products_quantity", "v_products_quantity_order_min", 
        "v_products_quantity_order_units", "v_products_sort_order", "v_manufacturers_name", "v_categories_name_1", 
        "v_tax_class_title", "v_status", "v_action", "EOREOR"};
      return string.Join("\t", cols);
    }
  }
}
