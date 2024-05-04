using DatapacLibrary.ApplicationCore.Commands;
using DatapacLibrary.Domain.Contracts;
using MediatR;

namespace DatapacLibrary.ApplicationCore.Handlers;

public class DeleteAdminHandler : IRequestHandler<DeleteAdminCommand>
{
    private readonly IAdminRepository _repository;

    public DeleteAdminHandler(IAdminRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteAdminCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAdminAsync(request.Id);
    }
}