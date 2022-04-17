using Microsoft.AspNetCore.Identity;

namespace Tests.Domain.Models;

public class TestsUser : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
}