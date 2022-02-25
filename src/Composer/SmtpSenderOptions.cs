namespace Composer;

/// <summary>
/// Options for <see cref="SmtpSender"/>.
/// </summary>
public sealed class SmtpSenderOptions
{
    public string SmtpServer { get; set; } = string.Empty;

    public int SmtpPort { get; set; }
}
