

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatapacLibrary.Infrastructure.DbEntities;

public class User : IDbEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set;}    
    [MaxLength(255)]
    public required string Name { get; set;}    
    [MaxLength(255)]
    public required string Email { get; set;}
    public byte[] Password { get; set; } = [];
    public byte[] Salt { get; set; } = [];
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }

    #pragma warning disable CS8618
    public ICollection<UserBook> UserBooks { get; set; }
}