
using System.Diagnostics;
using System.Net.Http;
using CNET_V7_Domain.Domain.ArticleSchema;
using CNET_V7_Domain.Domain.CommonSchema;
using CNET_V7_Domain.Domain.SecuritySchema;
using CNET_V7_Domain.Domain.ViewSchema;
using CNET_V7_Domain.Misc;
using CNET_V7_Domain.Misc.CommonTypes;
using Consolidated_App.Models;
using Consolidated_App.WebConstants;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Consolidated_App.Controllers
{
    public class ConsolidatedReportController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SharedHelpers _sharedHelpers;
        public ConsolidatedReportController(ILogger<HomeController> logger,
             SharedHelpers sharedHelpers)
        {
            _logger = logger;
            _sharedHelpers = sharedHelpers;
        }

        public async Task<IActionResult> Index()
        {
            return View(new ConsolidateModel());
        }
        [HttpGet]
        public async Task<object> GetDocumentList(DataSourceLoadOptions loadOptions, ConsolidateModel searchModel)
        {
            var testx = await GetAllSalesReport(searchModel);
       
            return DataSourceLoader.Load(testx, loadOptions);
        }
        [HttpPost]
        public virtual async Task<IActionResult> getDocumentsList(ConsolidateModel searchModel)
        {
            if (searchModel == null)
            {
                return BadRequest("Please set a valid search criteria!");
            }

            return PartialView("_consolidateGrid", searchModel);

        }
        public async Task<List<AllSalesReportViewDTO>> GetAllSalesReport(ConsolidateModel searchModel)
        {
                Dictionary<string, string> Dictionaryvalue = new Dictionary<string, string>();
            var issuedDateRange = GeneralHelpers.parseDateRange(searchModel.uploadDate);
            if (searchModel.issuedDate == "none")
            {
                issuedDateRange = new DateRange();
                issuedDateRange.startDate = DateTime.Now.AddYears(-22);
                issuedDateRange.endDate = DateTime.Now.AddYears(-22);
            }
            if (issuedDateRange?.startDate != null)
                Dictionaryvalue.Add(":IssuedDate", issuedDateRange?.startDate?.ToString());
            if (issuedDateRange?.endDate != null)
                Dictionaryvalue.Add("IssuedDate:", issuedDateRange?.endDate?.ToString());
            if (searchModel.Distributor != null && searchModel.Distributor != "")
                Dictionaryvalue.Add("Org", searchModel.Distributor.ToString());
            try
            {
                var report = await _sharedHelpers.GetDynamicData<List<AllSalesReportViewDTO>>("AllSalesReportView", Dictionaryvalue);
             
                if(report != null && report.Count() > 0)
                {
                    return report;
                }
                else
                {
                    return new List<AllSalesReportViewDTO>();
                }
               
            }
            catch
            {
                return new List<AllSalesReportViewDTO>();
            }
        }
    }
}

