using Tests.Domain.Models;

namespace Tests.Domain.Interfaces;

public interface IQuestionRepository
{
    public void Add(Question question);
    public void Delete(string id);
    public void Update(string id, Question question);
    public Question Get(string id);
    public IEnumerable<Question> Get();
}