using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1024.Shared.Models;

public record UserDto
(
    int Id,
    string Nickname,
    string AvatarUrl,
    string Signature
);
