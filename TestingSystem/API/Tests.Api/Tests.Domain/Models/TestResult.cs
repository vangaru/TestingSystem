namespace Tests.Domain.Models;

public class TestResult
{
    public string? Id { get; set; }
    public string? Status { get; set; }

    public string? TestId { get; set; }
    public Test? Test { get; set; }

    public string? StudentId { get; set; }
    public TestsUser? Student { get; set; }

    public ICollection<QuestionAnswer>? QuestionAnswers { get; set; }
}