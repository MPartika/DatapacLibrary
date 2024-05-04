namespace DatapacLibrary.Domain.DataTransferObjects;

public class AdminDto
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public byte[] Password { get; set; } = [];
    public byte[] Salt {get; set;} = [];
}