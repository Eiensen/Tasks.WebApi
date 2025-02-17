namespace Tasks.WebApi.Entities;

/// <summary>Сущность - задача </summary>
public class TaskEntity
{
    /// <summary>Идентификатор задачи </summary>
    public int Id { get; set; }

    /// <summary>Дата выполнения задачи </summary>
    public required DateOnly TaskDate { get; set; }

    /// <summary>Описание задачи </summary>
    public required string Description { get; set; }

    /// <summary>Время, потраченное на выполнение задачи </summary>
    public required TimeSpan TimeSpent { get; set; }

    /// <summary>Исполнитель задачи </summary>
    public required string Assignee { get; set; }
}
