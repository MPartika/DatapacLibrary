
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatapacLibrary.Infrastructure.DbEntities;

public class Admin : IDbEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set;}    
    [MaxLength(255)]
    public required string Name { get; set;}   
    public byte[] Password { get; set; } = [];
    public byte[] Salt { get; set; } = [];
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}