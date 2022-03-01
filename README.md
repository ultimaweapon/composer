# Composer
[![Nuget](https://img.shields.io/nuget/dt/Composer)](https://www.nuget.org/packages/Composer)

This is a framework for .NET to compose an email from a configured template so the email format can be update on the fly without updating the application code.

## Usage

First add essential services to `IServiceCollection` by invoke `AddComposer` extension method:

```csharp
services.AddComposer();
```

`AddComposer` return an object for configure Composer. The application required to provider `IEmailSender` and `ITemplateProvider` by invoke `AddSender`
and `AddTemplateProvider`:

```csharp
services
    .AddComposer()
    .AddSender<SenderImplementation>()
    .AddTemplateProvider<TemplateProviderImplementation>();
```

### SMTP sender

SMTP sender is shipped with Composer so you don't need to install additional package to use it. To use SMTP sender invoke `AddSmtpSender`:

```csharp
services
    .AddComposer()
    .AddSmtpSender(options =>
    {
        options.SmtpServer = "host";
        options.SmtpPort = 25;
    })
    .AddTemplateProvider<TemplateProviderImplementation>();
```

### Amazon SES sender

For Amazone SES you need to install [Composer.Aws](https://www.nuget.org/packages/Composer.Aws) then invoke `AddAmazonSimpleEmailService`:

```csharp
services
    .AddComposer()
    .AddAmazonSimpleEmailService()
    .AddTemplateProvider<TemplateProviderImplementation>();
```

You also need to allow `ses:SendRawEmail` for IAM role that used by your application.

## License

MIT
