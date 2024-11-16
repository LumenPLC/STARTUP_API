using Domain.Entities.Google_Drive;
using Infrastructure.DbContext;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.GoogleDriveService
{
    public class GoogleCredential
    {
        private readonly OracleDbConnection _dbConnection;
        public GoogleCredential(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("OracleConnection");
            _dbConnection = new OracleDbConnection(connectionString);
        }
        public Credential GetGoogleDriveCredential()
        {
            var list = new Credential();
            using (var connection = _dbConnection.GetConnection())
            {
                OracleParameter[] parameters = new OracleParameter[1];
                parameters[0] = _dbConnection.MakeOutParameter(OracleDbType.RefCursor, ParameterDirection.Output);
                list = _dbConnection.GetModelData<Credential>("DPG_GOOGLE_DRIVE_CREDENTIAL.DPD_GOOGLE_DRIVE_CREDENTIAL", parameters);
            }
            return list;
        }
    }
}
