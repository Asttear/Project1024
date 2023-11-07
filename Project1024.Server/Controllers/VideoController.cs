using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project1024.Server.Models;
using Project1024.Server.Services;
using Project1024.Shared.Models;
using System.Security.Claims;

namespace Project1024.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VideoController : ControllerBase
{
    private readonly VideoService _videoService;
    private readonly UserManager<User> _userManager;
    private readonly LikeService _likeService;

    public VideoController(VideoService videoService, UserManager<User> userManager, LikeService likeService)
    {
        _videoService = videoService;
        _userManager = userManager;
        _likeService = likeService;
    }

    [HttpGet]
    public IEnumerable<VideoDto> Get(int page = 0, int size = 5)
    {
        return _videoService.GetVideoList(page, size);
    }

    [HttpPost("{id}/like")]
    public IActionResult Like(int id)
    {
        //获取用户id
        ClaimsPrincipal user = HttpContext.User;

        string userIdStr = _userManager.GetUserId(user)!;
        int userId = int.Parse(userIdStr);
        if (!_likeService.VideoLike(userId, id))
            return Conflict();
        return Ok();
    }
}
