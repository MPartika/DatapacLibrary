using DatapacLibrary.Domain.DataTransferObjects;

namespace DatapacLibrary.Domain.Contracts;

public interface IUserRepository : IDependency
{
    /// <summary>
    /// Get User by it's name 
    /// </summary>
    /// <param name="name">User's name</param>
    /// <returns>User object</returns>
    Task<UserDto?> GetUserAsync(string name);
    /// <summary>
    /// Get User by it's Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>UserDto</returns>
    Task<UserDto?> GetUserAsync(long id);
    /// <summary>
    /// Get all Users
    /// </summary>
    /// <returns>User without password and salt</returns>
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    /// <summary>
    /// Create a user record
    /// </summary>
    /// <param name="name">Users's name</param>
    /// <param name="Email">Users's email</param>
    Task CreateUserAsync(string name, string Email);
    /// <summary>
    /// Update users record
    /// </summary>
    /// <param name="id">User's id</param>
    /// <param name="name">Users's name</param>
    /// <param name="email">Users's email</param>
    Task UpdateUserAsync(long id, string? name, string? email);
    /// <summary>
    /// Deletes users record
    /// </summary>
    /// <param name="id">User's Id</param>
    Task DeleteUserAsync(long id);
}