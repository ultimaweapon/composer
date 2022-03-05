# Composer
[![Nuget](https://img.shields.io/nuget/v/Composer)](https://www.nuget.org/packages/Composer)

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

### Available sender

- [Amazone SES](https://github.com/ultimicro/composer-aws)

### Available template provider

- [StringTemplate 4](https://github.com/ultimicro/composer-stringtemplate)

### Define an email

You need to create a new class that derived from `Email` class. Each class represents one type of email you want to send (e.g. an email to send when user has
signed up).

```csharp
namespace SampleApp;

using System;
using Composer;

internal sealed class RegistrationCompletedEmail : Email
{
    public static readonly Guid Id = new Guid("e8c01835-bc01-4f03-b4f7-197d0d0a3b4a");

    public RegistrationCompletedEmail(string username)
    {
        this.Username = username;
    }

    public string Username { get; }

    protected override object TemplateId => Id;

    protected override object? BuildTemplateData() => new { this.Username };
}
```

### Send an email

Inject `IEmailComposer` to the class you want to send email. Then invoke `ComposeAsync`:

```csharp
var email = new RegistrationCompletedEmail("john");

await composer.ComposeAsync("john@example.com", email, cancellationToken);
```

### Add attachments

Override `Email.BuildAttachments` or `Email.BuildAttachmentsAsync` to provide attachments for the email.

## License

MIT
