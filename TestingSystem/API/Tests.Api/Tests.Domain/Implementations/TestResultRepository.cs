using Tests.Domain.Data;
using Tests.Domain.Interfaces;
using Tests.Domain.Models;

namespace Tests.Domain.Implementations;

public class TestResultRepository : ITestResultRepository
{
    private readonly TestsDbContext _dbContext;

    public TestResultRepository(TestsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(TestResult testResult)
    {
        _dbContext.TestResults!.Add(testResult);
        _dbContext.SaveChanges();
    }

    public void Delete(string id)
    {
        TestResult testResult = Get(id);
        _dbContext.TestResults!.Remove(testResult);
        _dbContext.SaveChanges();
    }

    public void Update(string id, TestResult testResult)
    {
        testResult.Id = id;
        _dbContext.TestResults!.Update(testResult);
        _dbContext.SaveChanges();
    }

    public TestResult Get(string id)
    {
        TestResult testResult = _dbContext.TestResults!.First(tr => tr.Id == id);
        return testResult;
    }

    public IEnumerable<TestResult> Get()
    {
        return _dbContext.TestResults!;
    }
}