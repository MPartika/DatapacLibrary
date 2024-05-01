using MediatR;

namespace DatapacLibrary.ApplicationCore.Commands;

public class CreateUserCommand : IRequest
{
    public required string Name { get; set;}
    public required string Email { get; set;}
    public required string Password { get; set;}

}

public class UpdateUserCommand : IRequest
{
    public long Id { get; set; }
    public string? Name { get; set;}
    public string? Email { get; set;}
    public string? Password { get; set;}
}

public class DeleteUserCommand : IRequest
{
    public long Id { get; set; }
}

public class AuthenticateUserCommand : IRequest<string>
{
    public required string Name { get; set;}
    public required string Password { get; set;}
}