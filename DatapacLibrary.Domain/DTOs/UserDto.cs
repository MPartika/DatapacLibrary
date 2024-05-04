namespace DatapacLibrary.Domain.DataTransferObjects;

public class UserDto
{
    public long Id { get; set;}    
    public required string Name { get; set;}    
    public required string Email { get; set;}

    public IList<BookDto?>? BooksCurrentlyLanded{ get; set;}
}