using ASP.NET_Auth_under_the_hood_test.Authorization;
using ASP.NET_Auth_under_the_hood_test.DTO;
using ASP.NET_Auth_under_the_hood_test.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ASP.NET_Auth_under_the_hood_test.Pages
{

    [Authorize(Policy = "AdminOnly")]
    public class AdminPanelModel : PageModel
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;

        [BindProperty]
        public List<TaskDTO> Tasks { get; set; } = new List<TaskDTO>();

        public AdminPanelModel(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
        }

        public async Task OnGet()
        {
            var httpClient = httpClientFactory.CreateClient("JWTApi");
            string authKey = configuration.GetValue<string>("Authentication-Key") ??"";

            httpClient.DefaultRequestHeaders.Add("Authentication-Key", authKey);
            var response = await httpClient.PostAsJsonAsync("auth", new UserDTO{ UserID= Guid.NewGuid().ToString() });
            response.EnsureSuccessStatusCode();

            string jwtStr = await response.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<JwtToken>(jwtStr);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token?.AccessToken);
            Tasks = await httpClient.GetFromJsonAsync<List<TaskDTO>>("Tasks") ?? new List<TaskDTO>();
        }
    }
}
