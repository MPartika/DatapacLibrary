using DatapacLibrary.Domain.DataTransferObjects;

namespace DatapacLibrary.Domain.Contracts;

public interface IUserRepository
{
    Task<UserDto?> FindByNameAsync(string name);
    Task<IEnumerable<UserDto>> GetAllUsers();
    Task CreateUser(string name, string Email, byte[] password, byte[] salt);
    Task UpdateUser(int id, string? name, string? email);
    Task DeleteUser(int id);
}