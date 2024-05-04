namespace DatapacLibrary.Domain.DataTransferObjects;

public class WasBookReturnedDto
{
    public long LoanId { get; set; }
    public required string Name { get; set;}    
    public required string Email { get; set;}
    public required string Title { get; set; }
    public required string Author { get; set; } 
    public bool WasReturned { get; set; }
}