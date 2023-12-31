﻿namespace Project1024.Shared.Models;

public record VideoDto
(
    int Id,
    int CategoryId,
    int AuthorId,
    string? AuthorName,
    string Title,
    DateTimeOffset CreatedTime,
    string CoverUrl,
    string VideoUrl,
    int Likes
);