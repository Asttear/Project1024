﻿namespace Project1024.Server.Settings;

public class JwtSettings
{
    public string SecurityKey { get; set; } = "";
    public TimeSpan ExpiresIn { get; set; }
}
