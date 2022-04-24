using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Tests.Domain.Models;

public class TestsUser : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    
    [InverseProperty("Creator")]
    public ICollection<Test>? OwnedTests { get; set; }
    
    [InverseProperty("AssignedStudents")]
    public ICollection<Test>? AssignedTests { get; set; }
    
    public ICollection<TestResult>? TestResults { get; set; }
}