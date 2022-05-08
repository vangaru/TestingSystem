using Tests.Application.Interfaces;
using Tests.Domain.Models;

namespace Tests.Application.Implementations;

public class QuestionsService : IQuestionsService
{
    public IEnumerable<Question> GetQuestionsForTest(Test test, IEnumerable<Application.Models.Question> questions)
    {
        return questions.Select(question => 
            GetAssignedTestQuestion(test, question));
    }

    private Question GetAssignedTestQuestion(Test test, Application.Models.Question question)
    {
        return new Question
        {
            Id = Guid.NewGuid().ToString(),
            Name = question.Name,
            ExpectedAnswer = question.ExpectedAnswer,
            TestId = test.Id,
            Test = test,
            QuestionType = question.Type.ToString(),
            SelectableQuestionNames = GetSelectableQuestionNames(question).ToList()
        };
    }

    private IEnumerable<SelectableAnswer> GetSelectableQuestionNames(Application.Models.Question question)
    {
        return question.SelectableQuestionNames == null 
            ? new List<SelectableAnswer>() 
            : question.SelectableQuestionNames.Select(q => new SelectableAnswer
            {
                Id = Guid.NewGuid().ToString(),
                Name = q
            });
    }
}