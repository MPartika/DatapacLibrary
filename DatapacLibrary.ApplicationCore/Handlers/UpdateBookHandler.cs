using DatapacLibrary.ApplicationCore.Commands;
using DatapacLibrary.Domain.Contracts;
using MediatR;

namespace DatapacLibrary.ApplicationCore.Handlers;

public class UpdateBookHandler : IRequestHandler<UpdateBookCommand>
{
    private readonly IBookRepository _bookRepository;

    public UpdateBookHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        await _bookRepository.UpdateBookAsync(request);
    }
}