namespace Microsoft.Extensions.DependencyInjection;

public sealed class ComposerBuilder
{
    public ComposerBuilder(IServiceCollection services)
    {
        this.Services = services;
    }

    public IServiceCollection Services { get; }
}
