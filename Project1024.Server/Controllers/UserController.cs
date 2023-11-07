using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Project1024.Shared.Models;
using Project1024.Shared.Services;

namespace Project1024.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> RegisterAsync(RegisterDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var (result, token) = await _userService.RegisterAsync(dto.UserName, dto.Password, dto.Age);
        return result switch
        {
            RegisterResult.Success => Ok(token),
            RegisterResult.AlreadyExists => Conflict(),
            RegisterResult.ServerError => StatusCode(StatusCodes.Status500InternalServerError),
            _ => throw new UnreachableException()
        };
    }

    [HttpPost("Login")]
    public async Task<IActionResult> LoginAsync(LoginDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var (result, token) = await _userService.LoginAsync(dto.UserName, dto.Password);
        return result switch
        {
            LoginResult.Success => Ok(token),
            LoginResult.NotFound => NotFound(),
            LoginResult.WrongPassword => Unauthorized(),
            _ => throw new UnreachableException()
        };
    }
}