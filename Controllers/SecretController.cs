using AuthenticationApi.Application.Interface;
using AuthenticationApi.Domain.Data;
using Microsoft.AspNetCore.Mvc;
namespace AuthenticationApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class SecretController(ISecretService SecretService) : ControllerBase
    {
        private readonly ISecretService _secretService = SecretService;
        [HttpPost("GenerateSecret")]
        public IActionResult GenerateSecret([FromHeader] string AudId)
        {
            try
            {
                KeySecret secret = _secretService.GenerateSecret(AudId);
                return Ok(secret);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetSecret")]
        public IActionResult GetSecret([FromHeader] string ClientId)
        {
            try
            {
                KeySecret secret = _secretService.GetSecret(ClientId);
                if (secret == new KeySecret())//Guid.Equals(secret.ClientId, null) && secret.ClientSecret == null && secret.AudId == null
                {
                    return BadRequest($"Secret for {ClientId} Not found in database");
                }
                return Ok(secret);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}