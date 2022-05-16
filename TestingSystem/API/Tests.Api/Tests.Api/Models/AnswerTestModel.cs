namespace Tests.Api.Models;

public class AnswerTestModel
{
    public string? TestId { get; set; }
    public IEnumerable<AnswerQuestionModel>? QuestionAnswers { get; set; }
}