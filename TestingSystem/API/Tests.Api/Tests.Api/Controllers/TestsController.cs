using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tests.Api.Interfaces;
using Tests.Api.Models;

namespace Tests.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TestsController : ControllerBase
{
    private readonly ITestsInfoProvider _testsInfoProvider;

    public TestsController(ITestsInfoProvider testsInfoProvider)
    {
        _testsInfoProvider = testsInfoProvider;
    }

    [HttpGet]
    [Authorize(Roles = "teacher")]
    [Route("teacher/tests")]
    public async Task<IActionResult> GetCreatedTests()
    {
        if (User.Identity?.Name == null)
        {
            return Unauthorized();
        }

        string userName = User.Identity.Name;
        IEnumerable<CreatedTestsGridItem> testsGridItems = await _testsInfoProvider.GetCreatedTestsInfo(userName);
        return Ok(testsGridItems);
    }

    [HttpGet]
    [Authorize(Roles = "teacher")]
    [Route("teacher/tests/{id}")]
    public async Task<IActionResult> GetTestResults(string id)
    {
        if (User.Identity?.Name == null)
        {
            return Unauthorized();
        }

        string currentUserName = User.Identity?.Name!;
        try
        {
            IEnumerable<TestResultsGridItem> testResults = 
                await _testsInfoProvider.GetCreatedTestResults(id, currentUserName);
            return Ok(testResults);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
    }

    [HttpGet]
    [Authorize(Roles = "student")]
    [Route("student/tests")]
    public async Task<IActionResult> GetAssignedTests()
    {
        if (User.Identity?.Name == null)
        {
            return Unauthorized();
        }

        string userName = User.Identity.Name;
        IEnumerable<AssignedTestsGridItem> assignedTestsGridItems =
            await _testsInfoProvider.GetAssignedTestsInfo(userName);
        return Ok(assignedTestsGridItems);
    }

    [HttpGet]
    [Authorize(Roles = "student")]
    [Route("student/tests/{id}")]
    public async Task<IActionResult> GetAssignedTest(string id)
    {
        if (User.Identity?.Name == null)
        {
            return Unauthorized();
        }

        string userName = User.Identity.Name;

        try
        {
            TakeTestModel assignedTest = await _testsInfoProvider.GetAssignedTest(id, userName);
            return Ok(assignedTest);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
    }

    [HttpPost]
    [Authorize(Roles = "teacher")]
    [Route("teacher/tests")]
    public async Task<IActionResult> CreateTest([FromBody] CreateTestModel testModel)
    {
        if (User.Identity?.Name == null)
        {
            return Unauthorized();
        }
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        string currentUserName = User.Identity.Name;
        await _testsInfoProvider.CreateTest(testModel, currentUserName);
        
        var successResponse = new Response {Status = "Success", Message = "Test created successfully"};
        return Ok(successResponse);
    }

    [HttpPost]
    [Authorize(Roles = "student")]
    [Route("student/tests")]
    public async Task<IActionResult> AnswerTest([FromBody] AnswerTestModel answerTestModel)
    {
        if (User.Identity?.Name == null)
        {
            return Unauthorized();
        }
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        try
        {
            string currentUserName = User.Identity.Name;
            await _testsInfoProvider.AnswerTest(answerTestModel, currentUserName);
            var successResponse = new Response {Status = "Success", Message = "Test answered"};
            return Ok(successResponse);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
    }

    [HttpDelete]
    [Authorize(Roles = "teacher")]
    [Route("teacher/tests/{id}")]
    public async Task<IActionResult> DeleteTest(string id)
    {
        if (User.Identity?.Name == null)
        {
            return Unauthorized();
        }

        string currentUserName = User.Identity?.Name!;
        try
        {
            await _testsInfoProvider.DeleteTest(id, currentUserName);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }

        var successResponse = new Response {Status = "Success", Message = "Test deleted successfully"};
        return Ok(successResponse);
    }
}