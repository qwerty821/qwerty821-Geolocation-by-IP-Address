using MongoDB.Driver;
using WebApplication.Models;
using static MongoDB.Driver.WriteConcern;

namespace WebApplication.Controllers
{
    public partial class HomeController
    {
        public async Task insertClientInfo(ClientInfo clientInfo)
        {
            var _collection = _mongoDb.GetDatabase().GetCollection<ClientInfo>("clients");
            var filter = Builders<ClientInfo>.Filter.Eq(user => user.Ip, clientInfo.Ip);
            bool exists = await _collection.Find(filter).AnyAsync();

            if (exists == false)
            {
                await Insert(clientInfo, _collection);
            } else
            {
                await UpdateClientInfo(clientInfo, _collection, filter);
                await UpdateClientInfo(clientInfo.Ip, date:clientInfo.EnterTime);
                 
            }
        }
        public async Task<bool> Insert(ClientInfo clientInfo, IMongoCollection<ClientInfo> _collection)
        {
            try
            {
                await _collection.InsertOneAsync(clientInfo);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateClientInfo(ClientInfo clientInfo, IMongoCollection<ClientInfo> _collection, FilterDefinition<ClientInfo> filter)
        {
            var update = Builders<ClientInfo>.Update.Inc(client => client.AccessTimes, 1);
            var result = await _collection.UpdateOneAsync(filter, update);
        }
        public  async Task UpdateClientInfo(string ip, string? name = null, string? date = null)
        {
            var _collection = _mongoDb.GetDatabase().GetCollection<ClientInfo>("clients");

            var filter1 = Builders<ClientInfo>.Filter.Eq("ip", ip);

            var client = await _collection.Find(filter1).ToListAsync();

            var filter = Builders<ClientInfo>.Filter.Eq(user => user.Ip, ip);
            if (name != null)
            {
                var update = Builders<ClientInfo>.Update.Set(user => user.Nume, client[0].Nume + ", " + name);
                await _collection.UpdateOneAsync(filter, update);
            }
            if (date != null)
            {
                var update1 = Builders<ClientInfo>.Update.Set(user => user.EnterTime, client[0].EnterTime + ", " + date);
                await _collection.UpdateOneAsync(filter, update1);

            }
            bool exists = await _collection.Find(filter).AnyAsync();
            if (exists)
            {
                //await Console.Out.WriteLineAsync("exista");
            } else
            {
                //await Console.Out.WriteLineAsync("nu exista");
            }
         
        }

    }
}
