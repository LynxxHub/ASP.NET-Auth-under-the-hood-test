using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace ASP.NET_Auth_under_the_hood_test.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly string _cookieName = "LynxCookie";

        [BindProperty]
        public InputData InputData { get; set; } = new InputData();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            if (InputData.UserName == "Lynx" && InputData.Password == "Lynxhub")
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, InputData.UserName),
                    new Claim(ClaimTypes.Email, InputData.UserName + "@lynxhub.com"),
                    new Claim("Admin", "true"),
                    new Claim("Age", "12")
                };

                ClaimsIdentity LynxIdentity = new ClaimsIdentity(claims, _cookieName);
                ClaimsPrincipal cm = new ClaimsPrincipal(LynxIdentity);

                await HttpContext.SignInAsync(_cookieName, cm);
                return RedirectToPage("/Index");
            }

            return Page();
        }
    }

    public class InputData
    {
        [Display(Name = "User name")]
        [Required]
        public string UserName { get; set; } = String.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = String.Empty;

    }
}
