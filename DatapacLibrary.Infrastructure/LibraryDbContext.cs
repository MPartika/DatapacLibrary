using DatapacLibrary.Infrastructure.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace DatapacLibrary.Infrastructure;

public class LibraryDbContext : DbContext
{
    private readonly string _dbPath;

    public DbSet<User> Users { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<UserBook> UserBooks { get; set; }

    public LibraryDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        _dbPath = Path.Join(path, "Library.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={_dbPath}");


    public override int SaveChanges()
    {
        var entities = ChangeTracker
            .Entries()
            .Where(entity =>
                entity.Entity is IDbEntity
                && (entity.State == EntityState.Added || entity.State == EntityState.Modified)
            );
        foreach (var entity in entities)
        {
            if (entity.State == EntityState.Added)
                ((IDbEntity)entity.Entity).Created = DateTime.UtcNow;
            ((IDbEntity)entity.Entity).Updated = DateTime.UtcNow;
        }
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entities = ChangeTracker
            .Entries()
            .Where(entity =>
                entity.Entity is IDbEntity
                && (entity.State == EntityState.Added || entity.State == EntityState.Modified)
            );
        foreach (var entity in entities)
        {
            if (entity.State == EntityState.Added)
                ((IDbEntity)entity.Entity).Created = DateTime.UtcNow;
            ((IDbEntity)entity.Entity).Updated = DateTime.UtcNow;
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}
