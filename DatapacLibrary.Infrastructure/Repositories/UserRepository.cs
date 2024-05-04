using DatapacLibrary.Domain.Contracts;
using DatapacLibrary.Domain.DataTransferObjects;
using DatapacLibrary.Infrastructure.DbEntities;
using DatapacLibrary.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace DatapacLibrary.Infrastructure.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly LibraryDbContext _dbContext;

    public UserRepository(LibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserDto?> GetUserAsync(string name) => (await _dbContext.Users.FirstOrDefaultAsync(x => x.Name == name))?.ToUserDto();
    public async Task<UserDto?> GetUserAsync(long id) => (await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id))?.ToUserDto();
    public async Task<IEnumerable<UserDto>> GetAllUsersAsync() =>
        await _dbContext.Users
            .Include(ub => ub.UserBooks)
            .ThenInclude(b => b.Book)
            .Select(x => x.ToUserDto()).ToListAsync();
    public async Task CreateUserAsync(string name, string Email)
    {
        _dbContext.Add(new User
        {
            Name = name,
            Email = Email
        });
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(long id, string? name, string? email)
    {
        var user = await _dbContext.Users.SingleAsync(x => x.Id == id);
        if (name is not null && user.Name != name)
            user.Name = name;
        if (email is not null && user.Email != email)
            user.Email = email;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(long id)
    {
        var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == id);
        if (user == null)
            return;
        _dbContext.Remove(user);
        await _dbContext.SaveChangesAsync();
    }
}