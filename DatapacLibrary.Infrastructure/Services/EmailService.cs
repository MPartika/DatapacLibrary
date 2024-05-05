using DatapacLibrary.Domain.Contracts;
using MailKit.Net.Smtp;
using MimeKit;
using Serilog;

namespace DatapacLibrary.Infrastructure.Services;

internal class EmailService : IEmailService
{
    public async Task SendEmailNotificationToUser(string userName, string userEmail, string bookTitle, DateTime returnTime)
    {
        try
        {
            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress("Datapac Library", "library@datapac.com"));
            mailMessage.To.Add(new MailboxAddress(userName, userEmail));
            mailMessage.Subject = bookTitle;
            mailMessage.Body = new TextPart("plain")
            {
                Text = $"Book should be here by {returnTime.ToLocalTime():dd.MM.yyyy}!!!! You return book!!!",
            };

            using var smtpClient = new SmtpClient();
            await smtpClient.ConnectAsync("localhost", 1025);
            await smtpClient.SendAsync(mailMessage);
            await smtpClient.DisconnectAsync(true);
        }
        catch (Exception ex)
        {
            Log.Error($"Something went wrong while sending email: {ex.Message}");
        }
    }

}