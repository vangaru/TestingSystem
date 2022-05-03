using System.ComponentModel.DataAnnotations;
using Tests.Domain.Models;

namespace Tests.Api.Models;

public class CreateTestModel
{
    [Required]
    public string? CreatorName { get; set; }
    
    [Required]
    public IEnumerable<string>? AssignedStudentNames { get; set; }
    
    [Required]
    public Test? Test { get; set; }
}