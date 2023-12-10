using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManager.DatabaseComms;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskManagerController : Controller
    {
        private readonly TaskManagerDbContext _taskManagerDbContext;
        public TaskManagerController(TaskManagerDbContext dbContext)
        {
            this._taskManagerDbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(_taskManagerDbContext.Users.ToList());
        }
        
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetUser([FromRoute] Guid id)
        {
            var user = _taskManagerDbContext.Users.Find(id);
            if (user != null)
            {
                return Ok(user);
            }

            return NotFound($"user with id {id} not found");
        }
    }
}