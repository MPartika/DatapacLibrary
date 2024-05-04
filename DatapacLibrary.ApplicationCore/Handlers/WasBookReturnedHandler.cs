using DatapacLibrary.ApplicationCore.Queries;
using DatapacLibrary.Domain.Contracts;
using DatapacLibrary.Domain.DataTransferObjects;
using MediatR;

namespace DatapacLibrary.ApplicationCore.Handlers;

public class WasBookReturnedHandler : IRequestHandler<WasBookReturnedQuery, WasBookReturnedDto?>
{
    private readonly IBookLoanRepository _repository;

    public WasBookReturnedHandler(IBookLoanRepository repository)
    {
        _repository = repository;
    }

    public async Task<WasBookReturnedDto?> Handle(WasBookReturnedQuery request, CancellationToken cancellationToken)
    {
        return await _repository.WasBookReturned(request.UserId, request.BookId);
        
    }
}