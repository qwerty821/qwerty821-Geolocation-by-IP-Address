using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class AutentificareController : Controller
    {

        private IWebHostEnvironment _env;
        public AutentificareController(IWebHostEnvironment env) 
        {
            _env = env;    
        }
        [HttpGet]
        [Route("/autentificare")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("/autentificare")]
        public IResult AuthVerify([FromBody] User user)
        {
            user.Username = user.Email;
            Console.WriteLine(user.Username + " , " + user.Password);
            

            string path = _env.WebRootPath + "/json/utilizatori.json";
            string text = System.IO.File.ReadAllText(path);

            List<User> users = JsonConvert.DeserializeObject<List<User>>(text);
           
            bool ok = false;
            foreach (User us in users)
            {
                if (us.Password == user.Password && us.Password == user.Password)
                {
                    HttpContext.Session.SetString("user", user.Username);
                    if (us.isAdmin == "true")
                    {
                        HttpContext.Session.SetString("admin", "true");
                    }
                    else
                    {
                        HttpContext.Session.Remove("admin");
                    }
                    ok = true;
                    break;
                }
            }
            if (ok)
            {
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Username) };
                var jwt = new JwtSecurityToken(
                        issuer: AuthOptions.ISSUER,
                        audience: AuthOptions.AUDIENCE,
                        claims: claims,
                        expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                var response = new
                {
                    access_token = encodedJwt,
                    Username = user.Username
                };
                Console.WriteLine("ok");
                return Results.Json(response);
            }
            else
            {
                Console.WriteLine("not ok");
                TempData["eroare"] = "Acest utilizator nu exista" ;

                return Results.Unauthorized();
            }
            
        }
        [Authorize]
        [Route("/data")]
        public string Re(HttpContext context)
        {
            return "Yeees";
        }
        //[Authorize]
        //[Route("login-verify")]
        //public IResult TokenVerify()
        //{
        //    if ()
        //}
    }
}
