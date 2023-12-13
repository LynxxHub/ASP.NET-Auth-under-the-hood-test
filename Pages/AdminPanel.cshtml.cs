using ASP.NET_Auth_under_the_hood_test.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP.NET_Auth_under_the_hood_test.Pages
{

    [Authorize(Policy = "AdminOnly")]
    public class AdminPanelModel : PageModel
    {
        private readonly IHttpClientFactory httpClientFactory;

        [BindProperty]
        public List<TaskDTO> Tasks { get; set; } = new List<TaskDTO>();

        public AdminPanelModel(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task OnGet()
        {
            var httpClient = httpClientFactory.CreateClient("JWTApi");
            Tasks = await httpClient.GetFromJsonAsync<List<TaskDTO>>("Tasks") ?? new List<TaskDTO>();
        }
    }
}
