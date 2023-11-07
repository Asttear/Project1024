using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project1024.Server.Models;
using Project1024.Server.Services;
using Project1024.Shared.Models;
using System.Security.Claims;

namespace Project1024.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserFollowerController : ControllerBase
{
    private readonly UserFollowerService _userFollowerService;
    private readonly UserManager<User> _userManager;

    public UserFollowerController(UserFollowerService userFollowerService, UserManager<User> userManager)
    {
        _userFollowerService = userFollowerService;
        _userManager = userManager;
    }


    /// <summary>
    /// 根据用户id获取粉丝列表。
    /// </summary>
    /// <param name="id"></param>
    /// <param name="page"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    [HttpGet("{id}/fans")]
    public IEnumerable<UserDto> GetFuns(int id, int page = 0, int size = 10)
    {
        return _userFollowerService.GetFanList(id, page, size);
    }
    /// <summary>
    /// 根据用户id获取关注列表。
    /// </summary>
    /// <param name="id"></param>
    /// <param name="page"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    [HttpGet("{id}/follows")]
    public IEnumerable<UserDto> GetFollows(int id, int page = 0, int size = 10)
    {
        return _userFollowerService.GetFollowerList(id, page, size);
    }

    [HttpPost("{id}/follow")]
    public IActionResult Follow(int id)
    {
        //获取用户id
        ClaimsPrincipal user = HttpContext.User;

        string userIdStr = _userManager.GetUserId(user)!;
        int userId = int.Parse(userIdStr);
        if (!_userFollowerService.Follow(id, userId))
            return Conflict(new
            {
                Message = "已经关注该用户！"
            });
        return Ok(new
        {
            Message = "关注成功"
        });
    }

    [Authorize]
    [HttpPost("{id}/unfollow")]
    public IActionResult UnFollow(int id)
    {
        //获取用户id
        ClaimsPrincipal user = HttpContext.User;

        string userIdStr = _userManager.GetUserId(user)!;
        int userId = int.Parse(userIdStr);
        if (!_userFollowerService.UnFollow(id, userId))
            return Conflict(new
            {
                Message = "未关注该用户，无法取关！"
            });
        return Ok(new
        {
            Messgae = "取关成功"
        });
    }
    //[HttpGet]
    //public IEnumerable<VideoDto> Get(int page = 0, int size = 5)
    //{
    //    return _userFollowerService.GetVideoList(page, size);
    //}
    //[HttpGet]
    //public IEnumerable<VideoDto> Get(int page = 0, int size = 5)
    //{
    //    return _userFollowerService.GetVideoList(page, size);
    //}
}
