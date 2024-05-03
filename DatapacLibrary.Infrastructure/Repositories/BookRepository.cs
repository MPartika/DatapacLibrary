using DatapacLibrary.Domain.Contracts;
using DatapacLibrary.Domain.DataTransferObjects;
using DatapacLibrary.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace DatapacLibrary.Infrastructure.Repositories;

internal class BookRepository : IBookRepository
{
    private readonly LibraryDbContext _dbContext;

    public BookRepository(LibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<BookDto?> GetBookAsync(long id)
    {
        var book = await _dbContext.Books.SingleOrDefaultAsync(book => book.Id == id);
        if (book == null)
            return null;
        return book.ToBookDto();
    }

    public async Task CreateBookAsync(CreateBookDto book)
    {
        _dbContext.Add(book.ToBook());
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateBookAsync(PatchBookDto book)
    {
        var originalBook = await _dbContext.Books.SingleOrDefaultAsync(b => b.Id == book.Id);
        if (originalBook is null)
            return;
        if (book.Title is not null && book.Title != originalBook.Title)
            originalBook.Title = book.Title;
        if (book.Author is not null && book.Author != originalBook.Author)
            originalBook.Author = book.Author;
        if (book.Publisher is not null && book.Publisher != originalBook.Publisher)
            originalBook.Publisher = book.Publisher;
        if (book.ISBN is not null && book.ISBN != originalBook.ISBN)
            originalBook.ISBN = book.ISBN;
        if (book.PublicationYear is not null && book.PublicationYear != originalBook.PublicationYear)
            originalBook.PublicationYear = (int)book.PublicationYear;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteBookAsync(long id)
    {
        var originalBook = await _dbContext.Books.SingleOrDefaultAsync(b => b.Id == id);
        if (originalBook is null)
            return;
        _dbContext.Remove(originalBook);
        await _dbContext.SaveChangesAsync();
    }
}