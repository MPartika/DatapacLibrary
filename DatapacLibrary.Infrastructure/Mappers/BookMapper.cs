using DatapacLibrary.Domain.DataTransferObjects;
using DatapacLibrary.Infrastructure.DbEntities;

namespace DatapacLibrary.Infrastructure.Mappers;

internal static class BookMapper
{
    public static BookDto ToBookDto(this Book book)
    {
        return new BookDto
        {
            Id = book.Id,
            Title = book.Title, 
            Author = book.Author,
            Publisher = book.Publisher,
            ISBN= book.ISBN,
            PublicationYear = book.PublicationYear
        };
    }

    public static Book ToBook(this BookDto dto)
    {
        return new Book
        {
            Id = dto.Id,
            Title = dto.Title, 
            Author = dto.Author,
            Publisher = dto.Publisher,
            ISBN= dto.ISBN,
            PublicationYear = dto.PublicationYear
        };
    }
}