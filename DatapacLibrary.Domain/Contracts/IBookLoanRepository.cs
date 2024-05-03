﻿using DatapacLibrary.Domain.DataTransferObjects;

namespace DatapacLibrary.Domain.Contracts;

public  interface IBookLoanRepository
{
    /// <summary>
    /// Create a new record in User book table book will be unavailable after this
    /// </summary>
    /// <param name="userId">User's Id</param>
    /// <param name="bookId"></param>
    Task CreateNewLoan(long userId, long bookId);

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
    Task<IEnumerable<LoanWarningDto>> GetLoansPastDueTime();

    /// <summary>
    /// Will mark loan as returned making the book once again available
    /// </summary>
    /// <param name="userId">User's Id</param>
    /// <param name="bookId">Book's Id</param>
    Task ReturnBook(long userId, long bookId);
}
