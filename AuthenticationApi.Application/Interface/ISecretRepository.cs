using AuthenticationApi.Domain.Data;
namespace AuthenticationApi.Application.Interface;
public interface ISecretRepository
{
    public  KeySecret GenerateSecret(string AudId);
    public KeySecret GetSecret(string clientId);
}
