using ASP.NET_Auth_under_the_hood_test.DTO;

namespace ASP.NET_Auth_under_the_hood_test.Models
{
    public class User
    {
        public string UserID { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public IEnumerable<TaskDTO> Tasks { get; set; } = new List<TaskDTO>();
    }
}