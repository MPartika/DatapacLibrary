using DatapacLibrary.ApplicationCore.Commands;
using DatapacLibrary.Domain;
using DatapacLibrary.Domain.Contracts;
using MediatR;

namespace DatapacLibrary.ApplicationCore.Handlers;

class CreateAdminHandler : IRequestHandler<CreateAdminCommand>
{
    private readonly IAdminRepository _repository;

    public CreateAdminHandler(IAdminRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(CreateAdminCommand request, CancellationToken cancellationToken)
    {
        var password = AuthenticationHelper.HashPassword(request.Password, out byte[] salt);
        await _repository.CreateAdminAsync(request.Name, password, salt);
    }
}