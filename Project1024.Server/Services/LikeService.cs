using Project1024.Server.Data;
using Project1024.Server.Models;
using Project1024.Shared.Models;

namespace Project1024.Server.Services;

public class LikeService
{
    private readonly VideoContext _videoContext;

    public LikeService(VideoContext videoContext)
    {
        _videoContext = videoContext;
    }

    public bool VideoLike(int userId, int id)
    {
        List<Like> likes = _videoContext.Likes.Where(l => l.UserId == userId && l.TargetId == id).ToList();
        if (likes.Count > 0) return false;
        _videoContext.Likes.Add(new Like ()
        {
            UserId = userId,
            Type = 1,
            TargetId = id,
            LikeDate = DateTime.UtcNow,
        });
        _videoContext.SaveChanges();
        return true;

    }
}
