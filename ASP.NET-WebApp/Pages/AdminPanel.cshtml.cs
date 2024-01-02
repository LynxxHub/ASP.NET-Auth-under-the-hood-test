using ASP.NET_Auth_under_the_hood_test.DTO;
using ASP.NET_Auth_under_the_hood_test.Models;
using ASP.NET_WebApp.Authentication;
using ASP.NET_WebApp.Authentication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;

namespace ASP.NET_Auth_under_the_hood_test.Pages
{

    [Authorize(Policy = "AdminOnly")]
    public class AdminPanelModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly APIAuthenticationService _apiAuthenticationService;

        [BindProperty]
        public List<TaskDTO> Tasks { get; set; } = new List<TaskDTO>();

        public AdminPanelModel(IHttpClientFactory httpClientFactory, IConfiguration configuration, APIAuthenticationService apiAuthenticationService)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _apiAuthenticationService = apiAuthenticationService;
        }

        public async Task OnGet()
        {
            var httpClient = _httpClientFactory.CreateClient("JWTApi");
            JwtToken token = await _apiAuthenticationService.EnsureTokenAsync();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token?.AccessToken);

            try
            {
                Tasks = await httpClient.GetFromJsonAsync<List<TaskDTO>>("Tasks") ?? new List<TaskDTO>();
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
            {
                token = await _apiAuthenticationService.EnsureTokenAsync(forceRefresh: true);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token?.AccessToken);

                Tasks = await httpClient.GetFromJsonAsync<List<TaskDTO>>("Tasks") ?? new List<TaskDTO>();
            }
        }
    }
}
