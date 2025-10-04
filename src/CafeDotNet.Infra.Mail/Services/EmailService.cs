using CafeDotNet.Infra.Mail.Interfaces;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using CafeDotNet.Infra.Mail.DTOs;

namespace CafeDotNet.Infra.Mail.Services;

public class EmailService : IEmailService
{
    private readonly EmailSettings _settings;

    public EmailService(IOptions<EmailSettings> settings)
    {
        _settings = settings.Value;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        using var message = new MailMessage();
       
        message.From = new MailAddress(_settings.SenderEmail, _settings.SenderName);
        message.To.Add(to);
        message.Subject = subject;
        message.Body = body;
        message.IsBodyHtml = true;

        using var client = new SmtpClient(_settings.SmtpServer, _settings.Port)
        {
            Credentials = new NetworkCredential(_settings.Username, _settings.Password),
            EnableSsl = true
        };

        await client.SendMailAsync(message);
    }

    public string CreateEmailBody(string name, string email, string message)
    {
        return $@"
            <h2>Contato do site Cafe.Net</h2>
            <p><strong>Nome:</strong> {WebUtility.HtmlEncode(name)}</p>
            <p><strong>Email:</strong> {WebUtility.HtmlEncode(email)}</p>
            <p><strong>Mensagem:</strong></p>
            <p>{WebUtility.HtmlEncode(message).Replace("\n", "<br />")}</p>
        ";
    }
}