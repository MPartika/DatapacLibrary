using DatapacLibrary.Domain.DataTransferObjects;
using DatapacLibrary.Infrastructure.DbEntities;
using DatapacLibrary.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace DatapacLibrary.Infrastructure.Repositories;

internal class UserRepository
{
    private readonly LibraryDbContext _dbContext;

    public UserRepository(LibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserDto?> FindByNameAsync(string name) => (await _dbContext.Users.FirstOrDefaultAsync(x => x.Name == name))?.ToUserDto();
    public async Task<IEnumerable<UserDto>> GetAllUsers() => await _dbContext.Users.Select(x => x.ToUserDto()).ToListAsync();
    public async Task CreateUser(string name, string Email, byte[] password, byte[] salt)
    {
        _dbContext.Add(new User
        {
            Name = name,
            Email = Email,
            Password = password,
            Salt = salt
        });
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateUser(int id, string? name, string? email)
    {
        var user = await _dbContext.Users.SingleAsync(x => x.Id == id);
        if (name is not null && user.Name != name)
            user.Name = name;
        if (email is not null && user.Email != email)
            user.Email = email;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteUser(int id)
    {
        var user = await _dbContext.Users.SingleAsync(x => x.Id == id);
        _dbContext.Remove(user);
        await _dbContext.SaveChangesAsync();
    }
}