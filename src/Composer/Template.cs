namespace Composer;

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MimeKit;

/// <summary>
/// Represents an email template.
/// </summary>
public abstract class Template
{
    protected Template(CultureInfo culture, MailboxAddress sender)
    {
        this.Culture = culture;
        this.Sender = sender;
    }

    public CultureInfo Culture { get; }

    public MailboxAddress Sender { get; }

    public abstract ValueTask<string> BuildSubjectAsync(IEnumerable<InternetAddress> recipients, object? data, CancellationToken cancellationToken = default);

    /// <summary>
    /// Build an email body.
    /// </summary>
    /// <param name="recipients">
    /// Recipients of the email.
    /// </param>
    /// <param name="data">
    /// Data of the email.
    /// </param>
    /// <param name="attachments">
    /// Attachments of the email.
    /// </param>
    /// <param name="cancellationToken">
    /// The token to monitor for cancellation requests.
    /// </param>
    /// <returns>
    /// The email body.
    /// </returns>
    /// <remarks>
    /// The ownership of attachments provided in <paramref name="attachments"/> will be transferred to the return value.
    /// </remarks>
    public async ValueTask<MimeEntity> BuildBodyAsync(
        IEnumerable<InternetAddress> recipients,
        object? data,
        IEnumerable<MimeEntity> attachments,
        CancellationToken cancellationToken = default)
    {
        var builder = new BodyBuilder()
        {
            TextBody = await this.BuildPlainMessageAsync(recipients, data, cancellationToken),
        };

        var combined = await this.GetAttachmentsAsync(recipients, data, attachments, cancellationToken);

        try
        {
            foreach (var attachment in combined)
            {
                builder.Attachments.Add(attachment);
            }

            // The return value of ToMessageBody() will take ownership of attachments.
            return builder.ToMessageBody();
        }
        catch
        {
            // Dispose only template attachment.
            foreach (var attachment in combined)
            {
                if (!attachments.Any(a => ReferenceEquals(a, attachment)))
                {
                    attachment.Dispose();
                }
            }

            throw;
        }
    }

    /// <summary>
    /// Build a plain message from the specified data.
    /// </summary>
    /// <param name="recipients">
    /// Recipients of the email.
    /// </param>
    /// <param name="data">
    /// Data of the email.
    /// </param>
    /// <param name="cancellationToken">
    /// The token to monitor for cancellation requests.
    /// </param>
    /// <returns>
    /// A plain message or <c>null</c> if this template does not provide plain message.
    /// </returns>
    protected abstract ValueTask<string?> BuildPlainMessageAsync(
        IEnumerable<InternetAddress> recipients,
        object? data,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the attachments for the email.
    /// </summary>
    /// <param name="recipients">
    /// Recipients of the email.
    /// </param>
    /// <param name="data">
    /// Data of the email to inject to the template.
    /// </param>
    /// <param name="externals">
    /// Attachments of the email.
    /// </param>
    /// <param name="cancellationToken">
    /// The token to monitor for cancellation requests.
    /// </param>
    /// <returns>
    /// A list of attachments.
    /// </returns>
    /// <remarks>
    /// The template can override this method to provides additional attachments specific to the template. This implementation always return
    /// <paramref name="externals"/>.
    /// </remarks>
    protected virtual ValueTask<IEnumerable<MimeEntity>> GetAttachmentsAsync(
        IEnumerable<InternetAddress> recipients,
        object? data,
        IEnumerable<MimeEntity> externals,
        CancellationToken cancellationToken = default)
    {
        return new ValueTask<IEnumerable<MimeEntity>>(externals);
    }
}
