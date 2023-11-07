using Project1024.Server.Data;
using Project1024.Server.Models;

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
        List<Like> likes = _videoContext.Likes.Where(l => l.UserId == userId && l.TargetId == id && l.Type == 1).ToList();
        if (likes.Count > 0) return false;
        _videoContext.Likes.Add(new Like ()
        {
            UserId = userId,
            Type = 1,
            TargetId = id,
            LikeDate = DateTime.UtcNow,
        });
        var video = _videoContext.Videos.Single(l => l.Id == id);
        video.Likes ++;
        _videoContext.SaveChanges();
        return true;
    }

    public bool VideoUnLike(int userId, int id)
    {
        List<Like> likes = _videoContext.Likes.Where(l => l.UserId == userId && l.TargetId == id && l.Type == 1).ToList();
        if (likes.Count == 0) return false;
        _videoContext.Likes.Remove(_videoContext.Likes.Single(l => l.UserId == userId && l.TargetId == id && l.Type == 1));
        var video = _videoContext.Videos.Single(l => l.Id == id);
        video.Likes --;
        _videoContext.SaveChanges();
        return true;
    }
}
