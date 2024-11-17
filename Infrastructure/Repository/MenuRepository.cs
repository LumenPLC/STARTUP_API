using Application.IRepository;
using Domain.Entities.Common.Params;
using Domain.Entities.ViewEntities.Menu;
using Domain.Entities.ViewEntities.Role;
using Infrastructure.DbContext;
using Infrastructure.GoogleDriveService;
using Infrastructure.Helper.Redis;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class MenuRepository : IMenuRepository
    {
        private readonly OracleDbConnection _dbConnection;
        private CacheService _cacheService;
        private GoogleUtility _googleUtility;
        public MenuRepository(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("OracleConnection");
            _dbConnection = new OracleDbConnection(connectionString);
            _cacheService = new CacheService();
            _googleUtility = new GoogleUtility(configuration);

        }

       

        public List<MenuVM> GetMenu(string USER_CODE, int ROLE_ID)
        {
            string redisKey = USER_CODE + "_" + ROLE_ID + "_dBMenuList";
            var cacheData = _cacheService.GetData<IEnumerable<MenuVM>>(redisKey);
            if (cacheData != null)
            {
                return (List<MenuVM>)cacheData;
            }
            var expirationTime = DateTimeOffset.Now.AddMinutes(5.0);
            var list = new List<MenuVM>();
            using (var connection = _dbConnection.GetConnection())
            {
                OracleParameter[] parameters = new OracleParameter[2];
                parameters[0] = _dbConnection.MakeOutParameter(OracleDbType.RefCursor, ParameterDirection.Output);
              //  parameters[1] = _dbConnection.MakeInParameter(USER_CODE, OracleDbType.Int16);
                parameters[1] = _dbConnection.MakeInParameter(ROLE_ID, OracleDbType.Int16);
                list = _dbConnection.GetList<MenuVM>("DPG_ADMIN_LOGIN.DPD_ADMIN_MENU", parameters);

                MenuVM menu = new MenuVM();
                menu.MD = "ADMIN";
                menu.PMIID = 0;
                menu.ISM = "N";
                menu.SGID = 1;
                menu.SL = 1;
                list.Add(menu);
               
                cacheData = list;
                _cacheService.SetData<IEnumerable<MenuVM>>(redisKey, cacheData, expirationTime);
                return (List<MenuVM>)cacheData;
            }
        }

        public List<OrgInfoGrid> GetOrganizationInfo(OrgBranchParam orgBranchParam)
        {
            using (var connection = _dbConnection.GetConnection())
            {

                List<OrgInfoGrid> orgInfoList = new List<OrgInfoGrid>();
                OracleParameter[] parameters = new OracleParameter[3];
                parameters[0] = _dbConnection.MakeOutParameter(OracleDbType.RefCursor, ParameterDirection.Output);
                parameters[1] = _dbConnection.MakeInParameter(orgBranchParam.ORG_ID, OracleDbType.Decimal);
                parameters[2] = _dbConnection.MakeInParameter(orgBranchParam.BRANCH_ID, OracleDbType.Decimal);
                orgInfoList = _dbConnection.GetList<OrgInfoGrid>("DPG_ADMIN_LOGIN.DPD_ADMIN_ORG_INFO", parameters);
                if (!string.IsNullOrEmpty(orgInfoList[0].ORG_IMAGE_URL))
                {
                    string base64Image = _googleUtility.GetFilesByte(orgInfoList[0].ORG_IMAGE_URL);
                    try
                    {
                        // Assuming PHOTO_FILE_PATH contains the file system path
                        orgInfoList[0].ORG_IMAGE_BYTE = base64Image;
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions (e.g., file not found)
                        // You might want to log this or handle it as needed
                        orgInfoList[0].ORG_IMAGE_BYTE = null; // or handle it differently
                    }
                }

                return orgInfoList;

            }
        }


        //// role assign 



        public List<AllMenuViewModel> GetAllMenuList()
        {
            using (var connection = _dbConnection.GetConnection())
            {

                List<AllMenuViewModel> menuList = new List<AllMenuViewModel>();
                OracleParameter[] parameters = new OracleParameter[1];
                parameters[0] = _dbConnection.MakeOutParameter(OracleDbType.RefCursor, ParameterDirection.Output);

                menuList = _dbConnection.GetList<AllMenuViewModel>("DPG_ADMIN_ROLE_MENU_MAP.DPD_ADMIN_ALL_MENU_V3", parameters);

                return menuList;

            }
        }



        public List<UserRoleAssign> GetUserRoleAssignList(int roleId)
        {
            using (var connection = _dbConnection.GetConnection())
            {

                List<UserRoleAssign> roleAssignList = new List<UserRoleAssign>();
                OracleParameter[] parameters = new OracleParameter[2];
                parameters[0] = _dbConnection.MakeOutParameter(OracleDbType.RefCursor, ParameterDirection.Output);
                parameters[1] = _dbConnection.MakeInParameter(roleId, OracleDbType.Int16);
                roleAssignList = _dbConnection.GetList<UserRoleAssign>("DPG_ADMIN_ROLE_MENU_MAP.DPD_USER_MENU_LIST_ASSIGN_V3", parameters);

                return roleAssignList;

            }
        }







        public (int status, string[] message) SaveRoleMenu(RoleMenuMapViewModel roleMenuMapViewModel)
        {
            using (var connection = _dbConnection.GetConnection())
            {
                int status = 0;
                string[] message = new string[2];
                decimal[] ArrMenuItemid = roleMenuMapViewModel.MENU_ITEM_ID.ToArray();

                try
                {
                    OracleParameter[] Params = new OracleParameter[4];
                    Params[0] = _dbConnection.MakeOutParameter(OracleDbType.Int16, ParameterDirection.Output);                
                    Params[1] = _dbConnection.MakeInParameter(roleMenuMapViewModel.ROLE_ID, OracleDbType.Decimal);
                    Params[2] = _dbConnection.MakeCollectionParameter(ArrMenuItemid, OracleDbType.Decimal, ArrMenuItemid.Length);
                    Params[3] = _dbConnection.MakeInParameter(roleMenuMapViewModel.USER_CODE, OracleDbType.Varchar2);

                    var Status = _dbConnection.RunProcedureWithReturnValAndStatus("DPG_ADMIN_ROLE_MENU_MAP.DPD_ADMIN_ROLE_MENU_MAP_V3", Params);
                  
                    if (status == 1)
                    {
                        message[0] = "Assigned roles are saved successfully !!";
                        message[1] = "#5cb85c";
                    }
                    else
                    {
                        message[0] = "Role assign saved failed. Please try again.";
                        message[1] = "#e35b5a";
                    }

                    return (Status.status, message);

                }
                catch (Exception ex)
                {
                    message[0] = ex.Message;
                    return (0, message);
                }
            }
        }




















    }
}
