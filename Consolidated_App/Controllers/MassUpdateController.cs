
using System.Collections.Generic;
using System.Diagnostics;
using CNET_V7_Domain.Domain.Hrms;
using CNET_V7_Domain.Misc.ThirdParty;
using CNET_V7_Domain;
using Consolidated_App.Models;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using CNET_V7_Domain.Domain.ArticleSchema;
using CNET_V7_Domain.Misc.PmsDTO;
namespace Consolidated_App.Controllers
{
    public class MassUpdateController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public MassUpdateController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ImportExcelCount(MassUpdateForImport exCount)
        {
            if (string.IsNullOrEmpty(exCount?.distributorID))
            {
                TempData["errorData"] = "Please Select Distributor First!";
                return RedirectToAction("Index");
            }

            if (exCount?.fileUpload == null || exCount.fileUpload.Length == 0)
            {
                return Content("<center><span style='height:30px;line-height:30px;padding: 0px 15px;' class='alert alert-danger'>Invalid Excel File!</span></center>");
            }

            IFormFile file = exCount.fileUpload;
            string fileName = file.FileName;
            string sFileExtension = Path.GetExtension(fileName).ToLower();

            if (sFileExtension != ".xls" && sFileExtension != ".xlsx" && sFileExtension != ".csv" && sFileExtension != ".xlsm" && sFileExtension != ".xlsb")
            {
                return Content("<center><span style='height:30px;line-height:30px;padding: 0px 15px;' class='alert alert-danger'>Invalid Excel File!</span></center>");
            }

            List<ImportedCustomerModel> importedCustomers = new List<ImportedCustomerModel>();
            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                stream.Position = 0;

                ISheet sheet;
                if (sFileExtension == ".xls")
                {
                    HSSFWorkbook hssfwb = new HSSFWorkbook(stream);
                    sheet = hssfwb.GetSheetAt(0);
                }
                else
                {
                    XSSFWorkbook hssfwb = new XSSFWorkbook(stream);
                    sheet = hssfwb.GetSheetAt(0);
                }

                IRow headerRow = sheet.GetRow(0);
                int cellCount = headerRow?.LastCellNum ?? 0;

                for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    if (row == null || row.LastCellNum != 11) continue;

                    var data = new ImportedCustomerModel
                    {
                        organizationID = row.GetCell(0)?.ToString(),
                        organizationName = row.GetCell(1)?.ToString(),
                        parentCategory = row.GetCell(3)?.ToString(),
                        category = row.GetCell(4)?.ToString(),
                        tin = row.GetCell(5)?.ToString(),
                        city = row.GetCell(6)?.ToString(),
                        telephone = row.GetCell(7)?.ToString(),
                        street = row.GetCell(8)?.ToString(),
                        latitude = double.TryParse(row.GetCell(9)?.ToString(), out double lat) ? lat : 0,
                        longitude = double.TryParse(row.GetCell(10)?.ToString(), out double lng) ? lng : 0,
                    };

                    importedCustomers.Add(data);
                }
            }

            TempData["successData"] = "Updated!";
            var importResults = new MassUpdateForImport
            {
                ExcelCountList = importedCustomers,
                distributorName = exCount.distributorName
            };

            return View("Index", importResults);
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
