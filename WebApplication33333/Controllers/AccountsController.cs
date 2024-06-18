using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public partial class AccountsController : Controller
    {
        private readonly ILogger _logger;
        public AccountsController(ILogger<AccountsController> logger) {
            _logger = logger;
        }

        [HttpGet]
        [Route("/accounts/login")]
        public IActionResult LogIn()
        {
            _logger.LogError("pagina login");
            return View();
        }
        [HttpGet]
        [Route("/accounts/signup")]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [Route("/accounts/signup")]
        public IActionResult SignUp(User user)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            addUser(user);
            return Redirect("/");
        }

        [HttpPost]
        [Route("/accounts/login")]
        public JsonResult LogIn(User user)
        {
            if (UserExist(user))
            {
                return Json(createToken(user));
            }
            return Json("not ok");
        }

        [HttpGet]
        [Route("accounts/profile")]
        [Authorize]
        public ActionResult Profile()
        {
            return View();
        }
    }
}
