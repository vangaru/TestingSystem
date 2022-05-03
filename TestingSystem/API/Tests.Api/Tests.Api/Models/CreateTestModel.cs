using System.ComponentModel.DataAnnotations;

namespace Tests.Api.Models;

public class CreateTestModel
{
    [Required]
    public string? CreatorName { get; set; }
    
    [Required]
    public IEnumerable<string>? AssignedStudentNames { get; set; }
    
    [Required]
    public IEnumerable<string>? ExpectedAnswers { get; set; }
}