using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Project1024.Server.Data;
using Project1024.Shared.Models;
using Qiniu.Storage;
using Qiniu.Util;

namespace Project1024.Server.Services;

public class VideoService
{
    private readonly VideoContext _videoContext;
    private readonly UserContext _userContext;
    private readonly QiniuOptions _qiniuOptions;
    private readonly QiniuService _qiniuService;

    public VideoService(VideoContext videoContext, IOptions<QiniuOptions> options, QiniuService qiniuService, UserContext userContext)
    {
        _videoContext = videoContext;
        _qiniuOptions = options.Value;
        _qiniuService = qiniuService;
        _userContext = userContext;
    }

    public IEnumerable<VideoDto> GetVideoList(int page, int size)
    {
        return _videoContext.Videos
           .Skip(page * size)
           .Take(size)
           .OrderBy(v => v.Id)
           .Select(v => new VideoDto(v.Id,
                                     v.Category.Id,
                                     v.UserId,
                                     _userContext.Users.Where(u => u.Id == v.UserId).Single().Nickname ?? "",
                                     v.Title,
                                     v.UploadTime,
                                     _qiniuService.DownloadTokenGenerator(v.CoverUrl, _qiniuOptions),
                                     _qiniuService.DownloadTokenGenerator(v.Url, _qiniuOptions)));
    }

    public IEnumerable<VideoCategoryDto> GetVideoCategoryList()
    {
        return _videoContext.VideoCategories
            .Select(c => new VideoCategoryDto(c.Id, c.Name));
    }

    //public IEnumerable<VideoDto> GetVideoListByCategory(int page, int size, int categoryId)
    //{
    //    return _videoContext.Videos
    //       .Where(v => v.Url.Contains("dotnet"))
    //       .Skip(page * size)
    //       .Take(size)
    //       .OrderBy(v => v.Id)
    //       .Select(v => new VideoDto(v.Id, v.Category.Id, v.Title, v.UploadTime, _qiniuService.DownloadTokenGenerator(v.CoverUrl, _qiniuOptions), _qiniuService.DownloadTokenGenerator(v.Url, _qiniuOptions)));
    //}

    ///// <summary>
    ///// 生成带有下载凭证的url
    ///// </summary>
    ///// <param name="url"></param>
    ///// <returns></returns>
    //private static string UrlTokenGenerator(string url, QiniuOptions qiniuOptions)
    //{
    //    string accessKey = qiniuOptions.AccessKey;
    //    string secretKey = qiniuOptions.SecretKey;
    //    string domain = qiniuOptions.Domain;
    //    Mac mac = new(accessKey, secretKey);
    //    return DownloadManager.CreatePrivateUrl(mac, domain, url, 3600);
    //}
}
