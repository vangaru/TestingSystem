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

    [HttpPost]
    [Authorize(Roles = "teacher")]
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

    [HttpDelete]
    [Authorize(Roles = "teacher")]
    [Route("{id}")]
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