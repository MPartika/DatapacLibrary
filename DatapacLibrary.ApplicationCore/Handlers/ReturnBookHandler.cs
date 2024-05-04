using DatapacLibrary.ApplicationCore.Commands;
using DatapacLibrary.Domain.Contracts;
using MediatR;

namespace DatapacLibrary.ApplicationCore.Handlers;

public class ReturnBookHandler : IRequestHandler<ReturnBookCommand>
{
    private readonly IBookLoanRepository _repository;

    public ReturnBookHandler(IBookLoanRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(ReturnBookCommand request, CancellationToken cancellationToken)
    {
        await _repository.ReturnBookAsync(request.UserId, request.BookId);
    }
}