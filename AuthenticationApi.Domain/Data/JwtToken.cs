namespace AuthenticationApi.Domain.Data
{
    public class JwtToken
    {
        public string? Token {  get; set; } 
        public string? TokenIssueTime { get; set; }
        public string? TokenExpirationTime { get; set; }
    }
}
