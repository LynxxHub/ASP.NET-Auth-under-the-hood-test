namespace ASP.NET_Auth_under_the_hood_test.DTO
{
    public class UserDTO
    {
        public string UserID { get; set; } = string.Empty;
        public IEnumerable<TaskDTO> Tasks { get; set; } = new List<TaskDTO>();
    }
}