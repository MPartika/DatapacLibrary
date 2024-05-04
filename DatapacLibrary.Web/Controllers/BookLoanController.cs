using DatapacLibrary.ApplicationCore.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatapacLibrary.Web.Controllers;

[Route("[controller]")]
[Authorize]
public class BookLoanController : Controller
{
    private readonly IMediator _mediat;

    public BookLoanController(IMediator mediat)
    {
        _mediat = mediat;
    }

    /// <summary>
    /// Creates Loan For book It book will be unavailable 
    /// </summary>
    /// <param name="command"></param>
    /// <remarks>
    /// Sample request: {"UserId": 1, "BookId": 1 }
    /// </remarks>
    [HttpPost("[action]")]
    public async Task<IActionResult> CreateLoan([FromBody] CreateNewLoanCommand command)
    {
        await _mediat.Send(command);
        return Created();
    }

    /// <summary>
    /// Makes book available 
    /// </summary>
    /// <param name="command"></param>
    /// <remarks>
    /// Sample request: {"UserId": 1, "BookId": 1 }
    /// </remarks>
    [HttpPost("[action]")]
    public async Task<IActionResult> ReturnBook([FromBody] ReturnBookCommand command)
    {
        await _mediat.Send(command);
        return Accepted();
    }
}
