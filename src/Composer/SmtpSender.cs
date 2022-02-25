namespace Composer;

using System.Threading;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

/// <summary>
/// <see cref="IEmailSender"/> implementation to send email over SMTP.
/// </summary>
public sealed class SmtpSender : IEmailSender
{
    private readonly SmtpSenderOptions options;

    public SmtpSender(IOptions<SmtpSenderOptions> options)
    {
        this.options = options.Value;
    }

    public async Task SendAsync(MimeMessage mail, CancellationToken cancellationToken = default)
    {
        using var client = new SmtpClient();

        await client.ConnectAsync(this.options.SmtpServer, this.options.SmtpPort, cancellationToken: cancellationToken);
        await client.SendAsync(mail, cancellationToken);
        await client.DisconnectAsync(true);
    }
}
