using Tests.Api.Interfaces;
using Tests.Api.Models;
using Tests.Application.Interfaces;
using Tests.Domain.Models;

namespace Tests.Api.Implementations;

public class TestsInfoProvider : ITestsInfoProvider
{
    private readonly ITestsService _testsService;

    public TestsInfoProvider(ITestsService testsService)
    {
        _testsService = testsService;
    }

    public async Task CreateTest(CreateTestModel createTestModel, string testCreatorName)
    {
        IEnumerable<(string QuestionName, string ExpectedAnswer)> questions = 
            createTestModel.Questions!.Select(q => (q.QuestionName, q.ExpectedAnswer))!;

        await _testsService.Add(createTestModel.TestName!, questions, testCreatorName, createTestModel.AssignedStudentNames!);
    }

    public async Task<IEnumerable<CreatedTestsGridItem>> GetCreatedTestsInfo(string testCreatorName)
    {
        IEnumerable<Test> createdTests = await _testsService.GetCreatedTests(testCreatorName);
        IEnumerable<CreatedTestsGridItem> createdTestsInfo = createdTests.Select(test => new CreatedTestsGridItem
        {
            TestId = test.Id,
            TestName = test.Name,
            AssignedStudentsCount = test.AssignedStudents!.Count,
            QuestionsCount = test.Questions!.Count,
            ResultsCount = test.TestResults!.Count
        });

        return createdTestsInfo;
    }
}