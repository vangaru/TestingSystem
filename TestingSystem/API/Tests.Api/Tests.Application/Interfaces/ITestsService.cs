using Tests.Domain.Models;
using Question = Tests.Application.Models.Question;

namespace Tests.Application.Interfaces;

public interface ITestsService
{
    public Task Add(string testName, IEnumerable<Question> questions, string creatorName, 
        IEnumerable<string> assignedStudentNames);

    public Task<IEnumerable<Test>> GetCreatedTests(string creatorName);

    public Task<IEnumerable<Test>> GetAssignedTests(string assigneeName);

    public Task<TestResult?> GetAssigneeTestResult(string assigneeName, Test test);

    public Task<Test> GetByIdAndUserName(string testId, string assigneeName);

    public Task Delete(string testId, string creatorName);
}