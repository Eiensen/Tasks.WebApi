namespace Tasks.WebApi.Models;

/// <summary>Модель получения задачи </summary>
public class TaskGet
{
    /// <summary>Идентификатор задачи </summary>
    public int Id { get; set; }

    /// <summary>Дата выполнения задачи </summary>
    public DateOnly TaskDate { get; set; }

    /// <summary>Описание задачи </summary>
    public required string Description { get; set; }

    /// <summary>Время, потраченное на выполнение задачи </summary>
    public TimeSpan TimeSpent { get; set; }

    /// <summary>Исполнитель задачи </summary>
    public required string Assignee { get; set; }
}
