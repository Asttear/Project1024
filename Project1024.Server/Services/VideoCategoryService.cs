using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Project1024.Server.Data;
using Project1024.Shared.Models;
using Project1024.Shared.Services;

namespace Project1024.Server.Services;

public class VideoCategoryService : IVideoCategoryService
{
    private readonly VideoContext _videoContext;
    private readonly UserContext _userContext;
    private readonly QiniuOptions _qiniuOptions;
    private readonly QiniuService _qiniuService;

    public VideoCategoryService(VideoContext videoContext, IOptions<QiniuOptions> options, QiniuService qiniuService, UserContext userContext)
    {
        _videoContext = videoContext;
        _qiniuOptions = options.Value;
        _qiniuService = qiniuService;
        _userContext = userContext;
    }

    public async Task<List<VideoCategoryDto>?> GetVideoCategoriesAsync()
    {
        return await _videoContext.VideoCategories.OrderBy(v => v.Id)
                                                  .Select(c => new VideoCategoryDto(c.Id, c.Name))
                                                  .ToListAsync();
    }

    public async Task<List<VideoDto>?> GetVideosByCategoryAsync(int categoryId, int page, int size)
    {
        var models = await _videoContext.Videos.Where(v => v.Category.Id.Equals(categoryId))
                                               .Skip(page * size)
                                               .Take(size)
                                               .OrderBy(v => v.Id)
                                               .Include(v => v.Category)
                                               .ToListAsync();
        return models.Select(v => new VideoDto(v.Id,
                                               v.Category.Id,
                                               v.UserId,
                                               _userContext.Users.Single(u => u.Id == v.UserId).Nickname,
                                               v.Title,
                                               v.UploadTime, 
                                               _qiniuService.DownloadTokenGenerator(v.CoverUrl, _qiniuOptions),
                                               _qiniuService.DownloadTokenGenerator(v.Url, _qiniuOptions),
                                               v.Likes))
                     .ToList();
    }
}