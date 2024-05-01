namespace DatapacLibrary.Domain.DataTransferObjects;

public class UserPasswordDto
{
    public long Id { get; set; }
    public byte[] Password { get; set; } = [];
    public byte[] Salt {get; set;} = [];
}