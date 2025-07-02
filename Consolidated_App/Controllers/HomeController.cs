using System.Diagnostics;
using Cnetv7BufferHolder;
using Consolidated_App.AuthNavigation;
using Consolidated_App.Models;
using Consolidated_App.WebConstants;
using Microsoft.AspNetCore.Mvc;

namespace Consolidated_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private AuthenticationManager _authenticationManager;
        private readonly InitialBufferPopulator _initialBufferPopulator;
        private readonly SharedHelpers _sharedHelpers;
        public HomeController(ILogger<HomeController> logger,
             AuthenticationManager authenticationManager,
             InitialBufferPopulator initialBufferPopulator,
             SharedHelpers sharedHelpers)
        {
            _logger = logger;
            _authenticationManager = authenticationManager;
            _initialBufferPopulator = initialBufferPopulator;
            _sharedHelpers = sharedHelpers;
        }
    

        public async Task<IActionResult> Index()
        {
            var authUser = await _authenticationManager.GetAuthenticatedUser();
            if (authUser == null)
            {
                return RedirectToAction("Login", "Login");
            }
            await _Initialization();
            var reportAccess = await GetReportsWithAccess(authUser.Id);
            

            return View(new MainModel { reportaccess = reportAccess});
        }
        public async Task<List<ReportAccessDTO>> GetReportsWithAccess(int userId)
        {
            var userRole = await _sharedHelpers.GetUserRoleM(userId);
            if (userRole == null || userRole.Role <= 0)
                return new List<ReportAccessDTO>(); 

            var xReports = await _sharedHelpers.GetReportsBySubSystem();
            if (xReports == null || !xReports.Any())
                return new List<ReportAccessDTO>(); 

            var xAccess = await _sharedHelpers.GetAccessM(userRole.Role);
            if (xAccess == null || !xAccess.Any())
                return new List<ReportAccessDTO>(); 
            var reportsWithAccess = xReports
                .Where(report => xAccess.Any(access => access.Reference == report.Id.ToString()))
                .Select(report => new ReportAccessDTO
                {
                    DefaultName = report.DefaultName,
                    Access = true
                })
                .ToList();

            return reportsWithAccess;
        }

        private async Task _Initialization()
        {
            GeneralBufferHolder.FunctionWithAccessMatrix = await _initialBufferPopulator.GetFunctionWithAccessMatrix();
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
