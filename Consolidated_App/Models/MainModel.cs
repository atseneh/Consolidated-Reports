using NPOI.SS.Formula.Functions;

namespace Consolidated_App.Models
{
    public class MainModel
    {
        public List<ReportAccessDTO>? reportaccess { get; set; }
    }
    public class ReportAccessDTO
    {
        public string DefaultName { get; set; }
        public bool Access { get; set; }
    }
}
