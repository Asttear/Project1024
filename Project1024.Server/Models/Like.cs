using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project1024.Server.Models;

[Table("like")]
public class Like
{
    //public Like(int userId, int type, int targetId, DateTimeOffset likeDate)
    //{
    //    UserId = userId;
    //    Type = type;
    //    TargetId = targetId;
    //    LikeDate = likeDate;
    //}


    /// <summary>
    /// 主键。
    /// </summary>
    [Key] public int Id { get; set; }
    /// <summary>
    /// 点赞用户id。
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// 目标类型。
    /// </summary>
    public int Type { get; set; }

    /// <summary>
    /// 目标id。
    /// </summary>
    public int TargetId {  get; set; }

    /// <summary>
    /// 点赞日期时间。
    /// </summary>
    public DateTimeOffset LikeDate { get; set; }
}
