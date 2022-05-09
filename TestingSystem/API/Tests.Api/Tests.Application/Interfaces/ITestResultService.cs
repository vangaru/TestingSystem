using Tests.Domain.Models;

namespace Tests.Application.Interfaces;

public interface ITestResultService
{
    public double CalculateResults(IEnumerable<QuestionAnswer> questionAnswers);
    public IEnumerable<TestResult> InitializeTestResultsForAssignees(IEnumerable<TestsUser> assignees, Test test);
    public Task<IEnumerable<TestResult>> GetCreatedTestResults(string testId, string testCreatorName);
}