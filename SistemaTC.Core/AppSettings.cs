﻿namespace SistemaTC.Core;
public class AppSettings
{
    public string TCConnection { get; set; } = string.Empty;
    public MailSettings Mail { get; set; } = new();
    public JwtTokenSettings JwtTokenSettings { get; set; } = new();
}

public class MailSettings
{
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class JwtTokenSettings
{
    public string SymmetricSecurityKey { get; set; } = default!;
    public string JwtRegisteredClaimNamesSub { get; set; } = default!;
}