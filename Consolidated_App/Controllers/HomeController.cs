using System.Diagnostics;
using Consolidated_App.AuthNavigation;
using Consolidated_App.Models;
using Microsoft.AspNetCore.Mvc;

namespace Consolidated_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private AuthenticationManager _authenticationManager;
        public HomeController(ILogger<HomeController> logger,
             AuthenticationManager authenticationManager)
        {
            _logger = logger;
            _authenticationManager = authenticationManager;
        }

        public async Task<IActionResult> Index()
        {
            var authUser = await _authenticationManager.GetAuthenticatedUser();
            if (authUser == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
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
