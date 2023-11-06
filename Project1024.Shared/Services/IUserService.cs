using Project1024.Shared.Models;

namespace Project1024.Shared.Services;

public interface IUserService
{
    Task<(RegisterResult Result, string? Token)> RegisterAsync(string username, string password, int age);
    Task<(LoginResult Result, string? Token)> LoginAsync(string username, string password);
}