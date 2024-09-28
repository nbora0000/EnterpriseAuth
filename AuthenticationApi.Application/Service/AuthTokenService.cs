using AuthenticationApi.Domain;
using AuthenticationApi.Application.Interface;
namespace AuthenticationApi.Application.Service { 
public class AuthTokenService :IAuthTokenService
{
    private readonly IAuthTokenRepository _authTokenRepository;
    public AuthTokenService(IAuthTokenRepository authTokenRepository)
    {
        _authTokenRepository = authTokenRepository;
    }
    public KeySecret GenerateSecret(string AudId)
    {
        return _authTokenRepository.GenerateSecret(AudId);
    }
    public  string GenerateToken(KeySecret secret)
    {
        return _authTokenRepository.GenerateToken(secret);
    }
        public KeySecret GetSecret(string clientId)
        {
            return _authTokenRepository.GetSecret(clientId);
        }
    }
}
