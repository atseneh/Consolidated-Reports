using System.Diagnostics;
using Consolidated_App.Models;
using Microsoft.AspNetCore.Mvc;

namespace Consolidated_App.Controllers
{
    public class OrderManagementController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public OrderManagementController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
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
