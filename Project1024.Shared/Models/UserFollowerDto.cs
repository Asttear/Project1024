﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1024.Shared.Models;

public record UserFollowerDto
(
    int FollowedId,
    int FollowerId
);

