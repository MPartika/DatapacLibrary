using DatapacLibrary.Domain.DataTransferObjects;

namespace DatapacLibrary.Domain.Contracts;

public  interface IBookLoanRepository : IDependency
{
    /// <summary>
    /// Create a new record in User book table book will be unavailable after this
    /// </summary>
    /// <param name="userId">User's Id</param>
    /// <param name="bookId"></param>
    Task CreateNewLoanAsync(long userId, long bookId);

    /// <summary>
    /// Check if the book is available for lending 
    /// </summary>
    /// <param name="bookId">Book's Id</param>
    /// <returns>Returns true of false</returns>
    Task<bool> IsBookAvailable(long bookId);

    /// <summary>
    /// Get All loans that are past the valid lending period
    /// </summary>
    /// <returns>List of users that should be notified</returns>
    Task<IList<LoanWarningDto>> GetLoansPastReturnTimeAsync();

    /// <summary>
    /// Will mark loan as returned making the book once again available
    /// </summary>
    /// <param name="userId">User's Id</param>
    /// <param name="bookId">Book's Id</param>
    Task ReturnBookAsync(long userId, long bookId);
    /// <summary>
    /// Change valid until date by adding number of days
    /// </summary>
    /// <param name="id"></param>
    /// <param name="numberOfDays"></param>
    /// <returns></returns>
    Task ExtendValidUnitByDays(long id, short numberOfDays);
    /// <summary>
    /// Get current holder of the book or if it was returned
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="bookId"></param>
    /// <returns></returns>
    Task<WasBookReturnedDto?> WasBookReturned(long userId, long bookId);
}
