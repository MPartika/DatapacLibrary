using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatapacLibrary.Infrastructure.DbEntities;

public class UserBook : IDbEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int UserId { get; set; }
    public int BookId { get; set; }
    public DateTime ValidUntil { get; set; }
    public bool Returned { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }

    public User? Users { get; set; }
    public Book? Books { get; set; }
}