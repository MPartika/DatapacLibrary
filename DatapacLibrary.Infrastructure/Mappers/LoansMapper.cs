using DatapacLibrary.Domain.DataTransferObjects;
using DatapacLibrary.Infrastructure.DbEntities;

namespace DatapacLibrary.Infrastructure.Mappers;

internal static class LoansMapper
{
    public static LoanWarningDto ToLoanWarningDto(this UserBook userBook)
    {
        return new LoanWarningDto
        {
            LoanId = userBook.Id,
            UserId = userBook.UserId,
            BookId = userBook.BookId,
            Name = userBook.User?.Name ?? "",
            Email = userBook.User?.Email ?? "",
            ValidUntil= userBook.ValidUntil,
            Title= userBook.Book?.Title ?? "",
        };
    }

     public static WasBookReturnedDto ToWasBookReturnedDto(this UserBook userBook)
    {
        return new WasBookReturnedDto
        {
            LoanId = userBook.Id,
            Name = userBook.User?.Name ?? "",
            Email = userBook.User?.Email ?? "",
            Title= userBook.Book?.Title ?? "",
            Author = userBook.Book?.Author ?? "",
            WasReturned = userBook.Returned
        };
    }
}