using Tests.Domain.Models;

namespace Tests.Application.Interfaces;

public interface IQuestionsService
{
    public IEnumerable<Question> GetQuestionsForTest(Test test, IEnumerable<string> expectedAnswers);
}