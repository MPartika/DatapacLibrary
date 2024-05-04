using DatapacLibrary.Infrastructure;
using DatapacLibrary.Infrastructure.DbEntities;
using DatapacLibrary.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace DatapacLibrary.Test;

public class TestBookLoanRepository
{
    private LibraryDbContext _dbContext;
    private User _user = new User { Name = "Test", Email = "", Password = [], Salt = [] };
    private Book _book = new Book { Author = "Test", ISBN = "Test", Publisher = "Test", Title = "Test" };

    [SetUp]
    public void Init()
    {

        _dbContext = new LibraryDbContext("UnitTestDb.db");
        _dbContext.Database.EnsureDeleted();
        _dbContext.Database.Migrate();
    }

    [TearDown]
    public void Cleanup()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Dispose();
    }
    
    [Test]
    public async Task ShouldCreateNewLoan()
    {
        _dbContext.Add(_user);
        _dbContext.Add(_book);
        await _dbContext.SaveChangesAsync();

        var repository = new BookLoanRepository(_dbContext);
        await repository.CreateNewLoanAsync(_user.Id, _book.Id);
        var bookLoan = await _dbContext.UserBooks.FirstOrDefaultAsync(x => x.BookId == _book.Id && x.UserId == _user.Id);
        Assert.That(bookLoan, Is.Not.Null);
    }

    [Test]
    public async Task ShouldReturnBook()
    {
        _dbContext.Add(_user);
        _dbContext.Add(_book);
        await _dbContext.SaveChangesAsync();

        var notReturnedBookLoan = new UserBook{BookId = _book.Id, UserId = _user.Id, Returned = false, ValidUntil = DateTime.UtcNow};
        _dbContext.Add(notReturnedBookLoan);
        await _dbContext.SaveChangesAsync();

        var repository = new BookLoanRepository(_dbContext);
        await repository.ReturnBookAsync(_user.Id, _book.Id);

        var bookLoan = await _dbContext.UserBooks.FirstOrDefaultAsync(x => x.Id == notReturnedBookLoan.Id);
        Assert.That(bookLoan, Is.Not.Null);
        Assert.That(bookLoan.Returned, Is.True);
    }

    [Test]
    public async Task ShouldReturnedNotAvailable()
    {
        _dbContext.Add(_user);
        _dbContext.Add(_book);
        await _dbContext.SaveChangesAsync();

        var notReturnedBookLoan = new UserBook{BookId = _book.Id, UserId = _user.Id, Returned = false, ValidUntil = DateTime.UtcNow};
        _dbContext.Add(notReturnedBookLoan);
        await _dbContext.SaveChangesAsync();

        var repository = new BookLoanRepository(_dbContext);
        var IsBookAvailable = await repository.IsBookAvailable(_book.Id);
        Assert.That(IsBookAvailable, Is.False);
    }

    [Test]
    public async Task ShouldReturnListOfBooksThatArePastReturnTime()
    {
        _dbContext.Add(_user);
        _dbContext.Add(_book);
        await _dbContext.SaveChangesAsync();

        var notReturnedBookLoan = new UserBook{BookId = _book.Id, UserId = _user.Id, Returned = false, ValidUntil = DateTime.UtcNow.AddDays(-1)};
        _dbContext.Add(notReturnedBookLoan);
        await _dbContext.SaveChangesAsync();

        var repository = new BookLoanRepository(_dbContext);
        var booksPastReturnTime = await repository.GetLoansPastReturnTimeAsync();

        Assert.That(booksPastReturnTime, Is.Not.Null);
        Assert.That(booksPastReturnTime, Is.Not.Empty);
    }

    [Test]
    public async Task ShouldExtendBookValidDateByNumberOfDays()
    {
        _dbContext.Add(_user);
        _dbContext.Add(_book);
        await _dbContext.SaveChangesAsync();
        var originalDate = DateTime.UtcNow.AddDays(-1);
        var notReturnedBookLoan = new UserBook{BookId = _book.Id, UserId = _user.Id, Returned = false, ValidUntil = originalDate};
        _dbContext.Add(notReturnedBookLoan);
        await _dbContext.SaveChangesAsync();

        var repository = new BookLoanRepository(_dbContext);
        await repository.ExtendValidUnitByDays(notReturnedBookLoan.Id, 2);

        var newBookLoan = _dbContext.UserBooks.Single(x => x.Id == notReturnedBookLoan.Id);
        Assert.That(newBookLoan.ValidUntil > originalDate);
    }
}