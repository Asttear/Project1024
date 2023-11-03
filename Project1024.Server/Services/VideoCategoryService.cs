using Microsoft.Extensions.Options;
using Project1024.Server.Data;
using Project1024.Shared.Models;

namespace Project1024.Server.Services;

public class VideoCategoryService
{
    private readonly VideoContext _videoContext;
    private readonly QiniuOptions _qiniuOptions;
    private readonly QiniuService _qiniuService;

    public VideoCategoryService(VideoContext videoContext, IOptions<QiniuOptions> options, QiniuService qiniuService)
    {
        _videoContext = videoContext;
        _qiniuOptions = options.Value;
        _qiniuService = qiniuService;
    }

    public IEnumerable<VideoCategoryDto> GetVideoCategoryList()
    {
        return _videoContext.VideoCategories
           .OrderBy(v => v.Id)
           .Select(c => new VideoCategoryDto(c.Id, c.Name));
    }

    public IEnumerable<VideoDto> GetVideoListByCategoryId(int categotyId, int page, int size)
    {
        return _videoContext.Videos
           .Where(v => v.Category.Id.Equals(categotyId))
           .Skip(page * size)
           .Take(size)
           .OrderBy(v => v.Id)
           .Select(v => new VideoDto(v.Id, v.Category.Id, v.Title, v.UploadTime, _qiniuService.DownloadTokenGenerator(v.CoverUrl, _qiniuOptions), _qiniuService.DownloadTokenGenerator(v.Url, _qiniuOptions)));
    }
}
