using DatapacLibrary.ApplicationCore.Commands;
using DatapacLibrary.ApplicationCore.Handlers;
using DatapacLibrary.Domain;
using DatapacLibrary.Domain.Contracts;
using DatapacLibrary.Domain.DataTransferObjects;
using Microsoft.Extensions.Configuration;
using Moq;

namespace DatapacLibrary.Test;

public class TestUserHandlers
{
    private Mock<IAdminRepository> _userRepositoryMock = new();

    [Test]
    public async Task TestAuthenticateAdminHandler()
    {
        var password = "password";
        var command = new AuthenticateAdminCommand { Name = "Test", Password = password };
        var dbPassword = AuthenticationHelper.HashPassword(password, out byte[] salt);
        _userRepositoryMock.Setup(x => x.GetAdminAsync(command.Name)).ReturnsAsync(new AdminDto { Name= command.Name, Password = dbPassword, Salt = salt });
        var inMemorySettings = new Dictionary<string, string?> 
        {
            {"JwtSettings:Issuer", "Issuer"},
            {"JwtSettings:Key", "MFwwDQYJKoZIhvcNAQEBBQADSwAwSAJBAOsnDMt7pfNeG69VPOT7rrAzDrrGM4zBl8NcNsWDLq8bv/O38mo1+1fKA0mF2u9YbSxXnmSp1iDkM/vCnllIPHcCAwEAAQ=="},
            {"JwtSettings:Audience:0", "test"}
        };
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        var handler = new AuthenticateUserHandler(_userRepositoryMock.Object, configuration);
        var result = await handler.Handle(command, CancellationToken.None);
        Assert.That(result, Is.Not.Empty);
    }
}