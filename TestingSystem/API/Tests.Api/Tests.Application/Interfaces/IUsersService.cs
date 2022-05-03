using Tests.Domain.Models;

namespace Tests.Application.Interfaces;

public interface IUsersService
{
    public Task<IEnumerable<TestsUser>> GetStudents();
}