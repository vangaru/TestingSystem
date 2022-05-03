using Tests.Api.Interfaces;
using Tests.Application.Interfaces;
using Tests.Domain.Models;

namespace Tests.Api.Implementations;

public class UsersInfoProvider : IUsersInfoProvider
{
    private readonly IUsersService _usersService;

    public UsersInfoProvider(IUsersService usersService)
    {
        _usersService = usersService;
    }

    public async Task<IEnumerable<string>> GetStudentNames()
    {
        IEnumerable<TestsUser> students = await _usersService.GetStudents();
        IEnumerable<string> studentNames = students.Select(s => s.UserName);
        return studentNames;
    }
}