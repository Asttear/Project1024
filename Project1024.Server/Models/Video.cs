using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project1024.Server.Models;

/// <summary>
/// 视频实体类。
/// </summary>
[Table("video")]
public class Video
{
    public Video(int id, string title, string description, DateTimeOffset uploadTime, TimeSpan duration, int views, string url, string coverUrl, int userId, int likes)
    {
        Id = id;
        Title = title;
        Description = description;
        UploadTime = uploadTime;
        Duration = duration;
        Views = views;
        Url = url;
        CoverUrl = coverUrl;
        Category = null!;
        UserId = userId;
        Likes = likes;
    }

    [Key] public int Id { get; set; }
    [MaxLength(50)] public string Title { get; set; }
    [MaxLength(200)] public string Description { get; set; }

    public int UserId { get; set; }
    public int Likes { get; set; }
    public DateTimeOffset UploadTime { get; set; }
    public TimeSpan Duration { get; set; }
    public int Views { get; set; }
    [MaxLength(511)] public string Url { get; set; }
    [MaxLength(511)] public string CoverUrl { get; set; }
    public VideoCategory Category { get; set; }


    //视频点赞数
    //public int Likes { get; set; }

    //视频评论数
    //public int Comments { get; set; }

    //视频分享数
    //public int Shares { get; set; }

    //视频是否发布，0表示未发布，1表示已发布
    //[Column("is_published")]
    //public bool IsPublished { get; set; }

    ////视频上传用户id
    //[Column("publisher_id")]
    //[Required] public int PublisherId { get; set; }

}
