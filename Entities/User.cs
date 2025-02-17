using Microsoft.AspNetCore.Identity;

namespace Tasks.WebApi.Entities;

/// <summary>Сущность - пользователь </summary>
public class User : IdentityUser
{
    /// <summary>ФИО </summary>
    public string FullName { get; set; } = string.Empty;
}