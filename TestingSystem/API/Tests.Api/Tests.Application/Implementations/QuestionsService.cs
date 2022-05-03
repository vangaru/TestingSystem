using Tests.Application.Interfaces;
using Tests.Domain.Models;

namespace Tests.Application.Implementations;

public class QuestionsService : IQuestionsService
{
    public IEnumerable<Question> GetQuestionsForTest(Test test, IEnumerable<(string name, string expectedAnswer)> questions)
    {
        return questions.Select(question => 
            GetAssignedTestQuestion(test, question.name, question.expectedAnswer));
    }

    private Question GetAssignedTestQuestion(Test test, string name, string expectedAnswer)
    {
        var question = new Question
        {
            Id = Guid.NewGuid().ToString(),
            Name = name,
            ExpectedAnswer = expectedAnswer,
            TestId = test.Id,
            Test = test
        };

        return question;
    }
}