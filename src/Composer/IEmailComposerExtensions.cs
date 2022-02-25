namespace Composer;

using System.Globalization;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using MimeKit;

public static class IEmailComposerExtensions
{
    /// <summary>
    /// Compose an email and send it to the specified recipient.
    /// </summary>
    /// <param name="composer">
    /// The composer to use.
    /// </param>
    /// <param name="recipient">
    /// The recipient of the email.
    /// </param>
    /// <param name="mail">
    /// The email to send.
    /// </param>
    /// <param name="cancellationToken">
    /// The token to monitor for cancellation requests.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    public static Task ComposeAsync(this IEmailComposer composer, string recipient, IEmail mail, CancellationToken cancellationToken = default)
    {
        return composer.ComposeAsync(MailboxAddress.Parse(recipient), mail, cancellationToken);
    }

    /// <summary>
    /// Compose an email and send it to the specified recipient.
    /// </summary>
    /// <param name="composer">
    /// The composer to use.
    /// </param>
    /// <param name="recipient">
    /// The recipient of the email.
    /// </param>
    /// <param name="mail">
    /// The email to send.
    /// </param>
    /// <param name="cancellationToken">
    /// The token to monitor for cancellation requests.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    public static Task ComposeAsync(this IEmailComposer composer, MailAddress recipient, IEmail mail, CancellationToken cancellationToken = default)
    {
        return composer.ComposeAsync((MailboxAddress)recipient, mail, cancellationToken);
    }

    /// <summary>
    /// Compose an email and send it to the specified recipient.
    /// </summary>
    /// <param name="composer">
    /// The composer to use.
    /// </param>
    /// <param name="recipient">
    /// The recipient of the email.
    /// </param>
    /// <param name="mail">
    /// The email to send.
    /// </param>
    /// <param name="culture">
    /// The culture to use when composing email.
    /// </param>
    /// <param name="cancellationToken">
    /// The token to monitor for cancellation requests.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    public static Task ComposeAsync(
        this IEmailComposer composer,
        MailAddress recipient,
        IEmail mail,
        CultureInfo culture,
        CancellationToken cancellationToken = default)
    {
        return composer.ComposeAsync((MailboxAddress)recipient, mail, culture, cancellationToken);
    }

    /// <summary>
    /// Compose an email and send it to the specified recipient.
    /// </summary>
    /// <param name="composer">
    /// The composer to use.
    /// </param>
    /// <param name="recipient">
    /// The recipient of the email.
    /// </param>
    /// <param name="mail">
    /// The email to send.
    /// </param>
    /// <param name="cancellationToken">
    /// The token to monitor for cancellation requests.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    public static Task ComposeAsync(this IEmailComposer composer, InternetAddress recipient, IEmail mail, CancellationToken cancellationToken = default)
    {
        return composer.ComposeAsync(recipient, mail, CultureInfo.CurrentCulture, cancellationToken);
    }
}
