using DatapacLibrary.Domain.DataTransferObjects;
using MediatR;

namespace DatapacLibrary.ApplicationCore.Queries;

public class GetBookQuery : IRequest<BookDto?>
{
    public long Id { get; set; }
}