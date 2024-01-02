using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTTestAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public AuthController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost]
        public IActionResult Authenticate([FromBody] User user, [FromHeader(Name = "Authentication-Key")] string authKey)
        {
            string auth = configuration.GetValue<string>("Authentication-Key") ?? "";

            if (authKey != auth)
            {
                ModelState.AddModelError("Unauthorized", "You're not authorized to access the endpoint you're looking for.");
                return Unauthorized(ModelState);
            }

            List<Claim> claims = new()
            {
                new Claim("UserID", user.UserID)
            };

            var expiringDate = DateTime.UtcNow.AddSeconds(30);


            return Ok(new
            {
                access_token = GenerateJWT(claims, expiringDate),
                expiring_date = expiringDate,
            });
        }

        private string GenerateJWT(IEnumerable<Claim> claims, DateTime expiringDate)
        {
            byte[] securityKey = Encoding.ASCII.GetBytes(configuration.GetValue<string>("SKey") ?? "");

            var jwt = new JwtSecurityToken(
                    claims: claims,
                    notBefore: DateTime.UtcNow,
                    expires: expiringDate,
                    signingCredentials: new SigningCredentials(
                                new SymmetricSecurityKey(securityKey),
                                SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
