using Microsoft.AspNetCore.Mvc;
using Project1024.Shared.Models;
using Project1024.Shared.Services;

namespace Project1024.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VideoCategoryController : ControllerBase
{
    private readonly IVideoCategoryService _videoCategoryService;

    public VideoCategoryController(IVideoCategoryService videoCategoryService)
    {
        _videoCategoryService = videoCategoryService;
    }

    [HttpGet]
    public async Task<List<VideoCategoryDto>?> GetCategories()
    {
        return await _videoCategoryService.GetVideoCategoriesAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<List<VideoDto>?> GetVideosByCategory(int id, int page = 0, int size = 5)
    {
        return await _videoCategoryService.GetVideosByCategoryAsync(id, page, size);
    }
}