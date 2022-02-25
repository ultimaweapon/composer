namespace Composer;

using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// A service to provide <see cref="Template"/>.
/// </summary>
public interface ITemplateProvider
{
    /// <summary>
    /// Get a template by template identifier.
    /// </summary>
    /// <param name="id">
    /// The identifier of the template.
    /// </param>
    /// <param name="culture">
    /// The culture of the template to get.
    /// </param>
    /// <param name="cancellationToken">
    /// The token to monitor for cancellation requests.
    /// </param>
    /// <returns>
    /// A <see cref="Template"/> of the specified <paramref name="id"/> with culture specified by <paramref name="culture"/> or <c>null</c>
    /// if the specified template does not exists.
    /// </returns>
    /// <remarks>
    /// If the specified culture does not exists, the implementation should return a fallback instead of <c>null</c> (e.g. return an invariant
    /// culture template).
    /// </remarks>
    ValueTask<Template?> GetByIdAsync(object id, CultureInfo culture, CancellationToken cancellationToken = default);
}
