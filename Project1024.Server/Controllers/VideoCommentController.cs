using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project1024.Server.Models;
using Project1024.Server.Services;
using Project1024.Shared.Models;
using System.Security.Claims;

namespace Project1024.Server.Controllers;


/// <summary>
/// 视频评论控制器
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class VideoCommentController : ControllerBase
{
    private readonly VideoCommentService _videoCommentService;
    private readonly UserManager<User> _userManager;

    public VideoCommentController(VideoCommentService videoCommentService, UserManager<User> userManager)
    {
        _videoCommentService = videoCommentService;
        _userManager = userManager;
    }

    [HttpGet]
    public IEnumerable<CommentDto> Get(int videoId, int page = 0, int size = 5)
    {
        return _videoCommentService.GetCommentList(videoId, page, size);
    }

    //[HttpPost]
    //[Authorize]
    //public void Post(CommentDto commentDto)
    //{
        
    //    return _videoCommentService.AddComment();
    //}
}
