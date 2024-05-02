using DatapacLibrary.Infrastructure.DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;

namespace DatapacLibrary.Infrastructure;

public class LibraryDbContext : DbContext
{
    private readonly string _dbPath = Path.Join(GetPath, "Library.db");

    public DbSet<User> Users { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<UserBook> UserBooks { get; set; }

    public LibraryDbContext()
    {
    }

    // Fot Testing
    public LibraryDbContext(string dbPath)
    {
        _dbPath = Path.Join(GetPath, dbPath);
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={_dbPath}").LogTo(Log.Information, LogLevel.Information);


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

    private static string GetPath => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
}
