using System.Text;

namespace TeraBank.Application.Options;

public class AuthenticationOption
{
    public const string OptionKey = nameof(AuthenticationOption);

    public string EncryptionKey { get; init; }
    public string Issuer { get; init; }
    public string Audience { get; init; }
    public int ExpirationSeconds { get; init; }
    public ReadOnlySpan<byte> EncruptionKeyBytes => Encoding.UTF8.GetBytes(EncryptionKey);
}
