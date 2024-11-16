using Application.IRepository;
using Infrastructure.DbContext;
using Infrastructure.GoogleDriveService;
using Infrastructure.Helper.Redis;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Oracle.ManagedDataAccess.Client;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class SetupRepository : ISetupRepository
    {
        private readonly OracleDbConnection _dbConnection;
        private CacheService _cacheService;
        private GoogleUtility _googleUtility;

        public SetupRepository(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("OracleConnection");
            _dbConnection = new OracleDbConnection(connectionString);
            _cacheService = new CacheService();
            _googleUtility = new GoogleUtility(configuration);
        }




    }


}
