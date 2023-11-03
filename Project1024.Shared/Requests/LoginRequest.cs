using System.ComponentModel.DataAnnotations;

namespace Project1024.Shared.Requests;

public class LoginRequest
{
    [MinLength(5)]
    public string UserName { get; set; } = null!;

    [MinLength(6)]
    public string Password { get; set; } = null!;
}
