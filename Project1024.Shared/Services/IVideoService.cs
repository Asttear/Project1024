using Project1024.Shared.Models;

namespace Project1024.Shared.Services;

public interface IVideoService
{
    Task<List<VideoDto>?> GetVideosAsync(int page, int size);
}