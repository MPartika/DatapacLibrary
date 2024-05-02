using DatapacLibrary.ApplicationCore.Queries;
using DatapacLibrary.Domain.Contracts;
using DatapacLibrary.Domain.DataTransferObjects;
using MediatR;

namespace DatapacLibrary.ApplicationCore.Handlers;

public class GetBookHandler : IRequestHandler<GetBookQuery, BookDto?>
{
    private readonly IBookRepository _bookRepository;

    public GetBookHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<BookDto?> Handle(GetBookQuery request, CancellationToken cancellationToken)
    {
        return await _bookRepository.GetBookAsync(request.Id);
    }
}