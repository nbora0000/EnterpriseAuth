using AuthenticationApi.Application.Interface;
using AuthenticationApi.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
namespace AuthenticationApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AuthTokenController : ControllerBase
    {
        private readonly IAuthTokenService _authTokenService;
        public AuthTokenController(IAuthTokenService authTokenService)
        {
            _authTokenService = authTokenService;
        }
        [HttpPost("GenerateToken")]
        public IActionResult GetToken([FromBody] KeySecret secret)
        {
            try
            {
                string token = _authTokenService.GenerateToken(secret);
                if (string.IsNullOrEmpty(token))
                {
                    return BadRequest("Incorrect Payload");
                }
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("GenerateSecret")]
        public IActionResult GenerateSecret([FromHeader] string AudId)
        {
            try
            {
                KeySecret secret = _authTokenService.GenerateSecret(AudId);
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
                KeySecret secret = _authTokenService.GetSecret(ClientId);
                if (Guid.Equals(secret.ClientId,null) && secret.ClientSecret == null && secret.AudId == null)
                {
                    return BadRequest($"Secret for {ClientId} Not found in database");
                }
                return Ok(JsonSerializer.Serialize(secret));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}