namespace Tasks.WebApi.Models;

/// <summary>Модель авторизации пользователя </summary>
public class Login
{
    /// <summary>Почта </summary>
    public required string Email { get; set; }

    /// <summary>Пароль </summary>
    public required string Password { get; set; }
}
