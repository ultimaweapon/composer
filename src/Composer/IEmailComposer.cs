namespace Composer;

using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using MimeKit;

/// <summary>
/// A service to compose email and send it.
/// </summary>
public interface IEmailComposer
{
    /// <summary>
    /// Compose an email and send it to the specified recipient.
    /// </summary>
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
    Task ComposeAsync(InternetAddress recipient, IEmail mail, CultureInfo culture, CancellationToken cancellationToken = default);
}
