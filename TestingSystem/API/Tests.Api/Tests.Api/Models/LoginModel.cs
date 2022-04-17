using System.ComponentModel.DataAnnotations;

namespace Tests.Api.Models;

public class LoginModel
{
    [Required]
    public string? UserName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
}