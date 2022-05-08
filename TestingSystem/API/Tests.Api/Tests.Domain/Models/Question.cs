namespace Tests.Domain.Models;

public class Question
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? ExpectedAnswer { get; set; }
    
    public string? TestId { get; set; }
    public Test? Test { get; set; }
    
    public string? QuestionType { get; set; }
    
    public ICollection<SelectableAnswer>? SelectableQuestionNames { get; set; }
}