using System.Diagnostics;
using CNET_V7_Domain.Domain.ViewSchema;
using CNET_V7_Domain.Misc;
using Consolidated_App.Models;
using Consolidated_App.WebConstants;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace Consolidated_App.Controllers
{
    public class ActivityReportController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SharedHelpers _sharedHelpers;
        public ActivityReportController(ILogger<HomeController> logger,
             SharedHelpers sharedHelpers)
        {
            _logger = logger;
            _sharedHelpers = sharedHelpers;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<object> GetDocumentList_Activity(DataSourceLoadOptions loadOptions, ConsolidateModel searchModel)
        {
            var testx = await GetAllSalesReport_Acivity(searchModel);

            return DataSourceLoader.Load(testx, loadOptions);
        } 
        
        [HttpGet]
        public async Task<object> GetDocumentList_Activity_Detail(DataSourceLoadOptions loadOptions, ConsolidateModel searchModel)
        {
            var testx = await GetAllSalesReport_Acivity_Detail(searchModel);

            return DataSourceLoader.Load(testx, loadOptions);
        }

        [HttpPost]
        public virtual async Task<IActionResult> getDocumentsList_Activity(ConsolidateModel searchModel)
        {
            if (searchModel == null)
            {
                return BadRequest("Please set a valid search criteria!");
            }

            return PartialView("_consolidateActivityGrid", searchModel);

        }
        public async Task<List<CNET_V7_Domain.Misc.ConsolidateActivityResult>> GetAllSalesReport_Acivity(ConsolidateModel searchModel)
        {
            Dictionary<string, string> Dictionaryvalue = new Dictionary<string, string>();
            var issuedDateRange = GeneralHelpers.parseDateRange(searchModel.uploadDate);
            if (issuedDateRange == null)
            {
                issuedDateRange = new DateRange();
                issuedDateRange.startDate = DateTime.Now.AddYears(-22);
                issuedDateRange.endDate = DateTime.Now.AddYears(-22);
            }
            if (issuedDateRange?.startDate != null)
                Dictionaryvalue.Add("startDate", issuedDateRange?.startDate?.ToString());
            if (issuedDateRange?.endDate != null)
                Dictionaryvalue.Add("endDate", issuedDateRange?.endDate?.ToString());
           
            try
            {
                var report = await _sharedHelpers.GetFilterDynamicData<ResponseModel<List<CNET_V7_Domain.Misc.ConsolidateActivityResult>>>("cnetReport/consolidation_activity_summary", Dictionaryvalue);
                if (report.Success)
                {
                    return report.Data;
                }
                else
                {
                    return new List<CNET_V7_Domain.Misc.ConsolidateActivityResult>();
                }

            }
            catch
            {
                return new List<CNET_V7_Domain.Misc.ConsolidateActivityResult>();
            }
        }
        public async Task<List<CNET_V7_Domain.Misc.ActivityDetailResult>> GetAllSalesReport_Acivity_Detail(ConsolidateModel searchModel)
        {
            Dictionary<string, string> Dictionaryvalue = new Dictionary<string, string>();
            var issuedDateRange = GeneralHelpers.parseDateRange(searchModel.uploadDate);
            if (searchModel.issuedDate == null)
            {
                issuedDateRange = new DateRange();
                issuedDateRange.startDate = DateTime.Now.AddYears(-22);
                issuedDateRange.endDate = DateTime.Now.AddYears(-22);
            }
            if (issuedDateRange?.startDate != null)
                Dictionaryvalue.Add("startDate", issuedDateRange?.startDate?.ToString());
            if (issuedDateRange?.endDate != null)
                Dictionaryvalue.Add("endDate", issuedDateRange?.endDate?.ToString());
            if (searchModel?.currentdistributor != null)
                Dictionaryvalue.Add("distributer", searchModel?.currentdistributor?.ToString());

            try
            {
                var report = await _sharedHelpers.GetFilterDynamicData<ResponseModel<List<CNET_V7_Domain.Misc.ActivityDetailResult>>>("cnetReport/activity_detail_list", Dictionaryvalue);

                if (report.Success)
                {
                    return report.Data;
                }
                else
                {
                    return new List<CNET_V7_Domain.Misc.ActivityDetailResult>();
                }

            }
            catch
            {
                return new List<CNET_V7_Domain.Misc.ActivityDetailResult>();
            }
        }
    }
}
