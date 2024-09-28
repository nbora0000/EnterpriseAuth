using AuthenticationApi.Application.Interface;
using AuthenticationApi.Domain;
using AuthenticationApi.Domain.Data;
using Microsoft.AspNetCore.Mvc;
namespace AuthenticationApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AuthTokenController(IAuthTokenService authTokenService, IConfiguration configuration) : ControllerBase
    {
        private readonly IAuthTokenService _authTokenService = authTokenService;
        private readonly IConfiguration _configuration = configuration;

        public IActionResult GetToken([FromBody] KeySecret secret)
        {
            return GetToken(secret, _authTokenService);
        }

        [HttpPost("GenerateToken")]
        public IActionResult GetToken([FromBody] KeySecret secret, IAuthTokenService _authTokenService)
        {
            try
            {
                string token = _authTokenService.GenerateToken(secret);
                if (string.IsNullOrEmpty(token))
                {
                    return BadRequest("Incorrect Payload");
                }
                JwtToken jwtToken = new()
                {
                    Token = token,
                    TokenIssueTime = DateTime.UtcNow.ToString(),
                    TokenExpirationTime = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:TokenExpTimeMins"])).ToString()
                };
                return Ok(jwtToken);
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
                if (Guid.Equals(secret.ClientId, null) && secret.ClientSecret == null && secret.AudId == null)
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