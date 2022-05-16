namespace Tests.Api.Models;

public class TakeQuestionModel
{
    public string? QuestionId { get; set; }
    public string? QuestionName { get; set; }
    public string? QuestionType { get; set; }
    public IEnumerable<TakeSelectableAnswerModel>? SelectableQuestions { get; set; }
}