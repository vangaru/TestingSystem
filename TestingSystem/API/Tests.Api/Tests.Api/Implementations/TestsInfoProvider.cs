using Tests.Api.Interfaces;
using Tests.Api.Models;
using Tests.Application.Interfaces;
using Tests.Application.Models;
using Tests.Domain.Models;
using Question = Tests.Application.Models.Question;

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
        IEnumerable<Question> questions = 
            createTestModel.Questions!.Select(q => new Question
            {
                Name = q.QuestionName,
                ExpectedAnswer = q.ExpectedAnswer,
                Type = ParseQuestionType(q.QuestionType),
                SelectableQuestionNames = q.SelectableAnswers
            })!;
        
        await _testsService.Add(createTestModel.TestName!, questions, testCreatorName, createTestModel.AssignedStudentNames!);
    }

    private QuestionType ParseQuestionType(string? questionType)
    {
        if (questionType == null)
        {
            return QuestionType.String;
        }

        var type = (QuestionType) Enum.Parse(typeof(QuestionType), questionType, true);
        return type;
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

    public async Task DeleteTest(string testId, string testCreatorName)
    {
        await _testsService.Delete(testId, testCreatorName);
    }
}