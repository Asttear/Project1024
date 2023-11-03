using Microsoft.AspNetCore.Mvc;
using Project1024.Server.Services;
using Project1024.Shared.Models;

namespace Project1024.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VideoCategoryController : ControllerBase
{
    private readonly VideoCategoryService _videoCategoryService;

    public VideoCategoryController(VideoCategoryService videoCategoryService)
    {
        _videoCategoryService = videoCategoryService;
    }


    [HttpGet]
    public IEnumerable<VideoCategoryDto> Get()
    {
        return _videoCategoryService.GetVideoCategoryList();
    }

    [HttpGet("{id}")]
    public IEnumerable<VideoDto> Get(int id, int page = 0, int size = 5)
    {
        return _videoCategoryService.GetVideoListByCategoryId(id, page, size);
    }
}