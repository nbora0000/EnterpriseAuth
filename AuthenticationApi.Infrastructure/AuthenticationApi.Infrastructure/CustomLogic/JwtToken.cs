using AuthenticationApi.Domain;
using AuthenticationApi.Domain.Constants;
using AuthenticationApi.Infrastructure.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
namespace AuthenticationApi.Infrastructure.AuthenticationApi.Infrastructure.CustomLogic
{
    public class JwtToken
    {
        private readonly ApplicationDbContext _context;
        public JwtToken(ApplicationDbContext context)
        {
            _context = context;
        }
        public JwtSecurityToken SecurityToken(KeySecret secret)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret.ClientSecret ?? string.Empty));
            SigningCredentials cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: ApiConstant.Issuer,
                audience: secret.ClientId.ToString(),
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: cred);
            return token;
        }

    }
}
