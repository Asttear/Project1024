using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Project1024.Server.Models;
using Project1024.Server.Settings;
using Project1024.Shared.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Project1024.Shared.Services;

namespace Project1024.Server.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly JwtSettings _jwtSettings;

    public UserService(UserManager<User> userManager, IOptions<JwtSettings> jwtSettings)
    {
        _userManager = userManager;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<(RegisterResult Result, string? Token)> RegisterAsync(string username, string password, int age)
    {
        var existingUser = await _userManager.FindByNameAsync(username);
        if (existingUser != null)
        {
            return (RegisterResult.AlreadyExists, null);
        }

        var newUser = new User() { UserName = username, Age = age };
        var isCreated = await _userManager.CreateAsync(newUser, password);
        if (!isCreated.Succeeded)
        {
            return (RegisterResult.ServerError, null);
        }

        return (RegisterResult.Success, GenerateJwtToken(newUser));
    }

    public async Task<(LoginResult Result, string? Token)> LoginAsync(string username, string password)
    {
        var existingUser = await _userManager.FindByNameAsync(username);
        if (existingUser == null)
        {
            return (LoginResult.NotFound, null);
        }
        var isCorrect = await _userManager.CheckPasswordAsync(existingUser, password);
        if (!isCorrect)
        {
            return (LoginResult.WrongPassword, null);
        }
        return (LoginResult.Success, GenerateJwtToken(existingUser));
    }

    private string GenerateJwtToken(User user)
    {
        var key = Encoding.ASCII.GetBytes(_jwtSettings.SecurityKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                //Jti:JWT ID，调用方法生成32位无连字符字符串作为唯一标识符
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                //new(ClaimTypes.Name, user.UserName ?? ""),
            }),
            IssuedAt = DateTime.UtcNow,
            NotBefore = DateTime.UtcNow,
            Expires = DateTime.UtcNow.Add(_jwtSettings.ExpiresIn),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var jwtTokenHandler = new JwtSecurityTokenHandler();
        var securityToken = jwtTokenHandler.CreateToken(tokenDescriptor);
        var token = jwtTokenHandler.WriteToken(securityToken);
        return token;
    }
}