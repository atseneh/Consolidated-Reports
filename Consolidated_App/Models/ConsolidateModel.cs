using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Consolidated_App.Models.FramworkModels;

namespace Consolidated_App.Models
{
    public class ConsolidateModel
    {
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        [DisplayName("Date")]
        public string uploadDate { get; set; }
        public string issuedDate { get; set; }

        [DisplayName("Distributor")]
        public string Distributor { get; set; }
        public string currentdistributor { get; set; }
        public string monitorType { get; set; }
    }
    public class ConsolidateResult 
    {
        public int id { get; set; }
        public string code { get; set; }
        public string distributer { get; set; }
        public DateTime issueddate { get; set; }
        public string customrId { get; set; }
        public string customre_Name { get; set; }
        public string tin { get; set; }
        public string user_Name { get; set; }
        public string machine_Number { get; set; }
        public string sKU { get; set; }
        public string brand { get; set; }
        public string pack_Type { get; set; }
        public decimal qTY { get; set; }
        public decimal? priceBeforeVat { get; set; }
        public decimal? vat { get; set; }
        public string statusColor { get; set; }
        public string comp { get; set; }
        public string sEMID { get; set; }
        public decimal grandTotal { get; set; }
        public string presalesvssales { get; set; }
        public string sectorID { get; set; }
    }
    public class StockConsolidationSearch 
    {
        public string store { get; set; }
        public string uploadDate { get; set; }
        public string Distributor { get; set; }
        public string diststorescode { get; set; }
        public string diststoresname { get; set; }
        public string removezeros { get; set; }
        public bool removezero { get; set; }
    }
    public class EcommercevSearch : BaseSearchModel
    {
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        [DisplayName("Date")]
        public string uploadDate { get; set; }
        public string issuedDate { get; set; }

        [DisplayName("Distributor")]
        public string Distributor { get; set; }
        public string currentdistributor { get; set; }
        public string monitorType { get; set; }
    }
    public class OtherSettings
    {
        public string FtpFilePathIP { get; set; }
        public string BaseApiIp { get; set; }
    }
}
