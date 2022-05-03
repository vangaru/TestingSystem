using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tests.Api.Models;
using Tests.Application.Interfaces;
using Tests.Domain.Models;

namespace Tests.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TestsController : ControllerBase
{
    private readonly ITestsService _testsService;

    public TestsController(ITestsService testsService)
    {
        _testsService = testsService;
    }

    [HttpGet]
    [Authorize(Roles = "teacher")]
    public async Task<IActionResult> GetCreatedTests()
    {
        if (User.Identity?.Name == null)
        {
            return Unauthorized();
        }

        string currentUserName = User.Identity?.Name!;
        IEnumerable<Test> createdTests = await _testsService.GetCreatedTests(currentUserName);
        return Ok(createdTests);
    }

    [HttpPost]
    [Authorize(Roles = "teacher")]
    public async Task<IActionResult> AddTest([FromBody] CreateTestModel testModel)
    {
        if (User.Identity?.Name == null)
        {
            return Unauthorized();
        }
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        string currentUserName = User.Identity.Name!;
        IEnumerable<(string, string)> questions = testModel.Questions!
            .Select(q => (q.QuestionName, q.ExpectedAnswer))!;
        
        await _testsService.Add(testModel.TestName!, questions, currentUserName, testModel.AssignedStudentNames!);
        
        var successResponse = new Response {Status = "Success", Message = "Test created successfully"};
        return Ok(successResponse);
    }
}