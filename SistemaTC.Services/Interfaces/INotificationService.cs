namespace SistemaTC.Services.Interfaces;
public interface INotificationService
{
    Task<bool> SendEmail(string to, string subject, string body, bool isHtml = true);
}
