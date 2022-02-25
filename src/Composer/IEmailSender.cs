namespace Composer;

using System.Threading;
using System.Threading.Tasks;
using MimeKit;

/// <summary>
/// A service to send <see cref="MimeMessage"/>. This is a low-level interface and most application should use <see cref="IEmailComposer"/> instead.
/// </summary>
public interface IEmailSender
{
    Task SendAsync(MimeMessage mail, CancellationToken cancellationToken = default);
}
