using AuthenticationApi.Application.Interface;
using AuthenticationApi.Domain.Data;
namespace AuthenticationApi.Application.Service;
public class AuthTokenService(IAuthTokenRepository authTokenRepository) : IAuthTokenService
{
    private readonly IAuthTokenRepository _authTokenRepository = authTokenRepository;
    public string GenerateToken(KeySecret secret)
    {
        return _authTokenRepository.GenerateToken(secret);
    }
}

