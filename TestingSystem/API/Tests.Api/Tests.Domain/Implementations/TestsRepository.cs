using Microsoft.EntityFrameworkCore;
using Tests.Domain.Data;
using Tests.Domain.Interfaces;
using Tests.Domain.Models;

namespace Tests.Domain.Implementations;

public class TestsRepository : ITestsRepository, IDisposable
{
    private readonly TestsDbContext _dbContext;

    public TestsRepository(TestsDbContext dbContext)
    {
        _dbContext = dbContext; 
    }

    public void Add(Test test)
    {
        _dbContext.Tests!.Add(test);
        _dbContext.SaveChanges();
    }

    public void Delete(string id)
    {
        Test test = Get(id);
        _dbContext.Tests!.Remove(test);
        _dbContext.SaveChanges();
    }

    public void Update(string id, Test test)
    {
        test.Id = id;
        _dbContext.Tests!.Update(test);
        _dbContext.SaveChanges();
    }

    public Test Get(string id)
    {
        Test test = _dbContext.Tests!
            .Include(t => t.Creator)
            .Include(t => t.Questions)!
                .ThenInclude(q => q.SelectableQuestionNames)
            .Include(t => t.AssignedStudents)
            .Include(t => t.TestResults)!
                .ThenInclude(r => r.QuestionAnswers)
            .First(t => t.Id == id);
        return test;
    }

    public IEnumerable<Test> Get()
    {
        return _dbContext.Tests!
            .Include(t => t.Questions)!
                .ThenInclude(q => q.SelectableQuestionNames)
            .Include(t => t.AssignedStudents)
            .Include(t => t.TestResults)!
                .ThenInclude(r => r.QuestionAnswers)
            .Include(t => t.Creator);
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}