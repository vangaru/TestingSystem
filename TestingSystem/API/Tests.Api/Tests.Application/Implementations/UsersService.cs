using Microsoft.AspNetCore.Identity;
using Tests.Application.Interfaces;
using Tests.Domain.Models;

namespace Tests.Application.Implementations;

public class UsersService : IUsersService
{
    private readonly UserManager<TestsUser> _userManager;

    public UsersService(UserManager<TestsUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IEnumerable<TestsUser>> GetStudents()
    {
        List<TestsUser> users = _userManager.Users.ToList();
        string studentRole = UserRole.Student.ToString().ToLower();
        var students = new List<TestsUser>();
        foreach (TestsUser user in users)
        {
            if (await _userManager.IsInRoleAsync(user, studentRole))
            {
                students.Add(user);
            }
        }

        return students;
    }
}