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

    /// <summary>
    /// Get Book By Id
    /// </summary>
    /// <param name="id" example="1"></param>
    /// <returns>BookDto or null</returns>
    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetBook(long id)
    {
        return Ok(await _mediat.Send(new GetBookQuery {Id = id}));
    }

    /// <summary>
    /// Creates book
    /// </summary>
    /// <param name="command"></param>
    /// <remarks>
    /// Sample request: {"Title": "Book1", "Author": "Author1", "Publisher": "Publisher1", "PublicationYear": 2004, "ISBN":"ISBN1" }
    /// </remarks>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] CreateBookCommand command)
    {
        await _mediat.Send(command);
        return Created();
    }

    /// <summary>
    /// Updates Book 
    /// </summary>
    /// <param name="command"></param>
    /// <remarks>
    /// Sample request: {"Id": 1, "PublicationYear": 2005 }
    /// </remarks>
    /// <returns></returns>
    [HttpPatch]
    public async Task<IActionResult> UpdateBook([FromBody] UpdateBookCommand command)
    {
        await _mediat.Send(command);
        return Accepted();
    }

    /// <summary>
    /// Deletes book
    /// </summary>
    /// <param name="id" example="10"></param>
    /// <returns></returns>
    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteBook(long id)
    {
        await _mediat.Send(new DeleteBookCommand { Id = id });
        return Accepted();
    }
}