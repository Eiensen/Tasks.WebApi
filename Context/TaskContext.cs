using Microsoft.EntityFrameworkCore;
using Tasks.WebApi.Entities;

namespace Tasks.WebApi.Context;

/// <summary>Контекст задач </summary>
public class TaskContext : DbContext
{
    /// <summary>Строка подключения </summary>
    public static string? ConnectionString { get; set; }

    /// <summary>Таблица задач </summary>
    public DbSet<TaskEntity> Tasks { get; set; }

    /// <summary>Таблица пользователей </summary>
    public DbSet<User> Users { get; set; }

    public TaskContext(DbContextOptions<TaskContext> options) : base(options)
    {
       
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TaskEntity>().ToTable("Tasks");
        
        modelBuilder.Entity<TaskEntity>().Property(t => t.TaskDate)
            .HasColumnType("date")
            .IsRequired();

        modelBuilder.Entity<TaskEntity>().Property(t => t.Description)
            .HasMaxLength(255)
            .IsRequired();

        modelBuilder.Entity<TaskEntity>().Property(t => t.TimeSpent)
            .HasColumnType("time")
            .IsRequired();

        modelBuilder.Entity<TaskEntity>().Property(t => t.Assignee)
            .HasMaxLength(100)
            .IsRequired();
    }
}
