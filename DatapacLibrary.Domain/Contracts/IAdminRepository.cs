using DatapacLibrary.Domain.DataTransferObjects;

namespace DatapacLibrary.Domain.Contracts;

public interface IAdminRepository : IDependency
{
    /// <summary>
    /// Get Admin By Name
    /// </summary>
    /// <param name="name"></param>
    /// <returns>Admin DTO</returns>
    Task<AdminDto?> GetAdminAsync(string name);
    /// <summary>
    /// Create new Admin
    /// </summary>
    /// <param name="name"></param>
    /// <param name="password"></param>
    /// <param name="salt"></param>
    /// <returns></returns>
    Task CreateAdminAsync(string name, byte[] password, byte[] salt);
    /// <summary>
    /// Delete Admin
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    Task DeleteAdminAsync(long Id);
}