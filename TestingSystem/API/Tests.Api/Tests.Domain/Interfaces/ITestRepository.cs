using Tests.Domain.Models;

namespace Tests.Domain.Interfaces;

public interface ITestRepository
{
    public void Add(Test test);
    public void Delete(string id);
    public void Update(string id, Test test);
    public Test Get(string id);
    public IEnumerable<Test> Get();
}