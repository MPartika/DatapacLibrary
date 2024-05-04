namespace DatapacLibrary.Domain.DataTransferObjects;

public class BookDto
{
    public long Id { get; set; }
    public required string Title { get; set; }
    public required string Author { get; set; } 
    public required string Publisher { get; set; }
    public int PublicationYear { get; set; }
    public required string ISBN { get; set; }
    public bool IsAvailable { get; set; }
}