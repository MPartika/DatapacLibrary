using DatapacLibrary.Domain.DataTransferObjects;
using DatapacLibrary.Infrastructure;
using DatapacLibrary.Infrastructure.DbEntities;
using DatapacLibrary.Infrastructure.Mappers;
using DatapacLibrary.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DatapacLibrary.Test;

public class TestBookRepository
{
    private LibraryDbContext _dbContext;
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
    public async Task ShouldGetBookById()
    {
        var repository = new BookRepository(_dbContext);
        _dbContext.Add(_book);
        _dbContext.SaveChanges();
        var book = await repository.GetBookAsync(_book.Id);

        Assert.IsNotNull(book);
    }

    [Test]
    public async Task ShouldCreateBook()
    {
        var repository = new BookRepository(_dbContext);

        await repository.CreateBookAsync(
            new CreateBookDto
            {
                Author = _book.Author,
                ISBN = _book.ISBN,
                Title = _book.Title,
                Publisher = _book.Publisher
            });

        var book = _dbContext.Books.FirstOrDefault(x => x.Title == _book.Title);
        Assert.IsNotNull(book);
    }

    [Test]
    public async Task ShouldUpdateBookTitle()
    {
        var repository = new BookRepository(_dbContext);
        _dbContext.Add(_book);
        _dbContext.SaveChanges();
        var book = _book.ToBookDto();

        await repository.UpdateBookAsync(new PatchBookDto { Id = _book.Id, Title = "test" });

        Assert.True(_dbContext.Books.Any(x => x.Title == "test"));
    }

    [Test]
    public async Task ShouldUpdateBookAuthor()
    {
        var repository = new BookRepository(_dbContext);
        _dbContext.Add(_book);
        _dbContext.SaveChanges();

        await repository.UpdateBookAsync(new PatchBookDto { Id = _book.Id, Author = "test" });

        Assert.True(_dbContext.Books.Any(x => x.Author == "test"));
    }

    [Test]
    public async Task ShouldUpdateBookISBN()
    {
        var repository = new BookRepository(_dbContext);
        _dbContext.Add(_book);
        _dbContext.SaveChanges();
        var book = _book.ToBookDto();

        await repository.UpdateBookAsync(new PatchBookDto { Id = _book.Id, ISBN = "test" });

        Assert.True(_dbContext.Books.Any(x => x.ISBN == "test"));
    }

    [Test]
    public async Task ShouldUpdateBookPublisher()
    {
        var repository = new BookRepository(_dbContext);
        _dbContext.Add(_book);
        _dbContext.SaveChanges();

        await repository.UpdateBookAsync(new PatchBookDto { Id = _book.Id, Publisher = "test" });

        Assert.True(_dbContext.Books.Any(x => x.Publisher == "test"));
    }

    [Test]
    public async Task ShouldUpdateBookPublicationYear()
    {
        var repository = new BookRepository(_dbContext);
        _dbContext.Add(_book);
        _dbContext.SaveChanges();

        await repository.UpdateBookAsync(new PatchBookDto { Id = _book.Id, PublicationYear = 3 });

        Assert.True(_dbContext.Books.Any(x => x.PublicationYear == 3));
    }

    [Test]
    public async Task ShouldDeleteBook()
    {
        var repository = new BookRepository(_dbContext);
        _dbContext.Add(_book);
        _dbContext.SaveChanges();

        await repository.DeleteBookAsync(_book.Id);

        Assert.False(_dbContext.Books.Any(x => x.Id == _book.Id));
    }
}