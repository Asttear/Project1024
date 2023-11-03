using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project1024.Server.Models;

public class User : IdentityUser<int>
{
    [StringLength(3)]
    public int Age {  get; set; }
}
