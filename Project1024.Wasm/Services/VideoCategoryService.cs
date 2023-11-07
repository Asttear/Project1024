using System.Net.Http.Json;
using Project1024.Shared.Models;
using Project1024.Shared.Services;

namespace Project1024.Wasm.Services;

public class VideoCategoryService : IVideoCategoryService
{
    private readonly HttpClient _httpClient;
    private readonly Lazy<Task<List<VideoCategoryDto>?>> _videoCategories;

    public VideoCategoryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _videoCategories = new(async () => await _httpClient.GetFromJsonAsync<List<VideoCategoryDto>>("api/VideoCategory"));
    }

    public async Task<List<VideoCategoryDto>?> GetVideoCategoriesAsync()
    {
        return await _videoCategories.Value;
    }

    public async Task<List<VideoDto>?> GetVideosByCategoryAsync(int categoryId, int page, int size)
    {
        return await _httpClient.GetFromJsonAsync<List<VideoDto>>($"api/VideoCategory/{categoryId}?page={page}&size={size}");
    }
}