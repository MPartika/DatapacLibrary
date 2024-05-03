using DatapacLibrary.ApplicationCore.Commands;
using DatapacLibrary.ApplicationCore.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatapacLibrary.Web.Controllers;

[Authorize]
[Route("[controller]")]
public class UserController : Controller
{
    private readonly IMediator _mediat;

    public UserController(IMediator mediat)
    {
        _mediat = mediat;
    }

    [AllowAnonymous]
    [HttpPost("[action]")]
    public async Task<IActionResult> Login([FromBody] AuthenticateUserCommand command)
    {
        return Ok(await _mediat.Send(command));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        return Ok(await _mediat.Send(new GetAllUsersQuery()));
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
    {
        await _mediat.Send(command);
        return Created();
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command)
    {
        await _mediat.Send(command);
        return Accepted();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteUser(long id)
    {
        await _mediat.Send(new DeleteUserCommand { Id = id });
        return Accepted();
    }
}