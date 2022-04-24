namespace Tests.Domain.Models;

public class QuestionAnswer
{
    public string? Id { get; set; }
    public string? ActualAnswer { get; set; }

    public string? TestResultId { get; set; }
    public TestResult? TestResult { get; set; }

    public string? QuestionId { get; set; }
    public Question? Question { get; set; }
}