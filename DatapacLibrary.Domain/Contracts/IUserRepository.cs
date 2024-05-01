using DatapacLibrary.Domain.DataTransferObjects;

namespace DatapacLibrary.Domain.Contracts;

public interface IUserRepository : IDependency
{
    /// <summary>
    /// Get User by it's name for authorization purposes  
    /// </summary>
    /// <param name="name">User's name</param>
    /// <returns>User id, password and salt</returns>
    Task<UserPasswordDto?> FindByNameAsync(string name);
    /// <summary>
    /// Get all Users
    /// </summary>
    /// <returns>User without password and salt</returns>
    Task<IEnumerable<UserDto>> GetAllUsers();
    /// <summary>
    /// Create a user record
    /// </summary>
    /// <param name="name">Users's name</param>
    /// <param name="Email">Users's email</param>
    /// <param name="password">User's password</param>
    /// <param name="salt">Salt generated with password</param>
    Task CreateUser(string name, string Email, byte[] password, byte[] salt);
    /// <summary>
    /// Update users record
    /// </summary>
    /// <param name="id">User's id</param>
    /// <param name="name">Users's name</param>
    /// <param name="email">Users's email</param>
    /// <param name="password">Users's password</param>
    /// <param name="salt">Salt generated with password</param>
    Task UpdateUser(int id, string? name, string? email, byte[]? password, byte[]? salt);
    /// <summary>
    /// Deletes users record
    /// </summary>
    /// <param name="id">User's Id</param>
    Task DeleteUser(int id);
}