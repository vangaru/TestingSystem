using Tests.Domain.Data;
using Tests.Domain.Interfaces;
using Tests.Domain.Models;

namespace Tests.Domain.Implementations;

public class TestRepository : ITestRepository, IDisposable
{
    private readonly TestsDbContext _dbContext;

    public TestRepository(TestsDbContext dbContext)
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
        Test test = _dbContext.Tests!.First(t => t.Id == id);
        return test;
    }

    public IEnumerable<Test> Get()
    {
        return _dbContext.Tests!;
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}