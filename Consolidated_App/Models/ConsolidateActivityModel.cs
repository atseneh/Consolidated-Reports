using Consolidated_App.Models.FramworkModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Consolidated_App.Models
{
    public class ConsolidateActivitySearchModel 
    {
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        [DisplayName("Date")]
        public string issuedDate { get; set; }
        public string currentDistributor { get; set; }
        public string uploadDate { get; set; }
    }

    public class ConsolidateActivityResult 
    {
        public string distributor { get; set; }
        public string earliestActivityToday { get; set; }
        public string latestActivityToday { get; set; }
        public string lastseen { get; set; }
        public int vsmCount { get; set; }
        public string vsmCounts { get; set; }
        public int totalCustomersToday { get; set; }
        public int totalSKU { get; set; }
        public decimal totalSales { get; set; }
        public string totalSalesCurrencyFormat { get; set; }
        public string totalQuantityCurrencyFormat { get; set; }
        public string totalTransactionsCurrencyFormat { get; set; }
        public int voulumeSum { get; set; }
        public int totalTransactions { get; set; }
        public decimal totalQuantity { get; set; }
    }
    public class StockConsolidationResult 
    {
        public string lastUpdate { get; set; }
        public string date { get; set; }
        public string sku { get; set; }
        public string description { get; set; }
        public string store { get; set; }
        public string stockOut { get; set; }
        public string balance { get; set; }
        public string distributor { get; set; }
        public string balanceColor { get; set; }
    }
    public class OrderAttachments : BaseSearchModel
    {
        public string attachmentDesc { get; set; }
        public string date { get; set; }
        public string Distributor { get; set; }
        public List<string> distdistributor { get; set; }
        public string removezeros { get; set; }
        public bool removezero { get; set; }
    }
}
