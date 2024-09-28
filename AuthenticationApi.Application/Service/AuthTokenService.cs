using AuthenticationApi.Domain;
using AuthenticationApi.Application.Interface;
using AuthenticationApi.Domain.Data;
namespace AuthenticationApi.Application.Service
{
    public class AuthTokenService(IAuthTokenRepository authTokenRepository) : IAuthTokenService
    {
        private readonly IAuthTokenRepository _authTokenRepository = authTokenRepository;
        public KeySecret GenerateSecret(string AudId)
        {
            return _authTokenRepository.GenerateSecret(AudId);
        }
        public string GenerateToken(KeySecret secret)
        {
            return _authTokenRepository.GenerateToken(secret);
        }
        public KeySecret GetSecret(string clientId)
        {
            return _authTokenRepository.GetSecret(clientId);
        }
    }
}
