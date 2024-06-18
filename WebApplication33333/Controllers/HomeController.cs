using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using WebApplication.Classes;
using WebApplication.Models;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    public partial class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITimeService _timeService;
        private readonly MongoDbConnnectionFactory _mongoDb;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly LocationService _locationService;

        private Name? _name;

        //string test_ip = "81.180.211.46";


        public HomeController(ILogger<HomeController> logger, TimeService timeService, MongoDbConnnectionFactory factory, 
            IHttpClientFactory httpClientFactory, LocationService locationService)
        {
            _logger = logger;
            _timeService = timeService;
            _mongoDb = factory;
            _httpClientFactory = httpClientFactory;
            _locationService = locationService;
 
        }
        [Route("/")]
        public async Task<IActionResult> Index()
        {
            string ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "null";
            ClientInfo clientInfo = await _locationService.GetInfoAsync(_httpClientFactory, ip);
             
            clientInfo.Nume = _name?.name ?? "null";
            

            DateTime now = DateTime.UtcNow;  
            TimeSpan offset = new TimeSpan(2, 0, 0);
            DateTimeOffset europeanTime = new DateTimeOffset(now).ToOffset(offset);
            clientInfo.EnterTime = europeanTime.ToString("dd/MM/yyyy HH:mm:ss zzz");

            insertClientInfo(clientInfo);

            if (clientInfo.Latitude == null)
            {
                clientInfo.Longitude = "null";
                clientInfo.Latitude = "null";
            }

            return await Task.Run(() =>
                View(clientInfo));

            //return View();
        }

        [Route("/name")]
        public async void IndexWithName()
        {
            string ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? " ";
            var reader = await Request.BodyReader.ReadAsync();
            var buffer = reader.Buffer;

            var body = Encoding.UTF8.GetString(buffer.FirstSpan);

            _name = JsonConvert.DeserializeObject<Name>(body ?? "");
            if (_name != null && _name.name != "")
            {
                DateTime now = DateTime.UtcNow;
                TimeSpan offset = new TimeSpan(2, 0, 0);
                DateTimeOffset europeanTime = new DateTimeOffset(now).ToOffset(offset);
                string EnterTime = europeanTime.ToString("dd/MM/yyyy HH:mm:ss zzz");

                await UpdateClientInfo(ip, _name.name, EnterTime);

            }
          
        }
        record Name(string name);
        public IActionResult res()
        {
            return View();
        }
     
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

     
    }
}
