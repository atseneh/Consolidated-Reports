using System.Diagnostics;
using System.Net;
using System.Text;
using CNET_V7_Domain.Domain.ViewSchema;
using Consolidated_App.Models;
using Consolidated_App.WebConstants;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Consolidated_App.Controllers
{ 
    public class OrderManagementController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SharedHelpers _sharedHelpers;
        private static OtherSettings _ftpSettings = new OtherSettings();
        public OrderManagementController(ILogger<HomeController> logger,
             SharedHelpers sharedHelpers,
             IOptions<OtherSettings> otherSettings)
        {
            _logger = logger;
            _sharedHelpers = sharedHelpers;
            _ftpSettings = otherSettings.Value;
        }

        public IActionResult Index()
        {
            return View(new StockConsolidationSearch { uploadDate = DateTime.Now.ToString() + "-" + DateTime.Now.ToString() });
        }

        [HttpGet]
        public async Task<object> GetDocumentList(DataSourceLoadOptions loadOptions, StockConsolidationSearch searchModel)
        {
            var testx = await GetOrdersReport(searchModel);

            var groupedResult = testx
                 .GroupBy(item => item.Code)
                 .Select(group => new
                 {
                     Code = group.Key,
                     distributor = group.First().Distributer,
                     orderPlacementDate = group.First().OrderPlacementDate,
                     driverName = group.First().DriverName,
                     truckCode = group.First().TruckPlateNo,
                     trailerCode = group.First().TrailerPlateNo,
                     Description = string.Join(", ", group
                                                 .Where(g => !string.IsNullOrEmpty(g.Description))
                                                 .Select(g => g.Description)
                                             ),
                     state = group.First().State,
                     cgrnVoucher = group.First().CgrnVoucher,
                     invoiceNumber = group.First().InvoiceNo,
                     url = group.Select(g => g.Url).ToArray(),
                     grandTotalAmount = group.First().GrandTotal
                 })
                 .ToList();


            return DataSourceLoader.Load(groupedResult, loadOptions);
        }

        [HttpPost]
        public virtual async Task<IActionResult> getDocumentsList(StockConsolidationSearch searchModel)
        {
            if (searchModel == null)
            {
                return BadRequest("Please set a valid search criteria!");
            }

            return PartialView("_attachmentConsolidate", searchModel);
        }
        
        public async Task<List<VwConsolidatedHeinkenAttachmentDTO>> GetOrdersReport(StockConsolidationSearch searchModel)
        {
            Dictionary<string, string> Dictionaryvalue = new Dictionary<string, string>();
            var issuedDateRange = GeneralHelpers.parseDateRange(searchModel.uploadDate);
            if (searchModel.uploadDate == "none")
            {
                issuedDateRange = new DateRange();
                issuedDateRange.startDate = DateTime.Now.AddYears(-22);
                issuedDateRange.endDate = DateTime.Now.AddYears(-22);
            }
            if (issuedDateRange?.startDate != null)
                Dictionaryvalue.Add(":OrderPlacementDate", issuedDateRange?.startDate?.ToString());
            if (issuedDateRange?.endDate != null)
                Dictionaryvalue.Add("OrderPlacementDate:", issuedDateRange?.endDate?.ToString());
            if (searchModel.Distributor != null && searchModel.Distributor != "")
                Dictionaryvalue.Add("Distributer", searchModel.Distributor.ToString());
            try
            {
                var report = await _sharedHelpers.GetDynamicData<List<VwConsolidatedHeinkenAttachmentDTO>>("VwConsolidatedHeinkenAttachment", Dictionaryvalue);

                if (report != null && report.Count() > 0)
                {
                    return report;
                }
                else
                {
                    return new List<VwConsolidatedHeinkenAttachmentDTO>();
               }

            }
            catch
            {
                return new List<VwConsolidatedHeinkenAttachmentDTO>();
               
            }
        }
        #region read attachment
        string FtpFilePath_IP = _ftpSettings.FtpFilePathIP;
        string userName = "CHM_USER";
        string passWord = "AttACHeMenT5&@BBMF@TIIvsDNR";
        #endregion
        [HttpPost]
        public async Task<IActionResult> Retrive_Attachement(string urltoread)
        {
            // Split the concatenated string into a list of URLs
            var urls = urltoread.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(url => url.Trim())
                                .ToList();

            // Construct the list of Default Image URLs
            var DefaultImageUrls = urls.Select(url => String.Format(FtpFilePath_IP + url)).ToList();

            // Return the list of URLs as JSON
            return Json(new { data = DefaultImageUrls });
        }

        public async Task<Stream> ReadAttachmentFile(string id)
        {
            Stream responseStream = null;
            MemoryStream memoryStream = new MemoryStream();

            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(id);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential(userName, passWord);

                using (FtpWebResponse response = await request.GetResponseAsync() as FtpWebResponse)
                {
                    responseStream = response.GetResponseStream();
                    await responseStream.CopyToAsync(memoryStream);
                    memoryStream.Position = 0; // Reset position for reading
                    return memoryStream;
                }
            }
            catch (WebException webEx)
            {
                // Log the error if needed
                // Return a default image or error message stream
                return GetErrorImageStream();
            }
            catch (Exception ex)
            {
                // Log the error if needed
                // Return a default image or error message stream
                return GetErrorImageStream();
            }
            finally
            {
                responseStream?.Dispose();
            }
        }

        private Stream GetErrorImageStream()
        {
            // You can return a default "file not found" image or message
            // For example:
            byte[] errorMessage = Encoding.UTF8.GetBytes("File could not be loaded");
            return new MemoryStream(errorMessage);

            // Or if you have an error image:
            // return File.OpenRead("~/Content/Images/file-not-found.png");
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
    }
}
