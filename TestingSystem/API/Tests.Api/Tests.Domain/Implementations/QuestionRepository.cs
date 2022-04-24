using Tests.Domain.Data;
using Tests.Domain.Interfaces;
using Tests.Domain.Models;

namespace Tests.Domain.Implementations;

public class QuestionRepository : IQuestionRepository
{
    private readonly TestsDbContext _dbContext;
    
    public QuestionRepository(TestsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(Question question)
    {
        _dbContext.Questions!.Add(question);
        _dbContext.SaveChanges();
    }

    public void Delete(string id)
    {
        Question question = Get(id);
        _dbContext.Questions!.Remove(question);
        _dbContext.SaveChanges();
    }

    public void Update(string id, Question question)
    {
        question.Id = id;
        _dbContext.Update(question);
        _dbContext.SaveChanges();
    }

    public Question Get(string id)
    {
        Question question = _dbContext.Questions!.First(q => q.Id == id);
        return question;
    }

    public IEnumerable<Question> Get()
    {
        return _dbContext.Questions!;
    }
}