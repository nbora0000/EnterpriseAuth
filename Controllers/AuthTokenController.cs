using AuthenticationApi.Application.Interface;
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
    }
}