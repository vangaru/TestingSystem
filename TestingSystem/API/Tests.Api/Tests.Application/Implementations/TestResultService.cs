using Microsoft.AspNetCore.Identity;
using Tests.Application.Interfaces;
using Tests.Application.Models;
using Tests.Domain.Interfaces;
using Tests.Domain.Models;

namespace Tests.Application.Implementations;

public class TestResultService : ITestResultService
{
    private readonly ITestsRepository _testsRepository;
    private readonly UserManager<TestsUser> _userManager;

    public TestResultService(ITestsRepository testsRepository, UserManager<TestsUser> userManager)
    {
        _testsRepository = testsRepository;
        _userManager = userManager;
    }

    public double CalculateResults(IEnumerable<QuestionAnswer> questionAnswers)
    {
        var answers = questionAnswers.ToList();
        int totalAnswersCount = answers.Count;
        int correctAnswersCount = GetCorrectAnswersCount(answers);
        double results = CalculateResults(totalAnswersCount, correctAnswersCount);
        return results;
    }

    public IEnumerable<TestResult> InitializeTestResultsForAssignees(IEnumerable<TestsUser> assignees, Test test)
    {
        var testResults = new List<TestResult>();

        foreach (TestsUser assignee in assignees)
        {
            string id = Guid.NewGuid().ToString();
            var testResult = new TestResult
            {
                Id = id,
                Status = TestStatus.Assigned.ToString(),
                TestId = test.Id,
                Test = test,
                Student = assignee,
                StudentId = assignee.Id,
                QuestionAnswers = new List<QuestionAnswer>()
            };
            testResults.Add(testResult);
        }
        
        return testResults;
    }

    public async Task<IEnumerable<TestResult>> GetCreatedTestResults(string testId, string testCreatorName)
    {
        Test createdTest = _testsRepository.Get(testId);
        TestsUser testCreator = await _userManager.FindByNameAsync(testCreatorName);

        if (testCreator == null || createdTest.CreatorId != testCreator.Id)
        {
            throw new UnauthorizedAccessException();
        }

        IEnumerable<TestResult> testResults = createdTest.TestResults ?? new List<TestResult>();
        return testResults;
    }

    public async Task<IEnumerable<TestResult>> GetAssignedTestResults(string assigneeName)
    {
        TestsUser assignee = await _userManager.FindByNameAsync(assigneeName);
        
        if (assignee == null)
        {
            throw new ApplicationException($"Cannot find user with name ${assigneeName}");
        }

        IEnumerable<Test> tests = _testsRepository.Get();
        IEnumerable<TestResult> assignedTestResults =
            tests.SelectMany(t => t.TestResults!.Where(r => r.StudentId == assignee.Id));

        return assignedTestResults;
    }

    private int GetCorrectAnswersCount(IEnumerable<QuestionAnswer> questionAnswers)
    {
        return questionAnswers.Count(answer => 
            !AnswerSkipped(answer) && AnswerCorrect(answer.Question!, answer.ActualAnswer!));
    }

    private bool AnswerSkipped(QuestionAnswer answer)
    {
        return answer.Question == null || answer.ActualAnswer == null;
    }

    private bool AnswerCorrect(Tests.Domain.Models.Question question, string actualAnswer)
    {
        if (question.ExpectedAnswer == null)
        {
            return false;
        }

        if (question.QuestionType == QuestionType.Checkbox.ToString())
        {
            string[] expectedAnswer = question.ExpectedAnswer.Split(',');
            string[] answer = actualAnswer.Split(',');
            if (answer.Length != expectedAnswer.Length)
            {
                return false;
            }

            foreach (string a in answer)
            {
                if (!expectedAnswer.Contains(a))
                {
                    return false;
                }
            }

            return true;
        }
        
        return question.ExpectedAnswer.ToLower().Trim() == actualAnswer.ToLower().Trim();
    }

    private double CalculateResults(int totalAnswersCount, int correctAnswersCount)
    {
        return (double) correctAnswersCount / totalAnswersCount;
    }
}