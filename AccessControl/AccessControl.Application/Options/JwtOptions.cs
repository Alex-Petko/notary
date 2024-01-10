﻿using System.Diagnostics.CodeAnalysis;

namespace AccessControl.Application;

[ExcludeFromCodeCoverage]
public class JwtOptions
{
    public virtual string Key { get; set; } = null!;
    public virtual int ExpiresMinutes { get; set; }
}