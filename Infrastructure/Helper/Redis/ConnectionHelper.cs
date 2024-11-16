using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Helper.Redis
{
    public class ConnectionHelper
    {
        static ConnectionHelper()
        {
            IConfiguration configuration = new ConfigurationBuilder()
          .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
          .Build();

            string redisUrl = configuration["RedisURL"];
            ConnectionHelper.lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            {
                return ConnectionMultiplexer.Connect(redisUrl);
            });
        }

        private static Lazy<ConnectionMultiplexer> lazyConnection;

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }
    }
}
