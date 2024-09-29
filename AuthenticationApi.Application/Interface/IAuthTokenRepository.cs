using AuthenticationApi.Domain.Data;
namespace AuthenticationApi.Application.Interface;
public interface IAuthTokenRepository
{
    public  string GenerateToken(KeySecret secret);
}
