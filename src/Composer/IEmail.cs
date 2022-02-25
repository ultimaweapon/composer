namespace Composer;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MimeKit;

/// <summary>
/// Represents an email to compose. Application should derive from <see cref="Email"/> instead of this interface.
/// </summary>
public interface IEmail
{
    /// <summary>
    /// Gets the template identifier of this email.
    /// </summary>
    /// <remarks>
    /// Type of the value is depend on template implementation. The application need to return the same type as the template.
    /// </remarks>
    object TemplateId { get; }

    /// <summary>
    /// Get a data to inject into the template.
    /// </summary>
    /// <param name="cancellationToken">
    /// The token to monitor for cancellation requests.
    /// </param>
    /// <returns>
    /// A data to inject into the template or <c>null</c> if there is none.
    /// </returns>
    ValueTask<object?> GetTemplateDataAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a collection of attachments.
    /// </summary>
    /// <param name="cancellationToken">
    /// The token to monitor for cancellation requests.
    /// </param>
    /// <returns>
    /// A list of attachments.
    /// </returns>
    ValueTask<IEnumerable<MimeEntity>> GetAttachmentsAsync(CancellationToken cancellationToken = default);
}
