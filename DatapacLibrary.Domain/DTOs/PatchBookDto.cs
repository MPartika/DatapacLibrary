namespace DatapacLibrary.Domain.DataTransferObjects;

public class PatchBookDto
{
    public long Id { get; set; }
    public string? Title { get; set; }
    public string? Author { get; set; } 
    public string? Publisher { get; set; }
    public int? PublicationYear { get; set; }
    public string? ISBN { get; set; }
}