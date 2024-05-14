using Microsoft.Extensions.Logging;
using SistemaTC.Core;
using SistemaTC.Core.Extensions;
using SistemaTC.Services.Interfaces;
using System.Net;
using System.Net.Mail;

namespace SistemaTC.Services;
public class NotificationService(ILogger<NotificationService> logger, AppSettings appSettings): INotificationService
{
    private readonly MailSettings mailSettings = appSettings.Mail;

    public async Task<bool> SendEmail(string to, string subject, string body, bool isHtml = true)
    {
        MailMessage mailMessage = new(mailSettings.UserName, to) { 
            Subject = subject,
            Body = body,
            IsBodyHtml = isHtml
        };

        using SmtpClient smtpClient = new(mailSettings.Host, mailSettings.Port) {
            Credentials = new NetworkCredential(mailSettings.UserName, mailSettings.Password),
            EnableSsl = true
        };

        try
        {
            await smtpClient.SendMailAsync(mailMessage);
            return true;
        }
        catch (Exception e)
        {
            var errorMessage = e.GetLastException();
            logger.LogError("Error sending mail. Error: {errorMessage}. Message: {message}", errorMessage, mailMessage);
            return false;
        }
    }
}
