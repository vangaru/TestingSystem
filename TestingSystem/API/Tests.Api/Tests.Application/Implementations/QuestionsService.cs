using Tests.Application.Interfaces;
using Tests.Domain.Implementations;
using Tests.Domain.Interfaces;
using Tests.Domain.Models;

namespace Tests.Application.Implementations;

public class QuestionsService : IQuestionsService
{
    private readonly IQuestionsRepository _questionsRepository;

    public QuestionsService(IQuestionsRepository questionsRepository)
    {
        _questionsRepository = questionsRepository;
    }

    public IEnumerable<Question> GetQuestionsForTest(Test test, IEnumerable<Application.Models.Question> questions)
    {
        return questions.Select(question => GetAssignedTestQuestion(test, question));
    }

    public IEnumerable<QuestionAnswer> GetQuestionAnswers(IEnumerable<(string questionId, string answer)> questionAnswers)
    {
        var answers = new List<QuestionAnswer>();
        foreach ((string questionId, string answer) questionAnswer in questionAnswers)
        {
            Question question = _questionsRepository.Get(questionAnswer.questionId);
            var answer = new QuestionAnswer
            {
                Id = Guid.NewGuid().ToString(),
                ActualAnswer = questionAnswer.answer,
                Question = question,
                QuestionId = questionAnswer.questionId
            };
            answers.Add(answer);
        }

        return answers;
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
        return question.SelectableAnswers == null 
            ? new List<SelectableAnswer>() 
            : question.SelectableAnswers.Select(q => new SelectableAnswer
            {
                Id = Guid.NewGuid().ToString(),
                Name = q.Name,
                Index = q.Index
            });
    }
}