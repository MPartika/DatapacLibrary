using DatapacLibrary.Domain.DataTransferObjects;
using MediatR;

namespace DatapacLibrary.ApplicationCore.Queries;

public class WasBookReturnedQuery : IRequest<WasBookReturnedDto?>
{
    public long UserId { get; set;}
    public long BookId { get; set; }
}