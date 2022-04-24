using Tests.Domain.Data;
using Tests.Domain.Interfaces;
using Tests.Domain.Models;

namespace Tests.Domain.Implementations;

public class QuestionAnswerRepository : IQuestionAnswerRepository, IDisposable
{
    private readonly TestsDbContext _dbContext;

    public QuestionAnswerRepository(TestsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(QuestionAnswer questionAnswer)
    {
        _dbContext.QuestionAnswers!.Add(questionAnswer);
        _dbContext.SaveChanges();
    }

    public void Delete(string id)
    {
        QuestionAnswer questionAnswer = Get(id);
        _dbContext.QuestionAnswers!.Remove(questionAnswer);
        _dbContext.SaveChanges();
    }

    public void Update(string id, QuestionAnswer questionAnswer)
    {
        questionAnswer.Id = id;
        _dbContext.QuestionAnswers!.Update(questionAnswer);
        _dbContext.SaveChanges();
    }

    public QuestionAnswer Get(string id)
    {
        QuestionAnswer questionAnswer = _dbContext.QuestionAnswers!.First(qa => qa.Id == id);
        return questionAnswer;
    }

    public IEnumerable<QuestionAnswer> Get()
    {
        return _dbContext.QuestionAnswers!;
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}