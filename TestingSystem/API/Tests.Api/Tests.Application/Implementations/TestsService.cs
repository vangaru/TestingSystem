using Microsoft.AspNetCore.Identity;
using Tests.Application.Interfaces;
using Tests.Domain.Interfaces;
using Tests.Domain.Models;

namespace Tests.Application.Implementations;

public class TestsService : ITestsService
{
    private readonly ITestsRepository _testsRepository;
    private readonly IQuestionsService _questionService;
    private readonly ITestResultService _testResultService;
    private readonly UserManager<TestsUser> _userManager;

    public TestsService(
        ITestsRepository testsRepository, 
        UserManager<TestsUser> userManager, 
        IQuestionsService questionService, 
        ITestResultService testResultService)
    {
        _testsRepository = testsRepository;
        _userManager = userManager;
        _questionService = questionService;
        _testResultService = testResultService;
    }

    public async Task Add(string testName, IEnumerable<Application.Models.Question> questions, string creatorName, IEnumerable<string> assignedStudentNames)
    {
        string testId = Guid.NewGuid().ToString();
        TestsUser creator = await GetTestCreator(creatorName);
        List<TestsUser> assignedStudents = await GetAssignedStudents(assignedStudentNames);

        var test = new Test
        {
            Id = testId,
            Name = testName,
            AssignedStudents = assignedStudents,
            CreatorId = creator.Id,
            Creator = creator,
            TestResults = new List<TestResult>()
        };

        List<Question> assignedQuestions = _questionService.GetQuestionsForTest(test, questions).ToList();
        test.Questions = assignedQuestions;

        List<TestResult> testResults = _testResultService.InitializeTestResultsForAssignees(assignedStudents, test).ToList();
        test.TestResults = testResults;
        
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

    public async Task<IEnumerable<Test>> GetCreatedTests(string creatorName)
    {
        TestsUser creator = await _userManager.FindByNameAsync(creatorName);
        if (creator == null)
        {
            throw new ApplicationException($"Cannot find user with name ${creatorName}");
        }

        IEnumerable<Test> tests = _testsRepository.Get();
        IEnumerable<Test> createdTests = tests.Where(t => t.CreatorId == creator.Id);
        return createdTests;
    }

    public async Task<IEnumerable<Test>> GetAssignedTests(string assigneeName)
    {
        TestsUser assignee = await _userManager.FindByNameAsync(assigneeName);
        if (assignee == null)
        {
            throw new ApplicationException($"Cannot find user with name ${assigneeName}");
        }

        IEnumerable<Test> tests = _testsRepository.Get();
        IEnumerable<Test> assignedTests = tests
            .Where(t => t.AssignedStudents != null && t.AssignedStudents.Contains(assignee));
        return assignedTests;
    }

    public async Task<TestResult?> GetAssigneeTestResult(string assigneeName, Test test)
    {
        TestsUser assignee = await _userManager.FindByNameAsync(assigneeName);
        if (assignee == null)
        {
            return null;
        }

        TestResult testResult = test.TestResults?.First(t => t.StudentId == assignee.Id)!;
        return testResult;
    }

    public Test GetById(string testId)
    {
        return _testsRepository.Get(testId);
    }

    public async Task Delete(string testId, string creatorName)
    {
        Test testToDelete = GetById(testId);
        TestsUser testCreator = await _userManager.FindByNameAsync(creatorName);
        
        if (testCreator == null || testToDelete.CreatorId != testCreator.Id)
        {
            throw new UnauthorizedAccessException();
        }
        
        _testsRepository.Delete(testId);
    } 
}