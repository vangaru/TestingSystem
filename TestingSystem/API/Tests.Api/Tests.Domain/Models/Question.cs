namespace Tests.Domain.Models;

public class Question
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? ExpectedAnswer { get; set; }
    
    public string? TestId { get; set; }
    public Test? Test { get; set; }
}