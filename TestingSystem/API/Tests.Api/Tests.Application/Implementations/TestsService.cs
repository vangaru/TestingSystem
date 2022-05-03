using Microsoft.AspNetCore.Identity;
using Tests.Application.Interfaces;
using Tests.Domain.Interfaces;
using Tests.Domain.Models;

namespace Tests.Application.Implementations;

public class TestsService : ITestsService
{
    private readonly ITestsRepository _testsRepository;
    private readonly IQuestionsService _questionService;
    private readonly UserManager<TestsUser> _userManager;

    public TestsService(
        ITestsRepository testsRepository, 
        UserManager<TestsUser> userManager, 
        IQuestionsService questionService)
    {
        _testsRepository = testsRepository;
        _userManager = userManager;
        _questionService = questionService;
    }

    public async Task Add(IEnumerable<string> expectedAnswers, string creatorName, IEnumerable<string> assignedStudentNames)
    {
        string testId = Guid.NewGuid().ToString();
        TestsUser creator = await GetTestCreator(creatorName);
        List<TestsUser> assignedStudents = await GetAssignedStudents(assignedStudentNames);

        var test = new Test
        {
            Id = testId,
            AssignedStudents = assignedStudents,
            CreatorId = creator.Id,
            Creator = creator,
            TestResults = new List<TestResult>()
        };

        List<Question> assignedQuestions = _questionService.GetQuestionsForTest(test, expectedAnswers).ToList();
        test.Questions = assignedQuestions;

        _testsRepository.Add(test);
    }

    private async Task<TestsUser> GetTestCreator(string creatorName)
    {
        TestsUser creator = await _userManager.FindByNameAsync(creatorName);
        if (creator == null)
        {
            throw new ApplicationException($"Cannot find user with name ${creatorName}");
        }

        return creator;
    }

    private async Task<List<TestsUser>> GetAssignedStudents(IEnumerable<string> assignedStudentNames)
    {
        var assignedStudents = new List<TestsUser>();
        foreach (string studentName in assignedStudentNames)
        {
            TestsUser student = await _userManager.FindByNameAsync(studentName);
            if (student == null)
            {
                throw new ApplicationException($"Cannot find user with name ${studentName}");
            }
            assignedStudents.Add(student);
        }

        return assignedStudents;
    }
}