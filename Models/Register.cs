namespace Tasks.WebApi.Models;

/// <summary>Модель регистрации пользователя </summary>
public class Register
{
    /// <summary>Почта </summary>
    public required string Email { get; set; }

    /// <summary>ФИО </summary>
    public required string FullName { get; set; }

    /// <summary>Пароль </summary>
    public required string Password { get; set; }
}
