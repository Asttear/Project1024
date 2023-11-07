using System.Net.Http.Json;
using Project1024.Shared.Models;
using Project1024.Shared.Services;

namespace Project1024.Wasm.Services;

public class VideoService : IVideoService
{
    private readonly HttpClient _httpClient;

    public VideoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<VideoDto>?> GetVideosAsync(int page, int size)
    {
        return await _httpClient.GetFromJsonAsync<List<VideoDto>>($"api/Video?page={page}&size={size}");
    }
}    
