using DatapacLibrary.Domain.Contracts;
using DatapacLibrary.Domain.DataTransferObjects;
using DatapacLibrary.Infrastructure.DbEntities;
using DatapacLibrary.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace DatapacLibrary.Infrastructure.Repositories;

internal class BookLoanRepository : IBookLoanRepository
{
    private readonly LibraryDbContext _dbContext;

    public BookLoanRepository(LibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateNewLoanAsync(long userId, long bookId)
    {
        if (!await _dbContext.Books.AnyAsync(x => x.Id == bookId))
            throw new ArgumentException("Book not found", "BookId");
        if (!await _dbContext.Users.AnyAsync(x => x.Id == userId))
            throw new ArgumentException("User not found", "UserId");

        _dbContext.Add(new UserBook{UserId = userId, BookId = bookId, Returned = false, ValidUntil = DateTime.UtcNow.AddDays(7)});
        _dbContext.SaveChanges();
    }

    public async Task<bool> IsBookAvailable(long bookId)
    {
        return await _dbContext.Books.AnyAsync(b => b.Id == bookId && (b.UserBooks == null || !b.UserBooks.Any(x => !x.Returned)));
    }

    public async Task<IEnumerable<LoanWarningDto>> GetLoansPastReturnTimeAsync()
    {
        return await _dbContext.UserBooks
            .Where(x => x.ValidUntil <= DateTime.UtcNow)
            .Select(x => x.ToLoanWarningDto())
            .ToListAsync();
    }

    public async Task ReturnBookAsync(long userId, long bookId)
    {
        var loan = await _dbContext.UserBooks
            .Where(x => x.UserId == userId && x.BookId == bookId && !x.Returned)
            .ToListAsync();
        
        loan.ForEach(x => x.Returned = true);

        await _dbContext.SaveChangesAsync();

    }  
}