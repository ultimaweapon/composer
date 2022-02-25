namespace Microsoft.Extensions.DependencyInjection;

using System;
using Composer;

public static class ComposerBuilderExtensions
{
    /// <summary>
    /// Register a template provider to use.
    /// </summary>
    /// <typeparam name="T">
    /// An <see cref="ITemplateProvider"/> implementation to use.
    /// </typeparam>
    /// <param name="builder">
    /// The builder to register to.
    /// </param>
    /// <returns>
    /// The <paramref name="builder"/> to chain the call.
    /// </returns>
    public static ComposerBuilder AddTemplateProvider<T>(this ComposerBuilder builder)
        where T : class, ITemplateProvider
    {
        builder.Services.AddSingleton<ITemplateProvider, T>();

        return builder;
    }

    /// <summary>
    /// Register <see cref="SmtpSender"/> as an email sender.
    /// </summary>
    /// <param name="builder">
    /// The builder to register to.
    /// </param>
    /// <param name="options">
    /// A delegate to configure the options.
    /// </param>
    /// <returns>
    /// The <paramref name="builder"/> to chain the call.
    /// </returns>
    public static ComposerBuilder AddSmtpSender(this ComposerBuilder builder, Action<SmtpSenderOptions> options)
    {
        builder.Services.Configure(options);
        builder.AddSender<SmtpSender>();

        return builder;
    }

    /// <summary>
    /// Register an email sender to use.
    /// </summary>
    /// <typeparam name="T">
    /// An <see cref="IEmailSender"/> to use.
    /// </typeparam>
    /// <param name="builder">
    /// The builder to register to.
    /// </param>
    /// <returns>
    /// <paramref name="builder"/> to chain the call.
    /// </returns>
    public static ComposerBuilder AddSender<T>(this ComposerBuilder builder)
        where T : class, IEmailSender
    {
        builder.Services.AddSingleton<IEmailSender, T>();

        return builder;
    }
}
