
using Microsoft.AspNetCore.Mvc;
using TaskManager.DatabaseComms;
using TaskManager.Models;
using Task = TaskManager.Models.Task;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("api/")]
    public class TaskManagerController : Controller
    {
        private readonly TaskManagerDbContext _taskManagerDbContext;
        public TaskManagerController(TaskManagerDbContext dbContext)
        {
            this._taskManagerDbContext = dbContext;
        }

        #region UserControls
        [HttpGet]
        [Route("user/GetUsers")]
        public IActionResult GetUsers()
        {
            return Ok(_taskManagerDbContext.Users.ToList());
        }
        
        [HttpGet]
        [Route("user/GetUser/{id:guid}")]
        public IActionResult GetUser([FromRoute] Guid id)
        {
            var user = _taskManagerDbContext.Users.Find(id);
            if (user != null)
            {
                return Ok(user);
            }

            return NotFound($"user with id {id} not found");
        }

        [HttpPost]
        [Route("user/AddUser")]
        public IActionResult AddUser(UserRequest user)
        {
            var newUser = new User()
            {
                Id = Guid.NewGuid(),
                Username = user.Username,
                Email = user.Email,
                Password = user.Password
            };
            _taskManagerDbContext.Users.Add(newUser);
            _taskManagerDbContext.SaveChanges();

            return Ok(newUser);
        }

        [HttpPut]
        [Route("user/UpdateUser/{id:guid}")]
        public IActionResult UpdateUser([FromRoute] Guid id, UserRequest userRequest)
        {
            var user = _taskManagerDbContext.Users.Find(id);
            if (user != null)
            {
                user.Username = userRequest.Username;
                user.Email = userRequest.Email;
                user.Password = userRequest.Password;

                _taskManagerDbContext.SaveChanges();
                return Ok(user);
            }

            return NotFound($"No user with id {id}");
        }

        [HttpDelete]
        [Route("user/delete{id:guid}")]
        public IActionResult DeleteUser([FromRoute] Guid id)
        {
            var user = _taskManagerDbContext.Users.Find(id);
            if (user != null)
            {
                _taskManagerDbContext.Users.Remove(user);
                _taskManagerDbContext.SaveChanges();
                return Ok(user);
            }

            return NotFound($"no user with id {id}");
        }
        #endregion

        #region TaskControls
        [HttpGet]
        [Route("task/GetTasks")]
        public IActionResult GetTasks()
        {
            return Ok(_taskManagerDbContext.Tasks.ToList());
        }

        [HttpGet]
        [Route("task/GetTask/{id:guid}")]
        public IActionResult GetTask([FromRoute] Guid id)
        {
            var task = _taskManagerDbContext.Tasks.Find(id);
            if (task != null)
            {
                return Ok(task);
            }

            return NotFound($"task with id {id} not found");
        }

        [HttpGet]
        [Route("task/GetTaskByUserId/{userId:guid}")]
        public IActionResult GetTaskByUserId([FromRoute] Guid userId)
        {
            var userTasks = _taskManagerDbContext.Tasks.Where(task => task.Assignee == userId).ToList();
            if (userTasks.Any())
            {
                return Ok(userTasks);
            }

            return NotFound($"no tasks by user with id {userId}");
        }

        [HttpPost]
        [Route("task/AddTask")]
        public IActionResult AddTask(TaskRequest task)
        {
            var newTask = new Task()
            {
                Id = Guid.NewGuid(),
                Assignee = task.Assignee,
                Description = task.Description,
                DueDate = task.DueDate,
                Title = task.Title
            };

            _taskManagerDbContext.Tasks.Add(newTask);
            _taskManagerDbContext.SaveChanges();

            return Ok(newTask);
        }

        [HttpPut]
        [Route("task/UpdateTask/{id:guid}")]
        public IActionResult UpdateTask([FromRoute] Guid id, TaskRequest taskRequest)
        {
            var task = _taskManagerDbContext.Tasks.Find(id);
            if (task != null)
            {
                task.Title = taskRequest.Title;
                task.Description = taskRequest.Description;
                task.Assignee = taskRequest.Assignee;
                task.DueDate = taskRequest.DueDate;

                _taskManagerDbContext.SaveChanges();
                return Ok(task);
            }

            return NotFound($"No task with id {id}");
        }

        [HttpDelete]
        [Route("task/DeleteTask/{id:guid}")]
        public IActionResult DeleteTask([FromRoute] Guid id)
        {
            var task = _taskManagerDbContext.Tasks.Find(id);
            if (task != null)
            {
                _taskManagerDbContext.Tasks.Remove(task);
                _taskManagerDbContext.SaveChanges();
                return Ok(task);
            }

            return NotFound($"No task with id {id}");
        }
        #endregion
    }
}