namespace Tests.Api.Models;

public class TakeTestModel
{
    public string? TestId { get; set; }
    public string? TestName { get; set; }
    public IEnumerable<TakeQuestionModel> Questions { get; set; }
}