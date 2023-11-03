using Microsoft.AspNetCore.Mvc;
using Project1024.Server.Services;
using Project1024.Shared.Requests;
using Project1024.Shared.Responses;
namespace Project1024.Server.Controllers;


[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        if (!ModelState.IsValid)
        {
            return Unauthorized(new FailedResponse()
            {
                Errors = new[] { "Required fields not in correct format!" }
            });
        }
        var result = await _userService.RegisterAsync(request.UserName, request.Password, request.Age);
        if (!result.Success)
        {
            return BadRequest(new FailedResponse()
            {
                Errors = result.Errors
            });
        }
        return Ok(new TokenResponse
        {
            AccessToken = result.AccessToken!,
            TokenType = result.TokenType!
        });
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return Unauthorized(new FailedResponse()
            {
                Errors = new[] { "Required fields not in correct format!" }
            });
        }
        var result = await _userService.LoginAsync(request.UserName, request.Password);
        if (!result.Success)
        {
            return Unauthorized(new FailedResponse()
            {
                Errors = result.Errors
            });
        }

        return Ok(new TokenResponse
        {
            AccessToken = result.AccessToken!,
            TokenType = result.TokenType!    
        });
    }
}
