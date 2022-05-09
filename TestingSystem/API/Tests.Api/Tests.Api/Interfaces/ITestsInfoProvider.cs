using Tests.Api.Models;

namespace Tests.Api.Interfaces;

public interface ITestsInfoProvider
{
    public Task CreateTest(CreateTestModel createTestModel, string testCreatorName);
    public Task<IEnumerable<CreatedTestsGridItem>> GetCreatedTestsInfo(string testCreatorName);
    public Task<IEnumerable<AssignedTestsGridItem>> GetAssignedTestsInfo(string assigneeName);
    public Task<IEnumerable<TestResultsGridItem>> GetCreatedTestResults(string testId, string testCreatorName);
    public Task DeleteTest(string testId, string testCreatorName);
}