using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Consolidated_App.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
    public class LoginModel
    {
        [Required(ErrorMessage = "Username is required!")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        public string? Password { get; set; }

        public bool RememberMe { get; set; }

        public string? CompanyName { get; set; }

        public string? ReturnUrl { get; set; }
    }

}
