using Project1024.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1024.Shared.Requests;

public class RegisterRequest
{
    [MinLength(5)]
    public string UserName { get; set; } = null!;

    [MinLength(6)]
    public string Password { get; set; } = null!;

    public int Age { get; set; }
}
