namespace Tests.Api.Models;

public class CreateQuestionModel
{
    public string? QuestionName { get; set; }
    public string? ExpectedAnswer { get; set; }
    public string? QuestionType { get; set; }
    public IEnumerable<CreateSelectableAnswerModel>? SelectableAnswers { get; set; }
}