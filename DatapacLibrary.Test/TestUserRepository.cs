using DatapacLibrary.Infrastructure.Repositories;
using DatapacLibrary.Infrastructure;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using DatapacLibrary.Infrastructure.DbEntities;
using DatapacLibrary.Domain;

namespace DatapacLibrary.Test;

[TestFixture]
public class TestUserRepository
{
    private LibraryDbContext _dbContext;
    private User _user = new User { Name = "Test", Email = "", Password = [], Salt = [] };
    [SetUp]
    public void Init()
    {

        _dbContext = new LibraryDbContext("UnitTestDb.db");
        _dbContext.Database.EnsureDeleted();
        _dbContext.Database.Migrate();
    }

    [TearDown]
    public void Cleanup()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Dispose();
    }

    [Test]
    public async Task ShouldGetUserByName()
    {
        var repository = new UserRepository(_dbContext);
        _dbContext.Add(_user);
        _dbContext.SaveChanges();
        var user = await repository.GetUserAsync(_user.Name);

        Assert.IsNotNull(user);
    }

    [Test]
    public async Task ShouldCreateUser()
    {
        var repository = new UserRepository(_dbContext);
        await repository.CreateUserAsync(_user.Name, "", Array.Empty<byte>(), Array.Empty<byte>());

        var user = _dbContext.Users.FirstOrDefault(x => x.Name == _user.Name);
        Assert.IsNotNull(user);
    }

    [Test]
    public async Task ShouldUpdateUserEmail()
    {
        var repository = new UserRepository(_dbContext);
        _dbContext.Add(_user);
        _dbContext.SaveChanges();

        await repository.UpdateUserAsync(_user.Id, null, "test", null, null);

        Assert.True(_dbContext.Users.Any(x => x.Email == "test"));
    }

    [Test]
    public async Task ShouldUpdateUserName()
    {
        var repository = new UserRepository(_dbContext);
        _dbContext.Add(_user);
        _dbContext.SaveChanges();

        await repository.UpdateUserAsync(_user.Id, "test", null, null, null);

        Assert.True(_dbContext.Users.Any(x => x.Name == "test"));
    }

    [Test]
    public async Task ShouldUpdateUserPassword()
    {
        var repository = new UserRepository(_dbContext);
        _dbContext.Add(_user);
        _dbContext.SaveChanges();

        var password = AuthenticationHelper.HashPassword("test", out byte[] salt);

        await repository.UpdateUserAsync(_user.Id, "test", null, password, salt);

        Assert.True(_dbContext.Users.Any(x => x.Password == password));
    }

    [Test]
    public async Task ShouldDeleteUser()
    {
        var repository = new UserRepository(_dbContext);
        _dbContext.Add(_user);
        _dbContext.SaveChanges();

        await repository.DeleteUserAsync(_user.Id);

        Assert.False(_dbContext.Users.Any(x => x.Id == _user.Id));
    }
}