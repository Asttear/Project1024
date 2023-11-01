using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project1024.Server.Models;

[Table("video_category")]
public class VideoCategory
{
    public VideoCategory(int id, string name)
    {
        Id = id;
        Name = name;
        Videos = new();
    }

    [Key] public int Id { get; set; }
    [MaxLength(10)] public string Name { get; set; }

    public List<Video> Videos { get; }
}
