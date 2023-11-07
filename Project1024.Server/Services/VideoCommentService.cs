using Microsoft.Extensions.Options;
using Project1024.Server.Data;
using Project1024.Shared.Models;

namespace Project1024.Server.Services;

public class VideoCommentService
{

    private readonly VideoContext _videoContext;
    private readonly QiniuOptions _qiniuOptions;
    private readonly QiniuService _qiniuService;

    public VideoCommentService(VideoContext videoContext, IOptions<QiniuOptions> options, QiniuService qiniuService)
    {
        _videoContext = videoContext;
        _qiniuOptions = options.Value;
        _qiniuService = qiniuService;
    }
    public IEnumerable<CommentDto> GetCommentList(int videoId, int page, int size)
    {
        throw new NotImplementedException();

    }
}
