namespace CafeDotNet.Infra.Mail.Interfaces;

public interface IEmailService
{
    string CreateEmailBody(string name, string email, string message);
    Task SendEmailAsync(string to, string subject, string body);
}
