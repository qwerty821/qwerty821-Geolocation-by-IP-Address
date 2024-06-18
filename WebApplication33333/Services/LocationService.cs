using Amazon.Runtime;
using Newtonsoft.Json;
using WebApplication.Models;

namespace WebApplication.Services
{
    public class LocationService
    {
        public async Task<ClientInfo> GetInfoAsync(IHttpClientFactory _httpClientFactory, string ip )
        {
           
            string key = "CCDA776C28D40FFA39C419849903EAED";

            string url = "https://api.ip2location.io/?";
            string reqUrl = $"{url}key={key}&ip={ip}";

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(reqUrl);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseBody);
            ClientInfo _clientInfo = JsonConvert.DeserializeObject<ClientInfo>(responseBody);
           
            return _clientInfo;
        }
    }
}
