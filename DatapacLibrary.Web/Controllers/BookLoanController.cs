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

    [HttpPost("[action]")]
    public async Task<IActionResult> CreateLoan([FromBody] CreateNewLoanCommand command)
    {
        await _mediat.Send(command);
        return Created();
    }
}
