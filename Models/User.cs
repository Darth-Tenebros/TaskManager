namespace TaskManager.Models;

/**
 * represents a user
 */
public class User
{
    private Guid Id { get; set; }
    private string Username { get; set; }
    private string Email { get; set; }
    private string Password { get; set; }
}