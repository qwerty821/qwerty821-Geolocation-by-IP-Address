using MongoDB.Driver;
using System.Configuration;

namespace WebApplication.Classes
{
    public class MongoDbConnnectionFactory
    {
        private readonly IConfiguration _configuration;

        public MongoDbConnnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IMongoDatabase GetDatabase()
        {
            var connectionString = _configuration.GetSection("MyDb").GetValue<string>("ConnectionString");
            var dataBaseName = _configuration.GetSection("MyDb").GetValue<string>("DatabaseName");

            var url = MongoUrl.Create(connectionString);
            var client = new MongoClient(url);
            var dataBase = client.GetDatabase(dataBaseName);

            return dataBase;
        }
    }
}
