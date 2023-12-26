namespace JWTTestAPI
{
    public class User
    {
        public string UserID { get; set; } = string.Empty;
        public IEnumerable<Task> Tasks { get; set; } = new List<Task>();
    }
}
