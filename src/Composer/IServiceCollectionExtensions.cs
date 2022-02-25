namespace Microsoft.Extensions.DependencyInjection;

using Composer;

public static class IServiceCollectionExtensions
{
    /// <summary>
    /// Add Composer services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">
    /// The <see cref="IServiceCollection"/> to add Composer services to.
    /// </param>
    /// <returns>
    /// <see cref="ComposerBuilder"/> to configure Composer.
    /// </returns>
    /// <remarks>
    /// The application must also register <see cref="ITemplateProvider"/> and <see cref="IEmailSender"/> to use.
    /// </remarks>
    public static ComposerBuilder AddComposer(this IServiceCollection services)
    {
        services.AddSingleton<IEmailComposer, EmailComposer>();

        return new ComposerBuilder(services);
    }
}
