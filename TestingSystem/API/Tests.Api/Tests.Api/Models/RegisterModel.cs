using System.ComponentModel.DataAnnotations;
using Tests.Domain.Models;

namespace Tests.Api.Models;

public class RegisterModel
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }
    
    [Required]
    public string? UserName { get; set; }

    [StringLength(20, MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [Compare("Password")]
    public string? ConfirmPassword { get; set; }

    public string? UserRole { get; set; }
}