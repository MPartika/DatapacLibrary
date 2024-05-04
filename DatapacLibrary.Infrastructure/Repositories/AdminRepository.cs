using DatapacLibrary.Domain.Contracts;
using DatapacLibrary.Domain.DataTransferObjects;
using DatapacLibrary.Infrastructure.DbEntities;
using DatapacLibrary.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace DatapacLibrary.Infrastructure.Repositories;

internal class AdminRepository : IAdminRepository
{
    private readonly LibraryDbContext _dbContext;

    public AdminRepository(LibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AdminDto?> GetAdminAsync(string name) => (await _dbContext.Admins.FirstOrDefaultAsync(x => x.Name == name))?.ToAdminDto();


    public async Task CreateAdminAsync(string name, byte[] password, byte[] salt)
    {
        _dbContext.Add(new Admin { Name = name, Password= password ,Salt= salt });
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAdminAsync(long Id)
    {
        var admin = await _dbContext.Admins.SingleOrDefaultAsync(x => x.Id == Id);
        if (admin == null)
            return;
        _dbContext.Remove(admin);
        await _dbContext.SaveChangesAsync();
    }
}