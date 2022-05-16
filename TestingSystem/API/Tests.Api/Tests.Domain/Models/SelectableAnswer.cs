namespace Tests.Domain.Models;

public class SelectableAnswer
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public int Index { get; set; }
    
    public string? QuestionId { get; set; }
    public Question? Question { get; set; }
}