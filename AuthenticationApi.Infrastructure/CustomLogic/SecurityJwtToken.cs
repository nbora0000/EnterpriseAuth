using AuthenticationApi.Domain.Data;
using AuthenticationApi.Domain.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
namespace AuthenticationApi.Infrastructure.CustomLogic
{
    public class SecurityJwtToken(IConfiguration configuration)
    {
        private readonly IConfiguration _configuration = configuration;
        public JwtSecurityToken SecurityToken(KeySecret secret)
        {
            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(secret.ClientSecret ?? string.Empty));
            SigningCredentials cred = new(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new(issuer: ApiConstant.Issuer,
                                         audience: _configuration["Jwt:Audience"],
                                         expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:TokenExpTimeMins"])),
                                         signingCredentials: cred);
            return token;
        }

    }
}
