using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models;

/**
 * represents a user
 */
public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
    [InverseProperty("AssigneeUser")]
    public List<Task> AssignedTasks { get; set; }
}

public class UserRequest
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}