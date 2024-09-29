using AuthenticationApi.Domain.Data;
namespace AuthenticationApi.Application.Interface;
public interface ISecretService
{
    public KeySecret GenerateSecret(string AudId);
    public KeySecret GetSecret(string clientId);
}

