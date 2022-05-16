using Tests.Domain.Models;

namespace Tests.Application.Interfaces;

public interface IQuestionsService
{
    public IEnumerable<Question> GetQuestionsForTest(Test test, IEnumerable<Application.Models.Question> questions);
    
    public IEnumerable<QuestionAnswer> GetQuestionAnswers(IEnumerable<(string questionId, string answer)> questionAnswers);
}