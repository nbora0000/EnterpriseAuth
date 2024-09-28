using AuthenticationApi.Application.Interface;
using AuthenticationApi.Domain;
using AuthenticationApi.Infrastructure.Data;
using AuthenticationApi.Infrastructure.CustomLogic;
using AuthenticationApi.Infrastructure.AuthenticationApi.Infrastructure.CustomLogic;
namespace AuthenticationApi.Infrastructure.Repository
{
    public class AuthTokenRepository : IAuthTokenRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtToken _jwtToken;
        public AuthTokenRepository(ApplicationDbContext context,JwtToken jwtToken)
        {
            _context = context;
            _jwtToken = jwtToken;
        }
        public KeySecret GenerateSecret(string AudId)
        {
            KeySecret secret = Credentials.GenerateSecret(AudId);
            _context.KeySecret.Add(secret);
            _context.SaveChanges();
            return secret;
        }
        public string GenerateToken(KeySecret secret)
        {
            string token = string.Empty;
            KeySecret secretKey = _context.KeySecret.Find(secret.ClientId) ?? throw new NullReferenceException($"{secret} is not Present in Database");
            if (secretKey.ClientSecret == secret.ClientSecret && secretKey.ClientId == secret.ClientId && secretKey.AudId == secret.AudId)
            {
                token = _jwtToken.SecurityToken(secret).ToString();
            }
            return token;
        }
        public KeySecret GetSecret(string clientId)
        {
            Guid id = Guid.Parse(clientId);
            KeySecret secret = _context.KeySecret.Find(id)?? throw new NullReferenceException($"{clientId} Not found in database");
            return secret ?? new KeySecret();
        }
    }
}
