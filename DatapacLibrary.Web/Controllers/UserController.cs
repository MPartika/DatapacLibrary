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
    /// Sample request: {"Name": "User51", "Email": "User51@example.com" }
    /// </remarks>
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
    /// Delete User 
    /// </summary>
    /// <param name="id" example="10"></param>
    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteUser(long id)
    {
        await _mediat.Send(new DeleteUserCommand { Id = id });
        return Accepted();
    }
}