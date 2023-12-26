using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTTestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly List<Task> _tasks;

        public TasksController()
        {
            _tasks = new List<Task>();
        }

        [HttpGet(Name = "GetTasks")]
        public IEnumerable<Task> Get()
        {
            string userID = User?.Claims?.FirstOrDefault(c => c.Type == "UserID")?.Value ?? "";
            InitializeTasks();
            return _tasks;
        }

        private void InitializeTasks()
        {

            _tasks.Add(new Task
            {
                Name = "Database Cleanup",
                Description = "Remove outdated entries from the customer database.",
                Status = "In Progress"
            });

            _tasks.Add(new Task
            {
                Name = "UI Enhancement",
                Description = "Improve the user interface for better accessibility.",
                Status = "Pending"
            });

            _tasks.Add(new Task
            {
                Name = "Bug Fix: Login Issue",
                Description = "Resolve the login errors encountered by some users.",
                Status = "Completed"
            });

            _tasks.Add(new Task
            {
                Name = "API Optimization",
                Description = "Enhance the performance of the REST API.",
                Status = "On Hold"
            });

            _tasks.Add(new Task
            {
                Name = "Security Audit",
                Description = "Conduct a comprehensive security audit of the system.",
                Status = "Not Started"
            });

            _tasks.Add(new Task
            {
                Name = "Data Analysis Tool",
                Description = "Develop a tool for advanced data analysis.",
                Status = "In Review"
            });

        }
    }
}