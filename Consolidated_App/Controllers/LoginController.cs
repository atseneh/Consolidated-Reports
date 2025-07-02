
using Cnetv7BufferHolder;
using Consolidated_App.AuthNavigation;
using Consolidated_App.Controllers;
using Consolidated_App.Models;
using Consolidated_App.WebConstants;
using Microsoft.AspNetCore.Mvc;
//using System.Data.Entity;

namespace Consolidated_App.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient _httpClient;
        private AuthenticationManager _authenticationManager;
        private readonly SharedHelpers _sharedHelpers;
        public LoginController(IHttpClientFactory httpClientFactory,
            AuthenticationManager authenticationManager,
            SharedHelpers sharedHelpers)
        {
            _httpClient = httpClientFactory.CreateClient("mainclient");
            _authenticationManager = authenticationManager;
            _sharedHelpers = sharedHelpers;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var loginResult = await _authenticationManager.AuthenticateUser(model.Username?.Trim(), model.Password);

                if (loginResult.Success)
                {
                    var user = await _sharedHelpers.GetUserByUserName(model.Username);
                    _authenticationManager.SignIn(user, model.RememberMe);

                    var redirectUrl = string.IsNullOrEmpty(model.ReturnUrl) || !Url.IsLocalUrl(model.ReturnUrl)
                        ? Url.Action("Index", "Home")
                        : model.ReturnUrl;

                    return Json(new { success = true, redirectUrl });
                }

                return Json(new { success = false, message = loginResult.Message });
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return Json(new { success = false, message = string.Join(" ", errors) });
        }


        public async Task<IActionResult> Logout()
        {
            _authenticationManager.SignOut();

            return RedirectToAction("Index");
        }

        
    }
}
