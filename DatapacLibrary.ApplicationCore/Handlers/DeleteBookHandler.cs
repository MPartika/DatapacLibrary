using DatapacLibrary.ApplicationCore.Commands;
using DatapacLibrary.Domain.Contracts;
using MediatR;

namespace DatapacLibrary.ApplicationCore.Handlers;

public class DeleteBookHandler : IRequestHandler<DeleteBookCommand>
{
    private readonly IBookRepository _bookRepository;

    public DeleteBookHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        await _bookRepository.DeleteBookAsync(request.Id);
    }
}