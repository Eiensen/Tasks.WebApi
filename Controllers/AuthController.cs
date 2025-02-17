using Microsoft.AspNetCore.Mvc;
using Tasks.WebApi.Entities;
using Tasks.WebApi.Models;
using Tasks.WebApi.Servicies;

namespace Tasks.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(
    AuthService service
    ) : ControllerBase
{  

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] Register model)
    {
        var user = new User
        {
            UserName = model.Email,
            Email = model.Email,
            FullName = model.FullName
        };

        var result = await service.RegisterUserAsync(user, model.Password);

        if (result.Succeeded)
        {
            return Ok(new { Message = "User registered successfully" });
        }

        return BadRequest(result.Errors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] Login model)
    {
        var user = await service.FindUserByEmail(model.Email);

        if (user != null && await service.CheckPasswordAsync(user, model.Password))
        {
            var token = service.GenerateToken(user);
            return Ok(new { Token = token });
        }

        return Unauthorized();
    }    
}
