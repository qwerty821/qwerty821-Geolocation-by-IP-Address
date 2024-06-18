using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class TestController : Controller
    {

        private readonly IHttpClientFactory _httpClientFactory;
        public TestController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        [Route("/test")]
        public async Task<IActionResult> Index()
        {
          
            return await Task.Run(() =>  View());
        
        }
    }
}
