using System.ComponentModel.DataAnnotations.Schema;

namespace Tests.Domain.Models;

public class Test
{
    public string? Id { get; set; }

    public string? CreatorId { get; set; }
    public TestsUser? Creator { get; set; }

    public ICollection<TestsUser>? AssignedStudents { get; set; }
    public ICollection<TestResult>? TestResults { get; set; }
    public ICollection<Question>? Questions { get; set; }
}