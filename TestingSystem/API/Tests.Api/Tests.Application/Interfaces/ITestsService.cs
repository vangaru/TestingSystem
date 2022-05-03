using Tests.Domain.Models;

namespace Tests.Application.Interfaces;

public interface ITestsService
{
    public Task Add(Test test, string creatorName, IEnumerable<string> assignedStudentNames);
}