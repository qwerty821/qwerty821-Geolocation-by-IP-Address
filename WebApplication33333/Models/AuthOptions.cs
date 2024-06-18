using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebApplication.Models
{
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer";  
        public const string AUDIENCE = "MyAuthClient";  
        const string KEY = "keysecretkey11111sssssssadasdssd213!123";    
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}