using AuthenticationApi.Application.Interface;
using AuthenticationApi.Infrastructure.Data;
using AuthenticationApi.Infrastructure.CustomLogic;
using System.IdentityModel.Tokens.Jwt;
using AuthenticationApi.Domain.Data;
namespace AuthenticationApi.Infrastructure.Repository;
public class AuthTokenRepository(ApplicationDbContext context, SecurityJwtToken jwtToken) : IAuthTokenRepository
{
    private readonly ApplicationDbContext _context = context;
    private readonly SecurityJwtToken _jwtToken = jwtToken;
    public string GenerateToken(KeySecret secret)
    {
        string token = string.Empty;
        KeySecret secretKey = _context.KeySecret.Find(secret.ClientId)
            ?? throw new NullReferenceException($"{secret} is not Present in Database");
        if (secretKey == secret)
        {
            JwtSecurityToken JwtSecureToken = _jwtToken.SecurityToken(secret);
            token = new JwtSecurityTokenHandler().WriteToken(JwtSecureToken);
        }
        return token;
    }
}

