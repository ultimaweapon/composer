namespace Composer;

using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using MimeKit;

public sealed class EmailComposer : IEmailComposer
{
    private readonly ITemplateProvider provider;
    private readonly IEmailSender sender;

    public EmailComposer(ITemplateProvider provider, IEmailSender sender)
    {
        this.provider = provider;
        this.sender = sender;
    }

    public async Task ComposeAsync(InternetAddress recipient, IEmail mail, CultureInfo culture, CancellationToken cancellationToken = default)
    {
        // Load template.
        var template = await this.provider.GetByIdAsync(mail.TemplateId, culture, cancellationToken);

        if (template == null)
        {
            throw new ArgumentException($"No template with identifier {mail.TemplateId} and culture {culture}.", nameof(mail));
        }

        // Construct message.
        using var message = new MimeMessage();
        var data = await mail.GetTemplateDataAsync(cancellationToken);
        var attachments = await mail.GetAttachmentsAsync(cancellationToken);

        try
        {
            message.From.Add(template.Sender);
            message.To.Add(recipient);
            message.Subject = await template.BuildSubjectAsync(message.To, data, cancellationToken);
            message.Body = await template.BuildBodyAsync(message.To, data, attachments, cancellationToken);
        }
        catch
        {
            foreach (var attachment in attachments)
            {
                attachment.Dispose();
            }

            throw;
        }

        // Send email.
        await this.sender.SendAsync(message, cancellationToken);
    }
}
