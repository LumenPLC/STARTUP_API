using Application.IRepository;
using Domain.Entities.Common.Params;
using Domain.Entities.ViewEntities.Menu;
using Domain.Entities.ViewEntities.Role;
using Google.Apis.Drive.v3.Data;
using Infrastructure.DbContext;
using Infrastructure.GoogleDriveService;
using Infrastructure.Helper.Redis;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class RoleRepository : IRoleRepository
    {


        private readonly OracleDbConnection _dbConnection;
        private CacheService _cacheService;

        public RoleRepository(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("OracleConnection");
            _dbConnection = new OracleDbConnection(connectionString);
            _cacheService = new CacheService();


        }





        public List<RoleModel> GetRoleList()
        {
            using (var connection = _dbConnection.GetConnection())
            {

                List<RoleModel> roleList = new List<RoleModel>();
                OracleParameter[] parameters = new OracleParameter[1];
                parameters[0] = _dbConnection.MakeOutParameter(OracleDbType.RefCursor, ParameterDirection.Output);

                roleList = _dbConnection.GetList<RoleModel>("DPG_ADMIN_ROLE_MST.DPD_ADMIN_USER_ROLE_GIRD", parameters);
               
                return roleList;

            }
        }











    }
}
