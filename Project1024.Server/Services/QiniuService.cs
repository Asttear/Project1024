using Qiniu.Storage;
using Qiniu.Util;
using System.Diagnostics.CodeAnalysis;

namespace Project1024.Server.Services;

public class QiniuService
{
    /// <summary>
    /// 生成带有下载凭证的url
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    [return: NotNullIfNotNull(nameof(url))]
    public string? DownloadTokenGenerator(string? url, QiniuOptions qiniuOptions)
    {
        if (url is null) return null;
        string accessKey = qiniuOptions.AccessKey;
        string secretKey = qiniuOptions.SecretKey;
        string domain = qiniuOptions.Domain;
        Mac mac = new(accessKey, secretKey);
        return DownloadManager.CreatePrivateUrl(mac, domain, url, 3600);
    }
}
