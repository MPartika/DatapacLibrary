
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatapacLibrary.Infrastructure.DbEntities;

public class Book : IDbEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [MaxLength(255)]
    public required string Title { get; set; }
    [MaxLength(255)]
    public required string Author { get; set; } 
    [MaxLength(255)]
    public required string Publisher { get; set; }
    public int PublicationYear { get; set; }
    [MaxLength(13)]
    public required string ISBN { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }

    public ICollection<UserBook>? UserBooks { get; set; }

}