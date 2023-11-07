using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project1024.Server.Models;

[Table("user_follower")]
public class UserFollower
{

    [Key] public int Id { get; set; }
    /// <summary>
    /// 关注者id。
    /// </summary>
    public int FollowerId { get; set; }
    /// <summary>
    /// 被关注者id。
    /// </summary>
    public int FollowedId { get; set; }

    //关注时间。
    public DateTimeOffset FollowedTime { get; set; }

    //是否取关。
    [Column(TypeName = "tinyint(4)")] public int IsDeleted { get; set; }
}
