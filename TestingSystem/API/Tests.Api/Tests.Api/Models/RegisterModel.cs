using System.ComponentModel.DataAnnotations;

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
}