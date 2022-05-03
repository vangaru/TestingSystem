using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tests.Api.Models;
using Tests.Application.Interfaces;

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

    [HttpPost]
    [Authorize(Roles = "teacher")]
    public IActionResult AddTest([FromBody] CreateTestModel testModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        _testsService.Add(testModel.Test!, testModel.CreatorName!, testModel.AssignedStudentNames!);
        var successResponse = new Response {Status = "Success", Message = "Test created successfully"};
        return Ok(successResponse);
    }
}