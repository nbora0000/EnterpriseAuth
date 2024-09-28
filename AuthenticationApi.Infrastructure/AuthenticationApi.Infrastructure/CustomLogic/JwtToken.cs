using AuthenticationApi.Domain;
using AuthenticationApi.Domain.Constants;
using AuthenticationApi.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
namespace AuthenticationApi.Infrastructure.AuthenticationApi.Infrastructure.CustomLogic
{
    public class JwtToken
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        public JwtToken(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public JwtSecurityToken SecurityToken(KeySecret secret)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret.ClientSecret ?? string.Empty));
            SigningCredentials cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: ApiConstant.Issuer,
                audience: secret.ClientId.ToString(),
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:TokenExpTimeMins"])),
                signingCredentials: cred);
            return token;
        }

    }
}
