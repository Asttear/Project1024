using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project1024.Server.Models;

public class User : IdentityUser<int>
{
    /// <summary>
    /// 年龄
    /// </summary>
    [StringLength(3)]
    public int Age { get; set; }
    /// <summary>
    /// 头像url
    /// </summary>
    [MaxLength(511)] public string? AvatarUrl { get; set; }
    /// <summary>
    /// 个性签名
    /// </summary>
    [MaxLength(255)] public string Signature { get; set; } = "这个人很懒，什么都没有写";

    /// <summary>
    /// 昵称
    /// </summary>
    [MaxLength(25)] public string Nickname { get; set; } = "";
}
