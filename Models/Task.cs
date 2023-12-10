namespace TaskManager.Models;

public class Task
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid Assignee { get; set; }
    public DateTime DueDate { get; set; }
}

public class TaskRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid Assignee { get; set; }
    public DateTime DueDate { get; set; }
}