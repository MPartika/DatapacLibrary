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

    /// <summary>
    /// Authenticates user 
    /// </summary>
    /// <param name="command"></param>
    /// <returns>Authentication JWT token</returns>
    /// <remarks>
    /// Sample request: {"Name": "User1", "Password": "Password1" }
    /// </remarks>
    [AllowAnonymous]
    [HttpPost("[action]")]
    public async Task<IActionResult> Login([FromBody] AuthenticateUserCommand command)
    {
        return Ok(await _mediat.Send(command));
    }

    /// <summary>
    /// Get All Users
    /// </summary>
    /// <returns>Returns List of UserDto objects</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        return Ok(await _mediat.Send(new GetAllUsersQuery()));
    }

    /// <summary>
    /// Create User 
    /// </summary>
    /// <param name="command">Accepts CreateUserCommand object</param>
    /// <remarks>
    /// Sample request: {"Name": "User51", "Password": "Password51", "Email": "User51@example.com" }
    /// </remarks>
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
    {
        await _mediat.Send(command);
        return Created();
    }

    /// <summary>
    /// Update User 
    /// </summary>
    /// <param name="command">Accepts CreateUserCommand object</param>
    /// <remarks>
    /// Sample request: { "Id": 10, "Email": "User51@example.com" }
    /// </remarks>
    [HttpPatch]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command)
    {
        await _mediat.Send(command);
        return Accepted();
    }

    /// <summary>
    /// Update User 
    /// </summary>
    /// <param name="id" example="10">Accepts CreateUserCommand object</param>
    /// <remarks>
    /// </remarks>
    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteUser(long id)
    {
        await _mediat.Send(new DeleteUserCommand { Id = id });
        return Accepted();
    }
}