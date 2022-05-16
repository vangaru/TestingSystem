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
    private readonly ITestResultService _testResultService;

    public TestsInfoProvider(ITestsService testsService, ITestResultService testResultService)
    {
        _testsService = testsService;
        _testResultService = testResultService;
    }

    public async Task CreateTest(CreateTestModel createTestModel, string testCreatorName)
    {
        IEnumerable<Question> questions = 
            createTestModel.Questions!.Select(q => new Question
            {
                Name = q.QuestionName,
                ExpectedAnswer = q.ExpectedAnswer,
                Type = ParseQuestionType(q.QuestionType),
                SelectableAnswers = q.SelectableAnswers?.Select(a => new SelectableAnswer
                {
                    Name = a.Name,
                    Index = a.Index
                })
            });
        
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

    public async Task<IEnumerable<AssignedTestsGridItem>> GetAssignedTestsInfo(string assigneeName)
    {
        IEnumerable<TestResult> assignedTestResults = await _testResultService.GetAssignedTestResults(assigneeName);
        IEnumerable<AssignedTestsGridItem> assignedTestsGridItems = assignedTestResults
            .Select(result => new AssignedTestsGridItem
            {
                TestId = result.TestId,
                TestName = result.Test!.Name,
                TeacherName = result.Test.Creator!.UserName,
                Status = result.Status!,
                QuestionsCount = result.Test!.Questions!.Count,
                Results = TestPassed(result.Status) ? _testResultService.CalculateResults(result.QuestionAnswers!) : 0,
            });
        
        return assignedTestsGridItems;
    }

    public async Task<IEnumerable<TestResultsGridItem>> GetCreatedTestResults(string testId, string testCreatorName)
    {
        IEnumerable<TestResult> testResults = await _testResultService.GetCreatedTestResults(testId, testCreatorName);
        IEnumerable<TestResultsGridItem> testResultsGridItems = testResults.Select(result => new TestResultsGridItem
        {
            TestId = result.TestId,
            Result = TestPassed(result.Status) ? _testResultService.CalculateResults(result.QuestionAnswers!) : 0,
            Status = result.Status,
            StudentName = result.Student!.UserName,
            TestName = result.Test!.Name
        });

        return testResultsGridItems;
    }

    private bool TestPassed(string? testStatus)
    {
        return testStatus != null && testStatus.ToLower().Trim() == TestStatus.Done.ToString().ToLower().Trim();
    }

    public async Task DeleteTest(string testId, string testCreatorName)
    {
        await _testsService.Delete(testId, testCreatorName);
    }

    public async Task<TakeTestModel> GetAssignedTest(string testId, string userName)
    {
        Test test = await _testsService.GetByIdAndUserName(testId, userName);
        var testToTake = new TakeTestModel
        {
            TestId = test.Id,
            TestName = test.Name,
            Questions = test.Questions!.Select(q => new TakeQuestionModel
            {
                QuestionId = q.Id,
                QuestionName = q.Name,
                QuestionType = q.QuestionType,
                SelectableQuestions = q.SelectableQuestionNames!.Select(a => new TakeSelectableAnswerModel
                {
                    Index = a.Index,
                    Name = a.Name
                })
            })
        };

        return testToTake;
    }

    public async Task AnswerTest(AnswerTestModel answerTestModel, string userName)
    {
        IEnumerable<(string testId, string questionAnswer)> questionAnswers =
            answerTestModel.QuestionAnswers!.Select(qa => (qa.QuestionId, qa.Answer))!;
        await _testsService.Answer(answerTestModel.TestId!, questionAnswers, userName);
    }
}