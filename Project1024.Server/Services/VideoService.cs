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
    private readonly QiniuOptions _qiniuOptions;

    public VideoService(VideoContext videoContext, IOptions<QiniuOptions> options)
    {
        _videoContext = videoContext;
        _qiniuOptions = options.Value;
    }

    public IEnumerable<VideoDto> GetVideoList(int page, int size)
    {
        return _videoContext.Videos
           .Skip(page * size)
           .Take(size)
           .OrderBy(v => v.Id)
           .Select(v => new VideoDto(v.Id, v.Category.Id, v.Title, v.UploadTime, UrlTokenGenerator(v.CoverUrl, _qiniuOptions), UrlTokenGenerator(v.Url, _qiniuOptions)));
    }

    /// <summary>
    /// 生成带有下载凭证的url
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    private static string UrlTokenGenerator(string url, QiniuOptions qiniuOptions)
    {
        string accessKey = qiniuOptions.AccessKey;
        string secretKey = qiniuOptions.SecretKey;
        string domain = qiniuOptions.Domain;
        Mac mac = new(accessKey, secretKey);
        return DownloadManager.CreatePrivateUrl(mac, domain, url, 3600);
    }
}
