using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP.NET_Auth_under_the_hood_test.Pages
{

    [Authorize(Policy = "AdminOnly")]
    public class AdminPanelModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
