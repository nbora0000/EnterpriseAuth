using System.ComponentModel.DataAnnotations;
namespace AuthenticationApi.Domain.Data;

public record KeySecret([property: Key] Guid ClientId, [property: Required][property: StringLength(32)] string? ClientSecret, [property: Required] string? AudId)
{
    public KeySecret() : this(Guid.NewGuid(), null, null) // Provide default values
    {
    }
}
