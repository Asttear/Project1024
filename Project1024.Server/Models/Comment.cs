using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project1024.Server.Models;

[Table("comment")]
public class Comment
{

    /// <summary>
    /// 一级评论id。
    /// </summary>
    [Key] public int Id { get; set; }

    /// <summary>
    /// 视频id。
    /// </summary>
    public int VideoId { get; set; }

    /// <summary>
    /// 父评论。
    /// </summary>
    public int ParentId { get; set; }

    /// <summary>
    /// 发布评论的用户id。
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// 评论内容。
    /// </summary>
    [MaxLength(1000)] public required string Content { get; set; }

    /// <summary>
    /// 点赞数。
    /// </summary>
    [DefaultValue(0)] public int LikeCount { get; set; }

    /// <summary>
    /// 是否删除。
    /// </summary>
    [DefaultValue(0)] public int IsDeleted { get; set; }

    /// <summary>
    /// 创建时间。
    /// </summary>
    [DefaultValue(0)] public int CreatedTime { get; set; }
}
