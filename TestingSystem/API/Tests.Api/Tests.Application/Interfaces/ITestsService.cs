using Tests.Domain.Models;
using Question = Tests.Application.Models.Question;

namespace Tests.Application.Interfaces;

public interface ITestsService
{
    public Task Add(string testName, IEnumerable<Question> questions, string creatorName, 
        IEnumerable<string> assignedStudentNames);

    public Task<IEnumerable<Test>> GetCreatedTests(string creatorName);

    public Test GetById(string testId);

    public Task Delete(string testId, string creatorName);
}