using Tests.Application.Models;

namespace Tests.Api.Models;

public class AssignedTestsGridItem
{
    public string? TestId { get; set; }
    public string? TestName { get; set; }
    public string? TeacherName { get; set; }
    public string Status { get; set; } = TestStatus.Assigned.ToString();
    public int QuestionsCount { get; set; }
    public double Results { get; set; }
}