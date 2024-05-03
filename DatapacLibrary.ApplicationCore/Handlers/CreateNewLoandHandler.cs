using DatapacLibrary.ApplicationCore.Commands;
using DatapacLibrary.Domain.Contracts;
using MediatR;

namespace DatapacLibrary.ApplicationCore.Handlers;

public class CreateNewLoanHandler : IRequestHandler<CreateNewLoanCommand>
{
    private readonly IBookLoanRepository _repository;

    public CreateNewLoanHandler(IBookLoanRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(CreateNewLoanCommand request, CancellationToken cancellationToken)
    {
        await _repository.CreateNewLoan(request.UserId, request.BookId);
    }
}
