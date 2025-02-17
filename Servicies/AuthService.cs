using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tasks.WebApi.Entities;

namespace Tasks.WebApi.Servicies;

public class AuthService(
    UserManager<User> userManager,
    IConfiguration configuration
   )
{
    public async Task<IdentityResult> RegisterUserAsync(User user, string password) => await userManager.CreateAsync(user, password);

    public async Task<User?> FindUserByEmail(string email) => await userManager.FindByEmailAsync(email);

    public async Task<bool> CheckPasswordAsync(User user, string password) => await userManager.CheckPasswordAsync(user, password);

    public string GenerateToken(User user) => GenerateJwtToken(user);

    private string GenerateJwtToken(User user)
    {
        try
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var key = Encoding.ASCII.GetBytes(jwtSettings["Secret"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FullName)
            }),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpiryInMinutes"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        catch (ArgumentNullException ex)
        {
            throw new ArgumentNullException(nameof(ex), ex.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

}
