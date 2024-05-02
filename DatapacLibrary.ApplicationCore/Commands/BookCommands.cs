using DatapacLibrary.Domain.DataTransferObjects;
using MediatR;

namespace DatapacLibrary.ApplicationCore.Commands;

public class CreateBookCommand : CreateBookDto, IRequest;

public class UpdateBookCommand : PatchBookDto, IRequest;

public class DeleteBookCommand : IRequest
{
    public long Id { get; set; }
}