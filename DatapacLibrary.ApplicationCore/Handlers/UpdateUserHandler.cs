using DatapacLibrary.ApplicationCore.Commands;
using DatapacLibrary.Domain;
using DatapacLibrary.Domain.Contracts;
using MediatR;

namespace DatapacLibrary.ApplicationCore.Handlers;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        byte[]? salt = null;
        byte[]? password = request.Password is not null ? 
            AuthenticationHelper.HashPassword(request.Password, out salt) 
            : null;
        await _userRepository.UpdateUserAsync(request.Id, request.Name, request.Email, password, salt);
    }
}