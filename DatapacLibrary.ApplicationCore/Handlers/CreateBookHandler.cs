using DatapacLibrary.ApplicationCore.Commands;
using DatapacLibrary.Domain.Contracts;
using MediatR;

namespace DatapacLibrary.ApplicationCore.Handlers;

public class CreateBookHandler : IRequestHandler<CreateBookCommand>
{
    private readonly IBookRepository _bookRepository;

    public CreateBookHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        await _bookRepository.CreateBookAsync(request);
    }
}