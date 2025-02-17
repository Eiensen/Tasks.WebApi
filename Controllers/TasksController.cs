using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tasks.WebApi.Entities;
using Tasks.WebApi.Models;
using Tasks.WebApi.Servicies;

namespace Tasks.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TasksController(
    TaskService service
    ) : ControllerBase
{
   
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskGet>>> GetTasks()
    {
        var tasks = await service.GetAllAsync();

        return tasks.Select(x => new TaskGet
        {
            Id = x.Id,
            Assignee = x.Assignee,
            Description = x.Description,
            TaskDate = x.TaskDate,
            TimeSpent = x.TimeSpent,
        }).ToList();
    }

    [HttpPost]
    public async Task<ActionResult<TaskGet>> AddTask([FromBody] TaskAdd task)
    {
        if (task.TaskDate != DateOnly.FromDateTime(DateTime.UtcNow))
        {
            return BadRequest("Дата должна быть сегодняшней.");
        }

        if (task.TimeSpent <= TimeSpan.Zero)
        {
            return BadRequest("Время должно быть больше 0.");
        }
        TaskEntity newTask = new()
        {
            TaskDate = task.TaskDate,
            TimeSpent = task.TimeSpent,
            Description = task.Description,
            Assignee = User.Identity?.Name ?? ""
        };

        await service.CreateAsync(newTask);

        return CreatedAtAction(nameof(GetTasks), new { id = newTask.Id }, newTask);
    }
}
