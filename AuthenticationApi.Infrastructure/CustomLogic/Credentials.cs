using AuthenticationApi.Domain.Data;
using System.Security.Cryptography;
namespace AuthenticationApi.Infrastructure.CustomLogic
{
    public class Credentials
    {
        public static KeySecret GenerateSecret(string AudId)
        {
            KeySecret secret = new()
            {
                AudId = AudId,
                ClientId = Guid.NewGuid(),
                ClientSecret = GenerateClientSecret(32)
            };
            return secret;
        }
        private static string GenerateClientSecret(int length)
        {
            using var rng = RandomNumberGenerator.Create();
            byte[] secretByte = new byte[length / 2];
            rng.GetBytes(secretByte);
            return BitConverter.ToString(secretByte).Replace("-", "")[..length].ToLower();

        }
    }
}