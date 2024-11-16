using Application.IRepository;
using Domain.Entities.ViewEntities.Auth;
using Infrastructure.DbContext;
using Infrastructure.GoogleDriveService;
using Infrastructure.Helper.Redis;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly OracleDbConnection _dbConnection;
        private CacheService _cacheService;
        private GoogleUtility _googleUtility;


        public AuthRepository(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("OracleConnection");
            _dbConnection = new OracleDbConnection(connectionString);
            _cacheService = new CacheService();
            _googleUtility = new GoogleUtility(configuration);

        }

        public AdminUserMstVM GetLoggedData(UserParams user)
        {
            AdminUserMstVM loginData = new AdminUserMstVM();
            using (var connection = _dbConnection.GetConnection())
            {
                OracleParameter[] oracleParameter = new OracleParameter[3];
                oracleParameter[0] = _dbConnection.MakeOutParameter(OracleDbType.RefCursor, ParameterDirection.Output);
                oracleParameter[1] = _dbConnection.MakeInParameter(user.username, OracleDbType.Varchar2);
                oracleParameter[2] = _dbConnection.MakeInParameter(user.Password, OracleDbType.Varchar2);

                loginData = _dbConnection.GetModelData<AdminUserMstVM>("DPG_ADMIN_LOGIN.DPD_ADMIN_LOGIN_STATUS_CHECK", oracleParameter);
                if (!string.IsNullOrEmpty(loginData.ORG_IMAGE_URL))
                {
                    string base64Image = _googleUtility.GetFilesByte(loginData.ORG_IMAGE_URL);
                    try
                    {
                        loginData.ORG_IMAGE_BYTE = base64Image;
                    }
                    catch (Exception ex)
                    {
                        loginData.ORG_IMAGE_BYTE = null; // or handle it differently
                    }
                }
                return loginData;
            }
        }


    }
}
