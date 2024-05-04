namespace DatapacLibrary.Domain.Contracts;

public interface IEmailService : IDependency
{
    Task SendEmailNotificationToUser(string userName, string userEmail, string bookTitle, DateTime returnTime);
}
