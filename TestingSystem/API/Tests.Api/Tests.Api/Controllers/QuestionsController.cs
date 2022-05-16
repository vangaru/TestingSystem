using Microsoft.AspNetCore.Mvc;
using Tests.Domain.Interfaces;
using Tests.Domain.Models;

namespace Tests.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class QuestionsController : ControllerBase
{
    private readonly IQuestionsRepository _questionsRepository;

    public QuestionsController(IQuestionsRepository questionsRepository)
    {
        _questionsRepository = questionsRepository;
    }

    [HttpGet]
    public IActionResult Get()
    {
        List<Question> questions = _questionsRepository.Get().ToList();
        return Ok(questions);
    }

    [HttpGet]
    [Route("{id}")]
    public IActionResult Get(string id)
    {
        Question question = _questionsRepository.Get(id);
        return Ok(question);
    }
    
    [HttpPost]
    public IActionResult Add([FromBody] Question question)
    {
        _questionsRepository.Add(question);
        return NoContent();
    }
    
    [HttpPut]
    [Route("{id}")]
    public IActionResult Update(string id, [FromBody] Question question)
    {
        _questionsRepository.Update(id, question);
        return NoContent();
    }

    [HttpDelete]
    [Route("{id}")]
    public IActionResult Delete(string id)
    {
        _questionsRepository.Delete(id);
        return NoContent();
    }
}