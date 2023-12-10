namespace TaskManager.Models;

public class Task
{
    private Guid Id { get; set; }
    private string Title { get; set; }
    private string Description { get; set; }
    private Guid Assignee { get; set; }
    private DateTime DueDate { get; set; }
}