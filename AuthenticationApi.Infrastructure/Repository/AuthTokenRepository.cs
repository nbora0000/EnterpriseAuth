using AuthenticationApi.Application.Interface;
using AuthenticationApi.Infrastructure.Data;
using AuthenticationApi.Infrastructure.CustomLogic;
using System.IdentityModel.Tokens.Jwt;
using AuthenticationApi.Domain.Data;
namespace AuthenticationApi.Infrastructure.Repository
{
    public class AuthTokenRepository(ApplicationDbContext context, SecurityJwtToken jwtToken) : IAuthTokenRepository
    {
        private readonly ApplicationDbContext _context = context;
        private readonly SecurityJwtToken _jwtToken = jwtToken;
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
                JwtSecurityToken JwtSecureToken = _jwtToken.SecurityToken(secret);
                token = new JwtSecurityTokenHandler().WriteToken(JwtSecureToken);
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
