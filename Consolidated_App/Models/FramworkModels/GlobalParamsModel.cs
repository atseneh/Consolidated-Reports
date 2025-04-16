using CNET_V7_Domain.Domain.ConsigneeSchema;
using CNET_V7_Domain.Misc.CommonTypes;

namespace Consolidated_App.Models.FramworkModels
{
    public class GlobalParamsModel
    {
        public int? gslType { get; set; }
        public List<NavigatorDTO>? navigatorList { get; set; }
        public ConsigneeDTO? personInfo { get; set; }
        public string? currentBranch { get; set; }
        //public DocumentSearchModel? DocumentSearchParms { get; set; }
    }
    public class ProductionSaveResponse
    {
        public string savedVoucherCode { get; set; }
        public int savedVoucherId { get; set; }
        public bool issaved { get; set; }
        public string reason { get; set; }
    }
}
