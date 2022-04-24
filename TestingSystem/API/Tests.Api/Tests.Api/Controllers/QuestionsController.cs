using Microsoft.AspNetCore.Mvc;
using Tests.Domain.Interfaces;
using Tests.Domain.Models;

namespace Tests.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class QuestionsController : ControllerBase
{
    private readonly IQuestionRepository _questionRepository;

    public QuestionsController(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    [HttpGet]
    public IActionResult Get()
    {
        List<Question> questions = _questionRepository.Get().ToList();
        return Ok(questions);
    }

    [HttpGet]
    [Route("{id}")]
    public IActionResult Get(string id)
    {
        Question question = _questionRepository.Get(id);
        return Ok(question);
    }
    
    [HttpPost]
    public IActionResult Add([FromBody] Question question)
    {
        _questionRepository.Add(question);
        return NoContent();
    }
    
    [HttpPut]
    [Route("{id}")]
    public IActionResult Update(string id, [FromBody] Question question)
    {
        _questionRepository.Update(id, question);
        return NoContent();
    }

    [HttpDelete]
    [Route("{id}")]
    public IActionResult Delete(string id)
    {
        _questionRepository.Delete(id);
        return NoContent();
    }
}