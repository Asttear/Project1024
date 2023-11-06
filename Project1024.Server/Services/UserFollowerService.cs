using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Project1024.Server.Data;
using Project1024.Server.Models;
using Project1024.Shared.Models;
using System.Reflection.Metadata;

namespace Project1024.Server.Services;

public class UserFollowerService
{
    private readonly VideoContext _videoContext;
    private readonly UserContext _userContext;
    private readonly QiniuOptions _qiniuOptions;
    private readonly QiniuService _qiniuService;

    public UserFollowerService(VideoContext videoContext, IOptions<QiniuOptions> options, QiniuService qiniuService, UserContext userContext)
    {
        _videoContext = videoContext;
        _qiniuOptions = options.Value;
        _qiniuService = qiniuService;
        _userContext = userContext;
    }
    public IEnumerable<UserDto> GetFanList(int id, int page, int size)
    {
        List<int> followerIdList = _videoContext.UserFollowers.Where(f => f.FollowedId == id).Select(f => f.FollowerId).ToList();
        return _userContext.Users.Where(u => followerIdList.Contains(u.Id))
                                 .Select(u => new UserDto(u.Id, u.Nickname, _qiniuService.DownloadTokenGenerator(u.AvatarUrl, _qiniuOptions), u.Signature));
    }

    public IEnumerable<UserDto> GetFollowerList(int id, int page, int size)
    {
        List<int> followedIdList = _videoContext.UserFollowers.Where(f => f.FollowerId == id).Select(f => f.FollowedId).ToList();
        return _userContext.Users.Where(u => followedIdList.Contains(u.Id))
                                 .Select(u => new UserDto(u.Id, u.Nickname, _qiniuService.DownloadTokenGenerator(u.AvatarUrl, _qiniuOptions), u.Signature));
    }

    public bool Follow(int followedId, int followerId)
    {
        List<UserFollower> userFollowers = _videoContext.UserFollowers.Where(f => f.FollowerId == followerId && f.FollowedId == followedId).ToList();
        if (userFollowers.Count > 0) return false;
        _videoContext.UserFollowers.Add(new UserFollower()
        {
            FollowedId = followedId,
            FollowerId = followerId,
            FollowedTime = DateTimeOffset.UtcNow,
            IsDeleted = 0
        });
        _videoContext.SaveChanges();
        return true;

    }

    public bool UnFollow(int followedId, int followerId)
    {
        List<UserFollower> userFollowers = _videoContext.UserFollowers.Where(f => f.FollowerId == followerId && f.FollowedId == followedId).ToList();
        if (userFollowers.Count == 0) return false;
        _videoContext.UserFollowers.Remove(_videoContext.UserFollowers.Single(f => f.FollowedId == followedId && f.FollowerId == followerId));
        _videoContext.SaveChanges();
        return true;
    }
}
