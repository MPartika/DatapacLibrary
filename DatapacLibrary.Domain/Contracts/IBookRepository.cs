using DatapacLibrary.Domain.DataTransferObjects;

namespace DatapacLibrary.Domain.Contracts;

public interface IBookRepository : IDependency
{
    /// <summary>
    /// Get book by book id
    /// </summary>
    /// <param name="id">Book Id</param>
    /// <returns>BookDto ot Null</returns>
    Task<BookDto?> GetBookAsync(long id);
    /// <summary>
    /// Create a new book record in DB
    /// </summary>
    /// <param name="book">book object represented by BookDto</param>
    Task CreateBookAsync(BookDto book);
    /// <summary>
    /// If needed updates the book object in DB
    /// </summary>
    /// <param name="book">new book object represented by PatchBookDto</param>
    Task UpdateBookAsync(PatchBookDto book);
    /// <summary>
    /// Deletes a book records in Db
    /// </summary>
    /// <param name="id">Book id</param>
    Task DeleteBookAsync(long id);
}