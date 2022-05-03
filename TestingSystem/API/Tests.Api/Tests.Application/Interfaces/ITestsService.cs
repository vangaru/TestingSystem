using Tests.Domain.Models;

namespace Tests.Application.Interfaces;

public interface ITestsService
{
    public Task Add(IEnumerable<string> expectedAnswers, string creatorName, IEnumerable<string> assignedStudentNames);
}