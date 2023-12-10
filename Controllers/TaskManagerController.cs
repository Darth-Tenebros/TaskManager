using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManager.DatabaseComms;

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
    }
}