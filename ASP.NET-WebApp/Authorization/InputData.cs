using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ASP.NET_WebApp.Authorization
{
    public class InputData
    {
        [Display(Name = "User name")]
        [Required]
        public string UserName { get; set; } = String.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = String.Empty;

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; } = false;

    }
}
