using DatapacLibrary.ApplicationCore.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatapacLibrary.Web.Controllers;

[Authorize]
[Route("[controller]")]
public class AdminController : Controller
{
    private readonly IMediator _mediat;

    public AdminController(IMediator mediat)
    {
        _mediat = mediat;
    }

    /// <summary>
    /// Authenticates app user 
    /// </summary>
    /// <param name="command"></param>
    /// <returns>Authentication JWT token</returns>
    /// <remarks>
    /// Sample request: {"Name": "admin1", "Password": "Password1" }
    /// </remarks>
    [AllowAnonymous]
    [HttpPost("[action]")]
    public async Task<IActionResult> Login([FromBody] AuthenticateAdminCommand command)
    {
        return Ok(await _mediat.Send(command));
    }

    /// <summary>
    /// Create Admin Entity 
    /// </summary>
    /// <param name="command"></param>
    /// <remarks>
    /// Sample request: {"Name": "admin51", "Password": "Password51" }
    /// </remarks>
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> CreateAdmin([FromBody] CreateAdminCommand command)
    {
        await _mediat.Send(command);
        return Created();
    }

    /// <summary>
    /// Delete User 
    /// </summary>
    /// <param name="id" example="2">Accepts CreateUserCommand object</param>
    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteUser(long id)
    {
        await _mediat.Send(new DeleteUserCommand { Id = id });
        return Accepted();
    }
}