using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP.NET_Auth_under_the_hood_test.Pages
{
    [Authorize(Policy = "18YearsOnly")]
    public class _18YearsOnlyModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
