namespace Tasks.WebApi.Models;

/// <summary>Модель добавления задачи </summary>
public class TaskAdd
{
    /// <summary>Дата выполнения задачи </summary>
    public required DateOnly TaskDate { get; set; }

    /// <summary>Описание задачи </summary>
    public required string Description { get; set; }

    /// <summary>Время, потраченное на выполнение задачи </summary>
    public required TimeSpan TimeSpent { get; set; }
}
