using MongoDB.Driver;
using System.Configuration;
using WebApplication.Classes;

namespace WebApplication.Services
{
    public static class ServiceProviderExtensions
    {
        public static void AddTimeService(this IServiceCollection services)
        {
            services.AddSingleton<TimeService>();
        }

        public static void AddMongoDB(this IServiceCollection services)
        {
            services.AddSingleton<MongoDbConnnectionFactory>();
        }

        public static void AddLocation(this IServiceCollection services)
        {
            services.AddSingleton<LocationService>();
        }
    }
}
