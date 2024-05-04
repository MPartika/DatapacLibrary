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

    public async Task<UserPasswordDto?> GetUserAsync(string name) => (await _dbContext.Users.FirstOrDefaultAsync(x => x.Name == name))?.ToUserWithPasswordDto();
    public async Task<UserPasswordDto?> GetUserAsync(long id) => (await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id))?.ToUserWithPasswordDto();
    public async Task<IEnumerable<UserDto>> GetAllUsersAsync() =>
        await _dbContext.Users
            .Include(ub => ub.UserBooks)
            .ThenInclude(b => b.Book)
            .Select(x => x.ToUserDto()).ToListAsync();
    public async Task CreateUserAsync(string name, string Email, byte[] password, byte[] salt)
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

    public async Task UpdateUserAsync(long id, string? name, string? email, byte[]? password, byte[]? salt)
    {
        var user = await _dbContext.Users.SingleAsync(x => x.Id == id);
        if (name is not null && user.Name != name)
            user.Name = name;
        if (email is not null && user.Email != email)
            user.Email = email;
        if (password is not null)
            user.Password = password;
        if (salt is not null)
            user.Salt = salt;
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