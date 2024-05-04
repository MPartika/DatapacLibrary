using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatapacLibrary.Infrastructure.DbEntities;

public class UserBook : IDbEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public long UserId { get; set; }
    public long BookId { get; set; }
    public DateTime ValidUntil { get; set; }
    public bool Returned { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }

    #pragma warning disable CS8618
    public User User { get; set; }
    [ForeignKey(nameof(BookId))]
    public Book Book { get; set; }
}