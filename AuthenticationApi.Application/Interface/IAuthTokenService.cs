using AuthenticationApi.Domain.Data;
namespace AuthenticationApi.Application.Interface;
public interface IAuthTokenService
{
    public string GenerateToken(KeySecret secret);
}

