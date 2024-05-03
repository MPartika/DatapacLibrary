using MediatR;

namespace DatapacLibrary.ApplicationCore.Commands;

public class CreateNewLoanCommand : IRequest
{
    public long UserId { get; set; }
    public long BookId { get; set; }
}

public class ReturnBookCommand : IRequest
{
    public long UserId { get; set; } 
    public long BookId { get; set; }
}

public class NotifyUsersWithNotReturnedBooksCommand : IRequest;

