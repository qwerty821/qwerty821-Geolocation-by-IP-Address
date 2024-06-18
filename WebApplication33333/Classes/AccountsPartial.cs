using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public partial class AccountsController
    {
        private void addUser(User user)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                User? u = db.Users.FirstOrDefault(x => x.Password == user.Password && x.Password == user.Password);
                if (u == null)
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    Console.WriteLine("a fost adaugat: " + user.Username);
                }
            }
        }

        private bool UserExist(User user)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var users = db.Users.ToList();
                foreach (User u in users)
                {
                    Console.WriteLine(u.Username);
                }
                foreach (User u in users)
                {
                    if (u.Username == user.Username && user.Password == u.Password)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private TokenResponse createToken(User user)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Username) };
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new TokenResponse(encodedJwt, user.Username);

        }
    }

    
}
