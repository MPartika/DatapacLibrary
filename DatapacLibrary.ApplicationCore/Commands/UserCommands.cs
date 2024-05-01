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
    public int Id { get; set; }
    public string? Name { get; set;}
    public string? Email { get; set;}
    public string? Password { get; set;}
}

public class DeleteUserCommand : IRequest
{
    public int Id { get; set; }
}

public class AuthorizeUserCommand : IRequest
{
    public required string Name { get; set;}
    public required string Password { get; set;}
}