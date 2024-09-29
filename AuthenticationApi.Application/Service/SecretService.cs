using AuthenticationApi.Application.Interface;
using AuthenticationApi.Domain.Data;
namespace AuthenticationApi.Application.Service;
public class SecretService(ISecretRepository secretRepository) : ISecretService
{
    private readonly ISecretRepository _secretRepository = secretRepository;
    public KeySecret GenerateSecret(string AudId)
    {
        return _secretRepository.GenerateSecret(AudId);
    }
    public KeySecret GetSecret(string clientId)
    {
        return _secretRepository.GetSecret(clientId);
    }
}

