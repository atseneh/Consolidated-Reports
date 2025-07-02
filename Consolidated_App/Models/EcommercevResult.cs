
using Consolidated_App.Models.FramworkModels;

namespace Consolidated_App.Models
{
    public class EcommercevResult 
    {
        public string outletName { get; set; }
        public string referenceOrder { get; set; }
        public string deliveryDate { get; set; }
        public string numberOfOrders { get; set; }
        public string orderStatus { get; set; }
        public List<detailList> orderDetail { get; set; }
    }
    public class CustormerMVResult : BaseNopModel
    {
        public int id { get; set; }
        public string code { get; set; }
        public string cusName { get; set; }
        public string sector { get; set; }
        public string category { get; set; }
        public string selected { get; set; }
        public bool isActive { get; set; }
        public string connectTo { get; set; }
    }
    public class EcommerceVoucherResult : BaseNopModel
    {
        public string outletName { get; set; }
        public string issuedDate { get; set; }
        public string referenceOrder { get; set; }
        public string deliveryDate { get; set; }
        public string numberOfOrders { get; set; }
        public string orderStatus { get; set; }
        public string distributor { get; set; }
        public string vsmusername { get; set; }
        public string machine_Number { get; set; }
        public string user_Name { get; set; }
        public string sku { get; set; }
        public string sku_code { get; set; }
        public string quantityOrdered { get; set; }
        public string quantityDelivered { get; set; }
        public string price_Bfore_VAT { get; set; }
        public string vAT { get; set; }
        public string sEMID { get; set; }
        public string srname { get; set; }
    }
    public class EcommresultList
    {
        public string filteringDate { get; set; }
        public string consignee { get; set; }
        public string skucode { get; set; }
        public List<EcommercevResult> EcommOrderLists { get; set; }
    }
    public class detailList 
    {
        public string sku_code { get; set; }
        public string sku { get; set; }
        public string quantityOrdered { get; set; }
        public string quantityDelivered { get; set; }

    }

}
