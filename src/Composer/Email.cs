namespace Composer;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MimeKit;

/// <summary>
/// Represents an email. The application must create a new class based on this class to represent each type of email (e.g. RegistrationCompletedEmail).
/// </summary>
public abstract class Email : IEmail
{
    object IEmail.TemplateId => this.TemplateId;

    /// <summary>
    /// Gets the template identifier of this email.
    /// </summary>
    /// <remarks>
    /// Type of the value is depend on template implementation. The application need to return the same type as the template.
    /// </remarks>
    protected abstract object TemplateId { get; }

    ValueTask<IEnumerable<MimeEntity>> IEmail.GetAttachmentsAsync(CancellationToken cancellationToken)
    {
        return this.BuildAttachmentsAsync(cancellationToken);
    }

    ValueTask<object?> IEmail.GetTemplateDataAsync(CancellationToken cancellationToken)
    {
        return this.BuildTemplateDataAsync(cancellationToken);
    }

    /// <summary>
    /// Build a data to inject into the template.
    /// </summary>
    /// <returns>
    /// A data to inject into the template or <c>null</c> if there is none.
    /// </returns>
    /// <remarks>
    /// This implementation always return <c>null</c>.
    /// </remarks>
    protected virtual object? BuildTemplateData()
    {
        return null;
    }

    /// <summary>
    /// Build a data to inject into the template.
    /// </summary>
    /// <param name="cancellationToken">
    /// The token to monitor for cancellation requests.
    /// </param>
    /// <returns>
    /// A data to inject into the template or <c>null</c> if there is none.
    /// </returns>
    /// <remarks>
    /// This implementation always get a return value from <see cref="BuildTemplateData"/>.
    /// </remarks>
    protected virtual ValueTask<object?> BuildTemplateDataAsync(CancellationToken cancellationToken = default)
    {
        return new ValueTask<object?>(this.BuildTemplateData());
    }

    /// <summary>
    /// Build a collection of attachments.
    /// </summary>
    /// <returns>
    /// A list of attachments.
    /// </returns>
    /// <remarks>
    /// To provides attachments, override this method then use <see cref="AttachmentCollection"/> to build a list of attachments. This
    /// implementation always return an empty list.
    /// </remarks>
    protected virtual IEnumerable<MimeEntity> BuildAttachments()
    {
        return Enumerable.Empty<MimeEntity>();
    }

    /// <summary>
    /// Build a collection of attachments.
    /// </summary>
    /// <param name="cancellationToken">
    /// The token to monitor for cancellation requests.
    /// </param>
    /// <returns>
    /// A list of attachments.
    /// </returns>
    /// <remarks>
    /// To provides attachments, override this method then use <see cref="AttachmentCollection"/> to build a list of attachments. This
    /// implementation always get the return value from <see cref="BuildAttachments"/>.
    /// </remarks>
    protected virtual ValueTask<IEnumerable<MimeEntity>> BuildAttachmentsAsync(CancellationToken cancellationToken = default)
    {
        return new ValueTask<IEnumerable<MimeEntity>>(this.BuildAttachments());
    }
}
