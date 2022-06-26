﻿using Microsoft.AspNetCore.Identity;

namespace iSpend.Infra.Data.Identity;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
}
