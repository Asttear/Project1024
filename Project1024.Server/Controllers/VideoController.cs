using Microsoft.AspNetCore.Mvc;
using Project1024.Server.Services;
using Project1024.Shared.Models;

namespace Project1024.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VideoController : ControllerBase
{
    private readonly VideoService _videoService;

    public VideoController(VideoService videoService)
    {
        _videoService = videoService;
    }

    [HttpGet]
    public IEnumerable<VideoDto> Get(int page = 0, int size = 5)
    {
        return _videoService.GetVideoList(page, size);
    }
}
