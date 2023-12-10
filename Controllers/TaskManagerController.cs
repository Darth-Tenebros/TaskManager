using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public IActionResult AddUser(CreatNewUser user)
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
        public IActionResult AddTask(CreateNewTask task)
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
    }
}