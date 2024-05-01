using DatapacLibrary.ApplicationCore.Commands;
using DatapacLibrary.Domain;
using DatapacLibrary.Domain.Contracts;
using MediatR;

namespace DatapacLibrary.ApplicationCore.Handlers;

public class CreateUserHandler : IRequestHandler<CreateUserCommand>
{
    private readonly IUserRepository _userRepository;

    public CreateUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var password = AuthenticationHelper.HashPassword(request.Password, out byte[] salt);
        await _userRepository.CreateUserAsync(request.Name, request.Email, password, salt);
    }
}