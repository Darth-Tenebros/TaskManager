using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models;

public class Task
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    [ForeignKey("AssigneeUser")] public Guid Assignee { get; set; }

    [InverseProperty("AssignedTasks")] public User AssigneeUser { get; set; }

    public DateTime DueDate { get; set; }
}

public class TaskRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid Assignee { get; set; }
    public DateTime DueDate { get; set; }
}