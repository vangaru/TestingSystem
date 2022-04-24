using Tests.Domain.Models;

namespace Tests.Domain.Interfaces;

public interface IQuestionAnswerRepository
{
    public void Add(QuestionAnswer questionAnswer);
    public void Delete(string id);
    public void Update(string id, QuestionAnswer questionAnswer);
    public QuestionAnswer Get(string id);
    public IEnumerable<QuestionAnswer> Get();
}