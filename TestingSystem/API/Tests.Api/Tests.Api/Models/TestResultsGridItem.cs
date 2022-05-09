namespace Tests.Api.Models;

public class TestResultsGridItem
{
    public string? TestId { get; set; }
    public string? TestName { get; set; }
    public string? Status { get; set; }
    public string? StudentName { get; set; }
    public double Result { get; set; }
}