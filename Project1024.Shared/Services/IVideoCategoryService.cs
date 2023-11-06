using Project1024.Shared.Models;

namespace Project1024.Shared.Services;

public interface IVideoCategoryService
{
    Task<List<VideoCategoryDto>?> GetVideoCategoriesAsync();
    Task<List<VideoDto>?> GetVideosByCategoryAsync(int categoryId, int page, int size);
}