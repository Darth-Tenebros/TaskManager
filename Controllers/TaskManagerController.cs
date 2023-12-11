
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using TaskManager.DatabaseComms;
using TaskManager.Models;
using Task = TaskManager.Models.Task;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("api/")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class TaskManagerController : Controller
    {
        private readonly TaskManagerDbContext _taskManagerDbContext;
        public TaskManagerController(TaskManagerDbContext dbContext)
        {
            this._taskManagerDbContext = dbContext;
        }

        #region UserControls
        
        /// <summary>
        /// Retrieve all users in the database
        /// </summary>
        /// <returns>A list with all users.</returns>
        [HttpGet]
        [Route("user/GetUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetUsers()
        {
            return Ok(_taskManagerDbContext.Users.ToList());
        }
        
        /// <summary>
        /// Retrieve a user by the Id
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>A user object.</returns>
        [HttpGet]
        [Route("user/GetUser/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUser([FromRoute] Guid id)
        {
            var user = _taskManagerDbContext.Users.Find(id);
            if (user != null)
            {
                return Ok(user);
            }

            return NotFound($"user with id {id} not found");
        }

        /// <summary>
        /// Add a new user to the database
        /// </summary>
        /// <param name="user">Object with the supplied user data</param>
        /// <returns>The new user.</returns>
        [HttpPost]
        [Route("user/AddUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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

        /// <summary>
        /// Update the user with the given guid
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <param name="userRequest">Object with the supplied user data</param>
        /// <returns>The updated user.</returns>
        [HttpPut]
        [Route("user/UpdateUser/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Delete the user with the given id.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>The deleted user.</returns>
        [HttpDelete]
        [Route("user/delete{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        
        /// <summary>
        /// Retrieve all tasks in the database
        /// </summary>
        /// <returns>A list of tasks.</returns>
        [HttpGet]
        [Route("task/GetTasks")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetTasks()
        {
            return Ok(_taskManagerDbContext.Tasks.ToList());
        }

        /// <summary>
        /// Retrieve the task with the given id
        /// </summary>
        /// <param name="id">The ID of the task.</param>
        /// <returns>A task object.</returns>
        [HttpGet]
        [Route("task/GetTask/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetTask([FromRoute] Guid id)
        {
            var task = _taskManagerDbContext.Tasks.Find(id);
            if (task != null)
            {
                return Ok(task);
            }

            return NotFound($"task with id {id} not found");
        }

        /// <summary>
        /// Delete the task that is associated with the given user id
        /// </summary>
        /// <param name="userId">The id of the user.</param>
        /// <returns>The deleted task.</returns>
        [HttpGet]
        [Route("task/GetTaskByUserId/{userId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetTaskByUserId([FromRoute] Guid userId)
        {
            var userTasks = _taskManagerDbContext.Tasks.Where(task => task.Assignee == userId).ToList();
            if (userTasks.Any())
            {
                return Ok(userTasks);
            }

            return NotFound($"no tasks by user with id {userId}");
        }

        /// <summary>
        /// Add a new task to the database.
        /// </summary>
        /// <param name="task">Object with the supplied data.</param>
        /// <returns>The new task.</returns>
        [HttpPost]
        [Route("task/AddTask")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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

        /// <summary>
        /// Update the task with the given id.
        /// </summary>
        /// <param name="id">Object with the supplied data.</param>
        /// <param name="taskRequest">Object with the supplied task data.</param>
        /// <returns>The updated task.</returns>
        [HttpPut]
        [Route("task/UpdateTask/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Delete the task with the given id.
        /// </summary>
        /// <param name="id">The id of the task.</param>
        /// <returns>The deleted task.</returns>
        [HttpDelete]
        [Route("task/DeleteTask/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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