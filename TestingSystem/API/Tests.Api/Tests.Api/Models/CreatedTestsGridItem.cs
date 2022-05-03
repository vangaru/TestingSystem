namespace Tests.Api.Models;

public class CreatedTestsGridItem
{
    public string? TestId { get; set; }
    public string? TestName { get; set; }
    public int QuestionsCount { get; set; }
    public int ResultsCount { get; set; }
    public int AssignedStudentsCount { get; set; }
}