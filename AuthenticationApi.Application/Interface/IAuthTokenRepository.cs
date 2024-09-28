using AuthenticationApi.Domain;
namespace AuthenticationApi.Application.Interface;
public interface IAuthTokenRepository
{
    public  KeySecret GenerateSecret(string AudId);
    public  string GenerateToken(KeySecret secret);
    public KeySecret GetSecret(string clientId);
}
