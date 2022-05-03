using Tests.Domain.Models;

namespace Tests.Application.Interfaces;

public interface ITestsService
{
    public Task Add(string testName, IEnumerable<(string name, string expectedAnswer)> questions, 
        string creatorName, IEnumerable<string> assignedStudentNames);

    public Task<IEnumerable<Test>> GetCreatedTests(string creatorName);

    public Test GetById(string testId);

    public Task Delete(string testId, string creatorName);
}