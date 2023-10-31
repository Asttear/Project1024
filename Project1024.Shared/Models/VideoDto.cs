namespace Project1024.Shared.Models;

public record VideoDto
(
    int Id,
    int CategoryId,
    string Title,
    DateTimeOffset CreatedTime,
    string CoverUrl,
    string VideoUrl
);