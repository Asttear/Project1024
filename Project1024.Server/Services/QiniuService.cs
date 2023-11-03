using Qiniu.Storage;
using Qiniu.Util;

namespace Project1024.Server.Services;

public class QiniuService
{
    /// <summary>
    /// 生成带有下载凭证的url
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public string DownloadTokenGenerator(string url, QiniuOptions qiniuOptions)
    {
        string accessKey = qiniuOptions.AccessKey;
        string secretKey = qiniuOptions.SecretKey;
        string domain = qiniuOptions.Domain;
        Mac mac = new(accessKey, secretKey);
        return DownloadManager.CreatePrivateUrl(mac, domain, url, 3600);
    }
}
