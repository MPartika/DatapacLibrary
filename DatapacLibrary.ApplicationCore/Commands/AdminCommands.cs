using MediatR;

namespace DatapacLibrary.ApplicationCore.Commands;



public class AuthenticateAdminCommand : IRequest<string>
{
    public required string Name { get; set;}
    public required string Password { get; set;}
}

public class CreateAdminCommand : IRequest
{
    public required string Name { get; set;}
    public required string Password { get; set;}
}

public class DeleteAdminCommand : IRequest
{
    public long Id { get; set; }
}