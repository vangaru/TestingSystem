using System.ComponentModel.DataAnnotations;

namespace Tests.Api.Models;

public class CreateTestModel
{
    [Required] 
    public string? TestName { get; set; }
    
    [Required]
    public IEnumerable<string>? AssignedStudentNames { get; set; }
    
    [Required]
    public IEnumerable<CreateQuestionModel>? Questions { get; set; }
}