using Tests.Domain.Models;

namespace Tests.Domain.Interfaces;

public interface ITestResultRepository
{
    public void Add(TestResult testResult);
    public void Delete(string id);
    public void Update(string id, TestResult testResult);
    public TestResult Get(string id);
    public IEnumerable<TestResult> Get();
}