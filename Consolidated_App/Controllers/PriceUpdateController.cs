using System.Diagnostics;
using CNET_V7_Domain.Domain.CommonSchema;
using CNET_V7_Domain.Domain.TransactionSchema;
using CNET_V7_Domain.Domain.ViewSchema;
using CNET_V7_Domain.Misc;
using Consolidated_App.Models;
using Consolidated_App.WebConstants;
using Microsoft.AspNetCore.Mvc;

namespace Consolidated_App.Controllers
{
    public class PriceUpdateController : Controller
    {
        private Consolidated_App.AuthNavigation.AuthenticationManager _authenticationManager;
        private readonly ILogger<HomeController> _logger;
        private readonly SharedHelpers _sharedHelpers;
        int loggedUser = 0;
        int currentConsigneeUnit = 0;
        public PriceUpdateController(ILogger<HomeController> logger,
             SharedHelpers sharedHelpers,
             AuthNavigation.AuthenticationManager authenticationManager)
        {
            _logger = logger;
            _sharedHelpers = sharedHelpers;
            _authenticationManager = authenticationManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetArticlesByDistributor([FromBody] DistributorRequest request)
        {
            var articles = new List<ArticlePriceUpdateResponse>();
            Dictionary<string, string> Dictionaryvalue = new Dictionary<string, string>();
            Dictionaryvalue.Add("distributer", request?.Distributor?.ToString());
            
            var articleUpdates2 = new List<ArticlePriceUpdateResponse>();
            var report = await _sharedHelpers.GetFilterDynamicData<ResponseModel<List<ArticlePriceUpdateResponse>>>("cnetReport/get_articles_by_distributer", Dictionaryvalue);
            if(report != null && report.Success)
            {
                articleUpdates2 = report.Data.ToList();
            }
          
            foreach (var article in articleUpdates2)
            {
                var articleUpdate = new ArticlePriceUpdateResponse
                {
                    ArticleId = article.ArticleId,
                    Name = article.Name.ToString(),
                    Code = article.Code.ToString(),
                   
                };
                articles.Add(articleUpdate);
            }
            return Json(new { articles });
        }

        [HttpPost]
        public async Task<IActionResult> SaveArticlePrices([FromBody] SaveArticlesRequest request)
        {
            
            await currentUserAndUnit();
            request.Branch = 3;
            request.User = loggedUser;
            var response = await saveupdatedArticleValues(request);
            if (response != null && response.Success)
            {
                return Json(new
                {
                    success = true,
                    message = "Article prices updated successfully!"
                });
            }

            return Json(new
            {
                success = false,
                message = "Failed to update article prices. Please try again."
            });
        }

        public async Task<ResponseModel<SavedArticlePriceResponse>> saveupdatedArticleValues(SaveArticlesRequest postVoucherBuffer)
        {
            var xSavedResult = await _sharedHelpers.SendReqAsync<SaveArticlesRequest, ResponseModel<SavedArticlePriceResponse>>("cnetReport/save_article_price", HttpMethod.Post, postVoucherBuffer);
            return xSavedResult;
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private async Task currentUserAndUnit()
        {
            var authUser = await _authenticationManager.GetAuthenticatedUser();
            if (authUser != null)
            {
                loggedUser = authUser.Id;
            }
        }
    }
    public class ArticlePriceUpdateResponse2
    {
        public int ArticleId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public decimal SelfPrice { get; set; }
        public decimal DeliveryPrice { get; set; }
    }
    public class SaveArticlesRequest2
    {
        public string Distributer { get; set; }

        public int Branch { get; set; }

        public List<ArticlePriceUpdateResponse2> UpdatedArticles { get; set; }
    }
}
