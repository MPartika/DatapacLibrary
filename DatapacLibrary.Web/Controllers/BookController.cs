using DatapacLibrary.ApplicationCore.Commands;
using DatapacLibrary.ApplicationCore.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatapacLibrary.Web.Controllers;

[Authorize]
[Route("[controller]")]
public class BookController : Controller
{
    private readonly IMediator _mediat;

    public BookController(IMediator mediat)
    {
        _mediat = mediat;
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetBook(long id)
    {
        return Ok(await _mediat.Send(new GetBookQuery {Id = id}));
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] CreateBookCommand command)
    {
        await _mediat.Send(command);
        return Created();
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateBook([FromBody] UpdateBookCommand command)
    {
        await _mediat.Send(command);
        return Accepted();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteBook(long id)
    {
        await _mediat.Send(new DeleteBookCommand { Id = id });
        return Accepted();
    }
}