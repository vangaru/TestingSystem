using Tests.Application.Interfaces;
using Tests.Domain.Models;

namespace Tests.Application.Implementations;

public class QuestionsService : IQuestionsService
{
    public IEnumerable<Question> GetQuestionsForTest(Test test, IEnumerable<string> expectedAnswers)
    {
        return expectedAnswers.Select(expectedAnswer => GetAssignedTestQuestion(test, expectedAnswer));
    }

    private Question GetAssignedTestQuestion(Test test, string expectedAnswer)
    {
        var question = new Question
        {
            Id = Guid.NewGuid().ToString(),
            ExpectedAnswer = expectedAnswer,
            TestId = test.Id,
            Test = test
        };

        return question;
    }
}