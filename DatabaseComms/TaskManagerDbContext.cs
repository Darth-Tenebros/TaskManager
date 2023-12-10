using Microsoft.EntityFrameworkCore;
using TaskManager.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.DatabaseComms;

public class TaskManagerDbContext : DbContext
{
    public TaskManagerDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Task> Tasks { get; set; }
    public DbSet<User> Users { get; set; }
}