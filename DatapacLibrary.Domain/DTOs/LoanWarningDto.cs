namespace DatapacLibrary.Domain.DataTransferObjects;

public class LoanWarningDto
{
    public long LoanId { get; set; }
    public long UserId { get; set; }
    public long BookId { get; set; }
    public required string Name { get; set;}    
    public required string Email { get; set;}
    public required string Title { get; set; }
    public DateTime ValidUntil { get; set; }
}