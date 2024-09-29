using AuthenticationApi.Application.Interface;
using AuthenticationApi.Infrastructure.Data;
using AuthenticationApi.Infrastructure.CustomLogic;
using AuthenticationApi.Domain.Data;
namespace AuthenticationApi.Infrastructure.Repository;
public class SecretRepository(ApplicationDbContext context) : ISecretRepository
{
    private readonly ApplicationDbContext _context = context;
    public KeySecret GenerateSecret(string AudId)
    {
        KeySecret secret = Credentials.GenerateSecret(AudId);
        _context.KeySecret.Add(secret);
        _context.SaveChanges();
        return secret;
    }
    public KeySecret GetSecret(string clientId)
    {
        Guid id = Guid.Parse(clientId);
        KeySecret secret = _context.KeySecret.Find(id)
            ?? throw new NullReferenceException($"{clientId} Not found in database");
        return secret ?? new KeySecret();
    }
}

