using Project1024.Shared.Models;
using Project1024.Shared.Services;

namespace Project1024.Wasm.Services;

public class FakeVideoService : IVideoService
{
    public Task<List<VideoDto>?> GetVideosAsync(int page, int size)
    {
        var list = Enumerable.Range(page * size, size)
            .Select(i => new VideoDto(i, 0, 0, "尤克里里唱作教学", $"爷青结！《忘记时间》尤克里里弹唱 - {i}", DateTimeOffset.Now,
                                      "https://image1.pearvideo.com/cont/20211108/11905134-102749-1.png",
                                      "https://video.pearvideo.com/mp4/third/20211108/cont-1745429-11905134-102742-hd.mp4"))
            .ToList();
        return Task.FromResult(list)!;
    }
}
