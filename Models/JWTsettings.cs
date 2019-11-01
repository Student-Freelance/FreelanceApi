namespace Freelance_Api.Models
{
    public class JWTsettings: IJWTsettings
    {
        public string JwtKey { get; set; }
        public string JwtIssuer { get; set; }
    }
    
    public interface IJWTsettings
    {
        string JwtKey { get; set; }
        string JwtIssuer { get; set; }
    }
}