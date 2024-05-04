namespace DatapacLibrary.Domain.Contracts;

public interface IEmailService : IDependency
{
    /// <summary>
    /// Send email to customer notifying him about not returned books
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="userEmail"></param>
    /// <param name="bookTitle"></param>
    /// <param name="returnTime"></param>
    /// <returns></returns>
    Task SendEmailNotificationToUser(string userName, string userEmail, string bookTitle, DateTime returnTime);
}
